namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// web model for the reviewer evaluation rating guidance 
    /// </summary>
    public class RatingGuidance : IRatingGuidance
    {
        /// <summary>
        /// the rating group id the ratings and description belongs to
        /// </summary>
        public int RatingGroupId { get; set; }
        /// <summary>
        /// the rating group name the ratings and description belongs to
        /// </summary>
        public string RatingGroupName { get; set; }
        /// <summary>
        /// the numerical rating
        /// </summary>
        public int Rating { get; set; }
        /// <summary>
        /// the description that fits the rating
        /// </summary>
        public string RatingDescription { get; set; }
        
    }
}
