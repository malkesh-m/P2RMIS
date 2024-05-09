using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.Web.ViewModels.Shared;
namespace Sra.P2rmis.Web.UI.Models
{
    public class TransferDataViewModel
    {
        #region Constructor
        public TransferDataViewModel()
        {
            TabModel = new DataTransferTabsViewModel();
            FilterModel = new DataTransferFilterMenuViewModel();
        }
        #endregion
        #region Properties
         /// <summary>
        /// Gets or sets the tab model.
        /// </summary>
        /// <value>
        /// The tab model.
        /// </value>
        public DataTransferTabsViewModel TabModel { get; set; }

        /// <summary>
        /// Gets or sets the filter model.
        /// </summary>
        /// <value>
        /// The filter model.
        /// </value>
        public DataTransferFilterMenuViewModel FilterModel { get; set; }
        #endregion
    }
}