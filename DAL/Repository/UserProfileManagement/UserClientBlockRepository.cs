using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.UserProfileManagement
{
    public interface IUserClientBlockRepository : IGenericRepository<UserClientBlock>
    {
        /// <summary>
        /// Adds the specified user information identifier.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        UserClientBlock Add(int userInfoId, int clientId, int userId);
        /// <summary>
        /// Adds the log.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="blockedClientIds">The blocked client ids.</param>
        /// <param name="unblockedClientIds">The unblocked client ids.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="userId">The user identifier.</param>
        UserBlockLog AddLog(int userInfoId, List<int> blockedClientIds, List<int> unblockedClientIds, string comment, int userId);
        /// <summary>
        /// Deletes the specified user information identifier.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void Delete(int userInfoId, int clientId, int userId);
        /// <summary>
        /// Gets the user client block.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        UserClientBlock GetUserClientBlock(int userInfoId, int clientId);
        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="userInforId">The user infor identifier.</param>
        /// <returns></returns>
        IQueryable<UserClientBlock> GetList(int userInfoId);
        /// <summary>
        /// Gets the logs.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        IQueryable<UserBlockLog> GetLogs(int userInfoId);
    }

    public class UserClientBlockRepository : GenericRepository<UserClientBlock>, IUserClientBlockRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public UserClientBlockRepository(P2RMISNETEntities context) : base(context)
        {
        }
        #endregion               
        /// <summary>
        /// Adds the specified user information identifier.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public UserClientBlock Add(int userInfoId, int clientId, int userId)
        {
            UserClientBlock model = new UserClientBlock();
            model.ClientId = clientId;
            model.UserInfoId = userInfoId;
            Helper.UpdateCreatedFields(model, userId);
            Helper.UpdateModifiedFields(model, userId);
            context.UserClientBlocks.Add(model);

            return model;
        }
        /// <summary>
        /// Adds the log.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="blockedClientIds">The blocked client ids.</param>
        /// <param name="unblockedClientIds">The unblocked client ids.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public UserBlockLog AddLog(int userInfoId, List<int> blockedClientIds, List<int> unblockedClientIds, string comment, int userId)
        {
            UserBlockLog model = new UserBlockLog();
            model.UserInfoId = userInfoId;
            model.EnteredByUserId = userId;
            model.BlockComment = comment;

            for (var i = 0; i < blockedClientIds.Count; i++)
            {
                var logClient = AddLogClient(blockedClientIds[i], true, userId);
                model.UserBlockLogClients.Add(logClient);
            }
            for (var i = 0; i < unblockedClientIds.Count; i++)
            {
                var logClient = AddLogClient(unblockedClientIds[i], false, userId);
                model.UserBlockLogClients.Add(logClient);
            }
            Helper.UpdateCreatedFields(model, userId);
            Helper.UpdateModifiedFields(model, userId);
            context.UserBlockLogs.Add(model);

            return model;
        }
        /// <summary>
        /// Adds the log client.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="blocked">if set to <c>true</c> [blocked].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private UserBlockLogClient AddLogClient(int clientId, bool blocked, int userId)
        {
            UserBlockLogClient model = new UserBlockLogClient();
            model.ClientId = clientId;
            model.BlockFlag = blocked;
            Helper.UpdateCreatedFields(model, userId);
            Helper.UpdateModifiedFields(model, userId);

            context.UserBlockLogClients.Add(model);
            return model;
        }
        /// <summary>
        /// Deletes the specified user information identifier.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userInfoId, int clientId, int userId)
        {
            var model = GetUserClientBlock(userInfoId, clientId);
            Helper.UpdateDeletedFields(model, userId);
            Delete(model);
        }
        /// <summary>
        /// Gets the user client block.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public UserClientBlock GetUserClientBlock(int userInfoId, int clientId)
        {
            var o = context.UserClientBlocks.Where(x => x.UserInfoId == userInfoId && x.ClientId == clientId).FirstOrDefault();
            return o;
        }
        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="userInforId">The user info identifier.</param>
        /// <returns></returns>
        public IQueryable<UserClientBlock> GetList(int userInfoId)
        {
            var list = this.Select().Where(x => x.UserInfoId == userInfoId);
            return list;
        }
        /// <summary>
        /// Gets the logs.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        public IQueryable<UserBlockLog> GetLogs(int userInfoId)
        {
            var list = context.UserBlockLogs.Where(x => x.UserInfoId == userInfoId).OrderByDescending(y => y.CreatedDate);
            return list;
        }
    }
}
