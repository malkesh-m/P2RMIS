using System.Collections.Generic;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Web.UI.Models
{
    public class SummaryStatementViewModel : SSFilterMenuViewModel
    {
        #region Constructors

        /// <summary>
        /// available applications view model
        /// </summary>
        public SummaryStatementViewModel() : base ()
        {
            //
            // Hide user search criteria
            //
            HideUserSearchCriteria = true;
        }

        #endregion

        #region Properties
        #endregion
    }
}