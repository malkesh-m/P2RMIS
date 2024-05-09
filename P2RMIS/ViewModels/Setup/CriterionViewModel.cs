using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.CrossCuttingServices;
using Newtonsoft.Json;
using System.Net;

namespace Sra.P2rmis.Web.UI.Models
{
    public class CriterionViewModel
    {
        public CriterionViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriterionViewModel"/> class.
        /// </summary>
        /// <param name="criterion">The criterion.</param>
        public CriterionViewModel(IEvaluationCriteriaModel criterion)
        {
            Element = criterion.ElementAbbreviation;
            ElementDescription = criterion.ElementDescription;
            Description = HtmlServices.SanitizeHtml(criterion.InstructionText);
            Limit = criterion.RecommendedWordCount != null ?
                criterion.RecommendedWordCount.ToString() : String.Empty;
            Score = ViewHelpers.FormatBoolean(criterion.ScoreFlag);
            CritiqueRequiredText = ViewHelpers.FormatBoolean(criterion.TextFlag);
            CritiqueOrder = criterion.SortOrder.ToString();
            SummaryStatementOrder = criterion.SummarySortOrder != null ?
                criterion.SummarySortOrder.ToString() : String.Empty;
            IsOverall = criterion.OverallFlag;
            MechanismTemplateId = criterion.MechanismTemplateId;
            MechanismTemplateElementId = criterion.MechanismTemplateElementId;
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        [JsonProperty("index")]
        public int Index { get; set; }

        /// <summary>
        /// Gets the mechanism template identifier.
        /// </summary>
        /// <value>
        /// The mechanism template identifier.
        /// </value>
        [JsonProperty("mechanismTemplateId")]
        public int MechanismTemplateId { get; private set; }

        /// <summary>
        /// Gets the mechanism template element identifier.
        /// </summary>
        /// <value>
        /// The mechanism template element identifier.
        /// </value>
        [JsonProperty("mechanismTemplateElementId")]
        public int MechanismTemplateElementId { get; private set; }

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <value>
        /// The element.
        /// </value>
        [JsonProperty("evalCriteria")]
        public string Element { get; private set; }
        /// <summary>
        /// Element description
        /// </summary>
        [JsonProperty("elementDescription")]
        public string ElementDescription { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [JsonProperty("description")]
        public string Description { get; private set; }

        /// <summary>
        /// Gets the limit.
        /// </summary>
        /// <value>
        /// The limit.
        /// </value>
        [JsonProperty("limit")]
        public string Limit { get; private set; }

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        [JsonProperty("score")]
        public string Score { get; private set; }

        /// <summary>
        /// Gets the critique required text.
        /// </summary>
        /// <value>
        /// The critique required text.
        /// </value>
        [JsonProperty("critique")]
        public string CritiqueRequiredText { get; private set; }

        /// <summary>
        /// Gets the critique order.
        /// </summary>
        /// <value>
        /// The critique order.
        /// </value>
        [JsonProperty("critiqueOrder")]
        public string CritiqueOrder { get; private set; }

        /// <summary>
        /// Gets the summary statement order.
        /// </summary>
        /// <value>
        /// The summary statement order.
        /// </value>
        [JsonProperty("sumOrder")]
        public string SummaryStatementOrder { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [assignments released].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [assignments released]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("assignmentsReleased")]
        public bool AssignmentsReleased { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance is overall.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is overall; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("isOverall")]
        public bool IsOverall { get; private set; }

        /// <summary>
        /// Gets the overall text.
        /// </summary>
        /// <value>
        /// The overall text.
        /// </value>
        [JsonProperty("overallText")]
        public string OverallText => IsOverall ? Labels.OverallLabel : string.Empty;

        /// <summary>
        /// Container for label values
        /// </summary>
        private static class Labels
        {
            /// <summary>
            /// The overall label
            /// </summary>
            public const string OverallLabel = "Overall";
        }
        /// <summary>
        /// Gets a value indicating whether this instance is overall.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is overall; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("scoringTemplateId")]
        public string ScoringTemplateId { get; private set; }
    }
}