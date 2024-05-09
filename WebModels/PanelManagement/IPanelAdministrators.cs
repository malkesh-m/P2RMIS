
namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the GetPanelAdministrators requests.
    /// </summary>
    public interface IPanelAdministrators
    {
        /// <summary>
        /// Panel administrator first name
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// Panel administrator last name
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// Administrator's email address
        /// </summary>
        string EmailAddress { get; set; }
    }
}
