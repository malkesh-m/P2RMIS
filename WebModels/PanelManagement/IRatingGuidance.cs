namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// web model interface for the reviewer evaluation rating guidance 
    /// </summary>
    public interface IRatingGuidance
    {
        /// <summary>
        /// the group the rating and rating description belongs to
        /// </summary>
        int RatingGroupId { get; set; }
        /// <summary>
        /// the rating group name the ratings and description belongs to
        /// </summary>
        string RatingGroupName { get; set; }
        /// <summary>
        /// the numerical rating
        /// </summary>
        int Rating { get; set; }
        /// <summary>
        /// the description that fits the rating
        /// </summary>
        string RatingDescription { get; set; }
        
    }
}
