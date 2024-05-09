using System.Collections.Generic;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ReviewerEvaluationViewModel : PanelManagementViewModel
    {
        #region Constants

        /// <summary>
        /// Constants for the reviewer evaluation View Model
        /// </summary>
        public class FormValues
        {
            public const string PanelUserAssignment = "PanelUserAssignment";
            public const string Rating = "ScoreRating";
            public const string PotentialChairFlag = "PotentialChairFlag";
            public const string RatingComments = "CommentsRating";
            public const string ReviewerEvalId = "ReviewerEvalId";
            public const string IsChair = "IsChair";
        }

        #endregion

        #region Constructor & Setup
        /// <summary>
        /// Constructor - initialize so model is usable.
        /// </summary>
        public ReviewerEvaluationViewModel()
        {
            this.RatingGuidance = new SortedDictionary<int, List<IRatingGuidance>>();
            this.ReviewerEvaluation = new List<IReviewerEvaluation>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Reviewer evaluation rating guidance
        /// </summary>
        public SortedDictionary<int, List<IRatingGuidance>> RatingGuidance { get; set; }
        /// <summary>
        /// The reviewer evaluations object
        /// </summary>
        public List<IReviewerEvaluation> ReviewerEvaluation { get; set; }
        /// <summary>
        /// the id of the current user
        /// </summary>
        public int CurrentUser { get; set; }

        #endregion
    }
}