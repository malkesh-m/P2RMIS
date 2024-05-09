using System;
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.Setup
{
    #region SessionSetupModel
    /// <summary>
    /// Data model returned for the Session Setup grid
    /// </summary>
    public interface ISessionSetupModel
    {
        #region Attributes
        string SessionAbbreviation { get; set; }
        string SessionDescription { get; set; }
        Nullable<DateTime> StartDate { get; set; }
        Nullable<DateTime> EndDate { get; set; }
        bool PanelHasApplications { get; set; }
        bool ReviewersAssigned { get; set; }
        IEnumerable<ISessionPanelListModel> SessionPanelList { get; set; }
        #endregion
        #region Indexes
        int ClientId { get; set; }
        int ClientMeetingId { get; set; }
        int MeetingSessionId { get; set; }
        bool HasProgramMeetings { get; set; }
        #endregion
    }
    /// <summary>
    /// Data model returned for the Session Setup grid
    /// </summary>
    public class SessionSetupModel : ISessionSetupModel
    {
        #region Attributes
        /// <summary>
        /// Session abbreviation
        /// </summary>
        public string SessionAbbreviation { get; set; }
        /// <summary>
        /// Session name
        /// </summary>
        public string SessionDescription { get; set; }
        /// <summary>
        /// Session start date
        /// </summary>
        public Nullable<DateTime> StartDate { get; set; }
        /// <summary>
        /// Session end date
        /// </summary>
        public Nullable<DateTime> EndDate { get; set; }
        /// <summary>
        /// Indicates if the Panel has applications assigned
        /// </summary>
        public bool PanelHasApplications { get; set; }
        /// <summary>
        /// Indicates if the Panel has reviewers assigned
        /// </summary>
        public bool ReviewersAssigned { get; set; }
        /// <summary>
        /// List of the Session's panels
        /// </summary>
        public IEnumerable<ISessionPanelListModel> SessionPanelList { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// Client entity identifier
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// ClientMeeting entity identifier
        /// </summary>
        public int ClientMeetingId { get; set; }
        /// <summary>
        /// MeetingSession entity identifier
        /// </summary>
        public int MeetingSessionId { get; set; }

        public bool HasProgramMeetings { get; set; }
        #endregion
    }
    #endregion
    #region SessionPanelListModel
    /// <summary>
    /// Data model for the Session Setup grid's Panel list
    /// </summary>
    public interface ISessionPanelListModel
    {
        string ProgramAbbreviation { get; set; }
        string PanelAbbreviation { get; set; }
        int SessionPanelId { get; set; }
    }
    /// <summary>
    /// Data model for the Session Setup grid's Panel list
    /// </summary>
    public class SessionPanelListModel: ISessionPanelListModel
    {
        /// <summary>
        /// Program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// SessionPanel name
        /// </summary>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        public int SessionPanelId { get; set; }
    }
    #endregion
}
