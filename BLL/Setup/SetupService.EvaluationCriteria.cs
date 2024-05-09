using Sra.P2rmis.Bll.ModelBuilders.Setup;
using Sra.P2rmis.Bll.Rules;
using Sra.P2rmis.Bll.Setup.Actions;
using Sra.P2rmis.Bll.Setup.Blocks;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.Setup
{
    /// <summary>
    /// Provides services for the Evaluations functions of System Setup.
    /// </summary>
    public partial interface ISetupService
    {
        /// <summary>
        /// Retrieves the evaluation criterion.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        Container<IEvaluationCriteriaModel> RetrieveEvaluationCriterion(int programMechanismId);
        /// <summary>
        /// Retrieves the evaluation criterion header.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        Container<IEvaluationCriteriaHeaderModel> RetrieveEvaluationCriterionHeader(int programMechanismId);
        /// <summary>
        /// Retrieves the evaluation criteria modal.
        /// </summary>
        /// <param name="mechanismTemplateElementId">The mechanism template element identifier.</param>
        /// <returns></returns>
        Container<IEvaluationCriteriaModalModel> RetrieveEvaluationCriteriaModal(int mechanismTemplateElementId);
        /// <summary>
        /// Retrieves the evaluation criteria addition model.
        /// </summary>
        /// <param name="mechanismTemplateId">The mechanism template identifier.</param>
        /// <returns></returns>
        Container<IEvaluationCriteriaAdditionModel> RetrieveEvaluationCriteriaAdditionModel(Nullable<int> mechanismTemplateId);
        /// <summary>
        /// Retrieves the preview evaluation criteria model.
        /// </summary>
        /// <param name="mechanismTemplateId">The mechanism template identifier.</param>
        /// <returns></returns>
        Container<IPreviewCriteriaLayoutModel> RetrievePreviewEvaluationCriteriaModel(int mechanismTemplateId);
        /// <summary>
        /// Retrieves the upload scoring template model.
        /// </summary>
        /// <param name="systemTemplateId">The system template identifier.</param>
        /// <returns></returns>
        Container<IUploadScoringTemplateModalModel> RetrieveUploadScoringTemplateModel(int systemTemplateId);
        /// <summary>
        /// Adds the evaluation criteria.
        /// </summary>
        /// <param name="clientElementId">The client element identifier.</param>
        /// <param name="mechanismTemplateId">The mechanism template identifier.</param>
        /// <param name="overallFlag">if set to <c>true</c> [overall flag].</param>
        /// <param name="scoreFlag">if set to <c>true</c> [score flag].</param>
        /// <param name="textFlag">if set to <c>true</c> [text flag].</param>
        /// <param name="recommendedWordCount">The recommended word count.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="summaryIncludeFlag">if set to <c>true</c> [summary include flag].</param>
        /// <param name="summarySortOrder">The summary sort order.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        ServiceState AddEvaluationCriteria(int clientElementId, int mechanismTemplateId, bool overallFlag, bool scoreFlag, bool textFlag, int? recommendedWordCount, int sortOrder, bool summaryIncludeFlag, Nullable<int> summarySortOrder, string instructionText, int userId);
        /// <summary>
        /// Modifies the evaluation criteria.
        /// </summary>
        /// <param name="clientElementId">The client element identifier.</param>
        /// <param name="mechanismTemplateElementId">The mechanism template element identifier.</param>
        /// <param name="overallFlag">if set to <c>true</c> [overall flag].</param>
        /// <param name="scoreFlag">if set to <c>true</c> [score flag].</param>
        /// <param name="textFlag">if set to <c>true</c> [text flag].</param>
        /// <param name="recommendedWordCount">The recommended word count.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="summaryIncludeFlag">if set to <c>true</c> [summary include flag].</param>
        /// <param name="summarySortOrder">The summary sort order.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="showAbbreviationOnScoreboard">if set to <c>true</c> [show abbreviation on scoreboard].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        ServiceState ModifyEvaluationCriteria(int clientElementId, int mechanismTemplateElementId, bool overallFlag, bool scoreFlag, bool textFlag, int? recommendedWordCount, int sortOrder, bool summaryIncludeFlag, Nullable<int> summarySortOrder, string instructionText, bool showAbbreviationOnScoreboard, int userId, bool? partEditFlag);
        /// <summary>
        /// Deletes the evaluation criteria.
        /// </summary>
        /// <param name="mechanismTemplateElementId">The mechanism template element identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        ServiceState DeleteEvaluationCriteria(int mechanismTemplateElementId, int userId);
        /// <summary>
        /// Adds the mechanism scoring template.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <param name="scoringTemplateId">The scoring template identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        void AddMechanismScoringTemplate(int programMechanismId, int scoringTemplateId, int userId);
        /// <summary>
        /// Deletes the mechanism scoring template.
        /// </summary>
        /// <param name="mechanismScoringTemplateId">The mechanism scoring template identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void DeleteMechanismScoringTemplate(int mechanismScoringTemplateId, int userId);
    }
    /// <summary>
    /// Provides services for the Evaluations functions of System Setup.
    /// </summary>
    public partial class SetupService
    {
        /// <summary>
        /// Retrieves a container to populate the Evaluation Criteria Grid
        /// </summary>
        /// <param name="programMechanismId">ProgramMechanism entity identifier</param>
        /// <returns>Container of IEvaluationCriteriaModel model for the given ProgramMechanism Id</returns>
        public virtual Container<IEvaluationCriteriaModel> RetrieveEvaluationCriterion(int programMechanismId)
        {
            ValidateInt(programMechanismId, FullName(nameof(SetupService), nameof(RetrieveEvaluationCriterion)), nameof(programMechanismId));

            EvaluationCriteriaModelBuilder builder = new EvaluationCriteriaModelBuilder(this.UnitOfWork, programMechanismId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container to populate the Evaluation Criteria view header area.
        /// </summary>
        /// <param name="programMechanismId">ProgramMechanism entity identifier</param>
        /// <returns>Container of IEvaluationCriteriaModel model for the given ProgramMechanism Id</returns>
        public virtual Container<IEvaluationCriteriaHeaderModel> RetrieveEvaluationCriterionHeader(int programMechanismId)
        {
            ValidateInt(programMechanismId, FullName(nameof(SetupService), nameof(RetrieveEvaluationCriterionHeader)), nameof(programMechanismId));

            EvaluationCriteriaHeaderModelBuilder builder = new EvaluationCriteriaHeaderModelBuilder(this.UnitOfWork, programMechanismId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container to populate the Evaluation Criteria Setup modal.
        /// </summary>
        /// <param name="mechanismTemplateElementId">MechanismTemplateElement entity identifier</param>
        /// <returns>Container of IEvaluationCriteriaModalModel model for the given MechanismTemplateElement</returns>
        public virtual Container<IEvaluationCriteriaModalModel> RetrieveEvaluationCriteriaModal(int mechanismTemplateElementId)
        {
            ValidateInt(mechanismTemplateElementId, FullName(nameof(SetupService), nameof(RetrieveEvaluationCriteriaModal)), nameof(mechanismTemplateElementId));

            EvaluationCriteriaModalModelBuilder builder = new EvaluationCriteriaModalModelBuilder(this.UnitOfWork, mechanismTemplateElementId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container containing the information required to manipulate the Evaluation Criteria modal
        /// when adding a new MechanismTemplateElement
        /// </summary>
        /// <param name="mechanismTemplateId">MechanismTemplate entity identifier</param>
        /// <returns>Container of IEvaluationCriteriaAdditionModel model for the given MechanismTemplate</returns>
        public virtual Container<IEvaluationCriteriaAdditionModel> RetrieveEvaluationCriteriaAdditionModel(Nullable<int> mechanismTemplateId)
        {
            EvaluationCriteriaAdditionModelBuilder builder = new EvaluationCriteriaAdditionModelBuilder(this.UnitOfWork, mechanismTemplateId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container containing the information required to populate the Preview Evaluation Criteria modal
        /// </summary>
        /// <param name="mechanismTemplateId">MechanismTemplate entity identifier</param>
        /// <returns>Container of IPreviewCriteriaLayoutModel model for the given MechanismTemplate</returns>
        public virtual Container<IPreviewCriteriaLayoutModel> RetrievePreviewEvaluationCriteriaModel(int mechanismTemplateId)
        {
            ValidateInt(mechanismTemplateId, FullName(nameof(SetupService), nameof(RetrievePreviewEvaluationCriteriaModel)), nameof(mechanismTemplateId));

            PreviewCriteriaLayoutModelBuilder builder = new PreviewCriteriaLayoutModelBuilder(this.UnitOfWork, mechanismTemplateId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container containing the information required to populate the Upload ScoringTemplate modal
        /// </summary>
        /// <param name="systemTemplateId">SystemTemplate entity identifier</param>
        /// <returns>Container of IUploadScoringTemplateModalModel model for the given SystemTemplateId</returns>
        public virtual Container<IUploadScoringTemplateModalModel> RetrieveUploadScoringTemplateModel(int systemTemplateId)
        {
            ValidateInt(systemTemplateId, FullName(nameof(SetupService), nameof(RetrieveUploadScoringTemplateModel)), nameof(systemTemplateId));

            UploadScoringTemplateModalModelBuilder builder = new UploadScoringTemplateModalModelBuilder(this.UnitOfWork, systemTemplateId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Adds a new criteria (MechanismTemplateElement) to all phases.
        /// </summary>
        /// <param name="clientElementId">Container element for the criteria</param>
        /// <param name="mechanismTemplateId">Container element for the criteria</param>
        /// <param name="overallFlag">Indicates if the criteria is an Overall criteria</param>
        /// <param name="scoreFlag">Indicates if the criterion is scored</param>
        /// <param name="textFlag">Indicates if the criterion has text</param>
        /// <param name="recommendedWordCount">Recommend word count for the criteria</param>
        /// <param name="sortOrder">Criteria sort order</param>
        /// <param name="summaryIncludeFlag">Indicates if the criterion should be included with the summary statement</param>
        /// <param name="summarySortOrder">The sort order in the summary statement</param>
        /// <param name="instructionText">Criteria instructions</param>
        /// <param name="userId">User (making the change) entity identifier</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        public virtual ServiceState AddEvaluationCriteria(int clientElementId, int mechanismTemplateId, bool overallFlag, bool scoreFlag, bool textFlag, int? recommendedWordCount, int sortOrder, bool summaryIncludeFlag, Nullable<int> summarySortOrder, string instructionText, int userId)
        {
            //
            // From the one mechanism template we need to add an evaluation criteria to all phases.  So
            // we just work back up the chain.
            //
            MechanismTemplate templateEntity = UnitOfWork.MechanismTemplateRepository.GetByID(mechanismTemplateId);
            List<int> list = templateEntity.ProgramMechanism.MechanismTemplates.Select(x => x.MechanismTemplateId).ToList();

            List<ServiceState> results = new List<ServiceState>();
            foreach (int index in list)
            {
                //
                // This is a bit of a kludge.  The list of MechanismTemplateElements created for the sort order does not contain
                // the CRUD'ed one.  However in this case there is none.  Since 0 is not a valid index we use that knowing that 
                // it does not exist.
                //
                int excludeThisOne = 0;
                MechanismTemplate phaseTemplateEntity = UnitOfWork.MechanismTemplateRepository.GetByID(index);
                ReorderSortOrder(null, sortOrder, phaseTemplateEntity, excludeThisOne, userId);
                ReorderSummarySortOrder(null, summarySortOrder, phaseTemplateEntity, excludeThisOne, userId);
                ServiceState result = AddMechanismTemplate(clientElementId, index, overallFlag, scoreFlag, textFlag, recommendedWordCount, sortOrder, summaryIncludeFlag, summarySortOrder, instructionText, userId);
                results.Add(result);
                //
                // If the addition was not successful then there is no need to make any further attempts
                //
                if (!result.IsSuccessful)
                {
                    break;
                }
            }
            //
            // Now combine the ServiceStates and save.  After the save
            // we need to post process the result to retrieve the newly created
            // MechanismTemplateElement entity identifier.
            //
            ServiceState theResult = ServiceState.Merge(results);
            SaveMechanismTemplates(theResult);
            PostProcessMechanismTemplateElementEntityInfo(theResult);
            return theResult;
        }
        /// <summary>
        /// When services are processed as a transaction, the EntityInfo object is not post processed on it's
        /// own because the information may not be there.  But on a case by case basis the EntityInfo will
        /// contain the entity added.  After the Save() it can be accessed for the data.
        /// </summary>
        /// <param name="result">ServiceState to post process</param>
        protected virtual void PostProcessMechanismTemplateElementEntityInfo(ServiceState result)
        {
            //
            // All we need to do is to iterate over the EntityInfo and
            // set their return values.  Once it is accessed, toast it.
            //
            foreach(MechanismTemplateElementEntityInfo block in result.EntityInfo)
            {
                MechanismTemplateElement entity = block.Entity as MechanismTemplateElement;
                block.EntityId = (entity != null)? entity.MechanismTemplateElementId : 0;
                block.Entity = null;
            }
        }
        /// <summary>
        /// Save the MechanismTemplates in a transaction.
        /// </summary>
        /// <param name="result">ServiceState describing the results of the transaction</param>
        protected virtual void SaveMechanismTemplates(ServiceState result)
        {
            if (result.IsSuccessful)
            {
                UnitOfWork.Save();
            }
        }
        /// <summary>
        /// Adds a new criteria (MechanismTemplateElement)
        /// </summary>
        /// <param name="clientElementId">Container element for the criteria</param>
        /// <param name="mechanismTemplateId">Container element for the criteria</param>
        /// <param name="overallFlag">Indicates if the criteria is an Overall criteria</param>
        /// <param name="scoreFlag">Indicates if the criterion is scored</param>
        /// <param name="textFlag">Indicates if the criterion has text</param>
        /// <param name="recommendedWordCount">Recommend word count for the criteria</param>
        /// <param name="sortOrder">Criteria sort order</param>
        /// <param name="summaryIncludeFlag">Indicates if the criterion should be included with the summary statement</param>
        /// <param name="summarySortOrder">The sort order in the summary statement</param>
        /// <param name="instructionText">Criteria instructions</param>
        /// <param name="userId">User (making the change) entity identifier</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        internal virtual ServiceState AddMechanismTemplate(int clientElementId, int mechanismTemplateId, bool overallFlag, bool scoreFlag, bool textFlag, int? recommendedWordCount, int sortOrder, bool summaryIncludeFlag, Nullable<int> summarySortOrder, string instructionText, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(AddEvaluationCriteria));
            ValidateInteger(clientElementId, name, nameof(clientElementId));
            ValidateInteger(mechanismTemplateId, name, nameof(mechanismTemplateId));
            ValidateInteger(sortOrder, name, nameof(sortOrder));
            ValidateInteger(userId, name, nameof(userId));
            //
            // 1) create the P'Block & populate
            //
            EvaluationCriteriaBlock block = new EvaluationCriteriaBlock(clientElementId, mechanismTemplateId, overallFlag, scoreFlag, textFlag, recommendedWordCount, sortOrder, summaryIncludeFlag, summarySortOrder, instructionText, false);
            block.ConfigureAdd();
            //
            // 2) Get the rules we need to apply
            //
            RuleEngine<MechanismTemplateElement> rules = RuleEngineConstructors.CreateMechanismTemplateElementEngine(UnitOfWork, null, CrudAction.Add, block);
            //
            // 3) Create the action & execute it
            //
            EvaluationCriteriaServiceAction action = new EvaluationCriteriaServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.MechanismTemplateElementRepository, ServiceAction<MechanismTemplateElement>.DoNotUpdate, 0, userId);
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
        /// <summary>
        /// Adds the mechanism scoring template.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <param name="scoringTemplateId">The scoring template identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public void AddMechanismScoringTemplate(int programMechanismId, int scoringTemplateId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(AddMechanismScoringTemplate));
            ValidateInteger(programMechanismId, name, nameof(programMechanismId));
            ValidateInteger(scoringTemplateId, name, nameof(scoringTemplateId));

            UnitOfWork.MechanismScoringTemplateRepository.Add(programMechanismId, scoringTemplateId, userId);
            var phases = UnitOfWork.ScoringTemplatePhaseRepository.GetByScoringTemplateId(scoringTemplateId);
            var elements = UnitOfWork.MechanismTemplateElementRepository.GetByProgramMechanismId(programMechanismId);
            foreach(var phase in phases)
            {
                foreach(var element in elements)
                {
                    if (element.ScoreFlag &&
                        phase.StepType.ReviewStageId == element.MechanismTemplate.ReviewStageId)
                    {
                        if (!element.OverallFlag)
                        {
                            UnitOfWork.MechanismTemplateElementScoringRepository.Add(element.MechanismTemplateElementId, phase.CriteriaClientScoringId, phase.StepTypeId, userId);
                        }
                        else
                        {
                            UnitOfWork.MechanismTemplateElementScoringRepository.Add(element.MechanismTemplateElementId, phase.OverallClientScoringId, phase.StepTypeId, userId);
                        }
                    }
                }

            }
            UnitOfWork.Save();
        }
        /// <summary>
        /// Deletes the mechanism scoring template.
        /// </summary>
        /// <param name="mechanismScoringTemplateId">The mechanism scoring template identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public void DeleteMechanismScoringTemplate(int mechanismScoringTemplateId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(DeleteMechanismScoringTemplate));
            ValidateInteger(mechanismScoringTemplateId, name, nameof(mechanismScoringTemplateId));

            var scoringTemplate = UnitOfWork.MechanismScoringTemplateRepository.GetByID(mechanismScoringTemplateId);
            UnitOfWork.MechanismTemplateElementScoringRepository.DeleteByProgramMechanismId(scoringTemplate.ProgramMechanismId, userId);
            UnitOfWork.MechanismScoringTemplateRepository.Delete(mechanismScoringTemplateId, userId);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Retrieve all MechanismTemplates for all phases related to a ProgramMechanism
        /// </summary>
        /// <param name="mechanismTemplateId">Container element for the criteria</param>
        /// <returns>List of all MechanismTemplateId for the ProgramMechanism</returns>
        protected virtual List<int> GetAllMechanismTemplateIds(int mechanismTemplateId)
        {
            //
            // From the one mechanism template we need to )add an evaluation criteria to all phases.  So
            // we just work back up the chain.
            //
            MechanismTemplate templateEntity = UnitOfWork.MechanismTemplateRepository.GetByID(mechanismTemplateId);
            List<int> list = templateEntity.ProgramMechanism.MechanismTemplates.Select(x => x.MechanismTemplateId).ToList();

            return list;
        }
        /// <summary>
        /// Retrieves the MechanismTemplate entity identifiers for the ProgramMechanism
        /// </summary>
        /// <param name="mechanismTemplateElementId">MechanismTemplateElement entity identifier</param>
        /// <returns>List of MechanismTemplates identifiers</returns>
        protected virtual List<int> GetAllMechanismTemplateIdsForMechanismTemplateElement(int mechanismTemplateElementId)
        {
            MechanismTemplateElement entity = UnitOfWork.MechanismTemplateElementRepository.GetByID(mechanismTemplateElementId);
            return GetAllMechanismTemplateIds(entity.MechanismTemplateId);
        }
        /// <summary>
        /// Retrieves the identifying characteristics for a specific MechanismTemplateElement.
        /// </summary>
        /// <param name="mechanismTemplateElementId">MechanismTemplateElement entity identifier</param>
        /// <returns>Identifying characteristics of MechanismTemplateElement</returns>
        protected virtual MatchSet PropertyMatchForCriteria(int mechanismTemplateElementId)
        {
            MechanismTemplateElement entity = UnitOfWork.MechanismTemplateElementRepository.GetByID(mechanismTemplateElementId);
            return entity.MatchProperty();
        }
        /// <summary>
        /// Locates the MechanismTemplateElement specified by the identifying criteria.
        /// </summary>
        /// <param name="matchSet">Identifying criteria (basically the ClientElementId</param>
        /// <param name="mechanismTemplateId">MechanismTemplate entity identifier containing the target MechanismTemplateElement</param>
        /// <returns>Matching MechanismTemplateElement</returns>
        protected virtual MechanismTemplateElement MatchCriteriaToPhase(MatchSet matchSet, int mechanismTemplateId)
        {
            MechanismTemplate entity = UnitOfWork.MechanismTemplateRepository.GetByID(mechanismTemplateId);
            return entity.Match(matchSet);
        }
        /// <summary>
        /// Modifies the existing criteria (MechanismTemplateElement) for all phases of a ProgramMechanism.
        /// </summary>
        /// <param name="clientElementId">Container element for the criteria</param>
        /// <param name="mechanismTemplateId">Container element for the criteria</param>
        /// <param name="overallFlag">Indicates if the criteria is an Overall criteria</param>
        /// <param name="scoreFlag">Indicates if the criterion is scored</param>
        /// <param name="textFlag">Indicates if the criterion has text</param>
        /// <param name="recommendedWordCount">Recommend word count for the criteria</param>
        /// <param name="sortOrder">Criteria sort order</param>
        /// <param name="summaryIncludeFlag">Indicates if the criterion should be included with the summary statement</param>
        /// <param name="summarySortOrder">The sort order in the summary statement</param>
        /// <param name="instructionText">Criteria instructions</param>
        /// <param name="userId">User (making the change) entity identifier</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        public virtual ServiceState ModifyEvaluationCriteria(int clientElementId, int mechanismTemplateElementId, bool overallFlag, bool scoreFlag, bool textFlag, int? recommendedWordCount, int sortOrder, bool summaryIncludeFlag, Nullable<int> summarySortOrder, 
                                                            string instructionText, bool showAbbreviationOnScoreboard, int userId, bool? partEditFlag)
        {
            string name = FullName(nameof(SetupService), nameof(ModifyEvaluationCriteria));
            ValidateInteger(clientElementId, name, nameof(clientElementId));
            ValidateInteger(mechanismTemplateElementId, name, nameof(mechanismTemplateElementId));
            ValidateInteger(sortOrder, name, nameof(sortOrder));
            ValidateInteger(userId, name, nameof(userId));

            List<int> list = GetAllMechanismTemplateIdsForMechanismTemplateElement(mechanismTemplateElementId);
            MatchSet matchSet = PropertyMatchForCriteria(mechanismTemplateElementId);

            List<ServiceState> results = new List<ServiceState>();
            foreach (int index in list)
            {
                MechanismTemplateElement entity = MatchCriteriaToPhase(matchSet, index);
                ReorderSortOrder(entity.SortOrder, sortOrder, entity.MechanismTemplate, entity.MechanismTemplateElementId, userId);
                ReorderSummarySortOrder(entity.SummarySortOrder, summarySortOrder, entity.MechanismTemplate, entity.MechanismTemplateElementId, userId);
                ServiceState result = ModifyMechanismTemplateElement(clientElementId, index, entity, overallFlag, scoreFlag, textFlag, recommendedWordCount, sortOrder, summaryIncludeFlag, summarySortOrder, instructionText, showAbbreviationOnScoreboard, userId, partEditFlag);
                results.Add(result);
                //
                // If the edit was not successful then there is no need to make any further attempts
                //
                if (!result.IsSuccessful)
                {
                    break;
                }
            }
            //
            // Now combine the ServiceStates and save.  After the save
            // we need to post process the result to retrieve the newly created
            // MechanismTemplateElement entity identifier.
            //
            ServiceState theResult = ServiceState.Merge(results);
            SaveMechanismTemplates(theResult);
            PostProcessMechanismTemplateElementEntityInfo(theResult);
            return theResult;
        }

        #region Sandbox
        /// <summary>
        /// Marching orders for recalculating MechanismTemplateElement' SortOrder property
        /// </summary>
        /// <param name="oldValue">Old value of property</param>
        /// <param name="newValue">New value of property</param>
        /// <param name="template">MechanismTemplate containing all MechanismTemplateElements</param>
        /// <param name="mechanismTemplateElementId">MechanismTemplateElement entity identifier of MechanismTemplateElement changed</param>
        internal void ReorderSortOrder(Nullable<int> oldValue, Nullable<int> newValue, MechanismTemplate template, int mechanismTemplateElementId, int userId)
        {
            Order direction = GizmoHelper.DirectionIs(oldValue, newValue);
            GizmoOrderCalculator<MechanismTemplateElement> calculator = GizmoHelper.CreateSortOrderCalculator(oldValue, newValue, direction, template, mechanismTemplateElementId, userId);
            calculator.ReOrder();
            calculator.Apply();
        }
        /// <summary>
        /// Marching orders for recalculating MechanismTemplateElement' SummarySortOrder property
        /// </summary>
        /// <param name="oldValue">Old value of property</param>
        /// <param name="newValue">New value of property</param>
        /// <param name="template">MechanismTemplate containing all MechanismTemplateElements</param>
        /// <param name="mechanismTemplateElementId">MechanismTemplateElement entity identifier of MechanismTemplateElement changed</param>
        internal void ReorderSummarySortOrder(Nullable<int> oldValue, Nullable<int> newValue, MechanismTemplate template, int mechanismTemplateElementId, int userId)
        {
            Order direction = GizmoHelper.DirectionIs(oldValue, newValue);
            GizmoOrderCalculator<MechanismTemplateElement> calculator = GizmoHelper.CreateSummarySortOrderCalculator(oldValue, newValue, direction, template, mechanismTemplateElementId, userId);
            calculator.ReOrder();
            calculator.Apply();
        }
        #endregion
        /// <summary>
        /// Modifies an single MechanismTemplateElement with changes identified.
        /// </summary>
        /// <param name="clientElementId">Container element for the criteria</param>
        /// <param name="mechanismTemplateId">Container element for the criteria</param>
        /// <param name="entity">MechanismTemplate entity container.</param>
        /// <param name="overallFlag">Indicates if the criteria is an Overall criteria</param>
        /// <param name="scoreFlag">Indicates if the criterion is scored</param>
        /// <param name="textFlag">Indicates if the criterion has text</param>
        /// <param name="recommendedWordCount">Recommend word count for the criteria</param>
        /// <param name="sortOrder">Criteria sort order</param>
        /// <param name="summaryIncludeFlag">Indicates if the criterion should be included with the summary statement</param>
        /// <param name="summarySortOrder">The sort order in the summary statement</param>
        /// <param name="instructionText">Criteria instructions</param>
        /// <param name="showAbbreviationOnScoreboard">Indicates if the abbreviation is shown on the scoreboard</param>
        /// <param name="userId">User (making the change) entity identifier</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        internal virtual ServiceState ModifyMechanismTemplateElement(int clientElementId, int mechanismTemplateId, MechanismTemplateElement entity, bool overallFlag, bool scoreFlag, bool textFlag, int? recommendedWordCount, int sortOrder, 
                                                                bool summaryIncludeFlag, Nullable<int> summarySortOrder, string instructionText, bool showAbbreviationOnScoreboard, int userId, bool? partEditFlag)
        {
            //
            // 1) create the P'Block & populate
            //
            EvaluationCriteriaBlock block = new EvaluationCriteriaBlock(clientElementId, mechanismTemplateId, entity.MechanismTemplateElementId, overallFlag, scoreFlag, textFlag, recommendedWordCount, sortOrder, summaryIncludeFlag, summarySortOrder, instructionText, showAbbreviationOnScoreboard);
            block.ConfigureModify();
            //
            // 2) Get the rules we need to apply
            //
            RuleEngine<MechanismTemplateElement> rules = new RuleEngine<MechanismTemplateElement>();
            // Check if partial edit. No need to apply the validation rules in this case.
            if (!Convert.ToBoolean(partEditFlag))
            {
               rules = RuleEngineConstructors.CreateMechanismTemplateElementEngine(UnitOfWork, entity, CrudAction.Modify, block);

            }
            //
            // 3) Create the action & execute it
            //
            EvaluationCriteriaServiceAction action = new EvaluationCriteriaServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.MechanismTemplateElementRepository, ServiceAction<MechanismTemplateElement>.DoNotUpdate, entity.MechanismTemplateElementId, userId);
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
        /// <summary>
        /// Deletes a criteria (MechanismTemplateElement) from all phases of the ProgramMechanism
        /// </summary>
        /// <param name="mechanismTemplateElementId">MechanismTemplateElement entity identifier for the entity to delete</param>
        /// <param name="userId">User (making the delete) entity identifier</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        public virtual ServiceState DeleteEvaluationCriteria(int mechanismTemplateElementId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(ModifyEvaluationCriteria));
            ValidateInteger(mechanismTemplateElementId, name, nameof(mechanismTemplateElementId));
            ValidateInteger(userId, name, nameof(userId));

            List<int> list = GetAllMechanismTemplateIdsForMechanismTemplateElement(mechanismTemplateElementId);
            MatchSet matchSet = PropertyMatchForCriteria(mechanismTemplateElementId);

            List<ServiceState> results = new List<ServiceState>();
            foreach (int index in list)
            {
                MechanismTemplateElement entity = MatchCriteriaToPhase(matchSet, index);

                ReorderSortOrder(entity.SortOrder, null, entity.MechanismTemplate, entity.MechanismTemplateElementId, userId);
                ReorderSummarySortOrder(entity.SummarySortOrder, null, entity.MechanismTemplate, entity.MechanismTemplateElementId, userId);

                ServiceState result = DeleteMechanismTemplateElement(entity, userId);
                results.Add(result);
                //
                // If the delete was not successful then there is no need to make any further attempts
                //
                if (!result.IsSuccessful)
                {
                    break;
                }
            }
            //
            // Now combine the ServiceStates and save.  
            //
            ServiceState theResult = ServiceState.Merge(results);
            SaveMechanismTemplates(theResult);
            return theResult;
        }
        /// <summary>
        /// Deletes a criteria (MechanismTemplateElement)
        /// </summary>
        /// <param name="entity">MechanismTemplateElement entity to delete</param>
        /// <param name="userId">User (making the delete) entity identifier</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        internal virtual ServiceState DeleteMechanismTemplateElement(MechanismTemplateElement entity, int userId)
        {
            //
            // 1) create the P'Block & populate
            //
            EvaluationCriteriaBlock block = new EvaluationCriteriaBlock(entity.MechanismTemplateElementId);
            block.ConfigureDelete();
            //
            // 2) Get the rules we need to apply
            //
            RuleEngine<MechanismTemplateElement> rules = RuleEngineConstructors.CreateMechanismTemplateElementEngine(UnitOfWork, entity, CrudAction.Delete, block);
            //
            // 3) Create the action & execute it
            //
            EvaluationCriteriaServiceAction action = new EvaluationCriteriaServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.MechanismTemplateElementRepository, ServiceAction<MechanismTemplateElement>.DoNotUpdate, entity.MechanismTemplateElementId, userId);
            action.Populate(block);
            action.Rule(rules);
            action.Execute();
            //
            // We pull the status off of the rules engine even though the action has ran after.  If
            // there was a failure an exception would have been thrown ignoring us all.
            //
            return new ServiceState(!rules.IsBroken, rules.Messages, action.EntityInfo);
        }
    }
}
