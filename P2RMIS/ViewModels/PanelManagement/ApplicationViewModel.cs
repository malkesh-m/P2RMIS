using System;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.ViewModels.PanelManagement
{
    public class ApplicationViewModel
    {
        public ApplicationViewModel(IApplicationInformationModel model, bool canManagePanelApplication)
        {
            ApplicationId = model.ApplicationId;
            LogNumber = model.LogNumber;
            PiName = ViewHelpers.ConstructName(model.PiLastName, model.PiFirstName);
            AwardMechanism = model.AwardMechanism;
            Title = model.Title;
            PiOrganization = model.PiOrganization;
            PanelName = model.PanelName;
            IsSummaryStarted = model.IsSummaryStarted;
            HasSummaryText = model.HasSummaryText ?? false;
            HasAssignedReviewers = model.HasAssignedReviewers;
            PanelApplicationId = model.PanelApplicationId;
            FiscalYear = model.FiscalYear;
            ProgramAbbreviation = model.ProgramAbbreviation;
            PanelAbbreviation = model.PanelAbbreviation;
            CanRemoveApplication = canManagePanelApplication;
            CanTransferApplication = canManagePanelApplication;
            HasAdminNotes = model.HasAdminNotes;
            AddOrEditAdminNote = (model.HasAdminNotes == false)? "Add":"Edit";
        }

        /// <summary>
        /// unique identifier for the application
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        /// Applications log number
        /// </summary>
        public string LogNumber { get; set; }

        /// <summary>
        /// The application's principal investigator name
        /// </summary>
        public string PiName { get; set; }

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
        /// Whether the application has completed the in-person meeting and started scoring
        /// </summary>
        public bool ReviewDiscussionComplete { get; set; }

        /// <summary>
        /// Fiscal year
        /// </summary>
        public string FiscalYear { get; set; }

        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the has summary text.
        /// </summary>
        /// <value>
        /// The has summary text.
        /// </value>
        public bool HasSummaryText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is summary started.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is summary started; otherwise, <c>false</c>.
        /// </value>
        public bool IsSummaryStarted { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has assigned reviewers.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has assigned reviewers; otherwise, <c>false</c>.
        /// </value>
        public bool HasAssignedReviewers { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can remove application.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can remove application; otherwise, <c>false</c>.
        /// </value>
        public bool CanRemoveApplication { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can transfer application.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can transfer application; otherwise, <c>false</c>.
        /// </value>
        public bool CanTransferApplication { get; set; }
        /// <summary>
        /// add or edit admin notes
        /// </summary>
        public bool HasAdminNotes { get; set; }
        public string AddOrEditAdminNote { get; set; }
    }
}