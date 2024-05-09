using System.Collections.Generic;
using Sra.P2rmis.WebModels;
using System.Linq;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Web.ViewModels.SummaryStatementReview
{
    public class RequestReviewApplicationsViewModel
    {
        #region Constructors

        /// <summary>
        /// View model for displaying the available applications for review requests
        /// </summary>
        public RequestReviewApplicationsViewModel()
        {
        }

        public RequestReviewApplicationsViewModel(List<ISummaryStatementRequestReview> applications)
        {
            Applications = applications.ConvertAll(x => new RequestReviewApplicationViewModel(x)).Select((item, index) => { item.Index = index + 1; return item; }).ToList(); 
        }

        #endregion

        #region Properties
        /// <summary>
        /// the applications
        /// </summary>
        public List<RequestReviewApplicationViewModel> Applications { get; set; }
        /// <summary>
        /// Gets or sets the refresh time.
        /// </summary>
        /// <value>
        /// The refresh time.
        /// </value>
        public string RefreshTime { get; set; }
        #endregion
    }
}