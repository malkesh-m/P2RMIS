using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.CrossCuttingServices;
using Newtonsoft.Json;

namespace Sra.P2rmis.Web.UI.Models
{
    public class CriterionSetupViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CriterionSetupViewModel"/> class.
        /// </summary>
        public CriterionSetupViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriterionSetupViewModel"/> class.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="criteria">The criteria.</param>
        /// <param name="scoringTemplates">The scoring templates.</param>
        public CriterionSetupViewModel(IEvaluationCriteriaHeaderModel header,
            List<IEvaluationCriteriaModel> criteria, List<IListEntry> scoringTemplates)
        {
            ClientId = header.ClientId;
            MechanismTemplateId = header.MechanismTemplateId;
            ProgramMechanismId = header.ProgramMechanismId;
            Client = header.ClientAbrv;
            Program = header.ProgramAbbreviation;
            FiscalYear = header.Year;
            ReceiptCycle = header.ReceiptCycle != null ? header.ReceiptCycle.ToString() : Invariables.Labels.NA;
            Award = header.AwardAbbreviation;
            ScoringTemplateId = header.ScoringTemplateId;
            MechanismScoringTemplateId = header.MechanismScoringTemplateId;
            HasApplicationsBeenReleased = header.HasApplicationsBeenReleased;
            BlindedText = ViewHelpers.FormatBoolean(header.Blinded);
            ParnteringText = ViewHelpers.FormatBoolean(header.PartneringPiAllowedFlag);
            ScoringTemplates = scoringTemplates.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            Criteria = criteria.ConvertAll(x => new CriterionViewModel(x))
                .Select((item, index) => {
                    item.Index = index + 1;
                    item.AssignmentsReleased = header.HasApplicationsBeenReleased;
                    return item; }).ToList();
        }

        /// <summary>
        /// Gets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public int ClientId { get; private set; }

        /// <summary>
        /// Gets the mechanism template identifier.
        /// </summary>
        /// <value>
        /// The mechanism template identifier.
        /// </value>
        public int? MechanismTemplateId { get; private set; }

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public string Client { get; private set; }

        /// <summary>
        /// Gets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public string Program { get; private set; }

        /// <summary>
        /// Gets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        public string FiscalYear { get; private set; }

        /// <summary>
        /// Gets the receipt cycle.
        /// </summary>
        /// <value>
        /// The receipt cycle.
        /// </value>
        public string ReceiptCycle { get; private set; }

        /// <summary>
        /// Gets the award.
        /// </summary>
        /// <value>
        /// The award.
        /// </value>
        public string Award { get; private set; }

        /// <summary>
        /// Gets the blinded text.
        /// </summary>
        /// <value>
        /// The blinded text.
        /// </value>
        public string BlindedText { get; private set; }

        /// <summary>
        /// Gets the parntering text.
        /// </summary>
        /// <value>
        /// The parntering text.
        /// </value>
        public string ParnteringText { get; private set; }

        /// <summary>
        /// Gets the scoring templates.
        /// </summary>
        /// <value>
        /// The scoring templates.
        /// </value>
        public List<KeyValuePair<int, string>> ScoringTemplates { get; private set; }

        /// <summary>
        /// Gets the scoring template identifier.
        /// </summary>
        /// <value>
        /// The scoring template identifier.
        /// </value>
        public int? ScoringTemplateId { get; private set; }
        /// <summary>
        /// Gets the name of the scoring template.
        /// </summary>
        /// <value>
        /// The name of the scoring template.
        /// </value>
        public string ScoringTemplateName {
            get
            {
                return ScoringTemplates?.Where(x => x.Key == ScoringTemplateId)?.FirstOrDefault().Value;
            }
        }
        /// <summary>
        /// Gets the mechanism scoring template identifier.
        /// </summary>
        /// <value>
        /// The mechanism scoring template identifier.
        /// </value>
        public int? MechanismScoringTemplateId { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance has applications been released.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has applications been released; otherwise, <c>false</c>.
        /// </value>
        public bool HasApplicationsBeenReleased { get; private set; }

        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        /// <value>
        /// The criteria.
        /// </value>
        [JsonProperty("criteria")]
        public List<CriterionViewModel> Criteria { get; set; }

        /// <summary>
        /// Gets a value indicating the Id of the mechanism.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has an Id <c>false</c>.
        /// </value>
        public int ProgramMechanismId { get; private set; }
    }
}