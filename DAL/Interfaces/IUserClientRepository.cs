using System.Collections.Generic;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Database access methods for the UserClient table.
    /// </summary>
    public interface IUserClientRepository: IGenericRepository<UserClient>
    {
        /// <summary>
        /// Deletes all UserClient entries in the collection.
        /// </summary>
        /// <param name="collection">UserClient collection</param>
        void DeleteAll(ICollection<UserClient> collection);
    }
}
