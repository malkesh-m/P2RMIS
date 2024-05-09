
namespace Sra.P2rmis.Bll.PanelManagement
{
    /// <summary>
    /// Status values returned from PanelManagementServer UpdatePanelStageDates() method.
    /// </summary>
    public enum PanelStageDateUpdateStatus
    {
        /// <summary>
        /// All enums should have a default
        /// </summary>
        Default = 0,
        /// <summary>
        /// New panel dates are valid and were updated
        /// </summary>
        Success,
        /// <summary>
        /// ReOpen date is invalid
        /// </summary>
        ReOpenDateInvalid,
        /// <summary>
        /// Close date is invalid
        /// </summary>
        CloseDateInvalid,
        /// <summary>
        /// Reopen date & Close date are invalid
        /// </summary>
        BothDatesInvalid,
        /// <summary>
        /// Reopen date & Close date the same
        /// </summary>
        SameDates,
        /// <summary>
        /// the reopen date is in the past
        /// </summary>
        SomethingBadHappened
    }
}
