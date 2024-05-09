using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll.Views.CritiqueDetails
{
    public class CritiqueDetailsContainer : ICritiqueDetailsContainer
    {
        #region Constants
        private class Constants
        {
            public const int EntriesExist = 1;
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        protected CritiqueDetailsContainer() {}
        /// <summary>
        /// Constructor
        /// </summary>
        public CritiqueDetailsContainer(CritiqueDetailResultModel resultModel)
        {
            ///
            /// Convert the Reviewers, Scores & Comments
            /// 
            this.CritiqueDetails = resultModel.CritiqueDetails.ToList<ReviewerCritiques_Result>().ConvertAll(new Converter<ReviewerCritiques_Result, CritiqueFacts>(ReviewerCritiqueViewToCritiqueFacts));
            this.IsCritiqueDeadlinePassed = (this.CritiqueDetails.Count() < Constants.EntriesExist);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Container holding the business layer representation results for the Application details section.
        /// </summary>
        public ApplicationDetails.IApplicationDetailFact ApplicationDetails { get; internal set; }
        /// <summary>
        /// Container holding the business layer representation for the Reviewer critique details section.
        /// </summary>
        public IEnumerable<CritiqueFacts> CritiqueDetails { get; internal set; }
        /// <summary>
        /// Indicates if the critique deadline has passed and there are no scoreboard data.
        /// </summary>
        public bool IsCritiqueDeadlinePassed { get; protected set; }
        #endregion
        #region Helpers
        /// <summary>
        /// Converts a data layer Reviewer object into a business layer Critique
        /// Facts object.
        /// </summary>
        /// <param name="item">-----</param>
        /// <returns>-----</returns>
        private static CritiqueFacts ReviewerCritiqueViewToCritiqueFacts(ReviewerCritiques_Result item)
        {
            return new CritiqueFacts(item);
        }
        #endregion
    }
}
