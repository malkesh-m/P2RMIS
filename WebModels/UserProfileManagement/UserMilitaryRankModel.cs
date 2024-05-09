

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Class containing the user military rank information
    /// </summary>
    public class UserMilitaryRankModel : IUserMilitaryRankModel
    {
        /// <summary>
        /// Signifies there is no service information.
        /// </summary>
        public static readonly string NoServiceBranch = string.Empty;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="MilitaryRankId"></param>
        /// <param name="MilitaryRankAbbreviation"></param>
        /// <param name="MilitaryRankName"></param>
        /// <param name="Service"></param>
        public UserMilitaryRankModel(int? MilitaryRankId, string MilitaryRankAbbreviation, string MilitaryRankName, string Service)
        {
            this.MilitaryRankId = MilitaryRankId;
            this.RankAbbreviation = MilitaryRankAbbreviation;
            this.RankName = MilitaryRankName;
            this.ServiceBranch = Service;
        }
        /// <summary>
        /// Default constructor.  Also used when a user does not have military rank data.
        /// </summary>
        public UserMilitaryRankModel()
        {
            this.MilitaryRankId = null;
            this.RankAbbreviation = string.Empty;
            this.RankName = string.Empty;
            this.ServiceBranch = NoServiceBranch;
        }
        #endregion
        /// <summary>
        /// The military rank identifier
        /// </summary>
        public int? MilitaryRankId { get; set; }
        /// <summary>
        /// The military rank abbreviation
        /// </summary>
        public string RankAbbreviation { get; set; }
        /// <summary>
        /// The military rank name
        /// </summary>
        public string RankName { get; set; }
        /// <summary>
        /// The military service branch
        /// </summary>
        public string ServiceBranch { get; set; }
    }

}
