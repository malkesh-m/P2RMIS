using System;

namespace Sra.P2rmis.WebModels.MeetingManagement
{
    /// <summary>
    /// Interface for MeetingDetailsHeader
    /// </summary>
    public interface IMeetingDetailsHeader
    {
        /// <summary>
        /// Gets or sets the attendee.
        /// </summary>
        /// <value>
        /// The attendee.
        /// </value>
        string Attendee { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        string Email { get; set; }
        /// <summary>
        /// Gets or sets the meeting.
        /// </summary>
        /// <value>
        /// The meeting.
        /// </value>
        string Meeting { get; set; }

        /// <summary>
        /// Gets or sets the meeting start.
        /// </summary>
        /// <value>
        /// The meeting start.
        /// </value>
        DateTime? MeetingStart { get; set; }
        /// <summary>
        /// Gets or sets the meeting end.
        /// </summary>
        /// <value>
        /// The meeting end.
        /// </value>
        DateTime? MeetingEnd { get; set; }
        /// <summary>
        /// Gets or sets the panel.
        /// </summary>
        /// <value>
        /// The panel.
        /// </value>
        string Panel { get; set; }
        /// <summary>
        /// Gets or sets the type of the participant.
        /// </summary>
        /// <value>
        /// The type of the participant.
        /// </value>
        string ParticipantType { get; set; }
        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        string Phone { get; set; }
        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        string Session { get; set; }

        /// <summary>
        /// Gets or sets the session start.
        /// </summary>
        /// <value>
        /// The session start.
        /// </value>
        DateTime? SessionStart { get; set; }
        DateTime? SessionEnd { get; set; }
        /// <summary>
        /// Gets or sets the reviewer flag.
        /// </summary>
        /// <value>
        /// The reviewer flag.
        /// </value>
        bool? ReviewerFlag { get; set; }
    }
}