using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for an application's Critique summary in panel management
    /// </summary>
    public class CritiqueSummaryModel : ICritiqueSummaryModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CritiqueSummaryModel()
        {
            Phases = new List<PanelStageStepModel>();
            Phases.OrderBy(x => x.StepOrder);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="logNumber">Application log number</param>
        /// <param name="reviewerCount">Reviewer count</param>
        public CritiqueSummaryModel(int panelApplicationId, string logNumber, int rumberOfReviewers)
            : this()
        {
            this.PanelApplicationId = panelApplicationId;
            this.LogNumber = logNumber;
            this.NumberOfReviewers = rumberOfReviewers;
        }
        /// <summary>
        /// PanelApplication identifier
        /// </summary>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// Application log number
        /// </summary>
        public string LogNumber { get; set; }
        /// <summary>
        /// Number of reviewers on the panel
        /// </summary>
        public int NumberOfReviewers { get; set; }
        /// <summary>
        /// Gets or sets the application critique.
        /// </summary>
        /// <value>
        /// The application critique.
        /// </value>
        public IApplicationCritiqueModel ApplicationCritique { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is meeting phase started.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is meeting phase started; otherwise, <c>false</c>.
        /// </value>
        public bool IsMeetingPhaseStarted { get; set; }
        /// <summary>
        /// Collection of data describing the phases
        /// </summary>
        public ICollection<PanelStageStepModel> Phases { get; set; }
        /// <summary>
        /// Return the panel application's phases in step order.
        /// </summary>
        public ICollection<PanelStageStepModel> OrderedPhases
        {
            get
            {
                return Phases.OrderBy(phase => phase.StepOrder).ToList();
            }
        }
    }
}
