namespace Sra.P2rmis.Web.UI.Models
{
    public class SearchUserViewModel
    {

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public SearchUserViewModel()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// the first name string searched
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// the last name string searched
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// the email searched
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// the username searched
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// the user id searched
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// the vendor id searched
        /// </summary>
        public string VendorId { get; set; }

        /// <summary>
        /// whether the user is searching to create a user or searching to update a user
        /// </summary>
        public bool IsUpdateUserSearch { get; set; }


        #endregion
        #region Helpers
        #endregion
    }
}