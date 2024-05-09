using System;
using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices;
using FluentValidation.Attributes;
using FluentValidation;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.WebModels.TaskTracking;
using Sra.P2rmis.WebModels.UserProfileManagement;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Sra.P2rmis.Web.UI.Models
{
    [Validator(typeof(TaskTrackingValidator))]
    public class TaskTrackingViewModel : TaskTrackingViewModelBase
    {
        #region Constructor

        #endregion
        #region Properties
        /// <summary>
        /// Gets the client list.
        /// </summary>
        /// <value>
        /// The client list.
        /// </value>
        public List<string> ClientList => new List<string>(new string[] { "CPRIT", "USAMRMC/CDMRP", "Other" });

        /// <summary>
        /// Gets the request type list.
        /// </summary>
        /// <value>
        /// The request type list.
        /// </value>
        public List<string> RequestTypeList => new List<string>(new string[] { "Task", "Bug", "Improvement", "New Feature" });
        /// <summary>
        /// Gets the component list.
        /// </summary>
        /// <value>
        /// The component list.
        /// </value>
        public IList<Tuple<int, string>> ComponentList { get; internal set; }
        /// <summary>
        /// Gets the task type list.
        /// </summary>
        /// <value>
        /// The task type list.
        /// </value>
        public IList<Tuple<int, string>> TaskTypeList { get; internal set; }
        /// <summary>
        /// Gets the date time now.
        /// </summary>
        /// <value>
        /// The date time now.
        /// </value>
        public string DateTimeNow => ViewHelpers.AddBusinessDays(GlobalProperties.P2rmisDateToday, 2).ToString("d");
        /// <summary>
        /// Gets or sets the file attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        [DataType(DataType.Upload)]
        public IEnumerable<HttpPostedFileBase> Attachments { get; set; }
        #endregion
        #region Validation
        /// <summary>
        /// FluentValidation Validator for TaskTrackingViewModel
        /// </summary>
        public class TaskTrackingValidator : AbstractValidator<TaskTrackingViewModel>
        {
            public TaskTrackingValidator()
            {
                RuleFor(x => x.RequestorName).NotEmpty()
                .WithMessage("Requestor Name is requried.");
                RuleFor(x => x.RequestorEmail).NotEmpty()
                .WithMessage("Requestor Email is required.");
                RuleFor(x => x.Subject).NotEmpty()
                .WithMessage("Subject is required.");
                RuleFor(x => x.RequestDescription).NotEmpty()
                .WithMessage("Request Description is required.");
                RuleFor(x => x.Attachments).SetCollectionValidator(new FileValidator());
            }
        }
        public class FileValidator : AbstractValidator<HttpPostedFileBase>
        {
            public FileValidator()
            {
                RuleFor(x => x.ContentLength)
                    .LessThanOrEqualTo(10000000)
                    .WithMessage("The total size of an attachment exceeded the maximum allowed size (10 MB). Please use the document link for sharing large files.");
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Populates the user information and dropdowns for the ticket submission form.
        /// </summary>
        /// <param name="userInfo">The user information.</param>
        /// <param name="ticketMetadata">The ticket metadata.</param>
        public void PopulateInfoAndDropdowns(IUserInfoSmallModel userInfo, ITaskMetadataModel ticketMetadata)
        {
            RequestorName = $"{userInfo.FirstName} {userInfo.LastName}";
            RequestorEmail = $"{userInfo.PrimaryEmail}";
            ComponentList = ticketMetadata.ComponentList;
            TaskTypeList = ticketMetadata.TaskTypeList;
        }
        #endregion
    }
}