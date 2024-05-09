using System;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    public class SummaryStatementApplicationModel : ISummaryStatementApplicationModel
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
        public string OverallScore { get; set; }
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
        /// The workflow the application is using
        /// </summary>
        public string Workflow { get; set; }
        /// <summary>
        /// The workflows unique identifier
        /// </summary>
        public int? WorkflowId { get; set; }
        /// <summary>
        /// The id for the application's instance of a summary workflow, if started
        /// </summary>
        public int? ApplicationWorkflowId { get; set; }
        /// <summary>
        /// Workflow step value
        /// </summary>
        public string WorkflowStep { get; set; }
        /// <summary>
        /// Priority value
        /// </summary>
        public string Priority { get; set; }
        /// <summary>
        /// Priority 2 value
        /// </summary>
        public string Priority2 { get; set; }
        public string Order { get; set; }
    }
}
