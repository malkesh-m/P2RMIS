using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;
using Newtonsoft.Json;
using Sra.P2rmis.Web.UI.Models;

namespace Sra.P2rmis.Web.ViewModels.PanelManagement
{
    /// <summary>
    /// View model for the SearchForStaffs view.
    /// </summary>SearchForStaffsViewModel
    public class SearchForStaffsViewModel : PanelManagementViewModel
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        public SearchForStaffsViewModel() { }

        #endregion
        /// <summary>
        /// Gets the staff results.
        /// </summary>
        /// <value>
        /// The staff results.
        /// </value>
        public List<SearchForStaffViewModel> StaffResults { get; private set; }

        #region Search parameters
        /// <summary>
        /// Gets the person key dropdown.
        /// </summary>
        /// <value>
        /// The person key dropdown.
        /// </value>
        public List<KeyValuePair<string, string>> PersonKeyDropdown
        {
            get
            {
                var dd = new List<KeyValuePair<string, string>>();
                dd.Add(new KeyValuePair<string, string>(Invariables.PersonKey.Name, Invariables.PersonKey.Name));
                dd.Add(new KeyValuePair<string, string>(Invariables.PersonKey.LastName, Invariables.PersonKey.LastName));
                dd.Add(new KeyValuePair<string, string>(Invariables.PersonKey.FirstName, Invariables.PersonKey.FirstName));
                dd.Add(new KeyValuePair<string, string>(Invariables.PersonKey.UserId, Invariables.PersonKey.UserId));
                dd.Add(new KeyValuePair<string, string>(Invariables.PersonKey.Username, Invariables.PersonKey.Username));

                return dd;
            }
        }
        /// <summary>
        /// Gets or sets the person key such as last name, first name, user id or username.
        /// </summary>
        /// <value>
        /// The person key such as last name, first name, user id or username.
        /// </value>
        public string PersonKey { get; set; }
        /// <summary>
        /// Gets or sets the person's last name, first name, user id or username.
        /// </summary>
        /// <value>
        /// The person's last name, first name, user id or username.
        /// </value>
        public string PersonValue { get; set; }
        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        public string Organization { get; set; }
        #endregion
        #region Services        
        /// <summary>
        /// Gets the search for staffs model.
        /// </summary>
        /// <returns></returns>
        public SearchForStaffsModel GetSearchForStaffsModel()
        {
            string firstName = (PersonKey == Invariables.PersonKey.FirstName) ? PersonValue : string.Empty;
            string lastName = (PersonKey == Invariables.PersonKey.LastName) ? PersonValue : string.Empty;
            string userIdStr = (PersonKey == Invariables.PersonKey.UserId) ? PersonValue : string.Empty;
            string username = (PersonKey == Invariables.PersonKey.Username) ? PersonValue : string.Empty;
            string name = (PersonKey == Invariables.PersonKey.Name) ? PersonValue : string.Empty;

            int? userId = null;
            if (!string.IsNullOrWhiteSpace(userIdStr))
            {
                int tmp;
                int.TryParse(userIdStr.Trim(), out tmp);
                userId = tmp;
            }

            if (!string.IsNullOrEmpty(name))
            {
                var nameArray = name.Split(',');
                lastName = nameArray[0].Trim();
                if (nameArray.Length > 1)
                {
                    firstName = nameArray[1].Trim();
                }
            }

            var searchForStaffs = new SearchForStaffsModel(firstName, lastName, userId, username);
            return searchForStaffs;
        }
        /// <summary>
        /// Sets the staff results.
        /// </summary>
        /// <param name="staffSearchResultModels">The staff search result models.</param>
        public void SetStaffResults(List<IStaffSearchResultModel> staffSearchResultModels)
        {
            StaffResults = staffSearchResultModels.ConvertAll(x => new SearchForStaffViewModel(x)).ToList();
        }
        /// <summary>
        /// Gets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        public string FiscalYear
        {
            get { return this.Panels.FirstOrDefault(x => x.PanelId == this.SelectedPanel)?.FY; }
        }
        /// <summary>
        /// Gets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        public string ProgramAbbreviation
        {
            get { return this.Panels.FirstOrDefault(x => x.PanelId == this.SelectedPanel)?.ProgramAbbreviation; }
        }
        /// <summary>
        /// Gets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        public string PanelAbbreviation
        {
            get { return this.Panels.FirstOrDefault(x => x.PanelId == this.SelectedPanel)?.PanelAbbreviation; }
        }
        #endregion
    }
}