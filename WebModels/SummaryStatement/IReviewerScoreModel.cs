﻿namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Model representing an individual reviewers scores
    /// </summary>
    public interface IReviewerScoreModel
    {
        /// <summary>
        /// Order in which the reviewer is displayed
        /// </summary>
        int? ReviewerSortOrder { get; set; }

        /// <summary>
        /// Score value provided by the reviewer
        /// </summary>
        decimal? Score { get; set; }

        /// <summary>
        /// Adjectival equivalent of the score value
        /// </summary>
        string AdjectivalValue { get; set; }

        /// <summary>
        /// The type of scoring of the ScoreType
        /// </summary>
        string ScoreType { get; set; }
    }
}