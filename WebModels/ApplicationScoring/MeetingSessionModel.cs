using System;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// The interface model object returned by the repository GetMeetingSessionsByProgramId
    /// </summary>
    public interface IMeetingSessionModel
    {
        /// <summary>
        /// The meeting seesion identifier
        /// </summary>
        int MeetingSessionId { get; set; }

        /// <summary>
        /// The client meeting identifier
        /// </summary>
        int ClientMeetingId { get; set; }

        /// <summary>
        /// The seesion abbreviation
        /// </summary>
        string SessionAbbreviation { get; set; }

        /// <summary>
        /// The session Description
        /// </summary>
        string SessionDescription { get; set; }

        /// <summary>
        /// The meeting session start date
        /// </summary>
        Nullable<System.DateTime> StartDate { get; set; }

        /// <summary>
        /// the meeting session end date
        /// </summary>
        Nullable<System.DateTime> EndDate { get; set; }

        /// <summary>
        /// the application program year identifier
        /// </summary>
        int ProgramYearId { get; set; }
    }

    /// <summary>
    /// The model object returned by the repository GetMeetingSessionsByProgramId
    /// </summary>
    public class MeetingSessionModel : IMeetingSessionModel
    {
        /// <summary>
        /// The meeting seesion identifier
        /// </summary>
        public int MeetingSessionId { get; set; }
        /// <summary>
        /// The client meeting identifier
        /// </summary>
        public int ClientMeetingId { get; set; }
        /// <summary>
        /// The seesion abbreviation
        /// </summary>
        public string SessionAbbreviation { get; set; }
        /// <summary>
        /// The session Description
        /// </summary>
        public string SessionDescription { get; set; }
        /// <summary>
        /// The meeting session start date
        /// </summary>
        public Nullable<System.DateTime> StartDate { get; set; }
        /// <summary>
        ///  the meeting session end date
        /// </summary>
        public Nullable<System.DateTime> EndDate { get; set; }
        /// <summary>
        /// the application program year identifier
        /// </summary>
        public int ProgramYearId { get; set; }
    }
}
