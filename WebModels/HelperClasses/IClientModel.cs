

namespace Sra.P2rmis.WebModels.HelperClasses
{
    /// <summary>
    /// Web model interface for the client
    /// </summary>
    public interface IClientModel
    {
       #region Properties
        /// <summary>
        /// Unique identifier of the Client
        /// </summary>
        int ClientId { get; set; }
        /// <summary>
        /// Abbreviation for a specific client
        /// </summary>
        string ClientAbrv { get; set; }
        /// <summary>
        /// The clients full name
        /// </summary>
        string ClientName { get; set; }
        #endregion
    }
}
