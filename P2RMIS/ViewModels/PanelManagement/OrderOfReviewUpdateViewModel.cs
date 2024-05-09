namespace Sra.P2rmis.Web.ViewModels.PanelManagement
{
    /// <summary>
    /// The view model for updating the order of review and triaging applicaitons
    /// </summary>
    public class OrderOfReviewUpdateViewModel
    {
        /// <summary>
        /// Applications log number
        /// </summary>
        public string LogNumber { get; set; }
        /// <summary>
        /// Indicates if the application is triaged
        /// </summary>
        public bool IsTriaged { get; set; }
        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public string Order { get; set; }
    }
}