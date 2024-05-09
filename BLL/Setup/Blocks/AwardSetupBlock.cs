using Sra.P2rmis.Dal;
using System;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Setup.Blocks
{
    /// <summary>
    /// Crud block to use for ProgramMechanism (Award) setup.
    /// </summary>
    internal class AwardSetupBlock : CrudBlock<ProgramMechanism>, ICrudBlock
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor for Adding a new Award (ProgramMechanism)
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <param name="clientAwardTypeId">ClientAwardType entity identifier</param>
        /// <param name="receiptCycle">ReceiptCycle value</param>
        /// <param name="receiptDeadline">Receipt due deadline</param>
        /// <param name="blindedFlag">Blinded flag</param>
        /// <param name="fundingOpportunityId">Funding opportunity value</param>
        /// <param name="partneringPiAllowedFlag">Indicates if PI partnering is allowed</param>
        internal AwardSetupBlock(int programYearId, int clientAwardTypeId, int receiptCycle, Nullable<DateTime> receiptDeadline,
                                 bool blindedFlag, string fundingOpportunityId, bool partneringPiAllowedFlag)
        {
            this.ProgramYearId = programYearId;
            this.ClientAwardTypeId = clientAwardTypeId;
            this.ReceiptCycle = receiptCycle;
            this.ReceiptDeadline = receiptDeadline;
            this.BlindedFlag = blindedFlag;
            this.FundingOpportunityId = fundingOpportunityId;
            this.PartneringPiAllowedFlag = partneringPiAllowedFlag;
        }
        /// <summary>
        /// Constructor for Modifying an existing Award (ProgramMechanism)
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <param name="clientAwardTypeId">ClientAwardType entity identifier</param>
        /// <param name="receiptCycle">ReceiptCycle value</param>
        /// <param name="receiptDeadline">Receipt due deadline</param>
        /// <param name="blindedFlag">Blinded flag</param>
        /// <param name="fundingOpportunityId">Funding opportunity value</param>
        /// <param name="partneringPiAllowedFlag">Indicates if PI partnering is allowed</param>
        /// <param name="programMechanismId">ProgramMechanism entity identifier under edit</param>
        internal AwardSetupBlock(int programYearId, int clientAwardTypeId, int receiptCycle, Nullable<DateTime> receiptDeadline,
                         bool blindedFlag, string fundingOpportunityId, bool partneringPiAllowedFlag, int programMechanismId)
                       : this(programYearId, clientAwardTypeId, receiptCycle, receiptDeadline, blindedFlag, fundingOpportunityId, partneringPiAllowedFlag)
        {
            this.ProgramMechanismId = programMechanismId;
        }
        /// <summary>
        /// Constructor for Deleting an existing Award (ProgramMechanism)
        /// </summary>
        /// <param name="programMechanismId">ProgramMechanism entity identifier under edit</param>
        internal AwardSetupBlock(int programMechanismId)
        {
            this.ProgramMechanismId = programMechanismId;
        }
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureAdd()
        {
            IsAdd = true;
        }
        /// <summary>
        /// Configures block for an Add operation on a Pre-AppAward
        /// </summary>
        /// <param name="parentProgramMechanismId">ProgramMechanism entity identifier of parent ProgramMechanism</param>
        internal void ConfigureAdd(Nullable<int> parentProgramMechanismId)
        {
            ConfigureAdd();
            ConfigurePreApp(parentProgramMechanismId);
        }
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureModify()
        {
            IsModify = true;
        }
        /// <summary>
        /// Configures block for an Add operation on a Pre-AppAward
        /// </summary>
        /// <param name="parentProgramMechanismId">ProgramMechanism entity identifier of parent ProgramMechanism</param>
        internal void ConfigureModify(Nullable<int> parentProgramMechanismId)
        {
            ConfigureModify();
            ConfigurePreApp(parentProgramMechanismId);
        }
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureDelete()
        {
            IsDelete = true;
        }
        /// <summary>
        /// Sets appropriate properties when PreApp awards are Added or Modified.
        /// </summary>
        /// <param name="parentProgramMechanismId">ProgramMechanism entity identifier of parent ProgramMechanism</param>
        private void ConfigurePreApp(Nullable<int> parentProgramMechanismId)
        {
            this.ParentProgramMechanismId = parentProgramMechanismId;
            this.MechanismRelationshipTypeId = MechanismRelationshipType.Indexes.PreApplication;
        }
        /// <summary>
        /// Add a template to be added to the ProgramMechanism.
        /// </summary>
        /// <param name="template"></param>
        public void AddTemplate(MechanismTemplate template)
        {
            this.TemplateCollection.Add(template);
        }
        /// <summary>
        /// Set the PreAppAwardTypeEntity when a PreApp award is not selected or exists
        /// </summary>
        /// <param name="entity">ClientAwardType for PreApp ClientAwardType</param>
        internal void AddPreAppClientAwardType(ClientAwardType entity)
        {
            this.PreAppAwardTypeEntity = entity;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Only applicable if a PreApp Award is being created and the selected
        /// award type was not a PreApp award type or did not exist;
        /// </summary>
        internal ClientAwardType PreAppAwardTypeEntity { get; private set; }
        /// <summary>
        /// Identifier for the ProgramMechanism under CRUD
        /// </summary>
        internal int ProgramMechanismId { get; private set; }
        /// <summary>
        /// ProgramYear of the Mechanism
        /// </summary>
        internal int ProgramYearId { get; private set; }
        /// <summary>
        /// ClientAwardType of the Mechanism
        /// </summary>
        internal int ClientAwardTypeId { get; private set; }
        /// <summary>
        /// Receipt Cycle of the Mechanism
        /// </summary>
        internal int ReceiptCycle { get; private set; }
        /// <summary>
        /// Receipt Deadline of the Mechanism
        /// </summary>
        internal Nullable<DateTime> ReceiptDeadline { get; private set; }
        /// <summary>
        /// BlindedFlag of the Mechanism
        /// </summary>
        internal bool BlindedFlag { get; private set; }
        /// <summary>
        /// FundingOpportunityId of the Mechanism 
        /// </summary>
        internal string FundingOpportunityId { get; private set; }
        /// <summary>
        /// ParentProgramMechanismId of the Mechanism.  (Only applicable to Pre-App awards)
        /// </summary>
        internal Nullable<int> ParentProgramMechanismId { get; private set; }
        /// <summary>
        /// MechanismRelationshipTypeId of the Mechanism.  (Only applicable to Pre-App awards)
        /// </summary>
        internal Nullable<int> MechanismRelationshipTypeId { get; private set; }
        /// <summary>
        /// PartneringPiAllowedFlag of the Mechanism
        /// </summary>
        internal bool PartneringPiAllowedFlag { get; private set; }
        /// <summary>
        /// Collection of MechanismTemplates to add to the award.
        /// </summary>
        private List<MechanismTemplate> TemplateCollection { get; set; } = new List<MechanismTemplate>();
        #endregion
        #region Methods
        /// <summary>
        /// Determines if the block was constructed for a PreAppAward.
        /// </summary>
        /// <returns>True if the block was constructed for a PreApp award; false otherwise</returns>
        internal virtual bool IsPreAppAdd()
        {
            return MechanismRelationshipType.IsPreApp(this.MechanismRelationshipTypeId);
        }
        /// <summary>
        /// Sets individual properties on the CRUD entity.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="entity">ProgramYear to populate</param>
        internal override void Populate(int userId, ProgramMechanism entity)
        {
            if (!IsDelete)
            {
                entity.ProgramMechanismId = this.ProgramMechanismId;
                entity.ProgramYearId = this.ProgramYearId;
                if (this.PreAppAwardTypeEntity != null)
                {
                    entity.ClientAwardType = PreAppAwardTypeEntity;
                }
                else
                {
                    entity.ClientAwardTypeId = this.ClientAwardTypeId;
                }
                entity.ReceiptCycle = this.ReceiptCycle;
                entity.ReceiptDeadline = this.ReceiptDeadline;
                entity.BlindedFlag = this.BlindedFlag;
                entity.FundingOpportunityId = this.FundingOpportunityId;
                entity.PartneringPiAllowedFlag = this.PartneringPiAllowedFlag;
                //
                // If there are any new templates add them to the collection.
                // This should only apply to an Add operation but one never knows.
                //
                TemplateCollection.ForEach(x => entity.MechanismTemplates.Add(x));
                //
                // These two properties are only applicable to Pre-App awards
                //
                entity.ParentProgramMechanismId = this.ParentProgramMechanismId;
                entity.MechanismRelationshipTypeId = this.MechanismRelationshipTypeId;
            }
        }
        /// <summary>
        /// Indicates if the block has data.
        /// </summary>
        /// <returns>True if the block contains data; false otherwise</returns>
        internal override bool HasData() { return (IsAdd || IsModify); }
        /// <summary>
        /// Creates an AwardSetupBlock from an existing entity.
        /// </summary>
        /// <param name="entity">ProgramMechanism entity to initialize block from</param>
        /// <returns>AwardSetupBlock populated from entity</returns>
        internal static AwardSetupBlock Create(ProgramMechanism entity)
        {
            AwardSetupBlock result = new AwardSetupBlock(entity.ProgramYearId, entity.ClientAwardTypeId, entity.ReceiptCycle.Value, entity.ReceiptDeadline,
                                                        entity.BlindedFlag, entity.FundingOpportunityId, entity.PartneringPiAllowedFlag, entity.ProgramMechanismId);
            //
            // Then set these two fields which are pre-app specific
            //
            result.ParentProgramMechanismId = entity.ParentProgramMechanismId;
            result.MechanismRelationshipTypeId = entity.MechanismRelationshipTypeId;
            result.PartneringPiAllowedFlag = entity.PartneringPiAllowedFlag;
            return result;
        }
        /// <summary>
        /// Overrides the block's FundingOpportunityId and ClientAwardTypeId values.
        /// </summary>
        /// <param name="fundingOpportunityId">New Funding Opportunity id value</param>
        /// <param name="partneringPiAllowedFlag">Partnering PI allowed flag</param>
        internal void SetValues(string fundingOpportunityId, bool partneringPiAllowedFlag)
        {
            this.FundingOpportunityId = fundingOpportunityId;
            this.PartneringPiAllowedFlag = partneringPiAllowedFlag;
        }
        #endregion
    }
}
