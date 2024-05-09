using System.Collections.Generic;
using System;

namespace Sra.P2rmis.WebModels.Setup
{
    #region PreviewCriteriaLayoutModel
    /// <summary>
    /// Data model returned for a single criteria layout
    /// </summary>
    public interface IPreviewCriteriaLayoutModel
    {
        #region Attributes
        /// <summary>
        /// Criteria abbreviation
        /// </summary>
        string ElementAbbreviation { get; set; }
        /// <summary>
        /// Element description
        /// </summary>
        string ElementDescription { get; set; }
        /// <summary>
        /// Criteria instructions
        /// </summary>
        string InstructionText { get; set; }
        /// <summary>
        /// Criteria review recommended word count
        /// </summary>
        Nullable<int> RecommendedWordCount { get; set; }
        /// <summary>
        /// Scoring low value
        /// </summary>
        Nullable<decimal> LowValue { get; set; }
        /// <summary>
        /// Scoring high value
        /// </summary>
        Nullable<decimal> HighValue { get; set; }
        /// <summary>
        /// Indicates if the scoring is Integer
        /// </summary>
        bool IsScoringInteger { get; set; }
        /// <summary>
        /// Indicates if the scoring is Decimal
        /// </summary>
        bool IsScoringDecimal { get; set; }
        /// <summary>
        /// Indicates if the scoring is Adjectival
        /// </summary>
        bool IsScoringAdjectival { get; set; }
        /// <summary>
        /// List of adjectival values (if any)
        /// </summary>
        List<string> AdjectivalValues { get; set; }
        /// <summary>
        /// Whether the criterion contains text input
        /// </summary>
        bool IsCriteriaText { get; set; }
        /// <summary>
        /// Whether this is an overall element
        /// </summary>
        bool IsOverall { get; set; }
        /// <summary>
        /// SortOrder
        /// </summary>
        int SortOrder { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// MechanismTemplateElement entity identifier that describes this
        /// </summary>
        int MechanismTemplateElementId { get; set; }
        /// <summary>
        /// Parent MechanismTemplate entity container.
        /// </summary>
        int MechanismTemplateId { get; set; }
        /// <summary>
        /// ClientScoringScale entity identifier of the adjectival container.  Need this because population needs
        /// to be done in two steps.
        /// </summary>
        int ClientScoringScaleId { get; set; }
        #endregion
    }
    /// <summary>
    /// Data model returned for a single criteria layout
    /// </summary>
    public class PreviewCriteriaLayoutModel : IPreviewCriteriaLayoutModel
    {
        #region Attributes
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
        /// Whether the criterion contains text input
        /// </summary>
        public bool IsCriteriaText { get; set; }
        /// <summary>
        /// Whether this is an overall element
        /// </summary>
        public bool IsOverall { get; set; }
        /// <summary>
        /// SortOrder
        /// </summary>
        public int SortOrder { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// MechanismTemplateElement entity identifier that describes this
        /// </summary>
        public int MechanismTemplateElementId { get; set; }
        /// <summary>
        /// Parent MechanismTemplate entity container.
        /// </summary>
        public int MechanismTemplateId { get; set; }
        /// <summary>
        /// ClientScoringScale entity identifier of the adjectival container.  Need this because population needs
        /// to be done in two steps.
        /// </summary>
        public int ClientScoringScaleId { get; set; }
        #endregion
    }
    #endregion
}
