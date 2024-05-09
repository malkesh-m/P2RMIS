
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Identifies the access points to the database entities for user panel registration.
    /// </summary>
    public partial interface IUnitOfWork
    {
        /// <summary>
        /// Provides database access for PanelUserRegistrationRepository functions. 
        /// </summary>
        IGenericRepository<PanelUserRegistration> PanelUserRegistrationRepository { get; }
        /// <summary>
        /// Provides database access for PanelUserRegistrationDocument functions. 
        /// </summary>
        IGenericRepository<PanelUserRegistrationDocument> PanelUserRegistrationDocumentRepository { get; }
        /// <summary>
        /// Provides database access for PanelUserRegistrationDocumentItem functions. 
        /// </summary>
        IGenericRepository<PanelUserRegistrationDocumentItem> PanelUserRegistrationDocumentItemRepository { get; }
        /// <summary>
        /// Provides database access for PanelUserRegistrationDocumentContract functions. 
        /// </summary>
        IGenericRepository<PanelUserRegistrationDocumentContract> PanelUserRegistrationDocumentContractRepository { get; }
    }
}
