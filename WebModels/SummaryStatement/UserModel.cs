
namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Signature of delegate used to format a user's name.
    /// </summary>
    /// <param name="firstName">First name of user</param>
    /// <param name="lastName">Last name of user</param>
    /// <returns>Number formatted as string in standard way</returns>
    public delegate string FullNameByCommaFormatter(string firstName, string lastName);
    /// <summary>
    /// Class representing a user's basic information
    /// </summary>
    public class UserModel : IUserModel
    {
        /// <summary>
        /// Unique identifier for a user
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// First name of the user 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the user
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// delegate used to format a user's name
        /// </summary>
        public static FullNameByCommaFormatter NameFormatter { get; set; }
        /// <summary>
        /// User name formatted for presentation
        /// </summary>
        public string UserFullNameFormatted
        {
            get { return (NameFormatter != null) ? NameFormatter(FirstName, LastName) : string.Empty; }
        }
    }
}
