namespace Sra.P2rmis.WebModels.HelperClasses
{
    /// <summary>
    /// the model representing and award 
    /// </summary>
    public class AwardModel : IAwardModel
    {
        #region Properties
        /// <summary>
        /// Gets or sets the award type identifier.
        /// </summary>
        /// <value>
        /// The award type identifier.
        /// </value>
        public int AwardTypeId { get; set; }
        /// <summary>
        /// the awards abbreviation
        /// </summary>
        public string AwardAbbreviation { get; set; }
        /// <summary>
        /// the awards full name
        /// </summary>
        public string AwardDescription { get; set; }
        /// <summary>
        /// Gets or sets the legacy award type identifier.
        /// </summary>
        /// <value>
        /// The legacy award type identifier.
        /// </value>
        public string LegacyAwardTypeId { get; set; }
        /// <summary>
        /// Receipt cycle
        /// </summary>
        public int? ReceiptCycle { get; set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Year
        /// </summary>
        public string Year { get; set; }
        #endregion
    }
}
