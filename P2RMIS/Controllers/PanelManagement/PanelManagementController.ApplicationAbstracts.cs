using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.Web.ViewModels.PanelManagement;
using Sra.P2rmis.WebModels.PanelManagement;
using Newtonsoft.Json;
using Sra.P2rmis.Bll.Views;

namespace Sra.P2rmis.Web.Controllers.PanelManagement
{
    public partial class PanelManagementController 
    {
        #region Controller Actions
        /// <summary>
        /// Gets list of individuals who may have a conflict of interest in the review of the current proposal.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <returns>ViewCoiListViewModel populated with COI data</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManagePanelApplication)]
        public ActionResult GetCoiList(int applicationId)
        {
            ViewCoiListViewModel theViewModel = new ViewCoiListViewModel();
            try
            {
                var container = thePanelManagementService.ListPersonnelWithCoi(applicationId);
                theViewModel.Personnels = container.ModelList.ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            // send view model to view page
            return PartialView(ViewNames.CoiList, theViewModel);
        }

        /// <summary>
        /// Retrieves view data for the View Application Information page (PM1.3.3.001)
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <returns>ViewApplicationViewModel populated with application information</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManagePanelApplication)]
        public ActionResult Index()
        {
            ViewApplicationViewModel theViewModel = new ViewApplicationViewModel();
            try
            {
                // Set data for panel menu(s)
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                //
                // This may be the first time through.  In which case the panelId will be 0
                // and we do not need to call to get the application information
                //
                theViewModel.CanAddApplication = HasPermission(Permissions.PanelManagement.ManagePanelApplication);
                theViewModel.SelectedPanel = IsSessionVariableNull(SessionVariables.PanelId) ? 0 : (int)Session[SessionVariables.PanelId];
                theViewModel.IsSro = theUserProfileManagementService.IsSro(GetUserId());
                if (theViewModel.SelectedPanel > 0)
                {                    
                    var applicationInformation = this.thePanelManagementService.ListApplicationInformation((int)theViewModel.SelectedPanel);
                    theViewModel.Applications = new List<IApplicationInformationModel>(applicationInformation.ModelList);

                    // 
                    theViewModel.SelectedPanel = theViewModel.Panels.Exists(x => x.PanelId == theViewModel.SelectedPanel) ? theViewModel.SelectedPanel : 0;
                 }
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new ViewApplicationViewModel();
                HandleExceptionViaElmah(e);
            }

            return View(ViewNames.ApplicationAbstracts, theViewModel);
        }
        /// <summary>
        /// Gets the applications.
        /// </summary>
        /// <param name="panelId">The panel identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManagePanelApplication)]
        public ActionResult GetApplications(int panelId)
        {
            var panel = new SessionPanelModel();
            List<ApplicationViewModel> models = new List<ApplicationViewModel>();
            try
            {
                Session[SessionVariables.PanelId] = panelId;
                bool canManagePanelApplication = HasPermission(Permissions.PanelManagement.ManagePanelApplication);
                panel = (SessionPanelModel)thePanelManagementService.GetSessionPanel(panelId);
                var apps = thePanelManagementService.ListApplicationInformation(panelId).ModelList;

                models = apps.ToList().ConvertAll(x => new ApplicationViewModel(x, canManagePanelApplication));     
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new
            {
                applications = models,
                year = panel.Year,
                programAbbreviation = panel.ProgramAbbreviation,
                panelAbbreviation = panel.PanelAbbreviation
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Retrieves view data for the View Application Information page (PM1.3.3.001)
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <returns>ViewApplicationViewModel populated with application information</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManagePanelApplication)]
        public ActionResult Index(int? SelectedProgramYear, int SelectedPanel)
        {
            ViewApplicationViewModel theViewModel = new ViewApplicationViewModel();
            try
            {
                int? meetingSessionId = this.thePanelManagementService.GetMeetingSessionId(SelectedPanel);
                SetSessionVariables(SelectedProgramYear, meetingSessionId, SelectedPanel);
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
                // now get a list of the applications assigned to the selected panel
                //
                var applicationInformation = this.thePanelManagementService.ListApplicationInformation(sessionPanelId);
                theViewModel.Applications = new List<IApplicationInformationModel>(applicationInformation.ModelList);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new ViewApplicationViewModel();
                HandleExceptionViaElmah(e);
            }
            return View(ViewNames.ApplicationAbstracts, theViewModel);
        }
        #endregion
        /// <summary>
        /// Controller action for displaying the modal window to create a transfer request transfer 
        /// email to the help desk.
        /// </summary>
        /// <param name="logNumber">Application log number</param>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="currentPanelName">Panel name the application is currently assigned to</param>
        /// <param name="currentPanelId">Panel identifier of the panel the application is currently assigned to</param>
        /// <returns>Application transfer modal window partial view.</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageReviewerAssignmentExpertise)]
        public ActionResult RequestTransfer()
        {
            return PartialView(ViewNames.RequestReviewerTransfer);
        }
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ViewModal()
        {
            return PartialView(ViewNames.ApplicationModal);
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.RequestApplicationTransfer)]
        public ActionResult RequestApplicationModal(string logNumber, int applicationId, string currentPanelName, int currentPanelId)
        {
            //
            // Create the view model & set what information we need for display
            //
            RequestTransferViewModel viewModel = new RequestTransferViewModel();
            viewModel.ApplicationLogNumber = logNumber;
            viewModel.CurrentPanel = currentPanelName;

            try
            {
                //
                // Populate the list of target panels.
                //
                var listONames = thePanelManagementService.ListPanelNames(applicationId, currentPanelId);
                viewModel.AvailablePanels = listONames.ModelList.ToList<ITransferPanelModel>();
                //
                // Populate the list of transfer reasons.
                //
                var reasons = thePanelManagementService.ListTransferReasons();
                viewModel.TransferReasons = reasons.ModelList.ToList<IReasonModel>();
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                viewModel = new RequestTransferViewModel();
                HandleExceptionViaElmah(e);
            }
            return PartialView(ViewNames.RequestTransfer, viewModel);
        }
        /// <summary>
        /// Gets the cycles.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetCycles(int programYearId)
        {
            List<int> cyclesModel = new List<int>();
            try
            {
                var cycles = theCriteriaService.GetProgramYearCycles(programYearId).ModelList;
                cyclesModel = cycles.ToList();
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(cyclesModel, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get Application candidates.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetApplicationCandidates(int programYearId, int panelId, int cycle)
        {
            var availableApps = new List<KeyValuePair<int, string>>();
            try
            {
                availableApps = thePanelManagementService.GetApplications(programYearId, cycle);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(availableApps, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Adds the panel applications.
        /// </summary>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="applicationIds">The application ids.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManagePanelApplication)]
        public ActionResult AddPanelApplications(int panelId, string applicationIds)
        {
            var status = false;
            var newAppIds = JsonConvert.DeserializeObject<List<int>>(applicationIds);
            try
            {
                status = thePanelManagementService.AddPanelApplications(panelId, newAppIds, GetUserId());
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { status = status });
        }
        /// <summary>
        /// Emails a transfer request to the P2RMIS help desk and logs the message.
        /// </summary>
        /// <param name="comment">Comment on why the application is being transferred</param>
        /// <param name="sourcePanelName">Panel where the application is currently located</param>
        /// <param name="reason">Reason panel is being transferred</param>
        /// <param name="applicationLogNumber">Application identifier</param>
        /// <param name="targetPanelName">Destination panel</param>
        /// <param name="fullPanelName">Current panel's full panel name</param>
        /// <returns>Status indicator</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.RequestApplicationTransfer)]
        public JsonResult RequestApplicationTransfer(string comment, string sourcePanelName, string reason, string applicationLogNumber, string targetPanelName, string fullPanelName)
        {
            bool status = true;
            try
            {
                int userId = GetUserId();

                string message = theMailService.TransferRequest(userId, applicationLogNumber, sourcePanelName, targetPanelName, fullPanelName, reason, comment);

                thePanelManagementService.LogApplicationTranferRequest(message, userId);
            }
            catch (Exception e)
            {
                status = false;
                HandleExceptionViaElmah(e);
            }

            return Json(new { status, error = string.Empty }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// View/Edit overview section
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>The overview value</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManagePanelApplication)]
        public ActionResult EditOverview(int panelApplicationId)
        {
            var theViewModel = new EditOverviewViewModel();
            try
            {
                theViewModel.PanelApplicationId = panelApplicationId;
                theViewModel.PanelOverview = this.thePanelManagementService.GetPanelSummary(panelApplicationId);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                HandleExceptionViaElmah(e);
            }
            return PartialView(ViewNames.EditOverview, theViewModel);
        }
        /// <summary>
        /// Get destination panels
        /// </summary>
        /// <param name="programYearId"></param>
        /// <param name="panelId"></param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.Manage)]
        public JsonResult GetDestinationPanels(int programYearId, int panelId)
        {
            var results = new List<KeyValuePair<int, string>>();
            try
            {
                var panels = thePanelManagementService.ListPanelSignifications(GetUserId(), programYearId).ModelList
                    .Where(x => x.PanelId != panelId).ToList();
                results = panels.ConvertAll(x => new KeyValuePair<int, string>(x.PanelId, x.PanelAbbreviation));
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                HandleExceptionViaElmah(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the destination panels with participant methods.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.Manage)]
        public JsonResult GetDestinationPanelsWithParticipantMethods(int programYearId, int panelId)
        {
            var results = new List<ReviewerDestinationPanelViewModel>();
            try
            {
                var panels = thePanelManagementService.ListPanelsWithMeetingTypes(GetUserId(), programYearId)
                    .Where(x => x.Item1 != panelId).ToList();
                results = panels.ConvertAll(x => new ReviewerDestinationPanelViewModel((int)x.Item1, (string)x.Item2, (string)x.Item3));
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                HandleExceptionViaElmah(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManagePanelApplication)]
        public ActionResult RemoveApplicationFromPanel(int panelApplicationId)
        {
            bool flag = false;
            try
            {
                thePanelManagementService.RemoveApplicationFromPanel(panelApplicationId, GetUserId());
                flag = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag });
        }

        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManagePanelApplication)]
        public ActionResult TransferApplicationToPanel(int panelApplicationId, int applicationId, int newSessionPanelId)
        {
            bool flag = false;
            try
            {
                thePanelManagementService.TransferApplicationToPanel(panelApplicationId, applicationId, newSessionPanelId, GetUserId());
                flag = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag });
        }
    }
}