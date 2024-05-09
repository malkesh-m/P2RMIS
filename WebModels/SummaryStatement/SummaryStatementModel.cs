using System;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing a summary statement
    /// </summary>
    public class SummaryStatementModel : ISummaryStatementModel
    {
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
        public bool Priority1 { get; set; }
        /// <summary>
        /// Priority 2
        /// </summary>
        public bool Priority2 { get; set; }
        /// <summary>
        /// The application's award
        /// </summary>
        public string Award { get; set; }
        /// <summary>
        /// The score of the application
        /// </summary>
        public decimal? Score { get; set; }
        /// <summary>
        /// Data the application was posted
        /// </summary>
        public DateTime? PostedDate { get; set; }
        /// <summary>
        /// Gets or sets the available date.
        /// </summary>
        /// <value>
        /// The available date.
        /// </value>
        public DateTime? AvailableDate { get; set; }
        /// <summary>
        /// Date the application was checked out
        /// </summary>
        public DateTime? CheckoutDate { get; set; }
        /// <summary>
        /// The name of the workflow step of the application that is next to be completed
        /// </summary>
        public string CurrentStepName { get; set; }
        /// <summary>
        /// Whether or not notes currently exist for the application
        /// </summary>
        public bool NotesExist { get; set; }

        /// <summary>
        /// Whether or not admin (budget) notes currently exist for the application
        /// </summary>
        public bool AdminNotesExist { get; set; }
        /// <summary>
        /// delegate used to format the overall score
        /// </summary>
        public static OverallScoreFormatter ScoreFormatter { get; set; }
        /// <summary>
        /// Overall score formatted by specific business rules.  If no formatting delegate is defined
        /// an unformatted score is returned.
        /// </summary>
        public string FormattedScore
        {
            get { return (ScoreFormatter != null) ? ScoreFormatter(Score) : Score.ToString(); }
        }
        /// <summary>
        /// Identifier for an application
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the program abbr.
        /// </summary>
        /// <value>
        /// The program abbr.
        /// </value>
        public string ProgramAbbr { get; set; }
        /// <summary>
        /// Gets or sets the panel abbr.
        /// </summary>
        /// <value>
        /// The panel abbr.
        /// </value>
        public string PanelAbbr { get; set; }
    }
}
