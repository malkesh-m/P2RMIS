using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.ReviewerRecruitment
{
    /// <summary>
    /// Web Model for the recruits contact informion for the communication log modal
    /// </summary>
    public interface IRecruitPreferredContactInfo
    {
        /// <summary>
        /// The recruit's user identifier
        /// </summary>
        int UserId { get; set; }
        /// <summary>
        /// The recruit's last identifier
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// The recruit's first name
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// The recruit's phone
        /// </summary>
        string Phone { get; set; }
        /// <summary>
        /// The recruit's phone extension
        /// </summary>
        string PhoneExtension { get; set; }
        /// <summary>
        /// The recruit's fax
        /// </summary>
        string Fax { get; set; }
        /// <summary>
        /// The recruit's email address
        /// </summary>
        string Email { get; set; }
     }
    /// <summary>
    /// Web Model for the recruits contact informion for the communication log modal
    /// </summary>
    public class RecruitPreferredContactInfo : IRecruitPreferredContactInfo
    {
        #region Constructor
        public RecruitPreferredContactInfo(int userId, string lastName, string firstName, string phone, string extension, string fax, string email)
        {
            this.UserId = userId;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.Phone = phone;
            this.PhoneExtension = extension ?? string.Empty;
            this.Fax = fax ?? string.Empty;
            this.Email = email;
        }
        public RecruitPreferredContactInfo(int userId)
        {
            this.UserId = userId;
        }
        #endregion
        /// <summary>
        /// The recruit's user identifier
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// The recruit's first name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The recruit's first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The recruit's phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// The recruit's phone extension
        /// </summary>
        public string PhoneExtension { get; set; }
        /// <summary>
        /// The recruit's fax
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// The recruit's email address
        /// </summary>
        public string Email { get; set; }
    }
}
