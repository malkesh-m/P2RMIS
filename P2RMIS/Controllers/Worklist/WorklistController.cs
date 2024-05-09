using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.Bll.ReviewerRecruitment;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Sra.P2rmis.WebModels.ReviewerRecruitment;

namespace Sra.P2rmis.Web.Controllers.Worklist
{
    public class WorklistController : WorklistBaseController
    {
        public WorklistController(IReviewerRecruitmentService theRecruitmentService, IUserProfileManagementService theProfileManagementService)
        {
            this.theRecruitmentService = theRecruitmentService;
            this.theProfileManagementService = theProfileManagementService;
        }
        // GET: Worklist
        /// <summary>
        /// Default page for Worklist.
        /// </summary>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ReviewerRecruitment.ManageWorkList)]
        public ActionResult Index()
        {
            int userId = GetUserId();
            WorklistViewModel model = new WorklistViewModel();
            try
            {
                var clientList = theProfileManagementService.GetAssignedUserProfileClient(userId).ModelList.OrderBy(x => x.ClientName).ToList();
                model.PopulateClientList(clientList);
                model.SelectedClientId = GetSelectedClient(clientList);
                if (model.SelectedClientId > 0)
                {
                    var workList = theRecruitmentService.GetWorkList(model.SelectedClientId).ModelList.OrderBy(x => x.ModifiedOn).ToList();
                    model.PopulateWorkList(workList);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(model);
        }

        /// <summary>
        /// Gets the profile update modal
        /// </summary>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ReviewerRecruitment.ManageWorkList)]
        public ActionResult ProfileUpdateModal(int userInfoId)
        {
            ProfileUpdateViewModel model = new ProfileUpdateViewModel();
            try
            {
                var profileUpdateList = theRecruitmentService.GetProfileUpdateListForReview(userInfoId).ModelList.ToList();
                model = new ProfileUpdateViewModel(profileUpdateList);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.ProfileUpdateModal, model);
        }

        /// <summary>
        /// Saves the reviewed.
        /// </summary>
        /// <param name="changeLogIds">The change log ids.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ReviewerRecruitment.ManageWorkList)]
        public ActionResult SaveReviewed(List<int> changeLogIds)
        {
            bool isSuccessful = false;
            try
            {
                if (changeLogIds.Count > 0)
                {
                    theRecruitmentService.SaveProfileReviewerAcknowledgement(changeLogIds, GetUserId());
                }
                isSuccessful = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(isSuccessful, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Sets the client in session.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns>JSON response based on success</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ReviewerRecruitment.ManageWorkList)]
        public JsonResult SetClientInSession(int clientId)
        {
            bool isSuccessful = false;
            try
            {
                SetSessionVariable(clientId, Invariables.SessionKey.ClientId);
                isSuccessful = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(isSuccessful, JsonRequestBehavior.AllowGet);
        }
        #region Helpers
        /// <summary>
        /// Sets the selected client in the view model.
        /// </summary>
        /// <param name="model">The view model.</param>
        /// <remarks>If exists in session use session; otherwise default to first in list</remarks>
        private int GetSelectedClient(List<UserProfileClientModel> clientList)
        {
            var selectedClientId = 0;
            if (Session[Invariables.SessionKey.ClientId] != null)
            {
                // use session variable
                selectedClientId = GetSessionVariableId(Invariables.SessionKey.ClientId);
            }
            else
            {
                // we assume client list will have values
                selectedClientId = clientList.FirstOrDefault().ClientId;
            }
            return selectedClientId;
        }
        #endregion
    }
}