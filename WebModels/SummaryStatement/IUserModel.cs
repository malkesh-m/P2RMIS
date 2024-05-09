namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing a user's basic information
    /// </summary>
    public interface IUserModel
    {
        /// <summary>
        /// Unique identifier for a user
        /// </summary>
        int UserId { get; set; }

        /// <summary>
        /// First name of the user 
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// User name formatted for presentation
        /// </summary>
        string UserFullNameFormatted { get; }

    }
}