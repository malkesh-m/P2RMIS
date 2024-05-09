using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal.Repository.LibraryManagement
{
    public interface IUserPeerReviewDocumentRepository : IGenericRepository<UserPeerReviewDocument>
    {
        /// <summary>
        /// Adds the specified program year identifier.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="peerReviewDocumentId">The peer review document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void Add(int programYearId, int peerReviewDocumentId, int userId);
    }

    public class UserPeerReviewDocumentRepository : GenericRepository<UserPeerReviewDocument>, IUserPeerReviewDocumentRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public UserPeerReviewDocumentRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion
        #region Services
        /// <summary>
        /// Adds the specified program year identifier.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="peerReviewDocumentId">The peer review document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void Add (int programYearId, int peerReviewDocumentId, int userId)
        {
            UserPeerReviewDocument model = new UserPeerReviewDocument();
            model.PeerReviewDocumentId = peerReviewDocumentId;
            model.ReviewDate = GlobalProperties.P2rmisDateTimeNow;
            model.ProgramYearId = programYearId;
            model.UserId = userId;

            Helper.UpdateCreatedFields(model, userId);
            Helper.UpdateModifiedFields(model, userId);
            context.UserPeerReviewDocuments.Add(model);
        }
        #endregion
    }
}
