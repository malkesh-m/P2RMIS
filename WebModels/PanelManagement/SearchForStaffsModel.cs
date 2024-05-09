namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Web model for search for staffs
    /// </summary>
    public class SearchForStaffsModel : ISearchForStaffsModel
    {
        
        public SearchForStaffsModel(string firstName, string lastName, int? userId,
                string username)
        {
            FirstName = firstName;
            LastName = lastName;
            UserId = userId;
            Username = username;
        }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int? UserId { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }
    }
}
