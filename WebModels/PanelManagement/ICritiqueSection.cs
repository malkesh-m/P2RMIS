
namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Contains information related to a section of a critique
    /// </summary>
    public interface ICritiqueSection
    {
        /// <summary>
        /// The section's title
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// The section's instruction
        /// </summary>
        string Instructions { get; set; }
        /// <summary>
        /// The section's critique comments
        /// </summary>
        string Text { get; set; }        
        /// <summary>
        /// Flag whether to require critique comments
        /// </summary>
        bool TextFlag { get; set; }
        /// <summary>
        /// Score evaluation for user
        /// </summary>
        decimal? Score { get; set; }
        /// <summary>
        /// Whether the evaluation is scored
        /// </summary>
        bool ScoreFlag { get; set; }
    }
}
