
namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Model representing an applications evaluation criteria
    /// </summary>
    public interface ICriteriaReviewerScoreModel
    {
        /// <summary>
        /// Gets or sets the name of the criteria.
        /// </summary>
        /// <value>
        /// The name of the criteria.
        /// </value>
        string CriteriaName { get; set; }
        /// <summary>
        /// Gets or sets the criteria sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        int CriteriaSortOrder { get; set; }
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the step type identifier.
        /// </summary>
        /// <value>
        /// The step type identifier.
        /// </value>
        int StepTypeId { get; set; }
        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        string Score { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the criteria is overall.
        /// </summary>
        /// <value>
        ///   true if overall; otherwise, false.
        /// </value>
        bool OverallFlag { get; set; }

        /// <summary>
        /// Gets or sets the application workflow step identifier.
        /// </summary>
        /// <value>
        /// The application workflow step identifier.
        /// </value>
        int ApplicationWorkflowStepId { get; set; }
    }

    /// <summary>
    /// Model representing an applications evaluation criteria
    /// </summary>
    public class CriteriaReviewerScoreModel : ICriteriaReviewerScoreModel
    {
        /// <summary>
        /// Gets or sets the name of the criteria.
        /// </summary>
        /// <value>
        /// The name of the criteria.
        /// </value>
        public string CriteriaName { get; set; }
        /// <summary>
        /// Gets or sets the criteria sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public int CriteriaSortOrder { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the criteria is overall.
        /// </summary>
        /// <value>
        ///   true if overall; otherwise, false.
        /// </value>
        public bool OverallFlag { get; set; }
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public int ReviewerSortOrder { get; set; }
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the step type identifier.
        /// </summary>
        /// <value>
        /// The step type identifier.
        /// </value>
        public int StepTypeId { get; set; }
        /// <summary>
        /// Gets or sets the phase order.
        /// </summary>
        /// <value>
        /// The phase order.
        /// </value>
        public int PhaseOrder { get; set; }
        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public string Score { get; set; }

        /// <summary>
        /// Gets or sets the application workflow step identifier.
        /// </summary>
        /// <value>
        /// The application workflow step identifier.
        /// </value>
        public int ApplicationWorkflowStepId { get; set; }
    }
}
