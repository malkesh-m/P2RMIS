using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.MeetingManagement
{
    public class MeetingDetailsHeader : IMeetingDetailsHeader
    {
        /// <summary>
        /// Gets or sets the attendee.
        /// </summary>
        /// <value>
        /// The attendee.
        /// </value>
        public string Attendee { get; set; }
        /// <summary>
        /// Gets or sets the type of the participant.
        /// </summary>
        /// <value>
        /// The type of the participant.
        /// </value>
        public string ParticipantType { get; set; }
        /// <summary>
        /// Gets or sets the panel.
        /// </summary>
        /// <value>
        /// The panel.
        /// </value>
        public string Panel { get; set; }
        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string Phone { get; set; }
        /// <summary>
        /// Gets or sets the meeting.
        /// </summary>
        /// <value>
        /// The meeting.
        /// </value>
        public string Meeting { get; set; }
        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        public string Session { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the meeting start.
        /// </summary>
        /// <value>
        /// The meeting start.
        /// </value>
        public DateTime? MeetingStart { get; set; }
        /// <summary>
        /// Gets or sets the meeting end.
        /// </summary>
        /// <value>
        /// The meeting end.
        /// </value>
        public DateTime? MeetingEnd { get; set; }

        /// <summary>
        /// Gets or sets the session start.
        /// </summary>
        /// <value>
        /// The session start.
        /// </value>
        public DateTime? SessionStart { get; set; }
        /// <summary>
        /// Gets or sets the session end.
        /// </summary>
        /// <value>
        /// The session end.
        /// </value>
        public DateTime? SessionEnd { get; set; }
        public bool? ReviewerFlag { get; set; }

        public MeetingDetailsHeader()
        {

        }

        public MeetingDetailsHeader(string attendee, string participantType, string panel, string phone, string meeting, string session, string email, DateTime? meetingStart, DateTime? meetingEnd, DateTime? sessionStart, DateTime? sessionEnd, bool? reviewerFlag)
        {
            Attendee = attendee;
            ParticipantType = participantType;
            Panel = panel;
            Phone = phone;
            Meeting = meeting;
            Session = session;
            Email = email;
            MeetingStart = meetingStart;
            MeetingEnd = meetingEnd;
            SessionStart = sessionStart;
            SessionEnd = sessionEnd;
            ReviewerFlag = reviewerFlag;
        }
    }
}
