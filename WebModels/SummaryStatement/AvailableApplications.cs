using System;

namespace Sra.P2rmis.WebModels.SummaryStatement
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
    /// <summary>
    /// class representing the available applications to be pushed to SS workflow
    /// </summary>
    public class AvailableApplications : IAvailableApplications
    {
        /// <summary>
        /// Applications unique identifier (Application Log Number)
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// Panel application identifier
        /// </summary>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// the applications mechanism
        /// </summary>
        public string MechanismAbbreviation { get; set; }
        /// <summary>
        /// the applications overall score
        /// </summary>
        public decimal? OverallScore { get; set; }
        /// <summary>
        /// the date the application was concatenated
        /// </summary>
        public DateTime? ConcatenatedDate { get; set; }
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
        /// Formatting for the qualifying column display in view
        /// </summary>
        public int QualifyingCol { get { return IsQualifying ? 1 : 0; } }
        /// <summary>
        /// Formatting for the command draft column display in view
        /// </summary>
        public int CommandDraftCol { get { return IsCommandDraft ? 1 : 0; } }
        /// <summary>
        /// The workflow the application is using
        /// </summary>
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
            get { return (ScoreFormatter != null) ? ScoreFormatter(OverallScore) : OverallScore.ToString(); }
        }
        /// <summary>
        /// The id for the application's instance of a summary workflow, if started
        /// </summary>
        public int? ApplicationWorkflowId { get; set; }
    }
}
