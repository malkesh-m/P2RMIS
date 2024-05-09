namespace Sra.P2rmis.WebModels.ProgramRegistration
{
    /// <summary>
    /// Results of the user confirming (signing) a document.
    /// </summary>
    public interface IConfirmedModel
    {
        /// <summary>
        /// The PanelUserRegistrationiDocumentId entity signed
        /// </summary>
        int PanelUserRegistrationDocumentId { get;  }
        /// <summary>
        /// full user name of document signer
        /// </summary>
        string Name { get;  }
        /// <summary>
        /// Date document was signed
        /// </summary>
        string Date { get;  }
        /// <summary>
        /// Indicates if the registration is not complete
        /// </summary>
        bool IsRegistrationNotComplete { get; set; }
    }
    /// <summary>
    /// Results of the user confirming (signing) a document.
    /// </summary>
    public class ConfirmedModel: IConfirmedModel
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="panelUserRegistrationiDocumentId">PanelUserRegistrationDocument entity identifier</param>
        /// <param name="name">Formatted name of the user who signed the document</param>
        /// <param name="date">Date the document was signed</param>
        public ConfirmedModel(int panelUserRegistrationiDocumentId, string name, string date)
        {
            this.PanelUserRegistrationDocumentId = panelUserRegistrationiDocumentId;
            this.Name = name;
            this.Date = date;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// The PanelUserRegistrationiDocumentId entity signed
        /// </summary>
        public int PanelUserRegistrationDocumentId { get; private set; }
        /// <summary>
        /// full user name of document signer
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Date document was signed
        /// </summary>
        public string Date { get; private set; }
        /// <summary>
        /// Indicates if the registration is not complete
        /// </summary>
        public bool IsRegistrationNotComplete { get; set; }
        #endregion
    }
}
