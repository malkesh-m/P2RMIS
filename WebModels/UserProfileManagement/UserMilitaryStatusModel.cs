

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    public class UserMilitaryStatusModel : IUserMilitaryStatusModel
    /// <summary>
    /// Class containing the user's military status
    /// </summary>
    {
        /// <summary>
        /// User Military Status Type identifier
        /// </summary>
        public int? MilitaryStatusTypeId { get; set; }
        /// <summary>
        /// User Military Status
        /// </summary>
        public string MilitaryStatus { get; set; }
    }

}
