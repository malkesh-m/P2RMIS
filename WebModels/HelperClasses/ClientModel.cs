
namespace Sra.P2rmis.WebModels.HelperClasses
{
    public class ClientModel : IClientModel
    {
        #region Properties
        /// <summary>
        /// Unique identifier of the Client
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// Abbreviation for a specific client
        /// </summary>
        public string ClientAbrv { get; set; }
        /// <summary>
        /// The clients full name (ClientDesc field in the Client database table) 
        /// </summary>
        public string ClientName { get; set; }

        #endregion
    }
}
