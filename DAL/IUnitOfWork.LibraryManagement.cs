using Sra.P2rmis.Dal.Repository.LibraryManagement;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Identifies the access points to the database entities for training document management.
    /// </summary>
    public partial interface IUnitOfWork
    {
        /// <summary>
        /// Provides database access for the TrainingCategory repository functions.
        /// </summary>
        IGenericRepository<TrainingCategory> TrainingCategoryRepository { get; }
        /// <summary>
        /// Gets the peer review document repository.
        /// </summary>
        /// <value>
        /// The peer review document repository.
        /// </value>
        IPeerReviewDocumentRepository PeerReviewDocumentRepository { get; }

        /// <summary>
        /// Gets the user peer review document repository.
        /// </summary>
        /// <value>
        /// The user peer review document repository.
        /// </value>
        IUserPeerReviewDocumentRepository UserPeerReviewDocumentRepository { get; }
    }
}
