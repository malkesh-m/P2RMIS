using System;

namespace Sra.P2rmis.WebModels.ReviewerRecruitment
{
    /// <summary>
    /// Web model for the Reviewer Recruitment Work List Grid
    /// </summary>
    public interface IWorkList
    {
        /// <summary>
        /// Reviewer's first name
        /// </summary>
        string FirstName { get; }
        /// <summary>
        /// Reviewer's last name
        /// </summary>
        string LastName { get; }
        /// <summary>
        /// First name of user who modified the reviewer's profile
        /// </summary>
        string ModifedByFirstName { get; }
        /// <summary>
        /// Last name of user who modified the reviewer's profile
        /// </summary>
        string ModifedByLastName { get; }
        /// <summary>
        /// Date the reviewer's profile was modified
        /// </summary>
        DateTime? ModifiedOn { get; }
        /// <summary>
        /// Reviewer's profile type
        /// </summary>
        string RoleName { get;}
        /// <summary>
        /// Reviewer's User entity identifier.
        /// </summary>
        int UserInfoId { get; }
        /// <summary>
        /// Selected Client entity identifier
        /// </summary>
        int ClientId { get;}
        /// <summary>
        /// UserInfoChangeLog entity identifier
        /// </summary>
        int UserInfoChangeLogId { get;}
        /// <summary>
        /// Indicates if this entry should be displayed or is for processing only.
        /// </summary>
        bool Display { get; set; }
    }
    /// <summary>
    /// Web model for the Reviewer Recruitment Work List Grid
    /// </summary>
    public class WorkList : IWorkList
    {
        #region Attributes
        /// <summary>
        /// Reviewer's first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Reviewer's last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// First name of user who modified the reviewer's profile
        /// </summary>
        public string ModifedByFirstName { get; set; }
        /// <summary>
        /// Last name of user who modified the reviewer's profile
        /// </summary>
        public string ModifedByLastName { get; set; }
        /// <summary>
        /// Date the reviewer's profile was modified
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
        /// <summary>
        /// Reviewer's profile type
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// Reviewer's UserInfo entity identifier.
        /// </summary>
        public int UserInfoId { get; set; }
        /// <summary>
        /// Selected Client entity identifier
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// UserInfoChangeLog entity identifier
        /// </summary>
        public int UserInfoChangeLogId { get; set; }
        /// <summary>
        /// Indicates if this entry should be displayed or is for processing only.
        /// </summary>
        public bool Display { get; set; }
        #endregion
    }
}
