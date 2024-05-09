namespace Sra.P2rmis.WebModels.HelperClasses
{
    /// <summary>
    /// the model representing and award 
    /// </summary>
    public interface IAwardModel
    {
        #region Properties
        /// <summary>
        /// Gets or sets the award type identifier.
        /// </summary>
        /// <value>
        /// The award type identifier.
        /// </value>
        int AwardTypeId { get; set; }
        /// <summary>
        /// the awards abbreviation
        /// </summary>
        string AwardAbbreviation { get; set; }
        /// <summary>
        /// the awards full name
        /// </summary>
        string AwardDescription { get; set; }

        /// <summary>
        /// Gets or sets the legacy award type identifier.
        /// </summary>
        /// <value>
        /// The legacy award type identifier.
        /// </value>
        string LegacyAwardTypeId { get; set; }
        /// <summary>
        /// Receipt cycle
        /// </summary>
        int? ReceiptCycle { get; set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Year
        /// </summary>
        string Year { get; set; }
        #endregion
    }
}
