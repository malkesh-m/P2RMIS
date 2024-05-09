using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll.ProgramRegistration;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.CrossCuttingServices.FileServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.ProgramRegistration;

namespace Sra.P2rmis.Web.Controllers.ProgramRegistration
{
    /// <summary>
    /// 
    /// </summary>
    public class ProgramRegistrationStatusController : ProgramRegistrationBaseController
    {
        #region Construction; Setup & Disposal
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private ProgramRegistrationStatusController()
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="programRegistrationService">Program Registration service</param>
        /// <param name="criteriaService">The criteria service.</param>
        /// <param name="fileService">The file service.</param>
        public ProgramRegistrationStatusController(IProgramRegistrationService programRegistrationService, ICriteriaService criteriaService, IFileService fileService, ILookupService lookupService)
        {
            this.theProgramRegistrationService = programRegistrationService;
            this.theCriteriaService = criteriaService;
            this.theFileService = fileService;
            this.theLookupService = lookupService;
        }
        #endregion
        #region Services Provided
        //
        // GET: /ProgramRegistrationStatus/
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.RegistrationDocument.ViewAndModify)]
        public ActionResult Index()
        {
            var model = new ProgramRegistrationStatusViewModel();
            try
            {
                // Set program list from service layer
                var clientList = GetUsersClientList();
                model.ClientPrograms = theCriteriaService.GetOpenClientPrograms(clientList).ModelList.ToList();
                model.HasCustomizeContractPermissions = HasPermission(Permissions.RegistrationDocument.CustomizeContract);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);

                throw;
            }
            return View(model);
        }
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.RegistrationDocument.ViewAndModify)]
        public ActionResult GetRegistrationStatusList(ProgramRegistrationStatusViewModel model)
        {
            IEnumerable<IProgramRegistrationWebModel> registrationStatusList = null;
            try
            {
                SetSessionVariables(model.SelectedClientProgram, model.SelectedProgramYear, model.SelectedSessionPanels);
                var selectedSessionPanels = GetSessionVariableListIds(SessionVariables.PanelIdList);
                if (selectedSessionPanels.Any())
                {
                    registrationStatusList = theProgramRegistrationService.GetRegistrationStatus(selectedSessionPanels)
                                .ModelList
                                .ToList();
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);

                throw;
            }
            
            return Json(registrationStatusList, JsonRequestBehavior.AllowGet);
        }
       

        /// <summary>
        /// Downloads the word contract.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.RegistrationDocument.ViewAndModify)]
        public ActionResult DownloadWordContract(int panelUserAssignmentId)
        {
            byte[] fileContents;
            var name = String.Format("{0}.{1}", Guid.NewGuid().ToString(), "docx");
            try
            {
                fileContents = theFileService.RetrieveWordContract(panelUserAssignmentId);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);

                throw;
            }
            return File(fileContents, FileConstants.MimeTypes.Docx, name);
        }


        /// <summary>
        /// Confirms the off line contract via JSON.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns>Json result telling whether the save was successful.</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.RegistrationDocument.ViewAndModify)]
        public ActionResult ConfirmOfflineContract(int panelUserAssignmentId)
        {
            try
            {
                this.theProgramRegistrationService.SaveDocumentOffline(panelUserAssignmentId, GetUserId());
                TempData["message"] = MessageService.ConfirmOfflineContractSuccess;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Show the Registration Status Quick Reference Guide
        /// </summary>
        /// <returns>Quick Reference Guide view</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.RegistrationDocument.ViewAndModify)]
        public ActionResult QuickReferenceGuide()
        {
            return PartialView("_QuickReferenceGuide");
        }

        /// <summary>
        /// Open the customized contract modal
        /// </summary>
        /// <returns>Customized Contract view</returns>
        /// 
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.RegistrationDocument.CustomizeContract)]
        public ActionResult CustomizedContractModal(int panelUserAssignmentId, string revName, int panelUserRegistrationDocumentId, bool canAddAddendum, decimal? consultantFee)
        {
            var theViewModel = new RegistrationCustomizeContractViewModel();

            try
            {
                theViewModel.ContractStatusTypeList = theLookupService.ListContractStatus().ModelList.Where(x => x.Value != null).ToList();
                theViewModel.FullName = revName;
                theViewModel.PanelUserAssignmentId = panelUserAssignmentId;
                theViewModel.PanelUserRegistrationDocumentId = panelUserRegistrationDocumentId;
                theViewModel.CanAddAddendum = canAddAddendum;
                theViewModel.FeeAmount = consultantFee;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                HandleExceptionViaElmah(e);
            }

            return PartialView(ViewNames.CustomizeContract, theViewModel);
        }

        /// <summary>
        /// Save the customized contract 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.RegistrationDocument.CustomizeContract)]
        public ActionResult SaveCustomizedContract(RegistrationCustomizeContractViewModel model)
        {
            try
            {
                var file = model.CustomContractFile != null ? FileServices.GetBinary(model.CustomContractFile.InputStream) : null;
                theProgramRegistrationService.SaveContractContent(model.PanelUserRegistrationDocumentId, model.ContractStatusId, model.FeeAmount, 
                    model.BypassReason, file, GetUserId(), BaseUrl, DepPath);
                TempData["success"] = MessageService.ContractSaveSuccess;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                HandleExceptionViaElmah(e);
                TempData["failure"] = MessageService.FailedSave;
            }

            return RedirectToAction("Index");
        }



        

        /// <summary>
        /// Returns the program years for a given client program
        /// </summary>
        /// <param name="clientProgramId"></param>
        /// <returns>JSON list of ProgramYearModel web models</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult GetProgramYears(int clientProgramId)
        {
            List<IProgramYearModel> programYears = null;
            try
            {
                programYears = theCriteriaService.GetOpenProgramYears(clientProgramId).ModelList.ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);

                throw;
            }
            return Json(programYears, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the session panels for a given program year and cycle
        /// </summary>
        /// <param name="programYearId">Identifier for a program year</param>
        /// <returns>JSON list of SessionPanelModel web models</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult GetSessionPanels(int programYearId)
        {
            List<ISessionPanelModel> sessionPanels = null;
            try
            {
                sessionPanels = theCriteriaService.GetSessionPanels(programYearId).ModelList.ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);

                throw;
            }
            return Json(sessionPanels, JsonRequestBehavior.AllowGet);
        }
        #endregion
	}
}