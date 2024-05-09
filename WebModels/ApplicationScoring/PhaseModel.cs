namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Model representing phase information
    /// </summary>
    public interface IPhaseModel
    {
        /// <summary>
        /// Gets or sets the step type identifier.
        /// </summary>
        /// <value>
        /// The step type identifier.
        /// </value>
        int StepTypeId { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the name of the phase.
        /// </summary>
        /// <value>
        /// The name of the phase.
        /// </value>
        string PhaseName { get; set; }
    }

    /// <summary>
    /// Model representing phase information
    /// </summary>
    public class PhaseModel : IPhaseModel
    {
        /// <summary>
        /// Gets or sets the step type identifier.
        /// </summary>
        /// <value>
        /// The step type identifier.
        /// </value>
        public int StepTypeId { get; set; }
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public int SortOrder { get; set; }
        /// <summary>
        /// Gets or sets the name of the phase.
        /// </summary>
        /// <value>
        /// The name of the phase.
        /// </value>
        public string PhaseName { get; set; }
    }
}
