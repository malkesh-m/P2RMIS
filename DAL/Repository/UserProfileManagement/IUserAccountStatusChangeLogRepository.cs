
namespace Sra.P2rmis.Dal.Repository.UserProfileManagement
{
    /// <summary>
    /// Repository for UserAccountStatusChangeLog objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public interface IUserAccountStatusChangeLogRepository : IGenericRepository<UserAccountStatusChangeLog>
    {
        /// <summary>
        /// Returns the last change log entry for the specified user.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Last change log entry for the specified user; null if none exists</returns>
        UserAccountStatusChangeLog GetLastUnlock(int userId);
        /// <summary>
        /// Returns the last change log entry for the specified user where the account is locked.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Last change log entry for the specified user; null if none exists</returns>
        UserAccountStatusChangeLog GetLastLock(int userId);
    }
}
