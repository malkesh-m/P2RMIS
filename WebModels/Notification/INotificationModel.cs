using System;

namespace Sra.P2rmis.WebModels.Notification
{
    /// <summary>
    /// Model representing notification such as maintenance notification
    /// </summary>
    public interface INotificationModel
    {
        /// <summary>
        /// Gets or sets the notification identifier.
        /// </summary>
        /// <value>
        /// The notification identifier.
        /// </value>
        int NotificationId { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; set; }
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        DateTime StartDate { get; set; }
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        DateTime EndDate { get; set; }
    }
}
