using System.Collections.Generic;
using Sra.P2rmis.WebModels.Notification;
using System;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the Dashboard page
    /// </summary>
    public class NotificationMessageViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the NotificationMessageViewModel class.
        /// </summary>
        public NotificationMessageViewModel()
        {
        }
        /// <summary>
        /// </summary>

        public NotificationMessageViewModel(INotificationModel model)
        {
            NotificationId = model.NotificationId;
            Message = model.Message;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the notification identifier.
        /// </summary>
        /// <value>
        /// The notification identifier.
        /// </value>
        public int NotificationId { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public DateTime ModifiedDate { get; set; }
        #endregion

        #region Helpers
        internal void SetMaintenanceMessage()
        {
            if (StartDate.Day == EndDate.Day && StartDate.Month == EndDate.Month && StartDate.Year == EndDate.Year) {
                //TODO: if startdate and enddate are same date but different time, then only display the time portion of the end date.

                //string colon = ":";
                //string startDateString = StartDate.ToString();
                //string startDateHour = StartDate.Hour.ToString();
                //StartDate = (DateTime).
            }
            Message = String.Format(Message, StartDate.ToString(), EndDate.ToString());
        }
        #endregion 

    }
}