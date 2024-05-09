using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the GetApplicationPIInformation requests.
    /// </summary>
    public interface IApplicationPIInformation
    {
        /// <summary>
        /// Application identifier
        /// </summary>
        int applicationId { get; set; }
        /// <summary>
        /// Application Log Number
        /// </summary>
        string LogNumber { get; set; }
        /// <summary>
        /// Application's Title
        /// </summary>
        string ApplicationTitle { get; set; }
        /// <summary>
        /// Application's Award Mechanism
        /// </summary>
        string AwardMechanism { get; set; }
        /// <summary>
        /// Application's Research Area
        /// </summary>
        string ResearchArea { get; set; }
        /// <summary>
        /// PI's First Name
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// PI's Last Name
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// PI's Organization
        /// </summary>
        string OrganizationName { get; set; }
        /// <summary>
        /// Blinded
        /// </summary>
        bool Blinded { get; set; }
        /// <summary>
        /// List of Partner PI Information for this PI
        /// </summary>
        IEnumerable<IApplicationPartnerPIInformation> PartnerPIInformation { get; set; }
    }
}
