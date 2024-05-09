using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Data model for summary statement progress information
    /// </summary>
    public interface ISummaryStatementProgressModel
    {
        /// <summary>
        /// Applications unique identifier (Application Log Number)
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

        /// <summary>
        /// the application's cycle
        /// </summary>
        int? Cycle { get; set; }

        /// <summary>
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
        string ProgramAbbreviation { get; set; }

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
        /// The id for the application's instance of a summary workflow, if started
        /// </summary>
        int? ApplicationWorkflowId { get; set; }

        /// <summary>
        /// Whether or not admin notes currently exist for the application
        /// </summary>
        bool NotesExist { get; set; }

        /// <summary>
        /// Whether or not admin (budget) notes exist for the application
        /// </summary>
        bool AdminNotesExist { get; set; }

        /// <summary>
        /// The workflow step of the application that is next to be completed
        /// </summary>
        int? CurrentStepId { get; set; }

        /// <summary>
        /// The name of the workflow step of the application that is next to be completed
        /// </summary>
        string CurrentStepName { get; set; }

        /// <summary>
        /// Gets or sets the first name of the checked out user.
        /// </summary>
        /// <value>
        /// The first name of the checked out user.
        /// </value>
        string CheckedOutUserFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the checked out user.
        /// </summary>
        /// <value>
        /// The last name of the checked out user.
        /// </value>
        string CheckedOutUserLastName { get; set; }

        /// <summary>
        /// Summary statement post time (time the summary statement was checked in the 
        /// last workflow step.
        /// </summary>
        DateTime? PostDateTime { get; set; }

        /// <summary>
        /// Summary statement check out time (time the summary statement was checked out 
        /// for the last workflow step.
        /// </summary>
        DateTime? CheckOutDateTime { get; set; }

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        int ApplicationId { get; set; }
    }

    /// <summary>
    /// Data model for summary statement progress information
    /// </summary>
    /// <seealso cref="Sra.P2rmis.WebModels.SummaryStatement.ISummaryStatementProgressModel" />
    public class SummaryStatementProgressModel : ISummaryStatementProgressModel
    {
        /// <summary>
        /// Applications unique identifier (Application Log Number)
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
        /// The id for the application's instance of a summary workflow, if started
        /// </summary>
        public int? ApplicationWorkflowId { get; set; }
        /// <summary>
        /// Whether or not notes currently exist for the application
        /// </summary>
        public bool NotesExist { get; set; }

        /// <summary>
        /// Whether or not admin (budget) notes exist for the application
        /// </summary>
        public bool AdminNotesExist { get; set; }
        /// <summary>
        /// The workflow step of the application that is next to be completed
        /// </summary>
        public int? CurrentStepId { get; set; }
        /// <summary>
        /// The name of the workflow step of the application that is next to be completed
        /// </summary>
        public string CurrentStepName { get; set; }
        /// <summary>
        /// Gets or sets the first name of the checked out user.
        /// </summary>
        /// <value>
        /// The first name of the checked out user.
        /// </value>
        public string CheckedOutUserFirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name of the checked out user.
        /// </summary>
        /// <value>
        /// The last name of the checked out user.
        /// </value>
        public string CheckedOutUserLastName { get; set; }
        /// <summary>
        /// Summary statement post time (time the summary statement was checked in the 
        /// last workflow step.
        /// </summary>
        public DateTime? PostDateTime { get; set; }
        /// <summary>
        /// Summary statement check out time (time the summary statement was checked out 
        /// for the last workflow step.
        /// </summary>
        public DateTime? CheckOutDateTime { get; set; }
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public int ApplicationId { get; set; }
        public int Index { get; set; }
        public string Workflow { get; set; }
    }
}
