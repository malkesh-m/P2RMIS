
namespace Sra.P2rmis.Bll.UserProfileManagement
{
     /// <summary>
    /// Result for returning the user name
    /// </summary>
    public interface IUserNameResult
    {
        /// <summary>
        /// User First name
        /// </summary>
        string FirstName { get;  }
        /// <summary>
        /// User Last Name
        /// </summary>
        string LastName { get;  }
        /// <summary>
        /// User's full name
        /// </summary>
        string FullName { get; }
        /// <summary>
        /// Populates the model.
        /// </summary>
        /// <param name="firstName">First name value</param>
        /// <param name="lastName">Last name value</param>
        void Populate(string firstName, string lastName);
        /// <summary>
        /// Populates the model.
        /// </summary>
        /// <param name="firstName">First name value</param>
        /// <param name="lastName">Last name value</param>
        /// <param name="fullName">Full name value</param>
        void Populate(string firstName, string lastName, string fullName);
    }

    /// <summary>
    /// Result for returning the user name
    /// </summary>
    public class UserNameResult : IUserNameResult
    {
        #region Attributes
        /// <summary>
        /// User First name
        /// </summary>
        public string FirstName { get; private set; }
        /// <summary>
        /// User Last Name
        /// </summary>
        public string LastName { get; private set; }     
        /// <summary>
        /// User's full name
        /// </summary>
        public string FullName { get; private set; }
        #endregion
        #region Construction & set up
        /// <summary>
        /// Populates the model.
        /// </summary>
        /// <param name="firstName">First name value</param>
        /// <param name="lastName">Last name value</param>
        public void Populate(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        /// <summary>
        /// Populates the model.
        /// </summary>
        /// <param name="firstName">First name value</param>
        /// <param name="lastName">Last name value</param>
        /// <param name="fullName">Full name value</param>
        public void Populate(string firstName, string lastName, string fullName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FullName = fullName;
        }
        #endregion

    }
}
