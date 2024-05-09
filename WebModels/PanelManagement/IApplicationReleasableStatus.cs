
namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the AreApplicationsReadyForRelease() repository helper.
    /// </summary>
    public interface IApplicationReleasableStatus
    {
        /// <summary>
        /// Application identifier
        /// </summary>
        int ApplicationId { get; set; }
        /// <summary>
        /// MechanismTemplate identifier
        /// </summary>
        int? MechanismTemplateId { get; set; }
    }
}
