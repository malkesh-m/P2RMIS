namespace Sra.P2rmis.WebModels.SummaryStatement
{
    public interface ISummaryStatementRequestReview
    {
        /// <summary>
        /// the application's unique identifier
        /// </summary>
        int ApplicationId { get; set; }     
        /// <summary>
        /// the application's unique identifier (Log Number)
        /// </summary>
        string LogNumber { get; set; }       
        /// <summary>
        /// Applications workflow unique identifier
        /// </summary>
        int? ApplicationWorkflowId { get; set; }
        /// <summary>
        /// the application's priority 1 status
        /// </summary>
        bool Priority1 { get; set; }
        /// <summary>
        /// the application's priority 2 status
        /// </summary>
        bool Priority2 { get; set; }
        /// <summary>
        /// the application's Award Mechanism (abbreviation)
        /// </summary>
        string AwardMechanismAbbreviation { get; set; }
        /// <summary>
        /// the application's overall score
        /// </summary>
        decimal? OverallScore { get; set; }
        /// <summary>
        /// the application's research topic area
        /// </summary>
        string ResearchTopicArea { get; set; }
        /// <summary>
        /// PanelApplication entity identifier
        /// </summary>
        int PanelApplicationId { get; set; }
        /// <summary>
        /// Default Workflow entity identifier
        /// </summary>
        int WorkflowId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has preceding review step.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has preceding review step; otherwise, <c>false</c>.
        /// </value>
        bool HasPrecedingReviewStep { get; set; }
        /// <summary>
        /// Gets or sets the cycle.
        /// </summary>
        /// <value>
        /// The cycle.
        /// </value>
        int? Cycle { get; set; }
        /// <summary>
        /// Gets or sets the panel.
        /// </summary>
        /// <value>
        /// The panel.
        /// </value>
        string Panel { get; set; }
        int Index { get; set; }
    }
}
