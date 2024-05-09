

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    public interface IUserInfoSmallModel
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        string LastName { get; set; }

        /// <summary>
        /// Gets or sets the primary email.
        /// </summary>
        /// <value>
        /// The primary email.
        /// </value>
        string PrimaryEmail { get; set; }

        /// <summary>
        /// Populates the model with information.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="primaryEmail">The primary email.</param>
        void Populate(string firstName, string lastName, string primaryEmail);
    }

    /// <summary>
    /// Class containing a small portion of user information.
    /// </summary>
    public class UserInfoSmallModel : IUserInfoSmallModel
    {
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
        /// Gets or sets the primary email.
        /// </summary>
        /// <value>
        /// The primary email.
        /// </value>
        public string PrimaryEmail { get; set; }
        /// <summary>
        /// Populates the model with information.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="primaryEmail">The primary email.</param>
        public void Populate(string firstName, string lastName, string primaryEmail)
        {
            FirstName = firstName;
            LastName = lastName;
            PrimaryEmail = primaryEmail;
        }
    }
}
