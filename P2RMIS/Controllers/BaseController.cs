using System;
using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices;
using System.Web.Mvc;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.WebModels.Files;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// Base controller for P2RMIS controller.  Basically a container for controller
    /// common functionality.
    /// </summary>
    public class BaseController : Controller
    {
        #region Attributes
        /// <summary>
        /// Indicates if the controller has been disposed.
        /// </summary>
        protected bool _disposed;
        #endregion
        #region Setup & Disposal
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
                DisposeUnmanagedResources();
                base.Dispose(disposing);
                this._disposed = true;
            }
        }
        /// <summary>
        /// Dispose of unmanaged resources.  Controllers with unmanaged resources should
        /// override this method and dispose any objects there.
        /// </summary>
        public virtual void DisposeUnmanagedResources()
        {
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Returns the user's id from the CustomIdentity
        /// <remarks>
        ///     Method is primarily used to support unit testing.  The scope of 
        ///     'public virtual' visibility is necessary for mocking the the userId.
        /// </remarks>
        /// </summary>
        /// <returns>UserId</returns>
        public virtual int GetUserId()
        {
            CustomIdentity ident = User.Identity as CustomIdentity;
            return ident.UserID;
        }
        /// <summary>
        /// Returns the user's first name and last name
        /// </summary>
        /// <returns>First name + space + Last name</returns>
        public virtual string GetUserName()
        {
            CustomIdentity ident = User.Identity as CustomIdentity;
            return ident.FullUserName;
        }

        /// <summary>
        /// Gets the user login name.
        /// </summary>
        /// <returns></returns>
        public virtual string GetUserLogin()
        {
            CustomIdentity ident = User.Identity as CustomIdentity;
            return ident.Name;
        }
        /// <summary>
        /// Gets the current date.
        /// </summary>
        /// <returns>MM/dd/yyyy format of current date</returns>
        public virtual string GetCurrentDate()
        {
            return GlobalProperties.P2rmisDateTimeNow.ToString("MM/dd/yyyy");
        }
        /// <summary>
        /// Initialize the user's client list. 
        /// 
        /// Note:  The user parameter is tested for null specifically for testing.
        /// 
        /// </summary>
        /// <param name="user">User principal from the controller</param>
        /// <returns>User's client list; empty list if no user supplied</returns>
        protected List<int> GetUsersClientList()
        {
            List<int> result = new List<int>();
            if (Session?[SessionVariables.AuthorizedClientList] != null)
            {
                //gets users current permissions
                result = (List<int>)Session[SessionVariables.AuthorizedClientList];
            }
            return result;
        }
        /// <summary>
        /// Wrapper around the Elmah error function to support unit testing.
        /// </summary>
        /// <param name="exception">Exception thrown</param>
        public virtual void HandleExceptionViaElmah(Exception exception)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
        }
        /// <summary>
        /// Whether the current user has the required permission.
        /// </summary>
        /// <param name="requiredPermission">The required permission for the current action/control</param>
        /// <returns>True if the current user has the required permission. Otherwise return false.</returns>
        /// <remarks>Visibility should have "protected" but because RhinoMocks was used as the testing framework the unit test would not have worked otherwise.</remarks>
        public virtual bool HasPermission(string requiredPermission)
        {
            var hasPermission = false;
            if (string.IsNullOrWhiteSpace(requiredPermission)) 
            {
                // If no permission is required then they will have access to the current action/control.
                hasPermission = true;
            }                
            else
            {
                CustomIdentity ident = User.Identity as CustomIdentity;
                hasPermission = IsValidPermission(requiredPermission);
            }
            return hasPermission;
        }
        /// <summary>
        /// Gets message for reviewer assignment status
        /// </summary>
        /// <param name="status">the ReviewerAssignmentStatus value</param>
        /// <returns>the message if failed. Otherwise return an empty string.</returns>
        public virtual string GetAssignmentMessage(ReviewerAssignmentStatus status)
        {
            string message = string.Empty;
            switch (status)
            {
                case ReviewerAssignmentStatus.PositionOccupied:
                    message = Message.PositionOccupiedMessage;
                    break;
                case ReviewerAssignmentStatus.IncompleteAssignmentData:
                    message = Message.IncompleteAssignmentDataMessage;
                    break;
                case ReviewerAssignmentStatus.ReviewerHasWorkflow:
                    message = Message.ReviewerHasWorkflowMessage;
                    break;
                case ReviewerAssignmentStatus.MissingCoiTypeAndComments:
                    message = Message.MissingCoiTypeAndCommentsMessage;
                    break;
                case ReviewerAssignmentStatus.MissingCoiType:
                    message = Message.MissingCoiTypeMessage;
                    break;
                case ReviewerAssignmentStatus.MissingComments:
                    message = Message.MissingCommentsMessage;
                    break;
            }
            return message;
        }

        /// <summary>
        /// Retrieves values from session
        /// </summary>
        /// <param name="programYearId">Program identifier</param>
        /// <param name="sessionId">Meeting identifier</param>
        /// <param name="panelId">SessionPanel entity identifier</param>
        internal virtual void GetFromSession(ref int? programYearId, ref int? sessionId, ref int? panelId)
        {
            int? thePanelId = (int?)(Session[SessionVariables.PanelId]);

            if (thePanelId.HasValue) 
            {
                programYearId = (int?)Session[SessionVariables.ProgramYearId];
                sessionId = (int?)(Session[SessionVariables.SessionId]);
                panelId = (int?)(Session[SessionVariables.PanelId]);
            }
        }
        #region Services
        /// <summary>
        /// Clients the identifier.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        public virtual int? ClientId(ServerBase server, int sessionPanelId)
        {
            return server.ClientId(sessionPanelId);
        }
        #endregion
        #region SettingSessionVariables
        /// <summary>
        /// Sets the session variables
        /// </summary>
        /// <param name="programId">the program id</param>
        /// <param name="sessionId">session id</param>
        /// <param name="panelId">panel id</param>
        public void SetSessionVariables(int? programId, int? sessionId, int? panelId)
        {
            Session[SessionVariables.ProgramYearId] = programId;
            Session[SessionVariables.SessionId] = sessionId;
            Session[SessionVariables.PanelId] = panelId;
        }

        public void SetSessionVariables(int clientProgramId, int programYearId, int cycle)
        {
            Session[SessionVariables.ClientProgramId] = clientProgramId;
            Session[SessionVariables.ProgramYearId] = programYearId;
            Session[SessionVariables.Cycle] = cycle;
        }
        /// <summary>
        /// Sets the session variables for programId and panelId.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="panelId">The panel identifier.</param>
        public void SetSessionVariables(int? programId, int panelId)
        {
            SetSessionVariable(programId ?? 0, SessionVariables.ProgramYearId);
            SetSessionVariable(panelId, SessionVariables.PanelId);
        }
        /// <summary>
        /// Sets session variables for a program, year, cycle and one or more session panels
        /// </summary>
        /// <param name="clientProgramId">Identifier for a ClientProgram</param>
        /// <param name="programYearId">Identifier for a ProgramYear</param>
        /// <param name="cycle">Identifier for a cycle</param>
        /// <param name="sessionPanelIds">List of SessionPanel identifiers</param>
        public void SetSessionVariables(int clientProgramId, int programYearId, List<int> sessionPanelIds)
        {
            SetSessionVariable(clientProgramId, SessionVariables.ClientProgramId);
            SetSessionVariable(programYearId, SessionVariables.ProgramYearId);
            SetSessionVariable(sessionPanelIds, SessionVariables.PanelIdList);
        }
        /// <summary>
        /// Sets a Session variable of integer type
        /// </summary>
        /// <param name="value">Value to assign session variable</param>
        /// <param name="variableName">System name of the session variable</param>
        public void SetSessionVariable(int value, string variableName)
        {
            Session[variableName] = value;
        }
        /// <summary>
        /// Sets a Session variable of list integer type
        /// </summary>
        /// <param name="value">Value to assign session variable</param>
        /// <param name="variableName">System name of the session variable</param>
        public void SetSessionVariable(List<int> value, string variableName)
        {
            Session[variableName] = value;
        }
        /// <summary>
        /// Determines if the session variable exists
        /// </summary>
        /// <param name="name">The name of the session variable</param>
        /// <returns>True if session variable exists, false otherwise</returns>
        public bool IsSessionVariableNull(string name)
        {
            return Session == null || Session[name] == null;
        }
        /// <summary>
        /// Returns integer session variable values
        /// </summary>
        /// <param name="name">The name of the session variable</param>
        /// <returns>The value for an integer identifier sesssion variable</returns>
        public int GetSessionVariableId(string name)
        {
            return IsSessionVariableNull(name) ? 0 : (int)Session[name];
        }
        /// <summary>
        /// Returns integer session variable values
        /// </summary>
        /// <param name="name">The name of the session variable</param>
        /// <returns>The value for an integer identifier sesssion variable</returns>
        public List<int> GetSessionVariableListIds(string name)
        {
            return IsSessionVariableNull(name) ? new List<int>() : (List<int>)Session[name];
        }

        /// <summary>
        /// Gets the dep path for pdf conversion.
        /// </summary>
        /// <value>
        /// The dep path.
        /// </value>
        protected string DepPath
        {
            get
            {
                var depPath = HttpContext.ApplicationInstance.Server.MapPath("~/bin/Assemblies/Select.Html.dep");
                return depPath;
            }
        }

        /// <summary>
        /// Gets the base URL of the current site.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        protected string BaseUrl
        {
            get { return string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")); }
        }
        /// <summary>
        /// Gets the back button URL.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetBackButtonUrl()
        {
            string referrerPage = (Request.UrlReferrer != null) ? Request.UrlReferrer.AbsoluteUri : string.Empty;
            string currentPage = (Request.Url != null) ? Request.Url.AbsoluteUri : string.Empty;

            if (!referrerPage.Equals(currentPage, StringComparison.OrdinalIgnoreCase))
            {
                Session[SessionVariables.BackButton] = referrerPage;
            }
            return Session[SessionVariables.BackButton].ToString();
        }
        /// <summary>
        /// Sets the active log number.
        /// </summary>
        /// <param name="logNumber">The log number.</param>
        protected void SetActiveLogNumber(string logNumber)
        {
            Session[SessionVariables.ActiveLogNumber] = logNumber;
        }
        /// <summary>
        /// Gets the active log number.
        /// </summary>
        /// <returns></returns>
        protected string GetActiveLogNumber()
        {
            return Session[SessionVariables.ActiveLogNumber]?.ToString();
        }

        /// <summary>
        /// Whether the permission is valid. Point of access for checking permissions from controller
        /// </summary>
        /// <param name="operationNames">The operation names to check against. Can be comma seperated for multiple.</param>
        /// <returns>true if supplied permission is owned; otherwise false</returns>
        protected bool IsValidPermission(string operationNames)
        {
            return SecurityHelpers.CheckValidPermissionFromSession(Session, operationNames);
        }
        #endregion
        /// <summary>
        /// Return Common PDFViewer View
        /// </summary>
        /// <param name="fileUrl">URL for Pdf Viewer</param>
        /// <param name="downloadUrl"> URL for original file download</param>
        /// <returns></returns>
        protected ActionResult PdfViewer(string fileUrl, string downloadUrl)
        {
            var pdfViewerModel = new PdfViewerModel
            {
                FileURL = fileUrl,
                DownloadURL = downloadUrl
            };
            return View("PdfJs", pdfViewerModel);
        }       
        #endregion
    }
}