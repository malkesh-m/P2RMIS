using System;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// class representing the available applications to be pushed to SS workflow
    /// </summary>
    public interface IAvailableApplications
    {
        /// <summary>
        /// Applications unique identifier (Application Log Number)
        /// </summary>
        string ApplicationId { get; set; }
        /// <summary>
        /// Panel application identifier
        /// </summary>
        int PanelApplicationId { get; set; }
        /// <summary>
        /// the applications mechanism
        /// </summary>
        string MechanismAbbreviation { get; set; }
        /// <summary>
        /// the applications overall score
        /// </summary>
        decimal? OverallScore { get; set; }
        /// <summary>
        /// the date the application was concatenated
        /// </summary>
        DateTime? ConcatenatedDate { get; set; }
        /// <summary>
        /// the applications PI
        /// </summary>
        string PiFirstName { get; set; }
        /// <summary>
        /// the applications PI
        /// </summary>
        string PiLastName { get; set; }
        ///// <summary>
        ///// the application's cycle
        ///// </summary>
        //int? Cycle { get; set; }
        /// <summary>
        /// The panel's panelId when the application was evaluated
        /// </summary>
        ///int? PanelId { get; set; }
        /// <summary>
        /// The application's fiscal year
        /// </summary>
        string FY { get; set; }
        /// <summary>
        /// The application's program abbreviation
        /// </summary>
        ///string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Whether or not the application has been flagged as command draft
        /// </summary>
        bool IsCommandDraft { get; set; }
        /// <summary>
        /// Whether or not the application has been flagged as qualifying
        /// </summary>
        bool IsQualifying { get; set; }
        /// <summary>
        /// Formatting for the qualifying column display in view
        /// </summary>
        int QualifyingCol { get; }
        /// <summary>
        /// Formatting for the command draft column display in view
        /// </summary>
        int CommandDraftCol { get; }
        /// <summary>
        /// The workflow the application is using
        /// </summary>
        string Workflow { get; set; }
        /// <summary>
        /// The workflows unique identifier
        /// </summary>
        int? WorkflowId { get; set; }
        /// <summary>
        /// Overall score formatted by specific business rules.  If no formatting delegate is defined
        /// an unformatted score is returned.
        /// </summary>
        string FormattedScore { get; }

        /// <summary>
        /// The id for the application's instance of a summary workflow, if started
        /// </summary>
        int? ApplicationWorkflowId { get; set; }
    }
}
