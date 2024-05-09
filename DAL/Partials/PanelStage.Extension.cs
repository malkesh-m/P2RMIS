using Sra.P2rmis.CrossCuttingServices;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PanelStage object. 
    /// </summary>
    public partial class PanelStage : IStandardDateFields
    {
        /// <summary>
        /// Determines the current phase of this panel stage.
        /// </summary>
        /// <returns>Current Phase indicator</returns>
        /// <remarks>
        ///      This assumes that the calling sequence started with PanelApplication.CurrentPhase();
        ///      Unit tests not written
        /// </remarks>
        public PanelStageStep CurrentPhase()
        {
            PanelStageStep result = this.PanelStageSteps.FirstOrDefault(x => x.StartDate <= GlobalProperties.P2rmisDateTimeNow && GlobalProperties.P2rmisDateTimeNow <= x.EndDate);
            //
            // if there was no match on an open stage, we get the latest phase that has passed. 
            //
            if (result == null)
            {
                //
                // if no phase is going on, we look for the most recently ended step (in case all are done),
                // if no steps have ended we assume first step
                //
                result = this.PanelStageSteps.Any(x => x.EndDate <= GlobalProperties.P2rmisDateTimeNow)
                    ? this.PanelStageSteps.OrderByDescending(x => x.StepOrder)
                        .FirstOrDefault(x => x.EndDate <= GlobalProperties.P2rmisDateTimeNow)
                    : this.PanelStageSteps.OrderBy(x => x.StepOrder).FirstOrDefault();
            }
            return result;
        }
        /// <summary>
        /// Determines if the specified phase is open but not ReOpened.
        /// </summary>
        /// <param name="stepTypeId">StepType entity identifier</param>
        /// <returns>True if the PanelStageStep is open but not reopened; false otherwise</returns>
        public bool IsStageStepOpenButNotReopened(int stepTypeId)
        {
            PanelStageStep panelStageStepEntity = PanelStageSteps.FirstOrDefault(x => x.StepTypeId == stepTypeId);

            return (panelStageStepEntity != null) ? panelStageStepEntity.IsStageStepOpenButNotReopened(): false;
        }
    }
}
