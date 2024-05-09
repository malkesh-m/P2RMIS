using System;
using Sra.P2rmis.WebModels.SummaryStatement;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.ViewModels.SummaryStatement
{
    public class CompletedApplicationViewModel
    {
        public const string NonApplicable = "N/A";
        public CompletedApplicationViewModel(IApplicationsProgress application)
        {
            LogNumber = application.LogNumber;
            PanelApplicationId = application.PanelApplicationId;
            MechanismAbbreviation = application.MechanismAbbreviation;
            OverallScore = application.OverallScore;
            Cycle = application.Cycle;
            PanelId = application.PanelId;
            FY = application.FY;
            PanelAbbreviation = application.PanelAbbreviation;
            ApplicationWorkflowId = application.ApplicationWorkflowId;
            Priority = ViewHelpers.FormatBoolean(application.IsCommandDraft);
            Priority2 = ViewHelpers.FormatBoolean(application.IsQualifying);
            ApplicationId = application.ApplicationId;
            GenerateDate = application.GenerationCompletionDate;
        }

        /// <summary>
        /// Applications unique identifier (Application Log Number)
        /// </summary>
        [JsonProperty("appId")]
        public string LogNumber { get; private set; }
        /// <summary>
        /// Panel application identifier
        /// </summary>
        [JsonProperty("panel")]
        public int PanelApplicationId { get; private set; }
        /// <summary>
        /// the applications mechanism
        /// </summary>
        [JsonProperty("awardMechanism")]
        public string MechanismAbbreviation { get; private set; }
        /// <summary>
        /// the applications overall score
        /// </summary>
        [JsonProperty("score")]
        public decimal? OverallScore { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="StagedApplicationViewModel"/> is checkbox.
        /// </summary>
        /// <value>
        ///   <c>true</c> if checkbox; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("checkboxEnabled")]
        public bool CheckboxEnabled { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="StagedApplicationViewModel"/> is checkbox.
        /// </summary>
        /// <value>
        ///   <c>true</c> if checkbox; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("checkbox")]
        public bool Checkbox { get; private set; }
        /// <summary>
        /// the application's cycle
        /// </summary>
        [JsonProperty("cycle")]
        public int? Cycle { get; private set; }
        /// <summary>
        /// The panel's panelId when the application was evaluated
        /// </summary>
        public int? PanelId { get; set; }
        /// <summary>
        /// The application's fiscal year
        /// </summary>
        public string FY { get; set; }
        /// <summary>
        /// The application's program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// The panel abbreviation that evaluated the application
        /// </summary>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// The workflow the application is using
        /// </summary>
        [JsonProperty("workflow")]
        public string Workflow { get; set; }
        /// <summary>
        /// The workflows unique identifier
        /// </summary>
        public int? WorkflowId { get; set; }
        /// <summary>
        /// delegate used to format the overall score
        /// </summary>
        public static OverallScoreFormatter ScoreFormatter { get; set; }
        /// <summary>
        /// Overall score formatted by specific business rules.  If no formatting delegate is defined
        /// an unformatted score is returned.
        /// </summary>
        public string FormattedScore
        {
            get { return (ScoreFormatter != null) ? ScoreFormatter(OverallScore) : NonApplicable; }
        }
        /// <summary>
        /// The id for the application's instance of a summary workflow, if started
        /// </summary>
        public int? ApplicationWorkflowId { get; set; }
        /// <summary>
        /// Priority value
        /// </summary>
        [JsonProperty("priorityOne")]
        public string Priority { get; private set; }
        /// <summary>
        /// Priority value
        /// </summary>
        [JsonProperty("priorityTwo")]
        public string Priority2 { get; private set; }
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        [JsonProperty("index")]
        public int Index { get; set; }
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the generate date.
        /// </summary>
        /// <value>
        /// The generate date.
        /// </value>
        public DateTime? GenerateDate { get; set; }
    }
}