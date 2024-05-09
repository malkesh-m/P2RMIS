using System;
using System.Collections.Generic;
using Sra.P2rmis.Bll.ApplicationManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.ViewModels.MyWorkspace;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// My Workspace view model
    /// </summary>
    public class MyWorkspaceViewModel : MyWorkspaceTabsViewModel
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MyWorkspaceViewModel() : base()
        {
            SessionPanels = new List<IListEntry>();
            NoActivePanels = MessageService.NoActivePanels;
            NotRegistratedForPanel = MessageService.NotRegistratedForPanel;
            PollInterval = ConfigManager.MyWorkspaceScorableApplicationPollingInterval;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public MyWorkspaceViewModel(List<IBaseAssignmentModel> applications, PanelStatus panelStatus, bool isClientChair) : this()
        {
            IsReleased = panelStatus.IsReleased;
            IsScoringStatus = panelStatus.IsReleased && !panelStatus.IsPostAssigned;
            IsPostAssignedStatus = panelStatus.IsPostAssigned;
            PhaseName = panelStatus.PhaseName;
            PhaseEndDate = ViewHelpers.FormatEtDateTime(panelStatus.EndDate);

            ParticipantType = panelStatus.ParticipantType;
            ParticipantRole = panelStatus.RoleName;

            ParticipantTypeAndRole = ViewHelpers.ConcatenateStringWithComma(ParticipantType, ParticipantRole);
            IsClientChair = isClientChair;
            if (isClientChair)
            {
                ChairApplications = GetChairApplications(applications);
                if (HasCoiAllBeenStarted(ChairApplications))
                {
                    AppendCpritChairTabs();
                }
            }
            else
            {
                if (IsPostAssignedStatus)
                {
                    PostAssignmentApplications = GetPostAssignmentApplications(applications);
                    HasAssignedApplications = GetHasAssignedApplications(applications);
                }
                else if (IsScoringStatus)
                {
                    ScorableApplications = GetScorableApplications(applications);
                }
                else
                {
                    PreAssignmentApplications = GetPreAssignmentApplications(applications);
                }
            }
            NotRegistered = false;
        }

        #region Properties
        public int ApplicationId { get; set; }
        /// <summary>
        /// Is this user a CPRIT Chair role
        /// </summary>
        public bool IsClientChair { get; set; }
        /// <summary>
        /// List of session panels
        /// </summary>
        public List<IListEntry> SessionPanels { get; set; }
        /// <summary>
        /// The selected session panel identifier
        /// </summary>
        public int SessionPanelId { get; set; }
        /// <summary>
        /// Gets or sets the scorable applications.
        /// </summary>
        /// <value>
        /// The scorable applications.
        /// </value>
        public List<ScorableApplicationViewModel> ScorableApplications { get; set; }
        /// <summary>
        /// List of post assignment applications
        /// </summary>
        public List<PostAssignmentApplicationViewModel> PostAssignmentApplications { get; set; }
        /// <summary>
        /// Gets or sets the pre assignment applications.
        /// </summary>
        /// <value>
        /// The pre assignment applications.
        /// </value>
        public List<PreAssignmentApplicationViewModel> PreAssignmentApplications { get; set; }

        public List<ChairApplicationViewModel> ChairApplications { get; set; }
        /// <summary>
        /// String displayed if there are no active panels
        /// </summary>
        public string NoActivePanels { get; set; }
        /// <summary>
        /// String displayed if registration for panel is not complete
        /// </summary>
        public string NotRegistratedForPanel { get; set; }
        /// <summary>
        /// Registration for the panel is not complete
        /// </summary>
        public bool NotRegistered { get; set; }
        /// <summary>
        /// Current user id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Flag to indicate if the applications for the current panel are released
        /// </summary>
        public bool IsReleased { get; set; }
        /// <summary>
        /// Flag to indicate if the applications for the current panel are in Scoring status
        /// </summary>
        public bool IsScoringStatus { get; set; }
        /// <summary>
        /// Flag to indicate if the applications for the current panel are in Post Assignment status
        /// </summary>
        public bool IsPostAssignedStatus { get; set; }
        /// <summary>
        /// Flag to indicate if there are assigned applications
        /// </summary>
        public bool HasAssignedApplications { get; private set; }
        /// <summary>
        /// Gets or sets the participant role.
        /// </summary>
        /// <value>
        /// The participant role.
        /// </value>
        public string ParticipantRole { get; private set; }
        /// <summary>
        /// Gets the type of the participant.
        /// </summary>
        /// <value>
        /// The type of the participant.
        /// </value>
        public string ParticipantType { get; private set; }
        /// <summary>
        /// Gets the phase end date.
        /// </summary>
        /// <value>
        /// The phase end date.
        /// </value>
        public string PhaseEndDate { get; private set; }
        /// <summary>
        /// Gets the name of the phase.
        /// </summary>
        /// <value>
        /// The name of the phase.
        /// </value>
        public string PhaseName { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance is online discussion phase.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is online discussion phase; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnlineDiscussionPhase
        {
            get
            {
                return PhaseName == Invariables.Labels.OnlineDiscussionPhase;
            }
        }
        /// <summary>
        /// Gets the phase title.
        /// </summary>
        /// <value>
        /// The phase title.
        /// </value>
        public string PhaseTitle
        {
            get
            {
                if (PhaseName.Equals(Invariables.Labels.OnlineDiscussionPhase, StringComparison.OrdinalIgnoreCase))
                {
                    return PhaseName;
                }
                else
                {
                    return String.Format("{0} {1}", PhaseName, Invariables.Labels.PhaseTitleTag);
                }
            }
        }
        /// <summary>
        /// Gets or sets the participant type and role.
        /// </summary>
        /// <value>
        /// The participant type and role.
        /// </value>
        public string ParticipantTypeAndRole { get; set; }

        /// <summary>
        /// Gets or sets the polling interval of the Application Scoring Grid.
        /// </summary>
        public int PollInterval { get; set; }
        #endregion

        /// <summary>
        /// Assigns the review order if missing.
        /// </summary>
        /// <param name="applications">The applications.</param>
        /// <returns></returns>
        private List<IBaseAssignmentModel> AssignReviewOrderIfMissing(List<IBaseAssignmentModel> applications)
        {
            if (!applications.Exists((x => ((IReviewerApplicationScoring)x).ReviewOrder != null)))
            {
                for(var i = 0; i < applications.Count; i++)
                {
                    ((IReviewerApplicationScoring)applications[i]).ReviewOrder = i + 1;
                }
            }
            return applications;
        }
        /// <summary>
        /// Get applications to PostAssignmentApplicationViewModel list view model
        /// </summary>
        /// <param name="postAssignmentApplications">List of IPostAssignmentModel entities</param>
        /// <returns>List of PostAssignmentApplicationViewModel view model</returns>
        private List<PostAssignmentApplicationViewModel> GetPostAssignmentApplications(List<IBaseAssignmentModel> postAssignmentApplications)
        {
            var list = new List<PostAssignmentApplicationViewModel>();
            foreach (IPostAssignmentModel application in postAssignmentApplications)
            {
                var viewModel = new PostAssignmentApplicationViewModel(application);
                list.Add(viewModel);
            }
            return list;
        }
        /// <summary>
        /// Gets the scorable applications.
        /// </summary>
        /// <param name="applications">The applications.</param>
        /// <returns></returns>
        private List<ScorableApplicationViewModel> GetScorableApplications(List<IBaseAssignmentModel> applications)
        {
            var list = new List<ScorableApplicationViewModel>();
            applications = AssignReviewOrderIfMissing(applications);
            foreach (IReviewerApplicationScoring application in applications)
            {
                var viewModel = new ScorableApplicationViewModel(application);
                list.Add(viewModel);
            }
            return list;
        }
        /// <summary>
        /// Gets the pre assignment applications.
        /// </summary>
        /// <param name="applications">The applications.</param>
        /// <returns></returns>
        private List<PreAssignmentApplicationViewModel> GetPreAssignmentApplications(List<IBaseAssignmentModel> applications)
        {
            var list = new List<PreAssignmentApplicationViewModel>();
            foreach (IPreAssignmentModel application in applications)
            {
                var viewModel = new PreAssignmentApplicationViewModel(application);
                list.Add(viewModel);
            }
            return list;
        }
        /// <summary>
        /// Get the flag to indicate if there are assigned applications
        /// </summary>
        /// <param name="postAssignmentApplications"></param>
        /// <returns>True if there are assigned application; false otherwise.</returns>
        private bool GetHasAssignedApplications(List<IBaseAssignmentModel> postAssignmentApplications) 
        {
            return postAssignmentApplications.Exists(x => ((IPostAssignmentModel)x).IsAssigned);
        }
        public string LastPageUrl { get; set; }


        private List<ChairApplicationViewModel> GetChairApplications(List<IBaseAssignmentModel> applications)
        {
            var list = new List<ChairApplicationViewModel>();
            foreach (IChairAssignmentModel application in applications)
            {
                var viewModel = new ChairApplicationViewModel(application);
                list.Add(viewModel);
            }
            return list;
        }
        /// <summary>
        /// Determines whether [has coi all been started] [the specified applications].
        /// </summary>
        /// <param name="applications">The applications.</param>
        /// <returns>
        ///   <c>true</c> if [has coi all been started] [the specified applications]; otherwise, <c>false</c>.
        /// </returns>
        private bool HasCoiAllBeenStarted(List<ChairApplicationViewModel> applications)
        {
            return !(applications.Exists(x => x.ConflictFlag == null));
        }
        /// <summary>
        /// Get Client Id
        /// </summary>
        public int ClientId;
        #endregion
    }
}