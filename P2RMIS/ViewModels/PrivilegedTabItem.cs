namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// Tab item with a required permission.
    /// </summary>
    public class PrivilegedTabItem : TabItem
    {
        /// <summary>
        /// Defines the required permission for displaying this tab.
        /// </summary>
        public string RequiredPermission { get; set; }
    }
}