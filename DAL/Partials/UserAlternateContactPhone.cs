using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    public partial class UserAlternateContactPhone : IStandardDateFields
    {
        /// <summary>
        /// Populates a new UserAlternateContactPhone in preparation for addition to the repository.
        /// </summary>
        /// <param name="international">Internation phone</param>
        /// <param name="extension">The phone's extension</param>
        /// <param name="phoneNumber">The phone number</param>
        /// <param name="phoneTypeId">The phone type identifier</param>
        /// <param name="primary">The primary flag</param>
        /// <param name="userAlternateContactId">The associated user alternate contact identifier</param>
        /// <param name="userId">The user identifier of the user making the change</param>
        public void Populate(bool international, string extension, string phoneNumber, int phoneTypeId, bool primary, int userAlternateContactId, int userId)
        {
            this.International = international;
            this.PhoneExtension = extension;
            this.PhoneNumber = phoneNumber;
            this.PhoneTypeId = phoneTypeId;
            this.PrimaryFlag = primary;
            this.UserAlternateContactId = userAlternateContactId;

            Helper.UpdateModifiedFields(this, userId);
            Helper.UpdateCreatedFields(this, userId);
        }
    }
}
