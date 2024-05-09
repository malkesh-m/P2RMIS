using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's MeetingSession object. 
    /// </summary>
    public partial class MeetingSession : IStandardDateFields
    {
        /// <summary>
        /// Returns all the PanelStageSteps in this meeting session.
        /// </summary>
        /// <param name="stepTypeId">Identifies the particular stage the steps are requested for</param>
        /// <returns>Enumerable list of PanelStageSteps</returns>
        public IEnumerable<PanelStageStep> ListStageSteps(int stepTypeId)
        {
            var result = this.SessionPanels.SelectMany(t => t.PanelStages).SelectMany(u => u.PanelStageSteps).Where(p => p.StepTypeId == stepTypeId);

            return result;
        }
        /// <summary>
        /// Returns the Client entity identifier 
        /// </summary>
        /// <returns>Client entity identifier</returns>
        public int ClientId()
        {
            return this.ClientMeeting.ClientId;
        }
    }
}
