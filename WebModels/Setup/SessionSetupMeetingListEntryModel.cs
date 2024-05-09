using System;

namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Data model returned for the Session setup meeting drop down
    /// </summary>
    public interface  ISessionSetupMeetingListEntryModel
    {
        string MeetingAbbreviation { get; set; }
        string MeetingDescription { get; set; }
        string MeetingTypeName { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        bool HasEndDatePassed { get; set; }
        int ClientMeetingId { get; set; }
        int ClientId { get; set; }
        /// <summary>
        /// Whether the meeting is associated with an active program.
        /// </summary>
        bool ActiveProgram { get; set; }
    }
    /// <summary>
    /// Data model returned for the Session setup meeting drop down
    /// </summary>
    public class SessionSetupMeetingListEntryModel: ISessionSetupMeetingListEntryModel
    {
        #region Atttributes
        /// <summary>
        /// Meeting abbreviation
        /// </summary>
        public string MeetingAbbreviation { get; set; }
        /// <summary>
        /// Meeting description
        /// </summary>
        public string MeetingDescription { get; set; }
        /// <summary>
        /// Meeting type
        /// </summary>
        public string MeetingTypeName { get; set; }
        /// <summary>
        /// Meeting start date
        /// </summary>
        public DateTime StartDate{ get; set; }
        /// <summary>
        /// Meeting end date
        /// </summary>
        public DateTime EndDate{ get; set; }
        /// <summary>
        /// Indicates if the meeting EndDate has passed.
        /// </summary>
        public bool HasEndDatePassed { get; set; }
        /// <summary>
        /// Whether the meeting is associated with an active program.
        /// </summary>
        public bool ActiveProgram { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// ClientMeeting entity identifier
        /// </summary>
        public int ClientMeetingId { get; set; }
        /// <summary>
        /// Client entity identifier
        /// </summary>
        public int ClientId { get; set; }
        #endregion
    }
}
