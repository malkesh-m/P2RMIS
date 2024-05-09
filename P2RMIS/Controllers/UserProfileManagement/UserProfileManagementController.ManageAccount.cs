using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.ViewModels.UserProfileManagement;

namespace Sra.P2rmis.Web.Controllers.UserProfileManagement
{
    public partial class UserProfileManagementController
    {
        /// <summary>
        /// TODO: descriptions
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts)]
        public ActionResult ManageAccount()
        {
            try
            {
                //TODO: SetTabs(viewModel);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                HandleExceptionViaElmah(e);
            }
            return View();
        }
        /// <summary>
        /// Responds to the "Send Credentials" button on the Manage Account screen.
        /// </summary>
        /// <param name="targetUserId">Target user id</param>
        /// <returns>ManageAccountStatus object returning status to web page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts)]
        public JsonResult SendCredentials(int targetUserId)
        {
            ManageAccountStatus result = new ManageAccountStatus();
            try
            {
                MailService.MailStatus sentStatus = theProfileService.ResendCredentials(theMailService, targetUserId, GetUserId());
                result.AccountStatusName = theProfileService.GetUserAccountStatusName(targetUserId);
                result.AccountStatusDate = theProfileService.GetUserAccountStatusDate(targetUserId);

                if (sentStatus == MailService.MailStatus.Success)
                {
                    var model = theProfileService.WhoSentCredentials(targetUserId);
                    result.Status = true;
                    result.Populate(model.Sent.Value, model.SentByFirstName, model.SentByLastName);
                    result.SetStatus(model.StatusId, model.StatusReasonId, true, model.Status, model.StatusReason, model.StatusDate);

                    result.EnableSendCredentialButton = false;
                    result.ActionSuccessMessage = MessageService.Constants.SendCredentialsSuccess;
                }
                else
                {
                    result.Status = false;
                    result.EnableSendCredentialButton = true;
                    result.ActionSuccessMessage = MessageService.Constants.SendCredentialsFailure;
                }
            }
            catch (Exception e)
            {
                //
                // reset the view model to a known state & log the error
                //
                HandleExceptionViaElmah(e);
                result.Status = false;
            }
            var z = JsonConvert.SerializeObject(result);
            return Json(z, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Responds to the "Unlock" button on the Manage Account screen.
        /// </summary>
        /// <param name="targetUserId">Target user id</param>
        /// <returns>ManageAccountStatus object returning status to web page.  Only applicable fields are used.</returns>
        /// <remarks>
        ///   As a note the parameter name is defined as a constant in Routing.
        /// </remarks>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts)]
        public JsonResult Unlock(int targetUserId)
        {
            ManageAccountStatus result = new ManageAccountStatus();
            try
            {
                IReactivateDeactivateResult outcome = theProfileService.Unlock(targetUserId, GetUserId());
                result.AccountStatusDate = theProfileService.GetUserAccountStatusDate(targetUserId);

                result.Populate(GlobalProperties.P2rmisDateTimeNow, outcome.NameResult.FirstName, outcome.NameResult.LastName);
                result.SetStatus(outcome.AccountStatusId, outcome.AccountStatusReasonId, true, outcome.Status, outcome.Reason, outcome.AccountStatusDate);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a known state & log the error
                //
                HandleExceptionViaElmah(e);
                result.Status = false;
            }
            var z = JsonConvert.SerializeObject(result);
            return Json(z, JsonRequestBehavior.AllowGet);
        }
        #region Activate/Deactivate
        /// <summary>
        /// Responds to the "Activate" button on the Manage Account screen.
        /// </summary>
        /// <param name="targetUserId">Target user id</param>
        /// <returns>ManageAccountStatus object returning status to web page.  Only applicable fields are used.</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts)]
        public JsonResult ReActivate(int targetUserId)
        {
            ManageAccountStatus result = new ManageAccountStatus();
            try
            {
                //
                // Reactivate the account & set the appropriate values.
                //
                int userId = GetUserId();
                var q = theProfileService.Reactivate(targetUserId, userId);
                result.Populate(GlobalProperties.P2rmisDateTimeNow, q.NameResult.FirstName, q.NameResult.LastName);
                result.SetStatus(q.AccountStatusId, q.AccountStatusReasonId, true, q.Status, q.Reason, q.AccountStatusDate);
                result.SetProfileType(q.ProfileTypeId);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a known state & log the error
                //
                HandleExceptionViaElmah(e);
            }
            var z = JsonConvert.SerializeObject(result);
            return Json(z, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Responds to the "Deactivate" button on the Manage Account screen.
        /// </summary>
        /// <param name="targetUserId">Target user id</param>
        /// <param name="accountStatusReasonId">AccountStatusReason entity identifier</param>
        /// <returns>ManageAccountStatus object returning status to web page.  Only applicable fields are used.</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts)]
        public JsonResult DeActivate(int targetUserId, int accountStatusReasonId)
        {
            ManageAccountStatus result = new ManageAccountStatus();
            try
            {
                //
                // Deactivate the account & set the appropriate values.
                //
                var q = theProfileService.DeActivate(targetUserId, accountStatusReasonId, GetUserId());
                result.Populate(GlobalProperties.P2rmisDateTimeNow, q.NameResult.FirstName, q.NameResult.LastName);
                result.SetStatus(q.AccountStatusId, q.AccountStatusReasonId, true, q.Status, q.Reason, q.AccountStatusDate);
                result.SetProfileType(q.ProfileTypeId);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a known state & log the error
                //
                HandleExceptionViaElmah(e);
                result.Status = false;
            }
            var z = JsonConvert.SerializeObject(result);
            return Json(z, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Recruitment Blocking        
        /// <summary>
        /// Gets the user client blocks.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts)]
        [HttpGet]
        public ActionResult GetUserClientBlocks(int userInfoId)
        {
            var clients = new List<KeyValuePair<int, string>>();
            try
            {
                int loggedInUserId = GetUserId();
                var clientIds = theProfileService.GetAssignedUserProfileClient(loggedInUserId).ModelList.ToList().ConvertAll(x => x.ClientId);
                clients = theProfileService.GetUserClientBlocks(userInfoId, clientIds);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a known state & log the error
                //
                HandleExceptionViaElmah(e);
            }
            return Json(new { clients = clients }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Updates the user client block.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="blockedClientIds">The blocked client ids.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts)]
        [HttpPost]
        public ActionResult UpdateUserClientBlock(int userInfoId, List<int> blockedClientIds, string comment)
        {
            bool flag = false;
            try
               
            {
                int loggedInUserId = GetUserId();
                flag = theProfileService.UpdateUserClientBlock(userInfoId, blockedClientIds, comment, loggedInUserId);
            }
            catch (Exception e)
            {
                // reset the view model to a known state & log the error
                //
                HandleExceptionViaElmah(e);
            }
            return Json(new { flag = flag });
        }
        #endregion
    }
}