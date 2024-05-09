using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sra.P2rmis.Web.Common;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for viewing reviewers information
    /// </summary>
    public class ReviewersViewModel : PanelManagementViewModel
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ReviewersViewModel() : base()
        {
            AssignedStaffs = new List<string>();
            PanelReviewers = new List<PanelReviewerViewModel>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="assignedStaffs">The assigned staffs.</param>
        /// <param name="panelReviewers">The panel reviewers.</param>
        /// <param name="modifyParticipantPostAssignment">Indicates if the user has the permission to change participant information after assignment</param>
        /// <param name="modifyParticipantPostAssignmentLimited">Indicates if the user has limited permission to change participant information after assignment</param>
        public void SetData(List<IAssignedStaffModel> assignedStaffs, List<IPanelReviewerModel> panelReviewers, bool modifyParticipantPostAssignment, bool modifyParticipantPostAssignmentLimited, bool canProcessStaffs, bool canManageReviewers, bool canManageUserInfo, bool canEvaluateReviewers)
        {
            AssignedStaffs = assignedStaffs.ConvertAll(x => ViewHelpers.ConstructNameAndRole(x.FirstName, x.LastName, x.Role));
            PanelReviewers = panelReviewers.ConvertAll(x => new PanelReviewerViewModel(x, modifyParticipantPostAssignment, modifyParticipantPostAssignmentLimited, canProcessStaffs));
            CanProcessStaffs = canProcessStaffs;
            CanManagePanelReviewers = canManageReviewers;
            CanManageUserInfo = canManageUserInfo;
            CanEvaluateReviewers = canEvaluateReviewers;
        }
        #endregion

        #region Properties        
        /// <summary>
        /// Gets the names/roles of assigned staffs.
        /// </summary>
        /// <value>
        /// The names/roles of assigned staffs.
        /// </value>
        public List<string> AssignedStaffs { get; private set; }
        /// <summary>
        /// Gets the panel reviewers.
        /// </summary>
        /// <value>
        /// The panel reviewers.
        /// </value>
        public List<PanelReviewerViewModel> PanelReviewers { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance can manage staffs.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can manage staffs; otherwise, <c>false</c>.
        /// </value>
        public bool CanProcessStaffs { get; private set; }

        /// <summary>
        /// Gets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        public string FiscalYear
        {
            get { return this.Panels.FirstOrDefault(x => x.PanelId == this.SelectedPanel)?.FY; }
        }
        /// <summary>
        /// Gets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        public string ProgramAbbreviation
        {
            get { return this.Panels.FirstOrDefault(x => x.PanelId == this.SelectedPanel)?.ProgramAbbreviation; }
        }
        /// <summary>
        /// Gets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        public string PanelAbbreviation
        {
            get { return this.Panels.FirstOrDefault(x => x.PanelId == this.SelectedPanel)?.PanelAbbreviation; }
        }
        /// <summary>
        /// Whether the user can manage panel reviewers
        /// </summary>
        public bool CanManagePanelReviewers { get; private set; }

        /// <summary>
        /// Whether the user can manage user info
        /// </summary>
        public bool CanManageUserInfo { get; private set; }

        /// <summary>
        /// Whether the user can evaluate reviewers
        /// </summary>
        public bool CanEvaluateReviewers { get; private set; }
        /// <summary>
        /// Panel reviewer view model
        /// </summary>
        public class PanelReviewerViewModel
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="PanelReviewerViewModel"/> class.
            /// </summary>
            /// <param name="panelReviewer">The panel reviewer.</param>
            /// <param name="modifyParticipantPostAssignment">Indicates if the user has the permission to change participant information after assignment</param>
            /// <param name="modifyParticipantPostAssignmentLimited">Indicates if the user has limited permission to change participant information after assignment</param>
            /// <param name="canProcessStaffs">Indicates if the user has the permission to add, modify, or delete staffs</param>
            public PanelReviewerViewModel(IPanelReviewerModel panelReviewer, bool modifyParticipantPostAssignment, bool modifyParticipantPostAssignmentLimited, bool canProcessStaffs)
            {
                Name = ViewHelpers.ConstructNameWithSuffix(panelReviewer.FirstName, panelReviewer.LastName, panelReviewer.Suffix);
                DialogTitle = ViewHelpers.ConstructNameWithSpace(panelReviewer.FirstName, panelReviewer.LastName);
                Blocked = panelReviewer.IsBlocked;
                Rating = (panelReviewer.Rating != null)
                    ? Convert.ToString(ViewHelpers.P2rmisRound((decimal)panelReviewer.Rating))
                    : string.Empty;
                if (string.IsNullOrEmpty(panelReviewer.ParticipantMethod))
                {
                    ParticipantType = string.Format("{0}-{1}", panelReviewer.ParticipantType, panelReviewer.ParticipantLevel);
                }
                else
                {
                    ParticipantType = string.Format("{0}-{1}-{2}", panelReviewer.ParticipantType, panelReviewer.ParticipantMethod, panelReviewer.ParticipantLevel);
                }
                ParticipantRole = panelReviewer.ParticipantRole ?? string.Empty;
                ParticipantRoleAbbreviation = panelReviewer.ParticipantRoleAbbreviation ?? string.Empty;

                ParticipantTypeAndRoleAbbreviation = string.Format("{0}  {1}", ParticipantType, ParticipantRoleAbbreviation);
                Status = panelReviewer.TypeName == Routing.PanelManagement.AssignmentType.PanelUserAssignment ?
                    Routing.PanelManagement.AssignmentStatus.Assigned : Routing.PanelManagement.AssignmentStatus.Potential;
                Organization = panelReviewer.Organization;
                Degrees = (panelReviewer.Degrees != null) ? string.Join(",", panelReviewer.Degrees) : string.Empty;
                Expertise = ViewHelpers.EmptyIfNull(panelReviewer.Expertise);
                AcademicRank = panelReviewer.AcademicRank;
                MilitaryBranch = string.IsNullOrEmpty(panelReviewer.MilitaryBranch) ? string.Empty :
                    string.Format("{0}-{1}", panelReviewer.MilitaryBranch, panelReviewer.MilitaryStatus);
                MilitaryRank = panelReviewer.MilitaryRank;
                Ethnicity = panelReviewer.Ethnicity;
                Gender = panelReviewer.Gender;
                PotentialChair = panelReviewer.IsPotentialChair;
                PreviousProgramParticipation = panelReviewer.IsPreviouslyParticipated;
                ApprovedByClient = panelReviewer.IsClientApproved;
                PreferredWebsiteAddress = panelReviewer.PreferredWebsiteAddress;
                ShowPreferredLink = !string.IsNullOrWhiteSpace(panelReviewer.PreferredWebsiteAddress);
                ShowResumeLink = panelReviewer.UserResumeId != null;
                UserInfoId = panelReviewer.UserInfoId;
                UserId = panelReviewer.UserId;
                FirstName = panelReviewer.FirstName;
                LastName = panelReviewer.LastName;
                ResumeLink = panelReviewer.UserResumeId != null ? string.Format("/{0}/{1}?{2}={3}", Routing.P2rmisControllers.UserProfile, Routing.UserProfileManagement.ViewResumeByUserInfoId, 
                    Routing.UserProfileManagement.ViewResumeParameters.UserInfoId, panelReviewer.UserInfoId) : "#";
                PanelUserAssignmentId = panelReviewer.PanelUserAssignmentId;
                HasPreferredEmailAddress = panelReviewer.HasPreferredEmailAddress;
                InPersonParticipationId = panelReviewer.InPersonParticipationId;
                ParticipationTypeId = panelReviewer.ParticipationTypeId;
                ModifyParticipantPostAssignment = modifyParticipantPostAssignment;
            }
            /// <summary>
            /// Gets the user identifier.
            /// </summary>
            /// <value>
            /// The user identifier.
            /// </value>
            [JsonProperty(PropertyName = "userId")]
            public int UserId { get; private set; }
            /// <summary>
            /// Gets the user information identifier.
            /// </summary>
            /// <value>
            /// The user information identifier.
            /// </value>
            [JsonProperty(PropertyName = "userInfoId")]
            public int UserInfoId { get; private set; }
            /// <summary>
            /// Gets the name.
            /// </summary>
            /// <value>
            /// The name.
            /// </value>
            [JsonProperty(PropertyName = "name")]
            public string Name { get; private set; }
            /// <summary>
            /// Title for Ratings dialog
            /// </summary>
            /// <value>
            /// The name formated as first name, last name
            /// </value>
            [JsonProperty(PropertyName = "dialogTitle")]
            public string DialogTitle { get; private set; }
            /// <summary>
            /// Gets the first name.
            /// </summary>
            /// <value>
            /// The first name.
            /// </value>
            [JsonProperty(PropertyName = "firstName")]
            public string FirstName { get; private set; }

            /// <summary>
            /// Gets the last name.
            /// </summary>
            /// <value>
            /// The last name.
            /// </value>
            [JsonProperty(PropertyName = "lastName")]
            public string LastName { get; private set; }
            /// <summary>
            /// Gets a value indicating whether this <see cref="PanelReviewerViewModel"/> is blocked.
            /// </summary>
            /// <value>
            ///   <c>true</c> if blocked; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "blocked")]
            public bool Blocked { get; private set; }
            /// <summary>
            /// Gets the rating.
            /// </summary>
            /// <value>
            /// The rating.
            /// </value>
            [JsonProperty(PropertyName = "rating")]
            public string Rating { get; private set; }
            /// <summary>
            /// Gets a value indicating whether [communication log].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [communication log]; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "communicationLog")]
            public bool CommunicationLog { get; private set; }
            /// <summary>
            /// Gets the type of the participant.
            /// </summary>
            /// <value>
            /// The type of the participant.
            /// </value>
            [JsonProperty(PropertyName = "participantType")]
            public string ParticipantType { get; private set; }
            /// <summary>
            /// Gets the participant role.
            /// </summary>
            /// <value>
            /// The participant role.
            /// </value>
            [JsonProperty(PropertyName = "participantRole")]
            public string ParticipantRole { get; private set; }
            /// <summary>
            /// Gets the participant role abbreviation.
            /// </summary>
            /// <value>
            /// The participant role abbreviation.
            /// </value>
            [JsonProperty(PropertyName = "participantRoleAbbreviation")]
             public string ParticipantRoleAbbreviation { get; private set; }
            /// <summary>
            /// Gets the type of the participant and the participant role abbreviation.
            /// </summary>
            /// <value>
            /// The type of the participant and the participant role abbreviation.
            /// </value>
            [JsonProperty(PropertyName = "participantTypeAndRoleAbbreviation")]
            public string ParticipantTypeAndRoleAbbreviation { get; private set; }
            /// <summary>
            /// Gets the status.
            /// </summary>
            /// <value>
            /// The status.
            /// </value>
            [JsonProperty(PropertyName = "status")]
            public string Status { get; private set; }
            /// <summary>
            /// Gets the organization.
            /// </summary>
            /// <value>
            /// The organization.
            /// </value>
            [JsonProperty(PropertyName = "organization")]
            public string Organization { get; private set; }
            /// <summary>
            /// Get or set whether the user has a preferred email address
            /// </summary>
            /// <value>
            /// true if the user has one preferred email address; false otherwise
            /// </value>
            [JsonProperty(PropertyName = "hasPreferredEmailAddress")]
            public bool HasPreferredEmailAddress { get; private set; }
            /// <summary>
            /// Gets the degrees.
            /// </summary>
            /// <value>
            /// The degrees.
            /// </value>
            [JsonProperty(PropertyName = "degrees")]
            public string Degrees { get; private set; }
            /// <summary>
            /// Gets the expertise.
            /// </summary>
            /// <value>
            /// The expertise.
            /// </value>
            [JsonProperty(PropertyName = "expertise")]
            public string Expertise { get; private set; }
            /// <summary>
            /// Gets the academic rank.
            /// </summary>
            /// <value>
            /// The academic rank.
            /// </value>
            [JsonProperty(PropertyName = "academicRank")]
            public string AcademicRank { get; private set; }
            /// <summary>
            /// Gets the military branch.
            /// </summary>
            /// <value>
            /// The military branch.
            /// </value>
            [JsonProperty(PropertyName = "militaryBranch")]
            public string MilitaryBranch { get; private set; }
            /// <summary>
            /// Gets the military rank.
            /// </summary>
            /// <value>
            /// The military rank.
            /// </value>
            [JsonProperty(PropertyName = "militaryRank")]
            public string MilitaryRank { get; private set; }
            /// <summary>
            /// Gets the ethnicity.
            /// </summary>
            /// <value>
            /// The ethnicity.
            /// </value>
            [JsonProperty(PropertyName = "ethnicity")]
            public string Ethnicity { get; private set; }
            /// <summary>
            /// Gets the gender.
            /// </summary>
            /// <value>
            /// The gender.
            /// </value>
            [JsonProperty(PropertyName = "gender")]
            public string Gender { get; private set; }
            /// <summary>
            /// Gets a value indicating whether [potential chair] is true.
            /// </summary>
            /// <value>
            ///   <c>true</c> if [potential chair] is true; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "potentialChair")]
            public bool PotentialChair { get; private set; }
            /// <summary>
            /// Gets a value indicating whether [previous program participation] is true.
            /// </summary>
            /// <value>
            /// <c>true</c> if [previous program participation] is true; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "previousProgramParticipation")]
            public bool PreviousProgramParticipation { get; private set; }
            /// <summary>
            /// Returns the display value for the Previous Participation column
            /// </summary>
            [JsonProperty(PropertyName = "programParticipationDisplay")]
            public string ProgramParticipationDisplay
            {
                get
                {
                    return PreviousProgramParticipation ? Invariables.Reviewer.Yes : Invariables.Reviewer.No;
                }
            }
            /// <summary>
            /// Gets a value indicating whether [approved by client].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [approved by client]; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "approvedByClient")]
            public bool? ApprovedByClient { get; private set; }
            /// <summary>
            /// Whether the preferred website link should be enabled.
            /// </summary>
            /// <value>
            ///   <c>true</c> if enabled; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "showPreferredLink")]
            public bool ShowPreferredLink { get; private set; }
            /// <summary>
            /// Gets a value indicating whether resume link should be enabled.
            /// </summary>
            /// <value>
            ///   <c>true</c> if resume link should be enabled; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "showResumeLink")]
            public bool ShowResumeLink { get; private set; }

            /// <summary>
            /// Gets the preferred website address.
            /// </summary>
            /// <value>
            /// The preferred website address.
            /// </value>
            [JsonProperty(PropertyName = "preferredWebsiteAddress")]
            public string PreferredWebsiteAddress { get; private set; }
            /// <summary>
            /// Gets the resume link.
            /// </summary>
            /// <value>
            /// The resume link.
            /// </value>
            [JsonProperty(PropertyName = "resumeLink")]
            public string ResumeLink { get; private set; }
            /// <summary>
            /// Gets the panel user assignment identifier.
            /// </summary>
            /// <value>
            /// The panel user assignment identifier.
            /// </value>
            [JsonProperty(PropertyName = "panelUserAssignmentId")]
            public int PanelUserAssignmentId { get; private set; }
            /// <summary>
            /// MeetingType entity identifier
            /// </summary>
            [JsonProperty(PropertyName = "participationTypeId")]
            public int ParticipationTypeId { get; private set; }
            /// <summary>
            /// InPersonParticipation entity identifier
            /// </summary>
            [JsonProperty(PropertyName = "inPersonParticipationId")]
            public int InPersonParticipationId { get; private set; }
            /// <summary>
            /// Indicates if the panel is an on-site panel.
            /// </summary>
            [JsonProperty(PropertyName = "isOnSitePanel")]
            public bool IsOnSitePanel { get { return this.InPersonParticipationId == ParticipationTypeId; } }

            /// <summary>
            /// Gets a value indicating whether selected controls are enabled after the reviewer 
            /// has been assigned
            /// </summary>
            /// <value>
            ///   <c>true</c>If user has specific permission; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "modifyParticipantPostAssignment")]
            public bool ModifyParticipantPostAssignment { get; private set; }
            /// <summary>
            /// Gets a value indicating whether selected controls are enabled after the reviewer 
            /// has been assigned for an SRO
            /// </summary>
            /// <value>
            ///   <c>true</c>If user has specific permission; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "modifyParticipantPostAssignmentLimited")]
            public bool ModifyParticipantPostAssignmentLimited { get; private set; }
            public int PanelUserAssignementId { get; set; }
        }
        #endregion        
    }
}