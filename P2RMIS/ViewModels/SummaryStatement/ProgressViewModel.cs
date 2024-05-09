using System.Collections.Generic;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ProgressViewModel : SSFilterMenuViewModel
    {
        #region Constructors

        /// <summary>
        /// Available progress applications view model
        /// </summary>
        public ProgressViewModel() : base ()
        {
            //
            // Allocate a dictionary so view can display nothing.
            //
            SummaryGroup = new List<ISummaryGroup>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// the summary groups on the intial page load
        /// </summary>
        public List<ISummaryGroup> SummaryGroup { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is web based.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is web based; otherwise, <c>false</c>.
        /// </value>
        public bool IsWebBased { get; set; }
        #endregion
    }
}