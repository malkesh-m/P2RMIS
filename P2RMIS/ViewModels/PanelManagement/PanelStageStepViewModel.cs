using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.ViewModels.PanelManagement
{
    /// <summary>
    /// Data model for panel stage step/phase
    /// </summary>
    public interface IPanelStageStepViewModel
    {
        /// <summary>
        /// Panel stage step identifier
        /// </summary>
        int StageStepId { get; set; }
        /// <summary>
        /// Stage type Id
        /// </summary>
        int StageTypeId { get; set; }
        /// <summary>
        /// Panel stage step order
        /// </summary>
        int StepOrder { get; set; }
        /// <summary>
        /// Start date
        /// </summary>
        DateTime? StartDate { get; set; }
        /// <summary>
        /// End date
        /// </summary>
        DateTime? EndDate { get; set; }
        /// <summary>
        /// Re-open date
        /// </summary>
        DateTime? ReOpenDate { get; set; }
        /// <summary>
        /// Re-close date
        /// </summary>
        DateTime? ReCloseDate { get; set; }
        /// <summary>
        /// Phase status
        /// </summary>
        string PhaseStatus { get; set; }
        /// <summary>
        /// Phase status key
        /// 0: not started; 1: in progress; 2: re-opened; 3: closed
        /// </summary>
        int PhaseStatusKey { get; set; }
        /// <summary>
        /// Count of critique assignments
        /// </summary>
        int CritiqueAssignmentCount { get; set; }
        /// <summary>
        /// Count of submitted critique assignments
        /// </summary>
        int CritiqueAssignmentSubmittedCount { get; set; }
        /// <summary>
        /// Stage step name
        /// </summary>
        string StepName { get; set; }
        /// <summary>
        /// Count of the submitted applications (CritiqueAssignmentCount == CritiqueAssignmentSubmittedCount)
        /// </summary>
        int ApplicationSubmittedCount { get; set; }
        /// <summary>
        /// Indicates if the phase is an MOD (moderated on-line discussion)
        /// </summary>
        bool IsModPhase { get; set; }
        /// <summary>
        /// Indicates if the MOD can be started.
        /// </summary>
        bool IsModReady { get; set; }
        /// <summary>
        /// Indicates if the MOD is active
        /// </summary>
        bool IsModActive { get; set; }
        /// <summary>
        /// Indicates if the MOD is closed
        /// </summary>
        bool IsModClosed { get; set; }
        /// <summary>
        /// The ApplicationStageStep entity identifier
        /// </summary>
        int? ApplicationStageStepId { get; set; }
    }

    /// <summary>
    /// Data model for panel stage step/phase
    /// </summary>
    public class PanelStageStepViewModel : IPanelStageStepViewModel
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor used to create one PanelStageStepModel from another.
        /// This constructor is used when creating a PanelStageStepModel in the
        /// BL that will be sent up to the PL
        /// </summary>
        /// <param name="model">PanelStageStepModel with data from the DL</param>
        public PanelStageStepViewModel(PanelStageStepModel model)
        {
            //
            // Identification information
            //
            this.StageStepId = model.StageStepId;
            this.StageTypeId = model.StageTypeId;
            this.StepOrder = model.StepOrder;
            this.StepName = model.StepName;
            //
            // Copy the counts
            //
            this.CritiqueAssignmentCount = model.CritiqueAssignmentCount;
            this.CritiqueAssignmentSubmittedCount = model.CritiqueAssignmentSubmittedCount;
            this.ApplicationSubmittedCount = model.ApplicationSubmittedCount;
            //
            // Copy the dates
            //
            this.StartDate = model.StartDate;
            this.EndDate = model.EndDate;
            this.ReOpenDate = model.ReOpenDate;
            this.ReCloseDate = model.ReCloseDate;
            DateTime easternTime = GlobalProperties.P2rmisDateTimeNow;
            if (model.StartDate > easternTime)
            {
                this.PhaseStatus = "Not Started";
                this.PhaseStatusKey = 0;
            }
            else if (model.StartDate <= easternTime && easternTime <= model.EndDate)
            {
                this.PhaseStatus = "In Progress";
                this.PhaseStatusKey = 1;
            }
            else if (model.ReOpenDate <= easternTime && easternTime <= model.ReCloseDate)
            {
                this.PhaseStatus = "Re-opened";
                this.PhaseStatusKey = 2;
            }
            else
            {
                this.PhaseStatus = "Closed";
                this.PhaseStatusKey = 3;
            }
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
        public PanelStageStepViewModel() { }
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
        /// Phase status
        /// </summary>
        public string PhaseStatus { get; set; }
        /// <summary>
        /// Phase status key
        /// 0: not started; 1: in progress; 2: re-opened; 3: closed
        /// </summary>
        public int PhaseStatusKey { get; set; }
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
    }
}