
namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Class containing the user military rank information
    /// </summary>
    public interface IUserMilitaryRankModel 
    {
        /// <summary>
        /// The military rank identifier
        /// </summary>
        int? MilitaryRankId { get; set; }
        /// <summary>
        /// The military rank abbreviation
        /// </summary>
        string RankAbbreviation { get; set; }
        /// <summary>
        /// The military rank name
        /// </summary>
        string RankName { get; set; }
        /// <summary>
        /// The military service branch
        /// </summary>
        string ServiceBranch { get; set; }
    }
}
