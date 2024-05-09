using System;

namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Data model returned to populate the Evaluation Criteria header
    /// </summary>
    public interface IEvaluationCriteriaHeaderModel
    {
        #region Attributes
        /// <summary>
        /// Client Award Abbreviation
        /// </summary>
        string ClientAbrv { get; set; }
        /// <summary>
        /// Evaluation criteria is for this Program
        /// </summary>
        string ProgramDescription { get; set; }
        /// <summary>
        /// Evaluation criteria is for this program's year
        /// </summary>
        string Year { get; set; }
        /// <summary>
        /// The Program's abbreviation
        /// </summary>
        string ProgramAbbreviation { get; set; }
        /// <summary>
        /// The ReceiptCycle it belongs to
        /// </summary>
        int? ReceiptCycle { get; set; }
        /// <summary>
        /// The FundingOpportunityId that will be evaluated
        /// </summary>
        string FundingOpportunityId { get; set; }
        /// <summary>
        /// The Award abbreviation
        /// </summary>
        string AwardAbbreviation { get; set; }
        /// <summary>
        /// Indicates if the award is blinded
        /// </summary>
        bool Blinded { get; set; }
        /// <summary>
        /// Indicates if partnering is allowed
        /// </summary>
        bool PartneringPiAllowedFlag { get; set; }
        /// <summary>
        /// Indicates if any applications have been released;
        /// </summary>
        bool HasApplicationsBeenReleased { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// ProgramMechanism entity identifier
        /// </summary>
        int ProgramMechanismId { get; set; }
        /// <summary>
        /// Client entity identifier
        /// </summary>
        int ClientId { get; set; }
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        int ProgramYearId { get; set; }
        /// <summary>
        /// MechanismTemplateId
        /// </summary>
        int? MechanismTemplateId { get; set; }
        /// <summary>
        /// ScoringTemplate entity identifier
        /// </summary>
        int? ScoringTemplateId { get; set; }
        /// <summary>
        /// Gets or sets the mechanism scoring template identifier.
        /// </summary>
        /// <value>
        /// The mechanism scoring template identifier.
        /// </value>
        int? MechanismScoringTemplateId { get; set; }
        #endregion
    }
    /// <summary>
    /// Data model returned to populate the Evaluation Criteria header
    /// </summary>
    public class EvaluationCriteriaHeaderModel: IEvaluationCriteriaHeaderModel
    {
        #region Attributes
        /// <summary>
        /// Client Award Abbreviation
        /// </summary>
        public string ClientAbrv { get; set; }
        /// <summary>
        /// Evaluation criteria is for this Program
        /// </summary>
        public string ProgramDescription { get; set; }
        /// <summary>
        /// Evaluation criteria is for this program's year
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// The Program's abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// The ReceiptCycle it belongs to
        /// </summary>
        public int? ReceiptCycle { get; set; }
        /// <summary>
        /// The FundingOpportunityId that will be evaluated
        /// </summary>
        public string FundingOpportunityId { get; set; }
        /// <summary>
        /// The Award abbreviation
        /// </summary>
        public string AwardAbbreviation { get; set; }
        /// <summary>
        /// Indicates if the award is blinded
        /// </summary>
        public bool Blinded { get; set; }
        /// <summary>
        /// Indicates if partnering is allowed
        /// </summary>
        public bool PartneringPiAllowedFlag { get; set; }
        /// <summary>
        /// Indicates if any applications have been released;
        /// </summary>
        public bool HasApplicationsBeenReleased { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// ProgramMechanism entity identifier
        /// </summary>
        public int ProgramMechanismId { get; set; }
        /// <summary>
        /// Client entity identifier
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        public int ProgramYearId { get; set; }
        /// <summary>
        /// MechanismTemplateId
        /// </summary>
        public int? MechanismTemplateId { get; set; }
        /// <summary>
        /// ScoringTemplate entity identifier
        /// </summary>
        public int? ScoringTemplateId { get; set; }
        /// <summary>
        /// Gets or sets the mechanism scoring template identifier.
        /// </summary>
        /// <value>
        /// The mechanism scoring template identifier.
        /// </value>
        public int? MechanismScoringTemplateId { get; set; }
        #endregion
    }
}
