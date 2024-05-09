using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's UserAlternateContact object.
    /// </summary>
    public partial class UserAlternateContact : IStandardDateFields
    {
        /// <summary>
        /// Populates a new UserAlternateContact in preparation for addition to the repository.
        /// </summary>
        /// <<param name="userInfoId">the user info identifier</param>
        /// <param name="alternateContactTypeId">The alternate contact type identifier</param>
        /// <param name="emailAddress">the alternate contract email address</param>
        /// <param name="firstName">The alternate contact's first name</param>
        /// <param name="lastName">The alternate contact's last name</param>
        /// <param name="primary">The primary alternate contact flag</param>
        /// <param name="userId">The user identifier of the user making the change</param>
        public void Populate(int userInfoId, int alternateContactTypeId, string emailAddress, string firstName, string lastName, bool primary, int userId)
        {
            this.UserInfoId = userInfoId;
            this.AlternateContactTypeId = alternateContactTypeId;
            this.EmailAddress = emailAddress;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PrimaryFlag = primary;

            Helper.UpdateModifiedFields(this, userId);
            Helper.UpdateCreatedFields(this, userId);
        }
        /// <summary>
        /// Modifies an existing UserAlternateContact in preparation for update to the repository. 
        /// </summary>
        /// <param name="alternateContactTypeId"></param>
        /// <param name="emailAddress"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="primary"></param>
        /// <param name="userId"></param>
        public void Modify(int alternateContactTypeId, string emailAddress, string firstName, string lastName, bool primary, int userId)
        {
            this.AlternateContactTypeId = alternateContactTypeId;
            this.EmailAddress = emailAddress;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PrimaryFlag = primary;

            Helper.UpdateModifiedFields(this, userId);
        }
    }
}
