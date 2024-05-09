using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Sra.P2rmis.Bll;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Web.Controllers.UserProfileManagement;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.ViewModels.UserProfileManagement;
// need to get lookup constants into this table

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for Update User
    /// </summary>
    [Validator(typeof(ProfileValidator))]
    public class ProfileViewModel : UserProfileManagementViewModel
    {
        #region Constants
        /// <summary>
        /// Minimum number of phone numbers to display in the alternate contact section.
        /// </summary>
        private const int MinimumNumberPhone = 2;
        /// <summary>
        /// Identifier for Us in the country dropdown
        /// </summary>
        public static int UsCountryId = LookupService.LookupUsCountryId;
        /// <summary>
        /// Identifier for Spouse Alternate Contact Type
        /// </summary>
        public static int SpouseAlternateContactTypeId = LookupService.LookupAlternateContactTypeSpouseId;
        /// <summary>
        /// Identifier for Spouse Alternate Contact Type
        /// </summary>
        public static int EmergencyContactTypeId = LookupService.LookupEmContactTypeId;
        /// <summary>
        /// Identifier for Assistant Alternate Contact Type
        /// </summary>
        public static int AssistantAlternateContactTypeId = LookupService.LookupAlternateContactTypeAssistantId;
        /// <summary>
        /// Identifier for Organization Address Type
        /// </summary>
        public static int OrganizationAddressTypeId = LookupService.LookupAddressTypeOrganizationId;
        /// <summary>
        /// Identifier for Personal Address Type
        /// </summary>
        public static int PersonalAddressTypeId = LookupService.LookupAddressTypePersonalId;
        /// <summary>
        /// Minimum phone numbers to display in the phone numbers section.
        /// </summary>
        private static IReadOnlyCollection<int> RequiredPhoneTypes =
            new ReadOnlyCollection<int>(new List<int> { 9, 3, 6 });
        /// <summary>
        /// The misconduct profile type identifier
        /// </summary>
        public static int MisconductProfileTypeId = LookupService.LookupSystemProfileTypeMisconductId;
        /// <summary>
        /// Identifier for the assistant alternate contact type
        /// </summary>
        public static int AssistantAlternateContactId = LookupService.LookupAlternateContactTypeAssistantId;
        /// <summary>
        /// Identifier for a profile tye of prospect
        /// </summary>
        public static int ProspectProfileTypeId = LookupService.LookupSystemProfileTypeProspectId;
        /// <summary>
        /// Identifier for a profile type of reviewer
        /// </summary>
        public static int ReviewerProfileTypeId = LookupService.LookupSystemProfileTypeReviewerId;
        /// <summary>
        /// Identifier for a Professional affiliation Institution type
        /// </summary>
        public static int AffiliationInstutionTypeId = LookupService.LookupProfessionalAffiliationInstitutionOrganizationId;
        /// <summary>
        /// Identifier for Professional affiliation nominating organization type
        /// </summary>
        public static int AffiliationNominatingOrganization = LookupService.LookupProfessionalAffiliationNominatingOrganizationId;
        /// <summary>
        /// The default Address Type identifier
        /// </summary>
        public static int DefaultAddressTypeId = LookupService.LookupAddressTypeOrganizationId;
        internal bool IsSro;
        /// <summary>
        /// The account status reason identifier for ineligible
        /// </summary>
        public int AccountStatusReasonIneligibleId { get { return LookupService.LookupAccountStatusReasonIneligible; } }
        #endregion
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfileViewModel()
        {
            //
            // Initialize the various data containers
            //
            this.IsVerify = false;
            this.IsUserProfileVerified = false;
            this.GeneralInfo = new GeneralInfoModel();
            this.Websites = new List<WebsiteModel>();
            this.InstitutionEmailAddress = new EmailAddressModel();
            this.PersonalEmailAddress = new EmailAddressModel();
            this.Addresses = new List<AddressInfoModel>();
            this.PhoneTypeDropdown = new List<IListEntry>();
            this.AlternateContactTypeDropdown = new List<IListEntry>();
            this.AlternateContactPersons = new List<UserAlternateContactPersonModel>();
            this.UserPhones = new List<PhoneNumberModel>();
            this.UserDegrees = new List<UserDegreeModel>();
            this.MilitaryServiceAndRank = new UserMilitaryRankModel();
            this.MilitaryStatus = new UserMilitaryStatusModel();
            this.UserClients = new List<UserProfileClientModel>();
            this.ActiveUserClients = new List<UserProfileClientModel>();
            this.ClientsBlocked = new List<KeyValuePair<int, string>>();
            this.HistoryTable = new List<IUserClientBlockLog>();
            this.AvailableUserClients = new List<UserProfileClientModel>();
            this.AccountDetails = new UserManageAccountModel();
            this.W9Addresses = new W9AddressModel();
            this.ProfessionalAffiliation = new ProfessionalAffiliationModel();
            this.VendorInfoIndividual = new UserVendorModel();
            this.VendorInfoInstitutional = new UserVendorModel();

            //
            // Initialize the drop downs.  Service and specially the rank drop
            // down are special.  Rank depends on service and if there is not
            // a service selected it should be empty.
            //
            this.MilitaryServiceDropdown = new List<IListEntry>();
            this.MilitaryRankDropdown = new List<IListEntry>();
            this.RoleDropdown = new List<IListEntry>();
            this.AcademicRankDropdown = new List<IListEntry>();
            this.ProfessionalAffiliationDropdown = new List<IListEntry>();
            //
            // This is for the military service selection
            //
            this.MilitaryServiceId = 0;
            this.UsCountryValue = UsCountryId;
            this.IsMyProfile = false;
            this.CanUploadCv = false;
            //
            // This is for the W9 form
            //
            this.W9DownloadUrl = ConfigManager.W9FormDownload;
            this.W8DownloadUrl = ConfigManager.W8FormDownload;
            this.W9FaxNumber = ConfigManager.W9FormFax;
            //
            // Back button
            //
            this.LastPageUrl = string.Empty;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public ProfileViewModel(string firstName, string lastName, string userName, bool isUpdateUser) : this()
        {
            this.IsVerify = false;
            GeneralInfo.FirstName = firstName;
            GeneralInfo.LastName = lastName;
            GeneralInfo.Username = userName;
            IsUpdateUser = isUpdateUser;
        }
        #endregion

        #region Dropdowns
        /// <summary>
        /// Phone type dropdown list
        /// </summary>
        public List<IListEntry> PhoneTypeDropdown { get; set; }
        /// <summary>
        /// the prefix dropdown list
        /// </summary>
        public List<IListEntry> PrefixDropdown { get; set; }
        /// <summary>
        /// Alternate contact type dropdown list
        /// </summary>
        public List<IListEntry> AlternateContactTypeDropdown { get; set; }
        /// <summary>
        /// the gender dropdown list
        /// </summary>
        public List<IListEntry> GenderDropdown { get; set; }
        /// <summary>
        /// the ethnicity dropdown list
        /// </summary>
        public List<IListEntry> EthnicityDropdown { get; set; }
        /// <summary>
        /// the degree dropdown list
        /// </summary>
        public List<IListEntry> DegreeDropdown { get; set; }
        /// <summary>
        /// the profile types dropdown
        /// </summary>
        public List<IListEntry> ProfileTypesDropdown { get; set; }
        /// <summary>
        /// Values for the military service dropdown
        /// </summary>
        public IEnumerable<IListEntry> MilitaryServiceDropdown { get; set; }
        /// <summary>
        /// Values for the military rank  dropdown
        /// </summary>
        public IEnumerable<IListEntry> MilitaryRankDropdown { get; set; }
        /// <summary>
        /// Values for the military status dropdown
        /// </summary>
        public IEnumerable<IListEntry> MilitaryStatusDropdown { get; set; }
        /// <summary>
        /// the state dropdown
        /// </summary>
        public List<IListEntry> StateDropdown { get; set; }
        /// <summary>
        /// the country dropdown
        /// </summary>
        public List<IListEntry> CountryDropdown { get; set; }
        /// <summary>
        /// Values for the professional affiliation dropdown
        /// </summary>
        public IEnumerable<IListEntry> ProfessionalAffiliationDropdown { get; set; }
        /// <summary>
        /// Values for the alternate contact type dropdown
        /// </summary>
        public IEnumerable<IListEntry> AlternateContactTypesDropdown { get; set; }
        /// <summary>
        /// Dropdown list for account statuses
        /// </summary>
        public IEnumerable<IListEntry> AccountStatusDropdown { get; set; }
        /// <summary>
        /// Dropdown list for roles
        /// </summary>
        public IEnumerable<IListEntry> RoleDropdown { get; set; }
        /// <summary>
        /// Dropdown List for academic rank
        /// </summary>
        public IEnumerable<IListEntry> AcademicRankDropdown { get; set; }
        /// <summary>
        /// Dropdown List for address type
        /// </summary>
        public IEnumerable<IListEntry> AddressTypeDropdown { get; set; }
        #endregion
        #region Properties
        /// Is Verify Profile page
        /// </summary>
        public bool IsVerify { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is sro value.
        /// </summary>
        public bool IsSroValue { get; set; }
        /// <summary>
        /// Is user profile verified
        /// </summary>
        public bool IsUserProfileVerified { get; set; }
        /// <summary>
        /// Country Id for USA
        /// </summary>
        public int UsCountryValue { get; set; }
        /// <summary>
        /// The vendor information.
        /// </summary>
        public UserVendorModel VendorInfoIndividual { get; set; }
        /// <summary>
        /// Gets or sets the vendor information institutional.
        /// </summary>
        public UserVendorModel VendorInfoInstitutional { get; set; }

        /// <summary>
        /// General information
        /// </summary>
        /// <remarks>Use a specific type instead of an interface because validation attributes don't work with interfaces</remarks>
        public GeneralInfoModel GeneralInfo { get; set; }
        /// <summary>
        /// Websites
        /// </summary>
        public List<WebsiteModel> Websites { get; set; }
        /// <summary>
        /// Institution email addresses
        /// </summary>
        /// <remarks>Use a specific type instead of an interface because validation attributes don't work with interfaces</remarks>
        public EmailAddressModel InstitutionEmailAddress { get; set; }
        /// <summary>
        /// Personal email addresses
        /// </summary>
        /// <remarks>Use a specific type instead of an interface because validation attributes don't work with interfaces</remarks>
        public EmailAddressModel PersonalEmailAddress { get; set; }
        //public List<InstitutionAddressModel> InstitutionAddresses { get; set; }
        /// <summary>
        /// Personal address
        /// </summary>
        public List<AddressInfoModel> Addresses { get; set; }
        /// <summary>
        /// W9 addresses
        /// </summary>
        public W9AddressModel W9Addresses { get; set; }
        /// <summary>
        /// Alternate contact phones
        /// </summary>
        public List<UserAlternateContactPersonModel> AlternateContactPersons { get; set; }
        /// <summary>
        /// The user contact phone numbers
        /// </summary>
        public List<PhoneNumberModel> UserPhones { get; set; }
        /// <summary>
        /// whether the user is creating a user or updating a user
        /// </summary>
        public bool IsUpdateUser { get; set; }
        /// <summary>
        /// The users degrees
        /// </summary>
        public List<UserDegreeModel> UserDegrees { get; set; }
        /// <summary>
        /// The user resume
        /// </summary>
        public ResumeModel UserResume { get; set; }
        /// <summary>
        /// The user's military service branch and rank.
        /// </summary>
        public IUserMilitaryRankModel MilitaryServiceAndRank { get; set; }
        /// <summary>
        /// The users military service (used to drive the military selection dropdowns)
        /// </summary>
        public int? MilitaryServiceId { get; set; }
        /// <summary>
        /// The Military status
        /// </summary>
        public IUserMilitaryStatusModel MilitaryStatus { get; set; }
        /// <summary>
        /// The available user's clients 
        /// </summary>
        public List<UserProfileClientModel> AvailableUserClients { get; set; }
        /// <summary>
        /// The user's clients
        /// </summary>
        public List<UserProfileClientModel> UserClients { get; set; }
        /// <summary>
        /// Active Clients
        /// </summary>
        public List<UserProfileClientModel> ActiveUserClients { get; set; }
        /// <summary>
        /// Clients Blocked
        /// </summary>
        public List<KeyValuePair<int, string>> ClientsBlocked { get; set; }
        /// <summary>
        /// Clients Blocked
        /// </summary>
        public List<IUserClientBlockLog> HistoryTable { get; set; }
        /// <summary>
        /// The user's preofessional affiliation
        /// </summary>
        public ProfessionalAffiliationModel ProfessionalAffiliation { get; set; }
        /// <summary>
        /// The user's clients to show on the list
        /// </summary>
        public List<SelectListItem> UserClientsToShow
        {
            get
            {
                return (AvailableUserClients != null) ?
                    AvailableUserClients.Select(x => new SelectListItem { Text = x.ClientAbrv, Value = Convert.ToString(x.ClientId) }).ToList() :
                    new List<SelectListItem>();
            }
        }
        /// <summary>
        /// The user's clients selected
        /// </summary>
        public string[] UserClientsSelected
        {
            get
            {
                return (UserClients != null) ?
                    UserClients.Select(x => x.ClientId.ToString()).ToArray<string>() :
                    null;
            }
        }

        public bool ShowCreateLink
        {
            get
            {
                //Returns true if session has been set with create context and user is not viewing their own profile
                return HttpContext.Current.Session != null &&
                    HttpContext.Current.Session[UserProfileManagementBaseController.ProfileManagementSessionContext] != null &&
                    HttpContext.Current.Session[UserProfileManagementBaseController.ProfileManagementSessionContext]
                        .ToString() == UserProfileManagementBaseController.SearchContexts.CreateContext
                    && !IsMyProfile && IsUpdateUser;
            }
        }
        /// <summary>
        /// Indicates if the user has the ability to manage the password
        /// </summary>
        public bool CanManagePassword { get; set; }

        /// <summary>
        /// Indicates whether the user can upload a CV
        /// </summary>
        public bool CanUploadCv { get; set; }

        /// <summary>
        /// Does this user have permanent credentials
        /// </summary>
        public bool PermanentCredentials { get; set; }

        /// <summary>
        /// Indicates if the badge name should be editable?
        /// </summary>
        public bool IsBadgeNameEditable { get; set; }
        /// <summary>
        /// Misconduct type entity identifier
        /// </summary>
        public int MisconductType { get; set; }
        /// <summary>
        /// Indicates if the Role dropdown should be displayed
        /// </summary>
        public bool AreRolesDisplayed { get; set; }
        /// <summary>
        /// Indicates if the Role dropdown should be disabled when it is displayed.  By default this 
        /// property will remain false.  There is one case where it needs to be set.  This is when a user
        /// searches for their own profile.  In this case they should not be able to change their own role.
        /// </summary>
        public bool AreRolesDisabled { get; set; }
        public string W9DownloadUrl { get; private set; }
        public string W8DownloadUrl { get; private set; }
        public string W9FaxNumber { get; private set; }
        /// <summary>
        /// Gets the page title.
        /// </summary>
        /// <value>
        /// The page title.
        /// </value>
        public string PageTitle
        {
            get
            {
                return (Tabs.Count > 0) ? Tabs[0].TabName : string.Empty;
            }
        }
        /// <summary>
        /// Gets the profile date.
        /// </summary>
        /// <value>
        /// The profile date.
        /// </value>
        public string ProfileDate
        {
            get
            {
                return GeneralInfo.ModifiedDate == null ? ViewHelpers.FormatLastUpdateDateTime(GlobalProperties.P2rmisDateTimeNow) : ViewHelpers.FormatLastUpdateDateTime(GeneralInfo.ModifiedDate);
            }
        }
        /// <summary>
        /// Gets the save profile action.
        /// </summary>
        /// <value>
        /// The save profile action.
        /// </value>
        public string SaveProfileAction
        {
            get
            {
                return IsMyProfile ? Routing.UserProfileManagement.SaveMyProfile : (IsUpdateUser) ? Routing.UserProfileManagement.SaveProfile : Routing.UserProfileManagement.CreateProfile;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance has zero or one address.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has zero or one address; otherwise, <c>false</c>.
        /// </value>
        public bool HasZeroOrOneAddress
        {
            get
            {
                return Addresses.Where(x => !x.IsDeletable).GroupBy(x => x.IsDeletable).Count() < 2;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is reviewer.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is reviewer; otherwise, <c>false</c>.
        /// </value>
        public bool IsReviewer
        {
            get
            {
                return ViewHelpers.IsReviewer(GeneralInfo.ProfileTypeId);
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is prospect or misconduct.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is prospect or misconduct; otherwise, <c>false</c>.
        /// </value>
        public bool IsProspectOrMisconduct
        {
            get
            {
                return GeneralInfo.ProfileTypeId == ProspectProfileTypeId || GeneralInfo.ProfileTypeId == MisconductProfileTypeId;
            }
        }
        /// <summary>
        /// Gets the maximum website number.
        /// </summary>
        /// <value>
        /// The maximum website number.
        /// </value>
        public int MaxWebsiteNumber
        {
            get
            {
                return Math.Min(Websites.Count, 2);
            }
        }
        /// <summary>
        /// Gets the prospect profile type identifier value.
        /// </summary>
        /// <value>
        /// The prospect profile type identifier value.
        /// </value>
        public int ProspectProfileTypeIdValue
        {
            get
            {
                return ProspectProfileTypeId;
            }
        }
        /// <summary>
        /// Gets the misconduct profile type identifier value.
        /// </summary>
        /// <value>
        /// The misconduct profile type identifier value.
        /// </value>
        public int MisconductProfileTypeIdValue
        {
            get
            {
                return MisconductProfileTypeId;
            }
        }
        /// <summary>
        /// Gets the affiliation instution type identifier value.
        /// </summary>
        /// <value>
        /// The affiliation instution type identifier value.
        /// </value>
        public int AffiliationInstutionTypeIdValue
        {
            get
            {
                return AffiliationInstutionTypeId;
            }
        }
        /// <summary>
        /// Gets the affiliation nominating organization value.
        /// </summary>
        /// <value>
        /// The affiliation nominating organization value.
        /// </value>
        public int AffiliationNominatingOrganizationValue
        {
            get
            {
                return AffiliationNominatingOrganization;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance has zero or one alternate contacts.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has zero or one alternate contacts; otherwise, <c>false</c>.
        /// </value>
        public bool HasZeroOrOneAlternateContacts
        {
            get
            {
                return AlternateContactPersons.Count() < 2;
            }
        }
        /// <summary>
        /// Gets the account status.
        /// </summary>
        /// <value>
        /// The account status.
        /// </value>
        public string AccountStatus
        {
            get
            {
                return ViewHelpers.FormatStatus(AccountDetails.Status, AccountDetails.StatusReason);
            }
        }
        /// <summary>
        /// Gets the sent by.
        /// </summary>
        /// <value>
        /// The sent by.
        /// </value>
        public string SentBy
        {
            get
            {
                return ViewHelpers.ConstructShortName(AccountDetails.SentByFirstName, AccountDetails.SentByLastName);
            }
        }
        /// <summary>
        /// Gets the un locked by.
        /// </summary>
        /// <value>
        /// The un locked by.
        /// </value>
        public string UnLockedBy
        {
            get
            {
                return @ViewHelpers.ConstructShortName(AccountDetails.UnLockedByFirstName, AccountDetails.UnLockedByLastName);
            }
        }
        /// <summary>
        /// Last page's URL
        /// </summary>
        public string LastPageUrl { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can view vendor management.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can view vendor management; otherwise, <c>false</c>.
        /// </value>
        public bool CanViewVendorManagement { get; set; }
        #endregion
        #region Business layer error messages
        /// <summary>
        /// Status messages from the business layer
        /// </summary>
        public List<string> StatusMessages { get; set; }
        /// <summary>
        /// Whether the profile is the user's own profile
        /// </summary>
        /// <remarks>
        /// Lower level of permission requirement to access
        /// </remarks>
        public bool IsMyProfile { get; set; }
        /// <summary>
        /// Details of a user's account current state and history
        /// </summary>
        public UserManageAccountModel AccountDetails { get; set; }
        /// <summary>
        /// Gets the preferred text.
        /// </summary>
        /// <value>
        /// The preferred text.
        /// </value>
        public string PreferredText
        {
            get
            {
                return "(Preferred)";
            }
        }
        /// <summary>
        /// Gets the secondary text.
        /// </summary>
        /// <value>
        /// The secondary text.
        /// </value>
        public string SecondaryText
        {
            get
            {
                return "Secondary";
            }
        }
        /// <summary>
        /// Gets the organization address type identifier value.
        /// </summary>
        /// <value>
        /// The organization address type identifier value.
        /// </value>
        public int OrganizationAddressTypeIdValue
        {
            get
            {
                return OrganizationAddressTypeId;
            }
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Generates the section label tag.
        /// </summary>
        /// <param name="item">IUserAlternateContactPersonModel model describing the alternate contact</param>
        /// <returns>Section label</returns>
        public string AlternateContactSectionLabel(IUserAlternateContactPersonModel item)
        {
            return (item.PrimaryFlag) ? "Preferred" : "Secondary";
        }
        /// <summary>
        /// Checks the UserAlternateContactPersonModel list of phone number models for at least
        /// two entries.  If less that two entries are detected, then new UserAlternateContactPersonPhoneModel
        /// are added.
        /// </summary>
        /// <returns></returns>
        public string MakeAtLeastTwoAlternates(IUserAlternateContactPersonModel model)
        {
            //
            // We always need to have two entries on the screen.  If we do not
            // have two entries from the database then add them so there
            // is something to map to (and consequently save)
            //
            if (model.AlternateContactPhone.Count() < MinimumNumberPhone)
            {
                //
                // Create a new list to work with.  Then add one or more entries
                // to it to get to the minimum number.
                //
                List<UserAlternateContactPersonPhoneModel> list = model.AlternateContactPhone;

                while (list.Count() < MinimumNumberPhone)
                {
                    UserAlternateContactPersonPhoneModel entry = new UserAlternateContactPersonPhoneModel();
                    list.Add(entry);
                }
                //
                // And replace the original list with the new list
                //
                model.AlternateContactPhone = list;
            }
            return string.Empty;
        }
        /// <summary>
        /// Checks the PhoneNumberModel list of phone number models for required entries.  
        /// If the minimum number of phones is not present, then new PhoneNumberModel is added.
        /// </summary>
        /// <returns></returns>
        /// <remarks>TODO:Needs unit tests</remarks>
        public string PopulateMinimumPhones(List<PhoneNumberModel> model)
        {
            //
            // We always need to have two entries on the screen.  If we do not
            // have two entries from the database then add them so there
            // is something to map to (and consequently save)
            //
            if (model.Count() < RequiredPhoneTypes.Count())
            {
                //
                // Create a new list to work with.  Then add one or more entries
                // to it to get to the minimum number.
                //
                List<PhoneNumberModel> list = model;
                bool isFirst = list.Count == 0;
                foreach (int item in RequiredPhoneTypes.TakeWhile(x => list.Count() < RequiredPhoneTypes.Count()))
                {

                    var entry = new PhoneNumberModel { PhoneTypeId = item, Primary = isFirst, International = false };
                    if ((list.Any(x => x.PhoneTypeId == item)) == false)
                    {
                        list.Add(entry);
                        isFirst = false;
                    }
                }
                //
                // And replace the original list with the new list
                //
                model = list;
            }
            return string.Empty;
        }
        /// <summary>
        /// Shows the Extension control if the phone type is desk.  Extension control is not shown otherwise.
        /// </summary>
        /// <param name="model">IUserAlternatePersonContactPhoneModel describing the phone contact information</param>
        /// <returns>Tag value to show/hide extension control</returns>
        public string ShowExtension(IUserAlternatePersonContactPhoneModel model)
        {
            return ShowHidden(model.PhoneTypeId != LookupService.LookupPhoneTypeIdForDesk);
        }
        /// <summary>
        /// Shows the Extension control if the phone type is desk.  Extension control is not shown otherwise.
        /// </summary>
        /// <param name="model">PhoneNumberModel describing the phone contact information</param>
        /// <returns>Tag value to show/hide extension control</returns>
        public string ShowExtension(PhoneNumberModel model)
        {
            return ShowHidden(model.PhoneTypeId != LookupService.LookupPhoneTypeIdForDesk);
        }
        /// <summary>
        /// Sets the preferred radio button if the phone is the primary phone.
        /// </summary>
        /// <param name="model">IUserAlternatePersonContactPhoneModel describing the phone contact information</param>
        /// <returns>checked if the phone is primary; empty string otherwise</returns>
        public string IsChecked(IUserAlternatePersonContactPhoneModel model)
        {
            return (model.PrimaryFlag != null && (bool)model.PrimaryFlag) ? "checked" : string.Empty;
        }
        /// <summary>
        /// Show the radio button indicator that the alternate contact is the preferred contact.
        /// </summary>
        /// <param name="model">IUserAlternateContactPersonModel describing the contact information</param>
        /// <returns>Tag value to show/hide extension control</returns>
        public string IsPrimary(IUserAlternateContactPersonModel model)
        {
            return ShowHidden(!(bool)model.PrimaryFlag);
        }
        /// <summary>
        /// Show the radio button indicator that the phone number is the preferred contact.
        /// </summary>
        /// <param name="model">IUserAlternatePersonContactPhoneModel describing the phone contact information</param>
        /// <returns>Tag value to show/hide extension control</returns>
        public string IsPrimary(IUserAlternatePersonContactPhoneModel model)
        {
            return ShowHidden(!(bool)model.PrimaryFlag);
        }
        /// <summary>
        /// Indicates if the international checkbox should be checked
        /// </summary>
        /// <param name="model">IUserAlternatePersonContactPhoneModel describing the phone contact information</param>
        /// <returns>checked if the phone is international; empty string otherwise</returns>
        public string IsInternationalPhone(IUserAlternatePersonContactPhoneModel model)
        {
            return (model.International == false) ? string.Empty: "checked";
        }
        /// <summary>
        /// Returns the hidden tag or empty string (not hidden) based on test value.
        /// </summary>
        /// <param name="value">Test value</param>
        /// <returns>hidden tag if true; empty string otherwise</returns>
        private string ShowHidden(bool value)
        {
            return (value) ? "hidden" : string.Empty;
        }   
        /// <summary>
        /// Get HTML attributes for radio button status
        /// </summary>
        /// <param name="shouldCheck">Whether the element should be checked</param>
        /// <param name="classAttribute">The class attribute</param>
        /// <param name="checkedAttribute">The checked attribute</param>
        /// <returns>The HtmlAttribute dictionary object</returns>
        public IDictionary<string, object> GetHtmlAttribute(bool? shouldCheck, KeyValuePair<string, string> classAttribute, 
                KeyValuePair<string, string> checkedAttribute) {
            IDictionary<string, object> newAttributes = new Dictionary<string, object>();
            newAttributes.Add(new KeyValuePair<string, object>(classAttribute.Key, classAttribute.Value));
            if (shouldCheck != null && (bool)shouldCheck)
            {
                newAttributes.Add(new KeyValuePair<string, object>(checkedAttribute.Key, checkedAttribute.Value));
            }
            return newAttributes;
        }
        /// <summary>
        /// Get address preferred/not preferred text
        /// </summary>
        /// <param name="address">The AddressInfoModel model</param>
        /// <param name="preferredText">The preferred text</param>
        /// <param name="notPreferredText">The non-preferred text</param>
        /// <returns></returns>
        public string GetAddressPreferredText(AddressInfoModel address, string preferredText, string notPreferredText)
        {
            return (address != null && address.PrimaryFlag != null && (bool)address.PrimaryFlag) ? preferredText : notPreferredText;
        }
        /// <summary>
        /// Get alternate contact preferred/not preferred text
        /// </summary>
        /// <param name="alternateContact">The UserAlternateContactPersonModel model</param>
        /// <param name="preferredText">The preferred text</param>
        /// <param name="notPreferredText">The non-preferred text</param>
        /// <returns>The indicated alternate contact preferred or non-preferred text</returns>
        public string GetAlternateContactPreferredText(UserAlternateContactPersonModel alternateContact, string preferredText, string notPreferredText)
        {
            return (alternateContact != null && alternateContact.PrimaryFlag) ? preferredText : notPreferredText;
        }
        /// <summary>
        /// Get section title in HTML
        /// </summary>
        /// <param name="mainSectionTitle">The main section title</param>
        /// <param name="index">The index of the current section</param>
        /// <returns>The new section title in HTML</returns>
        public string GetSectionTitleHtml(string mainSectionTitle, int index)
        {
            string newTitle = mainSectionTitle;
            newTitle = mainSectionTitle + " " + (index + 1).ToString();

            return newTitle;
        }
        /// <summary>
        /// Determines if the Role dropdown list should be enabled
        /// </summary>
        /// <returns>true if the dropdown should be enabled, false otherwise</returns>
        public bool EnableRoleDropDownList()
        {
            return (this.RoleDropdown.Count() > 0 && this.GeneralInfo.ProfileTypeId.HasValue && this.GeneralInfo.ProfileTypeId.Value != LookupService.LookupSystemProfileTypeMisconductId);
        }
        /// <summary>
        /// Indicates if the drop down should be enabled or disabled for a user with the same profile type.
        /// </summary>
         /// <param name="targetProfileTypeId">Target ProfileType entity identifier</param>
        /// <param name="targetSystemPriorityOrder">Target user's role priority order.</param>
        /// <param name="userProfileTypeId">Current user ProfileType entity identifier</param>
        /// <param name="userSystemRolePriorityOrder">User role hierarchy order value</param>
        /// <returns>True if the dropdown should be enabled; false otherwise</returns>
        public bool EnableDropDownListForSameProfileType { get; set; }
        /// <summary>
        /// Formats the list of user clients into a comma separated string of client abbreviations.
        /// </summary>
        /// <returns></returns>
        public string UserClientsFormat()
        {
            StringBuilder clientFormat = new StringBuilder();

            foreach (UserProfileClientModel item in this.UserClients)
            {
                clientFormat = (clientFormat.Length == 0) ? clientFormat.Append(item.ClientAbrv) : clientFormat.AppendFormat(", {0}", item.ClientAbrv);
            }
            return clientFormat.ToString();
        }
        /// <summary>
        /// Cleans up addresses.
        /// </summary>
        public void CleanUpAddresses()
        {
            for (var i = 0; i < Addresses.Count; i++)
            {
                var address = Addresses[i];
                if (address.AddressTypeId == PersonalAddressTypeId)
                {
                    address.Address3 = null;
                    address.Address4 = null;
                }
            }
        }
        /// <summary>
        /// Constructs the attribute list for the W9 radio buttons.  The radio
        /// buttons should only be enabled when the actual user is viewing the
        /// profile
        /// </summary>
        /// <returns>Dictionary of mark-up attributes for MVC radio button control</returns>
        public Dictionary<string, object> W9RadioButtonAttributes()
        {
            var htmlAttributes = new Dictionary<string, object>();
            if (!this.IsMyProfile)
            {
                htmlAttributes.Add("disabled", "disabled");
            }
            return htmlAttributes;
        }

        public Dictionary<string, object> W9RadioButtonAttributesIncludingId(string id)
        {
            var htmlAttributes = W9RadioButtonAttributes();
            htmlAttributes.Add("id", id);
            htmlAttributes.Add("class", "alignLeft margin-right10");
            return htmlAttributes;
        }
        #endregion
    }
}