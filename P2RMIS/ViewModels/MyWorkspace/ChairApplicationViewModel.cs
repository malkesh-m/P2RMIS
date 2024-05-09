using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.ApplicationScoring;

namespace Sra.P2rmis.Web.ViewModels.MyWorkspace
{
    public class ChairApplicationViewModel
    {
        public ChairApplicationViewModel()
        {
        }

        public ChairApplicationViewModel(IChairAssignmentModel chairModel)
        {
            ApplicationLogNumber = chairModel.ApplicationLogNumber;
            PIFirstName = chairModel.PIFirstName;
            PILastName = chairModel.PILastName;
            Title = chairModel.Title;
            Mechanism = chairModel.Mechanism;
            ExpertiseLevel = chairModel.ExpertiseLevel;
            IsSummaryStarted = chairModel.IsSummaryStarted;
            PanelApplicationId = chairModel.PanelApplicationId;
            ConflictFlag = chairModel.ConflictFlag;
            PIOrganization = chairModel.PIOrganization;
            HasSummaryText = chairModel.HasSummaryText;
            PanelUserAssignmentId = (int)chairModel.PanelUserAssignmentId;
            SessionPanelId = chairModel.SessionPanelId;
            SessionPanelAbbreviation = chairModel.SessionPanelAbbreviation;
            ApplicationId = chairModel.ApplicationId;
            SessionPanelName = chairModel.SessionPanelName;
        }
        /// <summary>
        /// Application Id
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Panel user assignment Id
        /// </summary>
        public int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Application log number
        /// </summary>
        public string ApplicationLogNumber { get; set; }
        /// <summary>
        /// Application title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Principal investigator first name
        /// </summary>
        public string PIFirstName { get; set; }
        /// <summary>
        /// Principal investigator last name
        /// </summary>
        public string PILastName { get; set; }
        /// <summary>
        /// Mechanism
        /// </summary>
        public string Mechanism { get; set; }
        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// User's experience level
        /// </summary>
        public string ExpertiseLevel { get; private set; }
        /// <summary>
        /// Conflict flag (True if a COI; False if not)
        /// </summary>
        public bool? ConflictFlag { get; private set; }
        /// <summary>
        /// The organization
        /// </summary>
        public string PIOrganization { get; private set; }
        // <summary>
        /// Gets a value indicating whether this instance is summary started.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is summary started; otherwise, <c>false</c>.
        /// </value>
        public bool IsSummaryStarted { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance has summary text.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has summary text; otherwise, <c>false</c>.
        /// </value>
        public bool HasSummaryText { get; private set; }
        /// <summary>
        /// Indicates if the reviewer is a chairperson
        /// </summary>
        public bool IsChairPerson { get; private set; }
        /// <summary>
        /// Gets the session panel identifier.
        /// </summary>
        /// <value>
        /// The session panel identifier.
        /// </value>
        public int SessionPanelId { get; private set; }
        /// <summary>
        /// Gets the abbreviation of the session panel.
        /// </summary>
        /// <value>
        /// The abbreviation of the session panel.
        /// </value>
        public string SessionPanelAbbreviation { get; private set; }
        /// Gets the abbreviation of the session panel.
        /// </summary>
        /// <value>
        /// The name of the session panel.
        /// </value>
        public string SessionPanelName { get; private set; }
    }
}