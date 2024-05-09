using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Repository;

namespace Sra.P2rmis.Bll.EntityMetadata
{
    /// <summary>
    /// Gets metadata related to the UserInfo entity
    /// </summary>
    public class UserInfoMetadata
    {
        /// <summary>
        /// Provides the max length of the last name field
        /// </summary>
        public static int GetLastNameLength()
        {
            return EntityMetadataRepository.GetMaxLength<UserInfo>(x => x.LastName) ?? 0; 
        }
        /// <summary>
        /// Provides the max length of the first name field
        /// </summary>
        public static int GetFirstNameLength()
        {
            return EntityMetadataRepository.GetMaxLength<UserInfo>(x => x.FirstName) ?? 0;
        }
        /// <summary>
        /// Provides the max length of the Institution Field
        /// </summary>
        public static int GetInstitutionNameLength()
        {
            return EntityMetadataRepository.GetMaxLength<UserInfo>(x => x.Institution) ?? 0;
        }
        /// <summary>
        /// Provides the max length of the Department Field
        /// </summary>
        public static int GetDepartmentNameLength()
        {
            return EntityMetadataRepository.GetMaxLength<UserInfo>(x => x.Position) ?? 0;
        }
        /// <summary>
        /// Provides the max length of the Position Field
        /// </summary>
        public static int GetPositionNameLength()
        {
            return EntityMetadataRepository.GetMaxLength<UserInfo>(x => x.Position) ?? 0;
        }
    }
}
