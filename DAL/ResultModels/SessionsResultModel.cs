using System.Collections.Generic;
using Sra.P2rmis.WebModels.ApplicationScoring;

namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Model representing the results of a GetSessionsDetails() request
    /// from the SessionService
    /// </summary>
    public class SessionsResultModel
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public SessionsResultModel()
        {
            Sessions = new List<MeetingSessionModel>();
            Panels = new List<SessionPanel>();
            ApplicationCounts = new List<ApplicationCount>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Container holding the DAL Session representation results for the query.
        /// </summary>
        public IEnumerable<MeetingSessionModel> Sessions { get; internal set; }
        /// <summary>
        /// Container holding the DAL Panel representation results for the query.
        /// </summary>
        public IEnumerable<SessionPanel> Panels { get; internal set; }
        /// <summary>
        /// Container holding the counts of application by mechanisms
        /// </summary>
        public IEnumerable<ApplicationCount> ApplicationCounts { get; internal set; } 
        #endregion
    }
}
