using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Views.ApplicationDetails;
using Sra.P2rmis.Bll.Views.CritiqueDetails;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll.Views.Scoreboard
{
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public class ScoreboardContainer : CritiqueDetailsContainer
    {
        #region Constants
        /// <summary>
        /// Class constants
        /// </summary>
        private class Constants
        {
            public const int EntriesExist = 1;
            /// <summary>
            /// Criteria with this value are excluded.
            /// </summary>
            public class ExcludedCriteria
            {
                public const string Description = "Description";
                public const string Summary = "Summary";
            }
        }
        #endregion
        #region Constructor
        private ScoreboardContainer() { }
        /// <summary>
        /// Construct & instantiate the ScoreboardContainer from a business layer
        /// CritiqueDetailResultModel object.
        /// </summary>
        /// <param name="resultModel">Data layer container of scoreboard query results</param>
        public ScoreboardContainer(ScoreboardResultModel container)
        {
            this.ApplicationId = container.ApplicationDetails.ApplicationId;
            this.PrincipalInvestigator = container.ApplicationDetails.PiLastName;
            this.ProgramId = container.ApplicationDetails.ProgramId;
            this.PanelId = container.ApplicationDetails.PanelId;
            ///
            /// Convert the data layer object into business layer objects.  Don't
            /// need all the data.
            /// 
            this.ScoreValues = new List<ReviewerCritiques_Result>(container.CritiqueDetails).ConvertAll(new Converter<ReviewerCritiques_Result, ScoreboardLine>(ReviewerCritiques_ResultToScoreboardLine));
            this.IsCritiqueDeadlinePassed = (this.ScoreValues.Count() < Constants.EntriesExist);
            this.ReviewerDetails = new List<ReviewerInfo_Result>(container.ReviewerDetails).ConvertAll(new Converter<ReviewerInfo_Result, ReviewerFacts>(ReviewerInfoViewToReviewerFacts));
            ///
            /// Create the UI facing store
            /// 
            this.Lines = new SortedDictionary<int, List<ScoreboardLine>>();
            ///
            /// Now arrange it into an order.
            /// 
            ConstructLinesFromIndividualScores(this.ScoreValues);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Principal Investigator Last name
        /// </summary>
        public string PrincipalInvestigator { get; private set; }
        /// <summary>
        /// Application identifier
        /// </summary>
        public string ApplicationId { get; private set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        private IEnumerable<ScoreboardLine> ScoreValues { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public IDictionary<int, List<ScoreboardLine>> Lines { get; private set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public IDictionary<int, string> Names { get; private set; }
        /// <summary>
        /// Container holding the business layer representation for the Reviewer details section.
        /// </summary>
        public IEnumerable<IReviewerFacts> ReviewerDetails { get; internal set; }
        /// <summary>
        /// Applications program Id
        /// </summary>
        public int ProgramId { get; set; }
        /// <summary>
        /// Applications panel Id
        /// </summary>
        public int PanelId { get; set; }
        #endregion
        #region Helpers
        /// <summary>
        /// Converts a business layer ReviewerLineScore to a presentation layer ApplicationDetails object.
        /// </summary>
        /// <param name="item">ReviewerLineScore object to convert</param>
        /// <returns>ApplicationDetails object</returns>
        private static ScoreboardLine ReviewerCritiques_ResultToScoreboardLine(ReviewerCritiques_Result item)
        {
            return new ScoreboardLine(item);
        }
        /// <summary>
        /// Converts a data layer Reviewer object into a business layer ReviewerFacts object.
        /// </summary>
        /// <param name="item">ReviewerInfo_Result object</param>
        /// <returns>ReviewerFacts object</returns>
        private static ReviewerFacts ReviewerInfoViewToReviewerFacts(ReviewerInfo_Result item)
        {
            return new ReviewerFacts(item);
        }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="collection">Enumerable collection of scoreboard lines</param>
        private void ConstructLinesFromIndividualScores(IEnumerable<ScoreboardLine> collection)
        {
            Dictionary<int, string> names = new Dictionary<int, string>();
            this.Names = names;

            foreach (var item in collection)
            {
                ///
                /// Skip any collection entry that has certain criteria names
                /// 
                if (
                    (!item.CriteriaName.Equals(Constants.ExcludedCriteria.Summary, StringComparison.CurrentCultureIgnoreCase)) &&
                    (!item.CriteriaName.Equals(Constants.ExcludedCriteria.Description, StringComparison.CurrentCultureIgnoreCase))
                    )
                {
                    ///
                    /// If there is not a dictionary in the dictionary for this criteria then 
                    /// create one.  Then create a list to add the entries
                    /// 
                    if (!Lines.ContainsKey(item.CriteriaOrder))
                    {
                        Lines[item.CriteriaOrder] = new List<ScoreboardLine>();
                    }
                    ///
                    /// Otherwise add the entry to the dictionary.
                    ///
                    List<ScoreboardLine> sl = Lines[item.CriteriaOrder];
                    sl.Add(item);
                    ///
                    /// Get the users name and add it to the names dictionary.  Each line may or may
                    /// not contain a score for each reviewer.  Add the name to the dictionary.  It really does not
                    /// matter if it gets added multiple times because it will just overwrite itself with the same
                    /// value.
                    /// 
                    names[item.PrgPartId] = FormatUserNameAndRole(item);//item.ReviewerName;
                }
            }
        }
        /// <summary>
        /// Locates the users role in a list of reviewers assigned to this application.
        /// </summary>
        /// <param name="line">-----</param>
        /// <returns>-----</returns>
        private string FormatUserNameAndRole(ScoreboardLine line)
        {
            string result = string.Empty;

            int key = line.PrgPartId;

            var reviewer = new List<IReviewerFacts>(this.ReviewerDetails).Find(x => x.PrgPartId == line.PrgPartId);
            if (reviewer != null)
            {
                result = reviewer.PartType;
            }

            return string.Format("{0} ({1})", line.ReviewerName, result);
        }
        #endregion
    }
}
