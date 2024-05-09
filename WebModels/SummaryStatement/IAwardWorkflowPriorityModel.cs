namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing workflow assignments for various priorities by award mechanism
    /// </summary>
    public interface IAwardWorkflowPriorityModel
    {
        /// <summary>
        /// Abbreviated name for an award mechanism
        /// </summary>
        string AwardAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the cycle.
        /// </summary>
        /// <value>
        /// The cycle.
        /// </value>
        int? Cycle { get; set; }

        /// <summary>
        /// Workflow Id currently designated as priority 1
        /// </summary>
        int PriorityOneWorkflowId { get; set; }

        /// <summary>
        /// Workflow Id currently designated as priority 2
        /// </summary>
        int PriorityTwoWorkflowId { get; set; }

        /// <summary>
        /// Workflow Id currently designated for applications not considered priority
        /// </summary>
        int NoPriorityWorkflowId { get; set; }

        /// <summary>
        /// Unique identifier for an instance of an award mechanism
        /// </summary>
        int MechanismId { get; set; }
    }
}