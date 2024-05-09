using System.Collections.Generic;


namespace Sra.P2rmis.WebModels.ReviewerRecruitment
{
    /// <summary>
    /// Web model for the Reviewer Recruitment Communication Log Modal
    /// </summary>
    public interface IUserCommunicationLogModel
    {
        /// <summary>
        /// The preferred contact information for the recruit
        /// </summary>
        IRecruitPreferredContactInfo RecruitContactInformation { get; set; }
        /// <summary>
        /// The list of the recruit's communication log information
        /// </summary>
        ICollection<IRecruitCommunicationInfo> RecruitCommunicationLog { get; set; }
    }
    /// <summary>
    /// Web model for the Reviewer Recruitment Communication Log Modal
    /// </summary>
    public class UserCommunicationLogModel : IUserCommunicationLogModel
    {
        #region Constructor
        public UserCommunicationLogModel()
        {
            RecruitCommunicationLog = new List<IRecruitCommunicationInfo>();
        }
        #endregion
        #region Attributes
        /// <summary>
        /// The preferred contact information for the recruit
        /// </summary>
        public IRecruitPreferredContactInfo RecruitContactInformation { get; set; }
        /// <summary>
        /// The list of the recruit's communication log information
        /// </summary>
        public ICollection<IRecruitCommunicationInfo> RecruitCommunicationLog{ get; set;}
        #endregion
    }
}
