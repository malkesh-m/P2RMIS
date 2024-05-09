using System.Collections.Generic;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for Order of Review.
    /// </summary>
    public class OrderOfReviewViewModel : PanelManagementViewModel
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderOfReviewViewModel()
            : base()
        {
            this.OrdersOfReview = new List<OrderOfReview>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Orders of application review
        /// </summary>
        public IEnumerable<IOrderOfReview> OrdersOfReview { get; set; }
        #endregion
    }
}