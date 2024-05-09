using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.Ajax.Utilities;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.EntityMetadata;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Web.Controllers.UserProfileManagement;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    #region Validation
    /// <summary>
    /// FluentValidation Validator for ProfileViewModel
    /// </summary>
    public class ProfileValidator : AbstractValidator<ProfileViewModel>
    {
        public ProfileValidator()
        {
            //First name max length and required
            RuleFor(x => x.GeneralInfo.FirstName).NotEmpty()
                .WithMessage(MessageService.Constants.FieldRequired, "First Name");
            RuleFor(x => x.GeneralInfo.FirstName).Length(0, UserInfoMetadata.GetFirstNameLength())
                .WithMessage(MessageService.Constants.MaxLengthExceeded, "First Name", UserInfoMetadata.GetFirstNameLength());
            //Last name max length and required
            RuleFor(x => x.GeneralInfo.LastName).NotEmpty()
                .WithMessage(MessageService.Constants.FieldRequired, "Last Name");
            RuleFor(x => x.GeneralInfo.LastName).Length(0, UserInfoMetadata.GetLastNameLength())
                .WithMessage(MessageService.Constants.MaxLengthExceeded, "Last Name", UserInfoMetadata.GetLastNameLength());            
            //Email address format
            RuleFor(x => x.InstitutionEmailAddress.Address).EmailAddress()
                .WithMessage(MessageService.Constants.EmailAddressErrorMessage);
            //Email address format
            RuleFor(x => x.PersonalEmailAddress.Address).EmailAddress()
                .WithMessage(MessageService.Constants.EmailAddressErrorMessage);
            // One non-blank email address is required to be preferred
            RuleFor(x => x.IsUpdateUser)
                .Must(AtleastOnePreferredEmailAddress)
                .When(y => !y.InstitutionEmailAddress.Address.IsNullOrWhiteSpace() || !y.PersonalEmailAddress.Address.IsNullOrWhiteSpace())
                .WithMessage(MessageService.Constants.PreferredEmailRequired);
            //At least one phone number is required to be preferred if a non-blank phone exists or the user's profile type is reviewer
            RuleFor(x => x.IsUpdateUser)
                .Must(AtLeastOnePreferredIfPhoneNumbersExist)
                .When(y => y.UserPhones.Count(x => !x.Number.IsNullOrWhiteSpace()) > 0 || y.GeneralInfo.ProfileTypeId == ProfileViewModel.ReviewerProfileTypeId)
                .WithMessage(MessageService.Constants.PreferredPhoneRequired);
            //At least one address is required to be preferred if a non-blank address exists or the user's profile type is reviewer
            RuleFor(x => x.IsUpdateUser)
                .Must(AtLeastOnePreferredAddress)
                .When(y => (y.GeneralInfo.ProfileTypeId == ProfileViewModel.ReviewerProfileTypeId && (y.IsMyProfile || y.IsUserProfileVerified)) || AnyNonBlankAddress(y.Addresses))
                .WithMessage(MessageService.Constants.ConditionalFieldRequired, "a preferred address");
            RuleFor(x => x.IsUpdateUser)
                .Must(NoMoreThanOnePersonalAddress)
                 .WithMessage(MessageService.Constants.NoMoreThanOnePersonalAddressAllowed);
            //Profile type selection required for create user
            RuleFor(x => x.GeneralInfo.ProfileTypeId).NotEmpty()
                .When(w => !w.IsUpdateUser)
                .WithMessage(MessageService.Constants.FieldRequired, "Profile Type");
            //If military branch is selected, military status and rank are required.
            RuleFor(x => x.MilitaryServiceAndRank.MilitaryRankId).NotEmpty()
                .When(y => y.MilitaryServiceId.HasValue && y.MilitaryServiceId.Value > 0)
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "Rank", FieldsetLabels.MilitaryRank);
            //If military branch and rank is selected, military status is required.
            RuleFor(x => x.MilitaryStatus.MilitaryStatusTypeId).NotEmpty()
                .When(y => y.MilitaryServiceId.HasValue && y.MilitaryServiceAndRank.MilitaryRankId.HasValue)
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "Status", FieldLabels.Status);
            //If profile type is anything other than misconduct or prospect, at least one email is required
            RuleFor(x => x.InstitutionEmailAddress.Address).NotEmpty()
                .When(y => NotMisconductAndNotProspect(y) &&
                        (y.InstitutionEmailAddress.Primary || !y.PersonalEmailAddress.Primary))
                .WithMessage(MessageService.Constants.FieldRequired, "Email");
            RuleFor(x => x.PersonalEmailAddress.Address).NotEmpty()
                .When(y => NotMisconductAndNotProspect(y) && y.PersonalEmailAddress.Primary)
                .WithMessage(MessageService.Constants.FieldRequired, "Email");
            RuleFor(x => x.InstitutionEmailAddress.Address)
                .Must(DoesNotHaveDuplicateEmailAddress)
                .When(y => !y.InstitutionEmailAddress.Address.IsNullOrWhiteSpace())
                .WithMessage(MessageService.Constants.UniqueEmailAddress, "Institutional");
            RuleFor(x => new KeyValuePair<string, bool>(x.VendorInfoIndividual.VendorId, true))
                .Must(DoesNotHaveDuplicateVendorId)
                .WithName("IndividualVendorId")
                .WithMessage(MessageService.Constants.DuplicateIndVendorId, "Individual");
            RuleFor(x => new KeyValuePair<string, bool>(x.VendorInfoInstitutional.VendorId, false))
                .Must(DoesNotHaveDuplicateVendorId)
                .WithName("InstitutionalVendorId")
                .WithMessage(MessageService.Constants.DuplicateInsVendorId, "Institutional");
            RuleFor(x => x.PersonalEmailAddress.Address)
               .Must(DoesNotHaveDuplicateEmailAddress)
               .When(y => !y.PersonalEmailAddress.Address.IsNullOrWhiteSpace())
                .WithMessage(MessageService.Constants.UniqueEmailAddress, "Personal");
            RuleFor(x => x.InstitutionEmailAddress.Address)
                .NotEqual(x => x.PersonalEmailAddress.Address)
                .When(y => !y.InstitutionEmailAddress.Address.IsNullOrWhiteSpace() || !y.PersonalEmailAddress.Address.IsNullOrWhiteSpace())
                .WithMessage(MessageService.Constants.UniqueEmailAddress, "Institutional and personal");
            RuleFor(x => x.UserDegrees).SetCollectionValidator(new UserDegreeValidator());
            RuleFor(x => x.UserDegrees)
                .Must(HasDegreeInformation)
                .When(y => y.GeneralInfo.ProfileTypeId == ProfileViewModel.ReviewerProfileTypeId)
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "Degree", FieldsetLabels.Degree);
            //Ensure there are not more than 2 spouses listed for alternate contact
            //Note: We are binding to IsUpdateUser since we seemingly need to bind to a 
            //property whose parent is the entire ViewModel.
            RuleFor(x => x.IsUpdateUser)
                .Must(NoMoreThanTwoSpouses)
                .WithMessage(MessageService.Constants.AlternateContactNoMoreThanOneSpouse);
            RuleFor(x => x.IsUpdateUser)
                .Must(NoMoreThanOneEmergencyContact)
                .WithMessage(MessageService.Constants.AlternateContactNoMoreThanOneEmergencyContact);
            RuleFor(x => x.IsUpdateUser)
                .Must(HaveAtLeastOneClient)
                .When(y => y.GeneralInfo.ProfileTypeId != ProfileViewModel.MisconductProfileTypeId && !y.IsMyProfile)
                .WithMessage(MessageService.Constants.ClientRequired);
            RuleFor(x => x.AlternateContactPersons).SetCollectionValidator(new UserAlternateContactValidator());
            RuleFor(x => x.Addresses)
                .SetCollectionValidator(new AddressValidator())
                .When(x => AnyNonBlankAddress(x.Addresses));
            RuleFor(x => x.UserPhones).SetCollectionValidator(new UserPhoneValidator());


            //
            // Role drop down rules if:
            //   the profile type is not misconduct
            // then
            //   there must be a selection.
            RuleFor(x => x.GeneralInfo.SystemRoleId)
                .NotEmpty()
                .When(RolesRequired)
                .WithMessage(MessageService.RoleRequiredMessage);
            //
            // Validation for W-9.  The user must select something (TRUE or FALSE) does not really 
            // matter which.
            //
            RuleFor(x => x.W9Addresses.W9Verified)
                .NotEmpty()
                .When(y => y.IsMyProfile && y.GeneralInfo.ProfileTypeId == ProfileViewModel.ReviewerProfileTypeId && y.W9Addresses.W9AddressExists)
                .WithMessage(MessageService.Constants.W9AccuracyRequired);
            //
            // Validation for Professional Affiliation
            //
            RuleFor(x => x.ProfessionalAffiliation.ProfessionalAffiliationId)
                .NotEmpty()
                .When(y => NotMisconductAndNotProspect(y))
                .WithMessage(MessageService.Constants.FieldRequired, "Org Type");
            RuleFor(x => x.ProfessionalAffiliation.Name)
                .NotEmpty()
                .When(y => NotMisconductAndNotProspect(y))
                .WithMessage(MessageService.Constants.FieldRequired, "Organization");
            RuleFor(x => x.ProfessionalAffiliation.Name).Length(0, UserInfoMetadata.GetInstitutionNameLength())
            .WithMessage(MessageService.Constants.MaxLengthExceeded, "Name", UserInfoMetadata.GetInstitutionNameLength());
            RuleFor(x => x.ProfessionalAffiliation.Position).Length(0, UserInfoMetadata.GetPositionNameLength())
            .WithMessage(MessageService.Constants.MaxLengthExceeded, "Name", UserInfoMetadata.GetInstitutionNameLength());
            //
            // Validate that Prospects supply a preferred email or preferred phone.
            //
            RuleFor(x => !x.IsUpdateUser).Must(IfProspectIsPreferredEmailOrPhoneSupplied).WithMessage(MessageService.Constants.ProspectRequiresEmailOrPhone);
        }
        /// <summary>
        /// Method which checks view model to ensure a preferred address was provided
        /// </summary>
        /// <param name="profileViewModel">ProfileViewModel object</param>
        /// <param name="arg2">not used</param>
        /// <returns>true if a preferred address is provided; otherwise false</returns>
        internal bool AtLeastOnePreferredAddress(ProfileViewModel profileViewModel, bool arg2)
        {
            return
                (profileViewModel.Addresses.Any(
                    x => x.IsPreferredAddress && PerformAddressValidation(x)));
        }
        /// <summary>
        /// Method which checks view model to ensure no more than one personal address is selected
        /// </summary>
        /// <param name="profileViewModel">>ProfileViewModel object</param>
        /// <param name="arg2">not used</param>
        /// <returns>true if more than one personal address is provided, otherwise false</returns>
        internal bool NoMoreThanOnePersonalAddress(ProfileViewModel profileViewModel, bool arg2)
        {
            return (profileViewModel.Addresses.Count(
                x => x.AddressTypeId == ProfileViewModel.PersonalAddressTypeId && x.IsDeletable == false) < 2);
        }

        private bool HaveAtLeastOneClient(ProfileViewModel model, bool arg2)
        {
            return model.UserClients.Any(x => x.ClientId > 0);
        }

        /// <summary>
        /// Method which checks view model to ensure degree information is provided
        /// </summary>
        /// <param name="model">The ProfileViewModel model.</param>
        /// <param name="userDegrees">User degrees</param>
        /// <returns>true if the model has degree info provided</returns>
        /// <remarks>Reviewer type only</remarks>
        public bool HasDegreeInformation(ProfileViewModel model, List<UserDegreeModel> userDegrees)
        {
            var validUserDegrees = userDegrees.Where(x => !x.IsDeletable).ToList();
            return model.GeneralInfo.DegreeNotApplicable ||
                (validUserDegrees.Count > 0 && validUserDegrees[0].DegreeId.HasValue);
        }

        /// <summary>
        /// Determines whether more than 2 alternate contacts of type spouse were supplied
        /// </summary>
        /// <param name="pmViewModel">ProfileViewModel</param>
        /// <param name="isUpdateUser">Generic bool property from base ViewModel</param>
        public bool NoMoreThanTwoSpouses(ProfileViewModel pmViewModel, bool isUpdateUser)
        {
            return pmViewModel.AlternateContactPersons.Count(x => x.UserAlternateContactTypeId == ProfileViewModel.SpouseAlternateContactTypeId && !x.IsDeleted()) < 2;
        }
        /// <summary>
        /// Determines whether there are no more than 1 emergency contact
        /// </summary>
        /// <param name="pmViewModel">ProfileViewModel</param>
        /// <param name="isUpdateUser">Generic bool property from base ViewModel</param>
        public bool NoMoreThanOneEmergencyContact(ProfileViewModel pmViewModel, bool isUpdateUser)
        {
            return pmViewModel.AlternateContactPersons.Count(x => x.UserAlternateContactTypeId == ProfileViewModel.EmergencyContactTypeId && !x.IsDeleted()) <= 1;
        }
        /// <summary>
        /// Determines that an email address is selected as preferred
        /// </summary>
        /// <param name="pmViewModel">ProfileViewModel</param>
        /// <param name="isUpdateUser">Generic bool property from base ViewModel</param>
        public bool AtleastOnePreferredEmailAddress(ProfileViewModel pmViewModel, bool isUpdateUser)
        {
            return !string.IsNullOrWhiteSpace(pmViewModel.InstitutionEmailAddress.Address) && pmViewModel.InstitutionEmailAddress.Primary ||
                    !string.IsNullOrWhiteSpace(pmViewModel.PersonalEmailAddress.Address) && pmViewModel.PersonalEmailAddress.Primary;
        }
        /// <summary>
        /// Determines that a primary phone number has been provided
        /// </summary>
        /// <param name="pmViewModel">ProfileViewModel</param>
        /// <param name="isUpdateUser">Generic bool property from base ViewModel</param>
        public bool AtLeastOnePreferredIfPhoneNumbersExist(ProfileViewModel pmViewModel, bool isUpdateUser)
        {
            return pmViewModel.UserPhones.Count(x => x.Primary && !x.Number.IsNullOrWhiteSpace()) > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pmViewModel"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool DoesNotHaveDuplicateEmailAddress(ProfileViewModel pmViewModel, string address)
        {
            bool r = UserProfileManagementController.IsDuplicateEmailAddress(address, pmViewModel.GeneralInfo.UserInfoId);
            return !r;
        }
        /// <param name="pmViewModel">The pm view model.</param>
        /// <param name="target">The target.</param>
        /// <returns>
        ///   <c>true</c> if [is duplicate individual vendor identifier] [the specified pm view model]; otherwise, <c>false</c>.
        /// </returns>
        public bool DoesNotHaveDuplicateVendorId(ProfileViewModel pmViewModel, KeyValuePair<string, bool> target)
        {
            bool r = UserProfileManagementController.IsDuplicateVendorId(target.Key, pmViewModel.GeneralInfo.UserInfoId, target.Value);
            return !r;
        }
        public bool RolesRequired(ProfileViewModel pmViewModel)
        {
            return pmViewModel.GeneralInfo.ProfileTypeId != ProfileViewModel.MisconductProfileTypeId && !pmViewModel.IsMyProfile;
        }
        /// <summary>
        /// Indicates if the profile is a Prospect.
        /// </summary>
        /// <param name="pmViewModel">ProfileViewModel</param>
        /// <returns>True if the ProfileViewModel is for a user of profile type Prospect; false otherwise</returns>
        public bool WhenProspect(ProfileViewModel pmViewModel)
        {
            return (pmViewModel.GeneralInfo.ProfileTypeId == ProfileViewModel.ProspectProfileTypeId);
        }
        /// <summary>
        /// Indicates if the profile is a Misconduct.
        /// </summary>
        /// <param name="pmViewModel">ProfileViewModel</param>
        /// <returns>True if the ProfileViewModel is for a user of profile type Misconduct; false otherwise</returns>
        public bool WhenMisconduct(ProfileViewModel pmViewModel)
        {
            return (pmViewModel.GeneralInfo.ProfileTypeId == ProfileViewModel.MisconductProfileTypeId);
        }
        /// <summary>
        /// Indicates if the profile is a not Misconduct or not a Prospect.
        /// </summary>
        /// <param name="pmViewModel">ProfileViewModel</param>
        /// <returns>True if the ProfileViewModel is for a user of profile type that is not Misconduct and not a Prospect; false otherwise</returns>
        public bool NotMisconductAndNotProspect(ProfileViewModel pmViewModel)
        {
            return (!WhenMisconduct(pmViewModel) & !WhenProspect(pmViewModel));
        }
        /// <summary>
        /// Determines if a Prospect profile type user has a preferred email or preferred address supplied
        /// </summary>
        /// <param name="pmViewModel">ProfileViewModel</param>
        /// <param name="isUpdateUser">Is the user an update user.  (Not referenced)</param>
        /// <returns>False if the user is a Prospect and has not supplied an email or address; true otherwise</returns>
        public bool IfProspectIsPreferredAddressOrEmailSupplied(ProfileViewModel pmViewModel, bool isUpdateUser)
        {
            bool result = (WhenProspect(pmViewModel)) ? (
                                                            (AtleastOnePreferredEmailAddress(pmViewModel, isUpdateUser)) ||
                                                            (AnyNonBlankAddress(pmViewModel.Addresses))
                                                        ) :
                                                            true;

            return result;
        }
        /// <summary>
        /// Ifs the prospect is preferred email or phone supplied.
        /// </summary>
        /// <param name="pmViewModel">The pm view model.</param>
        /// <param name="isUpdateUser">if set to <c>true</c> [is update user].</param>
        /// <returns></returns>
        public bool IfProspectIsPreferredEmailOrPhoneSupplied(ProfileViewModel pmViewModel, bool isUpdateUser)
        {
            bool result = (WhenProspect(pmViewModel)) ? (
                                                            (AtleastOnePreferredEmailAddress(pmViewModel, isUpdateUser)) ||
                                                            (AnyNonBlackPhoneNumber(pmViewModel.UserPhones))
                                                        ) :
                                                            true;

            return result;
        }
        /// <summary>
        /// FluentValidation Validator for AddressModel - PerformAddressValidation
        /// </summary>
        /// <param name="model">AddressInfoModel model</param>
        /// <returns>true if address validation should be performed on this address, false otherwise</returns>
        internal static bool PerformAddressValidation(AddressInfoModel model)
        {
            return !model.IsDeletable && (model.AddressTypeId.HasValue || !model.Address1.IsNullOrWhiteSpace() || !model.Address2.IsNullOrWhiteSpace() || !model.Address3.IsNullOrWhiteSpace() || !model.Address4.IsNullOrWhiteSpace() || !model.City.IsNullOrWhiteSpace() || model.StateId.HasValue || !model.Zip.IsNullOrWhiteSpace());
        }
        /// <summary>
        /// Any non blank address
        /// </summary>
        /// <param name="addresses">list of AddressInfoModel</param>
        /// <returns>true if any of the addresses have a non blank entry</returns>
        internal static bool AnyNonBlankAddress(List<AddressInfoModel> addresses)
        {
            return addresses.Count(x => PerformAddressValidation(x)) > 0;
        }
        /// <summary>
        /// Anies the non black phone number.
        /// </summary>
        /// <param name="phoneNumbers">The phone numbers.</param>
        /// <returns></returns>
        internal static bool AnyNonBlackPhoneNumber(List<PhoneNumberModel> phoneNumbers)
        {
            return phoneNumbers.Count(x => x.HasData()) > 0;
        }
    }
    /// <summary>
    /// FluentValidation Validator for UserDegree
    /// </summary>
    public class UserPhoneValidator : AbstractValidator<PhoneNumberModel>
    {
        public UserPhoneValidator()
        {
            //Phone number must be between 14 and 25 character if not international
            RuleFor(x => x.Number).Length(14, 25)
                .When(y => !y.International && !y.Number.IsNullOrWhiteSpace())
                .WithMessage(MessageService.Constants.PhoneLengthInvalid);
        }
    }
    /// <summary>
    /// FluentValidation Validator for UserDegree
    /// </summary>
    public class UserDegreeValidator : AbstractValidator<UserDegreeModel>
    {
        public UserDegreeValidator()
        {

        }
    }
    /// <summary>
    /// FluentValidation Validator for UserAlternateContactPersonModel
    /// </summary>
    public class UserAlternateContactValidator : AbstractValidator<UserAlternateContactPersonModel>
    {
        public UserAlternateContactValidator()
        {
            //First Name and Last Name required if Phone or Email provided
            RuleFor(x => x.FirstName).NotEmpty()
                .When(y => !y.IsDeletable && (y.AlternateContactPhone.Count(c => !c.Number.IsNullOrWhiteSpace()) > 0) || (!y.Email.IsNullOrWhiteSpace()) || (!y.LastName.IsNullOrWhiteSpace()))
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "First Name", FieldsetLabels.AlternateContact);
            RuleFor(x => x.LastName).NotEmpty()
                .When(y => !y.IsDeletable && (y.AlternateContactPhone.Count(c => !c.Number.IsNullOrWhiteSpace()) > 0 || !y.Email.IsNullOrWhiteSpace()) || (!y.FirstName.IsNullOrWhiteSpace()))
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "Last Name", FieldsetLabels.AlternateContact);
            RuleFor(x => x.UserAlternateContactTypeId).NotEmpty()
                .When(y => !y.IsDeletable && (!y.FirstName.IsNullOrWhiteSpace() || !y.LastName.IsNullOrWhiteSpace()))
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "Type", FieldsetLabels.AlternateContact);
            RuleFor(x => x.AlternateContactPhone)
                .SetCollectionValidator(new UserAlternateContactPhoneValidator());
            //Assistant must have an email and at least one phone
            RuleFor(x => x.Email).NotEmpty()
                //
                // If the assistant type is Assistant or a first name is supplied  or last name supplied then there must be either an email addressor phone number supplied
                //
                .When(y => !y.IsDeletable && ((y.UserAlternateContactTypeId == ProfileViewModel.AssistantAlternateContactId) || (!y.FirstName.IsNullOrWhiteSpace()) || (!y.LastName.IsNullOrWhiteSpace())) &&  !AtLeastOneAltPhoneNumber(y.AlternateContactPhone))
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "an email or at least one phone number", FieldsetLabels.AlternateContact);
        }
        /// <summary>
        /// Determines whether the user has at least one alternate contact phone number
        /// </summary>
        /// <param name="alternateContactPhones">Alternate contact phones</param>
        internal bool AtLeastOneAltPhoneNumber(List<UserAlternateContactPersonPhoneModel> alternateContactPhones)
        {
            return alternateContactPhones.Count(x => !x.Number.IsNullOrWhiteSpace()) > 0;
        }

    }
    /// <summary>
    /// FluentValidation Validator for UserAlternateContactPersonPhoneModel
    /// </summary>
    public class UserAlternateContactPhoneValidator : AbstractValidator<UserAlternateContactPersonPhoneModel>
    {
        public UserAlternateContactPhoneValidator()
        {
            RuleFor(x => x.PhoneTypeId).NotEmpty()
                .When(y => !y.Number.IsNullOrWhiteSpace())
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "Type", FieldsetLabels.AlternateContactPhone);
            RuleFor(x => x.Number).NotEmpty()
                .When(y => y.PhoneTypeId.HasValue)
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "Number", FieldsetLabels.AlternateContactPhone);
        }
    }


    /// <summary>
    /// FluentValidation Validator for AddressModel
    /// </summary>
    public class AddressValidator : AbstractValidator<AddressInfoModel>
    {
        public AddressValidator()
        {
            RuleFor(x => x.AddressTypeId).NotNull()
                .When(x => !x.IsDeletable && AnyNonblankFields(x))
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "Address Type", FieldsetLabels.Address);
            RuleFor(x => x.Address1).NotEmpty()
                .When(x => !x.IsDeletable && x.AddressTypeId == LookupService.LookupAddressTypePersonalId && AnyNonblankFields(x))
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "Address 1", FieldsetLabels.Address);
            RuleFor(x => x.Address1).NotEmpty()
                .When(x => !x.IsDeletable && x.AddressTypeId == LookupService.LookupAddressTypeOrganizationId && AnyNonblankFields(x))
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "Org Name", FieldsetLabels.Address);
            RuleFor(x => x.Address3).NotEmpty()
                .When(x => !x.IsDeletable && x.AddressTypeId == LookupService.LookupAddressTypeOrganizationId && AnyNonblankFields(x))
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "Address 1", FieldsetLabels.Address);
            RuleFor(x => x.City).NotEmpty()
                .When(x => !x.IsDeletable && AnyNonblankFields(x))
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "City", FieldsetLabels.Address);
            RuleFor(x => x.StateId).NotEmpty()
                .When(x => !x.IsDeletable && x.CountryId == ProfileViewModel.UsCountryId && AnyNonblankFields(x))
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "State", FieldsetLabels.Address);
            RuleFor(x => x.Zip)
                .Must(BeAValidUsZipLength)
                .When(x => !x.IsDeletable && x.CountryId == ProfileViewModel.UsCountryId && AnyNonblankFields(x))
                .WithMessage(MessageService.Constants.USZipLengthIncorrect);
            RuleFor(x => x.Zip)
                .Must(BeAValidInternationalZipLength)
                .When(x => !x.IsDeletable && x.CountryId != ProfileViewModel.UsCountryId && AnyNonblankFields(x))
                .WithMessage(MessageService.Constants.InternationalZipLengthIncorrect);
            RuleFor(x => x.CountryId).NotEmpty()
                .When(x => !x.IsDeletable)
                .WithMessage(MessageService.Constants.ConditionalFieldRequiredWithFieldset, "Country", FieldsetLabels.Address);
        }

        /// <summary>
        /// Validates zip against US zip requirements
        /// </summary>
        /// <param name="zip">The zip code</param>
        /// <returns>true if zip is either 5 or 9 characters</returns>
        public static bool BeAValidUsZipLength(string zip)
        {
            return !zip.IsNullOrWhiteSpace() && (zip.Length == 5 || zip.Length == 10);
        }
        /// <summary>
        /// Validates zip against international requirements
        /// </summary>
        /// <param name="zip">The zip code</param>
        /// <returns>true if zip is null or less than 10 characters</returns>
        public static bool BeAValidInternationalZipLength(string zip)
        {
            return zip.IsNullOrWhiteSpace() || zip.Length < 10;
        }
        /// <summary>
        /// Does any address field contain data
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true if any field contains data, false otherwise</returns>
        public static bool AnyNonblankFields(AddressInfoModel model)
        {
            return ProfileValidator.PerformAddressValidation(model);
        }
    }
    /// <summary>
    /// Contains message labels for fieldsets in the profile form
    /// </summary>
    public class FieldsetLabels
    {
        public const string Address = "Address";
        public const string AlternateContactPhone = "Alternate Contact Phone";
        public const string AlternateContact = "Alternate Contact";
        public const string EmailAddresses = "Email Address";
        public const string PhoneNumbers = "Phone Number";
        public const string Degree = "Education/Degree";
        public const string MilitaryRank = "Military Rank";
        public const string Client = "Client";
        public const string IndividualVendorId = "IndividualVendorId";
        public const string InstitutionalVendorId = "InstitutionalVendorId";

    }
    /// <summary>
    /// Contains message labels for fields in the profile form
    /// </summary>
    public class FieldLabels
    {
        public const string Status = "Military Status";
    }
    #endregion
}