using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's _PUT_OBJECT_NAME_HERE object. 
    /// </summary>
	
    public partial class UserSystemRole: IStandardDateFields
    {
        /// <summary>
        /// Populate the entity fields.
        /// </summary>
        /// <param name="systemRoleId">SystemRole entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        public void Populate(int systemRoleId, int userId)
        {
            this.SystemRoleId = systemRoleId;
            this.UserID = userId;
        }
    }
}
