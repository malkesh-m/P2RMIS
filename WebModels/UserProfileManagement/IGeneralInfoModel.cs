using System;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Class containing the general user information
    /// </summary>
    public interface IGeneralInfoModel: IEditable
    {
        /// <summary>
        /// User first name
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// User middle initial
        /// </summary>
        string MI { get; set; }
        /// <summary>
        /// User last name
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// User nick name
        /// </summary>
        string NickName { get; set; }
        /// <summary>
        /// User login name
        /// </summary>
        string Username { get; set; }
        /// <summary>
        /// User badge name
        /// </summary>
        string Badge { get; set; }
        /// <summary>
        /// user prefix
        /// </summary>
        int? PrefixId { get; set; }
        /// <summary>
        /// user suffix
        /// </summary>
        string Suffix { get; set; }
        /// <summary>
        /// user gender
        /// </summary>
        int? GenderId { get; set; }
        /// <summary>
        /// user ethnicity
        /// </summary>
        int? EthinicityId { get; set; }
        /// <summary>
        /// users profile type id
        /// </summary>
        int? ProfileTypeId { get; set; }
        /// <summary>
        /// users profile last updated date
        /// </summary>
        DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        int UserId { get; set; }
        /// <summary>
        /// UserInfoId
        /// </summary>
        int UserInfoId { get; set; }
        /// <summary>
        /// display name for a user's profile type
        /// </summary>
        string ProfileTypeTypeName { get; set; }
        /// <summary>
        /// The user's system role
        /// </summary>
        int? SystemRoleId { get; set; }
        /// <summary>
        /// The UserSystemRole identifier
        /// </summary>
        int? UserSystemRoleId { get; set; }
        /// <summary>
        /// The role's order
        /// </summary>
        int? RoleOrder { get; set; }
        /// <summary>
        /// The users academic rank
        /// </summary>
        int? AcademicRankId { get; set; }
        /// <summary>
        /// Indicates if degrees are not applicable for this user
        /// </summary>
        bool DegreeNotApplicable { get; set; }
        /// <summary>
        /// User's expertise as a comma separated list.
        /// </summary>
        string Expertise { get; set; }
        /// <summary>
        /// Indicates the user is a Client
        /// </summary>
        bool IsClient { get; set; }
    }
}
