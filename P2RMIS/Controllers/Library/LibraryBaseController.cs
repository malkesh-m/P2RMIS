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
    public class LibraryBaseController : BaseController
    {
        #region Properties
        /// <summary>
        /// Service providing access to File services.
        /// </summary>
        protected IFileService theFileService { get; set; }
        /// <summary>
        /// Service providing access to Program Registration services.
        /// </summary>
        protected IProgramRegistrationService theProgramRegistrationService { get; set; }
        /// <summary>
        /// Gets or sets the panel management list service.
        /// </summary>
        /// <value>
        /// The panel management list service.
        /// </value>
        protected IPanelManagementService thePanelManagementService { get; set; }
        /// <summary>
        /// Gets or sets the user profile management service.
        /// </summary>
        /// <value>
        /// The user profile management service.
        /// </value>
        protected IUserProfileManagementService theUserProfileManagementService { get; set; }
        /// <summary>
        /// Gets or sets the library service.
        /// </summary>
        protected ILibraryService theLibraryService { get; set; }
        #endregion
        #region Constants
        public class Constants
        {
            public const string MarkAsReviewedInstructions = "(Check the box to indicate that the item has been reviewed.)";
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Sets the program identifier in the session.
        /// </summary>
        /// <param name="panelId">the program id to set in the session</param>
        public virtual void SetLibraryProgramSession(int programId)
        {
            Session[Invariables.SessionKey.LibraryProgramSession] = programId;
        }
        /// <summary>
        /// returns the program identifier stored in the session.
        /// </summary>
        /// <returns>the program identifier</returns>
        public virtual int GetLibraryProgramSession()
        {
            int programId = Convert.ToInt32(Session[Invariables.SessionKey.LibraryProgramSession]);
            return programId;
        }
        #endregion
    }
}