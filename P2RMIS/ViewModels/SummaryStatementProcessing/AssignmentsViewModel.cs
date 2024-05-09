using System;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.ViewModels.SummaryStatement;

namespace Sra.P2rmis.Web.UI.Models
{
    public class AssignmentsViewModel : SsTabMenuViewModel
    {
        #region Constructor
        /// <summary>
        /// The view model for my assignments page in summary statement processing
        /// </summary
        public AssignmentsViewModel()
            : base()
        {
            // Set property equal to the list
            this.Statements = new List<AssignmentViewModel>();
        }

        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets the refresh time.
        /// </summary>
        /// <value>
        /// The refresh time.
        /// </value>
        public string RefreshTime { get; set; }
        /// <summary>
        /// Whether the current user has a Manage permission
        /// </summary>
        public bool HasManagePermission { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can view history.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can view history; otherwise, <c>false</c>.
        /// </value>
        public bool CanViewHistory { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is web based.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is web based; otherwise, <c>false</c>.
        /// </value>
        public bool IsWebBased { get; set; }
        /// <summary>
        /// List of assignments on the my assignments page.
        /// </summary>
        public List<AssignmentViewModel> Statements { get; set; }
        #endregion
    }

    public class AssignmentViewModel
    {
        public AssignmentViewModel(ISummaryAssignedModel assignment)
        {
            CheckedoutUser = ViewHelpers.ConstructName(assignment.CheckedoutUserLastName,
                assignment.CheckedoutUserFirstName);
            CheckedoutUserId = assignment.CheckedoutUserId;
            ClientProgramId = assignment.ClientProgramId;
            if (assignment as SummaryAssignedModel != null)
            {
                Program = ((SummaryAssignedModel)assignment).ProgramAbbr;
                Panel = ((SummaryAssignedModel)assignment).PanelAbbr;
                Award = ((SummaryAssignedModel)assignment).Award;
                NotesExist = ((SummaryAssignedModel)assignment).NotesExist;
                AdminNotesExist = assignment.AdminNotesExist;
                Priority1 = ViewHelpers.FormatBoolean(assignment.Priority1);
                Priority2 = ViewHelpers.FormatBoolean(assignment.Priority2);
                SummaryAssignedModel.ScoreFormatter = ViewHelpers.ScoreFormatter;
                Score = ((SummaryAssignedModel)assignment).FormattedScore;
                PhaseName = ((SummaryAssignedModel)assignment).CurrentStepName;
                PostedDate = ViewHelpers.FormatDate(((SummaryAssignedModel)assignment).PostedDate);
                CheckoutDate = ViewHelpers.FormatDate(((SummaryAssignedModel)assignment).CheckoutDate);
                AvailableDate = ViewHelpers.FormatDate(((SummaryAssignedModel)assignment).AvailableDate);
                ApplicationWorkflowId = ((SummaryAssignedModel)assignment).ApplicationWorkflowId;
                ApplicationWorkflowStepId = ((SummaryAssignedModel)assignment).ApplicationWorkflowStepId;
                LogNumber = ((SummaryAssignedModel)assignment).LogNumber;
                PanelApplicationId = ((SummaryAssignedModel)assignment).PanelApplicationId;
                IsClientReviewStepType = ((SummaryAssignedModel)assignment).IsClientReviewStepType;
                IsEditingStepType = ((SummaryAssignedModel)assignment).IsEditingStepType;
                IsWritingStepType = ((SummaryAssignedModel)assignment).IsWritingStepType;
                ApplicationId = assignment.ApplicationId;
            }
        }
        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int Index { get; set; }
        /// <summary>
        /// Gets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        public string Program { get; private set; }
        /// <summary>
        /// Gets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        public string Panel { get; private set; }
        /// <summary>
        /// Unique identifier for an application workflow
        /// </summary>
        public int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// Gets or sets the application workflow step identifier.
        /// </summary>
        /// <value>
        /// The application workflow step identifier.
        /// </value>
        public int ApplicationWorkflowStepId { get; set; }
        /// <summary>
        /// The name for an application's assigned workflow 
        /// </summary>
        public string WorkflowName { get; set; }
        /// <summary>
        /// Application's identifier for client 
        /// </summary>
        public string LogNumber { get; set; }
        /// <summary>
        /// Panel application identifier
        /// </summary>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// Date workflow step was last assigned to user
        /// </summary>
        public DateTime AssignmentDate { get; set; }
        /// <summary>
        /// Date user first began work on workflow step 
        /// </summary>
        public DateTime? WorkStartDate { get; set; }
        /// <summary>
        /// Date user last checked-in work on workflow step 
        /// </summary>
        public DateTime? WorkEndDate { get; set; }
        /// <summary>
        /// Priority 1
        /// </summary>
        public string Priority1 { get; set; }
        /// <summary>
        /// Priority 2
        /// </summary>
        public string Priority2 { get; set; }
        /// <summary>
        /// The application's award
        /// </summary>
        public string Award { get; set; }
        /// <summary>
        /// The score of the application
        /// </summary>
        public string Score { get; set; }
        /// <summary>
        /// Data the application was posted
        /// </summary>
        public string PostedDate { get; set; }
        /// <summary>
        /// Gets or sets the available date.
        /// </summary>
        /// <value>
        /// The available date.
        /// </value>
        public string AvailableDate { get; set; }
        /// <summary>
        /// Date the application was checked out
        /// </summary>
        public string CheckoutDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the phase.
        /// </summary>
        /// <value>
        /// The name of the phase.
        /// </value>
        public string PhaseName { get; set; }
        /// <summary>
        /// Whether or not summary notes currently exist for the application
        /// </summary>
        public bool NotesExist { get; set; }
        /// <summary>
        /// Whether or not admin (budget) notes currently exist for the application
        /// </summary>
        public bool AdminNotesExist { get; set; }
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the checked out user.
        /// </summary>
        /// <value>
        /// The checked out user.
        /// </value>
        public string CheckedoutUser { get; set; }
        /// <summary>
        /// Checked out user Last Name
        /// </summary>
        public int CheckedoutUserId { get; set; }
        /// <summary>
        /// Gets or sets the client program identifier.
        /// </summary>
        /// <value>
        /// The client program identifier.
        /// </value>
        public int ClientProgramId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is client.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is client; otherwise, <c>false</c>.
        /// </value>
        public bool IsClient { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is client review step type.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is client review step type; otherwise, <c>false</c>.
        /// </value>
        public bool IsClientReviewStepType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is editing step type.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is editing step type; otherwise, <c>false</c>.
        /// </value>
        public bool IsEditingStepType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is writing step type.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is writing step type; otherwise, <c>false</c>.
        /// </value>
        public bool IsWritingStepType { get; set; }
    }
}