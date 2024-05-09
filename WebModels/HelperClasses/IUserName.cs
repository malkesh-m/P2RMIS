namespace Sra.P2rmis.WebModels.HelperClasses
{
    public interface IUserName
    {
        /// <summary>
        /// last name
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// middle name
        /// </summary>
        string MiddleName { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Prefix name
        /// </summary>
        string Prefix { get; set; }
    }
}
