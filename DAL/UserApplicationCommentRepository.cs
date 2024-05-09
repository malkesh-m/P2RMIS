using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Database access methods for the UserApplicationComments.
    /// </summary>
    public class UserApplicationCommentRepository : GenericRepository<UserApplicationComment>, IUserApplicationCommentRepository
    {
        #region Construction; Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public UserApplicationCommentRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion
  }
}
