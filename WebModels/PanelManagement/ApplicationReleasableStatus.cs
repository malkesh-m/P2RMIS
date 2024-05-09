
namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the AreApplicationsReadyForRelease() repository helper.
    /// </summary>
    public class ApplicationReleasableStatus : IApplicationReleasableStatus
    {
        /// <summary>
        /// Application identifier
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// MechanismTemplate identifier
        /// </summary>
        public int? MechanismTemplateId { get; set; }
    }
}
