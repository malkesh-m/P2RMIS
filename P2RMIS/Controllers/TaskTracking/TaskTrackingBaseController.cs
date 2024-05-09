using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.TaskTracking;
using Sra.P2rmis.Bll.UserProfileManagement;
namespace Sra.P2rmis.Web.Controllers.TaskTracking
{
    public class TaskTrackingBaseController : BaseController
    {
        #region Properties
        /// <summary>
        /// Service providing access to the Summary management services.
        /// </summary>
        protected IUserProfileManagementService theUserProfileManagementService { get; set; }
        /// <summary>
        /// Gets or sets the task tracking service.
        /// </summary>
        /// <value>
        /// The task tracking service.
        /// </value>
        protected ITaskTrackingService theTaskTrackingService { get; set; }
        /// <summary>
        /// Gets or sets the mail service.
        /// </summary>
        /// <value>
        /// The mail service.
        /// </value>
        protected IMailService theMailService { get; set; }
        #endregion
        #region Constants
        public const string ControllerName = "TaskTracking";
        public class ViewNames
        {
            public const string SubmitRequest = "Index";
        }

        /// <summary>
        /// The ticket result temporary data key
        /// </summary>
        public const string TicketResultTempDataKey = "TicketResult";

        /// <summary>
        /// The edit result message temporary data key
        /// </summary>
        public const string EditResultMessageTempDataKey = "EditResultMessage";
        /// <summary>
        /// The edit result temporary data key
        /// </summary>
        public const string EditResultTempDataKey = "EditResult";
        #endregion
        #region Supporting Classes
        /// <summary>
        /// Status messages shown to the user based on server status values
        /// </summary>
        public class Messages
        {
            public static readonly string AttachmentsExceedSize = "The total size of all attachments exceed the maximum allowed size.  Please use the document link section to specify the file location.";
            public static readonly string FailedToSubmit = "The requested ticket could not be submitted.  Please contact the P2RMIS IT Team if the issue persists.";
            public static readonly string EditSuccess = "Comment and/or attachments have been successfully saved.";
            public static readonly string EditNoAction = "No changes found.  Please add a comment or attachment before submitting.";
            public static readonly string EditFailed = "The requested comment and/or attachments could not be saved.  Please contact the P2RMIS IT Team if the issue persists.";
            public static readonly string RetreivalFailed = "There was a problem retreiving the requested ticket information.  Please contact the P2RMIS IT Team for the status of your ticket.";
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Sets the edit result.
        /// </summary>
        /// <param name="didAnything">if set to <c>true</c> an action was performed.</param>
        protected void SetEditResult(bool didAnything)
        {
            if (didAnything)
            {
                TempData[EditResultMessageTempDataKey] = Messages.EditSuccess;
                TempData[EditResultTempDataKey] = true;
            }
            else
            {
                TempData[EditResultMessageTempDataKey] = Messages.EditNoAction;
                TempData[EditResultTempDataKey] = false;
            }
        }
        /// <summary>
        /// Sets the edit result to a failed message.
        /// </summary>
        protected void SetEditResultFailed()
        {
            TempData[EditResultMessageTempDataKey] = Messages.EditFailed;
            TempData[EditResultTempDataKey] = false;
        }

        protected void SetRetreivalResultFailed()
        {
            TempData[EditResultMessageTempDataKey] = Messages.RetreivalFailed;
            TempData[EditResultTempDataKey] = false;
        }
        #endregion
    }
}

