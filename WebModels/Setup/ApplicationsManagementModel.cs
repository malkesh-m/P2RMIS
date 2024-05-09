using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.Setup
{
    public class ApplicationsManagementModel : IApplicationsManagementModel
    {
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
        /// withdrawn status
        /// </summary>
        public bool Withdrawn { get; set; }
        /// <summary>
        /// withdrawn action
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// withdraw by
        /// </summary>
        public int? WithdrawnBy { get; set; }
        /// <summary>
        /// withdraw date
        /// </summary>
        public DateTime? WithdrawnDate { get; set; }
        /// <summary>
        /// application id
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Session Panel identifier
        /// </summary>
        public int SessionPanelId { get; set; }
        /// <summary>
        /// cycle
        /// </summary>
        public int? ReceiptCycle { get; set; }
        /// <summary>
        /// award
        /// </summary>
        public int AwardId { get; set; }
        /// <summary>
        /// client id
        /// </summary>
        public int ClientId { get; set; }
    }
}
