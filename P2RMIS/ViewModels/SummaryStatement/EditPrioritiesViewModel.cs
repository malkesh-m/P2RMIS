using Sra.P2rmis.WebModels.SummaryStatement;
using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices;
using System.Linq;
namespace Sra.P2rmis.Web.UI.Models
{
    public class EditPrioritiesViewModel
    {
        #region Constructor
        public EditPrioritiesViewModel()
        {
            this.SummaryApplications = new List<SummaryPriorityViewModel>();
        }
        #endregion
        #region Properties
        public string PriorityOneSelection { get; set; }

        public string PriorityTwoSelection { get; set; }

        public List<SummaryPriorityViewModel> SummaryApplications { get; set; } 
        #endregion
        #region Helpers
        public void PopulateApplications(IEnumerable<ISummaryStatementRequestReview> apps)
        {
            apps.OrderBy(o => o.LogNumber).ToList().ForEach(x => this.SummaryApplications.Add(new SummaryPriorityViewModel(x)));
        }
        #endregion
        #region Subclass
        public class SummaryPriorityViewModel
        {
            #region Constructor
            public SummaryPriorityViewModel(ISummaryStatementRequestReview apps)
            {
                this.ApplicationId = apps.ApplicationId;
                this.LogNumber = apps.LogNumber;
                this.ApplicationWorkflowId = apps.ApplicationWorkflowId;
                this.Priority1 = apps.Priority1 ? "Yes" : "No";
                this.Priority2 = apps.Priority2 ? "Yes" : "No";
                this.AwardMechanismAbbreviation = apps.AwardMechanismAbbreviation;
                this.OverallScore = apps.OverallScore != null ? ViewHelpers.FormatScoreDecimal((decimal)apps.OverallScore) : string.Empty;
                this.ResearchTopicArea = apps.ResearchTopicArea;
                this.PanelApplicationId = apps.PanelApplicationId;
                this.Panel = apps.Panel;
                this.Cycle = apps.Cycle;
            }

            public SummaryPriorityViewModel()
            {

            }
            #endregion
            #region Properties
            /// <summary>
            /// the application's unique identifier
            /// </summary>
            public int ApplicationId { get; set; }
            /// <summary>
            /// the application's unique identifier (Log Number)
            /// </summary>
            public string LogNumber { get; set; }
            /// <summary>
            /// Applications workflow unique identifier
            /// </summary>
            public int? ApplicationWorkflowId { get; set; }
            /// <summary>
            /// the application's priority 1 status
            /// </summary>
            public string Priority1 { get; set; }
            /// <summary>
            /// the application's priority 2 status
            /// </summary>
            public string Priority2 { get; set; }
            /// <summary>
            /// the application's Award Mechanism (abbreviation)
            /// </summary>
            public string AwardMechanismAbbreviation { get; set; }
            /// <summary>
            /// the application's overall score
            /// </summary>
            public string OverallScore { get; set; }
            /// <summary>
            /// the application's research topic area
            /// </summary>
            public string ResearchTopicArea { get; set; }
            /// <summary>
            /// PanelApplication entity identifier
            /// </summary>
            public int PanelApplicationId { get; set; }
            /// <summary>
            /// Gets or sets the cycle.
            /// </summary>
            /// <value>
            /// The cycle.
            /// </value>
            public int? Cycle { get; set; }
            /// <summary>
            /// Gets or sets the panel.
            /// </summary>
            /// <value>
            /// The panel.
            /// </value>
            public string Panel { get; set; }
            #endregion
        }
        #endregion
    }
}