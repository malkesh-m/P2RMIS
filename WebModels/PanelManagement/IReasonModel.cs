namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the GetTransferReasons() requests.
    /// </summary>  
    public interface IReasonModel
    {
        /// <summary>
        /// Transfer reason
        /// </summary>
        string Reason { get; set; }
        /// <summary>
        /// Transfer reason identifier
        /// </summary>
        int ReasonId { get; set; }
    }
}
