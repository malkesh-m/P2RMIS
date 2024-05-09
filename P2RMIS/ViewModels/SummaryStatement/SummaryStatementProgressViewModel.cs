using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.SummaryStatement;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;

namespace Sra.P2rmis.Web.ViewModels
{
    /// <summary>
    /// Signature of delegate used to format the overall score.
    /// </summary>
    /// <remarks>
    /// Why is a delegate needed to format the score?  The summary statement
    /// listed on the display are retrieved by an AJAX call and the display is 
    /// constructed with JQuery.  The formatting method cannot be called from 
    /// within JQuery.  Only properties on the object can be referenced.  The
    /// object is to keep this layer independent of other layers so that is 
    /// cross cutting.  But we can declare a delegate and set the delegate anywhere.
    /// </remarks>
    /// <param name="number">Number to format</param>
    /// <returns>Number formatted as string in standard way</returns>
    public delegate string OverallScoreFormatter(decimal? number);

    public class SummaryStatementProgressViewModel
    {
        public const string NonApplicable = "N/A";
        public SummaryStatementProgressViewModel(ISummaryStatementProgressModel application)
        {
            LogNumber = application.LogNumber;
            PanelApplicationId = application.PanelApplicationId;
            MechanismAbbreviation = application.MechanismAbbreviation;
            OverallScore = application.OverallScore;
            ConcatenatedDate = ViewHelpers.FormatDate(application.ConcatenatedDate);
            CheckedOutDate = ViewHelpers.FormatDate(application.CheckOutDateTime);
            CheckedOutFirstName = application.CheckedOutUserFirstName;
            CheckedOutLastName = application.CheckedOutUserLastName;
            CheckboxEnabled = ConcatenatedDate == null;
            PiFirstName = application.PiFirstName;
            PiLastName = application.PiLastName;
            Cycle = application.Cycle;
            PanelId = application.PanelId;
            FY = application.FY;
            ProgramAbbreviation = application.ProgramAbbreviation;
            PanelAbbreviation = application.PanelAbbreviation;
            ApplicationWorkflowId = application.ApplicationWorkflowId;
            Priority = ViewHelpers.FormatBoolean(application.IsCommandDraft);
            Priority2 = ViewHelpers.FormatBoolean(application.IsQualifying);
            User = (CheckedOutLastName + ", " + CheckedOutFirstName) == ", " ? " - " : (CheckedOutLastName + ", " + CheckedOutFirstName);
            CurrentStepName = application.CurrentStepName;
            ApplicationId = application.ApplicationId;
            PostDateTime = ViewHelpers.FormatDate(application.PostDateTime);
            NotesExist = application.NotesExist;
            AdminNotesExist = application.AdminNotesExist;
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
        /// Gets a value indicating whether this <see cref="SummaryStatementApplicationViewModel"/> is checkbox.
        /// </summary>
        /// <value>
        ///   <c>true</c> if checkbox; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("checkboxEnabled")]
        public bool CheckboxEnabled { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="SummaryStatementApplicationViewModel"/> is checkbox.
        /// </summary>
        /// <value>
        ///   <c>true</c> if checkbox; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("checkbox")]
        public bool Checkbox { get; private set; }
        /// <summary>
        /// the date the application was concatenated
        /// </summary>
        public string ConcatenatedDate { get; set; }
        /// <summary>
        /// Gets or sets the checked out date.
        /// </summary>
        /// <value>
        /// The checked out date.
        /// </value>
        public string CheckedOutDate { get; set; }
        /// <summary>
        /// the applications PI
        /// </summary>
        public string PiFirstName { get; set; }
        /// <summary>
        /// the applications PI
        /// </summary>
        public string PiLastName { get; set; }
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
        /// Whether or not the application has been flagged as command draft
        /// </summary>
        public bool IsCommandDraft { get; set; }
        /// <summary>
        /// Whether or not the application has been flagged as qualifying
        /// </summary>
        public bool IsQualifying { get; set; }
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
        /// Display name of user with SS checked out
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// DB Identifier of application
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Name of step the summary statement is current in
        /// </summary>
        public string CurrentStepName { get; set; }
        /// <summary>
        /// Date time the application was posted for processing
        /// </summary>
        public string PostDateTime { get; set; }
        /// <summary>
        /// First name of user with SS checked out
        /// </summary>
        public string CheckedOutFirstName { get; set; }
        /// <summary>
        /// Last name of user with SS checked out
        /// </summary>
        public string CheckedOutLastName { get; set; }

        /// <summary>
        /// Whether notes exist for the application (discussion, reviewer, or general)
        /// </summary>
        public bool NotesExist { get; set; }

        /// <summary>
        /// Whether admin (budget) notes exist for the application
        /// </summary>
        public bool AdminNotesExist { get; set; }
    }
}