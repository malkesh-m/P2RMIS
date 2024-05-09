namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Contains information related to a section of a critique
    /// </summary>
    public class CritiqueSection : ICritiqueSection
    {
        /// <summary>
        /// The section's title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The section's instruction
        /// </summary>
        public string Instructions { get; set; }
        /// <summary>
        /// The section's critique comments
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Flag whether to require critique comments
        /// </summary>
        public bool TextFlag { get; set; }
        /// <summary>
        /// Score evaluation for criteria
        /// </summary>
        public decimal? Score { get; set; }
        /// <summary>
        /// Whether the evaluation is scored
        /// </summary>
        public bool ScoreFlag { get; set; }
    }
}
