using System.Collections.Generic;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.ViewModels.PanelManagement
{
    /// <summary>
    /// The view model for database stored abstracts in panel management
    /// </summary
    public class AbstractTextViewModel
    {
        #region Constructors
        /// <summary>
        ///application PI view model
        /// </summary>
        public AbstractTextViewModel()
            : base()
        {
            ApplicationLogNumber = string.Empty;
            //
            // Allocate abstract information so view can display nothing.
            //
            ApplicationAbstractDocument = new List<IApplicationAbstractDocumentModel>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Application Log Number the document list applies to
        /// </summary>
        public string ApplicationLogNumber { get; set; }
        /// <summary>
        /// List of Application Abstract documents contained in the database
        /// </summary>
        public List<IApplicationAbstractDocumentModel> ApplicationAbstractDocument { get; set; }
        #endregion
    }
}