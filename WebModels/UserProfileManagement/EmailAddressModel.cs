namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Email Address Information
    /// </summary>
    public class EmailAddressModel : Editable, IEmailAddressModel
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public EmailAddressModel() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="emailID"></param>
        /// <param name="emailAddressTypeId"></param>
        /// <param name="emailAddress"></param>
        /// <param name="primaryFlag"></param>
        public EmailAddressModel(int emailID, int emailAddressTypeId, string emailAddress, bool primaryFlag)
        {
            this.EmailId = emailID;
            this.EmailAddressTypeId = emailAddressTypeId;
            this.Address = emailAddress;
            this.Primary = primaryFlag;
        }
        /// <summary>
        /// Initialize model
        /// </summary>
        /// <param name="model">the EmailAddressModel</param>
        public static void InitializeModel(EmailAddressModel model) { }
        #endregion
        /// <summary>
        /// Email identifier
        /// </summary>
        public int EmailId { get; set; }
        /// <summary>
        /// Email Address Type identifier
        /// </summary>
        public int EmailAddressTypeId { get; set; }
        ///<summary>
        /// Email Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Primary Email
        /// </summary>
        public bool Primary { get; set; }
        /// <summary>
        /// Indicates if the email is a new email address to be added
        /// </summary>
        public bool IsAdd
        {
            get { return ((EmailId <= 0 && HasData() && !IsDeletable) ? true : false); }
        }
        /// <summary>
        /// Indicates if the email is an email address to be deleted
        /// </summary>
        public override bool IsDeleted()
        {
            return (EmailId > 0 && (IsDeletable || !HasData()));
        }
        public override bool HasData()
        {
            return !string.IsNullOrEmpty(Address);
        }
    }
}
