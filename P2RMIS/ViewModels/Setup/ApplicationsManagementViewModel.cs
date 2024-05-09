using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.ViewModels.Setup
{
    public class ApplicationsManagementViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationsManagementViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ApplicationsManagementViewModel(IApplicationsManagementModel model)
        {
            LogNumber = model.LogNumber;
            PiName = model.PiName;
            Title = model.Title;
            PiOrganization = model.PiOrganization;
            Award = model.Award;
            Panel = model.Panel;
            Withdrawn = model.Withdrawn;
            WithdrawnBy = model.WithdrawnBy;
            WithdrawnDate = ViewHelpers.FormatDate(model.WithdrawnDate);
            ApplicationId = model.ApplicationId;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationsManagementViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="canWithdraw">if set to <c>true</c> [can withdraw].</param>
        public ApplicationsManagementViewModel(IApplicationsManagementModel model, bool canWithdraw)
            : this(model)
        {
            CanWithdraw = canWithdraw;
        }

        /// <summary>
        /// Application log number
        /// </summary>
        public string LogNumber { get; set; }
        /// <summary>
        /// PI name
        /// </summary>
        public string PiName { get; set; }
        /// <summary>
        /// Application title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// PI Organization name
        /// </summary>
        public string PiOrganization { get; set; }
        /// <summary>
        /// Award or mechanism
        /// </summary>
        public string Award { get; set; }
        /// <summary>
        /// Panel name
        /// </summary>
        public string Panel { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ApplicationsManagementViewModel"/> is withdrawn.
        /// </summary>
        /// <value>
        ///   <c>true</c> if withdrawn; otherwise, <c>false</c>.
        /// </value>
        public bool Withdrawn { get; set; }
        /// <summary>
        /// withdraw by
        /// </summary>
        public int? WithdrawnBy { get; set; }
        /// <summary>
        /// withdraw date
        /// </summary>
        public string WithdrawnDate { get; set; }
        /// <summary>
        /// application id
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// panel application id
        /// </summary>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// cycle
        /// </summary>
        public int? ReceiptCycle { get; set; }
        /// <summary>
        /// award
        /// </summary>
        public int AwardId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can withdraw.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can withdraw; otherwise, <c>false</c>.
        /// </value>
        public bool CanWithdraw { get; set; }
    }
}