using System.Collections.Generic;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.WebModels.Files;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for PI information in panel management
    /// </summary
    public class ApplicationPIViewModel
    {
        #region Constructors
        /// <summary>
        /// Application PI view model
        /// </summary>
        public ApplicationPIViewModel() : base ()
        {            
            //
            // Allocate Application file information so view can display nothing.
            //
            ApplicationDocumentFiles = new List<IFileInfoModel>();
            //
            // Abstract in database
            //
            IsAbstractInDatabase = false;
        }
        /// <summary>
        /// Initializes a new instance of the ApplicationPIViewModel class.
        /// </summary>
        /// <param name="applicationPiInformation">The application PI information.</param>
        public ApplicationPIViewModel(IApplicationPIInformation applicationPiInformation) : this()
        {
            PiInformation = new PiInformationViewModel(applicationPiInformation);
        }
        #endregion

        #region Properties
        /// <summary>
        /// PI information with list of partners for the application
        /// </summary>
        public PiInformationViewModel PiInformation { get; set; }
        /// <summary>
        /// List of Application documents contained in files
        /// </summary>
        public List<IFileInfoModel> ApplicationDocumentFiles { get; set; }
        /// <summary>
        /// Abstract in database
        /// </summary>
        public bool IsAbstractInDatabase { get; set; }
        #endregion
    }
}