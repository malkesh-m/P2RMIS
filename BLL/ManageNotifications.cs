using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sra.P2rmis.Dal;


namespace Sra.P2rmis.Bll
{
    public class ManageNotifications
    {
        private UnitOfWork unitOfWork;

        public ManageNotifications()
        {
            unitOfWork = new UnitOfWork();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public IEnumerable<NotificationTemplate> GetNotificationsPaged(int page, int rows, out int totalCount)
        {
            return unitOfWork.UofwNotificationRepository.GetNotificationsPaged(page, rows, out totalCount);
        }

        public void AddNotificationTemplate(NotificationTemplate tmplateNotif)
        {
            this.unitOfWork.UofwNotificationRepository.AddNotification(tmplateNotif);
        }

        public IEnumerable<NotificationTemplate> GetNotificationTemplates()
        {
            return unitOfWork.UofwNotificationRepository.GetNotificationTemplates();
        }

        public NotificationTemplate GetNotificationByName(String name)
        {
            List<NotificationTemplate> retList = unitOfWork.UofwNotificationRepository.GetNotificationTemplates();

            NotificationTemplate notifName = retList.Find
            (
                delegate(NotificationTemplate notif)
                {
                    return notif.NotificationName == name;
                }
            );
            return notifName;
        }

        public void AddNotificationRecipient(NotificationRecipient tmpRecipient)
        {
            unitOfWork.UofwNotificationRepository.AddNotificationRecipient(tmpRecipient);
        }

        public void UpdateNotificationTemplate(NotificationTemplate notifTemplate)
        {
            unitOfWork.UofwNotificationRepository.UpdateNotification(notifTemplate);
        }

        public NotificationTemplate GetNotifById(int notificationId)
        {
            return unitOfWork.UofwNotificationRepository.GetNotificationByID(notificationId);
        }

        public NotificationRecipient GetRecipentById(int rcpntId)
        {
            return unitOfWork.UofwNotificationRepository.GetRecipientByID(rcpntId);
        }

        public void RemoveNotificationRecipient(NotificationRecipient t)
        {
            unitOfWork.UofwNotificationRepository.RemoveNotificationRecipient(t);
        }

        public IEnumerable<int> GetRecipientsForNotification(int currentPage, int rows, out int totalRecords, int notificationId)
        {
            NotificationTemplate notif = GetNotifById(notificationId);

            totalRecords = notif.NotificationRecipients.Count();

            return notif.NotificationRecipients.Select(a => a.NotificationRecipentID);
        }

        public int GetUsrIdFromRecipientID(int notifRecipntId)
        {
            NotificationRecipient rcpnt = GetRecipentById(notifRecipntId);
            return rcpnt.UserID;
        }
    }
}
