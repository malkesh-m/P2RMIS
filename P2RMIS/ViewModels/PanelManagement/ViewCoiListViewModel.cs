using System.Collections.Generic;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for viewing COI list.
    /// </summary>
    public class ViewCoiListViewModel
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ViewCoiListViewModel()
            : base()
        {
            this.Personnels = new List<IPersonnelWithCoi>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// List of personnels with COI
        /// </summary>
        public List<IPersonnelWithCoi> Personnels { get; set; }
        #endregion
    }
}