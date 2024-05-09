namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the GetPanelNames() requests.
    /// </summary>   
    public class TransferPanelModel: ITransferPanelModel
    {
        /// <summary>
        /// Panel name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Panel abbreviation
        /// </summary>
        public string Abbreviation { get; set; }
        /// <summary>
        /// Target panel Id.
        /// </summary>
        public int PanelId { get; set; }
    }
}
