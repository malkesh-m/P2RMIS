using System.Collections.Generic;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the communications' log
    /// </summary>
    public class CommunicationLogViewModel : PanelManagementViewModel
    {
        #region Constants
        public new const string SubTabController = "/PanelManagement/";
        public new const string SubTab1 = "Compose Email";
        public new const string SubTab2 = "Email Logs";
        public new const string SubTab1Route = "Communication";
        public new const string SubTab2Route = "CommunicationLog";
        public new const string SubTab1Link = SubTabController + SubTab1Route;
        public new const string SubTab2Link = SubTabController + SubTab2Route;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CommunicationLogViewModel()
            : base()
        {
            ///instantiate tab list
            List<TabItem> theTabList = new List<TabItem>();
            ///add items to list
            theTabList.Add(new TabItem() { TabOrder = 1, TabName = SubTab1, TabLink = SubTab1Link });
            theTabList.Add(new TabItem() { TabOrder = 2, TabName = SubTab2, TabLink = SubTab2Link });

            ///set property to the tab list
            this.SubTabs = theTabList;

            Communications = new List<ISessionPanelCommunicationsList>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// The list of emails that were sent relating to this session panel
        /// </summary>
        public List<ISessionPanelCommunicationsList> Communications { get; set; }

        #endregion
    }
}