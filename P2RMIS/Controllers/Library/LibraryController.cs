using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll.LibraryService;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.ProgramRegistration;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.UI.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Sra.P2rmis.Web.Controllers.Library
{
    /// <summary>
    /// Library controller provides access to the Training & Tutorials application.  Specifically
    /// displays the Library view.
    /// </summary>
    public class LibraryController : LibraryBaseController
    {
        /// <summary>
        /// Library constructor.
        /// </summary>
        /// <param name="theFileService">The FileService.</param>
        /// <param name="theProgramRegistrationService">The ProgramRegistrationService</param>
        /// <param name="thePanelManagementService">The PanelManagementService</param>
        /// <param name="theUserProfileManagementService">The UserProfileManagementService</param>
        public LibraryController(IFileService theFileService, IProgramRegistrationService theProgramRegistrationService, 
            IPanelManagementService thePanelManagementService, IUserProfileManagementService theUserProfileManagementService, ILibraryService theLibraryService)
        {
            this.theFileService = theFileService;
            this.theProgramRegistrationService = theProgramRegistrationService;
            this.thePanelManagementService = thePanelManagementService;
            this.theUserProfileManagementService = theUserProfileManagementService;
            this.theLibraryService = theLibraryService;
        }
        /// <summary>
        /// Get the Library view
        /// </summary>
        /// <returns>The Library view</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Library.AccessLibrary)]
        public ActionResult Index()
        {
            LibraryViewModel model = new LibraryViewModel();
            try
            {
                int userId = GetUserId();
                bool hasFullLibraryPermission = HasPermission(Permissions.Library.AccessFullLibrary);
                bool hasLibraryAnyProgram = HasPermission(Permissions.Library.AccessLibraryAnyProgram);

                var programYears = hasLibraryAnyProgram ? thePanelManagementService.ListProgramYears(userId).ModelList.ToList() : thePanelManagementService.ListActiveProgramYears(userId).ModelList.ToList();
                int programYearId = GetLibraryProgramSession();
                
                if (programYearId <= 0 && programYears.Count > 0)
                {
                    programYearId = programYears[0].ProgramYearId;
                }
                if (programYearId > 0)
                {
                    bool areUsersRegistrationIncomplete = theProgramRegistrationService.AreUsersRegistrationInComplete(userId);
                    var status = theProgramRegistrationService.CheckUserRegistrationStatusForSpecifiProgram(programYearId, userId);
                    // The drop-down and grid are hidden if "accessRestricted" is true
                    bool accessRestricted = !status.OneRegistrationComplete && !status.NoRegistrations;
                    var docs = theLibraryService.GetTrainingDocuments(programYearId, userId, hasFullLibraryPermission).ModelList.ToList();
                    model = new LibraryViewModel(programYears, programYearId, areUsersRegistrationIncomplete, accessRestricted, hasFullLibraryPermission, docs);
                }
                else
                {
                    model = new LibraryViewModel(hasFullLibraryPermission);

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
        /// Set program year identifier into the application session
        /// </summary>
        /// <param name="programYearId">the program year identifier</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Library.AccessLibrary)]
        public JsonResult SetProgramYear(int programYearId)
        {
            bool isSuccessful = false;
            try
            {
                SetLibraryProgramSession(programYearId);
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
        /// Marks the reviewed.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Library.AccessLibrary)]
        public JsonResult MarkReviewed(int programYearId, int documentId, bool reviewed)
        {
            bool isSuccessful = false;
            string reviewedDate = null;
            try
            {
                // Mark reviewed
                theLibraryService.MarkDocumentReviewed(programYearId, documentId, GetUserId());
                reviewedDate = ViewHelpers.FormatDate(GlobalProperties.P2rmisDateTimeNow);
                isSuccessful = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { Success = isSuccessful, ReviewedDate = reviewedDate }, JsonRequestBehavior.AllowGet);
        }
    }
}