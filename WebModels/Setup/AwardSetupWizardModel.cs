using System;

namespace Sra.P2rmis.WebModels.Setup
{
    #region Add/Edit Primary Award
    /// <summary>
    /// Data model returned to populate the Award Setup Wizard modal
    /// </summary>
    public interface IAwardSetupWizardModel
    {
        #region Attributes
        /// <summary>
        /// Client Abbreviation
        /// </summary>
        string Client { get; set; }
        /// <summary>
        /// Client program description
        /// </summary>
        string Program { get; set; }
        /// <summary>
        /// Program fiscal year
        /// </summary>
        string Year { get; set; }
        /// <summary>
        /// Indicates if the program is active
        /// </summary>
        bool Active { get; set; }
        /// <summary>
        /// Receipt cycle
        /// </summary>
        Nullable<int> ReceiptCycle { get; set; }
        /// <summary>
        /// Receipt date 
        /// </summary>
        DateTime? ReceiptDeadline { get; set; }
        /// <summary>
        /// Is PI blinded from reviewers?
        /// </summary>
        bool BlindedFlag { get; set; }
        /// <summary>
        /// Funding opportunity identifier
        /// </summary>
        string FundingOpportunityId { get; set; }
        /// <summary>
        /// Allow partnering
        /// </summary>
        bool PartneringPiAllowedFlag { get; set; }
        #endregion
        #region Indexes
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
        /// ClientAwardType entity identifier
        /// </summary>
        Nullable<int> ClientAwardTypeId { get; set; }
        /// <summary>
        /// ProgramMechanism entity identifier
        /// </summary>
        Nullable<int> ProgramMechanismId { get; set; }
        /// <summary>
        /// Parent program mechanism entity identifier.
        /// </summary>
        int? ParentProgramMechanismId { get; set; }
        #endregion
    }
    /// <summary>
    /// Data model returned to populate the Award Setup Wizard modal
    /// </summary>
    public class AwardSetupWizardModel : IAwardSetupWizardModel
    {
        #region Attributes
        /// <summary>
        /// Client Abbreviation
        /// </summary>
        public string Client {get; set;}
        /// <summary>
        /// Client program description
        /// </summary>
        public string Program { get; set; }
        /// <summary>
        /// Program fiscal year
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// Indicates if the program is active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Receipt cycle
        /// </summary>
        public Nullable<int> ReceiptCycle { get; set; }
        /// <summary>
        /// Receipt date 
        /// </summary>
        public DateTime? ReceiptDeadline { get; set; }
        /// <summary>
        /// Is PI blinded from reviewers?
        /// </summary>
        public bool BlindedFlag { get; set; }
        /// <summary>
        /// Funding opportunity identifier
        /// </summary>
        public string FundingOpportunityId { get; set; }
        /// <summary>
        /// Allow partnering
        /// </summary>
        public bool PartneringPiAllowedFlag { get; set; }
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
        /// ClientAwardType entity identifier
        /// </summary>
        public Nullable<int> ClientAwardTypeId { get; set; }
        /// <summary>
        /// ProgramMechanism entity identifier
        /// </summary>
        public Nullable<int> ProgramMechanismId { get; set; }
        /// <summary>
        /// Parent program mechanism entity identifier.
        /// </summary>
        public int? ParentProgramMechanismId { get; set; }
        #endregion
    }
    #endregion
    
}