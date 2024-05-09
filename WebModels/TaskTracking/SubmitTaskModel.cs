using System;

namespace Sra.P2rmis.WebModels.TaskTracking
{
    /// <summary>
    /// Interface for a SubmitTaskModel
    /// </summary>
    public interface ISubmitTaskModel
    {
        /// <summary>
        /// Gets the name of the requestor.
        /// </summary>
        /// <value>
        /// The name of the requestor.
        /// </value>
        string RequestorName { get; set; }

        /// <summary>
        /// Gets the requestor email.
        /// </summary>
        /// <value>
        /// The requestor email.
        /// </value>
        string RequestorEmail { get; set; }

        /// <summary>
        /// Gets the selected client.
        /// </summary>
        /// <value>
        /// The selected client.
        /// </value>
        string SelectedClient { get; set; }

        /// <summary>
        /// Gets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        string Subject { get; set; }

        /// <summary>
        /// Gets the type of the request.
        /// </summary>
        /// <value>
        /// The type of the request.
        /// </value>
        string RequestType { get; set; }

        /// <summary>
        /// Gets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        DateTime DueDate { get; set; }

        /// <summary>
        /// Gets the component.
        /// </summary>
        /// <value>
        /// The component.
        /// </value>
        int? Component { get; set; }

        /// <summary>
        /// Gets the request description.
        /// </summary>
        /// <value>
        /// The request description.
        /// </value>
        string RequestDescription { get; set; }

        /// <summary>
        /// Gets the project justification.
        /// </summary>
        /// <value>
        /// The project justification.
        /// </value>
        string ProjectJustification { get; set; }

        /// <summary>
        /// Gets the document link.
        /// </summary>
        /// <value>
        /// The document link.
        /// </value>
        string DocumentLink { get; set; }
        /// <summary>
        /// Gets the task type id.
        /// </summary>
        /// <value>
        /// The task type id.
        /// </value>
        int? TaskTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        /// <value>
        /// The name of the component.
        /// </value>
        string ComponentName { get; set; }

        /// <summary>
        /// Gets or sets the name of the task type.
        /// </summary>
        /// <value>
        /// The name of the task type.
        /// </value>
        string TaskTypeName { get; set; }
        /// <summary>
        /// Gets or sets the name of the assignee.
        /// </summary>
        /// <value>
        /// The name of the assignee.
        /// </value>
        string AssigneeName { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        string Status { get; set; }
    }

    /// <summary>
    /// Model containing information related to a task request
    /// </summary>
    /// <seealso cref="Sra.P2rmis.WebModels.TaskTracking.ISubmitTaskModel" />
    public class SubmitTaskModel : ISubmitTaskModel
    {
        public SubmitTaskModel(string requestorName, string requestorEmail, string selectedClient, string subject, string requestType, DateTime dueDate, int? component, int? taskTypeId, string requestDescription, string projectJustification, string documentLink, string componentName, string taskTypeName, string assigneeName, string status)
        {
            RequestorName = requestorName;
            RequestorEmail = requestorEmail;
            SelectedClient = selectedClient;
            Subject = subject;
            RequestType = requestType;
            DueDate = dueDate;
            Component = component;
            RequestDescription = requestDescription;
            ProjectJustification = projectJustification;
            DocumentLink = documentLink;
            TaskTypeId = taskTypeId;
            ComponentName = componentName;
            TaskTypeName = taskTypeName;
            AssigneeName = assigneeName;
            Status = status;
        }

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
        /// Gets the task type id.
        /// </summary>
        /// <value>
        /// The task type id.
        /// </value>
        public int? TaskTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        /// <value>
        /// The name of the component.
        /// </value>
        public string ComponentName { get; set; }
        /// <summary>
        /// Gets or sets the name of the task type.
        /// </summary>
        /// <value>
        /// The name of the task type.
        /// </value>
        public string TaskTypeName { get; set; }
        /// <summary>
        /// Gets or sets the name of the assignee.
        /// </summary>
        /// <value>
        /// The name of the assignee.
        /// </value>
        public string AssigneeName { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }
    }
}
