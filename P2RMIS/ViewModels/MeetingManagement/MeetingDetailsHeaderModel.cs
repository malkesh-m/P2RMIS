using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.WebModels.MeetingManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sra.P2rmis.Web.ViewModels.MeetingManagement
{
    /// <summary>
    /// Derived from MeetingDetailsHeader
    /// </summary>
    /// <seealso cref="Sra.P2rmis.WebModels.MeetingManagement.MeetingDetailsHeader" />
    public class MeetingDetailsHeaderModel : MeetingDetailsHeader, IMeetingDetailsHeader
    {

        /// <summary>
        /// Gets the meeting dates.
        /// </summary>
        /// <value>
        /// The meeting dates.
        /// </value>
        public string MeetingDates => ViewHelpers.FormatDate(MeetingStart) + " - " + ViewHelpers.FormatDate(MeetingEnd);
        /// <summary>
        /// Gets the session dates.
        /// </summary>
        /// <value>
        /// The session dates.
        /// </value>
        public string SessionDates => FormatStartEndDates(SessionStart, SessionEnd);

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingDetailsHeaderModel"/> class, inherited from WebModel MeetingDetailsHeader.
        /// </summary>
        /// <param name="attendee">The attendee.</param>
        /// <param name="participantType">Type of the participant.</param>
        /// <param name="panel">The panel.</param>
        /// <param name="phone">The phone.</param>
        /// <param name="meeting">The meeting.</param>
        /// <param name="session">The session.</param>
        /// <param name="email">The email.</param>
        /// <param name="meetingDates">The meeting dates.</param>
        /// <param name="sessionDates">The session dates.</param>
        public MeetingDetailsHeaderModel(string attendee, string participantType, string panel, string phone, string meeting, string session, string email, DateTime? meetingStart, DateTime? meetingEnd, DateTime? sessionStart, DateTime? sessionEnd, bool? reviewerFlag)
            : base(attendee, participantType, panel, phone, meeting, session, email, meetingStart, meetingEnd, sessionStart, sessionEnd, reviewerFlag)
        {
            ParticipantType = (ReviewerFlag.Value) ? participantType : "N/A";
            Session = (ReviewerFlag.Value) ? session : "N/A";
            Panel = (ReviewerFlag.Value && !string.IsNullOrEmpty(panel)) ? panel : "N/A";
        }

        private string FormatStartEndDates(DateTime? start, DateTime? end)
        {
            return (ReviewerFlag.Value) ? ViewHelpers.FormatDate(start) + " - " + ViewHelpers.FormatDate(end) : "N/A";
        }
    }
}
