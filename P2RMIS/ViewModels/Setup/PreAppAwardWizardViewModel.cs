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
    public class PreAppAwardWizardViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreAppAwardWizardViewModel"/> class.
        /// </summary>
        public PreAppAwardWizardViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreAppAwardWizardViewModel"/> class.
        /// </summary>
        /// <param name="awardMechanismModel">The award mechanism model.</param>
        public PreAppAwardWizardViewModel(IPreAppAwardSetupWizardModel awardMechanismModel)
        {
            ParentCycle = (int)awardMechanismModel.ParentReceiptCycle;
            ParentAwardName = awardMechanismModel.AwardDescription;
            ParentAwardAbbr = awardMechanismModel.AwardAbbreviation;
            ParentBlindedText = ViewHelpers.FormatBoolean(awardMechanismModel.ParentBlindedFlag);
            ParentPartneringPIsText = ViewHelpers.FormatBoolean(awardMechanismModel.PartneringPiAllowedFlag);
            ParentOpportunityId = awardMechanismModel.FundingOpportunityId;
            ParentAppDueDateText = ViewHelpers.FormatDate(awardMechanismModel.ParentReceiptDeadline);
            Cycle = awardMechanismModel.ReceiptCycle?.ToString();
            IsBlinded = awardMechanismModel.BlindedFlag;
            AppDueDate = awardMechanismModel.ReceiptDeadline;
            ProgramMechanismId = (int)awardMechanismModel.ProgramMechanismId;
            ParentProgramMechanismId = (int)awardMechanismModel.ParentProgramMechanismId;
        }

        /// <summary>
        /// Gets the program mechanism identifier.
        /// </summary>
        /// <value>
        /// The program mechanism identifier.
        /// </value>
        public int ProgramMechanismId { get; private set; }

        /// <summary>
        /// Gets the parent program mechanism identifier.
        /// </summary>
        /// <value>
        /// The parent program mechanism identifier.
        /// </value>
        public int ParentProgramMechanismId { get; set; }

        /// <summary>
        /// Gets the parent cycle.
        /// </summary>
        /// <value>
        /// The parent cycle.
        /// </value>
        public int ParentCycle { get; private set; }

        /// <summary>
        /// Gets the name of the parent award.
        /// </summary>
        /// <value>
        /// The name of the parent award.
        /// </value>
        public string ParentAwardName { get; private set; }

        /// <summary>
        /// Gets the parent award abbr.
        /// </summary>
        /// <value>
        /// The parent award abbr.
        /// </value>
        public string ParentAwardAbbr { get; private set; }

        /// <summary>
        /// Gets the parent blinded text.
        /// </summary>
        /// <value>
        /// The parent blinded text.
        /// </value>
        public string ParentBlindedText { get; private set; }

        /// <summary>
        /// Gets the parent partnering p is text.
        /// </summary>
        /// <value>
        /// The parent partnering p is text.
        /// </value>
        public string ParentPartneringPIsText { get; private set; }

        /// <summary>
        /// Gets the opportunity identifier.
        /// </summary>
        /// <value>
        /// The opportunity identifier.
        /// </value>
        public string ParentOpportunityId { get; private set; }

        /// <summary>
        /// Gets the parent application due text.
        /// </summary>
        /// <value>
        /// The parent application due text.
        /// </value>
        public string ParentAppDueDateText { get; private set; }

        /// <summary>
        /// Gets the cycle.
        /// </summary>
        /// <value>
        /// The cycle.
        /// </value>
        public string Cycle { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is blinded.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is blinded; otherwise, <c>false</c>.
        /// </value>
        public bool IsBlinded { get; private set; }

        /// <summary>
        /// Gets the application due date.
        /// </summary>
        /// <value>
        /// The application due date.
        /// </value>
        public DateTime? AppDueDate { get; private set; }

        /// <summary>
        /// Gets the blinded list.
        /// </summary>
        /// <value>
        /// The blinded list.
        /// </value>
        public List<KeyValuePair<bool, string>> BlindedList
        {
            get
            {
                var list = new List<KeyValuePair<bool, string>>();
                list.Add(new KeyValuePair<bool, string>(true, Invariables.Labels.Yes));
                list.Add(new KeyValuePair<bool, string>(false, Invariables.Labels.No));
                return list;
            }
        }
    }
}