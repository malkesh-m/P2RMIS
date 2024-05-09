using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's UserInfoChangeLog object. 
    /// </summary>
    public partial class UserInfoChangeLog : IStandardDateFields
    {
        /// <summary>
        /// Populates a UserInfoChangeLog when the UserInfoChangeLog has been reviewed.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        public void Populate(int userId)
        {
            this.ReviewedFlag = true;
            this.ReviewedDate = GlobalProperties.P2rmisDateTimeNow;
            this.ReviewedBy = userId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="entityFieldName"></param>
        /// <param name="entityTableName"></param>
        /// <param name="identifer"></param>
        public void Populate(string oldValue, string newValue, int identifer, int userInfoEntityIdentifier, int userInfoChangeTypeId)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
            this.Identifier = identifer;
            this.UserInfoId = userInfoEntityIdentifier;
            this.UserInfoChangeTypeId = userInfoChangeTypeId;
        }

    }
}
