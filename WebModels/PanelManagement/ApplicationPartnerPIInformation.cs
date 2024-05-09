namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object containing Application Partner PI information requests.
    /// </summary>
     public class ApplicationPartnerPIInformation : IApplicationPartnerPIInformation
    {
        /// <summary>
        /// Partner's First Name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Partner's Last Name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Partner's Full Name
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Partner's Organization
        /// </summary>
        public string OrganizationName { get; set; }
    }
}
