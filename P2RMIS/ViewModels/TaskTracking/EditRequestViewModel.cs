using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.TaskTracking;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using FluentValidation;

namespace Sra.P2rmis.Web.UI.Models
{
    [Validator(typeof(EditRequestValidator))]
    public class EditRequestViewModel : TaskTrackingViewModelBase
    {
        #region Properties
        /// <summary>
        /// Gets the ticket identifier.
        /// </summary>
        /// <value>
        /// The ticket identifier.
        /// </value>
        public string TicketId { get; set; }
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets the name of the component.
        /// </summary>
        /// <value>
        /// The name of the component.
        /// </value>
        public string ComponentName { get; set; }

        /// <summary>
        /// Gets the name of the task type.
        /// </summary>
        /// <value>
        /// The name of the task type.
        /// </value>
        public string TaskTypeName { get; set; }
        /// <summary>
        /// Gets or sets the file attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        [DataType(DataType.Upload)]
        public IEnumerable<HttpPostedFileBase> Attachments { get; set; }
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }
        /// <summary>
        /// Gets the assignee.
        /// </summary>
        /// <value>
        /// The assignee.
        /// </value>
        public string Assignee { get; set; }
        /// <summary>
        /// Gets a value indicating whether to display task type information or component.
        /// </summary>
        /// <value>
        /// <c>true</c> if request is a task; otherwise, false.
        /// </value>
        public bool DoDisplayTaskTypeInfo => RequestType == NameOfTaskRequest;
        /// <summary>
        /// Gets the due date formatted.
        /// </summary>
        /// <value>
        /// The due date formatted.
        /// </value>
        public string DueDateFormatted => DueDate.ToShortDateString();

        /// <summary>
        /// Gets the request description formatted.
        /// </summary>
        /// <value>
        /// The request description formatted for Html display.
        /// </value>
        public IHtmlString RequestDescriptionFormatted => new HtmlString(RequestDescription.Replace("\\", "<br />"));

        /// <summary>
        /// Gets a value indicating whether this editing is allowed for this ticket.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance editing is allowed; otherwise, <c>false</c>.
        /// </value>
        public bool IsEditingAllowed => !Status.Contains("Closed");
        /// <summary>
        /// Populates the view model.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <param name="domainModel">The domain model.</param>
        public void PopulateViewModel(string ticketId, ISubmitTaskModel domainModel)
        {
            this.TicketId = ticketId;
            this.RequestorName = domainModel.RequestorName;
            this.ComponentName = domainModel.ComponentName;
            this.DocumentLink = domainModel.DocumentLink;
            this.DueDate = domainModel.DueDate;
            this.ProjectJustification = domainModel.ProjectJustification;
            this.RequestDescription = domainModel.RequestDescription;
            this.RequestType = domainModel.RequestType;
            this.RequestorEmail = domainModel.RequestorEmail;
            this.SelectedClient = domainModel.SelectedClient;
            this.Subject = domainModel.Subject;
            this.TaskTypeName = domainModel.TaskTypeName;
            this.Assignee = domainModel.AssigneeName;
            this.Status = domainModel.Status;
        }
        #endregion
        #region Validation
        public class EditRequestValidator : AbstractValidator<EditRequestViewModel>
        {
            public EditRequestValidator()
            {
                RuleFor(x => x.Attachments)
                .SetCollectionValidator(new FileValidator());
            }

        }
        public class FileValidator : AbstractValidator<HttpPostedFileBase>
        {
            public FileValidator()
            {
                RuleFor(x => x.ContentLength)
                    .LessThanOrEqualTo(10000000)
                    .WithMessage("The total size of an attachment exceeded the maximum allowed size (10 MB). Please use a project drive for sharing large files.");
            }
        }
        #endregion
    }
}