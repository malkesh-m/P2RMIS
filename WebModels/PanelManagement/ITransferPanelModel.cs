namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the GetPanelNames() requests.
    /// </summary>  
    public interface ITransferPanelModel
    {
        /// <summary>
        /// Panel name
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Panel abbreviation
        /// </summary>
        string Abbreviation { get; set; }
        /// <summary>
        /// Target panel Id.
        /// </summary>
        int PanelId { get; set; }
    }
}
