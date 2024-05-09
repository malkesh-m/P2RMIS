
namespace Sra.P2rmis.Bll.PanelManagement
{
    /// <summary>
    /// Status values returned from PanelManagementServer Release() method.
    /// </summary>
    public enum ReleaseStatus
    {
        /// <summary>
        /// All enums should have a default
        /// </summary>
        Default = 0,
        /// <summary>
        /// Panel's applications were successfully released 
        /// </summary>
        Success = 1,
        /// <summary>
        /// Scoring was not set up on panel's applications
        /// </summary>
        ScoringNotSetUp = 2,
    }
}
