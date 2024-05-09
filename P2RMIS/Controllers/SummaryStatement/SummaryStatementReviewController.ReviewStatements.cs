using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.SummaryStatement;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// Controller Summary Statement Client Processing page tabs:
    ///   - SSM-800 - Summary Statement Client View
    ///   - Included in this file: actions related to SSM-800
    /// </summary>
    public partial class SummaryStatementReviewController : SummaryStatementBaseController
    {
        /// <summary>
        /// The clients review of the summary statement
        /// </summary>
        /// <param name="workflowId">the workflow Id to review</param>
        /// <returns>the view of the review summary statement page</returns>
        [Common.Authorize(Operations = Permissions.SummaryStatement.Review)]
        public ActionResult ReviewSummaryStatement(int workflowId)
        {
            // instantiate view model
            EditSummaryStatementViewModel editSummaryStatementVM = new EditSummaryStatementViewModel();

            try
            {
                CustomIdentity ident = User.Identity as CustomIdentity;
                // go to the BLL and get the data for editing
                editSummaryStatementVM = GetDataForEditing(editSummaryStatementVM, workflowId);
                editSummaryStatementVM.SetCommentPermissions(true);                
                editSummaryStatementVM.CanAcceptTrackChanges = IsValidPermission(Permissions.SummaryStatement.AcceptTrackChanges);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                editSummaryStatementVM = new EditSummaryStatementViewModel();
            }

            //return the view model to the page
            return View(editSummaryStatementVM);
        }
        /// <summary>
        /// performs the check out action for the summary
        /// </summary>
        /// <param name="applicationWorkflowId">the application workflow id.</param>
        /// <returns>the Boolean indicator of the checkout action status</returns>
        [Common.Authorize(Operations = Permissions.SummaryStatement.Review)]
        public ActionResult CheckoutAction(int applicationWorkflowId)
        {
            //
            // Call the base method to checkout the workflow.
            //
            return Checkout(applicationWorkflowId);
        }
        /// <summary>
        /// Performs a check-in action for the summary
        /// </summary>
        /// <param name="workflowId">the application workflow id of the SS being edited</param>
        /// <returns>view model back to page</returns>
        [Common.Authorize(Operations = Permissions.SummaryStatement.Review)]
        public ActionResult CheckinAction(int workflowId)
        {
            return Checkin(workflowId);
        }
        /// <summary>
        /// Directs user to a specific summary statement to review
        /// </summary>
        /// <param name="workflowId">ApplicationWorkflow entity identifier</param>
        /// <returns>the view model to summary statement edit page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Review)]
        public ActionResult EditSummaryStatement(int workflowId)
        {
            bool isReviewContext = true;
            //
            // instantiate view model & return it to the page
            //
            EditSummaryStatementViewModel viewModel = CreateSummaryStatementViewModel(workflowId, isReviewContext);

            return View(viewModel);
        }
    }
}

