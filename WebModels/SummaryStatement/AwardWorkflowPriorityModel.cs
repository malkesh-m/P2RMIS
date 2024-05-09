namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing workflow assignments for various priorities by award mechanism
    /// </summary>
    public class AwardWorkflowPriorityModel : IAwardWorkflowPriorityModel
    {
        /// <summary>
        /// Worflow Cycle
        /// </summary>
        public int WFCycle { get; set; }
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
        public int? Cycle { get; set; }
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
