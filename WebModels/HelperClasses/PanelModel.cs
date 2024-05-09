namespace Sra.P2rmis.WebModels.HelperClasses
{
    public class PanelModel
    {
        /// <summary>
        /// Panel unique identifier
        /// </summary>
        public int PanelId { get; set; }
        /// <summary>
        /// panel abbreviation
        /// </summary>
        public string PanelAbbrv { get; set; }
        /// <summary>
        /// Panel fiscal year
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        public string ProgramAbbrv { get; set; }
    }
}
