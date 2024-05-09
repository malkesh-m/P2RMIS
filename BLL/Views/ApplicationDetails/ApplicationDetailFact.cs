using System;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    public class ApplicationDetailFact : IApplicationDetailFact
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
        #region Constructors
        /// <summary>
        /// TODO: document me
        /// </summary>
        /// <param name="details">-----</param>
        internal ApplicationDetailFact(IApplicationDetail details)
        {
            this.ApplicationId = details.ApplicationId;
            this.PrincipalInvestigatorName = ViewHelpers.ConstructName(details.PiLastName, details.PiFirstName);
            this.PiOrgName = details.PiOrgName;
            this.ApplicationTitle = details.ApplicationTitle;
            this.AwardDesccription = details.AwardDescription;
            this.AwardShortDesc = details.AwardShortDesc;
            this.PanelId = details.PanelId;
            this.SessionStartDate = details.SessionStartDate.GetValueOrDefault(Constants.DefaultDate);
            this.SessionEndDate = details.SessionEndDate.GetValueOrDefault(Constants.DefaultDate);
            this.SessionClosedMessage = BuildSessionClosedMessage(details.SessionStartDate.GetValueOrDefault(Constants.DefaultDate), details.SessionEndDate.GetValueOrDefault(Constants.DefaultDate));
            this.ProgramId = details.ProgramId;
        }
        #endregion
        #region Properties
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string PrincipalInvestigatorName { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string ApplicationTitle { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string AwardDesccription { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string PiOrgName { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string AwardShortDesc { get; set; }
        /// <summary>
        /// Panel Unique Identifier
        /// </summary>
        public int PanelId { get; set; }
        /// <summary>
        /// Panels corresponding session start date
        /// </summary> 
        public DateTime SessionStartDate { get; set; }
        /// <summary>
        /// Panels corresponding session end date
        /// </summary> 
        public DateTime SessionEndDate { get; set; }
        /// <summary>
        /// Determines if panel is in open session or not
        /// </summary>
        public bool isSessionOpen { get { return ViewHelpers.IsOpen(SessionStartDate, SessionEndDate); } }
        /// <summary>
        /// String to display instead of comments if session is not open
        /// </summary>
        public String SessionClosedMessage { get; set; }
        /// <summary>
        /// Applications program Id
        /// </summary>
        public int ProgramId { get; set; }
        #endregion
        #region Helpers
        /// <summary>
        /// Builds message to display in place of add comment link
        /// </summary>
        /// <param name="SessionStart">-----</param>
        /// <param name="SessionEnd">-----</param>
        /// <returns>String</returns>
        private String BuildSessionClosedMessage(DateTime SessionStart, DateTime SessionEnd)
        {
            return "&nbsp;Adding notes available " + String.Format("{0:MM/dd/yyyy}", SessionStart) + " - " + String.Format("{0:MM/dd/yyyy}", SessionEnd);
        }
        #endregion
    }
}
