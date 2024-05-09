
using System;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the ListApplicationInformation requests.
    /// </summary>
    public interface IApplicationInformationModel
    {
        /// <summary>
        /// unique identifier for the application
        /// </summary>
        int ApplicationId { get; set; }
        /// <summary>
        /// Applications log number
        /// </summary>
        string LogNumber { get; set; }
        /// <summary>
        /// The application's principal investigator first name
        /// </summary>
        string PiFirstName { get; set; }
        /// <summary>
        /// The application's principal investigator last name
        /// </summary>
        string PiLastName { get; set; }
        /// <summary>
        /// The mechanism abbreviation of the application
        /// </summary>
        string AwardMechanism { get; set; }
        /// <summary>
        /// Title of the application
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// Principal investigator's institution or organization
        /// </summary>
        string PiOrganization { get; set; }
        /// <summary>
        /// Current assigned panel name
        /// </summary>
        string PanelName { get; set; }
        /// <summary>
        /// Abbreviated panel name
        /// </summary>
        string PanelAbbreviation { get; set; }
        /// <summary>
        /// PanelApplication entity identifier
        /// </summary>
        int PanelApplicationId { get; set; }
        /// <summary>
        /// SessionPanel identifier
        /// </summary>
        int SessionPanelId { get; set; }
        /// <summary>
        /// The date the panel begins
        /// </summary>
        DateTime PanelStartDate { get; set; }
        /// <summary>
        /// The date the panel ends
        /// </summary>
        DateTime PanelEndDate { get; set; }
        /// <summary>
        /// Whether the review has been completed
        /// </summary>
        bool ReviewDiscussionComplete { get; set; }

        /// <summary>
        /// Whether the application is blinded
        /// </summary>
        bool Blinded { get; set; }

        /// <summary>
        /// Fiscal year
        /// </summary>
        string FiscalYear { get; set; }

        /// <summary>
        /// Program abbreviation
        /// </summary>
        string ProgramAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the has summary text.
        /// </summary>
        /// <value>
        /// The has summary text.
        /// </value>
        bool? HasSummaryText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is summary started.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is summary started; otherwise, <c>false</c>.
        /// </value>
        bool IsSummaryStarted { get; set; }
        /// <summary>
        /// Gets or sets the client program identifier.
        /// </summary>
        /// <value>
        /// The client program identifier.
        /// </value>
        int ClientProgramId { get; set; }
        /// <summary>
        /// Gets or sets the program mechanism identifier.
        /// </summary>
        /// <value>
        /// The program mechanism identifier.
        /// </value>
        int ProgramMechanismId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has assigned reviewers.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has assigned reviewers; otherwise, <c>false</c>.
        /// </value>
        bool HasAssignedReviewers { get; set; }
        /// <summary>
        /// add or edit admin notes
        /// </summary>
        bool HasAdminNotes { get; set; }
        string AddOrEditAdminNote { get; set; }
    }
}
