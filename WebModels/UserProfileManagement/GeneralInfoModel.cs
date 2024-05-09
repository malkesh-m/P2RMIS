using System;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Class containing the general user information
    /// </summary>    
    public class GeneralInfoModel : IGeneralInfoModel, IEditable
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public GeneralInfoModel() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstName">User's first name</param>
        /// <param name="mi">User's middle initial</param>
        /// <param name="lastName">User's last name</param>
        /// <param name="nickName">User's nickname</param>
        /// <param name="userLogin">User's login</param>
        /// <param name="badgeName">User's badgename</param>
        /// <param name="prefixId"></param>
        /// <param name="suffix"></param>
        /// <param name="modifiedDate"></param>
        /// <param name="ethnicityId"></param>
        /// <param name="genderId"></param>
        /// <param name="profileTypeId"></param>
        /// <param name="userId">User identifier</param>
        /// <param name="userInfoId">UserInfo identifier</param>
        /// <param name="profileTypeName"></param>
        /// <param name="systemRoleId">User's SystemRole identifier</param>
        /// <param name="userSystemRoleId">UserSystemRole entity identifier</param>
        /// <param name="isClient">Indicates if the user is a client</param>
        public GeneralInfoModel(string firstName, string mi, string lastName, string nickName, string userLogin, string badgeName, 
            int? prefixId, string suffix, DateTime? modifiedDate, int? ethnicityId, int? genderId, int profileTypeId, int userId, int userInfoId, string profileTypeName, 
            int? systemRoleId, int? userSystemRoleId, int? roleOrder, int? academicRankId, bool degreeNotApplicable, string expertise, bool isClient)
        {
            this.FirstName = firstName;
            this.MI = mi;
            this.LastName = lastName;
            this.NickName = nickName;
            this.Username = userLogin;
            this.Badge = badgeName;
            this.PrefixId = prefixId;
            this.Suffix = suffix;
            this.EthinicityId = ethnicityId;
            this.GenderId = genderId;
            this.ModifiedDate = modifiedDate;
            this.ProfileTypeId = profileTypeId;
            this.ProfileTypeTypeName = profileTypeName;
            this.UserId = userId;
            this.UserInfoId = userInfoId;
            this.SystemRoleId = systemRoleId;
            this.UserSystemRoleId = userSystemRoleId;
            this.RoleOrder = roleOrder;
            this.AcademicRankId = academicRankId;
            this.DegreeNotApplicable = degreeNotApplicable;
            this.Expertise = expertise;
            this.IsClient = isClient;
        }
        #endregion
        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// User middle initial
        /// </summary>
        public string MI { get; set; }
        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// User nick name
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// User login name
        /// </summary>        
        public string Username { get; set; }
        /// <summary>
        /// User badge name
        /// </summary>
        public string Badge { get; set; }
        /// <summary>
        /// user prefix
        /// </summary>
        public int? PrefixId { get; set; }
        /// <summary>
        /// user suffix
        /// </summary>
        public string Suffix { get; set; }
        /// <summary>
        /// user gender
        /// </summary>
        public int? GenderId { get; set; }
        /// <summary>
        /// user ethnicity
        /// </summary>
        public int? EthinicityId { get; set; }
        /// <summary>
        /// users profile type id
        /// </summary>
        public int? ProfileTypeId { get; set; }
        /// <summary>
        /// display name for a user's profile type
        /// </summary>
        public string ProfileTypeTypeName { get; set; }
        /// <summary>
        /// users profile last updated date
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// UserInfoId
        /// </summary>
        public int UserInfoId { get; set; }
        /// <summary>
        /// The user's system role
        /// </summary>
        public int? SystemRoleId { get; set; }
        /// <summary>
        /// The UserSystemRole identifier
        /// </summary>
        public int? UserSystemRoleId { get; set; }
        /// <summary>
        /// The role's order
        /// </summary>
        public int? RoleOrder { get; set; }
        /// <summary>
        /// Indicates if the user's W-9 has been verified.
        /// </summary>
        public bool? VerifiedW9 { get; set; }
        /// <summary>
        /// The users academic rank
        /// </summary>
        public int? AcademicRankId { get; set; }
        /// <summary>
        /// Indicates if degress are not applicable for this user
        /// </summary>
        public bool DegreeNotApplicable { get; set; }
        /// <summary>
        /// User's expertise as a comma separated list.
        /// </summary>
        public string Expertise { get; set; }
        /// <summary>
        /// Indicates the user is a Client
        /// </summary>
        public bool IsClient { get; set; }
        #region IsEditable Implementation
        ///<remarks>
        /// The IsEditable implementation was added "after the fact" and is not called from the main service method.  It was
        /// added to support using the Generic Add/Save/Delete for UserSystemRole which overwrites several methods/properties
        /// in the ServiceAction for UserSystemRole entity.
        ///</remarks>
        /// <summary>
        /// Delete the object contained in the model from an object (e.g. associated record from a database table)
        /// </summary>
        public bool IsDeletable {
            get { return false; }
            set {}
        }
        /// <summary>
        /// Delete the object contained in the model from an object (e.g. associated record from a database table) 
        /// </summary>
        /// <returns></returns>
        public bool IsDeleted()
        {
            return IsDeletable;
        }
        /// <summary>
        /// Does the model have data?
        /// </summary>
        /// <returns>True if the model has data; false otherwise</returns>
        public bool HasData()
        {
            return false;
        }
        #endregion
    }
}