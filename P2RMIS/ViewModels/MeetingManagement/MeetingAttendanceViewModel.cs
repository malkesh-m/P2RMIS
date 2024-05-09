using System;
using Sra.P2rmis.CrossCuttingServices;
using Newtonsoft.Json;

namespace Sra.P2rmis.Web.ViewModels.MeetingManagement
{
    public class MeetingAttendanceViewModel
    {
        public const string Partial = "Partial";
        public const string Full = "Full";

        /// <summary>
        /// Constructor for all meeting attendees.
        /// </summary>
        /// <param name="reviewerUserId">The reviewer user identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="restrictedAssignedFlag">if set to <c>true</c> [restricted assigned flag].</param>
        /// <param name="participationMethod">The participation method.</param>
        /// <param name="program">The program.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="panelAbbreviation">The panel abbreviation.</param>
        /// <param name="meetingAbbreviation">The meeting abbreviation.</param>
        /// <param name="sessionName">Name of the session.</param>
        /// <param name="hotelModifiedByUserId">The hotel modified by user identifier.</param>
        /// <param name="travelModifiedByUserId">The travel modified by user identifier.</param>
        /// <param name="internalComments">The internal comments.</param>
        /// <param name="meetingRegistrationId">The meeting registration identifier.</param>
        /// <param name="panelUserAssignmentId">The panel user assignment identifer.</param>
        /// <param name="participantType">The participant type name.</param>
        public MeetingAttendanceViewModel(int reviewerUserId, string firstName, string lastName, bool restrictedAssignedFlag, string participationMethod,
                string program, string fiscalYear, string panelAbbreviation, string meetingAbbreviation, string sessionName,
                int? hotelModifiedByUserId, int? travelModifiedByUserId, string internalComments, int? meetingRegistrationId, int? panelUserAssignmentId, int? sessionUserAssignmentId, string participantType)
        {
            ReviewerUserId = reviewerUserId;
            FirstName = firstName;
            LastName = lastName;
            ParticipationInfo = String.Format("{0}-{1}", participationMethod, restrictedAssignedFlag ? Partial : Full);
            Program = program;
            FiscalYear = fiscalYear;
            PanelAbbreviation = panelAbbreviation;
            MeetingAbbreviation = meetingAbbreviation;
            SessionName = sessionName;
            HotelModified = ViewHelpers.FormatBoolean(hotelModifiedByUserId.HasValue && hotelModifiedByUserId != reviewerUserId);
            TravelModified = ViewHelpers.FormatBoolean(travelModifiedByUserId.HasValue && travelModifiedByUserId != reviewerUserId);
            InternalComments = internalComments;
            MeetingRegistrationId = meetingRegistrationId;
            PanelUserAssignmentId = panelUserAssignmentId;
            SessionUserAssignmentId = sessionUserAssignmentId;
            ParticipantType = participantType;

        }

        /// <summary>
        /// Constructor for non-reviewer attendees.
        /// </summary>
        /// <param name="reviewerUserId">The reviewer user identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="program">The program.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="panelAbbreviation">The panel abbreviation.</param>
        /// <param name="meetingAbbreviation">The meeting abbreviation.</param>
        /// <param name="sessionName">Name of the session.</param>
        /// <param name="meetingRegistrationId">The meeting registration identifier.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="email">The email.</param>
        /// <param name="role">The role.</param>
        public MeetingAttendanceViewModel(int reviewerUserId, string firstName, string lastName,
        string program, string fiscalYear, string meetingAbbreviation, string sessionName,
        int? meetingRegistrationId, string organization, string email, string role)
        {
            ReviewerUserId = reviewerUserId;
            FirstName = firstName;
            LastName = lastName;
            FiscalYear = fiscalYear;
            MeetingAbbreviation = meetingAbbreviation;
            SessionName = sessionName;
            MeetingRegistrationId = meetingRegistrationId;
            Organization = organization;
            Email = email;
            Role = role;
        }

        /// <summary>
        /// Gets or sets the reviewer user identifier.
        /// </summary>
        /// <value>
        /// The reviewer user identifier.
        /// </value>
        public int ReviewerUserId { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the participation information.
        /// </summary>
        /// <value>
        /// The participation information.
        /// </value>
        [JsonProperty("partInfo")]
        public string ParticipationInfo { get; set; }
        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        [JsonProperty("program")]
        public string Program { get; set; }
        /// <summary>
        /// Gets or sets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        [JsonProperty("fiscalYear")]
        public string FiscalYear { get; set; }
        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        [JsonProperty("panel")]
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the meeting abbreviation.
        /// </summary>
        /// <value>
        /// The meeting abbreviation.
        /// </value>
        [JsonProperty("meeting")]
        public string MeetingAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the name of the session.
        /// </summary>
        /// <value>
        /// The name of the session.
        /// </value>
        [JsonProperty("session")]
        public string SessionName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [hotel modified].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hotel modified]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("hotelUpdated")]
        public string HotelModified { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [travel modified].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [travel modified]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("travelUpdated")]
        public string TravelModified { get; set; }
        /// <summary>
        /// Gets or sets the internal comments.
        /// </summary>
        /// <value>
        /// The internal comments.
        /// </value>
        [JsonProperty("comments")]
        public string InternalComments { get; set; }
        /// <summary>
        /// Gets or sets the meeting registration identifier.
        /// </summary>
        /// <value>
        /// The meeting registration identifier.
        /// </value>
        [JsonProperty("meetingRegistrationId")]
        public int? MeetingRegistrationId { get; set; }
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        [JsonProperty("panelUserAssignmentId")]
        public int? PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the session user assignment identifier.
        /// </summary>
        /// <value>
        /// The session user assignment identifier.
        /// </value>
        [JsonProperty("sessionUserAssignmentId")]
        public int? SessionUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        [JsonProperty("Organization")]
        public string Organization { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [JsonProperty("Email")]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        [JsonProperty("Role")]
        public string Role { get; set; }
        /// <summary>
        /// Name of reviewer's participation type.
        /// </summary>
        [JsonProperty("participantType")]
        public string ParticipantType { get; set; }
    }
}