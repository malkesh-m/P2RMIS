
namespace Sra.P2rmis.Bll.PanelManagement
{
    /// <summary>
    /// Individual reviewer's presentation order counts
    /// </summary>
    public class ExperienceCounts
    {
        /// <summary>
        /// Unique identifier for the application
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Applications log number
        /// </summary>
        public string LogNumber { get; set; }
        /// <summary>
        /// Application reviewer count that are rated High in experience
        /// </summary>
        public int HighCount { get; set; }
        /// <summary>
        /// Application reviewer count that are rated medium in experience
        /// </summary>
        public int MediumCount { get; set; }
        /// <summary>
        /// Application reviewer count that are rated low in experience
        /// </summary>
        public int LowCount { get; set; }
    }
}
