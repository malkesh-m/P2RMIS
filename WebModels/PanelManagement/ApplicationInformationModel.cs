
using System;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the ListApplicationInformation requests.
    /// </summary>
    public class ApplicationInformationModel : IApplicationInformationModel
    {
        /// <summary>
        /// unique identifier for the application
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        /// Applications log number
        /// </summary>
        public string LogNumber { get; set; }

        /// <summary>
        /// The application's principal investigator first name
        /// </summary>
        public string PiFirstName { get; set; }

        /// <summary>
        /// The application's principal investigator last name
        /// </summary>
        public string PiLastName { get; set; }

        /// <summary>
        /// The mechanism abbreviation of the application
        /// </summary>
        public string AwardMechanism { get; set; }

        /// <summary>
        /// Title of the application
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Principal investigator's institution or organization
        /// </summary>
        public string PiOrganization { get; set; }

        /// <summary>
        /// Current assigned panel name
        /// </summary>
        public string PanelName { get; set; }

        /// <summary>
        /// Abbreviated panel name
        /// </summary>
        public string PanelAbbreviation { get; set; }

        /// <summary>
        /// PanelApplication entity identifier
        /// </summary>
        public int PanelApplicationId { get; set; }

        /// <summary>
        /// SessionPanel identifier
        /// </summary>
        public int SessionPanelId { get; set; }

        /// <summary>
        /// The date the panel begins
        /// </summary>
        public DateTime PanelStartDate { get; set; }

        /// <summary>
        /// The date the panel ends
        /// </summary>
        public DateTime PanelEndDate { get; set; }

        /// <summary>
        /// Whether the application has completed the in-person meeting and started scoring
        /// </summary>
        public bool ReviewDiscussionComplete { get; set; }

        /// <summary>
        /// Whether the application is blinded
        /// </summary>
        public bool Blinded { get; set; }

        /// <summary>
        /// Fiscal year
        /// </summary>
        public string FiscalYear { get; set; }

        /// <summary>
        /// Program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the has summary text.
        /// </summary>
        /// <value>
        /// The has summary text.
        /// </value>
        public bool? HasSummaryText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is summary started.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is summary started; otherwise, <c>false</c>.
        /// </value>
        public bool IsSummaryStarted { get; set; }
        /// <summary>
        /// Gets or sets the client program identifier.
        /// </summary>
        /// <value>
        /// The client program identifier.
        /// </value>
        public int ClientProgramId { get; set; }
        /// <summary>
        /// Gets or sets the program mechanism identifier.
        /// </summary>
        /// <value>
        /// The program mechanism identifier.
        /// </value>
        public int ProgramMechanismId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has assigned reviewers.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has assigned reviewers; otherwise, <c>false</c>.
        /// </value>
        public bool HasAssignedReviewers { get; set; }
        /// <summary>
        /// add or edit admin note
        /// </summary>
        public bool HasAdminNotes { get; set; }
        public string AddOrEditAdminNote { get; set; }
    }
}
