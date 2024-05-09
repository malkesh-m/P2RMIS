using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Setup;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;

namespace Sra.P2rmis.Web.UI.Models
{
    public class AwardViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AwardViewModel"/> class.
        /// </summary>
        public AwardViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwardViewModel"/> class.
        /// </summary>
        /// <param name="awardMechanismModel">The award mechanism model.</param>
        public AwardViewModel(IAwardMechanismModel awardMechanismModel)
        {
            FiscalYear = awardMechanismModel.FiscalYear;
            ClientAbbreviation = awardMechanismModel.ClientAbbreviation;
            LegacyAtmId = awardMechanismModel.LegacyAtmId;
            ProgramYearId = awardMechanismModel.ProgramYearId;
            ProgramAbbreviation = awardMechanismModel.ProgramAbbreviation; 
            Cycle = (int)awardMechanismModel.ReceiptCycle;
            OpportunityId = awardMechanismModel.FundingOpportunityId;
            AwardDescription = awardMechanismModel.AwardDescription;
            AwardAbbr = awardMechanismModel.AwardAbbreviation;
            LegacyAwardTypeId = awardMechanismModel.LegacyAwardTypeId;
            PartneringText = ViewHelpers.FormatBoolean(awardMechanismModel.PartneringPiAllowedFlag);
            BlindedText = ViewHelpers.FormatBoolean(awardMechanismModel.BlindedFlag);
            Receipt = ViewHelpers.FormatDate(awardMechanismModel.ReceiptDeadline);
            PreAppCycle = awardMechanismModel.PreAppReceiptCycle != null ?
                awardMechanismModel.PreAppReceiptCycle.ToString() : Invariables.Labels.NA;
            IsPreAppCycle = awardMechanismModel.IsChild;
            CriteriaCount = awardMechanismModel.CriteriaCount;
            ProgramMechanismId = awardMechanismModel.ProgramMechanismId;
            ParentProgramMechanismId = awardMechanismModel.ParentProgramMechanismId;
            HasApplications = awardMechanismModel.HasApplications;
        }

        /// <summary>
        /// Gets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        [JsonProperty("fiscalYear")]
        public string FiscalYear { get; private set; }

        /// <summary>
        /// Gets the client abbreviation.
        /// </summary>
        /// <value>
        /// The client abbreviation.
        /// </value>
        [JsonProperty("clientAbbreviation")]
        public string ClientAbbreviation { get; private set; }

        /// <summary>
        /// Gets the legacy ATM identifier.
        /// </summary>
        /// <value>
        /// The legacy ATM identifier.
        /// </value>
        [JsonProperty("legacyAtmId")]
        public int? LegacyAtmId { get; private set; }

        /// <summary>
        /// Gets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        [JsonProperty("programYearId")]
        public int ProgramYearId { get; private set; }

        /// <summary>
        /// Gets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        [JsonProperty("programAbbreviation")]
        public string ProgramAbbreviation { get; private set; }

        /// <summary>
        /// Gets the cycle.
        /// </summary>
        /// <value>
        /// The cycle.
        /// </value>
        [JsonProperty("cycle")]
        public int Cycle { get; private set; }

        /// <summary>
        /// Gets the opportunity identifier.
        /// </summary>
        /// <value>
        /// The opportunity identifier.
        /// </value>
        [JsonProperty("oppID")]
        public string OpportunityId { get; private set; }

        /// <summary>
        /// Gets the award abbr.
        /// </summary>
        /// <value>
        /// The award abbr.
        /// </value>
        [JsonProperty("awardAbbr")]
        public string AwardAbbr { get; private set; }

        /// <summary>
        /// Gets the legacy award type identifier.
        /// </summary>
        /// <value>
        /// The legacy award type identifier.
        /// </value>
        [JsonProperty("legacyAwardTypeId")]
        public string LegacyAwardTypeId { get; private set; }

        /// <summary>
        /// Gets the award description.
        /// </summary>
        /// <value>
        /// The award description.
        /// </value>
        [JsonProperty("awardDescription")]
        public string AwardDescription { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is partnering.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is partnering; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("partnering")]
        public string PartneringText { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is blinded.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is blinded; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("blinded")]
        public string BlindedText { get; private set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        [JsonProperty("index")]
        public int Index { get; set; }

        /// <summary>
        /// Gets the receipt.
        /// </summary>
        /// <value>
        /// The receipt.
        /// </value>
        [JsonProperty("receipt")]
        public string Receipt { get; private set; }

        /// <summary>
        /// Gets the pre application cycle.
        /// </summary>
        /// <value>
        /// The pre application cycle.
        /// </value>
        [JsonProperty("preAppCycle")]
        public string PreAppCycle { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is pre application cycle.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is pre application cycle; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("isPreAppCycle")]
        public bool IsPreAppCycle { get; private set; }

        /// <summary>
        /// Gets the number of criteria.
        /// </summary>
        /// <value>
        /// The number of criteria.
        /// </value>
        [JsonProperty("criteriaCount")]
        public int CriteriaCount { get; private set; }

        /// <summary>
        /// Gets the program mechanism identifier.
        /// </summary>
        /// <value>
        /// The program mechanism identifier.
        /// </value>
        [JsonProperty("programMechanismId")]
        public int ProgramMechanismId { get; private set; }

        /// <summary>
        /// Gets the parent program mechanism identifier.
        /// </summary>
        /// <value>
        /// The parent program mechanism identifier.
        /// </value>
        [JsonProperty("parentProgramMechanismId")]
        public int? ParentProgramMechanismId { get; private set; }
        /// <summary>
        /// Whether there are assigned applications.
        /// </summary>
        [JsonProperty("hasApplications")]
        public bool HasApplications { get; private set; }
    }
}