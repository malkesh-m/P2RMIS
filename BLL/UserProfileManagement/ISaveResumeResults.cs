using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Bll.UserProfileManagement
{
    public interface ISaveResumeResults
    {
        /// <summary>
        /// Collection of validation statuses
        /// </summary>
        IList<SaveResumeStatus> Status { get; set; }
        /// <summary>
        /// The resume display model
        /// </summary>
        IResumeModel ResumeModel { get; set; }
    }
}
