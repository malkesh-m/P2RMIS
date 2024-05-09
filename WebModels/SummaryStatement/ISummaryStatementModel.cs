using System;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing a summary statement
    /// </summary>
    public interface ISummaryStatementModel
    {
        /// <summary>
        /// Unique identifier for an application workflow
        /// </summary>
        int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// Gets or sets the application workflow step identifier.
        /// </summary>
        /// <value>
        /// The application workflow step identifier.
        /// </value>
        int ApplicationWorkflowStepId { get; set; }
        /// <summary>
        /// Identifier for an application
        /// </summary>
        int ApplicationId { get; set; }
        /// <summary>
        /// The name for an application's assigned workflow 
        /// </summary>
        string WorkflowName { get; set; }
        /// <summary>
        /// Application's identifier for client 
        /// </summary>
        string LogNumber { get; set; }
        /// <summary>
        /// Panel application identifier
        /// </summary>
        int PanelApplicationId { get; set; }
        /// <summary>
        /// Date workflow step was last assigned to user
        /// </summary>
        DateTime AssignmentDate { get; set; }
        /// <summary>
        /// Date user first began work on workflow step 
        /// </summary>
        DateTime? WorkStartDate { get; set; }
        /// <summary>
        /// Date user last checked-in work on workflow step 
        /// </summary>
        DateTime? WorkEndDate { get; set; }
        /// <summary>
        /// Priority 1
        /// </summary>
        bool Priority1 { get; set; }
        /// <summary>
        /// Priority 2
        /// </summary>
        bool Priority2 { get; set; }
        /// <summary>
        /// The application's award
        /// </summary>
        string Award { get; set; }
        /// <summary>
        /// The score of the application
        /// </summary>
        decimal? Score { get; set; }
        /// <summary>
        /// Data the application was posted
        /// </summary>
        DateTime? PostedDate { get; set; }
        /// <summary>
        /// Date the application was checked out
        /// </summary>
        DateTime? CheckoutDate { get; set; }
        /// <summary>
        /// The name of the workflow step of the application that is next to be completed
        /// </summary>
        string CurrentStepName { get; set; }
        /// <summary>
        /// Whether or not notes currently exist for the application
        /// </summary>
        bool NotesExist { get; set; }
        /// <summary>
        /// Whether or not admin (budget) notes currently exist for the application
        /// </summary>
        bool AdminNotesExist { get; set; }
        /// <summary>
        /// Overall score formatted by specific business rules.  If no formatting delegate is defined
        /// an unformatted score is returned.
        /// </summary>
        string FormattedScore { get; }
        /// <summary>
        /// Gets or sets the program abbr.
        /// </summary>
        /// <value>
        /// The program abbr.
        /// </value>
        string ProgramAbbr { get; set; }
        /// <summary>
        /// Gets or sets the panel abbr.
        /// </summary>
        /// <value>
        /// The panel abbr.
        /// </value>
        string PanelAbbr { get; set; }
    }
}
