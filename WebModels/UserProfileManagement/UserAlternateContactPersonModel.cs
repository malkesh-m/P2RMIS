using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Phone Number Information
    /// </summary>
    public class UserAlternateContactPersonModel : Editable, IUserAlternateContactPersonModel
    {
        /// <summary>
        /// Minimum entries
        /// </summary>
        public const int MinimumEntries = 1;
        /// <summary>
        /// Initialize model
        /// </summary>
        /// <param name="model">the UserAlternateContactPersonModel</param>
        public static void InitializeModel(UserAlternateContactPersonModel model)
        {
            if (model.AlternateContactPhone == null)
            {
                model.AlternateContactPhone = new List<UserAlternateContactPersonPhoneModel>();
            }
        }
        /// <summary>
        /// Indicates if the position is deleted
        /// </summary>
        /// <returns>Reurns true if the item is to be deleted, false otherwise</returns>
        public override bool IsDeleted()
        {
            return (UserAlternateContactId > 0 && (IsDeletable || !HasData()));
        }
        /// <summary>
        /// User alternate contact identifier
        /// </summary>
        public int UserAlternateContactId { get; set; }
        /// <summary>
        /// User alternate contact type identifier
        /// </summary>
        public int? UserAlternateContactTypeId { get; set; }
        /// <summary>
        /// Alternate contact first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// 
        /// Alternate contact last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// collection of the alternate contact's phone numbers
        /// </summary>
        public List<UserAlternateContactPersonPhoneModel> AlternateContactPhone { get; set; }
        /// <summary>
        /// Contact Email Address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Indicates if this is the primary contact
        /// </summary>
        public bool PrimaryFlag { get; set; }
        /// <summary>
        /// Does the model have data?  Because the Razor must have an object to map to 
        /// one needs a way to tell if there is data.
        /// </summary>
        /// <returns>True if the model has data; false otherwise</returns>
        public override bool HasData()
        {
            return ((!string.IsNullOrWhiteSpace(this.FirstName)) && (!string.IsNullOrWhiteSpace(this.LastName)));
        }
    }
}
