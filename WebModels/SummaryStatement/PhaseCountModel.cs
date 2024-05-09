namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Data model for the display of phase counts
    /// </summary>
    public class PhaseCountModel : IPhaseCountModel
    {
        /// <summary>
        /// the phase name
        /// </summary>
        public string StepName { get; set; }
        /// <summary>
        /// the order of the step
        /// </summary>
        public int StepOrder { get; set; }
        /// <summary>
        /// number of checked-out applications marked priority 1
        /// </summary>
        public int PriorityOneCount { get; set; }
        /// <summary>
        /// number of checked-out applications marked priority 2
        /// </summary>
        public int PriorityTwoCount { get; set; }
        /// <summary>
        /// number of checked-out applications marked priority 1 and 2
        /// </summary>
        public int PriorityOneTwoCount { get; set; }
        /// <summary>
        /// number of checked-out applications not marked priority 1 or 2
        /// </summary>
        public int NoPriorityCount { get; set; }
        /// <summary>
        /// number of total checked-out applications
        /// </summary>
        public int TotalCheckedCount { get; set; }
        /// <summary>
        /// number of non-checked-out applications
        /// </summary>
        public int NoCheckedCount { get; set; }
        /// <summary>
        /// number of total applications
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// whether the current step is a deliverable
        /// </summary>
        public bool IsDeliverable { get; set; }
    }
}
