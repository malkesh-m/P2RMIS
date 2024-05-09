using System;
using System.Linq;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.Web.UI.Models
{
    public class PreviewCriterionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewCriterionViewModel"/> class.
        /// </summary>
        public PreviewCriterionViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewCriterionViewModel"/> class.
        /// </summary>
        public PreviewCriterionViewModel(IPreviewCriteriaLayoutModel criterion, ClientScoringScaleLegendModel legend)
        {
            ElementAbbreviation = criterion.ElementAbbreviation;
            ElementDescription = criterion.ElementDescription;
            InstructionText = HtmlServices.SanitizeHtml(criterion.InstructionText);
            InstructionText = InstructionText.Replace("\"", "\\\"").Replace("&quot;", "\\\"");
            RecommendedWordCount = criterion.RecommendedWordCount;
            IsScoringInteger = criterion.IsScoringInteger;
            IsScoringDecimal = criterion.IsScoringDecimal;
            IsScoringAdjectival = criterion.IsScoringAdjectival;
            AdjectivalValues = criterion.AdjectivalValues;
            LowValue = criterion.LowValue;
            HighValue = criterion.HighValue;
            SortOrder = criterion.SortOrder;
            IsCriteriaText = criterion.IsCriteriaText;
            IsOverall = criterion.IsOverall;
            if (IsScoringInteger || IsScoringDecimal || IsScoringAdjectival)
            {
                ScoreScale = (criterion.IsOverall) ? GetScoreScaleLabel(legend.Overall) : GetScoreScaleLabel(legend.Criterion);
            }
        }        

        /// <summary>
        /// Criteria abbreviation
        /// </summary>
        public string ElementAbbreviation { get; set; }
        /// <summary>
        /// Element description
        /// </summary>
        public string ElementDescription { get; set; }
        /// <summary>
        /// Criteria instructions
        /// </summary>
        public string InstructionText { get; set; }
        /// <summary>
        /// Criteria review recommended word count
        /// </summary>
        public Nullable<int> RecommendedWordCount { get; set; }
        /// <summary>
        /// Scoring low value
        /// </summary>
        public Nullable<decimal> LowValue { get; set; }
        /// <summary>
        /// Scoring high value
        /// </summary>
        public Nullable<decimal> HighValue { get; set; }
        /// <summary>
        /// Indicates if the scoring is Integer
        /// </summary>
        public bool IsScoringInteger { get; set; }
        /// <summary>
        /// Indicates if the scoring is Decimal
        /// </summary>
        public bool IsScoringDecimal { get; set; }
        /// <summary>
        /// Indicates if the scoring is Adjectival
        /// </summary>
        public bool IsScoringAdjectival { get; set; }
        /// <summary>
        /// List of adjectival values (if any)
        /// </summary>
        public List<string> AdjectivalValues { get; set; } = new List<string>();
        /// <summary>
        /// Score scale
        /// </summary>
        public string ScoreScale { get; set; }
        /// <summary>
        /// Whether the criterion contains text input
        /// </summary>
        public bool IsCriteriaText { get; set; }
        /// <summary>
        /// SortOrder
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// Whether it is an overall criterion.
        /// </summary>
        public bool IsOverall { get; set; }
        /// <summary>
        /// Gets score scale label.
        /// </summary>
        /// <param name="scaleLegends">Scale legends</param>
        /// <returns></returns>
        private string GetScoreScaleLabel(IEnumerable<IScoringScaleLegendModel> scaleLegends)
        {
            string result = string.Empty;
            if (!scaleLegends.IsEmpty())
            {
                IScoringScaleLegendModel highModel = scaleLegends.First();
                IScoringScaleLegendModel lowModel = scaleLegends.Last();

                result = MessageService.CriterionScoreingScaleLabel(lowModel.LowValue, lowModel.LegendItemLabel, highModel.HighValue, highModel.LegendItemLabel);
            }
            return result;
        }
    }
}