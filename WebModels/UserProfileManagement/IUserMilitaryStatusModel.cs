
namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Class containing the user's military status
    /// </summary>
    public interface IUserMilitaryStatusModel 
    {
        /// <summary>
        /// User Military Status Type identifier
        /// </summary>
        int? MilitaryStatusTypeId { get; set; }
        /// <summary>
        /// User Military Status
        /// </summary>
        string MilitaryStatus { get; set; }
    }
}
