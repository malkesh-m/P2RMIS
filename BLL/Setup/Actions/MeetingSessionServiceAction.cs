using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Setup;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Sra.P2rmis.Bll.Setup.Actions
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete MeetingSession.
    /// </summary>
    internal class MeetingSessionServiceAction : ServiceAction<MeetingSession>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public MeetingSessionServiceAction() { }
        #endregion
        #region Attributes
        /// <summary>
        /// Information about the entity manipulated by the CRUD operation
        /// </summary>
        public IEnumerable<IEntityInfo> EntityInfo { get; private set; } = new List<IEntityInfo>();
        /// <summary>
        /// This is the CRUD'ed MeetingSession
        /// </summary>
        protected MeetingSession CRUDedMeetingSession { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// We tell the service action how to populate the entity with the data.
        /// </summary>
        /// <param name="entity">MeetingSession entity being populated</param>
        protected override void Populate(MeetingSession entity)
        {
            this.Block.Populate(this.UserId, entity);
        }
        /// <summary>
        /// Optional post modify processing.  Add any additional processing necessary after the entity object
        /// is modified.
        /// </summary>
        /// <param name="entity"></param>
        protected override void PostModify(MeetingSession entity)
        {
            entity.SessionPhases.Where(x => x.StartDate == DateTime.MinValue && x.EndDate == DateTime.MinValue)
                .ToList().ForEach(y =>
                {
                    this.UnitOfWork.SetEntityDeleted<SessionPhase>(this.UserId, y);
                });
            entity.SessionPanels.Where(x => x.StartDate != entity.StartDate || x.EndDate != entity.EndDate).ToList().ForEach(y =>
                {
                    y.StartDate = entity.StartDate;
                    y.EndDate = entity.EndDate;
                    Helper.UpdateModifiedFields(y, UserId);
                });
            // Re-set workflowIds as needed
            var reviewStageIds = new List<int>();
            var validSessionPhases = entity.SessionPhases.Where(x => x.StartDate != DateTime.MinValue && x.EndDate != DateTime.MinValue);
            foreach(var sessionPhase in validSessionPhases)
            {
                if (sessionPhase.StepType == null)
                    sessionPhase.StepType = UnitOfWork.StepTypeRepository.GetByID(sessionPhase.StepTypeId);
                if (!reviewStageIds.Contains(sessionPhase.StepType.ReviewStageId)) 
                    reviewStageIds.Add(sessionPhase.StepType.ReviewStageId);
            }
            for (var i = 0; i < reviewStageIds.Count; i++)
            {
                var stepTypeIds = entity.SessionPhases.Where(x => x.StartDate != DateTime.MinValue && x.EndDate != DateTime.MinValue
                        && x.StepType.ReviewStageId == reviewStageIds[i]).Select(y => y.StepTypeId).ToList();
                var workflowId = UnitOfWork.WorkflowRepository.GetWorkflow(stepTypeIds, entity.ClientId()).WorkflowId;
                entity.SessionPanels.SelectMany(x => x.PanelStages.Where(y => y.ReviewStageId == reviewStageIds[i]))
                    .ToList().ForEach(y =>
                {
                    if (y.WorkflowId != workflowId)
                    {
                        y.WorkflowId = workflowId;
                        Helper.UpdateModifiedFields(y, UserId);
                        y.PanelStageSteps.ToList().ForEach(z =>
                            {
                                this.UnitOfWork.SetEntityDeleted<PanelStageStep>(this.UserId, z);
                            });
                        // Add panel stage steps
                        var stepOrder = 1;
                        foreach (var phase in validSessionPhases.Where(w => w.StepType.ReviewStageId == y.ReviewStageId))
                        {
                            var step = new PanelStageStep();
                            step.StepTypeId = phase.StepTypeId;
                            step.StepName = phase.StepType.StepTypeName;
                            step.StepOrder = stepOrder;
                            step.StartDate = phase.StartDate;
                            step.EndDate = phase.EndDate;
                            step.ReOpenDate = phase.ReopenDate;
                            step.ReCloseDate = phase.CloseDate;
                            Helper.UpdateCreatedFields(step, UserId);
                            Helper.UpdateModifiedFields(step, UserId);
                            y.PanelStageSteps.Add(step);
                            stepOrder++;
                        }
                    }
                    else
                    {
                        foreach (var phase in validSessionPhases.Where(w => w.StepType.ReviewStageId == y.ReviewStageId))
                        {
                            var step = y.PanelStageSteps.FirstOrDefault(w2 => w2.StepTypeId == phase.StepTypeId);
                            if (step != null && (step.StartDate != phase.StartDate || step.EndDate != phase.EndDate ||
                                step.ReOpenDate != phase.ReopenDate || step.ReCloseDate != phase.CloseDate))
                            {
                                step.StartDate = phase.StartDate;
                                step.EndDate = phase.EndDate;
                                step.ReOpenDate = phase.ReopenDate;
                                step.ReCloseDate = phase.CloseDate;
                                Helper.UpdateModifiedFields(step, UserId);
                            }
                        }
                    }
                });
            }
        }
        /// <summary>
        /// And we tell it how to determine if we have data
        /// </summary>
        protected override bool HasData { get { return this.Block.HasData(); } }
        #endregion
        #region Optional Overrides
        /// <summary>
        /// What happens after an add is done.
        /// </summary>
        protected override void PostAdd(MeetingSession entity)
        {
            //
            // And we remember the MeetingSession we just created.
            //
            this.CRUDedMeetingSession = entity;
        }
        protected override bool IsDelete { get { return this.Block.IsDelete; } }
        /// <summary>
        /// Define the post processing here.  By definition (when the comment was written)
        /// the PostProcess method should only be called when something was added.
        /// </summary>
        public override void PostProcess()
        {
            if ((!this.RuleMachine.IsBroken) && (this.IsAdd))
            {
                this.EntityInfo = new List<IEntityInfo>() { new MeetingSessionEntityInfo(this.CRUDedMeetingSession.MeetingSessionId) };
            }
        }
        #endregion
    }
}
