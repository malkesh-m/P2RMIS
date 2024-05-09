using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Views.CritiqueDetails
{
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public class CritiqueFacts : ICritiqueFacts
    {
        #region Constructors
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private CritiqueFacts() { }
        /// <summary>
        /// Construct & populate from a data layer Reviewer object.
        /// </summary>
        /// <param name="reviewer">Data layer Reviewer object</param>
        public CritiqueFacts(ReviewerCritiques_Result revcritique)
        {
            this.ApplicationID = ViewHelpers.SetNonNull(revcritique.ApplicationID);
            this.PanelID = (int)revcritique.PanelID;
            this.PanelAbrv = revcritique.PanelAbrv;
            this.PrincipalInvestigator = ViewHelpers.ConstructName(revcritique.PiLastName, revcritique.PiFirstName);
            this.PiOrgName = revcritique.PiOrgName;
            this.ApplicationTitle = revcritique.ApplicationTitle;
            this.AwardDescription = revcritique.AwardMechanism;
            this.PrgPartId = (int)revcritique.PrgPartId;
            this.PMScore = revcritique.PMScore;
            this.MeetingScore = revcritique.MeetingScore;
            this.IsSubmitted = ViewHelpers.IsCritiqueSubmitted(revcritique.DateSubmitted);
            this.PMText = ViewHelpers.SetNonNull(revcritique.PMText);
            this.MeetingText = ViewHelpers.SetNonNull(revcritique.MeetingText);
            this.CriteriaName = ViewHelpers.SetNonNull(revcritique.CriteriaName);
            this.AdjLabel = ViewHelpers.SetNonNull(revcritique.AdjLabel);
            this.ScoringType = ViewHelpers.SetNonNull(revcritique.ScoringType);
            this.TextFlag = (bool)revcritique.TextFlag;
            this.RevFullName = ViewHelpers.ConstructNameWithPrefix(revcritique.RevPrefix, revcritique.RevFirstName, revcritique.RevLastName);
            this.CriteriaOrder = (int)revcritique.CriteriaOrder;
            this.MeetingAdjLabel = ViewHelpers.SetNonNull(revcritique.MeetingAdjLabel);
            this.MeetingScoreType = ViewHelpers.SetNonNull(revcritique.MeetingScoringType);
            this.OtherAssignedReviewersArray = ViewHelpers.SplitDelimitedString(revcritique.OtherAssignedReviewers);
            this.OtherAssignedPartIdsArray = ViewHelpers.SplitDelimitedString(revcritique.OtherAssignedPartIds);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets and sets info for Critique Details
        /// </summary>

        public string ApplicationID { get; private set; }
        public string PrincipalInvestigator { get; private set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string ApplicationTitle { get; private set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string AwardDescription { get; private set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string PiOrgName { get; private set; }
        public int PanelID { get; private set; }
        public string PanelAbrv { get; private set; }
        public decimal? PMScore { get; private set; }
        public decimal? MeetingScore { get; private set; }
        public string PMText { get; private set; }
        public string MeetingText { get; private set; }
        public string CriteriaName { get; private set; }
        public string AdjLabel { get; private set; }
        public string ScoringType { get; private set; }
        public bool TextFlag { get; private set; }
        public bool IsSubmitted { get; private set; }
        public int PrgPartId { get; private set; }
        public string RevFullName { get; private set; }
        public int CriteriaOrder { get; private set; }
        public string MeetingAdjLabel { get; private set; }
        public string MeetingScoreType { get; private set; }
        public string[] OtherAssignedReviewersArray { get; private set; }
        public string[] OtherAssignedPartIdsArray { get; private set; }
        #endregion
    }
}
