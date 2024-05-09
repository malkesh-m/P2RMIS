using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    /// <summary>
    /// Container for business layer objects describing an applications details.  
    /// </summary>
    public class ApplicationDetailsContainer : IApplicationDetailsContainer
    {
        #region Constants
        private class Constants
        {
            public const int NumberOfColumns = 10;
            public const string AverageScores = "Average Scores:";
            public const string StandardDeviation = "Standard Deviation:";
            public const int OverallEvaluationKey = 0;
            public const int TopOfList = 0;
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        private ApplicationDetailsContainer() {}
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="resultModel">Application details result model</param>
        /// <param name="adjectivalLabelForScore">Function to calculate adjectival value</param>
        public ApplicationDetailsContainer(IApplicationDetailResultModel resultModel, Func<int, int, string> adjectivalLabelForScore)
        {
            this.AdjectivalLabelForScore = adjectivalLabelForScore;
            ///
            /// Convert the Reviewers, Scores & Comments
            /// 
            this.ReviewerDetails = resultModel.ReviewerDetails.ToList<ReviewerInfo_Result>().ConvertAll(new Converter<ReviewerInfo_Result, ReviewerFacts>(ReviewerInfoViewToReviewerFacts));
            this.ReviewerScores = resultModel.ReviewerScoreDetails.ToList<IReviewerScores>().ConvertAll(new Converter<IReviewerScores, IReviewerScoresFacts>(IReviewerScoresToIReviewerScoresFacts));
            this.ReviewerComments = resultModel.ReviewerComments.ToList<IReviewerComments>().ConvertAll(new Converter<IReviewerComments, IReviewerCommentFacts>(IReviewerCommentsToIReviewerCommentFacts));
            this.UserApplicationComments = resultModel.UserApplicationComments.ToList<IUserApplicationComments>().ConvertAll(new Converter<IUserApplicationComments, IUserApplicationCommentFacts>(IUserCommentsToIUserCommentFacts));
            this.CommentLookupTypes = resultModel.CommentLookupTypes.ToList<ICommentTypes>().ConvertAll(new Converter<ICommentTypes, ICommentLookupTypes>(ICommentTypesToICommentLookupTypes));

            CalculateApplicationDetails();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Container holding the business layer representation results for the Application details section.
        /// </summary>
        public IApplicationDetailFact ApplicationDetails { get; internal set; }
        /// <summary>
        /// Container holding the business layer representation for the Reviewer details section.
        /// </summary>
        public IEnumerable<IReviewerFacts> ReviewerDetails { get; internal set; }
        /// <summary>
        /// Container holding the business layer representation for the Reviewer scores details section.
        /// </summary>
        public IEnumerable<IReviewerScoresFacts> ReviewerScores { get; internal set; }
        /// <summary>
        /// Container holding the business layer representation for the Reviewer comments.
        /// </summary>
        public IEnumerable<IReviewerCommentFacts> ReviewerComments { get; internal set; }
        /// <summary>
        /// Container holding the business layer representation for the User application comments.
        /// </summary>
        public IEnumerable<IUserApplicationCommentFacts> UserApplicationComments { get; internal set; }
        /// <summary>
        /// Container holding the business layer representation for the Comment Lookup types
        /// </summary>
        public IEnumerable<ICommentLookupTypes> CommentLookupTypes { get; internal set; }
        /// <summary>
        /// Container holding the business layer representation for the Reviewer Line details section.
        /// </summary>
        private List<ReviewerLineScore> _details { get; set; }
        public IEnumerable<IReviewerLineScore> Details { get { return _details; } }
        /// <summary>
        /// Column titles, indexed by column sort order
        /// </summary>
        private Dictionary<int, string> _columns { get; set; }
        /// <summary>
        /// Contains the Alt text values for the column titles
        /// </summary>
        public IDictionary<int, string> ColumnAltText { get; set; }
        /// <summary>
        /// Ordered column titles
        /// </summary>
        public IEnumerable<KeyValuePair<int, string>> Columns { get { return this._columns.OrderBy(k => k.Key); } }
        /// <summary>
        /// Application details averages
        /// </summary>
        public IReviewerLineScore Averages { get; set; }
        /// <summary>
        /// Application details standard deviation
        /// </summary>
        public IReviewerLineScore StandardDeviation { get; set; }
        /// <summary>
        /// Function providing the adjectival label
        /// </summary>
        public Func<int, int, string> AdjectivalLabelForScore { get; set; }
        /// <summary>
        /// Answers the question "Is the application scored adjectival'
        /// </summary>
        private bool IsAdjectival { get; set; }
        #endregion
        #region Helpers
        /// <summary>
        /// Converts a data layer Reviewer object into a business layer ReviewerFacts object.
        /// </summary>
        /// <param name="item">-----</param>
        /// <returns>-----</returns>
        private static ReviewerFacts ReviewerInfoViewToReviewerFacts(ReviewerInfo_Result item)
        {
            return new ReviewerFacts(item);
        }
        /// <summary>
        /// Converts a data layer Reviewer score object into a business layer Reviewer score object.
        /// </summary>
        /// <param name="item">-----</param>
        /// <returns>-----</returns>
        public static IReviewerScoresFacts IReviewerScoresToIReviewerScoresFacts(IReviewerScores item)
        {
            return new ReviewerScoresFacts(item);
        }
        /// <summary>
        /// Converts a data layer Reviewer comments object into a business layer Reviewer comments object.
        /// </summary>
        /// <param name="item">-----</param>
        /// <returns>-----</returns>
        public static IReviewerCommentFacts IReviewerCommentsToIReviewerCommentFacts(IReviewerComments item)
        {
            return new ReviewerCommentFacts(item);
        }
        public static IUserApplicationCommentFacts IUserCommentsToIUserCommentFacts(IUserApplicationComments item)
        {
            return new UserApplicationCommentFacts(item);
        }
        public static ICommentLookupTypes ICommentTypesToICommentLookupTypes(ICommentTypes item)
        {
            return new CommentLookupTypes(item);
        }
        #endregion
        #region Methods
        /// <summary>
        /// TODO:: document me
        /// </summary>
        protected void CalculateApplicationDetails()
        {
            CreateListOfReviewers();
            MatchScoresToReviewers();
            SetCommentStatusOfReviewers();
            //
            // Determine if the application is scored as adjectival
            //
            DetermineIfAdjectival();
            ///
            /// At this point we have listed the reviewers & parsed the scores to the reviewers
            /// & listed the unique column titles.
            ///
            if (!IsAdjectival)
            {
                CalculateAverageAndStandardDeviation();
            }
        }
        /// <summary>
        /// Answers the question "Is the application scored adjectival'
        /// </summary>
        protected void DetermineIfAdjectival()
        {
            IsAdjectival = this.ReviewerScores.FirstOrDefault(x => x.AdjLabel != null) != null;
        }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        protected void CreateListOfReviewers()
        {
            _details = new List<ReviewerLineScore>();
            foreach (var item in ReviewerDetails)
            {
                _details.Add(new ReviewerLineScore(item));                    
            }
        }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        protected void MatchScoresToReviewers()
        {
            ///
            /// Instantiate the dictionary to store the column sort order and alt text verbiage
            /// 
            _columns = new Dictionary<int, string>(Constants.NumberOfColumns);
            ColumnAltText = new Dictionary<int, string>(Constants.NumberOfColumns);
            ///
            ///  Go through the scores and match the score to the reviewer based
            ///  on the ProgramPartId.
            ///
            foreach (var item in this.ReviewerScores)
            {
                ///
                /// Save the column title.  Does not matter if it is replaced
                /// because all the titles for the same column should be the
                /// same.
                /// 
                int index = (item.OverallEval)? Constants.OverallEvaluationKey :item.CriteriaSortOrder;
                _columns[index] = item.EvaluationCriteriaDescription;
                ColumnAltText[index] = item.EvaluationCriteriaDescription;

                ///
                ///  Locate the appropriate property to sum the value to.
                ///                
                var entry = _details.Find(x => x.ProgramPartId == item.PrgPartId);
                if (entry != null)
                {
                    if (item.HasValue)
                    {
                        //
                        // Some scoring may use non decimal scoring, called adjectival.  In which
                        // case the flag is set & we use what ever the label indicates.
                        //
                        if (item.AdjLabel != null)
                        {
                            entry.CriteriaScores[index] = this.AdjectivalLabelForScore(item.ClientScoringId.Value, Convert.ToInt32(item.Score));
                        }
                        //
                        // There may be a reason the score is null.  That is because they abstained from scoring in 
                        // which case the AbstainFlag is true.
                        //
                        else if (item.AbstainFlag)
                        {
                            entry.CriteriaScores[index] = "A";
                        }
                        else if (item.IntegerFlag)
                        {
                            entry.Scores[index] = item.Score;
                            entry.CriteriaScores[index] = Math.Round(item.Score).ToString();
                        }
                        else
                        {
                            entry.Scores[index] = item.Score;
                            entry.CriteriaScores[index] = ViewHelpers.P2rmisRound(item.Score).ToString();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Set comment status of reviewers
        /// </summary>
        public void SetCommentStatusOfReviewers()
        {
            foreach (var entry in _details)
            {
                if (this.ReviewerComments.Any(x => x.ReviewerId == entry.ReviewerUserId)) {
                    entry.HasComment = true;

                } else {
                    entry.HasComment = false;
                }
            }
        }
        /// <summary>
        /// Calculates the application review averages
        /// </summary>
        protected void CalculateAverageAndStandardDeviation()
        {
            ///
            /// Get the number of reviewers
            /// 
            int numberReviewers = this.ReviewerDetails.Count();
            ///
            ///  Sum up the scores for each column & calculate
            ///  the averages.
            ///
            IReviewerLineScore average = new ReviewerLineScore(Constants.AverageScores);
            IReviewerLineScore standardDeviation = new ReviewerLineScore(Constants.StandardDeviation);
            ///
            ///  For each evaluation criteria 
            ///
            foreach (var criteriaKey in _columns.Keys)
            {
                List<decimal> reviewerScores = new List<decimal>();
                ///
                /// Loop through the reviewers scores and build
                /// a list of the scores.
                /// 
                foreach (var detailLine in this._details)
                {
                    decimal value;
                    ///
                    /// retrieve the reviewer score if there is one.  If there is one
                    /// then sum it & increment the count of scores 
                    /// 
                    if (detailLine.Scores.TryGetValue(criteriaKey, out value))
                    {
                        reviewerScores.Add(value);
                    }
                }
                ///
                /// Now calculate the average & standard deviation for this column.
                /// Note: We use the raw average to calculate standard deviation rather than rounded
                ///
                if (!IsAdjectival)
                {
                    decimal rawAverage = ViewHelpers.Average(reviewerScores);
                    average.Scores[criteriaKey] = ViewHelpers.P2rmisRound(rawAverage);
                    average.CriteriaScores[criteriaKey] = (!IsAdjectival) ? average.Scores[criteriaKey].ToString() : string.Empty;
                    ///
                    /// It may seem odd to be rounding the standard deviation twice but this
                    /// is done to match the existing CDMRP reports
                    /// 
                    decimal temp = ViewHelpers.P2rmisRoundTwoDecimalPlaces(ViewHelpers.StandardDeviation(reviewerScores, rawAverage));
                    standardDeviation.Scores[criteriaKey] = ViewHelpers.P2rmisRound(temp);
                    standardDeviation.CriteriaScores[criteriaKey] = (!IsAdjectival) ? standardDeviation.Scores[criteriaKey].ToString() : string.Empty;
                }
        }
            ///
            /// Now that the averages & standard deviation are calculated, add each to the list of reviewers application detail lines.
            ///
            this.Averages = average;
            this.StandardDeviation = standardDeviation;
        }
        #endregion
    }
}
