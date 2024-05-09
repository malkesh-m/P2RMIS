using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Views.Scoreboard
{
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public class ScoreboardLine : IScoreboardLine
    {
        #region Constants
        /// <summary>
        /// Class constants
        /// </summary>
        private class Constants
        {
            public const decimal DefaultPreMeetingScore = 0;
            public const decimal DefaultMeetingScore = 0;
            public const short DefaultCriteriaOrder = -1;
            public const int DefaultPartId = 0;
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private ScoreboardLine() { }
        /// <summary>
        /// Construct & initialize the scoreboard line from a data layer ReviewerCritiques_Result object.
        /// </summary>
        /// <param name="item">Data layer ReviewerCritiques_Result object</param>
        internal ScoreboardLine(ReviewerCritiques_Result item)
        {
            this._preMeetingScore = item.PMScore.GetValueOrDefault(Constants.DefaultPreMeetingScore);
            this._meetingScore = item.MeetingScore.GetValueOrDefault(Constants.DefaultMeetingScore);
            this.CriteriaName = ViewHelpers.SetNonNull(item.CriteriaName);
            this.CriteriaOrder = item.CriteriaOrder.GetValueOrDefault(Constants.DefaultCriteriaOrder);
            this.PrgPartId = item.PrgPartId.GetValueOrDefault(Constants.DefaultPartId);
            this.AdjLabel = item.AdjLabel;
            this.MeetingAdjLabel = item.MeetingAdjLabel;
            this.ReviewerName = ViewHelpers.MakeScoreboardReviewerName(ViewHelpers.SetNonNull(item.RevFirstName), ViewHelpers.SetNonNull(item.RevLastName));
            this.IsCritiqueSubmitted = ViewHelpers.IsCritiqueSubmitted(item.DateSubmitted);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Pre-meeting score
        /// </summary>
        private decimal _preMeetingScore;
        public object PreMeetingScore
        {
            get
            {
                return GetPreMeetingValue();
            }
        }
        /// <summary>
        /// Meeting score
        /// </summary>
        private decimal _meetingScore;
        public object MeetingScore { get { return GetMeetingValue(); } }
        /// <summary>
        /// Criteria name
        /// </summary>
        public string CriteriaName { get; private set; }
        /// <summary>
        /// Criteria display order
        /// </summary>
        public int CriteriaOrder { get; private set; }
        /// <summary>
        /// Link id
        /// </summary>
        public int PrgPartId { get; private set; }
        /// <summary>
        /// Reviewers role
        /// </summary>
        public string Role { get; private set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string AdjLabel { get; private set; }
        /// <summary>
        /// Reviewers display name;
        /// </summary>
        public string ReviewerName { get; private set; }
        /// <summary>
        /// Display adjectival label for meeting scores
        /// </summary>
        public string MeetingAdjLabel { get; private set; }
        public bool IsCritiqueSubmitted { get; private set; }
        /// <summary>
        #endregion
        #region Helpers
        /// <summary>
        /// Determines the value that will be displayed.  For ReviewerCritiques_Result items
        /// that contain a value in AdjLabel, the AdjLabel value is returned.  Otherwise the
        /// Pre Meeting score value is returned.
        /// </summary>
        /// <returns>-----</returns>
        private object GetPreMeetingValue()
        {
            return (string.IsNullOrWhiteSpace(AdjLabel)) ? (object)this._preMeetingScore : (object)this.AdjLabel;
        }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <returns>-----</returns>
        private object GetMeetingValue()
        {
            return (string.IsNullOrWhiteSpace(MeetingAdjLabel)) ? (object)this._meetingScore : (object)this.MeetingAdjLabel;
        }
        #endregion
    }
}
