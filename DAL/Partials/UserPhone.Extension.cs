using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's UserPhone object.
    /// </summary>
    public partial class UserPhone : IStandardDateFields
    {
        /// <summary>
        /// Populates a new UserPhone in preparation for addition to the repository.
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <param name="phoneNumber">The phone number</param>
        /// <param name="extension">The extension number of the phone</param>
        /// <param name="phoneTypeId">The type of phone</param>
        /// <param name="primary">Primary phone flag</param>
        /// <param name="international">International phone</param>
        /// <param name="userId">The user identifier of the user making the change</param>
        public void Populate(int userInfoId, string phoneNumber, string extension, int? phoneTypeId, bool primary, bool international, int userId)
        {
            this.UserInfoID = userInfoId;
            this.Phone = phoneNumber;
            this.Extension = extension;
            this.PhoneTypeId = phoneTypeId;
            this.International = international;
            this.PrimaryFlag = primary;

            Helper.UpdateModifiedFields(this, userId);
            Helper.UpdateCreatedFields(this, userId);
        }

    }
}
