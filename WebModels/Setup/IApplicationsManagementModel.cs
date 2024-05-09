using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.Setup
{
    public interface IApplicationsManagementModel
    {
        /// <summary>
        /// Application log number
        /// </summary>
        string LogNumber { get; set; }
        /// <summary>
        /// PI name
        /// </summary>
        string PiName { get; set; }
        /// <summary>
        /// Application title
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// PI Organization name
        /// </summary>
        string PiOrganization { get; set; }
        /// <summary>
        /// Award or mechanism
        /// </summary>
        string Award { get; set; }
        /// <summary>
        /// Panel name
        /// </summary>
        string Panel { get; set; }
        /// <summary>
        /// withdrawn status
        /// </summary>
        bool Withdrawn { get; set; }
        /// <summary>
        /// withdrawn action
        /// </summary>
        string Action { get; set; }
        /// <summary>
        /// withdraw by
        /// </summary>
        int? WithdrawnBy { get; set; }
        /// <summary>
        /// withdraw date
        /// </summary>
        DateTime? WithdrawnDate { get; set; }
        /// <summary>
        /// application id
        /// </summary>
        int ApplicationId { get; set; }
        /// <summary>
        /// client id
        /// </summary>
        int ClientId { get; set; }
    }
}
