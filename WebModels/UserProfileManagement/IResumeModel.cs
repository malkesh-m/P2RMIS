namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Resume information
    /// </summary>
    public interface IResumeModel
    {
        /// <summary>
        /// Resume Name as displayed on the screen
        /// </summary>
        string ResumeDisplayName { get; }
        /// <summary>
        /// Resume identifier
        /// </summary>
        int ResumeId { get; }
        /// <summary>
        /// Populates the ResumeModel
        /// </summary>
        /// <param name="resumeDisplayName">Resume Name as displayed on the screen</param>
        /// <param name="resumeId">Resume identifier</param>
        void Populate(string resumeDisplayName, int resumeId);
    }
}
