namespace Sra.P2rmis.Bll.Views
{
    /// <summary>
    /// BL class representing a CDMRP Application
    /// </summary>
    public class Application
    {
        /// <summary>
        /// ID of the fiscal year and program
        /// </summary>
        public int ProgramID { get; set; }
        /// <summary>
        /// Fiscal year for the application
        /// </summary>
        public string FiscalYear { get; set; }
        /// <summary>
        /// Application Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Application Abbreviation
        /// </summary>
        public string Abbreviation { get; set; }
    }
}
