using System;
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.Setup
{
    #region Evaluation Criteria Add
    /// <summary>
    /// Data model returned when a new Evaluation Criteria is added.
    /// </summary>
    public interface IEvaluationCriteriaAdditionModel
    {
        #region Attributes
        /// <summary>
        /// Indicates if the award has an overall criteria.
        /// </summary>
        bool HasOverall { get; set; }
        /// <summary>
        /// Indicates the maximum sort order of the awards criteria
        /// </summary>
        int MaxSortOrder { get; set; }
        /// <summary>
        /// Whether to show abbreviation on score board.
        /// </summary>
        bool ShowAbbreviationOnScoreboard { get; set; }
        /// <summary>
        /// Gets or sets the maximum summary statement sort order.
        /// </summary>
        /// <value>
        /// The maximum summary statement sort order.
        /// </value>
        int MaxSummaryStatementSortOrder { get; set; }
        /// <summary>
        /// Awards current EvaluationCriteria.  Values are for MechanismTemplateId.
        /// </summary>
        IEnumerable<int> EvaluationCriteria { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// MechanismTemplate entity identifier hosting the new MechanismTemplateElement
        /// </summary>
        Nullable<int> MechanismTemplateId { get; set; }
        #endregion
    }
    /// <summary>
    /// Data model returned when a new Evaluation Criteria is added.
    /// </summary>
    public class EvaluationCriteriaAdditionModel: IEvaluationCriteriaAdditionModel
    {
        /// <summary>
        /// Indicates if the award has an overall criteria.
        /// </summary>
        public bool HasOverall { get; set; }
        /// <summary>
        /// Indicates the maximum sort order of the awards criteria
        /// </summary>
        public int MaxSortOrder { get; set; }
        /// <summary>
        /// Gets or sets the maximum summary statement sort order.
        /// </summary>
        /// <value>
        /// The maximum summary statement sort order.
        /// </value>
        public int MaxSummaryStatementSortOrder { get; set; }
        
        /// <summary>
        /// Whether to show abbreviation on score board.
        /// </summary>
        public bool ShowAbbreviationOnScoreboard { get; set; }
        /// <summary>
        /// Awards current EvaluationCriteria.  Values are for MechanismTemplateId.
        /// </summary>
        public IEnumerable<int> EvaluationCriteria { get; set; }
        #region Indexes
        /// <summary>
        /// MechanismTemplate entity identifier hosting the new MechanismTemplateElement
        /// </summary>
        public Nullable<int> MechanismTemplateId { get; set; }
        #endregion
        /// <summary>
        /// Create & return an empty model
        /// </summary>
        /// <returns>Empty EvaluationCriteriaAdditionModel</returns>
        public static EvaluationCriteriaAdditionModel CreateEmptyModel()
        {
            EvaluationCriteriaAdditionModel model = new EvaluationCriteriaAdditionModel();
            model.MechanismTemplateId = null;
            model.EvaluationCriteria = new List<int>();

            return model;
        }
    }
    #endregion
    #region Evaluation Criteria model base class
    /// <summary>
    /// Base data model for the Evaluation Criteria modal & grid
    /// </summary>
    public interface IEvaluationCriteriaBaseModel
    {
        #region Attributes
        /// <summary>
        /// Indicates if the evaluation criteria is an Overall criteria
        /// </summary>
        bool OverallFlag { get; set; }
        /// <summary>
        /// Indicates if the criteria is scored
        /// </summary>
        bool ScoreFlag { get; set; }
        /// <summary>
        /// Indicates if the criteria requires text input
        /// </summary>
        bool TextFlag { get; set; }
        /// <summary>
        /// Criteria word limit
        /// </summary>
        int? RecommendedWordCount { get; set; }
        /// <summary>
        /// Whether to show abbreviation on score board.
        /// </summary>
        bool ShowAbbreviationOnScoreboard { get; set; }
        /// <summary>
        /// Indicates the critique order
        /// </summary>
        int SortOrder { get; set; }
        /// <summary>
        /// Order in summary statement
        /// </summary>
        Nullable<int> SummarySortOrder { get; set; }
        /// <summary>
        /// Indicates if the criteria should be hidden when summary statement is generated.
        /// </summary>
        bool SummaryIncludeFlag { get; set; }
        /// <summary>
        /// Criteria description
        /// </summary>
        string InstructionText { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// MechanismTemplateElement defining the criteria
        /// </summary>
        int MechanismTemplateElementId { get; set; }
        #endregion
    }
    /// <summary>
    /// Base data model for the Evaluation Criteria modal & grid
    /// </summary>
    public class EvaluationCriteriaBaseModel : IEvaluationCriteriaBaseModel
    {
        #region Attributes
        /// <summary>
        /// Indicates if the evaluation criteria is an Overall criteria
        /// </summary>
        public bool OverallFlag { get; set; }
        /// <summary>
        /// Indicates if the criteria is scored
        /// </summary>
        public bool ScoreFlag { get; set; }
        /// <summary>
        /// Indicates if the criteria requires text input
        /// </summary>
        public bool TextFlag { get; set; }
        /// <summary>
        /// Criteria word limit
        /// </summary>
        public int? RecommendedWordCount { get; set; }
        /// <summary>
        /// Whethere to show abbreviation on score board.
        /// </summary>
        public bool ShowAbbreviationOnScoreboard { get; set; }
        /// <summary>
        /// Indicates the critique order
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// Order in summary statement
        /// </summary>
        public Nullable<int> SummarySortOrder { get; set; }
        /// <summary>
        /// Indicates if the criteria should be hidden when summary statement is generated.
        /// </summary>
        public bool SummaryIncludeFlag { get; set; }
        /// <summary>
        /// Criteria description
        /// </summary>
        public string InstructionText { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// MechanismTemplateElement defining the criteria
        /// </summary>
        public int MechanismTemplateElementId { get; set; }
        #endregion
    }
    
    #endregion
    #region Evaluation Criteia grid
    /// <summary>
    /// Data model returned to populate the Setup Evaluation Criteria grid
    /// </summary>
    public interface IEvaluationCriteriaModel: IEvaluationCriteriaBaseModel
    {
        #region Attributes
        /// <summary>
        /// Evaluation criteria
        /// </summary>
        string ElementAbbreviation { get; set; }
        /// <summary>
        /// Element description
        /// </summary>
        string ElementDescription { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// Parent MechanismTemplate
        /// </summary>
        int MechanismTemplateId { get; set; }
        /// <summary>
        /// ProgramMechanism parent
        /// </summary>
        int ProgramMechanismid { get; set; }
        #endregion
    }
    /// <summary>
    /// Data model returned to populate the Setup Evaluation Criteria grid
    /// </summary>
    public class EvaluationCriteriaModel : EvaluationCriteriaBaseModel, IEvaluationCriteriaModel
    {
        #region Attributes
        /// <summary>
        /// Evaluation criteria
        /// </summary>
        public string ElementAbbreviation { get; set; }
        /// <summary>
        /// Element description
        /// </summary>
        public string ElementDescription { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// Parent MechanismTemplate
        /// </summary>
        public int MechanismTemplateId { get; set; }
        /// <summary>
        /// ProgramMechanism parent
        /// </summary>
        public int ProgramMechanismid { get; set; }
        #endregion
    }
    #endregion
    #region Evaluation Criteria Modal
    /// <summary>
    /// Data model returned to populate the Setup Evaluation Criteria Setup modal
    /// </summary>
    public interface IEvaluationCriteriaModalModel: IEvaluationCriteriaBaseModel
    {
        #region Attributes
        /// <summary>
        /// Awards current EvaluationCriteria.  Values are for MechanismTemplateId.
        /// </summary>
        IEnumerable<int> EvaluationCriteria { get; set; }
        /// <summary>
        /// Evaluation Criteria full description
        /// </summary>
        string ElementDescription { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// ClientElement
        /// </summary>
        int ClientElementId { get; set; }
        #endregion
    }
    /// <summary>
    /// Data model returned to populate the Setup Evaluation Criteria Setup modal
    /// </summary>
    public class EvaluationCriteriaModalModel : EvaluationCriteriaBaseModel, IEvaluationCriteriaModalModel
    {
        #region Attributes
        /// <summary>
        /// Awards current EvaluationCriteria.  Values are for MechanismTemplateId.
        /// </summary>
        public IEnumerable<int> EvaluationCriteria { get; set; }
        /// <summary>
        /// Evaluation Criteria full description
        /// </summary>
        public string ElementDescription { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// ClientElement
        /// </summary>
        public int ClientElementId { get; set; }
        #endregion
    } 
    #endregion
}
