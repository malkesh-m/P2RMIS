using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.Web.Common.Interfaces;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Reports;

namespace Sra.P2rmis.Web.Common
{
    /// <summary>
    /// Helper class to construct a list of reports into a list of report group.  Each report group
    /// contains a report list for the report group.
    /// </summary>
    public class MenuBuilder
    {

        #region Attributes
        /// <summary>
        /// Repository containing a report group indexed by the report group id.
        /// </summary>
		private Dictionary<int, MenuItem> _menuRepository { get; set;}
        /// <summary>
        /// The list of reports to construct into a menu tree.
        /// </summary>
        private IEnumerable<IReportListModel> _reportList { get; set; }
        /// <summary>
        /// The _session state
        /// </summary>
        private HttpSessionStateBase _sessionState { get; set; }
        #endregion
        #region Constructor & Setup
        /// <summary>
        /// Default constructor.
        /// </summary>
        private MenuBuilder() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="reportList">List of ReportListModels to construct into a menu.</param>
        /// <param name="identity">Users session state</param>
        public MenuBuilder(IEnumerable<IReportListModel> reportList, HttpSessionStateBase sessionState)
        {
            this._sessionState = sessionState;
            this._reportList = reportList;
        }
	    #endregion
        #region The Builder
        /// <summary>
        /// Builds the menu tree
        /// </summary>
        public List<MenuItem> Build()
        {
            //
            // Create the menu repository
            //
            _menuRepository = new Dictionary<int, MenuItem>();
            //
            // if we have a report list then iterate over each entry and create a Report menu item.
            // Group items & menu items are represented by the same class.  
            //
            if ((_reportList != null) && (_sessionState != null))
            {
                foreach (IReportListModel model in _reportList.OrderBy(x => x.ReportGroupSortOrder).ThenBy(x => x.ReportName))
                {
                    MenuItem groupItem = null;
                    //
                    // Check if the user has permission to this report.  If it does not
                    // just cycle to the next one.
                    //
                    if (!SecurityHelpers.CheckValidPermissionFromSession(_sessionState, model.RequiredPermission))
                    {
                        continue;
                    }
                    //
                    // if there is already an entry in the menu repository then we 
                    // retrieve that object and add the report entry to it.
                    //
                    if (_menuRepository.ContainsKey(model.ReportGroupId))
                    {
                        groupItem = _menuRepository[model.ReportGroupId];
                    }
                    else
                    {
                        groupItem = new ReportMenuItem {
                                                        Id = model.ReportGroupId,
                                                        Name = model.ReportGroupName,
                                                        Description = model.ReportGroupDescription,
                                                        SortOrder = model.ReportGroupSortOrder
                                                        };
                        _menuRepository.Add(model.ReportGroupId, groupItem);
                    }
                    //
                    // Now that we have a group make the individual report entry for it and add it to the 
                    // report group list.  By definition the records are retrieved alphabetically from the 
                    // database and presented in that manner.
                    //

                    ReportMenuItem reportItem = new ReportMenuItem {
                                                                Id = model.ReportId,
                                                                Name = model.ReportName,
                                                                Description = model.ReportDescription,
                                                                ReportFileName = model.ReportFileName,
                                                                SortOrder = ReportMenuItem.NO_SORT_ORDER
                                                                };
                    //
                    // Because the list may contain multiple instances of the same report because a report can have
                    // multiple permissions find out if it is there already.  If it is not add it.  Otherwise
                    // skip it because we do not want to add the same entry twice.
                    //
                    if (groupItem.Tree.Find(x => x.Id == reportItem.Id) == null)
                    {
                        groupItem.Tree.Add(reportItem);

                    }
               }
            }
            return GetMenu();
        }
        /// <summary>
        /// Constructs a list of MenuItems representing the report groups.
        /// </summary>
        /// <returns><List of MenuItems/returns>
        private List<MenuItem> GetMenu()
        {
            List<MenuItem> theMenu = new List<MenuItem>();

            foreach (MenuItem item in _menuRepository.Values)
            {
                theMenu.Add(item);
            }
            return theMenu;
        }
        #endregion
   }
}