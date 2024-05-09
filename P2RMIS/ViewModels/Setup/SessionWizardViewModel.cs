using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Setup;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    public class SessionWizardViewModel
    {
        public SessionWizardViewModel()
        {
            PreMeetingPhases = new List<ReviewPhaseViewModel>();
            MeetingPhases = new List<ReviewPhaseViewModel>();
        }

        public SessionWizardViewModel(IAddSessionModalModel session) : this()
        {
            PreMeetingPhases = session.PreMeetingPhases.ToList().ConvertAll(x => new ReviewPhaseViewModel(x));
            MeetingPhases = session.MeetingPhases.ToList().ConvertAll(x => new ReviewPhaseViewModel(x));
            ReviewPhases = PreMeetingPhases.Concat(MeetingPhases).ToList();
            MeetingStartDate = ViewHelpers.FormatDateTime2(session.MeetingStartDate);
            MeetingEndDate = ViewHelpers.FormatDateTime2(session.MeetingEndDate);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionWizardViewModel"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public SessionWizardViewModel(IModifySessionModalModel session) : this()
        {
            MeetingSessionId = session.MeetingSessionId;
            SessionAbbr = session.SessionAbbreviation;
            SessionName = session.SessionDescription;
            MeetingStartDate = ViewHelpers.FormatDateTime2(session.MeetingStartDate);
            MeetingEndDate = ViewHelpers.FormatDateTime2(session.MeetingEndDate);
            StartDate = ViewHelpers.FormatDateTime2(session.SessionStart);
            EndDate = ViewHelpers.FormatDateTime2(session.SessionEnd);
            MeetingType = session.MeetingTypeName;
            PreMeetingPhases = session.PreMeetingPhases.OrderBy(x => x.Value.StepTypeId).ToList().ConvertAll(x => new ReviewPhaseViewModel(x));
            MeetingPhases = session.MeetingPhases.ToList().ConvertAll(x => new ReviewPhaseViewModel(x));
            ReviewPhases = PreMeetingPhases.Concat(MeetingPhases).ToList();
            HasApplicationsBeenReleased = session.HasApplicationsBeenReleased;
            HasScoringBeenSetup = session.HasScoringBeenSetup;
        }
        /// <summary>
        /// Gets the meeting session identifier.
        /// </summary>
        /// <value>
        /// The meeting session identifier.
        /// </value>
        public int? MeetingSessionId { get; private set; }
        /// <summary>
        /// Gets the session abbr.
        /// </summary>
        /// <value>
        /// The session abbr.
        /// </value>
        public string SessionAbbr { get; private set; }

        /// <summary>
        /// Gets the name of the session.
        /// </summary>
        /// <value>
        /// The name of the session.
        /// </value>
        public string SessionName { get; private set; }
        /// <summary>
        /// Gets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public string StartDate { get; private set; }
        /// <summary>
        /// Gets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public string EndDate { get; private set; }
        /// <summary>
        /// Meeting start date.
        /// </summary>
        public string MeetingStartDate { get; set; }
        /// <summary>
        /// Meeting end date.
        /// </summary>
        public string MeetingEndDate { get; set; }
        /// <summary>
        /// Gets the type of the meeting.
        /// </summary>
        /// <value>
        /// The type of the meeting.
        /// </value>
        public string MeetingType { get; private set; }
        /// <summary>
        /// Gets the pre meeting phases.
        /// </summary>
        /// <value>
        /// The pre meeting phases.
        /// </value>
        public List<ReviewPhaseViewModel> PreMeetingPhases { get; private set; }
        /// <summary>
        /// Gets the meeting phases.
        /// </summary>
        /// <value>
        /// The meeting phases.
        /// </value>
        public List<ReviewPhaseViewModel> MeetingPhases { get; private set; }
        /// <summary>
        /// Review phases.
        /// </summary>
        public List<ReviewPhaseViewModel> ReviewPhases { get; private set; }
        /// <summary>
        /// Whether any application has been released.
        /// </summary>
        public bool HasApplicationsBeenReleased { get; set; }
        /// <summary>
        /// Whether the scoring has been set up.
        /// </summary>
        public bool HasScoringBeenSetup { get; set; }
    }
}