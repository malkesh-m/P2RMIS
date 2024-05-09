using System;
using System.Collections.Generic;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Bll.Views.SessionPanelDetails;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Web.UI.Models;

namespace Sra.P2rmis.Web.ViewModels.Shared
{
    public interface IManageApplicationScoringViewModel
    {
        /// <summary>
        /// Programs
        /// </summary>
        List<Program> Programs { get; set; }
        /// <summary>
        /// Meeting sessions
        /// </summary>
        List<SessionView> Sessions { get; set; }
        /// <summary>
        /// Panels
        /// </summary>
        List<PanelView> Panels { get; set; }
        /// <summary>
        /// Collection of application details for a specified panel
        /// </summary>
        List<ViewPanelDetails> PanelDetails { get; set; }
        /// <summary>
        /// Collection of mechanism application count information
        /// </summary>
        List<ApplicationCount> MechanismCounts { get; set; }
        /// <summary>
        /// Selected program identifier
        /// </summary>
        int? SelectedProgramId { get; set; }
        /// <summary>
        /// Selected meeting session identifier
        /// </summary>
        int? SelectedSessionId { get; set; }
        /// <summary>
        /// Selected panel identifier
        /// </summary>
        int? SelectedPanelId { get; set; }
        /// <summary>
        /// Whether the current user has permission of View Online Scoring Assigned Panels
        /// </summary>
        bool CanViewAssignedPanels { get; set; }
        /// <summary>
        /// Whether the current user has permission of View Online Scoring All Panels
        /// </summary>
        bool CanViewAllPanels { get; set; }
        /// <summary>
        /// Whether the current user has permission to view scoreboard
        /// </summary>
        bool CanViewScoreboard { get; set; }
        /// <summary>
        /// Whether the current user has permission to edit score status
        /// </summary>
        bool CanEditScoreStatus { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can access admin note.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can access admin note; otherwise, <c>false</c>.
        /// </value>
        bool CanAccessAdminNote { get; set; }
        /// <summary>
        /// Polling interval (only used by ApplicationScoring grid)
        /// </summary>
        int PollInterval { get; set; }
        /// <summary>
        /// Indicates if the Applications's status link should be enabled. (Scoring; Active etc.)
        /// </summary>
        bool CanShowLinks { get; }
    }
}
