using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Data model returned to populate the Upload Scoring Template modal grids
    /// </summary>
    public interface IUploadScoringTemplateGridModalModel
    {
        string TemplateName { get; set; }
        string StepTypeName { get; set; }
        string OverallType { get; set; }
        IList<string> OverallAdjectivalScale { get; set; }
        decimal OverallHighValue { get; set; }
        decimal OverallLowValue { get; set; }
        string CriteriaType { get; set; }
        IList<string> CriteriaAdjectivalScale { get; set; }
        decimal CriteriaHighValue { get; set; }
        decimal CriteriaLowValue { get; set; }
    }
    /// <summary>
    /// Data model returned to populate the Upload Scoring Template modal grids
    /// </summary>
    public class UploadScoringTemplateGridModalModel: IUploadScoringTemplateGridModalModel
    {
        /// <summary>
        /// Template title
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// Review phase
        /// </summary>
        public string StepTypeName { get; set; }
        /// <summary>
        /// Scoring type of overall criterion
        /// </summary>
        public string OverallType{ get; set; }
        /// <summary>
        /// Overall adjectival scoring scale if one exists
        /// </summary>
        public IList<string> OverallAdjectivalScale { get; set; }
        /// <summary>
        /// Overall high value
        /// </summary>
        public decimal OverallHighValue { get; set; }
        /// <summary>
        /// Overall low value
        /// </summary>
        public decimal OverallLowValue { get; set; }
        /// <summary>
        /// Scoring type of non-overall criteria
        /// </summary>
        public string CriteriaType { get; set; }
        /// <summary>
        /// Criteria adjectival scoring scale if one exists
        /// </summary>
        public IList<string> CriteriaAdjectivalScale { get; set; }
        /// <summary>
        /// Criteria high value
        /// </summary>
        public decimal CriteriaHighValue { get; set; }
        /// <summary>
        /// Criteria low value
        /// </summary>
        public decimal CriteriaLowValue { get; set; }
    }

    /// <summary>
    /// Data model returned to populate the Upload Scoring Template modal
    /// </summary>
    public interface IUploadScoringTemplateModalModel
    {
        string TemplateTitle { get; }
        IEnumerable<IUploadScoringTemplateGridModalModel> TopGrid { get; }
        IEnumerable<IUploadScoringTemplateGridModalModel> BottomGrid { get; }
        int ScoringTemplateId { get; set; }
    }
    /// <summary>
    /// Data model returned to populate the Upload Scoring Template modal
    /// </summary>
    public class UploadScoringTemplateModalModel : IUploadScoringTemplateModalModel
    {
        #region Constructor & setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="templateTitle">The template title</param>
        /// <param name="topGrid">The top grid contents</param>
        /// <param name="scoringTemplateId">ScoringTemplate entity identifier</param>
        /// <param name="bottomGrid">The bottom grid's contents</param>
        public UploadScoringTemplateModalModel(string templateTitle, IEnumerable<IUploadScoringTemplateGridModalModel> topGrid, IEnumerable<IUploadScoringTemplateGridModalModel> bottomGrid, int scoringTemplateId)
        {
            this.TemplateTitle = templateTitle;
            this.TopGrid = topGrid;
            this.BottomGrid = bottomGrid;
            this.ScoringTemplateId = scoringTemplateId;
        }
        #endregion
        /// <summary>
        /// Template Title
        /// </summary>
        public string TemplateTitle { get; private set; } = string.Empty;
        /// <summary>
        /// Contents of the upper grid
        /// </summary>
        public IEnumerable<IUploadScoringTemplateGridModalModel> TopGrid { get; private set; } = new List<IUploadScoringTemplateGridModalModel>();
        /// <summary>
        /// Contents of the bottom grid
        /// </summary>
        public IEnumerable<IUploadScoringTemplateGridModalModel> BottomGrid { get; private set; } = new List<IUploadScoringTemplateGridModalModel>();
        /// <summary>
        /// ScoringTemplate entity identifier
        /// </summary>
        public int ScoringTemplateId { get; set; }
    }
}
