using System;
using System.Collections.Generic;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Bll.Views.SessionPanelDetails;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.Web.ViewModels.Shared;

namespace Sra.P2rmis.Web.ViewModels.MyWorkspace
{
    public class ManageApplicationScoringViewModel : MyWorkspaceTabsViewModel, IManageApplicationScoringViewModel
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ManageApplicationScoringViewModel() : base()
        {            
            this.PollInterval = ConfigManager.ManageApplicationScoringPollingInterval;
            this.Programs = new List<Program>();
            this.Sessions = new List<SessionView>();
            this.Panels = new List<PanelView>();
            this.MechanismCounts = new List<ApplicationCount>();
            this.PanelDetails = new List<ViewPanelDetails>();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theApplicationBlView">The application management view</param>
        public ManageApplicationScoringViewModel(ApplicationManagementView theApplicationBlView) : this()
        {
            this.Programs = new List<Application>(theApplicationBlView.Applications).ConvertAll(new Converter<Application, Program>(ApplicationToProgram));     
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="thePanels">The panels</param>
        public ManageApplicationScoringViewModel(List<IListEntry> thePanels) : this()
        {
            this.Panels = new List<IListEntry>(thePanels).ConvertAll(new Converter<IListEntry, PanelView>(IListEntryToPanelView));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Programs
        /// </summary>
        public List<Program> Programs { get; set; }
        /// <summary>
        /// Meeting sessions
        /// </summary>
        public List<SessionView> Sessions { get; set; }
        /// <summary>
        /// Panels
        /// </summary>
        public List<PanelView> Panels { get; set; }
        /// <summary>
        /// Collection of application details for a specified panel
        /// </summary>
        public List<ViewPanelDetails> PanelDetails { get; set; }
        /// <summary>
        /// Collection of mechanism application count information
        /// </summary>
        public List<ApplicationCount> MechanismCounts { get; set; }
        /// <summary>
        /// Selected program identifier
        /// </summary>
        public int? SelectedProgramId { get; set; }
        /// <summary>
        /// Selected meeting session identifier
        /// </summary>
        public int? SelectedSessionId { get; set; }
        /// <summary>
        /// Selected panel identifier
        /// </summary>
        public int? SelectedPanelId { get; set; }
        /// <summary>
        /// Whether the current user has permission of View Online Scoring Assigned Panels
        /// </summary>
        public bool CanViewAssignedPanels { get; set; }
        /// <summary>
        /// Whether the current user has permission of View Online Scoring All Panels
        /// </summary>
        public bool CanViewAllPanels { get; set; }
        /// <summary>
        /// Whether the current user has permission to view scoreboard
        /// </summary>
        public bool CanViewScoreboard { get; set; }
        /// <summary>
        /// Whether the current user has permission to edit score status
        /// </summary>
        public bool CanEditScoreStatus { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can access admin note.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can access admin note; otherwise, <c>false</c>.
        /// </value>
        public bool CanAccessAdminNote { get; set; }
        /// <summary>
        /// Polling interval (only used by ApplicationScoring grid)
        /// </summary>
        public int PollInterval { get; set; }
        /// <summary>
        /// Indicates if the Applications's status link should be enabled. (Scoring; Active etc.)
        /// </summary>
        public bool CanShowLinks { get; internal set; }
        #endregion
        #region Helpers
        /// <summary>
        /// Converts a business layer SessionView object into
        /// a presentation layer SessionDetail object.
        /// </summary>
        /// <param name="theApplication">Business layer Application view</param>
        /// <returns>Program object created from Application object</returns>
        private static Program ApplicationToProgram(Application theApplication)
        {
            return new Program(theApplication);
        }
        /// <summary>
        /// Converts a list entry to a panelView object
        /// </summary>
        /// <param name="thePanelEntry">The list entry for the panel</param>
        /// <returns>The panelView object</returns>
        /// <remarks>TODO: refactoring needed for the PanelView</remarks>
        private static PanelView IListEntryToPanelView(IListEntry thePanelEntry)
        {
            var pv = new PanelView();
            pv.PanelId = thePanelEntry.Index;
            pv.PanelAbbreviation = thePanelEntry.Value;
            return pv;
        }
        /// <summary>
        /// Set the selections
        /// </summary>
        /// <param name="selecteProgramId">Selected program identifier</param>
        /// <param name="selectedSessionId">Selected session identifier</param>
        /// <param name="selectedPanelId">SelectedPanel identifier</param>
        public void SetSelectionIdentifiers(int? selecteProgramId, int? selectedSessionId, int? selectedPanelId)
        {
            this.SelectedProgramId = selecteProgramId;
            this.SelectedSessionId = selectedSessionId;
            this.SelectedPanelId = selectedPanelId;
        }

        public void AppendTabs(List<IBaseAssignmentModel> applications)
        {
            if (!(applications.Exists((x => ((IChairAssignmentModel)x).ConflictFlag == null))))
            {
                AppendCpritChairTabs();
            }
        }
        #endregion
    }
}