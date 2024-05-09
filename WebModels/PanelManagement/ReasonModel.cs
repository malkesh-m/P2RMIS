namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the GetTransferReasons() requests.
    /// </summary>  
    public class ReasonModel: IReasonModel
    {
        /// <summary>
        /// Transfer reason
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// Transfer reason identifier
        /// </summary>
        public int ReasonId { get; set; }
    }
}
