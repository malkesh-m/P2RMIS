using System.Linq;

namespace Sra.P2rmis.Dal.Repository.UserProfileManagement
{
    /// <summary>
    /// Repository for UserAccountStatusChangeLog objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public class UserAccountStatusChangeLogRepository : GenericRepository<UserAccountStatusChangeLog>, IUserAccountStatusChangeLogRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public UserAccountStatusChangeLogRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
		#region Services provided
        /// <summary>
        /// Returns the last change log entry for the specified user.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Last change log entry for the specified user; null if none exists</returns>
        public UserAccountStatusChangeLog GetLastUnlock(int userId)
        {
            return GetLast(userId, AccountStatusReason.Indexes.Locked);
        }
        /// <summary>
        /// Returns the last change log entry for the specified user where the account was locked.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Last change log entry for the specified user; null if none exists</returns>
        public UserAccountStatusChangeLog GetLastLock(int userId)
        {
            return GetLastNew(userId, AccountStatusReason.Indexes.Locked);
        }
        /// <summary>
        /// Returns the last change log entry for the specified user changing FROM the specified condition
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="condition">Account Status Reason</param>
        /// <returns>Last change log entry for the specified user with the specified condition; null if none exists</returns>
        internal UserAccountStatusChangeLog GetLast(int userId, int condition)
        {
            return context.UserAccountStatusChangeLogs.Where(x => (x.UserId == userId) && (x.OldAccountStatusReasonId == condition)).OrderByDescending(x => x.UserAccountStatusChangeLogId).FirstOrDefault();
        }
        /// <summary>
        /// Returns the last change log entry for the specified user changing TO the specified condition
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="condition">Account Status Reason</param>
        /// <returns>Last change log entry for the specified user with the specified condition; null if none exists</returns>
        internal UserAccountStatusChangeLog GetLastNew(int userId, int condition)
        {
            return context.UserAccountStatusChangeLogs.Where(x => (x.UserId == userId) && (x.NewAccountStatusReasonId == condition)).OrderByDescending(x => x.UserAccountStatusChangeLogId).FirstOrDefault();
        }
        #endregion
    }

}
