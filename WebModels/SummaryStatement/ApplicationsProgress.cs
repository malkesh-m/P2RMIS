using System;
using System.Web;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <param name="adminNoteExists">whether an admin note exists or not</param>
    /// <returns>Summary Note Html to display in the column</returns>
    public delegate decimal AdminNoteFormatter(bool adminNoteExists);
    /// <summary>
    /// Data model for displaying summary information on the summary management
    /// views.
    /// </summary>
    [Obsolete("Use SummaryStatementProgressModel instead.")]
    public class ApplicationsProgress : IApplicationsProgress
    {
        /// <summary>
        /// Applications unique identifier (the new one)
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Applications label
        /// </summary>
        public string LogNumber { get; set; }
        /// <summary>
        /// Panel application identifier
        /// </summary>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// the applications mechanism
        /// </summary>
        public string MechanismAbbreviation { get; set; }
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
        public int? Cycle { get; set; }
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
        /// Whether or not notes currently exist for the application
        /// </summary>
        public bool NotesExist { get; set; }
        /// <summary>
        /// The workflow step of the application that is next to be completed
        /// </summary>
        public int? CurrentStepId { get; set; }
        /// <summary>
        /// The name of the workflow step of the application that is next to be completed
        /// </summary>
        public string CurrentStepName { get; set; }
        /// <summary>
        /// Formatting for the qualifying column display in view
        /// </summary>
        public int QualifyingCol { get { return IsQualifying ? 1 : 0; } }
        /// <summary>
        /// Formatting for the command draft column display in view
        /// </summary>
        public int CommandDraftCol { get { return IsCommandDraft ? 1 : 0; } }
        /// <summary>
        /// the applications overall score
        /// </summary>
        public decimal? OverallScore { get; set; }
        /// <summary>
        /// Current step assigned users first name
        /// </summary>
        public string EditorFirstName { get; set; }
        /// <summary>
        /// Current step assigned users last name
        /// </summary>
        public string EditorLastName { get; set; }
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
            get { return (ScoreFormatter != null) ? ScoreFormatter(OverallScore) : OverallScore.ToString(); }
        }
        //TODO: this needs to be relocated.  It should be calling a delegate
        /// <summary>
        /// The display of the summary note icon
        /// </summary>
        public IHtmlString SummaryNoteDisplay { get { return (NotesExist) ? new HtmlString("<a href='javascript:void(0);' data-logno='" + LogNumber + "' id='noteId_" + LogNumber + "'><img src='" + Images.AdminNoteImg + "' alt='" + Images.AltAdminNoteImg + "' /></a>") : new HtmlString("-"); } }
        /// <summary>
        /// Applications workflow unique identifier
        /// </summary>
        public int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// Date the applications workflow was closed.  A null value indicates the workflow is incomplete.
        /// </summary>
        public DateTime? DateClosed { get; set; }
        /// <summary>
        /// Formatted Date
        /// </summary>
        public string FormattedDate { 
            get 
            {
                return (DateClosed != null) ? DateClosed.Value.ToShortDateString() : "";
            } 
        }
        /// <summary>
        /// Summary statement post time (time the summary statement was checked in the 
        /// last workflow step.
        /// </summary>
        public DateTime? PostTime { get; set; }
        /// <summary>
        /// Summary statement check out time (time the summary statement was checked out 
        /// for the last workflow step.
        /// </summary>
        public DateTime? CheckOutTime { get; set; }
        /// <summary>
        /// Gets or sets the generation completion date.
        /// </summary>
        /// <value>
        /// The generation completion date.
        /// </value>
        public DateTime? GenerationCompletionDate { get; set; }
    }
}
