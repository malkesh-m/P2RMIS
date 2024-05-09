using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.Bll.ApplicationScoring;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ReviewerScoringViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewerScoringViewModel"/> class.
        /// </summary>
        public ReviewerScoringViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewerScoringViewModel"/> class.
        /// </summary>
        /// <param name="reviewerScores">The reviewer scores.</param>
        public ReviewerScoringViewModel(List<ScoreCacheEntry> reviewerScores, List<ScoreEntryViewModel> scoreEntries)
        {
            ScoreEntries = scoreEntries;
            UpdateScoreEntries(reviewerScores);

            AverageScore = new ScoreEntryViewModel();
            StandardDeviation = new ScoreEntryViewModel();
            SetAverageScoreAndStandarDeviation();
        }
        /// <summary>
        /// The updated flag
        /// </summary>
        private bool UpdatedFlag = false;

        /// <summary>
        /// Gets the reviewer scores.
        /// </summary>
        /// <value>
        /// The reviewer scores.
        /// </value>
        public List<ScoreEntryViewModel> ScoreEntries { get; private set; }

        /// <summary>
        /// Gets the average score.
        /// </summary>
        /// <value>
        /// The average score.
        /// </value>
        public ScoreEntryViewModel AverageScore { get; private set; }

        /// <summary>
        /// Gets the standard deviation.
        /// </summary>
        /// <value>
        /// The standard deviation.
        /// </value>
        public ScoreEntryViewModel StandardDeviation { get; private set; }

        /// <summary>
        /// Sets the average score and standar deviation.
        /// </summary>
        private void SetAverageScoreAndStandarDeviation()
        {
            if (UpdatedFlag && ScoreEntries.Count > 0)
            {
                List<decimal> overalls = new List<decimal>();
                ScoreEntries.ForEach(x => {
                    decimal ds;
                    if (decimal.TryParse(x.OverallScore, out ds))
                    {
                        overalls.Add(Convert.ToDecimal(x.OverallScore));
                    }
                });
                decimal averageOverall = ViewHelpers.P2rmisRound(ViewHelpers.Average(overalls));
                decimal standardDeviationOverall = ViewHelpers.P2rmisRound(ViewHelpers.StandardDeviation(overalls, averageOverall));

                List<string> averageScores = new List<string>();
                List<string> standardDeviationScores = new List<string>();

                // Loop through score criteria and calculate average and standard deviation values
                for (var i = 0; i < ScoreEntries[0].Scores.Count; i++)
                {
                    List<decimal> reviewerScores = new List<decimal>();
                    ScoreEntries.ForEach(x => {
                        decimal ds;
                        if (decimal.TryParse(x.Scores[i], out ds))
                        {
                            reviewerScores.Add(Convert.ToDecimal(x.Scores[i]));
                        }
                    });

                    decimal averageScore = ViewHelpers.P2rmisRound(ViewHelpers.Average(reviewerScores));
                    averageScores.Add(String.Format("{0:0.0}", averageScore));

                    decimal standardDeviationScore = ViewHelpers.P2rmisRound(ViewHelpers.StandardDeviation(reviewerScores, averageScore));
                    standardDeviationScores.Add(String.Format("{0:0.0}", standardDeviationScore));
                }
                AverageScore.OverallScore = String.Format("{0:0.0}", averageOverall);
                AverageScore.Scores = averageScores;
                StandardDeviation.OverallScore = String.Format("{0:0.0}", standardDeviationOverall);
                StandardDeviation.Scores = standardDeviationScores;
            }
        }
        /// <summary>
        /// Updates the score entries.
        /// </summary>
        /// <param name="reviewerScores">The reviewer scores.</param>
        private void UpdateScoreEntries(List<ScoreCacheEntry> reviewerScores)
        {
            UpdatedFlag = reviewerScores.Count > 0;
            var rs = reviewerScores.ConvertAll(x => new ScoreEntryViewModel(x));
            ScoreEntries.ToList().ForEach(x =>
            {
                var newEntry = reviewerScores.FirstOrDefault(y => y.ReviewerIndex == x.ReviewerIndex);
                if (newEntry != null)
                {
                    x.OverallScore = newEntry.OverallScore ?? String.Empty;
                    x.Scores = newEntry.Scores.ToList();
                }
                else
                {
                    x.OverallScore = x.OverallScore ?? String.Empty;
                }
            });
        }

        public class ScoreEntryViewModel
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ScoreEntryViewModel" /> class.
            /// </summary>
            public ScoreEntryViewModel() { }

            /// <summary>
            /// Initializes a new instance of the <see cref="ScoreEntryViewModel" /> class.
            /// </summary>
            /// <param name="scoreCacheEntry">The score cache entry.</param>
            public ScoreEntryViewModel(ScoreCacheEntry scoreCacheEntry)
            {
                ReviewerIndex = scoreCacheEntry.ReviewerIndex;
                OverallScore = scoreCacheEntry.OverallScore ?? String.Empty;
                Scores = scoreCacheEntry.Scores.ToList();
            }

            /// <summary>
            /// UserId as a string value.  Since only updates will be sent
            /// some way is required to identify which reviewer the updated
            /// scores are for.
            /// </summary>
            public string ReviewerIndex { get; set; }
            /// <summary>
            /// Overall score value.
            /// </summary>
            public string OverallScore { get; set; }
            /// <summary>
            /// Scores is a list of the individual score values in criteria
            /// order (1 ... n).  Currently n has a maximum of 10 but this
            /// representation provides for any expansion.
            /// </summary>
            public List<string> Scores { get; set; }
        }
    }
}