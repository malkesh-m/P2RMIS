using System;
using System.Web;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
     /// <summary>
    /// Data model for displaying summary information on the summary management
    /// views.
    /// </summary>
    public interface IApplicationsProgress
    {    
        /// <summary>
        /// Applications unique identifier (the new one)
        /// </summary>
        int ApplicationId { get; set; }
        /// <summary>
        /// Applications label
        /// </summary>
        string LogNumber { get; set; }
        /// <summary>
        /// Panel application identifier
        /// </summary>
        int PanelApplicationId { get; set; }
        /// <summary>
        /// the applications mechanism
        /// </summary>
        string MechanismAbbreviation { get; set; }
        /// <summary>
        /// the applications PI
        /// </summary>
        string PiFirstName { get; set; }
        /// <summary>
        /// the applications PI
        /// </summary>
        string PiLastName { get; set; }
        /// <summary>
        /// the application's cycle
        /// </summary>
        int? Cycle { get; set; }
        ///<summary>
        /// The panel's panelId when the application was evaluated
        /// </summary>
        int? PanelId { get; set; }
        /// <summary>
        /// The application's fiscal year
        /// </summary>
        string FY { get; set; }
        /// <summary>
        /// The application's program abbreviation
        /// </summary>
        ///string ProgramAbbreviation { get; set; }
        /// <summary>
        /// The panel abbreviation that evaluated the application
        /// </summary>
        string PanelAbbreviation { get; set; }
        /// <summary>
        /// Whether or not the application has been flagged as command draft
        /// </summary>
        bool IsCommandDraft { get; set; }
        /// <summary>
        /// Whether or not the application has been flagged as qualifying
        /// </summary>
        bool IsQualifying { get; set; }
        /// <summary>
        /// Whether or not summary notes currently exist for the application
        /// </summary>
        bool NotesExist { get; set; }
        /// <summary>
        /// The workflow step of the application that is next to be completed
        /// </summary>
        int? CurrentStepId { get; set; }
        /// <summary>
        /// The name of the workflow step of the application that is next to be completed
        /// </summary>
        string CurrentStepName { get; set; }
        /// <summary>
        /// Formatting for the qualifying column display in view
        /// </summary>
        int QualifyingCol { get; }
        /// <summary>
        /// Formatting for the command draft column display in view
        /// </summary>
        int CommandDraftCol { get; }
        /// <summary>
        /// the applications overall score
        /// </summary>
        decimal? OverallScore { get; set; }
        /// <summary>
        /// current editors first name
        /// </summary>
        string EditorFirstName { get; set; }
        /// <summary>
        /// current editors first name
        /// </summary>
        string EditorLastName { get; set; }
        /// <summary>
        /// Overall score formatted by specific business rules.  If no formatting delegate is defined
        /// an unformatted score is returned.
        /// </summary>
        string FormattedScore { get; }
        /// <summary>
        /// The display of the summary note icon
        /// </summary>
        IHtmlString SummaryNoteDisplay { get; }
        /// <summary>
        /// Applications workflow unique identifier
        /// </summary>
        int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// Date the applications workflow was closed.  A null value indicates the workflow is incomplete.
        /// </summary>
        DateTime? DateClosed { get; set; }
        /// <summary>
        /// Formatted Date
        /// </summary>
        string FormattedDate { get; }
        /// <summary>
        /// Summary statement post time (time the summary statement was checked in the 
        /// last workflow step.
        /// </summary>
        DateTime? PostTime { get; set; }
        /// <summary>
        /// Summary statement check out time (time the summary statement was checked out 
        /// for the last workflow step.
        /// </summary>
        DateTime? CheckOutTime { get; set; }
        /// <summary>
        /// Gets or sets the generation completion date.
        /// </summary>
        /// <value>
        /// The generation completion date.
        /// </value>
        DateTime? GenerationCompletionDate { get; set; }
    }
}
