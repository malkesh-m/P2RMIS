using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.CrossCuttingServices;
using Newtonsoft.Json;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for the SearchForReviewers view.
    /// </summary>
    public class SearchForReviewersViewModel : PanelManagementViewModel
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        public SearchForReviewersViewModel() { }

        /// <summary>
        /// Initialize the "Other" section dropdowns.
        /// </summary>
        /// <param name="ethnicityDropdown">Ethnicity dropdown values</param>
        /// <param name="stateDropdown">State dropdown values</param>
        /// <param name="genderDropdown">Gender dropdown values</param>
        /// <param name="academicRankDropdown">Academic Rank dropdown values</param>
        /// <param name="participantTypeDropdown">Participant Type dropdown values</param>
        /// <param name="participantRoleDropdown">Participant Role dropdown values</param>
        public void SetOtherDropdowns(List<IListEntry> ethnicityDropdown, List<IListEntry> stateDropdown, List<IListEntry> genderDropdown,
                List<IListEntry> academicRankDropdown, List<IListEntry> participantTypeDropdown, List<IListEntry> participantRoleDropdown)
        {
            this.EthnicityDropdown = ethnicityDropdown;
            this.StateDropdown = stateDropdown;
            this.GenderDropdown = genderDropdown;
            this.AcademicRankDropdown = academicRankDropdown;
            this.ParticipantTypeDropdown = participantTypeDropdown;
            this.ParticipantRoleDropdown = participantRoleDropdown;
        }
        /// <summary>
        /// Initialize client specific information
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="clientOpenPrograms">Client open program dropdown values</param>
        internal virtual void SetClientSpecific(int clientId, List<IClientProgramModel> clientOpenProgramsDropdown)
        {
            this.ClientId = clientId;
            this.ClientOpenProgramsDropdown = clientOpenProgramsDropdown;
        }        
        /// <summary>
        /// Sets the fiscal year list.
        /// </summary>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="fiscalYearList">The fiscal year list.</param>
        internal virtual void SetFiscalYearList(int? yearId, List<Sra.P2rmis.WebModels.Criteria.IProgramYearModel> fiscalYearList)
        {
            this.YearId = yearId;
            this.FiscalYearList = fiscalYearList;
        }
        /// <summary>
        /// Sets the panel list.
        /// </summary>
        /// <param name="sessionPanelAbbreviation">The session panel abbreviation.</param>
        /// <param name="panelList">The panel list.</param>
        internal virtual void SetPanelList(string sessionPanelAbbreviation, List<Sra.P2rmis.WebModels.Criteria.ISessionPanelModel> panelList)
        {
            this.SessionPanelAbbreviation = sessionPanelAbbreviation;
            this.PanelList = panelList;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Client entity identifier
        /// </summary>
        public int ClientId { get; private set; }
        #region Dropdown contents
        /// <summary>
        /// The ethnicity dropdown list
        /// </summary>
        public List<IListEntry> EthnicityDropdown { get; private set; }
        /// <summary>
        /// The state dropdown list
        /// </summary>
        public List<IListEntry> StateDropdown { get; private set; }
        /// <summary>
        /// The gender dropdown list
        /// </summary>
        public List<IListEntry> GenderDropdown { get; private set; }
        /// <summary>
        /// Gets the academic rank dropdown.
        /// </summary>
        /// <value>
        /// The academic rank dropdown.
        /// </value>
        public List<IListEntry> AcademicRankDropdown { get; private set; }
        /// <summary>
        /// Gets the participant type dropdown.
        /// </summary>
        /// <value>
        /// The participant type dropdown.
        /// </value>
        public List<IListEntry> ParticipantTypeDropdown { get; private set; }
        /// <summary>
        /// Gets the participant role dropdown.
        /// </summary>
        /// <value>
        /// The participant role dropdown.
        /// </value>
        public List<IListEntry> ParticipantRoleDropdown { get; private set; }
        /// <summary>
        /// List of client's open programs
        /// </summary>
        public List<IClientProgramModel> ClientOpenProgramsDropdown { get; private set; }
        /// Gets or sets the fiscal year list.
        /// </summary>
        /// <value>
        /// The fiscal year list.
        /// </value>
        public List<Sra.P2rmis.WebModels.Criteria.IProgramYearModel> FiscalYearList { get; set; }
        /// <summary>
        /// Gets or sets the panel list.
        /// </summary>
        /// <value>
        /// The panel list.
        /// </value>
        public List<Sra.P2rmis.WebModels.Criteria.ISessionPanelModel> PanelList { get; set; }
        /// <summary>
        /// Gets the reviewer results.
        /// </summary>
        /// <value>
        /// The reviewer results.
        /// </value>
        public List<ReviewerResult> ReviewerResults { get; private set; }
        #endregion

        /// <summary>
        /// Review result model
        /// </summary>
        public class ReviewerResult
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ReviewerResult"/> class.
            /// </summary>
            public ReviewerResult() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="ReviewerResult"/> class.
            /// </summary>
            /// <param name="reviewerSearchResultModel">The reviewer search result model.</param>
            public ReviewerResult(IReviewerSearchResultModel reviewerSearchResultModel)
            {
                ReviewerName = ViewHelpers.ConstructNameWithSuffix(reviewerSearchResultModel.FirstName, reviewerSearchResultModel.LastName, reviewerSearchResultModel.Suffix);
                Expertise = reviewerSearchResultModel.Expertise ?? string.Empty;
                Organization = reviewerSearchResultModel.Organization ?? string.Empty;
                AcademicRank = reviewerSearchResultModel.AcademicRank ?? string.Empty;
                MilitaryRank = reviewerSearchResultModel.MilitaryRank ?? string.Empty;
                MilitaryBranch = reviewerSearchResultModel.MilitaryBranch ?? string.Empty;
                Ethinicity = reviewerSearchResultModel.Ethnicity ?? string.Empty;
                Gender = reviewerSearchResultModel.Gender ?? string.Empty;
                Rating = (reviewerSearchResultModel.Rating != null) ? Convert.ToString(ViewHelpers.P2rmisRound((decimal)reviewerSearchResultModel.Rating)) : string.Empty;
                IsPotentialChair = reviewerSearchResultModel.IsPotentialChair;
                IsPreviouslyParticipated = (reviewerSearchResultModel.IsPreviouslyParticipated || reviewerSearchResultModel.IsProgramUser) ? true : false;
                HasCommunicationLog = reviewerSearchResultModel.HasCommunicationLog;
                UserResumeId = reviewerSearchResultModel.UserResumeId;
                PreferredWebsiteAddress = reviewerSearchResultModel.PreferredWebsiteAddress;
                ShowPreferredLink = !string.IsNullOrWhiteSpace(reviewerSearchResultModel.PreferredWebsiteAddress);
                ShowResumeLink = reviewerSearchResultModel.UserResumeId != null;
                UserId = reviewerSearchResultModel.UserId;
                UserInfoId = reviewerSearchResultModel.UserInfoId;
                Blocked = reviewerSearchResultModel.IsBlocked;
                Status = Routing.PanelManagement.AssignmentStatus.Potential;
                View = (UserResumeId != null || !string.IsNullOrEmpty(PreferredWebsiteAddress));
                ResumeLink = reviewerSearchResultModel.UserResumeId != null ? string.Format("/{0}/{1}?{2}={3}", Routing.P2rmisControllers.UserProfile, Routing.UserProfileManagement.ViewResumeByUserInfoId,
                Routing.UserProfileManagement.ViewResumeParameters.UserInfoId, reviewerSearchResultModel.UserInfoId) : "#";
                IsOnPanel = reviewerSearchResultModel.IsOnPanel;
            }
            /// <summary>
            /// Gets the name of the reviewer.
            /// </summary>
            /// <value>
            /// The name of the reviewer.
            /// </value>
            [JsonProperty(PropertyName = "name")]
            public string ReviewerName { get; private set; }
            /// <summary>
            /// Gets the expertise.
            /// </summary>
            /// <value>
            /// The expertise.
            /// </value>
            [JsonProperty(PropertyName = "expertise")]
            public string Expertise { get; private set; }
            /// <summary>
            /// Gets the organization.
            /// </summary>
            /// <value>
            /// The organization.
            /// </value>
            [JsonProperty(PropertyName = "organization")]
            public string Organization { get; private set; }
            /// <summary>
            /// Gets the academic rank.
            /// </summary>
            /// <value>
            /// The academic rank.
            /// </value>
            [JsonProperty(PropertyName = "academicRank")]
            public string AcademicRank { get; private set; }
            /// <summary>
            /// Gets the military rank.
            /// </summary>
            /// <value>
            /// The military rank.
            /// </value>
            [JsonProperty(PropertyName = "militaryRank")]
            public string MilitaryRank { get; private set; }
            /// <summary>
            /// Gets the military branch.
            /// </summary>
            /// <value>
            /// The military branch.
            /// </value>
            [JsonProperty(PropertyName = "militaryBranch")]
            public string MilitaryBranch { get; private set; }
            /// <summary>
            /// Gets the ethnicity.
            /// </summary>
            /// <value>
            /// The ethnicity.
            /// </value>
            [JsonProperty(PropertyName = "ethnicity")]
            public string Ethinicity { get; private set; }
            /// <summary>
            /// Gets the gender.
            /// </summary>
            /// <value>
            /// The gender.
            /// </value>
            [JsonProperty(PropertyName = "gender")]
            public string Gender { get; private set; }
            /// <summary>
            /// Gets the rating.
            /// </summary>
            /// <value>
            /// The rating.
            /// </value>
            [JsonProperty(PropertyName = "rating")]
            public string Rating { get; private set; }
            /// <summary>
            /// Gets a value indicating whether this instance is potential chair.
            /// </summary>
            /// <value>
            /// <c>true</c> if this instance is potential chair; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "potentialChair")]
            public bool IsPotentialChair { get; private set; }
            /// <summary>
            /// Gets a value indicating whether this instance is previously participated.
            /// </summary>
            /// <value>
            /// <c>true</c> if this instance is previously participated; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "previousProgramParticipation")]
            public bool IsPreviouslyParticipated { get; private set; }
            /// <summary>
            /// Gets a value indicating whether this instance has communication log.
            /// </summary>
            /// <value>
            /// <c>true</c> if this instance has communication log; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "hasCommunicationLog")]
            public bool HasCommunicationLog { get; private set; }
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
            /// Gets the user resume identifier.
            /// </summary>
            /// <value>
            /// The user resume identifier.
            /// </value>
            [JsonProperty(PropertyName = "userResumeId")]
            public int? UserResumeId { get; private set; }
            /// <summary>
            /// Gets the preferred website address.
            /// </summary>
            /// <value>
            /// The preferred website address.
            /// </value>
            [JsonProperty(PropertyName = "preferredWebsiteAddress")]
            public string PreferredWebsiteAddress { get; private set; }
            /// <summary>
            /// Gets a value indicating whether this <see cref="ReviewerResult"/> is view.
            /// </summary>
            /// <value>
            ///   <c>true</c> if view; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "view")]
            public bool View { get; private set; }
            /// <summary>
            /// Gets a value indicating whether this <see cref="ReviewerResult"/> is blocked.
            /// </summary>
            /// <value>
            ///   <c>true</c> if blocked; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "blocked")]
            public bool Blocked { get; private set; }
            /// <summary>
            /// Gets the dialog title.
            /// </summary>
            /// <value>
            /// The dialog title.
            /// </value>
            [JsonProperty(PropertyName = "dialogTitle")]
            public string DialogTitle { get; private set; }
            /// <summary>
            /// Gets the user identifier.
            /// </summary>
            /// <value>
            /// The user identifier.
            /// </value>
            [JsonProperty(PropertyName = "userId")]
            public int? UserId { get; private set; }
            /// <summary>
            /// Gets the user information identifier.
            /// </summary>
            /// <value>
            /// The user information identifier.
            /// </value>
            [JsonProperty(PropertyName = "userInfoId")]
            public int? UserInfoId { get; private set; }
            /// <summary>
            /// Gets the status.
            /// </summary>
            /// <value>
            /// The status.
            /// </value>
            [JsonProperty(PropertyName = "status")]
            public string Status { get; private set; }
            /// <summary>
            /// Gets the resume link.
            /// </summary>
            /// <value>
            /// The resume link.
            /// </value>
            [JsonProperty(PropertyName = "resumeLink")]
            public string ResumeLink { get; private set; }
            /// <summary>
            /// Gets the is on panel status.
            /// </summary>
            /// <value>
            /// The is on panel status.
            /// </value>
            [JsonProperty(PropertyName = "isOnPanel")]
            public bool IsOnPanel { get; private set; }
            /// <summary>
            /// Gets or sets a value indicating whether this instance can manage account.
            /// </summary>
            /// <value>
            /// <c>true</c> if this instance can manage account; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "canManageAccount")]
            public bool CanManageAccount { get; set; }
            /// <summary>
            /// Gets the program participation display.
            /// </summary>
            /// <value>
            /// The program participation display.
            /// </value>
            [JsonProperty(PropertyName = "programParticipationDisplay")]
            public string ProgramParticipationDisplay
            {
                get
                {
                    return IsPreviouslyParticipated ? Invariables.Reviewer.Yes : Invariables.Reviewer.No;
                }
            }
        }

        #endregion
        #region Search parameters
        /// <summary>
        /// Ethnicity selection identifier
        /// </summary>
        public int? EthinicityId { get; set; }
        /// <summary>
        /// Ethnicity selection identifier
        /// </summary>
        public int? StateId { get; set; }
        /// <summary>
        /// Ethnicity selection identifier
        /// </summary>
        public int? GenderId { get; set; }
        /// <summary>
        /// Gets or sets the academic rank identifier.
        /// </summary>
        /// <value>
        /// The academic rank identifier.
        /// </value>
        public int? AcademicRankId { get; set; }
        /// <summary>
        /// Gets or sets the participant type identifier.
        /// </summary>
        /// <value>
        /// The participant type identifier.
        /// </value>
        public int? ParticipantTypeId { get; set; }
        /// <summary>
        /// Gets or sets the participant role identifier.
        /// </summary>
        /// <value>
        /// The participant role identifier.
        /// </value>
        public int? ParticipantRoleId { get; set; }
        /// <summary>
        /// Gets or sets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        public int? ProgramYearId { get; set; }
        /// <summary>
        /// Gets the person key dropdown.
        /// </summary>
        /// <value>
        /// The person key dropdown.
        /// </value>
        public List<KeyValuePair<string, string>> PersonKeyDropdown
        {
            get
            {
                var dd = new List<KeyValuePair<string, string>>();
                dd.Add(new KeyValuePair<string, string>(Invariables.PersonKey.Name, Invariables.PersonKey.Name));
                dd.Add(new KeyValuePair<string, string>(Invariables.PersonKey.LastName, Invariables.PersonKey.LastName));
                dd.Add(new KeyValuePair<string, string>(Invariables.PersonKey.FirstName, Invariables.PersonKey.FirstName));
                dd.Add(new KeyValuePair<string, string>(Invariables.PersonKey.UserId, Invariables.PersonKey.UserId));
                dd.Add(new KeyValuePair<string, string>(Invariables.PersonKey.Username, Invariables.PersonKey.Username));

                return dd;
            }
        }
        /// <summary>
        /// Gets or sets the person key such as last name, first name, user id or username.
        /// </summary>
        /// <value>
        /// The person key such as last name, first name, user id or username.
        /// </value>
        public string PersonKey { get; set; }
        /// <summary>
        /// Gets or sets the person's last name, first name, user id or username.
        /// </summary>
        /// <value>
        /// The person's last name, first name, user id or username.
        /// </value>
        public string PersonValue { get; set; }
        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        public string Organization { get; set; }
        /// <summary>
        /// Gets or sets the resume.
        /// </summary>
        /// <value>
        /// The resume.
        /// </value>
        public string Resume { get; set; }
        /// <summary>
        /// Gets or sets the expertise.
        /// </summary>
        /// <value>
        /// The expertise.
        /// </value>
        public string Expertise { get; set; }
        /// <summary>
        /// Gets or sets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        public string PanelName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is potential chair.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is potential chair; otherwise, <c>false</c>.
        /// </value>
        public bool IsPotentialChair { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is state excluded.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is state excluded; otherwise, <c>false</c>.
        /// </value>
        public bool IsStateExcluded { get; set; }
        /// <summary>
        /// Selected Program entity identifier
        /// </summary>
        public int? ProgramId { get; set; }
        /// <summary>
        /// Selected Program Year entity identifier
        /// </summary>
        public int? YearId { get; set; }
        /// <summary>
        /// Gets or sets the panel name selected.
        /// </summary>
        /// <value>
        /// The panel name selected.
        /// </value>
        public string PanelNameSelected { get; set; }
        /// <summary>
        /// Gets or sets the session panel identifier.
        /// </summary>
        /// <value>
        /// The session panel identifier.
        /// </value>
        public string SessionPanelAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public string Rating { get; set; }
        /// <summary>
        /// Gets the rating dropdown.
        /// </summary>
        /// <value>
        /// The rating dropdown.
        /// </value>
        public List<KeyValuePair<string, string>> RatingDropdown
        {
            get
            {
                var dd = new List<KeyValuePair<string, string>>();
                dd.Add(new KeyValuePair<string, string>(string.Empty, Invariables.Rating.NonApplicable));
                for (var i = Invariables.Rating.MinimumRating; i <= Invariables.Rating.MaximumRating; i++)
                {
                    dd.Add(new KeyValuePair<string, string>(i.ToString(), i.ToString()));
                }
                return dd;
            }
        }
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
        #endregion
        #region Services        
        /// <summary>
        /// Gets the search for reviewers model.
        /// </summary>
        /// <returns></returns>
        public SearchForReviewersModel GetSearchForReviewersModel()
        {
            string firstName = (PersonKey == Invariables.PersonKey.FirstName) ? PersonValue : string.Empty;
            string lastName = (PersonKey == Invariables.PersonKey.LastName) ? PersonValue : string.Empty;
            string userIdStr = (PersonKey == Invariables.PersonKey.UserId) ? PersonValue : string.Empty;
            string username = (PersonKey == Invariables.PersonKey.Username) ? PersonValue : string.Empty;
            string name = (PersonKey == Invariables.PersonKey.Name) ? PersonValue : string.Empty;

            int? userId = null;
            if (!string.IsNullOrWhiteSpace(userIdStr))
            {
                int tmp;
                int.TryParse(userIdStr.Trim(), out tmp);
                userId = tmp;
            }
            
            if (!string.IsNullOrEmpty(name))
            {
                var nameArray = name.Split(',');
                lastName = nameArray[0].Trim();
                if (nameArray.Length > 1)
                {
                    firstName = nameArray[1].Trim();
                }
            }

            string panelName = PanelName == null && PanelNameSelected != "0" ? PanelNameSelected : PanelName;

            var searchForReviewers = new SearchForReviewersModel(firstName, lastName, userId, username, Organization,
                    ProgramId, YearId, panelName, Resume, Expertise, 
                    ParticipantTypeId, ParticipantRoleId, AcademicRankId, IsPotentialChair, Rating, 
                    StateId, IsStateExcluded, GenderId, EthinicityId, ProgramYearId, SessionPanelAbbreviation);
            return searchForReviewers;
        }
        /// <summary>
        /// Sets the reviewer results.
        /// </summary>
        /// <param name="reviewerSearchResultModels">The reviewer search result models.</param>
        /// <param name="restrictedManageUserAccountsPermission">Restricted manage accounts permission.</param>
        public void SetReviewerResults(List<IReviewerSearchResultModel> reviewerSearchResultModels, bool restrictedManageUserAccountsPermission)
        {
            ReviewerResults = reviewerSearchResultModels.ConvertAll(x => new ReviewerResult(x))
                .Select((item, index) => 
                { item.CanManageAccount = item.IsOnPanel || !restrictedManageUserAccountsPermission; return item; }).ToList();
        }
        #endregion
    }
}