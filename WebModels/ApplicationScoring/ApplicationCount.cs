
namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Interface model object returned containing application counts.
    /// </summary>
    public interface IApplicationCount
    {
        /// <summary>
        /// The award abbreviation
        /// </summary>
        string AwardAbbreviation { get; set; }
       /// <summary>
        /// The total number of applications
        /// </summary>
        int TotalApplications { get; set; }
        /// <summary>
        /// The total number of scored applications
        /// </summary>
        int TotalScored { get; set; }
        /// <summary>
        /// The total number of expedited applications
        /// </summary>
        int TotalExpedited { get; set; }
    }
    /// <summary>
    /// Model object returned containing application counts.
    /// </summary>
    public class ApplicationCount : IApplicationCount
    {
        /// <summary>
        /// The award abbreviation
        /// </summary>
        public string AwardAbbreviation { get; set; }
        /// <summary>
        /// The total number of applications
        /// </summary>
        public int TotalApplications { get; set; }
        /// <summary>
        /// The total number of scored applications
        /// </summary>
        public int TotalScored { get; set; }
        /// <summary>
        /// The total number of expedited applications
        /// </summary>
        public int TotalExpedited { get; set; }
    }
}
