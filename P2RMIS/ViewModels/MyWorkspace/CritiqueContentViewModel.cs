using System.Collections.Generic;
using System;
using System.Linq;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    public class CritiqueContentViewModel
    {
        public const int RecommendedWordLimitIfNotSet = 500;

        public CritiqueContentViewModel(ICritiqueContent critiqueContent, bool isPanelStarted, bool isStageAsync, bool isCritiqueSubmitted)
        {
            Criterion = critiqueContent.Criterion;
            ClientElementId = critiqueContent.ClientElementId;
            Content = critiqueContent.Content;
            Score = SetScore(critiqueContent.Score, critiqueContent.ValidAdjectivalScores);
            ScoreType = critiqueContent.ScoreType;
            IsCriteriaScored = critiqueContent.IsCriteriaScored;
            IsCriteriaText = critiqueContent.IsCriteriaText;
            ApplicationWorkflowStepElementId = critiqueContent.ApplicationWorkflowStepElementId;
            ApplicationWorkflowStepElementContentId = critiqueContent.ApplicationWorkflowStepElementContentId;
            CritiqueRevised = critiqueContent.CritiqueRevised;
            PhaseScores = SetPhaseScores(critiqueContent.PhaseScores); 
            Instructions = critiqueContent.Instructions;
            IsAbstain = critiqueContent.Abstain;
            IsAbstainable = (!critiqueContent.IsOverall || critiqueContent.Abstain) && (!isPanelStarted || (isStageAsync && !isCritiqueSubmitted));
            IsCritiqueSubmitted = isCritiqueSubmitted;
            IsPanelStarted = isPanelStarted;
            IsRatable = ScoreType != string.Empty && (!isPanelStarted || (isStageAsync && !isCritiqueSubmitted));
            ScoreScales = SetScoreScales(critiqueContent.ScoreType, critiqueContent.ScoreLowValue, critiqueContent.ScoreHighValue,
                critiqueContent.ValidAdjectivalScores);
            IsOverall = critiqueContent.IsOverall;
            RecommendedWordLimit = critiqueContent.RecommendedWordLimit ?? RecommendedWordLimitIfNotSet;
        }
        /// <summary>
        /// Indicates if the panel has started
        /// </summary>
        public bool IsPanelStarted { get; private set; }
        /// <summary>
        /// The Criterion
        /// </summary>
        public string Criterion { get; private set; }
        /// <summary>
        /// ClientElementId of criteria
        /// </summary>
        public int ClientElementId { get; private set; }
        /// <summary>
        /// Reviewer entered text.
        /// </summary>
        public string Content { get; private set; }
        /// <summary>
        /// The pre-meeting score of the application (if applicable)
        /// </summary>
        public string PreMeetingScore { get; private set; }
        /// <summary>
        /// Score for the current stage
        /// </summary>
        public string Score { get; private set; }
        /// <summary>
        /// Score type
        /// </summary>
        public string ScoreType { get; private set; }
        /// <summary>
        /// Whether the criteria accepts a score
        /// </summary>
        public bool IsCriteriaScored { get; private set; }
        /// <summary>
        /// Flag to indicate if the criteria accepts a critique
        /// </summary>
        public bool IsCriteriaText { get; private set; }
        /// <summary>
        /// Application workflow step element identifier
        /// </summary>
        public int ApplicationWorkflowStepElementId { get; private set; }
        /// <summary>
        /// Application workflow step element content identifier
        /// </summary>
        public int ApplicationWorkflowStepElementContentId { get; private set; }
        /// <summary>
        /// Flag to indicate if the critique was revised during the current phase
        /// </summary>
        public bool CritiqueRevised { get; private set; }
        /// <summary>
        /// Gets the phase scores.
        /// </summary>
        public List<KeyValuePair<string, string>> PhaseScores { get; private set; }
        /// <summary>
        /// Flag to indicate if the reviewer abstains from the critique
        /// </summary>
        public bool IsAbstain { get; private set; }
        /// <summary>
        /// Flag to indicate if the reviewer already submitted the critique
        /// </summary>
        public bool IsCritiqueSubmitted { get; private set; }
        /// <summary>
        /// Flag to indicate if the element is abstain-able
        /// </summary>
        public bool IsAbstainable { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance is ratable.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is ratable; otherwise, <c>false</c>.
        /// </value>
        public bool IsRatable { get; private set; }
        /// <summary>
        /// Indicates if the critique content is for an Overall criterion.
        /// </summary>
        public bool IsOverall { get; private set; }
        /// <summary>
        /// Instructions
        /// </summary>
        public string Instructions { get; private set; }
        /// <summary>
        /// Gets the recommended word limit.
        /// </summary>
        /// <value>
        /// The recommended word limit.
        /// </value>
        public int RecommendedWordLimit { get; private set; }
        /// <summary>
        /// Score scales
        /// </summary>
        public List<KeyValuePair<string, string>> ScoreScales { get; private set; }
        /// <summary>
        /// Flag to indicate if score is selectable
        /// </summary>
        public bool IsScoreSelectable
        {
            get
            {
                return ScoreScales != null && ScoreScales.Count > 0;
            }
        }
        /// <summary>
        /// Replace the paragraph markers with HTML paragraph tags
        /// </summary>
        public void ReplaceParagraphMarkers()
        {
            this.Content = HtmlServices.ReplaceParagraphMarkers(this.Content);
        }
        #region Helpers                  
        /// <summary>
        /// Sets the score.
        /// </summary>
        /// <param name="score">The score.</param>
        /// <param name="validAdjectivalScores">The valid adjectival scores.</param>
        /// <returns></returns>
        private string SetScore(string score, IEnumerable<AdjectivalScoreValue> validAdjectivalScores)
        {
            if (validAdjectivalScores.Count() > 0)
            {
                var adjScore = validAdjectivalScores.Where(x => x.AdjectivalLabel == score).FirstOrDefault();
                if (adjScore != null) 
                    score = adjScore.NumericValue;
            }
            return score;
        }
        /// <summary>
        /// Sets the phase scores.
        /// </summary>
        /// <param name="critiqueScores">The critique scores.</param>
        /// <returns>The phase score list.</returns>
        private List<KeyValuePair<string, string>> SetPhaseScores(IEnumerable<CritiqueScore> critiqueScores)
        {
            var phaseScores = new List<KeyValuePair<string, string>>();
            if (critiqueScores != null)
            {
                critiqueScores.OrderBy(x => x.Order).ToList().ForEach(s => {
                    phaseScores.Add(new KeyValuePair<string, string>(s.PhaseName, s.Score));
                });     
            }
            return phaseScores;
        }
        /// <summary>
        /// Sets the score scales.
        /// </summary>
        /// <param name="scoreType">Type of the score.</param>
        /// <param name="lowValue">The low value.</param>
        /// <param name="highValue">The high value.</param>
        /// <param name="validAdjectivalScores">The valid adjectival scores.</param>
        /// <returns>The score scale list.</returns>
        private List<KeyValuePair<string, string>> SetScoreScales(string scoreType, decimal lowValue, decimal highValue,
                IEnumerable<AdjectivalScoreValue> validAdjectivalScores) {
            var scoreScales = new List<KeyValuePair<string, string>>();
            if (string.Equals(scoreType, Invariables.MyWorkspace.Integer, StringComparison.OrdinalIgnoreCase))
            {
                var minValue = Convert.ToInt32(Math.Min(lowValue, highValue));
                var maxValue = Convert.ToInt32(Math.Max(lowValue, highValue));
                for (var i = minValue; i <= maxValue; i++)
                {
                    scoreScales.Add(new KeyValuePair<string, string>(i.ToString(), i.ToString()));
                }
            }
            else if (string.Equals(scoreType, Invariables.MyWorkspace.Adjectival, StringComparison.OrdinalIgnoreCase)
                && validAdjectivalScores != null && validAdjectivalScores.Count() > 0)
            {
                validAdjectivalScores.OrderBy(x => x.SortOrder).ToList().ForEach(s => {
                    scoreScales.Add(new KeyValuePair<string, string>(s.NumericValue, s.AdjectivalLabel));
                });
            }
            return scoreScales;
        }
        #endregion
    }
}