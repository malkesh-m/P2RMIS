using Sra.P2rmis.Bll.Rules;
using Sra.P2rmis.Bll.Setup.Blocks;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Bll.Setup.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Bll.Setup
{
    /// <summary>
    /// Provided services for System Setup.
    /// </summary>
    public partial interface ISetupService
    {
        List<ServiceState> AddAward(int programYearId, int clientAwardTypeId, int receiptCycle, Nullable<DateTime> receiptDeadline, bool blindedFlag, string fundingOpportunityId, bool partneringPiAllowedFlag, bool? preApp, DateTime? preAppDue, int userId);
        ServiceState AddPreAppAward(int parentProgramMechanismId, int receiptCycle, Nullable<DateTime> receiptDeadline, bool blindedFlag, int userId);
        ServiceState ModifyAward(int programMechanismId, int clientAwardTypeId, int receiptCycle, Nullable<DateTime> receiptDeadline, bool blindedFlag, string fundingOpportunityId, bool partneringPiAllowedFlag, int userId);
        ServiceState ModifyPreAppAward(int programMechanismId, int receiptCycle, Nullable<DateTime> receiptDeadline, bool blindedFlag, int userId);
        ServiceState DeleteAward(int programMechanismId, int userId);
        ServiceState SaveSummarySetup(int programMechanismId, int? selectedStandardSummaryTemplateId, int? selectedExpeditedSummaryTemplateId, int userId, List<SummaryStatementReviewerDescription> reviewerDescriptions);
    }
    /// <summary>
    /// Provides services for the Awards functions of System Setup.
    /// </summary>
    public partial class SetupService
    {
        public class Constants
        {
            /// <summary>
            /// The base pre application cycle value
            /// </summary>
            public const int BasePreAppCycleValue = 25;
            /// <summary>
            /// Pre-app mechanism relationship identifier
            /// </summary>
            public const int PreAppMechanismRelationshipId = 1;
        }

        /// <summary>
        /// Adds the award.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientAwardTypeId">The client award type identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="receiptDeadline">The receipt deadline.</param>
        /// <param name="blindedFlag">if set to <c>true</c> [blinded flag].</param>
        /// <param name="fundingOpportunityId">The funding opportunity identifier.</param>
        /// <param name="partneringPiAllowedFlag">if set to <c>true</c> [partnering pi allowed flag].</param>
        /// <param name="preApp">The pre application.</param>
        /// <param name="preAppDue">The pre application due.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public virtual List<ServiceState> AddAward(int programYearId, int clientAwardTypeId, int receiptCycle, Nullable<DateTime> receiptDeadline,
                                             bool blindedFlag, string fundingOpportunityId, bool partneringPiAllowedFlag, bool? preApp, DateTime? preAppDue, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(AddAward));
            ValidateInteger(programYearId, name, nameof(programYearId));
            ValidateInteger(clientAwardTypeId, name, nameof(clientAwardTypeId));
            ValidateInteger(receiptCycle, name, nameof(receiptCycle));
            ValidateInteger(userId, name, nameof(userId));
            //ValidateString(fundingOpportunityId, name, nameof(fundingOpportunityId));
            //
            // First we create the two templates
            //
            MechanismTemplate templateAsynchronous = CreateMechanismTemplate(ReviewStage.Indexes.Asynchronous, userId);
            MechanismTemplate templateSynchronous = CreateMechanismTemplate(ReviewStage.Indexes.Synchronous, userId);
            //
            // Then we create the Award operation and add it.
            //
            AwardSetupBlock block = new AwardSetupBlock(programYearId, clientAwardTypeId, receiptCycle, receiptDeadline,
                                                        blindedFlag, fundingOpportunityId, partneringPiAllowedFlag);
            block.AddTemplate(templateAsynchronous);
            block.AddTemplate(templateSynchronous);
            block.ConfigureAdd();

            List<ServiceState> stateOfPreAppServices = new List<ServiceState>();

            stateOfPreAppServices.Add(DoCrud(block, null, CrudAction.Add, 0, true, userId));

            // if preApp is true, also create a preApp with the due date passed in as well.
            if (preApp.HasValue && preApp == true)
            {
                // need to retrieve parent record first
                var newParentAward = UnitOfWork.ProgramMechanismRepository.Select()
                    .Where(x => x.ProgramYearId == programYearId && x.ClientAwardTypeId == clientAwardTypeId && x.ReceiptCycle == receiptCycle && x.ParentProgramMechanismId == null).FirstOrDefault();

                if (newParentAward != null)
                {
                   // use derive method to assign the preApp cycle.
                   stateOfPreAppServices.Add(AddPreAppAward(newParentAward.ProgramMechanismId, GetPreAppReceiptCycle(newParentAward.ProgramYearId, receiptCycle), preAppDue, blindedFlag, userId));
                }
           }

            return stateOfPreAppServices;
        }       
        /// <summary>
        /// Gets the pre application receipt cycle.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="parentReceiptCycle">The parent receipt cycle.</param>
        /// <returns></returns>
        internal virtual int GetPreAppReceiptCycle(int programYearId, int parentReceiptCycle)
        {
            int cycle = Constants.BasePreAppCycleValue;
            var mechanisms = UnitOfWork.ProgramMechanismRepository.Get(x => x.ProgramYearId == programYearId);
            var mechanismIdsWithSameCycle = mechanisms.Where(o => o.ReceiptCycle == parentReceiptCycle)
                .Select(o2 => o2.ProgramMechanismId).ToList();
            var preAppMechanisms = mechanisms.Where(x => x.ParentProgramMechanismId != null);
            var preAppMechanismWithSameParentCycle = preAppMechanisms.Where(x => mechanismIdsWithSameCycle.Contains((int)x.ParentProgramMechanismId));

            if (preAppMechanismWithSameParentCycle.Count() > 0)
            {
                cycle = (int)preAppMechanismWithSameParentCycle.FirstOrDefault().ReceiptCycle;
            }
            else if (preAppMechanisms.Count() > 0)
            {
                cycle = (int)preAppMechanisms.Max(x => x.ReceiptCycle) + 1;
            }
            return cycle;
        }

        /// <summary>
        /// Create a MechanismTemplate when a ProgramMechanism is created
        /// </summary>
        /// <param name="reviewStageId">ReviewStage entity identifier of the stage being created</param>
        /// <param name="userId">User entity identifier of the user adding the award</param>
        /// <returns></returns>
        internal virtual MechanismTemplate CreateMechanismTemplate(int reviewStageId, int userId)
        {
            //
            // Create the block....
            //
            MechanismTemplateBlock block = new MechanismTemplateBlock(0, null, ReviewStatu.NoPriority, reviewStageId, null);
            block.ConfigureAdd();
            //
            // then Create the action & execute it
            //
            MechanismTemplateServiceAction action = new MechanismTemplateServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.MechanismTemplateRepository, false, 0, userId);
            action.Populate(block);
            action.Execute();
            action.PostProcess();
            return action.CRUDedEntity;
        }


        /// <summary>
        /// Creates a new ProgramMechanismSummaryStatement record
        /// </summary>
        /// <param name="reviewStatusId"></param>
        /// <param name="clientSummaryTemplateId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal virtual void CreateProgramMechanismSummaryStatement(ProgramMechanism programMechanismEntity, int reviewStatusId, int clientSummaryTemplateId, int userId)
        {
            ProgramMechanismSummaryStatement mechSSEntity = new ProgramMechanismSummaryStatement();
            mechSSEntity.Populate(reviewStatusId, clientSummaryTemplateId);
            Helper.UpdateCreatedFields(mechSSEntity, userId);
            Helper.UpdateModifiedFields(mechSSEntity, userId);
            programMechanismEntity.ProgramMechanismSummaryStatements.Add(mechSSEntity);
            UnitOfWork.ProgramMechanismSummaryStatementRepository.Add(mechSSEntity);
        }

        /// <summary>
        /// Removes a SummaryReviewerDescription record
        /// </summary>
        /// <param name="descToRemove">Entity instance to be remove</param>
        /// <param name="userId">UserId performing the action</param>
        internal virtual void RemoveReviewerDescription(SummaryReviewerDescription descToRemove, int userId)
        {
            Helper.UpdateDeletedFields(descToRemove, userId);
            UnitOfWork.SummaryReviewerDescriptionRepository.Delete(descToRemove);
        }



        /// <summary>
        /// Creates a reviewer description record
        /// </summary>
        internal virtual void AddReviewerDescription(ProgramMechanism programMechanismEntity, int assignmentOrder, int customOrder, string displayName, int userId)
        {
            SummaryReviewerDescription revDescriptionEntity = new SummaryReviewerDescription();
            revDescriptionEntity.Populate(assignmentOrder, customOrder, displayName);
            Helper.UpdateCreatedFields(revDescriptionEntity, userId);
            Helper.UpdateModifiedFields(revDescriptionEntity, userId);
            programMechanismEntity.SummaryReviewerDescriptions.Add(revDescriptionEntity);
            UnitOfWork.SummaryReviewerDescriptionRepository.Add(revDescriptionEntity);
        }


        /// <summary>
        /// Updates a reviewer description record
        /// </summary>
        internal virtual void ModifyReviewerDescription(SummaryReviewerDescription descToModify, int assignmentOrder, int customOrder, string displayName, int userId)
        {
            var hasChanges = (descToModify.AssignmentOrder != assignmentOrder) || (descToModify.CustomOrder != customOrder) || (descToModify.DisplayName != displayName);
            if (hasChanges)
            {
                descToModify.Populate(assignmentOrder, customOrder, displayName);
                Helper.UpdateModifiedFields(descToModify, userId);
                UnitOfWork.SummaryReviewerDescriptionRepository.Update(descToModify);
            }
        }
        /// <summary>
        /// Service method to add a new PreApp award to a program.
        /// </summary>
        /// <param name="parentProgramMechanismId"></param>
        /// <param name="receiptCycle">User specified receipt cycle value</param>
        /// <param name="receiptDeadline">User specified receipt date</param>
        /// <param name="blindedFlag">User specified  blinded flag value</param>
        /// <param name="userId">User entity identifier of the user adding the award</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        public virtual ServiceState AddPreAppAward(int parentProgramMechanismId, int receiptCycle, Nullable<DateTime> receiptDeadline, bool blindedFlag, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(AddPreAppAward));
            ValidateInteger(parentProgramMechanismId, name, nameof(parentProgramMechanismId));
            ValidateInteger(receiptCycle, name, nameof(receiptCycle));
            ValidateInteger(userId, name, nameof(userId));
            //
            // First we create the two templates
            //
            MechanismTemplate templateAsynchronous = CreateMechanismTemplate(ReviewStage.Indexes.Asynchronous, userId);
            MechanismTemplate templateSynchronous = CreateMechanismTemplate(ReviewStage.Indexes.Synchronous, userId);
            //
            // These are the unique steps for an Add PreApp Award operation.
            //
            ProgramMechanism parentEntity = UnitOfWork.ProgramMechanismRepository.GetByID(parentProgramMechanismId);
            AwardSetupBlock block = new AwardSetupBlock(parentEntity.ProgramYearId, parentEntity.ClientAwardTypeId,
                                                        receiptCycle, receiptDeadline,  blindedFlag, parentEntity.FundingOpportunityId, 
                                                        parentEntity.PartneringPiAllowedFlag);
            block.AddTemplate(templateAsynchronous);
            block.AddTemplate(templateSynchronous);
            block.ConfigureAdd(parentProgramMechanismId);
            return DoCrud(block, null, CrudAction.Add, 0, true, userId);
        }
        /// <summary>
        /// Service method to modify an existing ProgramMechanism.
        /// </summary>
        /// <param name="programMechanismId">ProgramMechanism entity identifier</param>
        /// <param name="clientAwardTypeId">ClientAwardType entity identifier</param>
        /// <param name="receiptCycle">User specified receipt cycle value</param>
        /// <param name="receiptDeadline">User specified receipt date</param>
        /// <param name="blindedFlag">User specified  blinded flag value</param>
        /// <param name="fundingOpportunityId">User specified funded opportunity id</param>
        /// <param name="partneringPiAllowedFlag">User specified indicator if partnering is allowed for the PI</param>
        /// <param name="userId">User entity identifier of the user adding the award</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        public virtual ServiceState ModifyAward(int programMechanismId, int clientAwardTypeId, int receiptCycle, Nullable<DateTime> receiptDeadline,
                                                bool blindedFlag, string fundingOpportunityId, bool partneringPiAllowedFlag, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(ModifyAward));
            ValidateInteger(programMechanismId, name, nameof(programMechanismId));
            ValidateInteger(clientAwardTypeId, name, nameof(clientAwardTypeId));
            ValidateInteger(receiptCycle, name, nameof(receiptCycle));
            ValidateInteger(userId, name, nameof(userId));
            //
            // These are the unique steps to modify an existing ProgramMechanism.
            //
            ProgramMechanism entity = UnitOfWork.ProgramMechanismRepository.GetByID(programMechanismId);
            AwardSetupBlock block = new AwardSetupBlock(entity.ProgramYearId, clientAwardTypeId, receiptCycle, receiptDeadline,
                                                        blindedFlag, fundingOpportunityId, partneringPiAllowedFlag, programMechanismId);
            if (entity.ParentProgramMechanismId != null)
                block.ConfigureModify(entity.ParentProgramMechanismId);
            else
                block.ConfigureModify();
            return DoCrud(block, entity, CrudAction.Modify, programMechanismId, true, userId);
        }
        /// <summary>
        /// Service method to modify an existing PreApp ProgramMechanism.
        /// </summary>
        /// <param name="programMechanismId">ProgramMechanism entity identifier</param>
        /// <param name="receiptCycle">User specified receipt cycle value</param>
        /// <param name="receiptDeadline">User specified receipt date</param>
        /// <param name="blindedFlag">User specified  blinded flag value</param>
        /// <param name="userId">User entity identifier of the user adding the award</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        public virtual ServiceState ModifyPreAppAward(int programMechanismId, int receiptCycle, Nullable<DateTime> receiptDeadline,  bool blindedFlag, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(ModifyPreAppAward));
            ValidateInteger(programMechanismId, name, nameof(programMechanismId));
            ValidateInteger(receiptCycle, name, nameof(receiptCycle));
            ValidateInteger(userId, name, nameof(userId));
            //
            // These are the unique steps to modify an existing PreApp ProgramMechanism.
            //
            ProgramMechanism entity = UnitOfWork.ProgramMechanismRepository.GetByID(programMechanismId);
            AwardSetupBlock block = new AwardSetupBlock(entity.ProgramYearId, entity.ClientAwardTypeId, receiptCycle, receiptDeadline,
                                                        blindedFlag, entity.FundingOpportunityId, entity.PartneringPiAllowedFlag, programMechanismId);
            block.ConfigureModify(entity.ParentProgramMechanismId);
            return DoCrud(block, entity, CrudAction.Modify, programMechanismId, true, userId);
        }
        /// <summary>
        /// Service method to delete an existing ProgramMechanism.
        /// </summary>
        /// <param name="programMechanismId">Identifies the ProgramMechanism to be deleted</param>
        /// <param name="userId">User entity identifier of the user deleting the award</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        public virtual ServiceState DeleteAward(int programMechanismId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(DeleteAward));
            ValidateInteger(programMechanismId, name, nameof(programMechanismId));
            ValidateInteger(userId, name, nameof(userId));

            ProgramMechanism entity = UnitOfWork.ProgramMechanismRepository.GetByID(programMechanismId);
            AwardSetupBlock block = new AwardSetupBlock(programMechanismId);
            block.ConfigureDelete();
            ServiceState state = DoCrud(block, entity, CrudAction.Delete, programMechanismId, true, userId);

            return state;
        }
        /// <summary>
        /// Saves summary statement setup information for a given mechanism
        /// </summary>
        /// <param name="programMechanismId">Identifier for a program mechanism</param>
        /// <param name="standardSummaryTemplateId">Identifier for a client summary template to associate with non expedited apps</param>
        /// <param name="expeditedSummaryTemplateId">Identifier for a client summary template to associate with expedited apps</param>
        /// <returns></returns>
        public virtual ServiceState SaveSummarySetup(int programMechanismId, int? standardSummaryTemplateId, int? expeditedSummaryTemplateId, int userId, List<SummaryStatementReviewerDescription> reviewerDescriptions)
        {
            //to preserve which template was applied when a ss was generated, we (soft) delete and re-add rather than modify
            //save deferred so all changes are committed or rolled back in a single transaction with the UnitOfWork.Save() call
            ProgramMechanism programMechanismEntity = UnitOfWork.ProgramMechanismRepository.GetByID(programMechanismId);
            RemoveSummaryTemplates(programMechanismEntity, userId);
            AddSummaryTemplates(programMechanismEntity, standardSummaryTemplateId, expeditedSummaryTemplateId, userId);
            ProcessDescriptionChanges(programMechanismEntity, reviewerDescriptions, userId);
            UpdateProgramMechanismLastUpdatedRecords(programMechanismEntity, userId);
            UnitOfWork.Save();
            return new ServiceState(true);
        }

        private void UpdateProgramMechanismLastUpdatedRecords(ProgramMechanism programMechanismEntity, int userId)
        {
            programMechanismEntity.PopulateSummarySetupInfo(userId, GlobalProperties.P2rmisDateTimeNow);
            UnitOfWork.ProgramMechanismRepository.Update(programMechanismEntity);
        }
        private void ProcessDescriptionChanges(ProgramMechanism programMechanismEntity, List<SummaryStatementReviewerDescription> reviewerDescriptions, int userId)
        {
            //first pull a list of existing reviewer descriptions for mech
            var existing = programMechanismEntity.SummaryReviewerDescriptions.ToList();
            //diff current vs incoming and add/update/delete as necessary
            foreach (var removedDesc in existing.Where(x => !reviewerDescriptions.Select(y => y.SummaryReviewerDescriptionId).Contains(x.SummaryReviewerDescriptionId)))
            {
                RemoveReviewerDescription(removedDesc, userId);
            }
            foreach (var addedDesc in reviewerDescriptions.Where(x => !existing.Select(y => y.SummaryReviewerDescriptionId).Contains(x.SummaryReviewerDescriptionId)))
            {
                AddReviewerDescription(programMechanismEntity, addedDesc.AssignmentOrder, addedDesc.CustomOrder, addedDesc.DisplayName, userId);
            }
            foreach (var modifiedDesc in existing.Where(x => reviewerDescriptions.Select(y => y.SummaryReviewerDescriptionId).Contains(x.SummaryReviewerDescriptionId)))
            {
                var newDesc = reviewerDescriptions.Where(x => x.SummaryReviewerDescriptionId == modifiedDesc.SummaryReviewerDescriptionId).FirstOrDefault();
                ModifyReviewerDescription(modifiedDesc, newDesc.AssignmentOrder, newDesc.CustomOrder, newDesc.DisplayName, userId);
            }
        }

        private void AddSummaryTemplates(ProgramMechanism programMechanismEntity, int? standardSummaryTemplateId, int? expeditedSummaryTemplateId, int userId)
        {

            if (standardSummaryTemplateId != null)
            {
                //template gets set up for each non-expedited review status
                foreach (int reviewStatus in ReviewStatu.NonExpeditedReviewStatuses)
                {
                    CreateProgramMechanismSummaryStatement(programMechanismEntity, reviewStatus, (int)standardSummaryTemplateId, userId);
                }

            }
            if (expeditedSummaryTemplateId != null)
            {
                CreateProgramMechanismSummaryStatement(programMechanismEntity, ReviewStatu.Triaged, (int)expeditedSummaryTemplateId, userId);
            }
        }

        private void RemoveSummaryTemplates(ProgramMechanism programMechanismEntity, int userId)
        {
            var templates = programMechanismEntity.ProgramMechanismSummaryStatements.ToList();
            foreach (var template in templates)
            {
                Helper.UpdateDeletedFields(template, userId);
                UnitOfWork.ProgramMechanismSummaryStatementRepository.Delete(template);
            }
        }

        /// <summary>
        /// Helper method to save any CRUD operations as a transaction.
        /// </summary>
        /// <param name="state">ServiceState of a transaction</param>
        internal virtual void TransactionDelete(ServiceState state)
        {
            //
            // Want to do execute all CRUD operations as a transaction.  If all CRUD operations were successful, 
            // then save the transaction
            //
            if (state.IsSuccessful)
            {
                UnitOfWork.Save();
            }
        }
        /// <summary>
        /// ProgramMechanism can have child ProgramMechanism.  If it does we make a list
        /// of those entity identifiers here then delete the lot of them in one transaction.
        /// </summary>
        /// <param name="programMechanismId">Identifies the ProgramMechanism to be deleted</param>
        /// <returns>List of entity identifiers to delete</returns>
        internal virtual List<int> FindProgramMechanismsTodDelete(int programMechanismId)
        {
            List<int> result = new List<int>() { programMechanismId };

            ProgramMechanism entity = UnitOfWork.ProgramMechanismRepository.GetByID(programMechanismId);

            if (entity.MayHaveChildern())
            {
                result = UnitOfWork.ProgramMechanismRepository.Select()
                                                               //
                                                               // we select any ProgramMechanism that has as it' parent
                                                               // the ProgramMechanism we want to delete
                                                               //
                                                              .Where(x => x.ParentProgramMechanismId == programMechanismId)
                                                              //
                                                              // Then we just get their entity identifier & make a list of it
                                                              //
                                                              .Select(x => x.ProgramMechanismId).ToList();
                //
                // And we add the one we already have so we can work off a list
                //
                result.Add(programMechanismId);
            }
            return result;
        }
        /// <summary>
        /// Does all the heavy lifting to perform CRUD operations for ProgramMechanisms
        /// </summary>
        /// <param name="block">Parameter block containing values for operation</param>
        /// <param name="entity">ProgramMechanism under edit</param>
        /// <param name="operation">CRUDAction to perform</param>
        /// <param name="entityId">ProgramMechanism entity identifier of entity</param>
        /// <param name="doUpdate">Indicates if the operation should be saved when executed</param>
        /// <param name="userId">User entity identifier of the user adding the award</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        internal virtual ServiceState DoCrud(AwardSetupBlock block, ProgramMechanism entity, CrudAction operation, int entityId, bool doUpdate, int userId)
        {
            //
            // 1) Get the rules we need to apply
            //
            RuleEngine<ProgramMechanism> rules = RuleEngineConstructors.CreateAwardEngine(UnitOfWork, entity, operation, block);
            //
            // 2) Create the action & execute it
            //
            ProgramMechanismServiceAction action = new ProgramMechanismServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.ProgramMechanismRepository, doUpdate, entityId, userId);
            action.Populate(block);
            action.Rule(rules);
            action.Execute();
            action.PostProcess();
            //
            // We pull the status off of the rules engine even though the action has ran after.  If
            // there was a failure an exception would have been thrown ignoring us all.
            //
            return new ServiceState(!rules.IsBroken, rules.Messages, action.EntityInfo);
        }
    }
}
