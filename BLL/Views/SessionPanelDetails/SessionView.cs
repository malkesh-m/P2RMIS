using System;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.WebModels.ApplicationScoring;

namespace Sra.P2rmis.Bll.Views
{
    /// <summary>
    /// Business layer representation of a Session.
    /// </summary>
    public class SessionView
    {
        #region Constants
        /// <summary>
        /// Class constants
        /// </summary>
        private class Constants
        {
            /// <summary>
            /// Default date for Open/Close date.  Database enforces non-null dates
            /// but in case of an error the default date is January 1, 1900
            /// </summary>
            public static DateTime DefaultDate = new DateTime(1900, 1, 1);
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private SessionView() { }
        /// <summary>
        /// Constructor.  Instantiates a business layer Session from a data layer Session.
        /// </summary>
        /// <param name="programSession">The MeetingSessionModel</param>
        public SessionView(MeetingSessionModel programSession)
        {
            ///
            /// Initialize the variables from the DL object
            ///
            ProgramId = programSession.ProgramYearId;
            SessionId = programSession.MeetingSessionId;
            OpenDate = programSession.StartDate.GetValueOrDefault(Constants.DefaultDate);
            CloseDate = programSession.EndDate.GetValueOrDefault(Constants.DefaultDate);
            SessionName = programSession.SessionDescription;
        }

        #endregion
        #region Properties
        /// <summary>
        /// Program identifier of the program owning the session
        /// </summary>
        public int ProgramId { get; private set; }
        /// <summary>
        /// Session identifier.
        /// </summary>
        public int SessionId { get; private set; }
        /// <summary>
        /// Session open date.
        /// </summary>
        public DateTime OpenDate { get; private set; }
        /// <summary>
        /// Session close date.
        /// </summary>
        public DateTime CloseDate { get; private set; }
        /// <summary>
        /// Session Name
        /// </summary>
        public string SessionName { get; private set; }
         /// <summary>
        /// Indicates if the Session is open
        /// </summary>
        public bool IsSessionOpen { get { return ViewHelpers.IsOpen(OpenDate, CloseDate); } }
        /// <summary>
        /// Open and close dates
        /// </summary>
        public string DatePeriod
        {
            get
            {
                return ViewHelpers.FormatSessionDates(OpenDate, CloseDate);
            }
        }
        #endregion
    }
}
