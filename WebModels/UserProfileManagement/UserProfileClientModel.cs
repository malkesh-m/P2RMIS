namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Web Model class for a single client assignment to a user
    /// </summary>
    public class UserProfileClientModel : IUserProfileClientModel
    {
        #region Constructor
        public UserProfileClientModel() : base() { }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="userId">the user identifier</param>
        /// <param name="userClientId">the user client identifier</param>
        /// <param name="clientId">The client identifier</param>
        /// <param name="clientAbrv">The client abbreviation</param>
        /// <param name="clientDesc">The client full name</param>
        /// <param name="isDeleteAble">Is this record delete-able</param>
        /// <param name="isActive">Indicates if the client is active</param>
        public UserProfileClientModel(int? userId, int? userClientId, int clientId, string clientAbrv, string clientDesc, bool isDeleteAble, bool isActive)
        {
            this.UserId = userId;
            this.UserClientId = userClientId;
            this.ClientId = clientId;
            this.ClientAbrv = clientAbrv;
            this.ClientName = clientDesc;
            this.IsDeleteAble = isDeleteAble;
            this.IsActive = isActive;
        }
        #endregion
        /// <summary>
        /// User identifier the client is assigned to
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// Entity identifier of the UserClient id.
        /// </summary>
        public int? UserClientId { get; set; }
        /// <summary>
        /// Unique identifier of the Client
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// Abbreviation for a specific client
        /// </summary>
        public string ClientAbrv { get; set; }
        /// <summary>
        /// The clients full name (ClientDesc field in the Client database table) 
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// Indicates if the model represents a delete.  This is 
        /// set/used by the BL layer.
        /// </summary>
        private bool IsDeleteAble { get; set; }
        /// <summary>
        /// Indicates if the client is active.
        /// </summary>
        public bool IsActive { get; private set; }
        /// <summary>
        /// Determines if this model is an add.
        /// </summary>
        /// <returns>True if the model represents a delete; false otherwise</returns>
        public bool IsAdd()
        {
            return ((UserClientId == null) || (UserClientId.Value == 0));
        }
        /// <summary>
        /// Determines if this model is a delete.
        /// </summary>
        /// <returns>True if the model represents a delete; false otherwise</returns>
        public bool IsDelete()
        {
            return IsDeleteAble;
        }
        /// <summary>
        /// Populates a model representing a request to delete the UserClient
        /// entity described.
        /// </summary>
        public void PopulateDelete(int userClientId)
        {
            this.IsDeleteAble = true;
            this.UserClientId = userClientId;
        }
        //public bool IsClientBlocked()
        //{
        //    return ((UserClientId == null) || (UserClientId.Value == 0));
        //}
    }
}
