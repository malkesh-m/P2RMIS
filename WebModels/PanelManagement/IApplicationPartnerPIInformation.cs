namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object containing Application Partner PI information requests.
    /// </summary>
    public interface IApplicationPartnerPIInformation
    {
        /// <summary>
        /// Partner's First Name
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// Partner's Last Name
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// Partner's Full Name
        /// </summary>
        string FullName { get; set; }
        /// <summary>
        /// Partner's Organization
        /// </summary>
        string OrganizationName { get; set; }
    }
}
