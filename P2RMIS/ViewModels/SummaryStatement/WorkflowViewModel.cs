using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Web.ViewModels.SummaryStatement
{
    /// <summary>
    /// Workflow view model
    /// </summary>
    public class WorkflowViewModel
    {
        public WorkflowViewModel() { }

        public WorkflowViewModel(IAwardWorkflowPriorityModel model)
        {
            AwardAbbreviation = model.AwardAbbreviation;
            Cycle = model.Cycle ?? 0;
            PriorityOneWorkflowId = model.PriorityOneWorkflowId;
            PriorityTwoWorkflowId = model.PriorityTwoWorkflowId;
            NoPriorityWorkflowId = model.NoPriorityWorkflowId;
            MechanismId = model.MechanismId;
        }

        /// <summary>
        /// Abbreviated name for an award mechanism
        /// </summary>
        public string AwardAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the cycle.
        /// </summary>
        /// <value>
        /// The cycle.
        /// </value>
        public int Cycle { get; set; }
        /// <summary>
        /// Workflow Id currently designated as priority 1
        /// </summary>
        public int PriorityOneWorkflowId { get; set; }
        /// <summary>
        /// Workflow Id currently designated as priority 2
        /// </summary>
        public int PriorityTwoWorkflowId { get; set; }
        /// <summary>
        /// Workflow Id currently designated for applications not considered priority
        /// </summary>
        public int NoPriorityWorkflowId { get; set; }
        /// <summary>
        /// Unique identifier for an instance of an award mechanism
        /// </summary>
        public int MechanismId { get; set; }
    }
}