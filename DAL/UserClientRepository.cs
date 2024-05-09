using System.Collections.Generic;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Database access methods for the UserClient table.
    /// </summary>
    public class UserClientRepository : GenericRepository<UserClient>, IUserClientRepository
    {
        #region Constructor; Setup and Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public UserClientRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion
        #region Repository Services
        /// <summary>
        /// Deletes all UserClient entries in the collection.
        /// </summary>
        /// <param name="collection">UserClient collection</param>
        public void DeleteAll(ICollection<UserClient> collection)
        {
            //
            // If we have something
            //
            if (collection != null)
            {
                //
                // Make a copy of the collection.  Need to do this because
                // once an entry is deleted, it could be removed from the original 
                // collection (if it was passed directly from the UserModel)
                //
                List<UserClient> list = new List<UserClient>(collection);
                foreach (UserClient uc in list)
                {
                    Delete(uc);
                }
            }
        }
        #endregion
    }
}
