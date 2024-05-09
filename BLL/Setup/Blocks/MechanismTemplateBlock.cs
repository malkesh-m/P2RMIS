using Sra.P2rmis.Dal;
using System;

namespace Sra.P2rmis.Bll.Setup.Blocks
{
    /// <summary>
    /// CrudBlock definition when adding a MechanismTemplate.  Primarily used when 
    /// adding a ProgramMechanism.
    /// </summary>
    internal class MechanismTemplateBlock : CrudBlock<MechanismTemplate>, ICrudBlock
    {
        #region Construction & Setup
        /// <summary>
        /// 
        /// </summary>
        /// <param name="programMechanismId"></param>
        /// <param name="mecchanismId"></param>
        /// <param name="reviewStatusId"></param>
        /// <param name="reviewStageId"></param>
        /// <param name="summaryDocumentId"></param>
        internal MechanismTemplateBlock(int programMechanismId, Nullable<int> mecchanismId, Nullable<int> reviewStatusId, int reviewStageId, Nullable<int> summaryDocumentId)
        {
            this.ProgramMechanismId = programMechanismId;
            this.MecchanismId = mecchanismId;
            this.ReviewStatusId = reviewStatusId;
            this.ReviewStageId = reviewStageId;
            this.SummaryDocumentId = summaryDocumentId;
        }
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureAdd()
        {
            IsAdd = true;
        }
        /// <summary>
        /// Configures block for an Modify operation
        /// </summary>
        internal void ConfigureModify()
        {
            IsModify = true;
        }
        /// <summary>
        /// Configures block for an Delete operation
        /// </summary>
        internal void ConfigureDelete()
        {
            IsDelete = true;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Parent ProgramMechanism entity
        /// </summary>
        public int ProgramMechanismId { get; private set; }
        /// <summary>
        /// MechanismId
        /// </summary>
        public Nullable<int> MecchanismId { get; private set; }
        /// <summary>
        /// ReviewStatus entity identifier
        /// </summary>
        public Nullable<int> ReviewStatusId { get; private set; }
        /// <summary>
        /// ReviewStage entity identifier
        /// </summary>
        public int ReviewStageId { get; private set; }
        /// <summary>
        /// SummaryDocument entity identifier
        /// </summary>
        public Nullable<int> SummaryDocumentId { get; private set; }
        #endregion
        #region Methods
        /// <summary>
        /// Sets individual properties on the CRUD entity.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="entity">MechanismTemplate to populate</param>
        internal override void Populate(int userId, MechanismTemplate entity)
        {
            //
            // No sense if we are doing a delete.
            //
            if (!IsDelete)
            {
                entity.ProgramMechanismId = this.ProgramMechanismId;
                entity.ReviewStatusId = this.ReviewStatusId;
                entity.ReviewStageId = this.ReviewStageId;
                entity.SummaryDocumentId = this.SummaryDocumentId;
            }
        }
        /// <summary>
        /// Indicates if the block has data.
        /// </summary>
        /// <returns>True if the block contains data; false otherwise</returns>
        internal override bool HasData() { return (IsAdd || IsModify); }
        #endregion
    }
}
