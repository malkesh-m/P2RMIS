using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Bll.UserProfileManagement
{
    /// <summary>
    /// Return status results & description of a saved resume.
    /// </summary>
    public class SaveResumeResults : ISaveResumeResults
    {
        #region Constructor & Set up
        /// <summary>
        /// Constructor
        /// </summary>
        public SaveResumeResults()
        {
            this.Status = new List<SaveResumeStatus>();
            this.ResumeModel = new ResumeModel();
        }
        #endregion
        /// <summary>
        /// Collection of validation statuses
        /// </summary>
        public IList<SaveResumeStatus> Status { get; set; }
        /// <summary>
        /// The resume display model
        /// </summary>
        public IResumeModel ResumeModel { get; set; }
    }
}
