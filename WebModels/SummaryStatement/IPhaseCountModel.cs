namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Data model for the display of phase counts
    /// </summary>
    public interface IPhaseCountModel
    {
        /// <summary>
        /// the phase name
        /// </summary>
        string StepName { get; set; }
        /// <summary>
        /// the order of the step
        /// </summary>
        int StepOrder { get; set; }
        /// <summary>
        /// number of checked-out applications marked priority 1
        /// </summary>
        int PriorityOneCount { get; set; }
        /// <summary>
        /// number of checked-out applications marked priority 2
        /// </summary>
        int PriorityTwoCount { get; set; }
        /// <summary>
        /// number of checked-out applications marked priority 1 and 2
        /// </summary>
        int PriorityOneTwoCount { get; set; }
        /// <summary>
        /// number of checked-out applications not marked priority 1 or 2
        /// </summary>
        int NoPriorityCount { get; set; }
        /// <summary>
        /// number of total checked-out applications
        /// </summary>
        int TotalCheckedCount { get; set; }
        /// <summary>
        /// number of non-checked-out applications
        /// </summary>
        int NoCheckedCount { get; set; }
        /// <summary>
        /// number of total applications
        /// </summary>
        int TotalCount { get; set; }
        /// <summary>
        /// whether the current step is a deliverable
        /// </summary>
        bool IsDeliverable { get; set; }
    }

}
