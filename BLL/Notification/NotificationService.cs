using Sra.P2rmis.WebModels.Notification;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Notification
{    
    public interface INotificationService
    {
        /// <summary>
        /// Gets the maintenance notifications.
        /// </summary>
        /// <returns></returns>
        IEnumerable<NotificationModel> GetMaintenanceNotifications();
    }
    /// <summary>
    /// Provides notification service
    /// </summary>
    /// <seealso cref="Sra.P2rmis.Bll.ServerBase" />
    /// <seealso cref="Sra.P2rmis.Bll.Notification.INotificationService" />
    public class NotificationService : ServerBase, INotificationService
    {
        public NotificationService()
        {
            UnitOfWork = new UnitOfWork();
        }
        /// <summary>
        /// Gets the maintenance notifications.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NotificationModel> GetMaintenanceNotifications()
        {
            var notifications = this.UnitOfWork.NotificationRepository.GetMaintenanceNotifications();
            var models = from notification in notifications
                     select new NotificationModel
                     {
                         NotificationId = notification.NotificationId,
                         Message = notification.Message,
                         StartDate = notification.StartDate,
                         EndDate = notification.EndDate
                     };
            return models;
        }
    }
}