using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data;
using System.Text;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    public class NotificationsRepository : GenericRepository<NotificationTemplate>
    {

        public NotificationsRepository(P2RMISNETEntities context)
            : base(context)
        {
            
        }

        public List<NotificationTemplate> GetNotificationsPaged(int page, int rows, out int totalCount)
        {
            totalCount = context.NotificationTemplates.Count();
            return context.NotificationTemplates.OrderBy(m => m.NotificationID).Skip(page * rows).Take(rows).ToList();
        }

        public List<NotificationTemplate> GetNotificationTemplates()
        {
            return context.NotificationTemplates.ToList();
        }

        public NotificationTemplate GetNotificationByID(int id)
        {
            return context.NotificationTemplates.Find(id);
        }


        public NotificationTemplate GetNotificationByName(string name)
        {
            return context.NotificationTemplates.Single(a => a.NotificationName == name);
        }

        public void UpdateNotification(NotificationTemplate notifTemplate)
        {
            context.Entry(notifTemplate).State = EntityState.Modified;
            this.Save();
        }

        public void AddNotification(NotificationTemplate notifTemplate)
        {
            context.NotificationTemplates.Add(notifTemplate);
            this.Save();
        }

        public void AddNotificationRecipient(NotificationRecipient tmpRecipient)
        {
            context.NotificationRecipients.Add(tmpRecipient);
            this.Save();
        }

        public void RemoveNotificationRecipient(NotificationRecipient recipnt)
        {
            context.NotificationRecipients.Remove(recipnt);
            this.Save();
        }

        public NotificationRecipient GetRecipientByID(int rcpntId)
        {
            return context.NotificationRecipients.Find(rcpntId);
        }

        public void Save()
        {
            context.SaveChanges();
        }


      
    }
}
