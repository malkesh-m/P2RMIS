using Sra.P2rmis.Bll.Notification;
using Sra.P2rmis.Bll.ProgramRegistration;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.CrossCuttingServices.FileServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.Web.UI.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Bll.AccessAccountManagement;
using Sra.P2rmis.WebModels.Files;

namespace Sra.P2rmis.Web.Controllers
{
    #region Home Base Controller
    /// <summary>
    /// HomeBaseController controller provides access to the BL services required by the home controller and to services contained in the BaseController.
    /// </summary>
    public class HomeBaseController : BaseController
    {
        #region Properties
        /// <summary>
        /// Service providing access to the user profile manage service
        /// </summary>
        protected IUserProfileManagementService theUserProfileManagementService { get; set; }
        /// <summary>
        /// Service providing acces to the ProgramRegistration application
        /// </summary>
        protected IProgramRegistrationService theProgramRegistrationService { get; set; }
        /// <summary>
        /// Service providing maintenance message
        /// </summary>
        protected INotificationService theNotificationService { get; set; }
        /// <summary>
        /// Service providing access to the user AccessAccount management service
        /// </summary>
        protected IAccessAccountManagementService theAccessAccountManagementService { get; set; }
        #endregion

    }
    #endregion
    /// <summary>
    /// Home controller provides the services consumed by the Home page
    /// </summary>
    public class HomeController : HomeBaseController
    {
        #region Construction; Setup & Disposal
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private HomeController() { }

        /// <summary>
        /// Constructor for the Home controller.
        /// </summary>
        /// <param name="theProfileManagementService">Profile management service</param>
        /// <param name="theProgramRegistrationService">Program registration service</param>
        /// <param name="NotificationService">Notification service</param>
        /// <param name="theAccessAccountManagementService">Access account management service</param>
        public HomeController(IUserProfileManagementService theProfileManagementService, IProgramRegistrationService theProgramRegistrationService, 
            INotificationService NotificationService, IAccessAccountManagementService theAccessAccountManagementService)
        {
            this.theUserProfileManagementService = theProfileManagementService;
            this.theProgramRegistrationService = theProgramRegistrationService;
            this.theNotificationService = NotificationService;
            this.theAccessAccountManagementService = theAccessAccountManagementService;
        }
        #endregion
        /// <summary>
        /// Public landing page for entire site
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                CustomIdentity ident = User.Identity as CustomIdentity;
                if (ident != null)
                {               
                    return RedirectToAction("Dashboard", "Home");
                }  
            }
            else
            {
                return RedirectToAction("LogOn", "Account");
            }
            return View();
        }
        /// <summary>
        /// Access to the Dash board
        /// </summary>
        /// <returns>The dash board view</returns>
        public ActionResult Dashboard()
        {
            if (!Request.IsAuthenticated || SecurityHelpers.CheckPasswordAgeExpiredFromSession(Session))
            {
                return RedirectToAction("LogOn", "Account");
            }
            // get banner verification/registration messages
            LoginVerificationMessageViewModel theViewModel = CheckStatus(GetUserId());


            return View(theViewModel);
        }
        public ActionResult NotificationMessage()
        {
            NotificationMessageViewModel viewModel = new NotificationMessageViewModel(); 
            try
            {
                var notifications = theNotificationService.GetMaintenanceNotifications().ToList();
                if (notifications.Count > 0)
                {
                    viewModel = new NotificationMessageViewModel(notifications[0]);
                    viewModel.SetMaintenanceMessage();
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception vi
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Access to the Privacy policy and Terms of use Links
        /// </summary>
        /// <returns></returns>
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        /// <summary>
        /// Access to the Privacy policy and Terms of use Links
        /// </summary>
        /// <returns></returns>
        public ActionResult Copyright()
        {
            return View();
        }
        /// <summary>
        /// Telerik's RadEditor
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult RadEditor()
        {
            return View();
        }
        /// <summary>
        /// Telerik's RadEditor Pro
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult RadEditorPro()
        {
            return View();
        }
        /// <summary>
        /// Homepage Reference Guide
        /// </summary>
        /// <returns>Homepage Reference Guide Modal</returns>
        public ActionResult InfoReferenceGuide()
        {
            return PartialView("_InfoReferenceGuide");
        }        
        /// <summary>
        /// Check for Dashboard banner messages when user checks in.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        internal virtual LoginVerificationMessageViewModel CheckStatus(int userId)
        {
            var model = new LoginVerificationMessageViewModel();
            var userManagePasswordModel = theUserProfileManagementService.GetUserPasswordAndSecurityQuestionInfo(userId);
            var passwordManagementUrl = $"/{Routing.P2rmisControllers.UserProfileManagement}/{Routing.UserProfileManagement.PasswordManagement}";
            var effectiveDaysLeft = userManagePasswordModel.EffectiveDaysUntilPasswordExpiration;
            if (effectiveDaysLeft > 0)
            {
                var warningDaysLeft = ConfigManager.PwdNumberDaysBeforeExpire - ConfigManager.PwdNumberDaysBeforeNotice;
                if (effectiveDaysLeft <= warningDaysLeft)
                {
                    model.VerificationMessages.Add(string.Format(MessageService.PasswordExpirationInXDays, $"{effectiveDaysLeft}", passwordManagementUrl));
                }
            }
            else if (effectiveDaysLeft == 0)
            {
                model.VerificationMessages.Add(string.Format(MessageService.PasswordExpiresToday, passwordManagementUrl));
            }

            if (theUserProfileManagementService.IsReviewer(userId))
            {
                bool? verified = theUserProfileManagementService.IsW9Verified(userId);
                
                // display banner if W9 is missing, has not been verified since entered or is marked as inaccurate
                if (theUserProfileManagementService.IsW9Missing(userId))
                {
                    // missing - download form
                    model.VerificationMessages.Add(string.Format(MessageService.W9FormMissing, ConfigManager.W9FormDownload, ConfigManager.W8FormDownload, ConfigManager.W9FormFax));
                }
                else if (!verified.HasValue)
                {
                    // updated - verify form
                    model.VerificationMessages.Add(string.Format(MessageService.W9Verify, Url.Action(Routing.UserProfileManagement.ViewUser, Routing.P2rmisControllers.UserProfile)));
                }
                else if ((verified.HasValue) && (!verified.Value))
                {
                    // inaccurate - download form
                    model.VerificationMessages.Add(string.Format(MessageService.W9Inaccurate, ConfigManager.W9FormDownload, ConfigManager.W8FormDownload, ConfigManager.W9FormFax));
                }
                else if (theUserProfileManagementService.IsW9Updated(userId))
                {
                    //
                    // W-9 has been updated
                    //
                    string link = "<a href='/" + Routing.P2rmisControllers.UserProfile + "/" + Routing.UserProfileManagement.ViewUser + "'>Profile</a>";
                    model.VerificationMessages.Add(string.Format(MessageService.W9Updated, link));
                }
                //
                // Now that we have done the W-9, check the registration status.
                //
                if (theProgramRegistrationService.AreUsersRegistrationInComplete(userId))
                {
                    //
                    // Registrations are not complete
                    //
                    string link = "<a href='/" + Routing.P2rmisControllers.UserProfile + "/" + Routing.UserProfileManagement.ViewParticipationHistory + "'>Program/Panel Participation</a>";
                    model.VerificationMessages.Add(string.Format(MessageService.RegistrationIncomplete, link));
                }
                //if registration is complete but contract changes are present
                if (theProgramRegistrationService.IsRegistrationContractUpdated(userId))
                {
                    string link = "<a href='/" + Routing.P2rmisControllers.UserProfile + "/" + Routing.UserProfileManagement.ViewParticipationHistory + "'>Program/Panel Participation</a>";
                    model.VerificationMessages.Add(string.Format(MessageService.ContractHasBeenUpdated, link));
                }

            }
            return model;
        }
        /// <summary>
        /// Gets the zip file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrProcessOrReview)]
        public ActionResult GetZipFile(string filePath)
        {
            ActionResult result = File(filePath, FileConstants.MimeTypes.Zip, Path.GetFileName(filePath));
            return result;
        }

        /// <summary>
        /// Get Download Warning View
        /// </summary>
        /// <returns>The view for showing download warning text</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult DownloadWarning()
        {
            return PartialView("_DownloadFileWarning");
        }
    }
}
