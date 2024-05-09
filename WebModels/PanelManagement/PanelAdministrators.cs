
namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the GetPanelAdministrators requests.
    /// </summary>
    public class PanelAdministrators: IPanelAdministrators
    {
        /// <summary>
        /// Panel administrator first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Panel administrator last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Administrator's email address
        /// </summary>
        public string EmailAddress { get; set; }
    }
}
