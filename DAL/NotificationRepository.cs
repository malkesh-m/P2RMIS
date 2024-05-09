using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using System.Linq;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Repository methods for notifications
    /// </summary>
    public class NotificationRepository : GenericRepository<NotificationModel>, INotificationRepository
    {
        public const string Maintenance = "Maintenance";

        #region Constructor; Setup and Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public NotificationRepository(P2RMISNETEntities context)
            : base(context)
        {
        }
        #endregion

        #region Repository Methods        
        /// <summary>
        /// Gets the maintenance notifications.
        /// </summary>
        /// <returns></returns>
        public IQueryable<INotificationModel> GetMaintenanceNotifications()
        {
            var result = RepositoryHelpers.GetNotifications(context, Maintenance);
            return result;
        }
        #endregion
    }
}
