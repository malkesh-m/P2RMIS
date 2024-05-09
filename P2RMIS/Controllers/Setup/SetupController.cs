using Newtonsoft.Json;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.Setup;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.Setup;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sra.P2rmis.Web.ViewModels.Shared;
using System.Text.RegularExpressions;
using Sra.P2rmis.Bll.LibraryService;
using Sra.P2rmis.Web.ViewModels.Setup;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.WebModels.Library;
using System.Runtime.InteropServices;
using System.Data;
using Sra.P2rmis.CrossCuttingServices.OpenXmlServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.CrossCuttingServices.FileServices;
using Sra.P2rmis.CrossCuttingServices.HttpServices;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Bll.MeetingManagement;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// Controller class for the Summary Management views.
    /// </summary>
    public partial class SetupController : SetupBaseController
    {
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="SetupController"/> class.
        /// </summary>
        public SetupController() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupController"/> class.
        /// </summary>
        /// <param name="setupService">The setup service.</param>
        /// <param name="criteriaService">The criteria service.</param>
        /// <param name="userProfileManagementService">The user profile management service.</param>
        /// <param name="lookupService">The lookup service.</param>
        /// <param name="libraryService">The library service.</param>
        /// <param name="applicationManagementService">The application management service.</param>
        public SetupController(ISetupService setupService, ICriteriaService criteriaService,
            IUserProfileManagementService userProfileManagementService, ILookupService lookupService, ILibraryService libraryService,
            IPanelManagementService panelManagementService, IApplicationManagementService applicationManagementService, IMeetingManagementService meetingManagementService)
        {
            theSetupService = setupService;
            theCriteriaService = criteriaService;
            theUserProfileManagementService = userProfileManagementService;
            theLookupService = lookupService;
            theLibraryService = libraryService;
            thePanelManagementService = panelManagementService;
            theApplicationManagementService = applicationManagementService;
            theMeetingManagementService = meetingManagementService;
        }
        #endregion

        /// <summary>
        /// System setup menu.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.General)]
        public ActionResult Menu()
        {
            return View();
        }
        #region Manage Setup Controller Actions
        /// <summary>
        /// Program setup.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult Index()
        {
            var vm = new ProgramSetupViewModel();
            return View(vm);
        }
        /// <summary>
        /// Applicationses this instance.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageApplications)]
        public ActionResult Applications()
        {
            var vm = new ApplicationsViewModel();
            return View(vm);
        }

        /// <summary>

        /// <summary>
        /// Meeting setup.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult AwardSetup()
        {
            return View();
        }
        /// <summary>
        /// Gets the clients json.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.General)]
        public ActionResult GetClientsJson()
        {
            List<ClientViewModel> results = new List<ClientViewModel>();
            try
            {
                int userId = GetUserId();
                var clients = theUserProfileManagementService.GetAssignedUserProfileClient(userId).ModelList.ToList();
                results = clients.ConvertAll(x => new ClientViewModel(x)).OrderBy(x => x.ClientName).ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the clients json.
        /// </summary>
        /// <returns>Clients sorted by full name (description)</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.General)]
        public ActionResult GetClientsSortByFullNameJson()
        {
            List<ClientViewModel> results = new List<ClientViewModel>();
            try
            {
                int userId = GetUserId();
                var clients = theUserProfileManagementService.GetAssignedUserProfileClient(userId).ModelList.ToList();
                results = clients.ConvertAll(x => new ClientViewModel(x)).OrderBy(x => x.ClientFullName).ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the fiscal years json.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.General)]
        public ActionResult GetFiscalYearsJson(int clientId)
        {
            List<FiscalYearViewModel> results = new List<FiscalYearViewModel>();
            try
            {
                var list = theSetupService.RetrieveClientProgramYears(clientId);
                results = list.ModelList.ToList().ConvertAll(x => new FiscalYearViewModel(x));
                results = results.OrderByDescending(w => w.IsActive).GroupBy(x => x.YearValue).Select(y => y.First())
                    .OrderByDescending(z => z.YearValue).ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the fee schedules json string.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="meetingTypeId">Meeting type.</param>
        /// <param name="sessionId">Session.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageFeeSchedule)]
        public ActionResult GetClientFeeSchedulesJsonString(int programYearId, int meetingTypeId, int? sessionId)
        {
            List<FeeScheduleViewModel> results = new List<FeeScheduleViewModel>();
            try
            {
                var list = theSetupService.RetrieveFiscalYearFeeScheduleGrid(programYearId, meetingTypeId, sessionId).ModelList.ToList();
                results = list.ConvertAll(x => new FeeScheduleViewModel(x))
                    .Select((item, index) => { item.Index = index + 1; return item; }).ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Content(JsonConvert.SerializeObject(results));
        }
        /// <summary>
        /// Gets the program years json.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.General)]
        public ActionResult GetProgramYearsJson(int clientId, string fiscalYear)
        {
            TempData["FiscalYear"] = fiscalYear;
            TempData["ClientId"] = clientId;
            List<ProgramYearViewModel> results = new List<ProgramYearViewModel>();
            try
            {
                var list = theSetupService.RetrieveFilterablePrograms(clientId, fiscalYear);
                results = list.ModelList.ToList().ConvertAll(x => new ProgramYearViewModel(x))
                    .OrderBy(x => x.ProgramName).ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the awards json string.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult GetAwardsJsonString(int? programYearId, int clientId, string fiscalYear, string isActive)
        {
            List<AwardViewModel> results = new List<AwardViewModel>();
            try
            {
                if (programYearId != null)
                {
                    var list = theSetupService.RetrieveAwardMechanismSetup((int)programYearId);
                    results = list.ModelList.ToList().ConvertAll(x => new AwardViewModel(x))
                        .Select((item, index) => { item.Index = index + 1; return item; }).ToList();

                    Session["programSetupPage_clientId"] = clientId;
                    Session["programSetupPage_fiscalYear"] = fiscalYear;
                    Session["programSetupPage_isActive"] = isActive;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Content(JsonConvert.SerializeObject(results));
        }
        /// <summary>
        /// Gets the referral mapping initial load json.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.General)]
        public ActionResult GetReferralMappingInitialLoadJson(int programYearId, int receiptCycle)
        {
            List<ReferralMappingViewModel> referralMapping = null;
            TempData["ProgramYearId"] = programYearId;
            TempData["ReceiptCycle"] = receiptCycle;
            int referredToPanelTotal = 0;
            int withdrawnTotal = 0;
            int nonCompliantTotal = 0;
            int assignTopanelTotal = 0;
            int? referralMappingId = null;
            bool flag = false;
            try
            {
                flag = theSetupService.HasPanelApplications(programYearId, receiptCycle);                
                if(flag == true)
                {
                    var applicationReleased = thePanelManagementService.GetReferralMappingApplications(programYearId, receiptCycle);
                    if (applicationReleased != null && applicationReleased.Count > 0)
                        referralMappingId = applicationReleased[0]?.ReferralMappingId;
                    referralMapping = GetReferralMappingViews(applicationReleased);                    
                }
                else
                {
                    referralMappingId = theSetupService.GetUploadedReferralMapping(programYearId, receiptCycle).FirstOrDefault()?.ReferralMappingId;
                    if (referralMappingId != null && referralMappingId != 0)
                    {
                        flag = true;
                        var applicationNotreleased = theSetupService.GetReferralMapping((int)referralMappingId);

                        theSetupService.ValidateUploadedReferralMapping(applicationNotreleased, programYearId, receiptCycle);
                        referralMapping = GetReferralMappingViews(applicationNotreleased);
                    }
                }
                if (referralMapping != null && referralMapping.Count > 0)
                {
                    foreach (var item in referralMapping)
                    {
                        referredToPanelTotal += item.ReferredToPanel;
                        withdrawnTotal += item.withdrawnTotal + item.WithDrawn;
                        nonCompliantTotal += (int)item.NonCompliant;
                        assignTopanelTotal += item.AssignedToPanel;
                    }
                    // Add totals to model
                    referralMapping[0].referredToPanelTotal += referredToPanelTotal;
                    referralMapping[0].assignTopanelTotal += assignTopanelTotal;
                    referralMapping[0].withdrawnTotal += withdrawnTotal;
                    referralMapping[0].nonCompliantTotal += nonCompliantTotal;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, referralMappingId = referralMappingId, referralMapping = referralMapping }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Resets the referral mapping json.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageReferralMapping)]
        public ActionResult ResetReferralMappingJson(int referralMappingId)
        {
            bool flag = false;
            try
            {
                var userId = GetUserId();
                flag = theSetupService.ResetReferralMapping(referralMappingId, userId);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Releases the referral mapping json.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageReferralMapping)]
        public ActionResult ReleaseReferralMappingJson(int referralMappingId, List<int> sessionPanelIds)
        {
            bool flag = false;
            var errorMessages = new List<string>();
            try
            {
                var userId = GetUserId();
                errorMessages = thePanelManagementService.ValidateReferralMapping(referralMappingId, sessionPanelIds);
                if (errorMessages.Count == 0)
                {
                    flag = theSetupService.ReleaseReferralMapping(thePanelManagementService, referralMappingId, sessionPanelIds, userId);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, errorResults = errorMessages }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the criteria json string.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult GetCriteriaJsonString(int? programMechanismId)
        {
            List<CriterionViewModel> results = new List<CriterionViewModel>();
            try
            {
                if (programMechanismId != null)
                {
                    var list = theSetupService.RetrieveEvaluationCriterion((int)programMechanismId);
                    results = list.ModelList.ToList().ConvertAll(x => new CriterionViewModel(x))
                        .Select((item, index) => { item.Index = index + 1; return item; }).ToList();
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Content(JsonConvert.SerializeObject(results));
        }

        /// <summary>
        /// Gets the meetings json.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="isDateEnded">Filter only if meeting has been ended</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult GetMeetingsJson(int clientId, bool? isDateEnded)
        {
            List<MeetingViewModel> results = new List<MeetingViewModel>();
            try
            {
                var list = theSetupService.RetrieveSessionSetupMeetingList(clientId);
                if (isDateEnded != null && isDateEnded == true)
                {
                    results = list.ModelList.Where(x => !x.HasEndDatePassed).ToList().ConvertAll(x => new MeetingViewModel(x));
                }
                else
                {
                    results = list.ModelList.ToList().ConvertAll(x => new MeetingViewModel(x));
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the meeting types.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.General)]
        public ActionResult GetMeetingTypesJson()
        {
            List<KeyValuePair<int, string>> results = new List<KeyValuePair<int, string>>();
            try
            {
                results = theLookupService.ListMeetingTypes().ModelList.ToList()
                    .ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets all meetings from Fiscal Year and Client json.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientId">The Client Id.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult GetMeetingsFromYearJson(string fiscalYear, int clientId)
        {
            List<GenericListEntry<int, string>> results = new List<GenericListEntry<int, string>>();
            List<int> clientList = new List<int>();
            clientList.Add(clientId);
            try
            {
                // make sure only onsite meetings are pulled.
                var list = theMeetingManagementService.RetrieveMeetingList(fiscalYear, clientList).ToList();
                results = list;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the fee schedule sessions json.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult GetSessionsFromMeetingJson(int clientMeetingId, int programYearId)
        {
            List<KeyValuePair<int, string>> results = new List<KeyValuePair<int, string>>();
            try
            {
                var list = theLookupService.ListMeetingSessions(clientMeetingId, programYearId).ModelList.ToList();
                results = list.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Adds the mechanism scoring template.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <param name="scoringTemplateId">The scoring template identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult AddMechanismScoringTemplate(int programMechanismId, int scoringTemplateId)
        {
            var flag = false;
            try
            {
                theSetupService.AddMechanismScoringTemplate(programMechanismId, scoringTemplateId, GetUserId());
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
        /// <summary>
        /// Gets the fee schedule meetings json.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year.</param>
        /// <param name="meetingTypeId">Meeting type.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageFeeSchedule)]
        public ActionResult GetFeeScheduleMeetingsJson(int clientId, string fiscalYear, int programYearId, int meetingTypeId)
        {
            List<KeyValuePair<int, string>> results = new List<KeyValuePair<int, string>>();
            try
            {
                var list = theSetupService.RetrieveSessionFeeScheduleMeetingList(clientId, fiscalYear, programYearId, meetingTypeId).ModelList.ToList();
                results = list.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the meetings json string.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult GetMeetingsJsonString(int clientId, string fiscalYear, int? programYearId)
        {
            List<MeetingViewModel> results = new List<MeetingViewModel>();
            try
            {
                var list = theSetupService.RetrieveMeetingSetupGrid(clientId, fiscalYear, programYearId);
                results = list.ModelList.OrderBy(y => y.MeetingAbbreviation).ToList()
                    .ConvertAll(x => new MeetingViewModel(x))
                    .Select((item, index) => { item.Index = index + 1; return item; })
                    .ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Content(JsonConvert.SerializeObject(results));
        }

        /// <summary>
        /// Meeting setup.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult MeetingSetup()
        {
            return View();
        }

        /// <summary>
        /// Session setup.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientMeetingId"></param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SessionSetup(int? clientId, int? clientMeetingId)
        {
            var vm = new SessionSetupViewModel();
            try
            {
                vm.ClientId = clientId;
                vm.MeetingId = clientMeetingId;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(vm);
        }

        /// <summary>
        /// Gets the sessions json string.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult GetSessionsJsonString(int clientMeetingId)
        {
            List<SessionViewModel> results = new List<SessionViewModel>();
            try
            {
                var sessionList = theSetupService.RetrieveSessionSetupGrid(clientMeetingId).ModelList
                    .OrderBy(y => y.SessionAbbreviation)
                    .ThenByDescending(y => y.StartDate).ToList();
                results = sessionList.ConvertAll(x => new SessionViewModel(x))
                    .Select((item, index) => { item.Index = index + 1; return item; })
                    .ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Content(JsonConvert.SerializeObject(results));
        }

        /// <summary>
        /// Gets the session's programs json string.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="sessionId">The client meeting identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult GetSessionProgramsJson(int clientMeetingId, int sessionId)
        {
            List<SessionViewModel> res = new List<SessionViewModel>();

            var results = new List<KeyValuePair<int, string>>();
            List<string> resStr = new List<string>();

            try
            {
                var sessionList = theSetupService.RetrieveSessionSetupGrid(clientMeetingId).ModelList
                    .OrderBy(y => y.SessionAbbreviation)
                    .ThenByDescending(y => y.StartDate).ToList();

                var currSession = sessionList.Where(s => s.MeetingSessionId == sessionId).FirstOrDefault();
            //    results = currSession.SessionPanelList.Select(x => new KeyValuePair<int, string>(x.SessionPanelId, x.ProgramAbbreviation)).Distinct().ToList();
                resStr = currSession.SessionPanelList.Select(x => x.ProgramAbbreviation).Distinct().ToList();

            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Content(JsonConvert.SerializeObject(resStr));
        }


        /// <summary>
        /// Deletes the award.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult DeleteAward(int programMechanismId)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                ServiceState s = theSetupService.DeleteAward(programMechanismId, GetUserId());
                messages = s.Messages.ToList();
                if (messages.Count == 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, messages = messages });
        }
        /// <summary>
        /// Deletes the meeting.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult DeleteMeeting(int clientMeetingId)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                ServiceState s = theSetupService.DeleteMeeting(clientMeetingId, GetUserId());
                messages = s.Messages.ToList();
                if (messages.Count == 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, messages = messages });
        }
        /// <summary>
        /// Deletes the mechanism scoring template.
        /// </summary>
        /// <param name="mechanismScoringTemplateId">The mechanism scoring template identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult DeleteMechanismScoringTemplate(int mechanismScoringTemplateId)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                theSetupService.DeleteMechanismScoringTemplate(mechanismScoringTemplateId, GetUserId());
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
        /// <summary>
        /// Deletes the program.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult DeleteProgram(int clientProgramId, int programYearId)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                ServiceState s = theSetupService.DeleteProgram(clientProgramId, programYearId, GetUserId());
                messages = s.Messages.ToList();
                if (messages.Count == 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, messages = messages });
        }
        /// <summary>
        /// Deletes the program.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult CheckForLastProgramYear(int clientProgramId, int programYearId)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                ServiceState s = theSetupService.CheckForLastProgramYear(clientProgramId, programYearId, GetUserId());
                messages = s.Messages.ToList();
                if (messages.Count == 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, messages = messages });
        }

        /// <summary>
        /// Deletes the evalutaion criteria from the award.
        /// </summary>
        /// <param name="mechanismTemplateElementId">The mechanism template element identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult DeleteEvaluationCriteria(int mechanismTemplateElementId)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                ServiceState s = theSetupService.DeleteEvaluationCriteria(mechanismTemplateElementId, GetUserId());
                messages = s.Messages.ToList();
                if (messages.Count == 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, messages = messages });
        }
        /// <summary>
        /// Deletes the session.
        /// </summary>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult DeleteSession(int meetingSessionId)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                ServiceState s = theSetupService.DeleteSession(meetingSessionId, GetUserId());
                messages = s.Messages.ToList();
                if (messages.Count == 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, messages = messages });
        }
        /// <summary>
        /// View document management file in embedded viewer
        /// </summary>
        /// <param name="documentId">The document identifier</param>
        /// <returns>The PdfViewer view</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ViewDocumentManagementFile(string documentId)
        {
            var fileUrl = $"/Setup/DocumentManagementFile?documentId={documentId}&isPreview=true";
            var downloadUrl = $"/Setup/DocumentManagementFile?documentId={documentId}&isPreview=false";
            return PdfViewer(fileUrl, downloadUrl);
        }
        /// <summary>
        /// Retreives a document for view or download
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="isPreview"></param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult DocumentManagementFile(int documentId, bool isPreview)
        {
            ActionResult result = null;
            try
            {
                var document = theLibraryService.GetPeerReviewDocument(documentId);
                var physicalPath = String.Format(@"{0}\{1}\{2}", ConfigManager.DocumentDirectoryPath, document.ClientId, document.ContentFileLocation);
                var fileName = (new Regex(@"(.+)(\.[a-zA-Z0-9\-]+)(\.[a-zA-Z0-9]+$)")).Replace(document.ContentFileLocation, "$1$3");
                var fileContents = System.IO.File.ReadAllBytes(physicalPath);
                if (fileContents != null && fileContents.Length > 0)
                {
                    if (isPreview)
                    {
                        var pdfStream = PdfServices.ConvertToPdf(fileContents, fileName);
                        result = new FileStreamResult(pdfStream, FileConstants.MimeTypes.Pdf);
                    }
                    else
                    {
                        var contentType = MimeMapping.GetMimeMapping(document.ContentFileLocation);
                        result = File(fileContents, contentType, fileName);
                    }
                }
                else
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(MessageService.FileNoContents));
                    fileContents = PdfServices.CreatePdf(MessageService.FileNoContents, string.Empty, BaseUrl, DepPath);
                    result = File(fileContents, FileConstants.MimeTypes.Pdf);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                // Redirect to file not found error page
                //
                return RedirectToAction("FileNotFound", "ErrorPage");                
            }
            return result;
        }

        /// <summary>
        /// Fees schedule setup.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageFeeSchedule)]
        public ActionResult FeeScheduleSetup()
        {
            var vm = new FeeScheduleSetupViewModel();
            try
            {
                int userId = GetUserId();
                var clients = theUserProfileManagementService.GetAssignedUserProfileClient(userId).ModelList
                    .OrderBy(x => x.ClientName).ToList();
                vm = new FeeScheduleSetupViewModel(clients);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(vm);
        }
        /// <summary>
        /// Program wizard.
        /// </summary>
        /// <returns></returns> 
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult ProgramWizard(int? clientId, int? programYearId)
        {
            var vm = new ProgramWizardViewModel();
            try
            {
                if (clientId != null && programYearId != null)
                {
                    var program = theSetupService.RetrieveProgramSetupWizard((int)clientId, (int)programYearId).ModelList.ToList()[0];
                    vm = new ProgramWizardViewModel(program);
                }
                int userId = GetUserId();
                var clients = theUserProfileManagementService.GetAssignedUserProfileClient(userId).ModelList
                    .OrderBy(x => x.ClientName).ToList();
                vm.SetClients(clients);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            // send view model to view page
            return PartialView(ViewNames.ProgramWizard, vm);
        }

        /// <summary>
        /// Gets the programs json.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetProgramsJson(int clientId, string fiscalYear)
        {
            List<ClientProgramViewModel> results = new List<ClientProgramViewModel>();
            try
            {
                var list = theCriteriaService.GetOpenClientPrograms(clientId).ModelList;
                if (fiscalYear != null)
                {
                    list = list.Where(x => x.FiscalYears.Contains(fiscalYear));
                }
                results = list.OrderBy(x => x.ProgramName).ToList().ConvertAll(x => new ClientProgramViewModel(x));
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets programs in JSON.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetClientProgramsJson()
        {
            List<ClientProgramViewModel> results = new List<ClientProgramViewModel>();
            try
            {
                int userId = GetUserId();
                var clientIds = theUserProfileManagementService.GetAssignedActiveClients(userId).ModelList.ToList().ConvertAll(x => x.ClientId);
                var list = theCriteriaService.GetOpenClientPrograms(clientIds).ModelList;
                results = list.OrderBy(x => x.ProgramName).ToList().ConvertAll(x => new ClientProgramViewModel(x));
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            // send view model to view page
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets programs in JSON string.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetProgramsJsonString(int clientId, string fiscalYear, bool isActive)
        {
            List<ProgramViewModel> programsView = null;
            try
            {
                var programs = theSetupService.GetProgramsByFiscalYear(clientId, fiscalYear);
                if (isActive)
                {
                    programs = programs.Where(x => x.Active);
                }
                programsView = programs
                    .ToList()
                    .ConvertAll(x => new ProgramViewModel(x))
                    .Select((item, index) => { item.Index = index + 1; return item; }).ToList();

                Session["programSetupPage_clientId"] = clientId;
                Session["programSetupPage_fiscalYear"] = fiscalYear;
                Session["programSetupPage_isActive"] = isActive;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            // send view model to view page
            return Content(JsonConvert.SerializeObject(programsView));
        }

        /// <summary>
        /// Warning message of removing a setup entity.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult RemoveWarning()
        {
            return PartialView(ViewNames.RemoveWarning);
        }

        /// <summary>
        /// Award wizard.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult AwardWizard(int clientId, int? programYearId, int? programMechanismId)
        {
            var vm = new AwardWizardViewModel();
            try
            {
                if (programYearId != null && programMechanismId != null)
                {
                    var model = theSetupService.RetrieveAwardSetupModal((int)programYearId, (int)programMechanismId).Model;
                    vm = new AwardWizardViewModel(model);
                }
                if (vm.ParentProgramMechanismId != null)
                {
                    vm.AwardTypes = theLookupService.ListChildAwardTypes(clientId).ModelList.ToList()
                        .ConvertAll(x => new AwardTypeViewModel(x));
                }
                else
                { 
                    vm.AwardTypes = theLookupService.ListAwardTypes(clientId).ModelList.ToList()
                        .ConvertAll(x => new AwardTypeViewModel(x));
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.AwardWizard, vm);
        }

        /// <summary>
        /// Gets the awards by cycle.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult GetAwardsByCycle(int programYearId, int cycle)
        {
            var awards = new List<AwardTypeViewModel>();
            try
            {
                awards = theLookupService.ListAwardTypesByClientWithCycleFilter(programYearId, cycle).ModelList.ToList()
                    .ConvertAll(x => new AwardTypeViewModel(x));
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Content(JsonConvert.SerializeObject(awards));
        }

        /// <summary>
        /// Disallowed message of removing a setup entity.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult RemoveDisallowed()
        {
            return PartialView(ViewNames.RemoveDisallowed);
        }

        /// <summary>
        /// Criterion setup.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult CriterionSetup(int programMechanismId)
        {
            var vm = new CriterionSetupViewModel();
            try
            {
                var header = theSetupService.RetrieveEvaluationCriterionHeader(programMechanismId).Model;
                var criteria = theSetupService.RetrieveEvaluationCriterion(programMechanismId).ModelList.ToList();
                var templates = theLookupService.ListScoringTemplates(header.ClientId).ModelList.ToList();
                vm = new CriterionSetupViewModel(header, criteria, templates);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(vm);
        }
        /// <summary>
        /// Criterions the wizard.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="mechanismTemplateId">The mechanism template identifier.</param>
        /// <param name="mechanismTemplateElementId">The mechanism template element identifier.</param>
        /// <param name="partialEdit">Indicates whether all fields on the form are available for edit.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult CriterionWizard(int clientId, int? mechanismTemplateId, int? mechanismTemplateElementId, bool? partialEdit)
        {
            var vm = new CriterionWizardViewModel();
            ViewBag.PartialEdit = false;
            try
            {
                var criteriaList = theLookupService.ListClientEvaluationCriteria2(clientId).ModelList.ToList();
                if (mechanismTemplateElementId == null)
                {
                    var criteriaModel = theSetupService.RetrieveEvaluationCriteriaAdditionModel(mechanismTemplateId).Model;
                    vm = new CriterionWizardViewModel(criteriaModel, criteriaList);
                }
                else
                {
                    var criteriaModel = theSetupService.RetrieveEvaluationCriteriaModal((int)mechanismTemplateElementId).Model;
                    vm = new CriterionWizardViewModel(criteriaModel, criteriaList);
                    ViewBag.PartialEdit = partialEdit;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.CriterionWizard, vm);
        }

        /// <summary>
        /// Uploads the scoring template.
        /// </summary>
        /// <param name="systemTemplateId">The system template identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult UploadScoringTemplate(int systemTemplateId)
        {
            var vm = new ScoringTemplateWizardViewModel();
            try
            {
                var template = theSetupService.RetrieveUploadScoringTemplateModel(systemTemplateId).Model;
                vm = new ScoringTemplateWizardViewModel(template);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.UploadScoringTemplate, vm);
        }


        #region Meeting Wizard
        /// <summary>
        /// Displays the Meeting Wizard view.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <returns>the meeting wizard view model</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult MeetingWizard(int clientId, int? clientMeetingId)
        {
            MeetingWizardViewModel model = new MeetingWizardViewModel();
            try
            {
                ConfigureMeetingWizardModel(model, clientId);
                if (clientMeetingId != null)
                {
                    //
                    // First we retrieve the common information between the Add & Edit
                    // Then we retrieve the information for this ClientMeeting
                    //
                    ConfigureMeetingWizardModel(model, clientId);
                    ConfigureMeetingWizardModel(model, clientId, (int)clientMeetingId);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.MeetingWizard, model);
        }
        /// <summary>
        /// Configures the view model with values for Editing a ClientMeeting
        /// </summary>
        /// <param name="model">MeetingWizardViewModel view model to populate</param>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="clientMeetingId"></param>
        private void ConfigureMeetingWizardModel(MeetingWizardViewModel model, int clientId, int clientMeetingId)
        {
            var a = theSetupService.RetrieveMeetingSetupModal(clientId, clientMeetingId);
            model.ConfigureMeeting(a.Model);
        }
        /// <summary>
        /// Configures the view model with values for Add/Edit
        /// </summary>
        /// <param name="model">MeetingWizardViewModel view model to populate</param>
        /// <param name="clientId">Client entity identifier</param>
        private void ConfigureMeetingWizardModel(MeetingWizardViewModel model, int clientId)
        {
            //
            // Get the client
            // 
            Container<IListEntry> a = this.theLookupService.ListClientAbbreviation(clientId);
            model.ConfigureClient(clientId, a.Model.Value);
            //
            // Retrieve the lists
            //
            Container<IListEntry> meetingList = this.theLookupService.ListMeetingTypes();
            Container<IListEntry> hotelList = this.theLookupService.ListHotels();
            //
            // Set the lists into the view model
            //
            model.ConfigureLists(ConvertListType(meetingList.ModelList), ConvertListType(hotelList.ModelList));
        }
        /// <summary>
        /// Converts a Enumeration of IListEntry's to a list of KeyValuePair<int, string>'s.
        /// </summary>
        /// <param name="listIn">List of values to convert</param>
        /// <returns>List of <KeyValuePair<int, string>></returns>
        protected List<KeyValuePair<int, string>> ConvertListType(IEnumerable<IListEntry> listIn)
        {
            List<KeyValuePair<int, string>> outList = new List<KeyValuePair<int, string>>();
            listIn.ToList().ForEach(x => outList.Add(new KeyValuePair<int, string>(x.Index, x.Value)));
            return outList;
        }
        #endregion

        /// <summary>
        /// Session wizard.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SessionWizard(int clientMeetingId, int? meetingSessionId)
        {
            var vm = new SessionWizardViewModel();
            try
            {
                if (meetingSessionId != null)
                {
                    var session = theSetupService.RetrieveModifySessionModal((int)meetingSessionId).Model;
                    vm = new SessionWizardViewModel(session);
                }
                else
                {
                    var session = theSetupService.RetrieveAddSessionModal(clientMeetingId).Model;
                    vm = new SessionWizardViewModel(session);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.SessionWizard, vm);
        }
        /// <summary>
        /// Assigns the programs.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult AssignPrograms(int clientId, int clientMeetingId)
        {
            var vm = new AssignProgramsViewModel();
            try
            {
                var availablePrograms = theSetupService.RetrieveAssignModalFiscalYearList(clientId).ModelList.ToList();
                var assignedPrograms = theSetupService.UnassignProgramModal(clientId, clientMeetingId).Model.AssignPrograms;
                vm = new AssignProgramsViewModel(availablePrograms, assignedPrograms);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.AssignPrograms, vm);
        }

        /// <summary>
        /// Unassign programs.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult UnAssignPrograms(int clientId, int clientMeetingId)
        {
            var vm = new UnassignProgramsViewModel();
            try
            {
                var assignedProgramsModel = theSetupService.UnassignProgramModal(clientId, clientMeetingId).Model;
                vm = new UnassignProgramsViewModel(assignedProgramsModel);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.UnAssignPrograms, vm);
        }
        /// <summary>
        ///  Preview criteira layout page
        /// </summary>
        /// <param name="mechanismTemplateId">Mechanism template identifier</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult PreviewCriteriaLayout(int mechanismTemplateId)
        {
            return View();
        }
        /// <summary>
        /// Previews the criteria.
        /// </summary>
        /// <param name="mechanismTemplateId">The mechanism template identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult PreviewCriteria(int mechanismTemplateId)
        {
            var vm = new PreviewCriteriaViewModel();
            try
            {
                var criteria = theSetupService.RetrievePreviewEvaluationCriteriaModel(mechanismTemplateId).ModelList.ToList();
                var legend = theApplicationManagementService.GetScoringLegendByMechanismTemplateId(mechanismTemplateId);
                vm = new PreviewCriteriaViewModel(criteria, legend);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.PreviewCriteria, vm);
        }
        /// <summary>
        /// Panel wizard.
        /// </summary>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult PanelWizard(int meetingSessionId)
        {
            var vm = new PanelWizardViewModel();
            try
            {
                var panel = theSetupService.RetrieveAddSessionPanelModal(meetingSessionId).Model;
                vm = new PanelWizardViewModel(panel);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.PanelWizard, vm);
        }
        /// <summary>
        /// Updates the panel.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult UpdatePanel(int sessionPanelId)
        {
            var vm = new UpdatePanelViewModel();
            try
            {
                var panel = theSetupService.RetrieveUpdateSessionPanelModal(sessionPanelId).Model;
                vm = new UpdatePanelViewModel(panel);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.UpdatePanel, vm);
        }

        /// <summary>
        /// Saves the award.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientAwardTypeId">The client award type identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="receiptDeadline">The receipt deadline.</param>
        /// <param name="blindedFlag">if set to <c>true</c> [blinded flag].</param>
        /// <param name="fundingOpportunityId">The funding opportunity identifier.</param>
        /// <param name="partneringPiAllowedFlag">if set to <c>true</c> [partnering pi allowed flag].</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SaveAward(int? programMechanismId, int? clientProgramId, int? programYearId,
            int clientAwardTypeId, int receiptCycle, DateTime? receiptDeadline,
            bool blindedFlag, string fundingOpportunityId, bool partneringPiAllowedFlag, bool? preApp, DateTime? preAppDue)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                if (programMechanismId == null || programMechanismId == 0)
                {
                    List<ServiceState> s = theSetupService.AddAward((int)programYearId, clientAwardTypeId, receiptCycle,
                        receiptDeadline, blindedFlag, fundingOpportunityId,
                        partneringPiAllowedFlag, preApp, preAppDue, GetUserId());
                    // Now returns app and preapp service state together, so combine messages.
                    messages = s.SelectMany(x => x.Messages).ToList();
                    if (s.First().EntityInfo.ToList().Count > 0) // get programYearId from first servicestate as before
                    {
                        programYearId = s.First().EntityInfo.ToList()[0].EntityId;
                    }
                }
                else
                {
                    ServiceState s = theSetupService.ModifyAward((int)programMechanismId, clientAwardTypeId, receiptCycle,
                        receiptDeadline, blindedFlag, fundingOpportunityId,
                        partneringPiAllowedFlag, GetUserId());
                    messages = s.Messages.ToList();
                }
                if (messages.Count == 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, programMechanismId = programMechanismId, programYearId = programYearId, messages = messages });
        }

        /// <summary>
        /// Checks if pre application matches values entered for new award.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientAwardTypeId">The client award type identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult CheckIfPreAppMatch(int programYearId, int cycle)
        {
            bool checkIfPreAppExists = false;
            try
            {
                checkIfPreAppExists = theLookupService.CheckForAwardPreApps(programYearId, cycle);
            }
            catch (Exception e)
            {

                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { warningFlag = checkIfPreAppExists });
        }

        /// <summary>
        /// Check if scoring scale is missing for mechanism template.
        /// </summary>
        /// <param name="mechanismTemplateId">Mechnaism template identifier</param>
        /// <returns></returns>
        [HttpGet]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult CheckIfScoringScaleIsMissing(int mechanismTemplateId)
        {
            bool CheckIfScoringScaleIsMissing = false;
            try
            {
                var criteria = theSetupService.RetrievePreviewEvaluationCriteriaModel(mechanismTemplateId).ModelList.ToList();
                CheckIfScoringScaleIsMissing = criteria.Count(x => x.ClientScoringScaleId > 0) == 0;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { warningFlag = CheckIfScoringScaleIsMissing }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Saves the pre application award.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <param name="parentProgramMechanismId">The parent program mechanism identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="receiptDeadline">The receipt deadline.</param>
        /// <param name="blindedFlag">if set to <c>true</c> [blinded flag].</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SavePreAppAward(int? programMechanismId, int parentProgramMechanismId,
            int receiptCycle, DateTime? receiptDeadline, bool blindedFlag)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                if (programMechanismId == null || programMechanismId == 0)
                {
                    ServiceState s = theSetupService.AddPreAppAward((int)parentProgramMechanismId, receiptCycle,
                        receiptDeadline, blindedFlag, GetUserId());
                    messages = s.Messages.ToList();
                    if (s.EntityInfo.ToList().Count > 0)
                    {
                        programMechanismId = s.EntityInfo.ToList()[0].EntityId;
                    }
                }
                else
                {
                    ServiceState s = theSetupService.ModifyPreAppAward((int)programMechanismId, receiptCycle,
                        receiptDeadline, blindedFlag, GetUserId());
                    messages = s.Messages.ToList();
                }
                if (messages.Count == 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, programMechanismId = programMechanismId, messages = messages });
        }

        /// <summary>
        /// Saves the program.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="programDescription">The program description.</param>
        /// <param name="programAbbreviation">The program abbreviation.</param>
        /// <param name="programYear">The program year.</param>
        /// <param name="activate">if set to <c>true</c> [activate].</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SaveProgram(int clientId, int? programYearId, int? clientProgramId, string programDescription,
                    string programAbbreviation, string programYear, bool activate)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                if (programYearId == null || programYearId == 0)
                {
                    ServiceState s = theSetupService.AddProgram(clientId, clientProgramId, programDescription, programAbbreviation,
                        programYear, activate, GetUserId());
                    messages = s.Messages.ToList();
                    if (s.EntityInfo.ToList().Count > 0)
                    {
                        programYearId = s.EntityInfo.ToList()[0].EntityId;
                    }
                }
                else
                {
                    ServiceState s = theSetupService.ModifyProgram((int)clientProgramId, (int)programYearId, activate, GetUserId());
                    messages = s.Messages.ToList();
                }
                flag = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, programYearId = programYearId, messages = messages });
        }
        /// <summary>
        /// Saves the evaluation criteria.
        /// </summary>
        /// <param name="clientElementId">The client element identifier.</param>
        /// <param name="mechanismTemplateId">The mechanism template identifier.</param>
        /// <param name="mechanismTemplateElementId">The mechanism template element identifier.</param>
        /// <param name="overallFlag">Returns whether or not the criteria is the overall criteria or not.</param>
        /// <param name="scoreFlag">Returns whether the criteria is scored or not.</param>
        /// <param name="textFlag">Returns any text value.</param>
        /// <param name="recommendedWordCount">Returns the max text value.</param>
        /// <param name="sortOrder">Returns the value of the sort order.</param>
        /// <param name="summaryIncludeFlag">Returns the included summary flag or not.</param>
        /// <param name="summarySortOrder">Returns the value of the order of the summary statement.</param>
        /// <param name="instructionText">Returns the description text.</param>
        /// <param name="showAbbreviationOnScoreboard">Returns the abbreviation.</param>
        /// <param name="addEvaluationFlag">Returns whether or not to add an evaluation.</param>
        /// <returns></returns>        
        [HttpPost]
        [ValidateInput(false)]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SaveCriteria(int clientElementId, int? mechanismTemplateId, int? mechanismTemplateElementId, bool overallFlag, bool scoreFlag, bool textFlag, int? recommendedWordCount, int sortOrder, bool summaryIncludeFlag,
            int? summarySortOrder, string instructionText, bool showAbbreviationOnScoreboard, bool addEvaluationFlag, bool? partEditFlag)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                if (addEvaluationFlag == true)
                {
                    ServiceState s = theSetupService.AddEvaluationCriteria((int)clientElementId, (int)mechanismTemplateId, overallFlag, scoreFlag, textFlag, recommendedWordCount, (int)sortOrder, summaryIncludeFlag, (int?)summarySortOrder, instructionText, GetUserId());
                    messages = s.Messages.ToList();
                    if (s.EntityInfo.ToList().Count > 0)
                    {
                        mechanismTemplateId = s.EntityInfo.ToList()[0].EntityId;
                    }
                }
                else
                {
                    ServiceState s = theSetupService.ModifyEvaluationCriteria((int)clientElementId, (int)mechanismTemplateElementId, overallFlag, scoreFlag, textFlag, recommendedWordCount, (int)sortOrder,
                        summaryIncludeFlag, summarySortOrder, instructionText, showAbbreviationOnScoreboard, GetUserId(), partEditFlag);
                    messages = s.Messages.ToList();
                }
                if (messages.Count == 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, mechanismTemplateId = mechanismTemplateId, messages = messages });
        }
        /// <summary>
        /// Saves the meeting.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="meetingId">The meeting identifier.</param>
        /// <param name="meetingAbbreviation">The meeting abbreviation.</param>
        /// <param name="meetingDescription">The meeting description.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="meetingTypeId">The meeting type identifier.</param>
        /// <param name="hotelId">The hotel identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SaveMeeting(int clientId, int? clientMeetingId, string clientAbbreviation, string meetingAbbreviation,
                string meetingDescription, DateTime startDate, DateTime endDate, string meetingLocation, int meetingTypeId, int? hotelId)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                if (clientMeetingId == null || clientMeetingId == 0)
                {
                    ServiceState s = theSetupService.AddMeeting(clientId, meetingAbbreviation,
                        meetingDescription, startDate.Date, endDate.Date.AddDays(1).AddSeconds(-1), 
                        meetingLocation, meetingTypeId, hotelId, GetUserId());
                    messages = s.Messages.ToList();
                    if (messages.Count == 0)
                    {
                        if (s.EntityInfo.ToList().Count > 0)
                        {
                            clientMeetingId = s.EntityInfo.ToList()[0].EntityId;
                        }
                        flag = true;
                    }
                }
                else
                {
                    ServiceState s = theSetupService.ModifyMeeting((int)clientMeetingId, clientId, clientAbbreviation,
                        meetingAbbreviation, meetingDescription, startDate.Date, endDate.Date.AddDays(1).AddSeconds(-1),
                        meetingLocation, meetingTypeId, hotelId, GetUserId());
                    messages = s.Messages.ToList();
                    if (messages.Count == 0)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, clientMeetingId = clientMeetingId, messages = messages });
        }
        /// <summary>
        /// Saves the assigned program.
        /// </summary>
        /// <param name="programYearIds">The program year identifiers.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SaveAssignProgram(List<int> programYearIds, int clientMeetingId)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                {
                    ServiceState s = theSetupService.AssignProgramMeetings(programYearIds, (int)clientMeetingId, GetUserId());
                    messages = s.Messages.ToList();
                    if (messages.Count == 0)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, clientMeetingId = clientMeetingId, messages = messages });
        }
        /// <summary>
        /// Saves the assigned program to the meeting.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="panelAbbreviation">The abbreviation used for the panel.</param>
        /// <param name="panelName">The name given to the panel.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SaveAssignProgramMeeting(int programYearId, int clientMeetingId, int meetingSessionId, string panelAbbreviation, string panelName)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                {
                    ServiceState s = theSetupService.AddSessionPanel((int)programYearId, (int)clientMeetingId, (int)meetingSessionId, panelAbbreviation, panelName, GetUserId());
                    messages = s.Messages.ToList();
                    if (messages.Count == 0)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, meetingSessionId = programYearId, messages = messages });
        }
        /// <summary>
        /// Deletes the assigned program.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="programMeetingId">The program meeting identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SaveUnassignProgram(List<int> programMeetingIds)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                {
                    ServiceState s = theSetupService.UnassignProgramMeetings(programMeetingIds, GetUserId());
                    messages = s.Messages.ToList();
                    if (messages.Count == 0)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, messages = messages });
        }
        /// <summary>
        /// Saves the session.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="sessionAbbreviation">The abbreviation used for the session.</param>
        /// <param name="sessionDescription">The description given to the session.</param>
        /// <param name="phases">The phases that are added and the dates/times to go with them.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SaveSession(int clientId, int? meetingSessionId, int? clientMeetingId, string sessionAbbreviation, string sessionDescription, List<dynamic> phases)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            var sessionPhases = new List<IGenericListEntry<int?, IPhaseModel>>();
            for (var i = 0; i < phases.Count; i++)
            {
                var phase = JsonConvert.DeserializeObject(phases[i]);
                var sessionPhase = new GenericListEntry<int?, IPhaseModel>();
                var pm = new PhaseModel();
                pm.StepTypeId = phase.Value.StepTypeId;
                pm.StartDate = string.IsNullOrEmpty((string)phase.Value.StartDate) ? null : phase.Value.StartDate;
                pm.EndDate = string.IsNullOrEmpty((string)phase.Value.EndDate) ? null : phase.Value.EndDate;
                pm.ReopenDate = string.IsNullOrEmpty((string)phase.Value.ReopenDate) ? null : phase.Value.ReopenDate;
                pm.CloseDate = string.IsNullOrEmpty((string)phase.Value.CloseDate) ? null : phase.Value.CloseDate;
                sessionPhase.Value = pm;
                sessionPhase.Index = string.IsNullOrEmpty((string)phase.Index) ? null : phase.Index;
                if (sessionPhase.Index != null || (sessionPhase.Value.StartDate != null && sessionPhase.Value.EndDate != null))
                {
                    sessionPhases.Add(sessionPhase);
                }
            }
            try
            {
                var startDate = (DateTime)sessionPhases.Last().Value.StartDate;
                var endDate = (DateTime)sessionPhases.Last().Value.EndDate;
                if (meetingSessionId == null)
                {
                    ServiceState s = theSetupService.AddSession((int)clientId, (int)clientMeetingId, sessionAbbreviation, sessionDescription, sessionPhases, startDate, endDate, GetUserId());
                    messages = s.Messages.ToList();
                    if (messages.Count == 0)
                    {
                        if (s.EntityInfo.ToList().Count > 0)
                        {
                            meetingSessionId = s.EntityInfo.ToList()[0].EntityId;
                        }
                        flag = true;
                    }
                }
                else
                {
                    ServiceState s = theSetupService.ModifySession((int)meetingSessionId, (int)clientMeetingId, sessionAbbreviation, sessionDescription, sessionPhases, startDate, endDate, GetUserId());
                    messages = s.Messages.ToList();
                    if (messages.Count == 0)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, meetingSessionId = meetingSessionId, messages = messages });
        }
        /// <summary>
        /// Saves the updated panel ( modify, move or delete ).
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="panelAbbreviation">The abbreviation used for the panel.</param>
        /// <param name="panelName">The name for the panel.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="newMeetingSessionId">The new meeting session created identifier.</param>
        /// <param name="checkedValue">The value that checks which control to use.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SaveUpdatePanel(int? sessionPanelId, string panelAbbreviation, string panelName, int meetingSessionId, int? newMeetingSessionId, string checkedValue)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                if (checkedValue == "update")
                {
                    {
                        ServiceState s = theSetupService.ModifySessionPanel((int)sessionPanelId, (int)meetingSessionId, panelAbbreviation, panelName, GetUserId());
                        messages = s.Messages.ToList();
                        if (messages.Count == 0)
                        {
                            flag = true;
                        }
                    }
                }
                else if (checkedValue == "move")
                {
                    {
                        ServiceState s = theSetupService.MoveSessionPanel((int)sessionPanelId, (int)newMeetingSessionId, panelAbbreviation, panelName, GetUserId());
                        messages = s.Messages.ToList();
                        if (messages.Count == 0)
                        {
                            if (s.EntityInfo.ToList().Count > 0)
                            {
                                sessionPanelId = s.EntityInfo.ToList()[0].EntityId;
                            }
                            flag = true;
                        }
                    }
                }
                else
                {
                    ServiceState s = theSetupService.DeleteSessionPanel((int)sessionPanelId, GetUserId());
                    messages = s.Messages.ToList();
                    if (messages.Count == 0)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, sessionPanelId = sessionPanelId, messages = messages });
        }
        /// <summary>
        /// Saves the newly created meeting.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="programYearID">The program year Id.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SaveAddMeeting(int programYearId, int clientMeetingId)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                {
                    {
                        ServiceState s = theSetupService.AddProgramMeeting((int)programYearId, (int)clientMeetingId, GetUserId());
                        messages = s.Messages.ToList();
                        if (messages.Count == 0)
                        {
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, clientMeetingId = clientMeetingId, messages = messages });
        }
        /// <summary>
        /// Saves the schedule.
        /// </summary>
        /// <param name="feeSchedule">The fee schedule.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageFeeSchedule)]
        public JsonResult SaveSchedule(HttpPostedFileBase feeSchedule, int? programYearId, int? sessionId)
        {
            bool isSuccessful = false;
            List<string> messages = new List<string>();
            List<FeeScheduleViewModel> feeSchedules = new List<FeeScheduleViewModel>();
            try
            {
                //TODO: upload
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { success = isSuccessful, feeSchedules = feeSchedules, messages = messages });
        }
        /// <summary>
        /// Session fee schedule page
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageFeeSchedule)]
        public ActionResult SessionFeeSchedule()
        {
            var vm = new FeeScheduleSetupViewModel();
            try
            {
                int userId = GetUserId();
                var clients = theUserProfileManagementService.GetAssignedUserProfileClient(userId).ModelList
                    .OrderBy(x => x.ClientName).ToList();
                vm = new FeeScheduleSetupViewModel(clients);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(vm);
        }
        /// <summary>
        /// Adds program.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult AddPanelNoPrograms(int clientId)
        {
            var vm = new AddProgramViewModel();
            try
            {
                var fyList = theSetupService.RetrieveSessionAssignProgramFiscalYearList(clientId).ModelList.ToList();
                vm = new AddProgramViewModel(fyList);
                vm.ClientId = clientId;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.AddPanelNoPrograms, vm);
        }
        /// <summary>
        /// Gets the programs in json.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult GetProgramsByFiscalYearJson(int clientId, string fiscalYear)
        {
            var results = new List<KeyValuePair<int, string>>();
            try
            {
                var programs = theSetupService.RetrieveSessionAssignProgramList(clientId, fiscalYear).ModelList.ToList();
                results = programs.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Uploads the fee schedule.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageFeeSchedule)]
        public ActionResult UploadFeeSchedule()
        {
            return PartialView(ViewNames.UploadFeeSchedule);
        }
        #endregion
        #region Data Transfer Controller Actions
        /// <summary>
        /// Uploads the fee schedule file.
        /// </summary>
        /// <param name="feeSchedule">The fee schedule.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="meetingTypeId">The meeting type identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="overwrite">Overwrites the existing fee schedule if true</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageFeeSchedule)]
        [HttpPost]
        public JsonResult UploadFeeScheduleFile(HttpPostedFileBase feeSchedule, int programYearId, int meetingTypeId, int? sessionId, bool overwrite)
        {
            var flag = false;
            var messages = new List<string>();
            try
            {
                if (overwrite)
                {
                    if (sessionId == null)
                        theSetupService.DeleteProgramFeeSchedule(programYearId, meetingTypeId, GetUserId());
                    else
                    {
                        int meetingSessionId = (int)sessionId;
                        theSetupService.DeleteSessionFeeSchedule(programYearId, meetingTypeId, meetingSessionId, GetUserId());

                    }
                }                    

                // Requires 10 columns
                var requiredColumns = 10;
                var tbl = ExcelServices.GetExcelData(feeSchedule.InputStream, 0, true, requiredColumns);
                if (tbl.Columns.Count == requiredColumns)
                {
                    var fsRows = new List<FeeScheduleUploadModel>();
                    for (var i = 0; i < tbl.Rows.Count; i++)
                    {
                        var row = tbl.Rows[i];
                        var fsRow = new FeeScheduleUploadModel((string)row[0], (string)row[1], (string)row[2], (string)row[3], (string)row[4],
                            (string)row[5], (string)row[6], (string)row[7], (string)row[8], (string)row[9]);
                        fsRows.Add(fsRow);
                    }
                    var errorMessages = theSetupService.AddFeeScheduleList(fsRows, programYearId, meetingTypeId, sessionId, GetUserId()).ToList();
                    messages = MessageService.GetErrorMessages(errorMessages);
                    flag = messages.Count == 0;
                }
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { flag = flag, messages = messages });
        }
  
        /// <summary>
        /// Default method for data transfer operations.
        /// </summary>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.DataTransferAndGenerate)]
        public ActionResult TransferData()
        {
            var hasImportPermission = HasPermission(Permissions.Setup.ImportData);
            if (hasImportPermission)
            {
                return RedirectToAction("ImportData");
            }
            else
            {
                return RedirectToAction("GenerateData");
            }
        }

        /// <summary>
        /// Import application data.
        /// </summary>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ImportData)]
        public ActionResult ImportData()
        {
            TabMenuViewModel.HasPermission = HasPermission;
            TransferDataViewModel theModel = new TransferDataViewModel();
            //set filters from Session if existing
            SetFilterDropdownsFromSession(theModel.FilterModel);
            return View(theModel);
        }
        /// <summary>
        /// Transfers the data json.
        /// </summary>
        /// <param name="clientTransferTypeId">The client transfer type identifier.</param>
        /// <param name="program">The program.</param>
        /// <param name="fy">The fy.</param>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ImportData)]
        public ActionResult TransferDataJson(int clientTransferTypeId, int programMechanismId)
        {
            var flag = false;
            int? importLogId = default(int);
            try
            {
                var award = theSetupService.GetAward(programMechanismId);
                var url = String.Format(ConfigManager.EgsDataTransferUri, award.ProgramAbbreviation, 
                    award.Year, award.LegacyAwardTypeId, award.ReceiptCycle);
                var credentialKey = ConfigManager.EgsDataTransferKey;
                importLogId = theSetupService.TransferData(clientTransferTypeId, programMechanismId, credentialKey, url, GetUserId());
                flag = true;
            }
            catch (Exception ex)
            {
                HandleExceptionViaElmah(ex);
            }
            return Json(new { flag = flag, importLogId = importLogId }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the import log json.
        /// </summary>
        /// <param name="importLogId">The import log identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ImportData)]
        public ActionResult GetImportLogJson(int importLogId)
        {
            var flag = false;
            IImportLogModel importLog = new ImportLogModel();
            try
            {
                importLog = theSetupService.GetImportLog(importLogId);
                flag = true;
            }
            catch (Exception ex)
            {
                HandleExceptionViaElmah(ex);
            }
            return Json(new { flag = flag, success = importLog.SuccessFlag,
                messages = importLog.Messages, 
                importLogId = importLog.ImportLogId,
                importedCount = importLog.ImportedCount,
                failedCount = importLog.ApplicationMessages.Count }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the mechanism transfer detail data in json format.
        /// </summary>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ImportData)]
        public JsonResult GetMechanismTransferDataJson(int clientProgramId, int programYearId, int cycle)
        {
            var result = new List<MechanismTransferDetailViewModel>();
            try
            {
                //set incoming values in Session
                SetSessionVariables(clientProgramId, programYearId, cycle);
                result = theSetupService.RetrieveMechanismTransferDetails(programYearId, cycle).ToList().ConvertAll(x => new MechanismTransferDetailViewModel(x));
            }
            catch (Exception ex)
            {
                HandleExceptionViaElmah(ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Page load for generate data tab.
        /// </summary>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.GenerateDeliverable)]
        public ActionResult GenerateData()
        {
            TabMenuViewModel.HasPermission = HasPermission;
            TransferDataViewModel theModel = new TransferDataViewModel();
            //set filters from Session if existing
            SetFilterDropdownsFromSession(theModel.FilterModel);
            return View(theModel);
        }
        /// <summary>
        /// Gets the deliverable data in json format.
        /// </summary>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.GenerateDeliverable)]
        public JsonResult GetDeliverableDataJson(int clientProgramId, int programYearId, int cycle)
        {
            var result = new List<ProgramDataDeliverableViewModel>();
            try
            {
                //set incoming values in Session
                SetSessionVariables(clientProgramId, programYearId, cycle);
                result = theSetupService.RetrieveProgramDeliverableDetails(programYearId, cycle).ToList().ConvertAll(x => new ProgramDataDeliverableViewModel(x));
            }
            catch (Exception ex)
            {
                HandleExceptionViaElmah(ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Marks the deliverable as QC'd.
        /// </summary>
        /// <param name="programCycleDeliverableId">The program cycle deliverable identifier.</param>
        //[HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.GenerateDeliverable)]
        public JsonResult MarkDeliverableQcJson(int programCycleDeliverableId)
        {
            bool success = false;
            try
            {
                theSetupService.MarkDeliverableQc(programCycleDeliverableId, GetUserId());
                success = true;
            }
            catch (Exception ex)
            {
                HandleExceptionViaElmah(ex);
            }
            return Json(new { success = success, qcName = GetUserLogin(), qcDate = GetCurrentDate() }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Generates the deliverable json.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="clientDeliverableId">The client deliverable identifier.</param>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.GenerateDeliverable)]
        public JsonResult GenerateDeliverableJson(int programYearId, int receiptCycle, int clientDeliverableId)
        {
            bool success = false;
            int newId = 0;
            try
            {
                newId = theSetupService.GenerateDeliverable(programYearId, receiptCycle, clientDeliverableId, GetUserId());
                success = true;
            }
            catch (Exception ex)
            {
                HandleExceptionViaElmah(ex);
            }
            return Json(new { success = success, generateName = GetUserLogin(), generateDate = GetCurrentDate(), newDeliverableId = newId }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Downloads the data deliverable.
        /// </summary>
        /// <param name="programCycleDeliverableId">The program cycle deliverable identifier.</param>
        /// <returns>Excel file</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.GenerateDeliverable)]
        public FileResult DownloadDataDeliverable(int programCycleDeliverableId)
        {
            IDeliverableFileModel file = null;
            try
            {
                file = theSetupService.DownloadExcelDeliverable(programCycleDeliverableId);
            }
            catch (Exception ex)
            {
                HandleExceptionViaElmah(ex);
            }
            return File(file.DeliverableQcFile, FileConstants.MimeTypes.Xlsx, file.DeliverableFileName);
        }
        #endregion
        #region Document Management Actions
        /// <summary>
        /// Documents the management.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageDocument)]
        public ActionResult DocumentManagement()
        {
            return View();
        }
        /// <summary>
        /// Edits the document.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageDocument)]
        public ActionResult EditDocument()
        {
            return View();
        }
        /// <summary>
        /// Gets the active documents json.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageDocument)]
        public ActionResult GetActiveDocumentsJson(int clientId, string fiscalYear, int? clientProgramId)
        {
            var results = new List<PeerReviewDocumentViewModel>();
            if (fiscalYear == "Select Fiscal Year")
            {
                fiscalYear = null;
            }
            try
            {
                var docs = theLibraryService.GetPeerReviewDocuments(clientId, fiscalYear, clientProgramId);
                results = docs.ConvertAll(x => new PeerReviewDocumentViewModel(x)).Where(y => y.Active).ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Content(JsonConvert.SerializeObject(results));
        }
        /// <summary>
        /// Gets the archived documents json.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageDocument)]
        public ActionResult GetArchivedDocumentsJson(int clientId, string fiscalYear, int? clientProgramId)
        {
            var results = new List<PeerReviewDocumentViewModel>();
            if (fiscalYear == "Select Fiscal Year")
            {
                fiscalYear = null;
            }
            try
            {
                var docs = theLibraryService.GetPeerReviewDocuments(clientId, fiscalYear, clientProgramId);
                results = docs.ConvertAll(x => new PeerReviewDocumentViewModel(x)).Where(y => !y.Active).ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Content(JsonConvert.SerializeObject(results));
        }
        /// <summary>
        /// Gets the training categories.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageDocument)]
        public ActionResult GetTrainingCategoriesJson()
        {
            var results = new List<KeyValuePair<int, string>>();
            try
            {
                results = theLibraryService.GetTrainingCategories();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Adds the document.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageDocument)]
        public ActionResult AddDocument()
        {
            return View();
        }
        /// <summary>
        /// Adds the peer review document json.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <param name="heading">The heading.</param>
        /// <param name="description">The description.</param>
        /// <param name="contentTypeId">The content type identifier.</param>
        /// <param name="documentTypeId">The document type identifier.</param>
        /// <param name="trainingCategoryId">The training category identifier.</param>
        /// <param name="contentUrl">The content URL.</param>
        /// <param name="contentFileLocation">The content file location.</param>
        /// <param name="meetingTypeIds">The meeting type ids.</param>
        /// <param name="participantTypeIds">The participant type ids.</param>
        /// <param name="participationMethodIds">The participation method ids.</param>
        /// <param name="participationLevel">The participation level.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageDocument)]
        public ActionResult AddPeerReviewDocumentJson(int clientId, string fiscalYear, int? clientProgramId,
            string heading, string description, int contentTypeId, int documentTypeId, int? trainingCategoryId, string contentUrl,
            string contentFileLocation, string meetingTypeIds, string participantTypeIds, string participationMethodIds, bool? participationLevel)
        {
            var flag = false;
            try
            {
                if (meetingTypeIds == "")
                {
                    meetingTypeIds = default(string);
                }
                if (participantTypeIds == "")
                {
                    participantTypeIds = default(string);
                }
                if (participationMethodIds == "")
                {
                    participationMethodIds = default(string);
                }
                if (fiscalYear == "All")
                {
                    fiscalYear = default(string);
                }
                var model = theLibraryService.AddPeerReviewDocument(clientId, fiscalYear, clientProgramId,
                    heading, description, contentTypeId, documentTypeId, trainingCategoryId, contentUrl,
                    contentFileLocation, GetUserId());
                theLibraryService.AddPeerReviewDocumentAccess(model.DocumentId, meetingTypeIds, participantTypeIds, participationMethodIds, participationLevel, GetUserId());
                flag = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Updates the peer review document json.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <param name="heading">The heading.</param>
        /// <param name="description">The description.</param>
        /// <param name="documentTypeId">The document type identifier.</param>
        /// <param name="trainingCategoryId">The training category identifier.</param>
        /// <param name="meetingTypeIds">The meeting type ids.</param>
        /// <param name="participantTypeIds">The participant type ids.</param>
        /// <param name="participationMethodIds">The participation method ids.</param>
        /// <param name="participationLevel">The participation level.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageDocument)]
        public ActionResult UpdatePeerReviewDocumentJson(int documentId, string fiscalYear, int? clientProgramId,
            string heading, string description, int documentTypeId, int? trainingCategoryId,
            string meetingTypeIds, string participantTypeIds, string participationMethodIds, bool? participationLevel)
        {
            var flag = false;
            try
            {
                if (meetingTypeIds == "")
                {
                    meetingTypeIds = default(string);
                }
                if (participantTypeIds == "")
                {
                    participantTypeIds = default(string);
                }
                if (participationMethodIds == "")
                {
                    participationMethodIds = default(string);
                }
                if (fiscalYear == "All")
                {
                    fiscalYear = default(string);
                }
                theLibraryService.UpdatePeerReviewDocument(documentId, fiscalYear, clientProgramId,
                    heading, description, documentTypeId, trainingCategoryId, GetUserId());
                theLibraryService.UpdatePeerReviewDocumentAccess(documentId, meetingTypeIds, participantTypeIds, participationMethodIds, participationLevel, GetUserId());
                flag = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Saves the file location.
        /// </summary>
        /// <param name="formData">The form data.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageDocument)]
        public JsonResult SaveFileLocation(int clientId, HttpPostedFileBase fileBase)
        {
            bool isSuccessful = false;
            string fileName = "";
            try
            {
                // Upload file
                fileName = SaveFile(fileBase, clientId);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { success = isSuccessful, fileName = fileName });
        }
        /// <summary>
        /// Gets the peer review document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageDocument)]
        public JsonResult GetPeerReviewDocument(int? documentId, bool addDocClient, int newClientId)
        {
            var model = new PeerReviewDocumentUpdateViewModel();
            try
            {
                if (documentId != null)
                {
                    // Document data
                    var document = theLibraryService.GetPeerReviewDocument((int)documentId);
                    model.SetDocument(document);
                    // Document access
                    var access = theLibraryService.GetPeerReviewDocumentAccessByDocumentId((int)documentId);
                    if (access != null)
                    {
                        model.SetAccess(access);
                    }
                }
                // List data
                var contentTypeList = theLibraryService.GetContentTypes();
                var documentTypeList = theLibraryService.GetDocumentTypes();
                var fiscalYearList = theSetupService.RetrieveClientProgramYears(newClientId).ModelList
                    .ToList().ConvertAll(x => new FiscalYearViewModel(x)).Where(w => w.IsActive).GroupBy(x => x.YearValue).Select(y => y.First())
                    .OrderByDescending(z => z.YearValue).ToList().ConvertAll(z2 => z2.YearValue);
                var programList = theCriteriaService.GetOpenClientPrograms(newClientId).ModelList
                    .OrderBy(x => x.ProgramName).ToList().ConvertAll(x => new KeyValuePair<int, string>(x.ClientProgramId, x.ProgramAbbreviation));
                var trainingCategoryList = theLibraryService.GetTrainingCategories();
                var meetingTypeList = theLookupService.ListMeetingTypes().ModelList.ToList().ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
                var participantTypeList = (addDocClient) ? theLookupService.ListParticipantType(newClientId).ModelList.ToList().ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value)) : theLookupService.ListParticipantType(newClientId).ModelList.ToList().ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
                var participationMethodList = theLookupService.ListParticipationMethods().ModelList.ToList().ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
                model.SetLists(contentTypeList, documentTypeList, fiscalYearList, programList, trainingCategoryList, meetingTypeList, participantTypeList,
                    participationMethodList);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Archives the document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageDocument)]
        public ActionResult ArchiveDocument(int documentId)
        {
            var flag = false;
            try
            {
                theLibraryService.ArchiveDocument(documentId, GetUserId());
                flag = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Unarchives the document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageDocument)]
        public ActionResult UnarchiveDocument(int documentId)
        {
            var flag = false;
            try
            {
                theLibraryService.UnarchiveDocument(documentId, GetUserId());
                flag = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Deletes the document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageDocument)]
        public ActionResult DeleteDocument(int documentId)
        {
            var flag = false;
            try
            {
                theLibraryService.DeleteDocument(documentId, GetUserId());
                flag = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Opens Vimeo modal
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult VimeoModal()
        {
            return PartialView(ViewNames.VimeoModal);
        }
        #endregion
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageW9Addresses)]
        public ActionResult W9AddressManagement()
        {
            return View();
        }

        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageReferralMapping)]
        public ActionResult ReferralMapping()
        {
            var vm = new ApplicationsViewModel();
            return View(vm);
        }
        /// <summary>
        /// Saves the file location.
        /// </summary>
        /// <param name="formData">The form data.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageReferralMapping)]
        public JsonResult ProcessReferralMappingExcelFile(HttpPostedFileBase fileBase)
        {
            // This is final object
            List<KeyValuePair<string, string>> finalFile = new List<KeyValuePair<string, string>>();
            try
            {
                var tbl = ExcelServices.GetExcelData(fileBase.InputStream, 0, true, 2);
                finalFile = ExcelServices.SetRMRows(tbl, 2);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            TempData["Referrals"] = finalFile;

            return Json(JsonConvert.SerializeObject(finalFile));
        }
        /// <summary>
        /// Saves the file location.
        /// </summary>
        /// <param name="formData">The form data.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageW9Addresses)]
        public JsonResult ProcessExcelFile(HttpPostedFileBase fileBase)
        {
            // This is final object
            List<List<string>> finalFile = new List<List<string>>();
            try
            {
                var tbl = ExcelServices.GetExcelData(fileBase.InputStream, 0, true, 14);
                finalFile = ExcelServices.SetRows(tbl, 14);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(JsonConvert.SerializeObject(finalFile));
        }

        /// <summary>
        /// Deletes the fee schedule.
        /// </summary>
        /// <param name="mechanismScoringTemplateId">The mechanism scoring template identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageFeeSchedule)]
        public ActionResult DeleteProgramFeeSchedule(int programYearId, int meetingTypeId, int? sessionId)
        {
            bool flag = false;
            List<string> messages = new List<string>();
            try
            {
                if (sessionId == null)
                {
                    theSetupService.DeleteProgramFeeSchedule(programYearId, meetingTypeId, GetUserId());
                    flag = true;
                }
                else
                {
                    int meetingSessionId = (int)sessionId;
                    theSetupService.DeleteSessionFeeSchedule(programYearId, meetingTypeId, meetingSessionId, GetUserId());
                    flag = true;
                }

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
        /// <summary>
        /// Get Applications
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="fiscalYear"></param>
        /// <param name="programYearId"></param>
        /// <param name="receiptCycle"></param>
        /// <param name="panelId"></param>
        /// <param name="awardId"></param>
        /// <param name="logNumber"></param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageApplications)]
        public ActionResult GetApplicationsJson(int? clientId, string fiscalYear, int? programYearId, int? receiptCycle,
            int? panelId, int? awardId, string logNumber)
        {
            int userId = GetUserId();
            bool success = false;
            var applications = new List<ApplicationsManagementViewModel>();
            fiscalYear = (fiscalYear == "") ? null : fiscalYear;
            logNumber = (logNumber == "") ? null : logNumber;
            try
            {
                var canWithdraw = HasPermission(Permissions.Setup.ManageApplicationWithdraw);
                applications = thePanelManagementService.GetApplications(clientId, fiscalYear, programYearId, panelId,
                    receiptCycle, awardId, logNumber, userId).ConvertAll(x => new ApplicationsManagementViewModel(x, canWithdraw));
                success = true;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { success = success, applications = applications }, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// Gets the panels json.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetPanelsJson(int? programYearId)
        {
            var results = new List<IPanelSignificationsModel>();
            try
            {
                int userId = GetUserId();
                var container = thePanelManagementService.ListPanelSignifications(userId, (int)programYearId);
                results = new List<IPanelSignificationsModel>(container.ModelList);
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
        /// open withdraw or update withdraw modal
        /// </summary>
        /// <param name="logNumber"></param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageApplicationWithdraw)]
        public ActionResult GetWithdrawModal(int applicationId)
        {
            ApplicationWithdrawnModal model = new ApplicationWithdrawnModal();
            try
            {
            var withDrawnTypeEnum = from WithdrawnType w in Enum.GetValues(typeof(WithdrawnType))
                select new
                {
                    Value = (int) w,
                    Text = w.ToString()
                };
            ViewData["WithdrawnType"] = new SelectList(withDrawnTypeEnum, "Value", "Text");
            var application = theApplicationManagementService.FindApplicationById(applicationId);
            model.ApplicationId = application.ApplicationId;
            model.LogNumber = application.LogNumber;
            model.WithDrawnBy = application.WithdrawnBy;
            model.WithdrawnDate = application.WithdrawnDate;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                HandleExceptionViaElmah(e);
            }
            
            return PartialView(ViewNames.WithdrawApplication, model);
        }
        /// <summary>
        /// open reset withdrawal modal
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageApplicationWithdraw)]
        public ActionResult GetResetWithdrawModal()
        {
            return PartialView(ViewNames.RemoveWarning);
        }
        /// <summary>
        /// save withdraw status
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageApplicationWithdraw)]
        public JsonResult SaveWithdrawStatus(int applicationId,int? withDrawnBy, DateTime? withDrawnDate)
        {
    
            bool success = false;
            try
            {

                theApplicationManagementService.ModifyWithdrawStatus(applicationId, withDrawnBy, true, withDrawnDate);
                success = true;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);

            }
            return Json(new { success = success }, JsonRequestBehavior.AllowGet);


        }
        /// <summary>
        /// confirm that withdraw status need to be reset to null
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageApplicationWithdraw)]
        public JsonResult ConfirmResetWithdrawStatus(int applicationId)
        {
            bool success = false;
            try
            {

                theApplicationManagementService.ModifyWithdrawStatus(applicationId, null, false, null);
                success = true;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return Json(new { success = success }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Uploads the referral mapping.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.General)]
        public ActionResult UploadReferralMapping(int programYearId, int receiptCycle)
        {
            List<KeyValuePair<string, string>> referrals = (List<KeyValuePair<string, string>>)TempData["Referrals"];
            int referredToPanelTotal = 0;
            int withdrawnTotal = 0;
            int nonCompliantTotal = 0;
            int assignTopanelTotal = 0;

            List<ReferralMappingViewModel> referralMapping = null;
            var errorMessage = new KeyValuePair<List<string>, int>();
            if (referrals != null && referrals.Count > 0)
            {
                errorMessage = thePanelManagementService.SaveReferralMapping(referrals, programYearId, receiptCycle, GetUserId());
                if (errorMessage.Key.Count == 0)
                {
                    var referralsTotal  = theSetupService.GetReferralMapping(errorMessage.Value);
                    referralMapping = GetReferralMappingViews(referralsTotal);
                    if (referralMapping.Count > 0)
                    {
                        foreach (var item in referralMapping)
                        {
                            referredToPanelTotal += item.ReferredToPanel;
                            withdrawnTotal += item.withdrawnTotal + item.WithDrawn;
                            nonCompliantTotal += (int)item.NonCompliant;
                            assignTopanelTotal += item.AssignedToPanel;
                        }
                        // Add totals to model
                        referralMapping[0].referredToPanelTotal += referredToPanelTotal;
                        referralMapping[0].assignTopanelTotal += assignTopanelTotal;
                        referralMapping[0].withdrawnTotal += withdrawnTotal;
                        referralMapping[0].nonCompliantTotal += nonCompliantTotal;
                    }
                }
            }
            var results = new { errorResults = errorMessage, referralResults = referralMapping };
            return  Json(results);
        }
        /// <summary>
        /// Gets the referral mapping post upload json.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ManageReferralMapping)]
        public ActionResult GetReferralMappingPostUploadJson(int referralMappingId)
        { 
            bool flag = false;
            var models = new List<ReferralMappingViewModel>();
            try
            {
                var referralsTotal = theSetupService.GetReferralMapping((int)referralMappingId);
                models = GetReferralMappingViews(referralsTotal);
                if (models != null && models.Count > 0)
                {
                    int referredToPanelTotal = 0;
                    int withdrawnTotal = 0;
                    int nonCompliantTotal = 0;
                    int assignTopanelTotal = 0;
                    foreach (var item in models)
                    {
                        referredToPanelTotal += item.ReferredToPanel;
                        withdrawnTotal += item.withdrawnTotal + item.WithDrawn;
                        nonCompliantTotal += (int)item.NonCompliant;
                        assignTopanelTotal += item.AssignedToPanel;
                    }
                    // Add totals to model
                    models[0].referredToPanelTotal += referredToPanelTotal;
                    models[0].assignTopanelTotal += assignTopanelTotal;
                    models[0].withdrawnTotal += withdrawnTotal;
                    models[0].nonCompliantTotal += nonCompliantTotal;
                }
                flag = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, referralMapping = models }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the referral mapping view data.
        /// </summary>
        /// <param name="referralsTotal">The referrals total.</param>
        /// <returns></returns>
        private List<ReferralMappingViewModel> GetReferralMappingViews(List<ReferralMappingModel> referralsTotal)
        {
            var referralMapping = referralsTotal.GroupBy(x => new { x.PanelName, x.SessionPanelId }).Select(y =>
                new ReferralMappingViewModel(y.Key.PanelName, y.Key.SessionPanelId, y.Count(z => z.ReferralMappingId != null), 
                y.Count(z => z.NonCompliant == (int)ComplianceStatus.ComplianceStatusId),
                y.Count(z => z.WithdrawalStatus), 
                y.Count(z => z.PanelApplicationId > 0))).OrderBy(w => w.PanelName).ToList();
            var misAssignedList = referralsTotal.Where(x => x.AssignedToPanelId != null && x.SessionPanelId != x.AssignedToPanelId);
            foreach (var misAssigned in misAssignedList)
            {
                referralMapping.First(x => x.SessionPanelId == misAssigned.SessionPanelId).AssignedToPanel -= 1;
                var assignedToPanelRm = referralMapping.FirstOrDefault(x => x.SessionPanelId == misAssigned.AssignedToPanelId);
                if (assignedToPanelRm != null)
                {
                    assignedToPanelRm.AssignedToPanel += 1;
                    assignedToPanelRm.Status = ReferralMappingViewModel.RELEASED;
                }
            }
            return referralMapping;
        }
        /// <summary>
        /// Returns error messages.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.ImportData)]
        public ActionResult ImportErrorMessages(int importLogId)
        {
            IImportLogModel importLog = new ImportLogModel();
            try
            {
                importLog = theSetupService.GetImportLog(importLogId);
            }
            catch (Exception ex)
            {
                HandleExceptionViaElmah(ex);
            }
            var model = new ImportErrorViewModel(importLog.Messages,
                importLog.ApplicationMessages.Count, importLog.ImportDate);
            return PartialView(ViewNames.ImportErrorMessages, model);
		}

        /// <summary>
        /// Page for managing summary statement templates and reviewer descriptions
        /// </summary>
        /// <param name="programMechanismId">Unique identifier for a programmechanism entity</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SummarySetup(int programMechanismId)
        {
            SummarySetupViewModel vm = new SummarySetupViewModel();
            try
            {
                var summaryData = theSetupService.GetSummarySetupInfo(programMechanismId);
                vm.Populate(summaryData, programMechanismId);
            }
            catch (Exception ex)
            {
                HandleExceptionViaElmah(ex);
            }
            return View(vm);
        }
        /// <summary>
        /// Save summary setup action
        /// </summary>
        /// <param name="model">Summary setup view model from page</param>
        /// <returns>Redirect back to the summary page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.Manage)]
        public ActionResult SaveSummarySetup(SummarySetupViewModel model)
        {
            try
            {
                List<SummaryStatementReviewerDescription> descriptions = ProcessDescriptionsForSave(model.ReviewerDescriptions);
                theSetupService.SaveSummarySetup(model.ProgramMechanismId, model.SelectedStandardSummaryTemplateId, model.SelectedExpeditedSummaryTemplateId, GetUserId(), descriptions);
                TempData["SuccessMessage"] = GetSummarySaveSuccess(model.IsTemplateEdit, model.IsDescriptionEdit);
            }
            catch (Exception ex)
            {
                HandleExceptionViaElmah(ex);
                TempData["FailureMessage"] = MessageService.FailedSave;
            }

            return RedirectToAction("SummarySetup", new { model.ProgramMechanismId });
        }

        internal List<SummaryStatementReviewerDescription> ProcessDescriptionsForSave(List<ReviewerDescriptionViewModel> reviewerDescriptions)
        {
            var descList = new List<SummaryStatementReviewerDescription>();
            if (reviewerDescriptions != null)
            {
                foreach (var desc in reviewerDescriptions)
                {
                    descList.Add(new SummaryStatementReviewerDescription()
                    {
                        SummaryReviewerDescriptionId = desc.ReviewerDescriptionId,
                        AssignmentOrder = desc.AssignmentOrder,
                        DisplayName = desc.DisplayName,
                        CustomOrder = desc.DisplayOrder
                    });
                }
            }
            return descList;
        }

        internal string GetSummarySaveSuccess(bool isTemplateEdit, bool isDescriptionEdit)
        {
            return isTemplateEdit ? (isDescriptionEdit ? MessageService.ReviewerDescriptionAndTemplateSaveSuccess : MessageService.SelectedTemplateSaveSuccess) : MessageService.ReviewerDescriptionSaveSuccess;
        }
    }
}