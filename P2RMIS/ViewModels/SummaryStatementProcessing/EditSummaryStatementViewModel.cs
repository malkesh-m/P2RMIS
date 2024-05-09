using System.Collections.Generic;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for SSM-600 Summary Statement
    /// </summary>
    public class EditSummaryStatementViewModel : ProcessingTabsViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EditSummaryStatementViewModel() : base ()
        {
            this.TheWorkflow = new List<ApplicationWorkflowStepModel>();
            this.ApplicationDetails = new ApplicationDetailModel();
            this.Criteria = new Dictionary<string, List<IStepContentModel>>();
        }

        /// <summary>
        /// the workflow of the application
        /// </summary>
        public IEnumerable<ApplicationWorkflowStepModel> TheWorkflow { get; set; }
        /// <summary>
        /// the workflows application details
        /// </summary>
        public IApplicationDetailModel ApplicationDetails { get; set; }
        /// <summary>
        /// the scored criteria step content
        /// </summary>
        public IDictionary<string, List<IStepContentModel>> Criteria { get; set; }
        /// <summary>
        /// the active workflow step
        /// </summary>
        public int ActiveWorkflowStep { get; set; }
        /// <summary>
        /// whether the current user has a Manage permission
        /// </summary>
        public bool HasManagePermission { get; set; }
        /// <summary>
        /// Whether user can view editor comments
        /// </summary>
        public bool CanViewEditorComments { get; set; }
        /// <summary>
        /// Whether user can modify general comments
        /// </summary>
        public bool CanModifyGeneralComments { get; set; }
        /// <summary>
        /// Whether user can accept track changes
        /// </summary>
        public bool CanAcceptTrackChanges { get; set; }
        /// <summary>
        /// Whether user can view all changes
        /// </summary>
        public bool CanViewAllChanges { get; set; }
        /// <summary>
        /// the previous workflow steps
        /// </summary>
        public List<KeyValuePair<int, string>> PreviousWorkflowSteps { get; set; }
        /// <summary>
        /// the username of the checkout user
        /// </summary>
        public string CheckoutUsername { get; set; }
        /// <summary>
        /// the name of the checkout user
        /// </summary>
        public string CheckoutName { get; set; }
        /// <summary>
        /// the identifier of the checkout user
        /// </summary>
        public int CheckoutUserId { get; set; }
        /// <summary>
        /// The Overview criteria step content
        /// </summary>
        public IDictionary<string, List<IStepContentModel>> OverallCriteria { get; set; }
        /// <summary>
        /// The Scored criteria step content
        /// </summary>
        public IDictionary<string, List<IStepContentModel>> ScoredCriteria { get; set; }
        /// <summary>
        /// The UnScored criteria step content
        /// </summary>
        public IDictionary<string, List<IStepContentModel>> UnScoredCriteria { get; set; }
        /// <summary>
        /// Data for the average score table
        /// </summary>
        public SummaryAverageScoreModel AverageScoreTableData { get; set; }
        /// <summary>
        /// Data for the individual score table
        /// </summary>
        public IList<SummaryIndividualScoreModel> IndividualScoreTableData { get; set; }
        /// <summary>
        /// Whether the average score table should be displayed. Otherwise individual score table displays.
        /// </summary>
        public bool DoDisplayAverageScoreTable { get; set; }
        /// <summary>
        /// Maximum length of in line comments.
        /// </summary>
        public int InlineCommentMaximum { get; set; }
        /// <summary>
        /// Maximum length of in general comments.
        /// </summary>
        public int GeneralCommentMaximum { get; set; }
        /// <summary>
        /// Indicates if the user can check the summary statement into any phase.
        /// </summary>
        public bool CanCheckIntoAnyPhase { get; set; }
        /// <summary>
        /// Gets or sets the admin note.
        /// </summary>
        /// <value>
        /// The admin note.
        /// </value>
        public string AdminNote { get; set; }
        #region Helpers
        /// <summary>
        /// Sets permissions on editor form for comment components
        /// </summary>
        /// <param name="isReviewContext"></param>
        public void SetCommentPermissions (bool isReviewContext)
        {
            //current rule is that the review context does not have the ability to 
            //view editor comments and does have ability to modify general comments 
            this.CanViewEditorComments = !isReviewContext;
            this.CanModifyGeneralComments = isReviewContext;
            this.CanViewAllChanges = !isReviewContext;
        }
        /// <summary>
        /// Set the indicator determining if a modal is displayed upon submit.
        /// </summary>
        /// <param name="value">Indicator vlaue</param>
        internal void SetCheckinDidplayIndicator(bool value)
        {
            this.CanCheckIntoAnyPhase = value;
        }
        #endregion
    }
}