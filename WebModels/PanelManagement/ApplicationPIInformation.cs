using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the GetApplicationPIInformation requests.
    /// </summary>
    public class ApplicationPIInformation : IApplicationPIInformation
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationPIInformation()
        {
            Blinded = false;
        }
        /// <summary>
        /// Application identifier
        /// </summary>
        public int applicationId { get; set; }
        /// <summary>
        /// Application Log Number
        /// </summary>
        public string LogNumber { get; set; }
        /// <summary>
        /// Application's Title
        /// </summary>
        public string ApplicationTitle { get; set; }
        /// <summary>
        /// Application's Award Mechanism
        /// </summary>
        public string AwardMechanism { get; set; }
        /// <summary>
        /// Application's Research Area
        /// </summary>
        public string ResearchArea { get; set; }
        /// <summary>
        /// PI's First Name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// PI's Last Name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// PI's Organization
        /// </summary>
        public string OrganizationName { get; set; }
        /// <summary>
        /// Blinded
        /// </summary>
        public bool Blinded { get; set; }
        /// <summary>
        /// List of Partner PI Information for this PI
        /// </summary>
        public IEnumerable<IApplicationPartnerPIInformation> PartnerPIInformation { get; set; }
    }
}
