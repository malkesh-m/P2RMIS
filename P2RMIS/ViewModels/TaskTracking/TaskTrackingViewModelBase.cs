using System;

namespace Sra.P2rmis.Web.UI.Models
{
    public class TaskTrackingViewModelBase
    {
        /// <summary>
        /// Gets the name of the requestor.
        /// </summary>
        /// <value>
        /// The name of the requestor.
        /// </value>
        public string RequestorName { get; set; }

        /// <summary>
        /// Gets the requestor email.
        /// </summary>
        /// <value>
        /// The requestor email.
        /// </value>
        public string RequestorEmail { get; set; }

        /// <summary>
        /// Gets the selected client.
        /// </summary>
        /// <value>
        /// The selected client.
        /// </value>
        public string SelectedClient { get; set; }

        /// <summary>
        /// Gets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets the type of the request.
        /// </summary>
        /// <value>
        /// The type of the request.
        /// </value>
        public string RequestType { get; set; }

        /// <summary>
        /// Gets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets the component.
        /// </summary>
        /// <value>
        /// The component.
        /// </value>
        public int? Component { get; set; }

        /// <summary>
        /// Gets or sets the type of the task.
        /// </summary>
        /// <value>
        /// The type of the task.
        /// </value>
        public int? TaskType { get; set; }

        /// <summary>
        /// Gets the request description.
        /// </summary>
        /// <value>
        /// The request description.
        /// </value>
        public string RequestDescription { get; set; }

        /// <summary>
        /// Gets the project justification.
        /// </summary>
        /// <value>
        /// The project justification.
        /// </value>
        public string ProjectJustification { get; set; }

        /// <summary>
        /// Gets the document link.
        /// </summary>
        /// <value>
        /// The document link.
        /// </value>
        public string DocumentLink { get; set; }

        /// <summary>
        /// The name of task request type in Jira
        /// </summary>
        public const string NameOfTaskRequest = "Task";
    }
}