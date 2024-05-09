using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Security.Principal;
using System.Web;
using Microsoft.Reporting.WebForms;
using Sra.P2rmis.Bll;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Models;

namespace Sra.P2rmis.Web.Report
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        #region Properties
        /// <summary>
        /// Service providing access to the report viewer services.
        /// </summary>
        private IReportViewerService theReportViewerService { get; set; }
        private CustomIdentity ident { get; set; }

        private HttpSessionStateBase TheSessionState { get; set; }
        private IReportService theReportService { get; set; }
        #endregion
        #region Constants
        public const string paramSuffix = "List";

        public const string defaultFormat = "WORD";
        public const string DefaultExtension = "doc";
        /// <summary>
        /// {0} = report path
        /// {1} = Report file name
        /// {2} = Report download format
        /// {3} = ApplicationWorkflowId
        /// </summary>
        private static string summaryStatementPageUrl = "?{0}{1}" +
            "&rs:Command=Render&rs:Format={2}&ApplicationWorkflowId={3}";
        #endregion
        #region Page_Init
        protected void Page_Init(object sender, EventArgs e)
        {
            CustomIdentity ident = User.Identity as CustomIdentity;
            //make sure the user is authenticated before proceeding with processing report request
            if (ident == null || !ident.IsAuthenticated)
            {
                Response.Redirect("/ErrorPage/NoAccess");
            }
            reportViewer.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
            theReportViewerService = new ReportViewerService();
            TheSessionState = new HttpSessionStateWrapper(Session);
            theReportService = new ReportService();
        }
        #endregion
        #region Page_Load
        /// <summary>
        /// Event hander invoked when the page is loaded
        /// </summary>
        /// <param name="sender">Control that caused the event</param>
        /// <param name="e">Event arguments</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
           //TODO:We need to add service calls to verify the permissions of the report
                if (!IsPostBack)
                {                                  
                    int reportId = Convert.ToInt32(Request.QueryString[Invariables.UrlParameters.ReportId]);
                    //Get details about the report user is pulling to set up proper call to report server
                    var reportDetails = theReportViewerService.GetReportInfo(reportId);
                    //
                    // If this report is supposed to generate one or more summary statements
                    //
                    if ((reportId == 0) && (SecurityHelpers.CheckValidPermissionFromSession(TheSessionState, Permissions.SummaryStatement.Manage)))
                    {
                        string zippedPath = GenerateSummaryStatementsAndZip();
                        WriteZipToResponse(zippedPath);
                    }

                    //check that the user is authorized to view report

                    else  if (SecurityHelpers.CheckValidPermissionFromSession(TheSessionState, reportDetails.ReportPermissionName))
                    {
                        //Sets the report path based upon the environment and the report name based upon which report is selected (variable passed through URL)
                        string reportPath = ConfigManager.ReportPath + reportDetails.ReportFileName;

                        // Set the processing mode for the ReportViewer to Remote to read from server
                        reportViewer.ProcessingMode = ProcessingMode.Remote;

                        ServerReport serverReport = reportViewer.ServerReport;

                        ReportParameter rptParamProgram = new ReportParameter();
                        rptParamProgram.Name = "ProgramList";
                        rptParamProgram.Values.Add(Request.QueryString["programList"]);

                        ReportParameter rptParamFy = new ReportParameter();
                        rptParamFy.Name = "FiscalYearList";
                        rptParamFy.Values.Add(Request.QueryString["fiscalYearList"]);

                        ReportParameter rptParamMeetingType = new ReportParameter();
                        rptParamMeetingType.Name = "MeetingTypeList";
                        rptParamMeetingType.Values.Add(Request.QueryString["meetingTypeList"]);

                        ReportParameter rptParamMeeting = new ReportParameter();
                        rptParamMeeting.Name = "MeetingList";
                        rptParamMeeting.Values.Add(Request.QueryString["meetingList"]);

                        ReportParameter rptParamPanel = new ReportParameter();
                        rptParamPanel.Name = "PanelList";
                        rptParamPanel.Values.Add(Request.QueryString["panelList"]);

                        ReportParameter rptParamCycle = new ReportParameter();
                        rptParamCycle.Name = "CycleList";
                        rptParamCycle.Values.Add(Request.QueryString["cycleList"]);

                        // Set the report server URL and report path
                        serverReport.ReportServerUrl = new Uri(ConfigManager.ReportServerUrl);
                        serverReport.ReportPath = reportPath;

                        switch (reportDetails.ReportParameterGroupId)
                        {
                            case (int)ReportParameterGroup.PanelReportGroupId:
                                reportViewer.ServerReport.SetParameters(new ReportParameter[] { rptParamProgram, rptParamFy, rptParamPanel });
                                break;

                            case (int)ReportParameterGroup.CycleReportGroupId:
                                reportViewer.ServerReport.SetParameters(new ReportParameter[] { rptParamProgram, rptParamFy, rptParamCycle });
                                break;

                            case (int)ReportParameterGroup.MeetingReportGroupId:
                                reportViewer.ServerReport.SetParameters(new ReportParameter[] { rptParamFy, rptParamMeetingType, rptParamMeeting });
                                break;

                            default:
                                reportViewer.ServerReport.SetParameters(new ReportParameter[] { rptParamProgram, rptParamFy });
                                break;

                        }
                        errorMessage.Visible = false;
                    }
                    else
                        //Can we use MVC route methods here?
                        Response.Redirect("/ErrorPage/NoAccess");
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                ex.Data.Add("reportId", Convert.ToInt32(Request.QueryString[Invariables.UrlParameters.ReportId]));
                ex.Data.Add("programList", Request.QueryString["programList"]);
                ex.Data.Add("fiscalYearList", Request.QueryString["fiscalYearList"]);
                ex.Data.Add("panelList", Request.QueryString["panelList"]);
                ex.Data.Add("cycleList", Request.QueryString["cycleList"]);
                ex.Data.Add("meetingTypeList", Request.QueryString["meetingTypeList"]);
                ex.Data.Add("meetingList", Request.QueryString["meetingList"]);
                reportViewer.Visible = false;
                errorMessage.Visible = true;
            }
        }
        /// <summary>
        /// Construct the necessary parameters, execute a SQL Server report to generate one or more
        /// summary statements, and zip them.
        /// </summary>
        private string GenerateSummaryStatementsAndZip()
        {
            string[] appWorkflowIds = Request.QueryString[Invariables.UrlParameters.ApplicationWorkflowId].Split(',');
            string[] programAbrvs = Request.QueryString[Invariables.UrlParameters.ProgramAbrv].Split(',');
            string[] years = Request.QueryString[Invariables.UrlParameters.FiscalYear].Split(',');
            string[] cycles = Request.QueryString[Invariables.UrlParameters.Cycle].Split(',');
            string[] logNumbers = Request.QueryString[Invariables.UrlParameters.ApplicationLogNumber].Split(',');
            string[] templateNames = Request.QueryString[Invariables.UrlParameters.TemplateName].Split(',');
            string format = Request.QueryString[Invariables.UrlParameters.Format];
            string extension = format;
            if (string.IsNullOrEmpty((format)))
                format = defaultFormat;
            if (format == defaultFormat)
                extension = DefaultExtension;
            
            CustomIdentity ident = User.Identity as CustomIdentity;
            int userId = ident.UserID;

            // Remove the current user's files at the beginning of each request.
            RemoveStoredFiles(userId);

            List<string> fileNames = new List<string>();
            using (WebClient client = new WebClient())
            {
                for (var i = 0; i < appWorkflowIds.Length; i++)
                {
                    string fileName = GenerateSummaryStatementAndSave(client, appWorkflowIds[i], programAbrvs[i], years[i], cycles[i],
                        logNumbers[i], format, userId, extension, templateNames[i]);
                    fileNames.Add(fileName);
                }
            }
            // Zip files
            string zippedFilePath = ZipFiles(fileNames, userId);
            return zippedFilePath;
        }
        /// <summary>
        /// Write the zipped file to the page response.
        /// </summary>
        /// <param name="zippedFilePath">The absolute path of the zipped file.</param>
        private void WriteZipToResponse(string zippedFilePath)
        {
            string fileName = Path.GetFileName(zippedFilePath);
            Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            Response.ContentType = "application/zip";
            Response.WriteFile(zippedFilePath);
        }
        /// <summary>
        /// Zip files
        /// </summary>
        /// <param name="filePaths">A list of files to be zipped</param>
        /// <param name="userId">The user identifier</param>
        /// <returns></returns>
        private string ZipFiles(List<string> filePaths, int userId)
        {
            string zippedPath = GetStoredZippedFilePath(userId);
            string dirPath = GetStoredDirectoryPath(userId);
            ZipFile.CreateFromDirectory(dirPath, zippedPath, CompressionLevel.Optimal, false);
            return zippedPath;
        } 
        /// <summary>
        /// Execute a SQL Server report to generate one summary statement.
        /// </summary>
        /// <param name="webclient">The web client object</param>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="programAbrv">The program abbreviation</param>
        /// <param name="year">The year</param>
        /// <param name="cycle">The cycle</param>
        /// <param name="logNumber">The log number</param>
        /// <param name="format">The format such as PDF. Optional.</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="extension">The file extension for the report generated</param>
        /// <param name="template">The template to be used for report generation</param>
        /// <returns></returns>
        private string GenerateSummaryStatementAndSave(WebClient webclient, string applicationWorkflowId, string programAbrv, string year, string cycle, string logNumber,
            string format, int userId, string extension, string template)
        {
            string targetUrl = GetTargetUrl(format, applicationWorkflowId, template);
            string filePath = GetStoredFilePath(userId, programAbrv, year, cycle, extension, logNumber);
            if (File.Exists(filePath))
                File.Delete(filePath);
            string dirPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            
            webclient.Credentials = new MyReportServerCredentials().NetworkCredentials;

            webclient.DownloadFile(targetUrl, filePath);
            // Log report file name
            theReportViewerService.LogReportInfo(userId, Convert.ToInt32(applicationWorkflowId));
            return filePath;
        }
        /// <summary>
        /// Get the target URL of report(s).
        /// </summary>
        /// <param name="format">The format such as PDF</param>
        /// <param name="applicationWorkflowId">The application workflow identifier</param>
        /// <param name="template">The report file to use to generate the summary statement</param>
        /// <returns></returns>
        private string GetTargetUrl(string format, string applicationWorkflowId, string template)
        {
            return ConfigManager.ReportServerUrl + string.Format(summaryStatementPageUrl, ConfigManager.ReportPath, template, format, applicationWorkflowId);
        }
        /// <summary>
        /// Remove all the stored files.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        private void RemoveStoredFiles(int userId)
        {
            string rootDir = ConfigManager.ReportStorageRoot + userId.ToString();
            DirectoryInfo di = new DirectoryInfo(rootDir);
            if (di.Exists)
                di.Delete(true);
        }
        /// <summary>
        /// Get the path of the summary statement report file.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="programAbrv">The program abbreviation</param>
        /// <param name="year">The year</param>
        /// <param name="cycle">The cycle</param>
        /// <param name="format">The format such as PDF</param>
        /// <param name="logNumber">The log number</param>
        /// <returns>The absolute path of the report file</returns>
        private string GetStoredFilePath(int userId, string programAbrv, string year, string cycle, string format,
            string logNumber)
        {
            return ConfigManager.ReportStorageRoot +
                   string.Format("{0}\\Source\\{1}\\{2}\\Cycle {3}\\{4}\\{5}.{6}", userId.ToString(), programAbrv, year, cycle,
                       format, logNumber, format.ToLower());
        }
        /// <summary>
        /// Get the path of the zipped summary statement report file.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>The absolute path of the zipped report file</returns>
        private string GetStoredZippedFilePath(int userId)
        {
            return ConfigManager.ReportStorageRoot +
                   string.Format("{0}\\{1:yyyy-MM-dd_hh-mm-ss-tt}.zip", userId.ToString(), GlobalProperties.P2rmisDateTimeNow);
        }
        /// <summary>
        /// Get the directory path of the summary statement report file(s).
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>The absolute path of the directory</returns>
        private string GetStoredDirectoryPath(int userId)
        {
            return ConfigManager.ReportStorageRoot + string.Format("{0}\\Source", userId.ToString());
        }
        #endregion
        [Serializable]
        public sealed class MyReportServerCredentials : IReportServerCredentials
        {
            public WindowsIdentity ImpersonationUser
            {
                get
                {
                    // Use the default Windows user.  Credentials will be
                    // provided by the NetworkCredentials property.
                    return null;
                }
            }

            public ICredentials NetworkCredentials
            {
                get
                {
                    // Read the user information from the Web.config file.  
                    // By reading the information on demand instead of 
                    // storing it, the credentials will not be stored in 
                    // session, reducing the vulnerable surface area to the
                    // Web.config file, which can be secured with an ACL.

                    // User name
                    string userName =
                        ConfigurationManager.AppSettings
                            ["MyReportViewerUser"];

                    if (string.IsNullOrEmpty(userName))
                        throw new Exception(
                            "Missing user name from web.config file");

                    // Password
                    string password =
                        ConfigurationManager.AppSettings
                            ["MyReportViewerPassword"];

                    if (string.IsNullOrEmpty(password))
                        throw new Exception(
                            "Missing password from web.config file");

                    // Domain
                    string domain =
                        ConfigurationManager.AppSettings
                            ["MyReportViewerDomain"];

                    if (string.IsNullOrEmpty(domain))
                        throw new Exception(
                            "Missing domain from web.config file");

                    return new NetworkCredential(userName, password, domain);
                }
            }

            public bool GetFormsCredentials(out Cookie authCookie,
                        out string userName, out string password,
                        out string authority)
            {
                authCookie = null;
                userName = null;
                password = null;
                authority = null;

                // Not using form credentials
                return false;
            }
        }
    }
}