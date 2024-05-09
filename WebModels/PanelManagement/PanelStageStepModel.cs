using System;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for panel stage step/phase
    /// </summary>
    public class PanelStageStepModel: IPanelStageStepModel
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor used to create one PanelStageStepModel from another.
        /// This constructor is used when creating a PanelStageStepModel in the
        /// BL that will be sent up to the PL
        /// </summary>
        /// <param name="model">PanelStageStepModel with data from the DL</param>
        public PanelStageStepModel(PanelStageStepModel model)
        {
            //
            // Identification information
            //
            this.StageStepId = model.StageStepId;
            this.StageTypeId = model.StageTypeId;
            this.StepOrder = model.StepOrder;
            this.StepName = model.StepName;
            this.PanelApplicationId = model.PanelApplicationId;
            //
            // Copy the counts
            //
            this.CritiqueAssignmentCount = model.CritiqueAssignmentCount;
            this.CritiqueAssignmentSubmittedCount = model.CritiqueAssignmentSubmittedCount;
            //
            // Copy the dates
            //
            this.StartDate = model.StartDate;
            this.EndDate = model.EndDate;
            this.ReOpenDate = model.ReOpenDate;
            this.ReCloseDate = model.ReCloseDate;
            //
            // Copy the MOD information
            //
            this.IsModPhase = model.IsModPhase;
            this.IsModActive = model.IsModActive;
            this.IsModReady = model.IsModReady;
            this.IsModClosed = model.IsModClosed;
            this.ApplicationStageStepId = model.ApplicationStageStepId;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public PanelStageStepModel() { }
        #endregion
        /// <summary>
        /// Panel stage step identifier
        /// </summary>
        public int StageStepId { get; set; }
        /// <summary>
        /// Stage type Id
        /// </summary>
        public int StageTypeId { get; set; }
        /// <summary>
        /// Panel stage step order
        /// </summary>
        public int StepOrder { get; set; }
        /// <summary>
        /// Start date
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// End date
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Re-open date
        /// </summary>
        public DateTime? ReOpenDate { get; set; }
        /// <summary>
        /// Re-close date
        /// </summary>
        public DateTime? ReCloseDate { get; set; }
        /// <summary>
        /// Count of critique assignments
        /// </summary>
        public int CritiqueAssignmentCount { get; set; }
        /// <summary>
        /// Count of submitted critique assignments
        /// </summary>
        public int CritiqueAssignmentSubmittedCount { get; set; }
        /// <summary>
        /// Stage step name
        /// </summary>
        public string StepName { get; set; }
        /// <summary>
        /// Count of the submitted applications (CritiqueAssignmentCount == CritiqueAssignmentSubmittedCount)
        /// </summary>
        public int ApplicationSubmittedCount { get; set; }
        /// <summary>
        /// Indicates if the phase is an MOD (moderated on-line discussion)
        /// </summary>
        public bool IsModPhase { get; set; }
        /// <summary>
        /// Indicates if the MOD can be started.
        /// </summary>
        public bool IsModReady { get; set; }
        /// <summary>
        /// Indicates if the MOD is active
        /// </summary>
        public bool IsModActive { get; set; }
        /// <summary>
        /// Indicates if the MOD is closed
        /// </summary>
        public bool IsModClosed { get; set; }
        /// <summary>
        /// The ApplicationStageStep entity identifier
        /// </summary>
        public int? ApplicationStageStepId { get; set; }

        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        public int PanelApplicationId { get; set; }
    }
}
