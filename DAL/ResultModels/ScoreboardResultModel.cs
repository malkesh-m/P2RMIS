using System.Collections.Generic;

namespace Sra.P2rmis.Dal.ResultModels
{
    public class ScoreboardResultModel : Sra.P2rmis.Dal.ResultModels.IScoreboardResultModel
    {
        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        internal ScoreboardResultModel() { }
        #endregion
        #region Properties
        /// <summary>
        /// Container holding the data layer representation results for the Application details query section.
        /// </summary>
        public IApplicationDetail ApplicationDetails { get; internal set; }
        /// <summary>
        /// Container holding the data layer representation results for the Reviewer critiques details query section.
        /// </summary>
        public IEnumerable<ReviewerCritiques_Result> CritiqueDetails { get; internal set; }
        /// <summary>
        /// Container holding the data layer representation results for the Reviewer details query section.
        /// </summary>
        public IEnumerable<ReviewerInfo_Result> ReviewerDetails { get; internal set; }
        #endregion
    }
}
