using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.ReviewerRecruitment;
using Sra.P2rmis.CrossCuttingServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Controllers;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for panel assignment
    /// </summary>
    public class PanelAssignmentViewModel
    {
        public const string ChangeStatus = "Change Status";
        public const string Transfer = "Transfer";
        public const string Remove = "Remove";
        public const string Assign = "Assign";
        public const string IPM = "Integration Panel Member";

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelAssignmentViewModel"/> class.
        /// </summary>
        public PanelAssignmentViewModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PanelAssignmentViewModel"/> class.
        /// </summary>
        /// <param name="panelAssignmentModalModel">The panel assignment modal model.</param>
        /// <param name="participantUserId">The participant user identifier.</param>
        /// <param name="isBlocked">The user is blocked.</param>
        /// <param name="status">The user is assigned</param>
        /// <param name="canProcessPanel">Whether the user can process panel</param>
        /// <param name="canManageApplicationReviewer">Whether the user can add, modify, or delete panel application reviewer assignment</param>
        public PanelAssignmentViewModel(IPanelAssignmentModalModel panelAssignmentModalModel, int participantUserId, bool isBlocked, bool status, 
            bool canProcessPanel, bool canManageApplicationReviewer)
        {
            MeetingType = panelAssignmentModalModel.MeetingType;
            PotentialAddedDate = ViewHelpers.FormatDate(panelAssignmentModalModel.PotentialAddedDate);
            AssignedDate = ViewHelpers.FormatDate(panelAssignmentModalModel.AssignedDate);
            ParticipantTypeId = panelAssignmentModalModel.ParticipantTypeId;
            ParticipationMethodId = panelAssignmentModalModel.ParticipationMethodId;
            ParticipantRoleId = panelAssignmentModalModel.ParticipantRoleId;
            ClientApproval = panelAssignmentModalModel.ClientApproved;
            ParticipationLevel = panelAssignmentModalModel.Level;
            var panelParticipationHistory = panelAssignmentModalModel.ParticipationHistory.ToList().ConvertAll(x => new ParticipationEntry(x));
            var programParticipationHistory = panelAssignmentModalModel.ProgramParticipationHistory.ToList().ConvertAll(x => new ParticipationEntry(x));
            ParticipationHistory = panelParticipationHistory.Concat(programParticipationHistory).OrderByDescending(x => x.FiscalYear).ThenBy(x => x.PanelEndDate).ThenBy(x => x.Panel).ToList();
            PanelUserAssignmentId = panelAssignmentModalModel.PanelUserAssignmentId;
            PanelUserPotentialAssignmentId = panelAssignmentModalModel.PanelUserPotentialAssignmentId;
            ParticipantUserId = participantUserId;
            ClientApprovalId = (ClientApproval != null) ? Convert.ToInt32(ClientApproval) : -1;
            IsParticipationRestricted = ParticipationLevel != Invariables.Labels.Full;
            SessionPanelId = panelAssignmentModalModel.SessionPanelId;
            IsBlocked = isBlocked;
            StatusValue = ChangeStatus;
            CanProcessPanel = canProcessPanel;
            CanManageApplicationReviewer = canManageApplicationReviewer;
            IsAssigned = status;
            SetChangeStatusList();
        }

        /// <summary>
        /// Sets the lists.
        /// </summary>
        /// <param name="participantTypeList">The participant type list.</param>
        /// <param name="participantRoleList">The participant role list.</param>
        /// <param name="participationMethodList">The participation method list.</param>
        /// <param name="participationLevelList">The participation level list.</param>
        public void SetLists(List<IListEntry> participantTypeList, List<IListEntry> participantRoleList, List<IListEntry> participationMethodList,
                List<ILogicalListEntry> participationLevelList)
        {
            ParticipantTypeList = participantTypeList;
            ParticipantRoleList = participantRoleList;
            ParticipationMethodList = participationMethodList;
            ParticipationLevelList = participationLevelList.OrderBy(x => x.Index).ToList();
        }
        /// <summmary>
        /// </summmary>
        public bool CanProcessPanel { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can manage application reviewer.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can manage application reviewer; otherwise, <c>false</c>.
        /// </value>
        public bool CanManageApplicationReviewer { get; set; }
        /// <summary>
        /// Gets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int? PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Gets the panel user potential assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user potential assignment identifier.
        /// </value>
        public int? PanelUserPotentialAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the session panel identifier.
        /// </summary>
        /// <value>
        /// The session panel identifier.
        /// </value>
        public int SessionPanelId { get; set; }
        /// <summary>
        /// Gets or sets the participant user identifier.
        /// </summary>
        /// <value>
        /// The participant user identifier.
        /// </value>
        public int ParticipantUserId { get; set; }
        /// <summary>
        /// Gets the type of the meeting.
        /// </summary>
        /// <value>
        /// The type of the meeting.
        /// </value>
        public string MeetingType { get; private set; }
        /// <summary>
        /// Gets the potential added date.
        /// </summary>
        /// <value>
        /// The potential added date.
        /// </value>
        public string PotentialAddedDate { get; private set; }
        /// <summary>
        /// Gets the assigned date.
        /// </summary>
        /// <value>
        /// The assigned date.
        /// </value>
        public string AssignedDate { get; private set; }
        /// <summary>
        /// Gets or sets the participant type identifier.
        /// </summary>
        /// <value>
        /// The participant type identifier.
        /// </value>
        public int? ParticipantTypeId { get; set; }
        /// <summary>
        /// Gets or sets the participation method identifier.
        /// </summary>
        /// <value>
        /// The participation method identifier.
        /// </value>
        public int? ParticipationMethodId { get; set; }
        /// <summary>
        /// Gets or sets the client approval.
        /// </summary>
        /// <value>
        /// The client approval.
        /// </value>
        public bool? ClientApproval { get; set; }
        /// <summary>
        /// Gets the client approval identifier.
        /// </summary>
        /// <value>
        /// The client approval identifier.
        /// </value>
        public int ClientApprovalId { get; set; }
        /// <summary>
        /// Gets or sets the participant role identifier.
        /// </summary>
        /// <value>
        /// The participant role identifier.
        /// </value>
        public int? ParticipantRoleId { get; set; }
        /// <summary>
        /// Gets or sets the participant role identifier.
        /// </summary>
        /// <value>
        /// The participant role identifier.
        /// </value>
        public string StatusValue { get; set; }
        /// <summary>
        /// Gets or sets the participation level identifier.
        /// </summary>
        /// <value>
        /// The participation level identifier.
        /// </value>
        public string ParticipationLevel { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is participation restricted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is participation restricted; otherwise, <c>false</c>.
        /// </value>
        public bool IsParticipationRestricted { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PanelAssignmentViewModel"/> is assigned.
        /// </summary>
        /// <value>
        ///   <c>true</c> if assigned; otherwise, <c>false</c>.
        /// </value>
        public bool IsAssigned { get; set; }
        /// <summary>
        /// Gets or sets the change status list.
        /// </summary>
        /// <value>
        /// The change status list.
        /// </value>
        public List<String> ChangeStatusList { get; set; } = new List<string>();
        /// <summary>
        /// Gets the participant type list.
        /// </summary>
        /// <value>
        /// The participant type list.
        /// </value>
        public List<IListEntry> ParticipantTypeList { get; set; }
        /// <summary>
        /// Gets the participation method list.
        /// </summary>
        /// <value>
        /// The participation method list.
        /// </value>
        public List<IListEntry> ParticipationMethodList { get; set; }
        /// <summary>
        /// Gets or sets the client approval list.
        /// </summary>
        /// <value>
        /// The client approval list.
        /// </value>
        public List<KeyValuePair<int, string>> ClientApprovalList
        {
            get
            {
                var list = new List<KeyValuePair<int, string>>();
                list.Add(new KeyValuePair<int, string>(-1, Invariables.Labels.NonApplicable));
                list.Add(new KeyValuePair<int, string>(1, Invariables.Labels.Yes));
                list.Add(new KeyValuePair<int, string>(0, Invariables.Labels.No));
                return list;
            }
        }
        /// <summary>
        /// Gets the participant role list.
        /// </summary>
        /// <value>
        /// The participant role list.
        /// </value>
        public List<IListEntry> ParticipantRoleList { get; set; }
        /// <summary>
        /// Gets the participant level list.
        /// </summary>
        /// <value>
        /// The participant level list.
        /// </value>
        public List<ILogicalListEntry> ParticipationLevelList { get; set; }
        /// <summary>
        /// Gets or sets the participation history.
        /// </summary>
        /// <value>
        /// The participation history.
        /// </value>
        public IEnumerable<ParticipationEntry> ParticipationHistory { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is blocked.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is blocked; otherwise, <c>false</c>.
        /// </value>
        public bool IsBlocked { get; set; }
        /// <summary>
        /// Sets the client approval.
        /// </summary>
        public bool HasAssignedApplications { get; set; }

        public void SetClientApproval()
        {
            ClientApproval = ClientApprovalId == -1 ? null : (bool?)Convert.ToBoolean(ClientApprovalId);
        }
        /// <summary>
        /// Sets the change status list.
        /// </summary>
        private void SetChangeStatusList()
        {
            // Create different lists based on different statuses
            ChangeStatusList.Add(ChangeStatus);

            if (IsAssigned)
            {
                if (CanManageApplicationReviewer)
                {
                    ChangeStatusList.Add(Transfer);
                    ChangeStatusList.Add(Remove);
                }
            }
            else if (!String.IsNullOrEmpty(PotentialAddedDate))
            {
                if (!IsBlocked)
                {
                    ChangeStatusList.Add(Assign);
                    if (CanProcessPanel)
                        ChangeStatusList.Add(Remove);
                }
                else
                {
                    if (CanProcessPanel)
                        ChangeStatusList.Add(Remove);
                }
            }
        }

        /// <summary>
        /// Participation history
        /// </summary>
        public class ParticipationEntry
        {
            #region Construction & Setup
            /// <summary>
            /// Initializes a new instance of the <see cref="ParticipationEntry"/> class.
            /// </summary>
            public ParticipationEntry() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="ParticipationEntry"/> class.
            /// </summary>
            /// <param name="participationHistoryModel">The participation history model.</param>
            public ParticipationEntry(IParticipationHistoryModel participationHistoryModel)
            {
                Client = participationHistoryModel.ClientName;
                Program = participationHistoryModel.ProgramAbbreviation;
                ParticipantType = participationHistoryModel.ParticipationType ?? string.Empty;
                Role = participationHistoryModel.ParticipationRole ?? string.Empty;
                PanelEndDate = ViewHelpers.FormatDate(participationHistoryModel.PanelEndDate);
                MeetingType = participationHistoryModel.MeetingType;
                Registration = RegistrationIs(participationHistoryModel);
                IsPotential = participationHistoryModel.IsPotential;
                FiscalYear = participationHistoryModel.FiscalYear;
                PanelAbbreviation = participationHistoryModel.PanelAbbreviation;
                PanelId = participationHistoryModel.PanelId;
                Level = participationHistoryModel.Level;
                ParticipationMethodLabel = participationHistoryModel.ParticipationMethodLabel;
                SroNames = (participationHistoryModel.SroList.Count != 0) ? participationHistoryModel.SroList.OrderBy(x => x.Item2).ToList().ConvertAll(x => new SroName(x.Item1, x.Item2, x.Item3)) : null;
            }

            public ParticipationEntry(IProgramParticipationHistoryModel programParticipationHistoryModel)
            {
                ClientParticipantTypeId = programParticipationHistoryModel.ClientParticipantTypeId;
                ParticipationMethodLabel = IPM;
                ParticipantType = IPM;
                ProgramYearId = programParticipationHistoryModel.ProgramYearId;
                Program = programParticipationHistoryModel.Program;
                FiscalYear = programParticipationHistoryModel.FiscalYear;
            }
            /// <summary>
            /// Determine the Participation history entry "Registration" state.
            /// </summary>
            /// <param name="participationHistoryModel">Participation history entry</param>
            /// <returns>Registration state</returns>
            protected string RegistrationIs(IParticipationHistoryModel participationHistoryModel)
            {
                if(participationHistoryModel.ParticipationMethodLabel == "Integration Panel Member")
                {
                    return "";
                }
                else
                {
                    return participationHistoryModel.IsPotential ? Invariables.Labels.Potential : participationHistoryModel.IsRegistrationComplete ? Invariables.Labels.Complete : Invariables.Labels.Incomplete;
                }
            }
            #endregion
            #region Attributes
            /// <summary>
            /// Indicates if the history entry is a potential assignment.
            /// </summary>
            [JsonProperty(PropertyName = "isPotential")]
            public bool IsPotential { get; private set; }

            /// <summary>
            /// Gets the client.
            /// </summary>
            /// <value>
            /// The client.
            /// </value>
            [JsonProperty(PropertyName = "client")]
            public string Client { get; private set; }
            /// <summary>
            /// Gets the program.
            /// </summary>
            /// <value>
            /// The program.
            /// </value>
            [JsonProperty(PropertyName = "program")]
            public string Program { get; private set; }
            /// <summary>
            /// Gets the type of the participant.
            /// </summary>
            /// <value>
            /// The type of the participant.
            /// </value>
            public string ParticipantType { get; private set; }
            /// <summary>
            /// Gets the role.
            /// </summary>
            /// <value>
            /// The role.
            /// </value>
            [JsonProperty(PropertyName = "role")]
            public string Role { get; private set; }
            /// <summary>
            /// Gets the panel end date.
            /// </summary>
            /// <value>
            /// The panel end date.
            /// </value>
            [JsonProperty(PropertyName = "panelEnd")]
            public string PanelEndDate { get; private set; }
            /// <summary>
            /// Gets the type of the meeting.
            /// </summary>
            /// <value>
            /// The type of the meeting.
            /// </value>
            [JsonProperty(PropertyName = "meetingType")]
            public string MeetingType { get; private set; }
            /// <summary>
            /// Gets the registration.
            /// </summary>
            /// <value>
            /// The registration.
            /// </value>
            [JsonProperty(PropertyName = "registration")]
            public string Registration { get; private set; }

            /// <summary>
            /// Program fiscal year
            /// </summary>
            public string FiscalYear { get; private set; }

            /// <summary>
            /// Gets the program year identifier.
            /// </summary>
            /// <value>
            /// The program year identifier.
            /// </value>
            public int ProgramYearId { get; private set; }
            /// <summary>
            /// Gets the client participant type identifier.
            /// </summary>
            /// <value>
            /// The client participant type identifier.
            /// </value>
            public int ClientParticipantTypeId { get; private set; }
            /// <summary>
            /// Panel abbreviation
            /// </summary>
            public string PanelAbbreviation { get; private set; }
            public int PanelId { get; private set; }
            /// <summary>
            /// Panel abbreviation
            /// </summary>
            [JsonProperty(PropertyName = "panel")]
            public string Panel
            {
                get
                {
                    return $"{PanelAbbreviation}";
                }
            }
            [JsonProperty(PropertyName = "sro")]
            public string ProgramFy
            {
                get
                {
                    return $"{PanelAbbreviation}";
                    // return sro's assigned 
                }
            }
            /// <summary>
            /// ParticipationMethod label value
            /// </summary>
            public string ParticipationMethodLabel { get; private set; }
            /// <summary>
            /// Reviewer participation level (Partial or Full)
            /// </summary>
            public string Level { get; private set; }
            /// <summary>
            /// Gets the sro names.
            /// </summary>
            /// <value>
            /// The sro names.
            /// </value>
            public List<SroName> SroNames { get; private set; }
            /// <summary>
            /// The view's grid value.
            /// </summary>
            [JsonProperty(PropertyName = "participantType")]
            public string FormattedParticipationType
            {
                get
                {
                    return FormatParticipationType();
                }
            }
            #endregion
            #region Methods
            /// <summary>
            /// Produce short fiscal year string (ex. 2015 -> 15).
            /// </summary>
            /// <returns>
            /// If the fiscal string length is not 4 (ex 2015) the fiscal year string
            /// is returned.  Otherwise the last 2 numbers (ex 15) are returned.
            /// </returns>
            protected string ShortFiscalYear()
            {
                return (this.FiscalYear.Length == 4) ? this.FiscalYear.Substring(2, 2) : this.FiscalYear;
            }
            /// <summary>
            /// Create a formatted string for the Participation type.
            /// </summary>
            /// <remarks>
            /// You might be wondering why a method is used to format the string when it could have been
            /// inserted in place of the call in the property getter.  Basically a property is
            /// a macro and the property definition is replaced by the compiler at the calling site.  I admit
            /// that I do not know how the kendo control is implemented but it seemed prudent to error on the 
            /// side of caution.
            /// </remarks>
            /// <returns>Formatted participation type</returns>
            protected string FormatParticipationType()
            {
                if(this.ParticipationMethodLabel == "Integration Panel Member")
                {
                    return $"{this.ParticipationMethodLabel}";
                }
                else
                {
                    return $"{this.ParticipantType}-{this.ParticipationMethodLabel}-{this.Level}";
                }
            }
            #endregion

            public class SroName
            {
                public SroName(string firstName, string lastName, string email)
                {
                    FirstName = firstName;
                    LastName = lastName;
                    Email = email;
                }

                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string Email { get; set; }
            }
        }

    }
}