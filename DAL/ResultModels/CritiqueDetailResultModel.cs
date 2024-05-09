using System.Collections.Generic;

namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Container holding the results of retrieving an Application's detail.
    /// </summary>
    public class CritiqueDetailResultModel : ICritiqueDetailResultModel
    {
        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        internal CritiqueDetailResultModel() { }
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
        
        #endregion
        
    }
}
