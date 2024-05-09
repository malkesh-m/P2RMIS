using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.Web.ViewModels.PanelManagement;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using WebModel = Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.Controllers.PanelManagement
{
    public partial class PanelManagementController
    {
        #region Controller Actions
        /// <summary>
        /// action result for the Order of Review page
        /// </summary>
        /// <returns>the view of the reviews</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageDiscussionOrder)]
        public ActionResult OrderOfReview()
        {
            OrderOfReviewViewModel theViewModel = new OrderOfReviewViewModel();
            try
            {
                // Set data for panel menu(s)
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                //
                // This may be the first time through.  In which case the panelId will be 0
                // and we do not need to call to get the application information
                //
                if (theViewModel.SelectedPanel > 0)
                {  
                    var container = thePanelManagementService.ListOrderOfReview(theViewModel.SelectedPanel);
                    var reviews = container.ModelList.ToList();
                    ReIndexOrder(reviews);
                    AddPremeetingScoresIfMissing(reviews);

                    theViewModel.OrdersOfReview = reviews;
                    WebModel.ReviewerScoreModel.ScoreFormatter = ViewHelpers.ScoreFormatterNotCalculated;
                }
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new OrderOfReviewViewModel();
                HandleExceptionViaElmah(e);
            }
            return View(theViewModel);
        }

        /// <summary>
        /// action result for the Order of Review page
        /// </summary>
        /// <param name="SelectedProgramYear">the selected Program/Year identifier</param>
        /// <param name="SelectedPanel">the selected Panel identifier</param>
        /// <returns>the view of the reviews</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageDiscussionOrder)]
        public ActionResult OrderOfReview(int? SelectedProgramYear, int SelectedPanel)
        {
            OrderOfReviewViewModel theViewModel = new OrderOfReviewViewModel();
            try
            {
                if (SelectedProgramYear.HasValue)
                {
                    //
                    // Set the selected value so the drop down displays the selected value
                    //
                    SetProgramYearSession((int)SelectedProgramYear);
                }
                int sessionPanelId = (int)SelectedPanel;
                //
                // Set the selected value so the drop down displays the selected value
                //
                SetPanelSession(sessionPanelId);
                // Set data for panel menu(s)
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                //
                // Get order of review list
                //
                var container = thePanelManagementService.ListOrderOfReview(theViewModel.SelectedPanel);
                var reviews = container.ModelList.ToList();
                ReIndexOrder(reviews);
                AddPremeetingScoresIfMissing(reviews);

                theViewModel.OrdersOfReview = reviews;
                WebModel.ReviewerScoreModel.ScoreFormatter = ViewHelpers.ScoreFormatterNotCalculated;
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new OrderOfReviewViewModel();
                HandleExceptionViaElmah(e);
            }
            return View(theViewModel);
        }

        /// <summary>
        /// Saves the user specified order of review for the applications located within
        /// this panel
        /// </summary>
        /// <param name="ordersOfReview">List of objects specifying the application, it's order and if it is triaged.</param>
        /// <returns>True if the reordering was saved; false otherwise</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageDiscussionOrder)]
        public ActionResult SaveOrderOfReview(List<OrderOfReviewUpdateViewModel> ordersOfReview)
        {
            bool isSuccessful = false;
            List<KeyValuePair<string, string>> messages = new List<KeyValuePair<string, string>>();
            try
            {
                int sessionPanelId = GetPanelSession();
                int userId = GetUserId();
                messages = ValidateOrdersOfReview(ordersOfReview);
                if (messages.Count == 0)
                {
                    List<SetOrderOfReviewToSave> list = BuildSetOrderOfReviewToSaveList(ordersOfReview);
                    // Now save the reordering & mark those that are triaged.
                    thePanelManagementService.SetOrderOfReview(sessionPanelId, list, userId);
                    isSuccessful = true;
                }
            }
            catch (Exception e)
            {
                // Log the exception 
                messages = new List<KeyValuePair<string, string>>();
                messages.Add(new KeyValuePair<string, string>(String.Empty, MessageService.SaveOrderGenericError));
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = isSuccessful, messages = messages });
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Constructs the list of related values for Order of Review to be saved in the server.
        /// </summary>
        /// <param name="ordersOfReview">List of objects specifying the application, it's order and if it is triaged.</param>
        /// <returns>List containing parameter describing the item to save</returns>
        private List<SetOrderOfReviewToSave> BuildSetOrderOfReviewToSaveList(List<OrderOfReviewUpdateViewModel> ordersOfReview)
        {
            List<SetOrderOfReviewToSave> list = new List<SetOrderOfReviewToSave>(ordersOfReview.Count());
            //
            // Iterate through the list and create the new object & add it to the list
            //
            foreach (var item in ordersOfReview)
            {
                int? newOrder = (item.IsTriaged) ? null : (int?)Convert.ToInt32(item.Order);
                var orderItem = new SetOrderOfReviewToSave(item.LogNumber, newOrder, item.IsTriaged);
                list.Add(orderItem);
            }
            return list;
        }
        /// <summary>
        /// Validates the orders of review.
        /// </summary>
        /// <param name="ordersOfReview">The orders of review.</param>
        /// <returns></returns>
        private List<KeyValuePair<string, string>> ValidateOrdersOfReview(List<OrderOfReviewUpdateViewModel> ordersOfReview)
        {
            List<KeyValuePair<string, string>> messages = new List<KeyValuePair<string, string>>();
            foreach (var item in ordersOfReview)
            {
                if (!item.IsTriaged)
                {
                    int newOrder;
                    if (String.IsNullOrEmpty(item.Order))
                    {
                        messages.Add(new KeyValuePair<string, string>(item.LogNumber, MessageService.SaveOrderBlankNumber));            
                    }
                    else if (!Int32.TryParse(item.Order, out newOrder) || newOrder < 0)
                    {
                        messages.Add(new KeyValuePair<string, string>(item.LogNumber, MessageService.SaveOrderInvalidFormat));
                    }
                    else if (ordersOfReview.Any(x => x.Order == item.Order && x.LogNumber != item.LogNumber))
                    {
                        messages.Add(new KeyValuePair<string, string>(item.LogNumber, MessageService.SaveOrderDuplicateNumber));
                    }                    
                }
            }
            return messages;
        }
        /// <summary>
        /// Re-index the order of review
        /// </summary>
        /// <param name="reviews">the review list</param>
        private void ReIndexOrder(List<WebModel.IOrderOfReview> reviews)
        {
            var hasOrders = reviews.Any(x => !x.IsTriaged && !String.IsNullOrEmpty(x.Order));
            if (!hasOrders)
            {
                for (var i = 0; i < reviews.Count; i++) {
                    if (!reviews[i].IsTriaged)
                        reviews[i].Order = (i + 1).ToString();
                }
            }
        }
        /// <summary>
        /// Adds the pre-meeting scores if missing.
        /// </summary>
        /// <param name="reviews">The reviews.</param>
        private void AddPremeetingScoresIfMissing(List<WebModel.IOrderOfReview> reviews)
        {
            for (var i = 0; i < reviews.Count; i++)
            {
                if (reviews[i].PreMeetingScores.Count() < reviews[i].ApplicationReviewers.Count())
                {
                    var premeetingScores = new List<WebModel.IReviewerScoreModel>();
                    foreach (var reviewer in reviews[i].ApplicationReviewers)
                    {
                        var premeetingScore = reviews[i].PreMeetingScores.Where(x => x.PanelUserAssignmentId == reviewer.PanelUserAssignmentId).FirstOrDefault();
                        if (premeetingScore == null)
                        {
                            premeetingScore = new WebModel.ReviewerScoreModel();
                        }
                        premeetingScores.Add(premeetingScore);
                    }
                    reviews[i].PreMeetingScores = premeetingScores;
                }
            }
        }
        #endregion
    }
}