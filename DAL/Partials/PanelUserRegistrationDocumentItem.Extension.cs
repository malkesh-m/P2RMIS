using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PanelUserRegistrationDocumentItem object. 
    /// </summary>
    public partial class PanelUserRegistrationDocumentItem: IStandardDateFields
    {
        /// <summary>
        /// Populate the entity 
        /// </summary>
        /// <param name="registrationDocumentId">The key's value</param>
        /// <param name="value">The key value</param>
        public void Populate(int panelUserRegistrationDocumentId, int registrationDocumentId, string value)
        {
            this.PanelUserRegistrationDocumentId = panelUserRegistrationDocumentId;
            this.RegistrationDocumentItemId = registrationDocumentId;
            this.Value = value;
        }
    }
}
