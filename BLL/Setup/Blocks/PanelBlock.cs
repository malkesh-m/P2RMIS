using Sra.P2rmis.Dal;
using System;
using System.Linq;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Bll.Setup.Blocks
{
    /// <summary>
    /// Crud block to use for SessionPanel setup.
    /// </summary>
    internal class PanelBlock : CrudBlock<SessionPanel>, ICrudBlock
    {
        #region Construction & Setup
        public PanelBlock(int meetingSessionId, string panelAbbreviation, string panelName)
        {
            this.MeetingSessionId = meetingSessionId;
            this.PanelAbbreviation = panelAbbreviation;
            this.PanelName = panelName;
        }
        public PanelBlock(int sessionPanelId, int meetingSessionId, string panelAbbreviation, string panelName)
        {
            this.PanelAbbreviation = panelAbbreviation;
            this.PanelName = panelName;
            this.SessionPanelId = sessionPanelId;
            this.MeetingSessionId = meetingSessionId;
        }
        public PanelBlock(int sessionPanelId, int newMeetingSessionId)
        {
            this.SessionPanelId = sessionPanelId;
            this.MeetingSessionId = newMeetingSessionId;
        }
        public PanelBlock(int sessionPanelId)
        {
            this.SessionPanelId = sessionPanelId;
        }
        #endregion        
        /// <summary>
        /// Gets the session panel identifier.
        /// </summary>
        /// <value>
        /// The session panel identifier.
        /// </value>
        public int? SessionPanelId { get; private set; }
        /// <summary>
        /// Gets the meeting session identifier.
        /// </summary>
        /// <value>
        /// The meeting session identifier.
        /// </value>
        public int MeetingSessionId { get; private set; }
        #region Attributes
        /// <summary>
        /// Panel abbreviation
        /// </summary>
        public string PanelAbbreviation { get; private set; }
        /// <summary>
        /// Panel name
        /// </summary>
        public string PanelName { get; private set; }
        /// <summary>
        /// Meeting start date & time
        /// </summary>
        public DateTime? StartDate { get; private set; }
        /// <summary>
        /// Meeting end date & time
        /// </summary>
        public DateTime? EndDate { get; private set; }

        public int ProgramYearId { get; set; }

        public List<PanelStage> StageCollection { get; set; } = new List<PanelStage>();

        public List<PanelStageStep> StageStepCollection { get; set; } = new List<PanelStageStep>();
        #endregion
        #region Methods
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureAdd()
        {
            IsAdd = true;
        }
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureModify()
        {
            IsModify = true;
        }
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureDelete()
        {
            IsDelete = true;
        }
        /// <summary>
        /// Sets the program year identifier.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        internal void SetProgramYearId(int programYearId)
        {
            ProgramYearId = programYearId;
        }
        /// <summary>
        /// Set dates.
        /// </summary>
        /// <param name="startDate">Start date.</param>
        /// <param name="endDate">End date.</param>
        internal void SetDates(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
        /// <summary>
        /// Sets the panel stages.
        /// </summary>
        /// <param name="preMeetingPhases">The pre meeting phases.</param>
        /// <param name="preMeetingWorkflowId">Premeeting workflow identifier</param>
        /// <param name="meetingPhases">The meeting phases.</param>
        /// <param name="meetingWorkflowId">Meeting workflow identifier</param>
        internal void SetPanelStages(List<IGenericListEntry<Nullable<int>, IPhaseModel>> preMeetingPhases, int preMeetingWorkflowId,
            List<IGenericListEntry<Nullable<int>, IPhaseModel>> meetingPhases, int? meetingWorkflowId)
        {
            if (preMeetingPhases.Count > 0)
            {
                var stage = new PanelStage();
                stage.ReviewStageId = preMeetingPhases[0].Value.ReviewStageId;
                stage.StageOrder = preMeetingPhases[0].Value.ReviewStageId;
                stage.WorkflowId = preMeetingWorkflowId;
                Helper.UpdateCreatedFields(stage, UserId);
                Helper.UpdateModifiedFields(stage, UserId);
                for (var i = 0; i < preMeetingPhases.Count; i++)
                {
                    var phase = preMeetingPhases[i];
                    var step = new PanelStageStep();
                    step.StepTypeId = phase.Value.StepTypeId;
                    step.StepName = phase.Value.ReviewPhase;
                    step.StepOrder = i + 1;
                    step.StartDate = phase.Value.StartDate;
                    step.EndDate = phase.Value.EndDate;
                    step.ReOpenDate = phase.Value.ReopenDate;
                    step.ReCloseDate = phase.Value.CloseDate;
                    Helper.UpdateCreatedFields(step, UserId);
                    Helper.UpdateModifiedFields(step, UserId);
                    stage.PanelStageSteps.Add(step);
                }
                AddPanelStage(stage);
            }
            if (meetingPhases.Count > 0)
            {
                var stage = new PanelStage();
                stage.ReviewStageId = meetingPhases[0].Value.ReviewStageId;
                stage.StageOrder = meetingPhases[0].Value.ReviewStageId;
                stage.WorkflowId = meetingWorkflowId;
                Helper.UpdateCreatedFields(stage, UserId);
                Helper.UpdateModifiedFields(stage, UserId);
                for (var i = 0; i < meetingPhases.Count; i++)
                {
                    var phase = meetingPhases[i];
                    var step = new PanelStageStep();
                    step.StepTypeId = phase.Value.StepTypeId;
                    step.StepName = phase.Value.ReviewPhase;
                    step.StepOrder = i + 1;
                    step.StartDate = phase.Value.StartDate;
                    step.EndDate = phase.Value.EndDate;
                    step.ReOpenDate = phase.Value.ReopenDate;
                    step.ReCloseDate = phase.Value.CloseDate;
                    Helper.UpdateCreatedFields(step, UserId);
                    Helper.UpdateModifiedFields(step, UserId);
                    stage.PanelStageSteps.Add(step);
                }
                AddPanelStage(stage);
            }
        }
        /// <summary>
        /// Adds the panel stage.
        /// </summary>
        /// <param name="stage">The stage.</param>
        internal void AddPanelStage(PanelStage stage)
        {
            this.StageCollection.Add(stage);
        }
        /// <summary>
        /// Sets individual properties on the CRUD entity.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="entity">SessionPanel entity to populate</param>
        internal override void Populate(int userId, SessionPanel entity)
        {
            entity.MeetingSessionId = this.MeetingSessionId;
            entity.PanelAbbreviation = this.PanelAbbreviation;
            entity.PanelName = this.PanelName;
            entity.StartDate = this.StartDate;
            entity.EndDate = this.EndDate;
            
            if (SessionPanelId == null)
            {
                // ProgramPanel
                var pp = new ProgramPanel();
                pp.ProgramYearId = this.ProgramYearId;
                entity.ProgramPanels.Add(pp);
            }
            // Add panel or move panel to another session
            if (StageCollection.Count > 0)
            {
                // Reset workflowId to be deleted later
                entity.PanelStages.ToList().ForEach(x =>
                {
                    x.WorkflowId = null;
                });
                // Add panel stages
                StageCollection.ForEach(x => {
                    entity.PanelStages.Add(x);
                });
            }
        }
        #endregion
    }
}
