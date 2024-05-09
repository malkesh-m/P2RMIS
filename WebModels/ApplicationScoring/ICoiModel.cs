
namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Returns the COI information for scoring
    /// </summary>
    public interface ICoiModel
    {
        /// <summary>
        /// COI First Name
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// COI Last Name
        /// </summary>
        string LastName { get; set; }
    }
}
