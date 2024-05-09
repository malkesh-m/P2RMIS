namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Web Model class for a single client assignment to a user.
    /// </summary>
    public interface IUserProfileClientModel 
    {
        /// <summary>
        /// User identifier the client is assigned to
        /// </summary>
        int? UserId { get; set; }
        /// <summary>
        /// Entity identifier of the UserClient id.
        /// </summary>
        int? UserClientId { get; set; }
        /// <summary>
        /// Unique identifier of the Client
        /// </summary>
        int ClientId { get; set; }
        /// <summary>
        /// Abbreviation for a specific client
        /// </summary>
         string ClientAbrv { get; set; }
        /// <summary>
        /// The clients full name (ClientDesc field in the Client database table) 
        /// </summary>
        string ClientName { get; set; }
        /// <summary>
        /// Determines if this model is an add.
        /// </summary>
        /// <returns>True if the model represents a delete; false otherwise</returns>
        bool IsAdd();
        /// <summary>
        /// Determines if this model is a delete.
        /// </summary>
        /// <returns>True if the model represents a delete; false otherwise</returns>
        bool IsDelete();
        /// <summary>
        /// Populates a model representing a request to delete the UserClient
        /// entity described.
        /// </summary>
        void PopulateDelete(int userClientId);
        /// <summary>
        /// Indicates if the client is active.
        /// </summary>
        bool IsActive { get; }
    }
}
 