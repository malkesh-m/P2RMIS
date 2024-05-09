using Sra.P2rmis.CrossCuttingServices;
using System.Linq;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Linq implementation of search result methods
    /// </summary>
    internal partial class RepositoryHelpers
    {
        
        internal static IQueryable<INotificationModel> GetNotifications(P2RMISNETEntities context, string label)
        {
            var result = from notification in context.Notifications
                         where notification.StartDate <= GlobalProperties.P2rmisDateTimeNow &&
                               notification.EndDate > GlobalProperties.P2rmisDateTimeNow && notification.Label == label
                         select new NotificationModel
                         {
                             NotificationId = notification.NotificationId,
                             Message = notification.Message,
                             StartDate = notification.StartDate,
                             EndDate = notification.EndDate,
                             ModifiedDate = notification.ModifiedDate
                         };
            return result;
        }
    }

}
