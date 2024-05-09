using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// Delegate for criteria formatter
    /// </summary>
    /// <param name="firstName">First name searched</param>
    /// <param name="lastName">Last name searched</param>
    /// <param name="email">Email searched</param>
    /// <param name="userName">Username searched</param>
    /// <param name="userId">UserId searched</param>
    /// <returns>List of criteria string</returns>
    public delegate List<string> CriteriaFormatter(string firstName, string lastName, string email, string userName, int? userId);

    /// <summary>
    /// Search results view model
    /// </summary>
    public class SearchResultsViewModel : UserProfileManagementViewModel
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public SearchResultsViewModel(string firstName, string lastName, string email, string userName, int? userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            UserId = userId;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The first name string searched
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The last name string searched
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The email searched
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The username searched
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// The user id searched
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// List of users
        /// </summary>
        public List<IFoundUserModel> Users { get; set; }
        /// <summary>
        /// Criteria formatter
        /// </summary>
        public static CriteriaFormatter CriteriaFormatter { get; set; }
        /// <summary>
        /// Criteria
        /// </summary>
        public List<string> Criteria
        {
            get
            {
                return CriteriaFormatter(FirstName, LastName, Email, UserName, UserId);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Formatted criteria
        /// </summary>
        /// <returns></returns>
        public string FormattedCriteria
        {
            get
            {
                return string.Join(", ", Criteria);
            }
        }
        /// <summary>
        /// Whether there are users returned
        /// </summary>
        public bool HasNoUsers
        {
            get
            {
                return (this.Users.Count() == 0);
            }
        }
        /// <summary>
        /// Format the search criteria
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="email">Email</param>
        /// <param name="userName">UserName</param>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        internal static List<string> FormatSearchCriteria(string firstName, string lastName, string email, string userName, int? userId)
        {
            var criteria = new List<string>();
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                criteria.Add(string.Format("{0}: {1}", Invariables.Labels.UserProfileManagement.FirstName, firstName));
            }
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                criteria.Add(string.Format("{0}: {1}", Invariables.Labels.UserProfileManagement.LastName, lastName));
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                criteria.Add(string.Format("{0}: {1}", Invariables.Labels.UserProfileManagement.Email, email));
            }
            if (!string.IsNullOrWhiteSpace(userName))
            {
                criteria.Add(string.Format("{0}: {1}", Invariables.Labels.UserProfileManagement.UserName, userName));
            }
            if (userId > 0)
            {
                criteria.Add(string.Format("{0}: {1}", Invariables.Labels.UserProfileManagement.UserId, userId));
            }
            return criteria;
        }
        #endregion
    }
}