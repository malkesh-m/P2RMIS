namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Phone Number Information
    /// </summary>
    public class PhoneNumberModel : Editable, IPhoneNumberModel
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public PhoneNumberModel() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="phoneId">UserPhone entity identifier</param>
        /// <param name="phoneTypeId">PhoneType  entity identifier</param>
        /// <param name="number">Phone number</param>
        /// <param name="primary">Primary indicator</param>
        /// <param name="extension">Phone extension</param>
        /// <param name="international">International indicator</param>
        public PhoneNumberModel(int phoneId, int? phoneTypeId, string number, bool primary, string extension, bool international)
        {
            this.PhoneId = phoneId;
            this.PhoneTypeId = phoneTypeId;
            this.Number = number;
            this.Primary = primary;
            this.Extension = extension;
            this.International = international;
        }
        #endregion
        /// <summary>
        /// Phone Number identifier
        /// </summary>
        public int PhoneId { get; set; }
        /// <summary>
        /// Phone Type identifier
        /// </summary>
        public int? PhoneTypeId { get; set; }
        /// <summary>
        /// Phone Number
        /// </summary>
        public string Number { get; set; }
        /// <Summary>
        /// Primary Phone indicator
        /// </Summary>
        public bool Primary { get; set; }
        /// <summary>
        /// Optional extension
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// International Phone indicator
        /// </summary>
        public bool International { get; set; }
        /// <summary>
        /// Indicates if the phone number is deleted
        /// </summary>
        /// <returns>Returns true if the item is to be deleted, false otherwise</returns>
        public override bool IsDeleted()
        {
            return (PhoneId > 0 & !HasData());
        }
        /// <summary>
        /// Indicates if the phone number model has data:
        /// - Number property is not null or white space
        /// </summary>
        /// <returns>True if the model has data; false otherwise</returns>
        public override bool HasData()
        {
            return !string.IsNullOrWhiteSpace(this.Number);
        }
    }
}
