using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.ConsumerManagement;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Bll
{
    public partial interface IConsumerManagementService
    {
        /// <summary>
        /// Gets nominee types.
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<int, string>> GetNomineeTypes();
        /// <summary>
        /// Maps nominee data to the nominee update model
        /// </summary>
        /// <param name="nomineeId">The Nominee identifier</param>
        /// <returns></returns>
        NomineeUpdateModel GetNomineeUpdateModel(int nomineeId);
        /// <summary>
        /// Maps nominee program data to the nominee program update model
        /// </summary>
        /// <param name="nomineeId">The Nominee identifier</param>
        /// <returns></returns>
        NomineeProgramUpdateModel GetNomineeProgramUpdateModel(int nomineeId);
        /// <summary>
        /// Maps nominee sponsor data to the nominee sponsor update model
        /// </summary>
        /// <param name="nomineeId">The Nominee identifier</param>
        /// <returns></returns>
        NomineeSponsorUpdateModel GetNomineeSponsorUpdateModel(int nomineeId);
        /// <summary>
        /// Gets nominees.
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="nomineeTypeId">Nominee type identifier</param>
        /// <param name="nominatingOrganization">Nominating organization</param>
        /// <param name="score">Score</param>
        /// <returns></returns>
        List<NomineeSearchModel> GetNominees(string firstName, string lastName, int? nomineeTypeId, string nominatingOrganization, int? score);
        /// <summary>
        /// Saves nominee.
        /// </summary>
        /// <param name="nomineeUpdateModel">The nominee update model</param>
        /// <param name="NomineeProgramUpdateModel">The nominee program update model</param>
        /// <param name="nomineeSponsorUpdateModel">The nominee sponsor update model</param>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        SaveNomineeUpdateModel SaveNominee(NomineeUpdateModel nomineeUpdateModel, NomineeProgramUpdateModel NomineeProgramUpdateModel, NomineeSponsorUpdateModel nomineeSponsorUpdateModel, int userId);
        /// <summary>
        /// Detects whether the email is unavailable.
        /// </summary>
        /// <param name="email">Email address</param>
        /// <param name="nomineeId">Nominee identifier</param>
        /// <returns></returns>
        bool EmailIsUnavailable(string email, int? nomineeId);
        /// <summary>
        /// Gets the active program years for the provided client program that fall within the last three years, includes future years if applicable
        /// </summary>
        /// <param name="clientProgramId">The client program identifier</param>
        /// <param name="editing">Indicates consumer editing state</param>
        /// <returns></returns>
        List<KeyValuePair<int, string>> GetConsumerProgramYears(int clientProgramId, bool editing);
        /// <summary>
        /// Gets the active nominating organizations
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<int, string>> GetNominatingOrganizationList(string prefstr, string ftype);


    }

    public partial class ConsumerManagementService : ServerBase, IConsumerManagementService
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConsumerManagementService(ICriteriaService criteriaService, IUserProfileManagementService userProfileManagementService)
        {
            UnitOfWork = new UnitOfWork();
            theUserProfileManagementService = userProfileManagementService;
            theCriteriaService = criteriaService;
        }
        protected ICriteriaService theCriteriaService { get; set; }
        #endregion
        protected IUserProfileManagementService theUserProfileManagementService { get; set; }
        /// <summary>
        /// Gets nominee types.
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetNomineeTypes()
        {
            var types = UnitOfWork.NomineeTypeRepository.GetAll()
                .ToList().ConvertAll(x => new KeyValuePair<int, string>
                (x.NomineeTypeId, x.NomineeType1));
            return types;
        }
        /// <summary>
        /// Gets a nominee by id
        /// </summary>
        /// <param name="nomineeId">The nominee identifier</param>
        /// <returns></returns>
        private Nominee GetNomineeById(int nomineeId)
        {
            return UnitOfWork.NomineeRepository.GetByID(nomineeId);
        }
        /// <summary>
        /// Gets a nominee program by id
        /// </summary>
        /// <param name="nomineeProgramId">The nominee program identifier</param>
        /// <returns></returns>
        private NomineeProgram GetNomineeProgramById(int nomineeProgramId)
        {
            return UnitOfWork.NomineeProgramRepository.GetByID(nomineeProgramId);
        }
        /// <summary>
        /// Gets a nominee sponsor by id
        /// </summary>
        /// <param name="nomineeSponsorId">The nominee sponsor identifier</param>
        /// <returns></returns>
        private NomineeSponsor GetNomineeSponsorById(int nomineeSponsorId)
        {
            return UnitOfWork.NomineeSponsorRepository.GetByID(nomineeSponsorId);
        }

        /// <summary>
        /// Maps nominee data to the nominee update model
        /// </summary>
        /// <param name="nomineeId">The Nominee identifier</param>
        /// <returns></returns>
        public NomineeUpdateModel GetNomineeUpdateModel(int nomineeId)
        {
            var nominee = GetNomineeById(nomineeId);
            var nomineePhones = nominee.NomineePhones.ToList();
            var phoneNumber1 = nomineePhones.FirstOrDefault();
            var phoneNumber2 = nomineePhones.Skip(1).FirstOrDefault();
            var lastModifiedByName = string.Empty;
            if (nominee.ModifiedBy != null)
            {
                //using SearchUser instead of GetUser here since there is no guarantee of a result based on legacy records
                var lastModifiedByUser = theUserProfileManagementService.SearchUser(string.Empty, string.Empty, string.Empty, string.Empty, nominee.ModifiedBy, string.Empty)
                .ModelList
                .ToList()
                .FirstOrDefault();
                if (lastModifiedByUser != null)
                {
                    lastModifiedByName = $"{lastModifiedByUser.FirstName} {lastModifiedByUser.LastName}";
                }
            }
            
            var nomineeUpdateModel = new NomineeUpdateModel()
            {
                UserId = nominee.UserId,
                UserInfoId = nominee.User?.UserInfoes.FirstOrDefault()?.UserInfoID,
                NomineeId = nominee.NomineeId,
                FirstName = nominee.FirstName,
                LastName = nominee.LastName,
                PrefixId = nominee.PrefixId,
                DateOfBirth = nominee.DOB,
                GenderId = nominee.GenderId,
                EthnicityId = nominee.EthnicityId,
                Address1 = nominee.Address1,
                Address2 = nominee.Address2,
                City = nominee.City,
                StateId = nominee.StateId,
                Zip = nominee.ZipCode,
                CountryId = nominee.CountryId,
                Email = nominee.Email,
                PhoneId1 = phoneNumber1?.NomineePhoneId,
                PhoneType1 = phoneNumber1?.PhoneTypeId,
                PhoneNumber1 = phoneNumber1?.Phone,
                PhoneId2 = phoneNumber2?.NomineePhoneId,
                PhoneType2 = phoneNumber2?.PhoneTypeId,
                PhoneNumber2 = phoneNumber2?.Phone,
                CreatedDate = nominee.CreatedDate,
                ModifiedDate = nominee.ModifiedDate,
                LastModifiedBy = lastModifiedByName
            };
            return nomineeUpdateModel;
        }
        /// <summary>
        /// Maps nominee program data to the nominee program update model
        /// </summary>
        /// <param name="nomineeId">The nominee identifier</param>
        /// <returns></returns>
        public NomineeProgramUpdateModel GetNomineeProgramUpdateModel(int nomineeId)
        {
            var nomineeProgramUpdateModel = new NomineeProgramUpdateModel();
            var nominee = GetNomineeById(nomineeId);
            var nomineeProgram = nominee.NomineePrograms.FirstOrDefault(y => y.PrimaryFlag);
            if (nomineeProgram != null)
            {
                var clientProgramId = nomineeProgram.ProgramYear?.ClientProgramId;
                nomineeProgramUpdateModel.NomineeProgramId = nomineeProgram.NomineeProgramid;
                nomineeProgramUpdateModel.ClientProgramId = clientProgramId;
                nomineeProgramUpdateModel.ProgramYearId = nomineeProgram.ProgramYear.ProgramYearId;
                nomineeProgramUpdateModel.DiseaseSite = nomineeProgram.DiseaseSite;
                nomineeProgramUpdateModel.AffectedId = nomineeProgram.NomineeAffectedId;
                nomineeProgramUpdateModel.Score = nomineeProgram.Score;
                nomineeProgramUpdateModel.Comments = nomineeProgram.Comments;
                nomineeProgramUpdateModel.NomineeTypeId = nomineeProgram.NomineeTypeId;
                nomineeProgramUpdateModel.ProgramFiscalYears = GetConsumerProgramYears((int)clientProgramId, true);
            }
            return nomineeProgramUpdateModel;
        }
        /// <summary>
        /// Maps nominee sponsor data to the nominee sponsor update model
        /// </summary>
        /// <param name="nomineeId">The nominee identifier</param>
        /// <returns></returns>
        public NomineeSponsorUpdateModel GetNomineeSponsorUpdateModel(int nomineeId)
        {
            var nomineeSponsorUpdateModel = new NomineeSponsorUpdateModel();
            var nominee = GetNomineeById(nomineeId);
            var nomineeProgram = nominee.NomineePrograms.FirstOrDefault(y => y.PrimaryFlag);
            if (nomineeProgram != null)
            {
                var nomineeSponsor = nomineeProgram.NomineeSponsor;
                if (nomineeSponsor != null)
                {
                    var sponsorPhones = nomineeSponsor.NomineeSponsorPhones.ToList();
                    var sponsorPhoneNumber1 = sponsorPhones.FirstOrDefault();
                    var sponsorPhoneNumber2 = sponsorPhones.Skip(1).FirstOrDefault();
                    nomineeSponsorUpdateModel.NominatorId = nomineeSponsor.NomineeSponsorId;
                    nomineeSponsorUpdateModel.NominatingOrganization = nomineeSponsor.Organization;
                    nomineeSponsorUpdateModel.NominatingOrganizationId = nomineeSponsor.OrganizationId;
                    nomineeSponsorUpdateModel.NominatorAddress1 = nomineeSponsor.Address1;
                    nomineeSponsorUpdateModel.NominatorAddress2 = nomineeSponsor.Address2;
                    nomineeSponsorUpdateModel.NominatorCity = nomineeSponsor.City;
                    nomineeSponsorUpdateModel.NominatorStateId = nomineeSponsor.StateId;
                    nomineeSponsorUpdateModel.NominatorZip = nomineeSponsor.ZipCode;
                    nomineeSponsorUpdateModel.NominatorCountryId = nomineeSponsor.CountryId;
                    nomineeSponsorUpdateModel.NominatorFirstName = nomineeSponsor.FirstName;
                    nomineeSponsorUpdateModel.NominatorLastName = nomineeSponsor.LastName;
                    nomineeSponsorUpdateModel.NominatorTitle = nomineeSponsor.Title;
                    nomineeSponsorUpdateModel.NominatorEmail = nomineeSponsor.Email;
                    nomineeSponsorUpdateModel.NominatorPhoneId1 = sponsorPhoneNumber1?.NomineeSponsorPhoneId;
                    nomineeSponsorUpdateModel.NominatorPhoneTypeId1 = sponsorPhoneNumber1?.PhoneTypeId;
                    nomineeSponsorUpdateModel.NominatorPhoneNumber1 = sponsorPhoneNumber1?.Phone;
                    nomineeSponsorUpdateModel.NominatorPhoneId2 = sponsorPhoneNumber2?.NomineeSponsorPhoneId;
                    nomineeSponsorUpdateModel.NominatorPhoneTypeId2 = sponsorPhoneNumber2?.PhoneTypeId;
                    nomineeSponsorUpdateModel.NominatorPhoneNumber2 = sponsorPhoneNumber2?.Phone;
                }
            }
            return nomineeSponsorUpdateModel;
        }
        
        /// <summary>
        /// Gets nominees.
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="nomineeTypeId">Nominee type identifier</param>
        /// <param name="nominatingOrganization">Nominating organization</param>
        /// <param name="score">Score</param>
        /// <returns></returns>
        public List<NomineeSearchModel> GetNominees(string firstName, string lastName, int? nomineeTypeId, string nominatingOrganization, int? score)
        {
            var models = new List<NomineeSearchModel>();
            var nominees = UnitOfWork.NomineeRepository.Select()
                .Where(x => x.NomineePrograms.Count(y =>
                (nomineeTypeId == null || y.NomineeTypeId == nomineeTypeId) &&
                (score == null || y.Score == score) &&
                (nominatingOrganization == string.Empty ||
                (nominatingOrganization != string.Empty && y.NomineeSponsor.Organization.Contains(nominatingOrganization)))) > 0);
            if (!string.IsNullOrWhiteSpace(lastName))
                nominees = nominees.Where(x => x.LastName.StartsWith(lastName));
            if (!string.IsNullOrWhiteSpace(firstName))
                nominees = nominees.Where(x => x.FirstName.StartsWith(firstName));
            models = nominees.Select(
                x => new NomineeSearchModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Type = x.NomineePrograms.FirstOrDefault(y => y.PrimaryFlag) != null ?
                        x.NomineePrograms.FirstOrDefault(y => y.PrimaryFlag).NomineeType.NomineeType1 : null,
                    NominatingOrganization = x.NomineePrograms.FirstOrDefault(y => y.PrimaryFlag) != null ?
                        x.NomineePrograms.FirstOrDefault(y => y.PrimaryFlag).NomineeSponsor.Organization : null,
                    Program = x.NomineePrograms.FirstOrDefault(y => y.PrimaryFlag) != null ?
                        x.NomineePrograms.FirstOrDefault(y => y.PrimaryFlag).ProgramYear.ClientProgram.ProgramAbbreviation : null,
                    FiscalYear = x.NomineePrograms.FirstOrDefault(y => y.PrimaryFlag) != null ?
                        x.NomineePrograms.FirstOrDefault(y => y.PrimaryFlag).ProgramYear.Year : null,
                    Score = x.NomineePrograms.FirstOrDefault(y => y.PrimaryFlag) != null ?
                        x.NomineePrograms.FirstOrDefault(y => y.PrimaryFlag).Score : null,
                    UserId = x.UserId,
                    UserInfoId = x.User != null ?
                        (int?)x.User.UserInfoes.FirstOrDefault().UserInfoID : null,
                    NomineeId = x.NomineeId
                }).OrderBy(o1 => o1.LastName).ThenBy(o2 => o2.FirstName)
                .ThenByDescending(o3 => o3.FiscalYear).ThenBy(o4 => o4.Program).ToList();
            return models;
        }
        /// <summary>
        /// Saves nominee.
        /// </summary>
        /// <param name="nomineeUpdateModel">The nominee update model</param>
        /// <param name="NomineeProgramUpdateModel">The nominee program update model</param>
        /// <param name="nomineeSponsorUpdateModel">The nominee sponsor update model</param>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        public SaveNomineeUpdateModel SaveNominee(NomineeUpdateModel nomineeUpdateModel, NomineeProgramUpdateModel nomineeProgramUpdateModel, NomineeSponsorUpdateModel nomineeSponsorUpdateModel, int userId)
        {
            var saveNomineeUpdateModel = new SaveNomineeUpdateModel();
            var nomineeId = nomineeUpdateModel.NomineeId;
            if (nomineeId == null)
            {
                //Add nominee
                var nominee = MapNomineeFromUpdateModel(new Nominee(), new List<NomineePhone>(), nomineeUpdateModel);
                nominee.NomineePhones.Add(new NomineePhone()
                {
                    Phone = nomineeUpdateModel.PhoneNumber1,
                    PhoneTypeId = nomineeUpdateModel.PhoneType1
                });
                nominee.NomineePhones.Add(new NomineePhone()
                {
                    Phone = nomineeUpdateModel.PhoneNumber2,
                    PhoneTypeId = nomineeUpdateModel.PhoneType2
                });

                nominee = UnitOfWork.NomineeRepository.Add(nominee, userId);
                nomineeId = nominee.NomineeId;

                var nomineeSponsor = MapNomineeSponsorFromUpdateModel(new NomineeSponsor(), new List<NomineeSponsorPhone>(), nomineeSponsorUpdateModel);
                nomineeSponsor.NomineeSponsorPhones.Add(new NomineeSponsorPhone()
                {
                    Phone = nomineeSponsorUpdateModel.NominatorPhoneNumber1,
                    PhoneTypeId = nomineeSponsorUpdateModel.NominatorPhoneTypeId1
                });
                nomineeSponsor.NomineeSponsorPhones.Add(new NomineeSponsorPhone()
                {
                    Phone = nomineeSponsorUpdateModel.NominatorPhoneNumber2,
                    PhoneTypeId = nomineeSponsorUpdateModel.NominatorPhoneTypeId2
                });

                nomineeSponsor = UnitOfWork.NomineeSponsorRepository.Add(nomineeSponsor, userId);

                var nomineeProgram = MapNomineeProgramFromUpdateModel(new NomineeProgram(), nomineeProgramUpdateModel);
                nomineeProgram.NomineeId = (int)nomineeId;
                nomineeProgram.NomineeSponsorId = nomineeSponsor.NomineeSponsorId;
                nomineeProgram.PrimaryFlag = true;
                nomineeProgram = UnitOfWork.NomineeProgramRepository.Add(nomineeProgram, userId);
                saveNomineeUpdateModel.StatusMessage = $"Consumer record for {nominee.FirstName} {nominee.LastName} added successfully.";
            }
            else
            {
                //Update nominee
                var nominee = GetNomineeById((int)nomineeId);
                var nomineePhones = new List<NomineePhone>();
                var nomineePhone1 = nominee.NomineePhones.FirstOrDefault(x => x.NomineePhoneId == nomineeUpdateModel.PhoneId1);
                if (nomineePhone1 != null)
                {
                    nomineePhone1.PhoneTypeId = nomineeUpdateModel.PhoneType1;
                    nomineePhone1.Phone = nomineeUpdateModel.PhoneNumber1;
                    Helper.UpdateModifiedFields(nomineePhone1, userId);
                    nomineePhones.Add(nomineePhone1);
                }
                var nomineePhone2 = nominee.NomineePhones.FirstOrDefault(x => x.NomineePhoneId == nomineeUpdateModel.PhoneId2);
                if (nomineePhone2 != null)
                {
                    nomineePhone2.PhoneTypeId = nomineeUpdateModel.PhoneType2;
                    nomineePhone2.Phone = nomineeUpdateModel.PhoneNumber2;
                    Helper.UpdateModifiedFields(nomineePhone2, userId);
                    nomineePhones.Add(nomineePhone2);
                }

                nominee = MapNomineeFromUpdateModel(nominee, nomineePhones, nomineeUpdateModel);

                var nomineeProgram = GetNomineeProgramById((int)nomineeProgramUpdateModel.NomineeProgramId);
                nomineeProgram = MapNomineeProgramFromUpdateModel(nomineeProgram, nomineeProgramUpdateModel);
                Helper.UpdateModifiedFields(nomineeProgram, userId);
                UnitOfWork.NomineeProgramRepository.Update(nomineeProgram);

                var nomineeSponsor = GetNomineeSponsorById((int)nomineeSponsorUpdateModel.NominatorId);
                var nomineeSponsorPhones = new List<NomineeSponsorPhone>();
                var nomineeSponsorPhone1 = nomineeSponsor.NomineeSponsorPhones.FirstOrDefault(x => x.NomineeSponsorPhoneId == nomineeSponsorUpdateModel.NominatorPhoneId1);
                if (nomineeSponsorPhone1 != null)
                {
                    nomineeSponsorPhone1.PhoneTypeId = nomineeSponsorUpdateModel.NominatorPhoneTypeId1;
                    nomineeSponsorPhone1.Phone = nomineeSponsorUpdateModel.NominatorPhoneNumber1;
                    Helper.UpdateModifiedFields(nomineeSponsorPhone1, userId);
                    nomineeSponsorPhones.Add(nomineeSponsorPhone1);
                }
                
                var nomineeSponsorPhone2 = nomineeSponsor.NomineeSponsorPhones.FirstOrDefault(x => x.NomineeSponsorPhoneId == nomineeSponsorUpdateModel.NominatorPhoneId2);
                if (nomineeSponsorPhone2 != null)
                {
                    nomineeSponsorPhone2.PhoneTypeId = nomineeSponsorUpdateModel.NominatorPhoneTypeId2;
                    nomineeSponsorPhone2.Phone = nomineeSponsorUpdateModel.NominatorPhoneNumber2;
                    Helper.UpdateModifiedFields(nomineeSponsorPhone2, userId);
                    nomineeSponsorPhones.Add(nomineeSponsorPhone2);
                }
                nomineeSponsor = MapNomineeSponsorFromUpdateModel(nomineeSponsor, nomineeSponsorPhones, nomineeSponsorUpdateModel);
                Helper.UpdateModifiedFields(nomineeSponsor, userId);
                UnitOfWork.NomineeSponsorRepository.Update(nomineeSponsor);

                var reviewerProfileStatusMessage = string.Empty;
                if (nomineeProgram.NomineeTypeId == LookupService.LookupNomineeTypeSelectedNovice)
                {
                    var user = nominee.User;
                    if (user == null)
                    {
                        var results = theUserProfileManagementService.SearchUser(string.Empty, string.Empty, nominee.Email)
                            .ModelList
                            .ToList();
                        var match = results.FirstOrDefault(x => x.EmailAddress?.ToLower() == nominee.Email?.ToLower() || x.SecondaryEmailAddress?.ToLower() == nominee.Email?.ToLower());
                        if (match == null)
                        {
                            var nomineeUserId = CreateUserProfileUsingCMData(nominee, nomineeUpdateModel, nomineeProgram, nomineeSponsor, userId);
                            nominee.UserId = nomineeUserId;
                            reviewerProfileStatusMessage = $" Reviewer account for {nominee.FirstName} {nominee.LastName} has been created.";
                        }
                        else
                        {
                            saveNomineeUpdateModel.StatusMessage = "Email in Use";
                            saveNomineeUpdateModel.StatusObject = new
                            {
                                UserProfileName = $"{match.FirstName} {match.LastName}",
                                UserEmail = match.EmailAddress,
                                UserId = match.UserId,
                                UserInfoId = match.UserInfoId
                            };
                            return saveNomineeUpdateModel;
                        }
                    }
                }
                Helper.UpdateModifiedFields(nominee, userId);
                UnitOfWork.NomineeRepository.Update(nominee);
                saveNomineeUpdateModel.StatusMessage = $"Consumer record for {nominee.FirstName} {nominee.LastName} updated successfully.{reviewerProfileStatusMessage}";
            }
            UnitOfWork.Save();

            return saveNomineeUpdateModel;
        }
        /// <summary>
        /// Creates a user profile for a consumer
        /// </summary>
        /// <param name="nominee">The nominee</param>
        /// <param name="nomineeUpdateModel">The nominee update model</param>
        /// <param name="nomineeProgram">The nominee program</param>
        /// <param name="nomineeSponsor">The nominee sponsor</param>
        /// <param name="userId">The user identifier</param>
        /// <returns></returns>
        private int CreateUserProfileUsingCMData(Nominee nominee, NomineeUpdateModel nomineeUpdateModel,
            NomineeProgram nomineeProgram, NomineeSponsor nomineeSponsor, int userId)
        {
            IGeneralInfoModel generalInfoModel = null;
            int? profileId = null;
            var profileType = UnitOfWork.ProfileTypeRepository.GetByID(LookupService.LookupProfileTypeIdForReviewer);
            var systemRole = UnitOfWork.SystemRoleRepository.GetByID(LookupService.LookupSystemRoleReviewerId);
            generalInfoModel = new GeneralInfoModel(nominee.FirstName, nominee.MiddleName, nominee.LastName, string.Empty, string.Empty, string.Empty,
            nominee.PrefixId, string.Empty, DateTime.Now, nominee.EthnicityId, nominee.GenderId, profileType.ProfileTypeId, 0,
                0, profileType.ProfileTypeName, systemRole.SystemRoleId, null, systemRole.SortOrder, null, true, string.Empty, false);

            var personalEmailAddress = new EmailAddressModel(0, LookupService.LookupEmailAddressTypePersonalId, nominee.Email, true);
            var addressList = new List<AddressInfoModel>()
            {
                new AddressInfoModel() {
                    Address1 = nominee.Address1,
                    Address2 = nominee.Address2,
                    City = nominee.City,
                    StateId = nominee.StateId,
                    Zip = nominee.ZipCode,
                    CountryId = nominee.CountryId,
                    AddressTypeId = LookupService.LookupAddressTypePersonalId
                }
            };
            var professionalAffiliationModel = new ProfessionalAffiliationModel(LookupService.LookupProfessionalAffiliationNominatingOrganizationId, nomineeSponsor.Organization, null, null);
            var phoneNumber1IsPrimary = nomineeUpdateModel.PhoneNumber1 != null;
            var phoneNumberModelList = new List<PhoneNumberModel>()
            {
                new PhoneNumberModel(0, nomineeUpdateModel.PhoneType1, nomineeUpdateModel.PhoneNumber1, phoneNumber1IsPrimary, string.Empty, false),
                new PhoneNumberModel(0, nomineeUpdateModel.PhoneType2, nomineeUpdateModel.PhoneNumber2, !phoneNumber1IsPrimary, string.Empty, false)
            };
            var nomineeClientProgram = nomineeProgram.ProgramYear.ClientProgram;
            var userProfileClientModels = new List<UserProfileClientModel>()
            {
                new UserProfileClientModel(0, 0, nomineeClientProgram.ClientId, nomineeClientProgram.ProgramAbbreviation, nomineeClientProgram.ProgramDescription, false, true)
            };

            //Non-CM parameters
            var websiteModelList = new List<WebsiteModel>();
            var institutionEmailAddress = new EmailAddressModel();
            var w9AddressModel = new W9AddressModel();
            var phoneTypeList = new List<WebModels.Lists.IListEntry>();
            var alternateContactTypeList = new List<WebModels.Lists.IListEntry>();
            var userAlternateContactPersonModelList = new List<UserAlternateContactPersonModel>();
            var userDegreeModelList = new List<UserDegreeModel>();
            var userMilitaryRankModel = new UserMilitaryRankModel();
            var userMilitaryStatusModel = new UserMilitaryStatusModel();
            var userVendorModelIndividual = new UserVendorModel();
            var userVendorModelInstitutional = new UserVendorModel();

            theUserProfileManagementService.CreateProfile(generalInfoModel, websiteModelList, institutionEmailAddress, personalEmailAddress, addressList,
            professionalAffiliationModel, w9AddressModel, phoneTypeList, alternateContactTypeList, phoneNumberModelList, userAlternateContactPersonModelList,
            userDegreeModelList, userMilitaryRankModel, userMilitaryStatusModel, null, userVendorModelIndividual, userVendorModelInstitutional, userProfileClientModels, userId, false, ref profileId);

            return (int)profileId;
        }
        private Nominee MapNomineeFromUpdateModel(Nominee nominee, List<NomineePhone> nomineePhones, NomineeUpdateModel nomineeUpdateModel)
        {
            nominee.FirstName = nomineeUpdateModel.FirstName;
            nominee.LastName = nomineeUpdateModel.LastName;
            nominee.PrefixId = nomineeUpdateModel.PrefixId;
            nominee.DOB = nomineeUpdateModel.DateOfBirth;
            nominee.GenderId = nomineeUpdateModel.GenderId;
            nominee.EthnicityId = nomineeUpdateModel.EthnicityId;
            nominee.Address1 = nomineeUpdateModel.Address1;
            nominee.Address2 = nomineeUpdateModel.Address2;
            nominee.City = nomineeUpdateModel.City;
            nominee.StateId = nomineeUpdateModel.StateId;
            nominee.ZipCode = nomineeUpdateModel.Zip;
            nominee.CountryId = nomineeUpdateModel.CountryId;
            nominee.Email = nomineeUpdateModel.Email;
            nominee.NomineePhones = nomineePhones;
            nominee.UserId = nomineeUpdateModel.UserId;
            return nominee;
        }
        private NomineeSponsor MapNomineeSponsorFromUpdateModel(NomineeSponsor nomineeSponsor, List<NomineeSponsorPhone> nomineeSponsorPhones, NomineeSponsorUpdateModel nomineeSponsorUpdateModel)
        {
            nomineeSponsor.Organization = nomineeSponsorUpdateModel.NominatingOrganization;
            nomineeSponsor.OrganizationId = nomineeSponsorUpdateModel.NominatingOrganizationId;
            nomineeSponsor.FirstName = nomineeSponsorUpdateModel.NominatorFirstName;
            nomineeSponsor.LastName = nomineeSponsorUpdateModel.NominatorLastName;
            nomineeSponsor.Email = nomineeSponsorUpdateModel.NominatorEmail;
            nomineeSponsor.Title = nomineeSponsorUpdateModel.NominatorTitle;
            nomineeSponsor.Address1 = nomineeSponsorUpdateModel.NominatorAddress1;
            nomineeSponsor.Address2 = nomineeSponsorUpdateModel.NominatorAddress2;
            nomineeSponsor.City = nomineeSponsorUpdateModel.NominatorCity;
            nomineeSponsor.StateId = nomineeSponsorUpdateModel.NominatorStateId;
            nomineeSponsor.ZipCode = nomineeSponsorUpdateModel.NominatorZip;
            nomineeSponsor.CountryId = nomineeSponsorUpdateModel.NominatorCountryId;
            nomineeSponsor.NomineeSponsorPhones = nomineeSponsorPhones;
            return nomineeSponsor;
        }
        private NomineeProgram MapNomineeProgramFromUpdateModel(NomineeProgram nomineeProgram, NomineeProgramUpdateModel nomineeProgramUpdateModel)
        {
            nomineeProgram.ProgramYearId = nomineeProgramUpdateModel.ProgramYearId;
            nomineeProgram.NomineeTypeId = nomineeProgramUpdateModel.NomineeTypeId;
            nomineeProgram.DiseaseSite = nomineeProgramUpdateModel.DiseaseSite;
            nomineeProgram.NomineeAffectedId = nomineeProgramUpdateModel.AffectedId;
            nomineeProgram.Score = nomineeProgramUpdateModel.Score;
            nomineeProgram.Comments = nomineeProgramUpdateModel.Comments;
            return nomineeProgram;
        }
        /// <summary>
        /// Detects whether the email is unavailable.
        /// </summary>
        /// <param name="email">Email address</param>
        /// <param name="nomineeId">Nominee identifier</param>
        /// <returns></returns>
        public bool EmailIsUnavailable(string email, int? nomineeId)
        {
            var nominees = UnitOfWork.NomineeRepository.Select().Where(x => x.Email == email);
            if (nomineeId != null)
            {
                nominees = nominees.Where(x => x.NomineeId != nomineeId);
            }
            return nominees.Any();
        }

        public List<KeyValuePair<int, string>> GetConsumerProgramYears(int clientProgramId, bool editing)
        {
            var programYears = theCriteriaService.GetAllProgramYears(clientProgramId).ModelList
                .OrderByDescending(x => x.Year)
                .ToList()
                .ConvertAll(x => new KeyValuePair<int, string>(x.ProgramYearId, x.Year));
            if (!editing)
            {
                var minYear = GlobalProperties.P2rmisDateTimeNow.AddYears(-3).Year;
                var maxYear = GlobalProperties.P2rmisDateTimeNow.AddYears(1).Year;
                programYears = programYears.Where(x => {
                    var intYear = Convert.ToInt32(x.Value);
                    return intYear >= minYear && intYear <= maxYear;
                }).ToList();
            }
            return programYears;
        }

        /// <summary>
        /// Gets the active nominating organizations
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetNominatingOrganizationList(string prefstr, string ftype)
        {
            var nomOrgs = UnitOfWork.NomineeSponsorRepository.NominatingOrganizations(prefstr, ftype);

            return nomOrgs; 
        }

    }
}
