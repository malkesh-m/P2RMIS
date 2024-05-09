using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ReportViewModel
    {
        #region Constructors
        /// <summary>
        /// Report View model
        /// </summary>
        /// <param name="">Business layer containing report details</param>
        public ReportViewModel()
        {
            //Initializing the programs list
            List<IClientProgramModel> programs = new List<IClientProgramModel>();
            this.Programs = programs;
            //Initializing the fiscal year list
            List<string> fiscalYears = new List<string>();
            this.FiscalYears = fiscalYears;
            //Initializing the panel list
            List<ISessionPanelModel> panels = new List<ISessionPanelModel>();
            this.Panels = panels;
            //Initializing the cycle list
            List<int> cycles = new List<int>();
            this.Cycles = cycles;
            //Initializing meeting type list
            List<IMeetingTypeModel> meetingTypes = new List<IMeetingTypeModel>();
            this.MeetingType = meetingTypes;
            //Initializing meeting list
            List<IClientMeetingModel> meetings = new List<IClientMeetingModel>();
            this.Meetings = meetings;
        }
        #endregion

        #region Properties
        
        /// <summary>
        /// List of report groups
        /// </summary>
        public List<ReportGroups> ReportGroups { get; set; }

        /// <summary>
        /// list of Meeting type
        /// </summary>
        public List<IMeetingTypeModel> MeetingType { get; set; }

        /// <summary>
        /// list of meetings
        /// </summary>
        public List<IClientMeetingModel> Meetings { get; set; }

        /// <summary>
        /// List of Programs
        /// </summary>
        public List<IClientProgramModel> Programs { get; set; }

        /// <summary>
        /// List of Fiscal Years
        /// </summary>
        public List<string> FiscalYears { get; set; }

        /// <summary>
        /// List of Panels
        /// </summary>
        public List<ISessionPanelModel> Panels { get; set; }

        /// <summary>
        /// List of Cycles
        /// </summary>
        public List<int> Cycles { get; set; }

        /// <summary>
        /// The report menu
        /// </summary>
        public List<MenuItem> ReportMenu { get; set; }

        /// <summary>
        /// The selected report Id
        /// </summary>
        public int SelectedReportId { get; set; }

        /// <summary>
        /// Selected report group id
        /// </summary>
        public int SelectedReportGroupId { get; set; }

        /// <summary>
        /// The selected report parameter group Id
        /// </summary>
        public int? SelectedReportParamGroupId { get; set; }

        /// <summary>
        /// Selected report group name
        /// </summary>
        public string SelectedReport { get; set; }
        /// <summary>
        /// Selected report group desc
        /// </summary>
        public string SelectedReportDesc { get; set; }
        /// <summary>
        /// Selected meeting(s) (required)
        /// </summary>
        [Required(ErrorMessage = "Please select meeting(s)")]
        public List<string> SelectedMeetings { get; set; }
        /// <summary>
        /// Selected meeting types (required)
        /// </summary>
        [Required(ErrorMessage = "Please select meeting type(s)")]
        public List<string> SelectedMeetingTypes { get; set; }
        /// <summary>
        /// Selected program(s) (required)
        /// </summary>
        [Required(ErrorMessage = "Please select program(s)")]
        public List<string> SelectedPrograms { get; set; }
        /// <summary>
        /// Selected fiscal year(s) (required)
        /// </summary>
        [Required(ErrorMessage = "Please select fiscal year(s)")]
        public List<string> SelectedFys { get; set; }
        /// <summary>
        /// Selected Panels(s) (optional)
        /// </summary>
        public List<int> SelectedCycles { get; set; }
        /// <summary>
        /// Selected Cycles(s) (optional)
        /// </summary>
        public List<int> SelectedPanels { get; set; }
        /// <summary>
        /// determines if client drop down is disabled
        /// </summary>
        public bool DisabledClientDropDown { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can view program level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can view program level; otherwise, <c>false</c>.
        /// </value>
        public bool CanViewProgramLevel { get; set; }
        /// <summary>
        /// Decides whether the params are going to be retained in session
        /// </summary>
        public bool RetainParams 
        { 
            get 
            {
                return (HttpContext.Current.Session["RetainReportParams"] != null) ? (bool)HttpContext.Current.Session["RetainReportParams"] : false;
            } 
        }
               
        /// <summary>
        /// the selected programs stored in the session
        /// </summary>
        public List<int> SessionProgramList
        {
            get { return (List<int>)HttpContext.Current.Session["ReportProgramList"]; }
        }
        /// <summary>
        /// the selected fys stored in the session
        /// </summary>
        public List<string> SessionFyList
        {
            get { return (List<string>)HttpContext.Current.Session["ReportFyList"]; }
        }
        /// <summary>
        /// the selected panels stored in the session
        /// </summary>
        public List<int> SessionPanelList
        {
            get { return (List<int>)HttpContext.Current.Session["ReportPanelList"]; }
        }
        /// <summary>
        /// the selected sessions stored in the session
        /// </summary>
        public List<int> SessionCycleList
        {
            get { return (List<int>)HttpContext.Current.Session["ReportCycleList"]; }
        }
        /// <summary>
        /// the selected meetings type stored in the session
        /// </summary>
        public List<int> SessionMeetingTypeList
        {
            get { return (List<int>) HttpContext.Current.Session["ReportMeetingTypeList"]; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<int> SessionMeetingList
        {
            get { return (List<int>)HttpContext.Current.Session["ReportMeetingList"]; }
        }
        #endregion

        #region Helpers
        ///// <summary>
        ///// Converts a business layer IReportFacts object to a presentation layer ReoportDetails object.
        ///// </summary>
        ///// <param name="item">IReport Facts object to convert</param>
        ///// <returns>Report Details object</returns>
        //private static ReportDetails IReportFactsToReportDetails(IReportFacts item)
        //{
        //    return new ReportDetails(item);
        //}

        /// <summary>
        /// Converts a business layer ProgramDescription tuple object to a presentation layer ReportGroups object.
        /// </summary>
        /// <param name="reportGroupModel">ProgramDescription tuple object to convert</param>
        /// <returns>ReportGroups object</returns>
        private static ReportGroups ProgramDescriptionsTuppleToReportGroups(Tuple<int, string, string, int> reportGroupModel)
        {
            return new ReportGroups { ReportGroupId = reportGroupModel.Item1, ReportGroupName = reportGroupModel.Item2, ReportGroupDescription = reportGroupModel.Item3 };
        }
        #endregion

        #region ReportParameters

        /// <summary>
        /// the bool value for displaying program parameter 
        /// </summary>
        public bool ShowProgram { get; set; }

        /// <summary>
        /// Determines if the program dropdown should be displayed to the user by setting css class
        /// </summary>
        public string DisplayProgramParamClass
        {
            get
            {
                return (ShowProgram ? "row-fluid" : "hide");
            }
        }

        /// <summary>
        /// the bool value true if program is required 
        /// </summary>
        public bool IsProgramRequired { get; set; }

        /// <summary>
        /// Determines if the program parameter required symbol should be displayed to the user by setting css class
        /// </summary>
        public string DisplayProgramRequiredClass
        {
            get
            {
                return (IsProgramRequired == true ? "fluid" : "hide");
            }
        }

        /// <summary>
        /// the bool value true if program is multiselect
        /// </summary>
        public bool IsProgramMultiSelect { get; set; }

        /// <summary>
        /// the bool value for displaying year parameter 
        /// </summary>
        public bool ShowYear { get; set; }

        /// <summary>
        /// Determines if the year dropdown should be displayed to the user by setting css class
        /// </summary>
        public string DisplayYearParamClass
        {
            get
            {
                return (ShowYear ? "row-fluid" : "hide");
            }
        }

        /// <summary>
        /// the bool value true if year is required 
        /// </summary>
        public bool IsYearRequired { get; set; }

        /// <summary>
        /// Determines if the year parameter required symbol should be displayed to the user by setting css class
        /// </summary>
        public string DisplayYearRequiredClass
        {
            get
            {
                return (IsYearRequired == true ? "fluid" : "hide");
            }
        }

        /// <summary>
        /// the bool value true if year is multiselect
        /// </summary>
        public bool IsYearMultiSelect { get; set; }

        /// <summary>
        /// the bool value for displaying Panel parameter 
        /// </summary>
        public bool ShowPanel { get; set; }

        /// <summary>
        /// Determines if the panel dropdown should be displayed to the user by setting css class
        /// </summary>
        public string DisplayPanelParamClass
        {
            get
            {
                return (ShowPanel ? "row-fluid" : "hide");
            }
        }

        /// <summary>
        /// the bool value true if Panel is required 
        /// </summary>
        public bool IsPanelRequired { get; set; }

        /// <summary>
        /// Determines if the panel parameter required symbol should be displayed to the user by setting css class
        /// </summary>
        public string DisplayPanelRequiredClass
        {
            get
            {
                return (IsPanelRequired == true ? "fluid" : "hide");
            }
        }

        /// <summary>
        /// the bool value true if Panel is multiselect
        /// </summary>
        public bool IsPanelMultiSelect { get; set; }

        /// <summary>
        /// the bool value for displaying Cycle parameter 
        /// </summary>
        public bool ShowCycle { get; set; }

        /// <summary>
        /// Determines if the cycle dropdown should be displayed to the user by setting css class
        /// </summary>
        public string DisplayCycleParamClass
        {
            get
            {
                return (ShowCycle ? "row-fluid" : "hide");
            }
        }

        /// <summary>
        /// the bool value true if Cycle is required 
        /// </summary>
        public bool IsCycleRequired { get; set; }

        /// <summary>
        /// Determines if the cycle parameter required symbol should be displayed to the user by setting css class
        /// </summary>
        public string DisplayCycleRequiredClass
        {
            get
            {
                return (IsCycleRequired == true ? "fluid" : "hide");
            }
        }

        /// <summary>
        /// the bool value true if Cycle is multiselect
        /// </summary>
        public bool IsCycleMultiSelect { get; set; }

        /// <summary>
        /// the bool value for displaying Meeting parameter 
        /// </summary>
        public bool ShowMeeting { get; set; }

        /// <summary>
        /// Determines if the meeting dropdown should be displayed to the user by setting css class
        /// </summary>
        public string DisplayMeetingParamClass
        {
            get
            {
                return (ShowMeeting ? "row-fluid" : "hide");
            }
        }

        /// <summary>
        /// the bool value true if Meeting is required 
        /// </summary>
        public bool IsMeetingRequired { get; set; }

        /// <summary>
        /// Determines if the meeting parameter required symbol should be displayed to the user by setting css class
        /// </summary>
        public string DisplayMeetingRequiredClass
        {
            get
            {
                return (IsMeetingRequired == true ? "fluid" : "hide");
            }
        }

        /// <summary>
        /// the bool value true if Meeting is multiselect
        /// </summary>
        public bool IsMeetingMultiSelect { get; set; }

        /// <summary>
        /// the bool value for displaying Meeting type parameter 
        /// </summary>
        public bool ShowMeetingType { get; set; }

        /// <summary>
        /// Determines if the meeting type dropdown should be displayed to the user by setting css class
        /// </summary>
        public string DisplayMeetingTypeParamClass
        {
            get
            {
                return (ShowMeetingType ? "row-fluid" : "hide");
            }
        }

        /// <summary>
        /// the bool value true if Meeting type is required 
        /// </summary>
        public bool IsMeetingTypeRequired { get; set; }

        /// <summary>
        /// Determines if the meeting type parameter required symbol should be displayed to the user by setting css class
        /// </summary>
        public string DisplayMeetingTypeRequiredClass
        {
            get
            {
                return (IsMeetingTypeRequired == true ? "fluid" : "hide");
            }
        }

        /// <summary>
        /// the bool value true if Meeting type is multiselect
        /// </summary>
        public bool IsMeetingTypeMultiSelect { get; set; }
        #endregion
    }

    public class ReportDetails
    { 
        #region Constructors
        private ReportDetails() { }
        /// <summary>
        /// Report Model
        /// </summary>
        /// <param name="">-----</param>
        public ReportDetails(IReportFacts reports)
        {
            this.ReportId = reports.ReportId;
            this.ReportName = reports.ReportName;
            this.ReportFileName = reports.ReportFileName;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Report unique identifier
        /// </summary>
        public int ReportId { get; internal set; }
        /// <summary>
        ///  Report Name
        /// </summary>
        public string ReportName { get; internal set; }
        /// <summary>
        /// Report File name
        /// </summary>
        public string ReportFileName { get; internal set; }
        #endregion

    }

    public class ReportGroups 
    {
        #region Properties

        /// <summary>
        /// Unique Identifier for report group
        /// </summary>
        public int ReportGroupId { get; internal set; }
        /// <summary>
        /// The report group name
        /// </summary>
        public string ReportGroupName { get; internal set; }
        /// <summary>
        /// The report group description
        /// </summary>
        public string ReportGroupDescription { get; internal set; }
        /// <summary>
        /// The report group link
        /// </summary>
        public string ReportGroupLink { get { return "/Report/Index/?reportGroupId=" + ReportGroupId; } }

        #endregion
    }

    
}