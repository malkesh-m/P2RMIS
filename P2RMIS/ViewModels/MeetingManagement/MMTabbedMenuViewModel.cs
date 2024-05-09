using System.Collections.Generic;

namespace Sra.P2rmis.Web.UI.Models
{    
    /// <summary>
    /// The view model for the tabbed list on the meeting management pages.
    /// </summary>
    public class MMTabbedMenuViewModel : MMSubTabbedMenuViewModel
    {       
        #region Properties
        /// <summary>
        /// the list of tabs in meeting management.
        /// </summary>
        public List<MMTabItem> Tabs { get; set; }
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int? PanelUserAssignmentId {get;set;}
        /// <summary>
        /// Gets or sets the session user assignment identifier.
        /// </summary>
        /// <value>
        /// The session user assignment identifier.
        /// </value>
        public int? SessionUserAssignmentId { get;set;}
        #endregion
    }

    public class MMTabItem 
    {
        /// <summary>
        /// the order of the tabs
        /// </summary>
        public int TabOrder { get; set; }
        /// <summary>
        /// the name of the tab
        /// </summary>
        public string TabName { get; set; }
        /// <summary>
        /// the action link for the tab
        /// </summary>
        public string TabLink { get; set; }
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int? PanelUserAssignmentId {get;set;}
        /// <summary>
        /// Gets or sets the session user assignment identifier.
        /// </summary>
        /// <value>
        /// The session user assignment identifier.
        /// </value>
        public int? SessionUserAssignmentId { get;set;}
    }
}