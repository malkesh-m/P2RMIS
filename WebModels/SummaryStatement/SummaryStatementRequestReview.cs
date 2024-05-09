namespace Sra.P2rmis.WebModels.SummaryStatement
{
    public class SummaryStatementRequestReview : ISummaryStatementRequestReview
    {
        /// <summary>
        /// the application's unique identifier
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// the application's unique identifier (Log Number)
        /// </summary>
        public string LogNumber { get; set; }
        /// <summary>
        /// Applications workflow unique identifier
        /// </summary>
        public int? ApplicationWorkflowId { get; set; }
        /// <summary>
        /// the application's priority 1 status
        /// </summary>
        public bool Priority1 { get; set; }
        /// <summary>
        /// the application's priority 2 status
        /// </summary>
        public bool Priority2 { get; set; }
        /// <summary>
        /// the application's Award Mechanism (abbreviation)
        /// </summary>
        public string AwardMechanismAbbreviation { get; set; }
        /// <summary>
        /// the application's overall score
        /// </summary>
        public decimal? OverallScore { get; set; }
        /// <summary>
        /// the application's research topic area
        /// </summary>
        public string ResearchTopicArea { get; set; }
        /// <summary>
        /// PanelApplication entity identifier
        /// </summary>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// Default Workflow entity identifier
        /// </summary>
        public int WorkflowId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has preceding review step.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has preceding review step; otherwise, <c>false</c>.
        /// </value>
        public bool HasPrecedingReviewStep { get; set; }
        /// <summary>
        /// Gets or sets the cycle.
        /// </summary>
        /// <value>
        /// The cycle.
        /// </value>
        public int? Cycle { get; set; }
        /// <summary>
        /// Gets or sets the panel.
        /// </summary>
        /// <value>
        /// The panel.
        /// </value>
        public string Panel { get; set; }
        public int Index { get; set; }
    }
}
