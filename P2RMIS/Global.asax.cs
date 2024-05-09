using System;
using System.Security.Principal;
using System.Threading;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Optimization;
using FluentValidation.Mvc;
using Microsoft.Practices.Unity;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.AccessAccountManagement;
using Sra.P2rmis.Bll.ApplicationScoring;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.ProgramRegistration;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Bll.LibraryService;
using Sra.P2rmis.Bll.MeetingManagement;
using Sra.P2rmis.Bll.ReviewerRecruitment;
using Sra.P2rmis.Bll.HotelAndTravel;
using Sra.P2rmis.Bll.TaskTracking;
using Sra.P2rmis.Bll.Notification;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Controllers;
using Sra.P2rmis.Web.Controllers.AccountManagement;
using Sra.P2rmis.Web.Controllers.ManageApplicationScoring;
using Sra.P2rmis.Web.Controllers.MyWorkspace;
using Sra.P2rmis.Web.Controllers.PanelManagement;
using Sra.P2rmis.Web.Controllers.ProgramRegistration;
using Sra.P2rmis.Web.Controllers.UserProfileManagement;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.Web.Controllers.Library;
using Sra.P2rmis.Web.Controllers.TaskTracking;
using Sra.P2rmis.Web.Controllers.Worklist;
using Sra.P2rmis.Bll.Setup;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Bll.Security;

namespace Sra.P2rmis.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private class Constants
        {
            public class ControllerNames
            {
                public const string ApplicationManagement = "ApplicationManagement";
                public const string ApplicationDetails = "ApplicationDetails";
                public const string Report = "Report";
                public const string SummaryStatement = "SummaryStatement";
                public const string SummaryProcessing = "SummaryStatementProcessing";
                public const string SummaryStatementReview = "SummaryStatementReview";
                public const string PanelManagement = "PanelManagement";
                public const string MyWorkspace = "MyWorkspace";
                public const string UserProfileManagement = "UserProfileManagement";
                public const string Account = "Account";
                public const string ManageApplicationScoring = "ManageApplicationScoring";
                public const string TaskTracking = "TaskTracking";
            }
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("apple-touch-icon-114x114-precomposed.png");
            routes.Ignore("apple-touch-icon-72x72-precomposed.png");
            routes.Ignore("apple-touch-icon-57x57-precomposed.png");
            routes.Ignore("apple-touch-icon-precomposed.png");
            routes.Ignore("apple-touch-icon.png");
            routes.Ignore("browserconfig.xml");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.MapRoute(
                "TaskTracking",
                "TaskTracking/EditRequest/{ticketId}",
                new { Controller = "TaskTracking", Action = "EditRequest" });
            routes.MapRoute(
                "Report",
                "Report/RunReport/{reportGroupId}",
                new { Controller = "Report", Action = "RunReport" });
            routes.MapRoute(
                "Setup",
                "Setup/GetFiscalYearsJson/{clientId}",
                new { Controller = "Setup", Action = "GetFiscalYearsJson" });
            routes.MapRoute(
                "MyWorkspace_CanSubmitCritique",
                "MyWorkspace/CanSubmitCritique/{applicationWorkflowId}",
                new { Controller = "MyWorkspace", Action = "CanSubmitCritique" });
            routes.MapRoute(
                "MyWorkspace_EditOverview",
                "MyWorkspace/EditOverview/{panelApplicationId}",
                new { Controller = "MyWorkspace", Action = "EditOverview" });
            routes.MapRoute(
                "SummaryStatementProcessing",
                "SummaryStatementProcessing/CheckinAction/{workflowId}",
                new { Controller = "SummaryStatementProcessing", Action = "CheckinAction" });
            routes.MapRoute(
                "PanelManagement",
                "PanelManagement/GetCycles/{programYearId}",
                new { Controller = "PanelManagement", Action = "GetCycles" });
            routes.MapRoute(
                "SummaryStatement",
                "SummaryStatement/GetPanelsJson/{selectedFY}",
                new { Controller = "SummaryStatement", Action = "GetPanelsJson" });
            routes.MapRoute(
                "PanelManagement_SaveAssignmentTypeThreshold",
                "PanelManagement/SaveAssignmentTypeThreshold/{sessionPanelId}",
                new { Controller = "PanelManagement", Action = "SaveAssignmentTypeThreshold" });
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Account", action = "LogOn", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            //Only access session state if it is available
            if (Context.Handler is IRequiresSessionState || Context.Handler is IReadOnlySessionState)
            {
                //If we are authenticated AND we dont have a session here.. redirect to login page.
                HttpCookie authenticationCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authenticationCookie != null)
                {
                    FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(authenticationCookie.Value);
                    if (!authenticationTicket.Expired)
                    {
                        if (Session[SessionVariables.Verified] == null || Session[SessionVariables.CredentialPermanent] == null)
                        {
                            //This means for some reason the session expired before the authentication ticket. Force a login.
                            FormsAuthentication.SignOut();
                            Session.Abandon();
                            Response.Redirect(FormsAuthentication.LoginUrl, true);
                            return;
                        }
                    }
                }
            }
        }


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AjaxableViewResult.AjaxViewNameConvention =
                context => "_" + context.RouteData.GetRequiredString("action");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var container = RegisterUnityObjects();
            RegisterControllerFactory(container);

            FluentValidationModelValidatorProvider.Configure();
            // Adopt pre-defined security protocol
            if (ConfigManager.AdoptPredefinedSecurityProtocol)
                SetCustomSecurityProtocol();
        }
        /// <summary>
        /// Handles the Start event of the Session control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void Session_Start(Object sender, EventArgs e)
        {
            Session[SessionVariables.Verified] = null;
            Session[SessionVariables.CredentialPermanent] = null;
        }
        /// <summary>
        /// Sets the custom security protocol.
        /// </summary>
        protected void SetCustomSecurityProtocol()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 |
                SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }
        void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            // Get a reference to the current User

            IPrincipal usr = HttpContext.Current.User;

            // If we are dealing with an authenticated forms authentication request

            if (usr.Identity.IsAuthenticated && usr.Identity.AuthenticationType == "Forms")
            {
                FormsIdentity fIdent = usr.Identity as FormsIdentity;

                // Create a CustomIdentity based on the FormsAuthenticationTicket  

                CustomIdentity ci = new CustomIdentity(fIdent.Ticket);

                // Create the CustomPrincipal

                CustomPrincipal p = new CustomPrincipal(ci);

                // Attach the CustomPrincipal to HttpContext.User and Thread.CurrentPrincipal

                HttpContext.Current.User = p;

                Thread.CurrentPrincipal = p;
            }
        }
        /// <summary>
        /// Configuration for Microsoft Unity Dependency Injection Framework
        /// </summary>
        private UnityContainer RegisterUnityObjects()
        {
            var container = new UnityContainer();
            /// Register controllers here
            container.RegisterType<IApplicationManagementService, ApplicationManagementService>();
            container.RegisterType<ISessionService, SessionService>();
            container.RegisterType<IReportService, ReportService>();
            container.RegisterType<IController, ReportController>(Constants.ControllerNames.Report);
            container.RegisterType<ISummaryProcessingService, SummaryProcessingService>();
            container.RegisterType<ISummaryManagementService, SummaryManagementService>();
            container.RegisterType<IWorkflowService, WorkflowService>();
            container.RegisterType<IClientSummaryService, ClientSummaryService>();
            container.RegisterType<IApplicationScoringService, ApplicationScoringService>();
            container.RegisterType<IMailService, MailService>();
            container.RegisterType<ITaskTrackingService, TaskTrackingService>();
            #region Program Management Application
            container.RegisterType<IPanelManagementService, PanelManagementService>();
            //container.RegisterType<IController, PanelManagementController>(Constants.ControllerNames.PanelManagement);       
            #endregion
            #region User Profile Management
            container.RegisterType<IController, UserProfileManagementController>(Constants.ControllerNames.UserProfileManagement);
            #endregion
            #region Account Management
            container.RegisterType<IController, AccountController>(Constants.ControllerNames.Account);
            #endregion
            #region Program Registration
            container.RegisterType<IProgramRegistrationService, ProgramRegistrationService>();
            container.RegisterType<IController, ProgramRegistrationController>(Routing.P2rmisControllers.ProgramRegistration);
            #endregion
            #region Library Service
            container.RegisterType<ILibraryService, LibraryService>();
            container.RegisterType<IController, LibraryController>(Routing.P2rmisControllers.Library);
            #endregion
            #region Reviewer Recruitment Services
            container.RegisterType<IReviewerRecruitmentService, ReviewerRecruitmentService>();
            container.RegisterType<IController, WorklistController>(Routing.P2rmisControllers.Worklist);
            #endregion
            #region Hotel & Travel Services
            container.RegisterType<IHotelAndTravelService, HotelAndTravelService>();
            #endregion
            #region Setup Services 
            container.RegisterType<ISetupService, SetupService>();
            #endregion
            /// Register other types here
            container.RegisterType<ITest, Message>();
            container.RegisterType<ICriteriaService, CriteriaService>();
            container.RegisterType<IReportService, ReportService>();
            container.RegisterType<ILookupService, LookupService>();
            container.RegisterType<IFileService, FileService>();
            container.RegisterType<IUserProfileManagementService, UserProfileManagementService>();
            container.RegisterType<IAccessAccountManagementService, AccessAccountManagementService>();
            container.RegisterType<IMeetingManagementService, MeetingManagementService>();
            container.RegisterType<IReportViewerService, ReportViewerService>();
            container.RegisterType<INotificationService, NotificationService>();
            container.RegisterType<IConsumerManagementService, ConsumerManagementService>();
            container.RegisterType<ISecurityService, SecurityService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        /// <summary>
        /// Register a controller factory for the Unity DI framework
        /// </summary>
        private void RegisterControllerFactory(UnityContainer container)
        {
            var factory = new UnityControllerFactory(container);
            ControllerBuilder.Current.SetControllerFactory(factory);
        }
    }
}
