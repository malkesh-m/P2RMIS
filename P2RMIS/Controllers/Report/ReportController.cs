using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.Bll;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.Reports;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// Methods responding to HTTP requests from Reporting views.
    /// </summary>
    public class ReportController : BaseController
    {
        private int ReportParameterGroupId;
        #region Properties
        /// <summary>
        /// Service providing access to the reports services.
        /// </summary>
        private IReportService theReportService { get; set; }
        /// <summary>
        /// Service providing access to common search criteria.
        /// </summary>
        private ICriteriaService theCriteriaService { get; set; }
        #endregion

        #region Construction; Setup & Disposal

        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private ReportController()
        {
        }

        /// <summary>
        /// Controller constructor.  Controller controls the following views:
        ///      = Report
        ///      
        /// </summary>
        /// <param name="reportrService">Service coming in from BL</param>
        public ReportController(IReportService reportrService, ICriteriaService criteriaService)
        {
            theReportService = reportrService;
            theCriteriaService = criteriaService;
        }

        /// <summary>
        /// Dispose of the controller
        /// </summary>
        /// <param name="disposing">Indicates if the object should be disposed</param>
        protected override void Dispose(bool disposing)
        {
            ///
            /// if the object has not been disposed & we should dispose the object
            /// 
            if ((!this._disposed) && (disposing))
            {
                theReportService.Dispose();
                base.Dispose(disposing);
                this._disposed = true;
            }
        }

        #endregion

        #region Controller Actions

        /// <summary>
        /// Logic to get a list of reports from the database
        /// </summary>
        /// <param name="reportGroupId">Report group identifier</param>
        /// <returns>List of Reports to the View Model</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Reports.ViewProgramOrPanel)]
        public ActionResult Index(int? reportId)
        {
            //sets the client list for the specific user
            List<int> clientList = GetUsersClientList();

            //building the report tree menu
            IEnumerable<IReportListModel> listOMenuItems = GetMenu();

            List<IReportListModel> test = new List<IReportListModel>(listOMenuItems);
            test.AddRange(test);

            MenuBuilder builder = new MenuBuilder(test, Session);
            List<MenuItem> theMenu = builder.Build();

            //initializing the report view model
            var reportsVM = new ReportViewModel();

            // Find the report description & title for the provided index (if it exists)
            // If the reportId does not have a value (which is the case when the page is 
            // initially loaded) then return empty strings and 0 for id.
            if(reportId.HasValue)
            {
                // Tru to locate the report's entry and retrieve the values from it.
                IReportListModel reportListModel = ReportControllerHelpers.FindReportListModel(reportId, listOMenuItems);
                if (reportListModel == null)
                {
                    GetEmptyVM(reportsVM);
                }
                else
                {
                    reportsVM.SelectedReportId = reportId.GetValueOrDefault(DEFAULT_REPORT_ID);
                    reportsVM.SelectedReportDesc = reportListModel.ReportDescription;
                    reportsVM.SelectedReport = reportListModel.ReportName;
                    reportsVM.SelectedReportGroupId = reportListModel.ReportGroupId;

                    reportsVM.ShowPanel = reportListModel.ShowPanel;
                    reportsVM.IsPanelRequired = reportListModel.IsPanelRequired;
                    reportsVM.IsPanelMultiSelect = reportListModel.IsPanelMultiSelect;

                    reportsVM.ShowCycle = reportListModel.ShowCycle;
                    reportsVM.IsCycleRequired = reportListModel.IsCycleRequired;
                    reportsVM.IsCycleMultiSelect = reportListModel.IsCycleMultiSelect;

                    reportsVM.ShowMeetingType = reportListModel.ShowMeetingType;
                    reportsVM.IsMeetingTypeRequired = reportListModel.IsMeetingTypeRequired;
                    reportsVM.IsMeetingTypeMultiSelect = reportListModel.IsMeetingTypeMultiSelect;

                    reportsVM.ShowMeeting = reportListModel.ShowMeeting;
                    reportsVM.IsMeetingRequired = reportListModel.IsMeetingRequired;
                    reportsVM.IsMeetingMultiSelect = reportListModel.IsMeetingMultiSelect;

                    reportsVM.ShowProgram = reportListModel.ShowProgram;
                    reportsVM.IsProgramRequired = reportListModel.IsProgramRequired;
                    reportsVM.IsProgramMultiSelect = reportListModel.IsProgramMultiSelect;

                    reportsVM.ShowYear = reportListModel.ShowYear;
                    reportsVM.IsYearRequired = reportListModel.IsYearRequired;
                    reportsVM.IsYearMultiSelect = reportListModel.IsYearMultiSelect;

                    reportsVM.SelectedReportParamGroupId = reportListModel.ReportParameterGroupId;
                }
            }
            else
            {
                GetEmptyVM(reportsVM);
            }
            // Permission
            bool canViewProgramLevel = IsValidPermission(Permissions.Reports.ViewProgramLevel);
            reportsVM.CanViewProgramLevel = canViewProgramLevel;
            // Populate the report tree menu to the view model
            reportsVM.ReportMenu = theMenu;
            if (reportId != null)
            {
                Session["ReportParameterGroupId"] = reportsVM.SelectedReportParamGroupId;
            }
            if (reportsVM.SelectedReportParamGroupId == (int)ReportParameterGroup.MeetingReportGroupId)
                {
                if (Session["ReportProgramList"] != null)
                {
                    Session.Remove("ReportFyList");
                    Session.Remove("RetainReportParams");
                }
                //populate the fiscal years list from the programs selected in the session
                var fys = (canViewProgramLevel) ? theCriteriaService.GetAllProgramYearsForPanelBadges() : theCriteriaService.GetAllProgramYearsForPanelBadges(GetUserId());
                reportsVM.FiscalYears = fys.ModelList.Select(x => x.Year).Distinct().OrderByDescending(x => x).ToList();
                //populate the meeting type
                //var meetingTypes = theCriteriaService.GetAllMeetingType(GetUserId());
                //reportsVM.MeetingType = meetingTypes.ModelList.OrderBy(x => x.MeetingTypeName).ToList();
                if ((Session["RetainReportParams"] != null) && ((bool)Session["RetainReportParams"] == true))
                {
                    var meetingTypes = (canViewProgramLevel) ? theCriteriaService.GetAllMeetingType((List<string>)Session["ReportFyList"]) : theCriteriaService.GetAllMeetingType((List<string>)Session["ReportFyList"], GetUserId());
                    reportsVM.MeetingType = meetingTypes.ModelList.OrderBy(x => x.MeetingTypeName).ThenBy(x => x.MeetingTypeId).ToList();
                    var meetings = theCriteriaService.GetMeetingsByMeetingType((List<int>)Session["ReportMeetingTypeList"], (List<string>)Session["ReportFyList"], GetUserId());
                    reportsVM.Meetings = meetings.ModelList.OrderBy(x => x.MeetingDescription).ThenBy(x => x.MeetingTypeId).ToList();
                }
            }
            else
            {
                if(Session["ReportMeetingTypeList"] != null)
                {
                    Session.Remove("RetainReportParams");
                }

                // get users program list based off of their client list
                var programs = (canViewProgramLevel) ? theCriteriaService.GetAllClientPrograms(clientList) :
                    theCriteriaService.GetAllClientPrograms(clientList, GetUserId());
                // populate the programs list in view model for the view from BLL
                reportsVM.Programs = programs.ModelList.OrderBy(x => x.ProgramAbbreviation).ToList();
                //POPULATE THE DROPDOWNS IF USER DECIDED TO RETAIN PARAMETERS IN SESSION
                //determine if there are session variables from the user retaining report parameters
                if ((Session["RetainReportParams"] != null) && ((bool)Session["RetainReportParams"] == true))
                {
                    Session.Remove("ReportMeetingTypeList");
                    Session.Remove("ReportMeetingList");
                    //populate the fiscal years list from the programs selected in the session
                    var fys = (canViewProgramLevel) ? theCriteriaService.GetAllProgramYears((List<int>)Session["ReportProgramList"]) :
                        theCriteriaService.GetAllProgramYears((List<int>)Session["ReportProgramList"], GetUserId());
                    reportsVM.FiscalYears = fys.ModelList.Select(x => x.Year).Distinct().OrderByDescending(x => x).ToList();
                    //populate the panel list from the fiscal years selected in the session
                    var panels = (canViewProgramLevel) ? this.theCriteriaService.GetSessionPanels((List<int>)Session["ReportProgramList"], (List<string>)Session["ReportFyList"]) :
                        this.theCriteriaService.GetSessionPanels((List<int>)Session["ReportProgramList"], (List<string>)Session["ReportFyList"], GetUserId());
                    reportsVM.Panels = panels.ModelList.OrderBy(x => x.PanelAbbreviation).ThenBy(x => x.ProgramAbbreviation).ThenBy(x => x.Year).ToList();
                    var cycles = this.theCriteriaService.GetProgramYearCycles((List<int>)Session["ReportProgramList"], (List<string>)Session["ReportFyList"]);
                    reportsVM.Cycles = cycles.ModelList.Distinct().OrderBy(x => x).ToList();
                }

            }



            //pass view model to view page
            return View(reportsVM);
        }

        /// <summary>
        /// COMMENTS
        /// </summary>
        /// <param name="programIds"></param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Reports.ViewProgramOrPanel)]
        public ActionResult GetFiscalYears(List<int> programIds)
        {
            var result = new List<string>();

            if (IsGetFiscalYearsParamsValid(programIds))
            {
                // Gets users current permissions
                CustomIdentity ident = User.Identity as CustomIdentity;
                // Permission
                bool canViewProgramLevel = IsValidPermission(Permissions.Reports.ViewProgramLevel);
                // Get list of FYs from the BLL
                var fys = (canViewProgramLevel) ? theCriteriaService.GetAllProgramYears(programIds) :
                    theCriteriaService.GetAllProgramYears(programIds, GetUserId());
                if (fys != null)
                {
                    result = fys.ModelList.Select(x => x.Year).Distinct().OrderByDescending(x => x).ToList();
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets Fiscal Years of all porgrams
        /// </summary>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Reports.ViewProgramOrPanel)]
        public ActionResult GetAllProgramFiscalYears()
        {
            var result = new List<string>();

            // Gets users current permissions
            CustomIdentity ident = User.Identity as CustomIdentity;
            // Permission
            bool canViewProgramLevel = IsValidPermission(Permissions.Reports.ViewProgramLevel);
            // Get list of FYs from the BLL
            var fys = (canViewProgramLevel) ? theCriteriaService.GetAllProgramYears() : theCriteriaService.GetAllProgramYearsByUserId(GetUserId());
            if (fys != null)
            {
                result = fys.ModelList.Select(x => x.Year).Distinct().OrderByDescending(x => x).ToList();
            }
           
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Json method returning the panels for a list of programs & fiscal years.
        /// </summary>
        /// <param name="programIds"></param>
        /// <param name="fiscalYears"></param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Reports.ViewProgramOrPanel)]
        public ActionResult GetPanels(List<int> programIds, List<string> fiscalYears)
        {
            var result = new List<ISessionPanelModel>();

            if (IsGetPanelsParamsValid(programIds, fiscalYears))
            {
                // Gets users current permissions
                CustomIdentity ident = User.Identity as CustomIdentity;
                // Permission
                bool canViewProgramLevel = IsValidPermission(Permissions.Reports.ViewProgramLevel);
                // Get list of panels from the BLL
                var panels = (canViewProgramLevel) ? this.theCriteriaService.GetSessionPanels(programIds, fiscalYears) :
                    this.theCriteriaService.GetSessionPanels(programIds, fiscalYears, GetUserId());
                if (panels != null)
                {
                    result = panels.ModelList.OrderBy(x => x.PanelAbbreviation).ThenBy(x => x.ProgramAbbreviation).ThenBy(x => x.Year).ToList();
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Json method returning the cycles for a list of programs & fiscal years.
        /// </summary>
        /// <param name="programIds"></param>
        /// <param name="fiscalYears"></param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Reports.ViewProgramOrPanel)]
        public ActionResult GetCycles(List<int> programIds, List<string> fiscalYears)
        {
            var result = new List<int>();

            if (IsGetPanelsParamsValid(programIds, fiscalYears))
            {
                // get list of items from the BLL
                var cycles = this.theCriteriaService.GetProgramYearCycles(programIds, fiscalYears);
                if (cycles != null)
                {
                    result = cycles.ModelList.Distinct().OrderBy(x => x).ToList();
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Retrieves list of reports for typeahead search
        /// </summary>
        /// <returns>list of report list model to the dropdown in the view</returns>
        /// 
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Reports.ViewProgramOrPanel)]
        public ActionResult GetReportNames()
        {
            return Json(GetMenu(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Invokes the configured SQLReporting server report with the
        /// user entered parameters.
        /// </summary>
        /// <param name="reportGroupId">Report Group identifier</param>
        /// <param name="reportId">Report identifier. Replace by id</param>
        /// <param name="programList">User's selected programs</param>
        /// <param name="fyList">List of fiscal years</param>
        /// <param name="panelList">List of panels</param>
        /// <returns>Redirects to page displaying SQL Reporting Server report output</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Reports.ViewProgramOrPanel)]
        public ActionResult RunReport(int reportGroupId, int reportId, List<int> programList, List<string> fyList, List<int> panelList, List<int> cycleList, bool retainParams, List<int> meetingTypeList, List<int> meetingList)
        {
            //SETTING SESSION VARIABLES FOR RETAINING PARAMETERS
            Session["RetainReportParams"] = retainParams;
            if (retainParams)
            {
                Session["ReportProgramList"] = programList;
                Session["ReportFyList"] = fyList;
                Session["ReportPanelList"] = panelList;
                Session["ReportCycleList"] = cycleList;
                Session["ReportMeetingTypeList"] = meetingTypeList;
                Session["ReportMeetingList"] = meetingList;

            }
            //if the user does not want to retain parameters clear session variables 
            else
            {
                Session.Remove("ReportProgramList");
                Session.Remove("ReportFyList");
                Session.Remove("ReportPanelList");
                Session.Remove("ReportCycleList");
                Session.Remove("ReportMeetingTypeList");
                Session.Remove("ReportMeetingList");



            }

            //if the user would like to retain parameters create session variables

            string queryString = BuildQueryString(reportId, programList, fyList, panelList, cycleList, meetingTypeList, meetingList);
            return Redirect("../Reports/ReportViewer.aspx?" + queryString);
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Reports.ViewProgramOrPanel)]
        public ActionResult ResetParams()
        {
            Session.Remove("ReportProgramList");
            Session.Remove("ReportFyList");
            Session.Remove("ReportPanelList");
            Session.Remove("ReportCycleList");
            Session.Remove("RetainReportParams");

            return Json(Session, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// reset meeting params
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Reports.ViewProgramOrPanel)]
        public ActionResult ResetMeetingParams()
        {
            Session.Remove("ReportFyList");
            Session.Remove("ReportMeetingTypeList");
            Session.Remove("ReportMeetingList");
            Session.Remove("RetainReportParams");

            return Json(Session, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// get meetings by meeting type
        /// </summary>
        /// <param name="meetingTypeIds"></param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Reports.ViewProgramOrPanel)]
        public ActionResult GetMeetingsByMeetingType(List<int> meetingTypeIds, List<string> fiscalYear)
        {
            var result = theCriteriaService.GetMeetingsByMeetingType(meetingTypeIds, fiscalYear, GetUserId());
            return Json(result.ModelList.OrderBy(x => x.MeetingDescription), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// get all meeting types
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Reports.ViewProgramOrPanel)]
        public ActionResult GetMeetingType(List<string> fiscalYear)
        {
            var result = theCriteriaService.GetAllMeetingType(fiscalYear, GetUserId()).ModelList.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helpers
        private void GetEmptyVM(ReportViewModel reportsVM)
        {
            reportsVM.SelectedReportId = DEFAULT_REPORT_ID;
            reportsVM.SelectedReportDesc = string.Empty;
            reportsVM.SelectedReport = string.Empty;
            reportsVM.SelectedReportGroupId = DEFAULT_REPORT_ID;

            reportsVM.ShowPanel = false;
            reportsVM.IsPanelRequired = false;
            reportsVM.IsPanelMultiSelect = false;

            reportsVM.ShowCycle = false;
            reportsVM.IsCycleRequired = false;
            reportsVM.IsCycleMultiSelect = false;

            reportsVM.ShowMeetingType = false;
            reportsVM.IsMeetingTypeRequired = false;
            reportsVM.IsMeetingTypeMultiSelect = false;

            reportsVM.ShowMeeting = false;
            reportsVM.IsMeetingRequired = false;
            reportsVM.IsMeetingMultiSelect = false;

            reportsVM.ShowProgram = false;
            reportsVM.IsProgramRequired = false;
            reportsVM.IsProgramMultiSelect = false;

            reportsVM.ShowYear = false;
            reportsVM.IsYearRequired = false;
            reportsVM.IsYearMultiSelect = false;

            reportsVM.SelectedReportParamGroupId = DEFAULT_REPORT_ID;
        }

        /// <summary>
        /// Validates the parameters for GetFiscalYears() are valid.
        /// </summary>
        /// <param name="programIds">List of program Ids</param>
        /// <returns>True if the parameters are valid; false otherwise</returns>
        private bool IsGetFiscalYearsParamsValid(List<int> programIds)
        {
            return ((programIds != null) && (programIds.Count > 0));
        }
        /// <summary>
        /// Validates the parameters for GetPanels are valid.
        /// </summary>
        /// <param name="programIds"></param>
        /// <param name="fiscalYears"></param>
        /// <returns></returns>
        private bool IsGetPanelsParamsValid(List<int> programIds, List<string> fiscalYears)
        {
            return ((programIds != null) && (programIds.Count > 0) && (fiscalYears != null) && (fiscalYears.Count > 0));
        }
        /// <summary>
        /// Retrieves a list of all reports.
        /// </summary>
        /// <returns>List of all reports</returns>
        private IEnumerable<IReportListModel> GetMenu()
        {
            IEnumerable<IReportListModel> result = new List<IReportListModel>();
            //
            // Get the menu from the BL
            //
            var outcome = this.theReportService.GetMenu(GetUserId());
            if (outcome != null)
            {
                result = outcome.ModelList;
            }

            return result;
        }
        /// <summary>
        /// Build the query string to pass to aspx.
        /// </summary>
        /// 
        /// <returns>Query string report parameter</returns>
        protected string BuildQueryString(int reportId, List<int> programList, List<string> fyList, List<int> panelList, List<int> cycleList, List<int> meetingTypeList, List<int> meetingList)
        {
            string theQueryString = "";
            ReportParameterGroupId = (int)Session["ReportParameterGroupId"];
            if (ReportParameterGroupId == (int)ReportParameterGroup.MeetingReportGroupId)
            { 
                theQueryString = "&reportId=" + reportId + "&fiscalYearList=" + string.Join(",", fyList) +
                                 "&meetingTypeList=" + string.Join(",", meetingTypeList) + "&meetingList=" +
                                 string.Join(",", meetingList);

            }
            else
            {
                theQueryString = "&reportId=" + reportId + "&programList=" + string.Join(",", programList) + "&fiscalYearList=" + string.Join(",", fyList);
                if (panelList != null && panelList.Count > 0)
                    theQueryString = theQueryString + "&panelList=" + string.Join(",", panelList);
                if (cycleList != null && cycleList.Count > 0)
                    theQueryString = theQueryString + "&cycleList=" + string.Join(",", cycleList);
            }

            return theQueryString;
        }
        #endregion

        #region Constants

        /// <summary>
        /// This is the default value for report and report group ids
        /// </summary>
        public const int DEFAULT_REPORT_ID = 0;

        #endregion
    }
}

