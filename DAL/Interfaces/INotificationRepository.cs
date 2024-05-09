using Sra.P2rmis.Dal.ResultModels;
using System.Linq;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository methods for notifications.
    /// </summary>
    public interface INotificationRepository
    {
        /// <summary>
        /// Gets the maintenance notifications.
        /// </summary>
        /// <returns></returns>
        IQueryable<INotificationModel> GetMaintenanceNotifications();
    }
}
