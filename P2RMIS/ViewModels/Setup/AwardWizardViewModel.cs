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
    public class AwardWizardViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AwardWizardViewModel"/> class.
        /// </summary>
        public AwardWizardViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwardWizardViewModel"/> class.
        /// </summary>
        /// <param name="awardMechanismModel">The award mechanism model.</param>
        public AwardWizardViewModel(IAwardMechanismModel awardMechanismModel)
        {
            Cycle = (int)awardMechanismModel.ReceiptCycle;
            OpportunityId = awardMechanismModel.FundingOpportunityId;
            AwardMechanism = awardMechanismModel.AwardDescription;
            IsPartnering = awardMechanismModel.PartneringPiAllowedFlag;
            IsBlinded = awardMechanismModel.BlindedFlag;
            PreAppCycle = awardMechanismModel.PreAppReceiptCycle != null ?
                awardMechanismModel.PreAppReceiptCycle.ToString() : Invariables.Labels.NA;
            ProgramMechanismId = awardMechanismModel.ProgramMechanismId;
            ClientProgramId = awardMechanismModel.ClientProgramId;
        }

        public AwardWizardViewModel(IAwardSetupWizardModel awardWizardModel)
        {
            Cycle = (int)awardWizardModel.ReceiptCycle;
            OpportunityId = awardWizardModel.FundingOpportunityId;
            IsPartnering = awardWizardModel.PartneringPiAllowedFlag;
            IsBlinded = awardWizardModel.BlindedFlag;
            AwardTypeId = awardWizardModel.ClientAwardTypeId;
            ProgramMechanismId = awardWizardModel.ProgramMechanismId;
            ClientProgramId = awardWizardModel.ClientProgramId;
            ReceiptDeadline = awardWizardModel.ReceiptDeadline;
            ParentProgramMechanismId = awardWizardModel.ParentProgramMechanismId;
        }

        /// <summary>
        /// Gets or sets the program mechanism identifier.
        /// </summary>
        /// <value>
        /// The program mechanism identifier.
        /// </value>
        public int? ProgramMechanismId { get; set; }

        /// <summary>
        /// Gets or sets the client program identifier.
        /// </summary>
        /// <value>
        /// The client program identifier.
        /// </value>
        public int ClientProgramId { get; set; }

        /// <summary>
        /// Gets or sets the award types.
        /// </summary>
        /// <value>
        /// The award types.
        /// </value>
        [JsonProperty("awardTypes")]
        public List<AwardTypeViewModel> AwardTypes { get; set; }

        /// <summary>
        /// Gets or sets the award type identifier.
        /// </summary>
        /// <value>
        /// The award type identifier.
        /// </value>
        public int? AwardTypeId { get; set; }

        /// <summary>
        /// Gets the cycle.
        /// </summary>
        /// <value>
        /// The cycle.
        /// </value>
        public int Cycle { get; private set; }

        /// <summary>
        /// Gets the opportunity identifier.
        /// </summary>
        /// <value>
        /// The opportunity identifier.
        /// </value>
        public string OpportunityId { get; private set; }

        /// <summary>
        /// Gets the award mechanism.
        /// </summary>
        /// <value>
        /// The award mechanism.
        /// </value>
        public string AwardMechanism { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is partnering.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is partnering; otherwise, <c>false</c>.
        /// </value>
        public bool IsPartnering { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is blinded.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is blinded; otherwise, <c>false</c>.
        /// </value>
        public bool IsBlinded { get; private set; }
        
        /// <summary>
        /// Gets the pre application cycle.
        /// </summary>
        /// <value>
        /// The pre application cycle.
        /// </value>
        public string PreAppCycle { get; private set; }

        /// <summary>
        /// Gets the receipt deadline.
        /// </summary>
        /// <value>
        /// The receipt deadline.
        /// </value>
        public DateTime? ReceiptDeadline { get; private set; }
        /// <summary>
        /// Parent program mechanism entity identifier.
        /// </summary>
        public int? ParentProgramMechanismId { get; set; }
    }
}