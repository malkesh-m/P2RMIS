using System;
using System.Web.Mvc;
using Sra.P2rmis.Bll.TaskTracking;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.TaskTracking;

namespace Sra.P2rmis.Web.Controllers.TaskTracking
{
    public class TaskTrackingController : TaskTrackingBaseController
    {
        #region Construction; Setup & Disposal
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private TaskTrackingController()
        {
        }

        /// <summary>
        /// Constructor for the task tracking controller.
        /// </summary>
        /// <param name="theProfileService">File Service</param>
        /// <param name="theTaskTrackingService">The task tracking service.</param>
        public TaskTrackingController( IUserProfileManagementService theProfileService, ITaskTrackingService theTaskTrackingService, IMailService theMailService)
        {
            this.theUserProfileManagementService = theProfileService;
            this.theTaskTrackingService = theTaskTrackingService;
            this.theMailService = theMailService;
        }
        #endregion
        #region Controller Actions
        /// <summary>
        /// Default page for task submittal
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.TaskTracking.SubmitTask)]
        public ActionResult Index()
        {
            var theModel = new TaskTrackingViewModel();
            try
            {
                SetUpViewModel(theModel);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                HandleExceptionViaElmah(e);
            }
            return View(theModel);
        }
        /// <summary>
        /// Submission action for new task request.
        /// </summary>
        /// <param name="theModel">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.TaskTracking.SubmitTask)]
        public ActionResult Index(TaskTrackingViewModel theModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //TODO: may want to consider refactoring SubmitTaskModel into 2 models, one for Submit one for Edit since the reqs have drifted
                    var serviceModel =
                        new SubmitTaskModel(theModel.RequestorName, theModel.RequestorEmail, theModel.SelectedClient,
                            theModel.Subject, theModel.RequestType, theModel.DueDate, theModel.Component, theModel.TaskType,
                            theModel.RequestDescription, theModel.ProjectJustification, theModel.DocumentLink, string.Empty, string.Empty, string.Empty, string.Empty);
                    TempData[TicketResultTempDataKey] = theTaskTrackingService.SubmitTask(serviceModel, theModel.Attachments, theMailService);
                    //Refresh the view model in case user wants to submit another task
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    HandleExceptionViaElmah(e);
                    TempData[TicketResultTempDataKey] = string.Empty;
                }

            }
            SetUpViewModel(theModel);
            return View(theModel);
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.TaskTracking.SubmitTask)]
        public ActionResult EditRequest(string ticketId)
        {
            var theModel = new EditRequestViewModel();
            try
            {
                var ticketInfo = theTaskTrackingService.GetTicketInformation(ticketId);
                theModel.PopulateViewModel(ticketId, ticketInfo);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
                SetRetreivalResultFailed();
            }
            return View(theModel);
        }
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.TaskTracking.SubmitTask)]
        public ActionResult EditRequest(EditRequestViewModel theModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = theTaskTrackingService.EditTask(theModel.TicketId, theModel.Comment, theModel.Attachments, GetUserName());
                    SetEditResult(result);
                    return RedirectToAction("EditRequest", new { ticketId = theModel.TicketId });
                }
                catch (Exception e)
                {
                    HandleExceptionViaElmah(e);
                    SetEditResultFailed();
                }
            }
            return View(theModel);
        }
        #endregion
        #region Helpers
        internal TaskTrackingViewModel SetUpViewModel(TaskTrackingViewModel theModel)
        {
            var userInfo = theUserProfileManagementService.GetUsersNameAndPrimaryEmail(GetUserId());
            var ticketMetadata = theTaskTrackingService.GetTicketMetaData();
            theModel.PopulateInfoAndDropdowns(userInfo, ticketMetadata);
            return theModel;
        }
        #endregion

    }
}