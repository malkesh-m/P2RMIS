namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Identifies the access points to the database entities for mail crud operations on Mail entity objects
    /// </summary>
    public partial interface IUnitOfWork
    {
        /// <summary>
        /// Provides database access for the CommunicationLog repository functions.
        /// </summary>
        IGenericRepository<CommunicationLog> CommunicationLogRepository { get; }
        /// <summary>
        /// Provides database access for the CommunicationLogRecipient repository functions.
        /// </summary>
        IGenericRepository<CommunicationLogRecipient> CommunicationLogRecipientRepository { get; }
        /// <summary>
        /// Provides database access for the CommunicationLogAttachment repository functions.
        /// </summary>
        IGenericRepository<CommunicationLogAttachment> CommunicationLogAttachmentRepository { get; }
    }
}
