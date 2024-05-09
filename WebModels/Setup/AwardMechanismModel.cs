using System;

namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Data model returned to populate the Award/Mechanism grid
    /// </summary>
    public interface IAwardMechanismModel
    {
        #region The Data
        /// <summary>
        /// Fiscal year
        /// </summary>
        string FiscalYear { get; set; }
        /// <summary>
        /// Client abbreviation
        /// </summary>
        string ClientAbbreviation { get; set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Legacy ATM Identifier
        /// </summary>
        int? LegacyAtmId { get; set; }
        /// <summary>
        /// Receipt cycle
        /// </summary>
        Nullable<int> ReceiptCycle { get; set; }
        /// <summary>
        /// Funding opportunity identifier
        /// </summary>
        string FundingOpportunityId { get; set; }
        /// <summary>
        /// Award abbreviation
        /// </summary>
        string AwardAbbreviation { get; set; }
        /// <summary>
        /// Legacy award type identifier
        /// </summary>
        string LegacyAwardTypeId { get; set; }
        /// <summary>
        /// Award description
        /// </summary>
        string AwardDescription { get; set; }
        /// <summary>
        /// Allow partnering
        /// </summary>
        bool PartneringPiAllowedFlag { get; set; }
        /// <summary>
        /// Is PI blinded from reviewers?
        /// </summary>
        bool BlindedFlag { get; set; }
        /// <summary>
        /// Receipt date 
        /// </summary>
        DateTime? ReceiptDeadline { get; set; }
        /// <summary>
        /// Indicates if the Award/Mechanism is a child
        /// </summary>
        bool IsChild { get; set; }
        /// <summary>
        /// Number of Criteria in the mechanism
        /// </summary>
        int CriteriaCount { get; set; }
        /// <summary>
        /// Pre-app receipt cycle
        /// </summary>
        Nullable<int> PreAppReceiptCycle { get; set; }
        #endregion
        #region The Indexes
        /// <summary>
        /// Client entity identifier
        /// </summary>
        int ClientId { get; set; }
        /// <summary>
        /// ClientProgram entity identifier
        /// </summary>
        int ClientProgramId { get; set; }
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        int ProgramYearId { get; set; }
        /// <summary>
        /// ProgramMechanism entity identifier
        /// </summary>
        int ProgramMechanismId { get; set; }
        /// <summary>
        /// Parent ProgramMechanism entity identifier.  If non-null
        /// is is a pre-app mechanism
        /// </summary>
        Nullable<int> ParentProgramMechanismId { get; set; }
        /// <summary>
        /// Whether there are assigned applications.
        /// </summary>
        bool HasApplications { get; set; }
        #endregion
    }
    /// <summary>
    /// Data model returned to populate the Award/Mechanism grid
    /// </summary>
    public class AwardMechanismModel: IAwardMechanismModel
    {
        #region Attributes
        /// <summary>
        /// Fiscal year
        /// </summary>
        public string FiscalYear { get; set; }
        /// <summary>
        /// Client abbreviation
        /// </summary>
        public string ClientAbbreviation { get; set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Legacy ATM identifier
        /// </summary>
        public int? LegacyAtmId { get; set; }
        /// <summary>
        /// Receipt cycle
        /// </summary>
        public Nullable<int> ReceiptCycle { get; set; }
        /// <summary>
        /// Funding opportunity identifier
        /// </summary>
        public string FundingOpportunityId { get; set; }
        /// <summary>
        /// Award abbreviation
        /// </summary>
        public string AwardAbbreviation { get; set; }
        /// <summary>
        /// Legacy award type identifier
        /// </summary>
        public string LegacyAwardTypeId { get; set; }
        /// <summary>
        /// Award description
        /// </summary>
        public string AwardDescription { get; set; }
        /// <summary>
        /// Allow partnering
        /// </summary>
        public bool PartneringPiAllowedFlag { get; set; }
        /// <summary>
        /// Is PI blinded from reviewers?
        /// </summary>
        public bool BlindedFlag { get; set; }
        /// <summary>
        /// Receipt date 
        /// </summary>
        public DateTime? ReceiptDeadline { get; set; }
        /// <summary>
        /// Indicates if the Award/Mechanism is a child
        /// </summary>
        public bool IsChild { get; set; }
        /// <summary>
        /// Number of Criteria in the mechanism
        /// </summary>
        public int CriteriaCount { get; set; }
        /// <summary>
        /// Pre-app receipt cycle
        /// </summary>
        public Nullable<int> PreAppReceiptCycle { get; set; }
        /// <summary>
        /// Whether there are assigned applications.
        /// </summary>
        public bool HasApplications { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// Client entity identifier
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// ClientProgram entity identifier
        /// </summary>
        public int ClientProgramId { get; set; }
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        public int ProgramYearId { get; set; }
        /// <summary>
        /// ProgramMechanism entity identifier
        /// </summary>
        public int ProgramMechanismId { get; set; }
        /// <summary>
        /// Parent ProgramMechanism entity identifier.  If non-null
        /// is is a pre-app mechanism
        /// </summary>
        public Nullable<int> ParentProgramMechanismId { get; set; }
        #endregion
    }
}
