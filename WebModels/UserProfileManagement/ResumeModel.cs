using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Resume information
    /// </summary>
    public class ResumeModel : IResumeModel
    {
        #region Construction & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public ResumeModel() 
        {
            StatusMessages = new List<string>();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public ResumeModel(string resumeDisplayName, int resumeId)
        {
            this.ResumeDisplayName = resumeDisplayName;
            this.ResumeId = resumeId;
        }     
        #endregion
        /// <summary>
        /// Resume Name as displayed on the screen
        /// </summary>
        public string ResumeDisplayName { get; private set; }
        /// <summary>
        /// Resume identifier
        /// </summary>
        public int ResumeId { get; private set; }
        #region Helpers
        /// <summary>
        /// Populates the ResumeModel
        /// </summary>
        /// <param name="resumeDisplayName">Resume Name as displayed on the screen</param>
        /// <param name="resumeId">Resume identifier</param>
        public void Populate(string resumeDisplayName, int resumeId)
        {
            this.ResumeDisplayName = resumeDisplayName;
            this.ResumeId = resumeId;
        }
        #endregion
        #region Business layer error messages
        /// <summary>
        /// Status messages from the business layer
        /// </summary>
        public List<string> StatusMessages { get; set; }
        #endregion
    }
}
