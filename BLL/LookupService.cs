using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.MeetingManagement;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// The LookupService provides services to return collections of model
    /// data from entity objects intended for population of drop downs or 
    /// other fixed data sets
    /// </summary>
    public class LookupService : ServerBase, ILookupService
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public LookupService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion
        #region Services
        /// <summary>
        /// Retrieves vales for 'Gender' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListGender()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.GenderRepository.GetAll();
            container.ModelList = this.FillDescendingContainer<Gender>(results, x => x.GenderId, x => x.Gender1, x => x.Gender1);

            return container;
        }
        /// <summary>
        /// Retrieves vales for 'Ethnicity' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListEthnicity()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.EthnicityRepository.GetAll();
            container.ModelList = this.FillOrderedContainer<Ethnicity>(results, x => x.EthnicityId, x => x.EthnicityLabel, x => x.EthnicityLabel);

            return container;
        }

        /// <summary>
        /// Retrieves vales for Contract status drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListContractStatus()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.ContractStatusRepository.GetAll();
            container.ModelList = this.FillOrderedContainer<ContractStatus>(results, x => x.ContractStatusId, x =>x.ActionLabel, x => x.StatusLabel);

            return container;
        }



        /// <summary>
        /// Retrieves vales for 'Prefix' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListPrefix()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.PrefixRepository.GetAll();
            container.ModelList = FillContainer<Prefix>(results, x => x.PrefixId, x => x.PrefixName);
            
            return container;
        }
        /// <summary>
        /// Retrieves vales for 'PhoneType' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListPhoneType()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.PhoneTypeRepository.GetAll();
            container.ModelList = FillOrderedContainer<PhoneType>(results, x => x.PhoneTypeId, x => x.PhoneType1, x => x.SortOrder);

            return container;
        }
        /// <summary>
        /// Retrieves vales for 'State' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListStateByName()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.StateRepository.GetAll();
            container.ModelList = this.FillOrderedContainer<State>(results, x => x.StateId, x => x.StateName, x => x.StateName);

            return container;
        }
        /// <summary>
        /// Retrieves vales for 'Country' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListCountryByName()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.CountryRepository.GetAll();
            container.ModelList = this.FillOrderedContainer<Country>(results, x => x.CountryId, x => x.CountryName, x => x.CountryName);

            return container;
        }
        /// <summary>
        /// List US and Canada as countries
        /// </summary>
        /// <returns></returns>
        public Container<IListEntry> ListCountryUsCanada()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.CountryRepository.GetAll().Where(x => x.CountryAbbreviation == "US" ||
                x.CountryAbbreviation == "CA");
            container.ModelList = this.FillOrderedContainer<Country>(results, x => x.CountryId, x => x.CountryName, x => x.CountryName);

            return container;
        }
        /// <summary>
        /// Retrieves vales for 'Military Service' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListMilitaryService()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.MilitaryRankRepository.GetAll();
            var filteredResults = results.GroupBy(x => x.Service).Select(x => x.FirstOrDefault());
            container.ModelList = this.FillOrderedContainer<MilitaryRank>(filteredResults, x => x.MilitaryRankId, x => x.Service, x => x.Service);

            return container;
        }
        /// <summary>
        /// Retrieves vales for 'Military Rank' drop downs
        /// </summary>
        /// <param name="service">Service to list ranks</param>
        /// <returns>Container of IListEntry models</returns>
        /// <exception cref="ArgumentException">Thrown if service is null or empty</exception>
        public Container<IListEntry> ListMilitaryRanks(string service)
        {
            ValidateParameter(service, "LookupService.ListMilitaryRanks", "service");

            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.MilitaryRankRepository.GetAll();
            var filteredResults = results.Where(x => x.Service == service);
            container.ModelList = FillOrderedContainer<MilitaryRank>(filteredResults, x => x.MilitaryRankId, x => x.MilitaryRankName, x => x.SortOrder);

            return container;
        }
        /// <summary>
        /// Retrieves vales for 'Degree' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListDegree()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.DegreeRepository.GetAll();
            container.ModelList = FillOrderedContainer<Degree>(results, x => x.DegreeId, x => x.DegreeName, x => x.DegreeName);

            return container;
        }
        /// <summary>
        /// Retrieves vales for 'ProfileType' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListProfileType()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.ProfileTypeRepository.GetAll();
            results = FilterNonAssignableProfileTypes(results);
            container.ModelList = this.FillOrderedContainer<ProfileType>(results, x => x.ProfileTypeId, x => x.ProfileTypeName, x => x.SortOrder);

            return container;
        }
        /// <summary>
        /// Retrieves vales for 'MilitaryStatusType' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListMilitaryStatusType()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.MilitaryStatusTypeRepository.GetAll();
            container.ModelList = this.FillOrderedContainer<MilitaryStatusType>(results, x => x.MilitaryStatusTypeId, x => x.StatusType, x => x.SortOrder);

            return container;
        }
        /// <summary>
        /// Retrieves vales for 'AddressType' drop downs for organizational or personal
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListOrganizationalPersonalAddressType()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.AddressTypeRepository.GetAll().Where(x => x.AddressTypeId == LookupAddressTypeOrganizationId || x.AddressTypeId == LookupAddressTypePersonalId);
            container.ModelList = FillContainer<AddressType>(results, x => x.AddressTypeId, x => x.AddressTypeName);

            return container;
        }
        /// <summary>
        /// Retrieves vales for 'AlternateContactTypes' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListAlternateContactType()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.AlternateContactTypeRepository.GetAll();
            container.ModelList = this.FillOrderedContainer<AlternateContactType>(results, x => x.AlternateContactTypeId, x => x.AlternateContactType1, x => x.SortOrder);

            return container;
        }
        /// <summary>
        /// Retrieves values for the 'Recovery Question' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListRecoveryQuestions()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.RecoveryQuestionRepository.GetAll();
            container.ModelList = this.FillContainer<RecoveryQuestion>(results, x => x.RecoveryQuestionId , x => x.QuestionText);

            return container;
        }
        /// <summary>
        /// Retrieves(constructs) values for the 'De-activate Account' drop down on the
        /// manage account modal.
        /// </summary>
        /// <returns></returns>
        public Container<IListEntry> ListDeActivateAccount()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            List<IListEntry> list = new List<IListEntry>();

            list.Add(MakeDeActivateAccountListEntry(AccountStatusReason.Indexes.Ineligible));
            list.Add(MakeDeActivateAccountListEntry(AccountStatusReason.Indexes.AccountClosed));

            container.ModelList = list;
            return container;
        }
        /// <summary>
        /// Retrieves SystemRoles for the specified profile type.  
        /// </summary>
        /// <param name="targetProfileTypeId">Target ProfileType entity identifier</param>
        /// <param name="targetSystemPriorityOrder">Target user's role priority order.</param>
        /// <param name="userProfileTypeId">Current user ProfileType entity identifier</param>
        /// <param name="userSystemRolePriorityOrder">User role hierarchy order value</param>
        /// <returns>Container of IListEntry models; where model contains: WhatWasLookupRole identifier, WhatWasLookupRole value</returns>
        public Container<IListEntry> ListProfileTypesRoles(int? targetProfileTypeId, int? targetSystemPriorityOrder, int userProfileTypeId, int? userSystemRolePriorityOrder)
        {
            Container<IListEntry> container = new Container<IListEntry>();
            //
            // Get a list of roles that are in the target user profile
            //
            var matchingProfileTypes = UnitOfWork.ProfileTypeRoleRepository.Get(x => x.ProfileTypeId == targetProfileTypeId.Value).Select(s => new { id = s.SystemRoleId }).ToList();
            //
            // Consolidate them into a list of ids.
            //
            var profileTypeRoleIds = new List<int>();
            matchingProfileTypes.ForEach(x => { if (x.id.HasValue) { profileTypeRoleIds.Add(x.id.Value); } });
            //
            // Now figure out if we need to filter the roles.  We need to filter the roles if the profile types are the same
            //
            var results = ((targetProfileTypeId.HasValue) && (userProfileTypeId == targetProfileTypeId.Value)) ?
                          UnitOfWork.SystemRoleRepository.Get(x => profileTypeRoleIds.Contains(x.SystemRoleId)).Where(x => ((targetSystemPriorityOrder.HasValue) & (x.SystemPriorityOrder == targetSystemPriorityOrder)) || (x.SystemPriorityOrder >= userSystemRolePriorityOrder)).ToList() :
                          UnitOfWork.SystemRoleRepository.Get(x => profileTypeRoleIds.Contains(x.SystemRoleId)).ToList();

            container.ModelList = this.FillOrderedContainer<SystemRole>(results, x => x.SystemRoleId, x => x.SystemRoleName, x => x.SortOrder);
            return container;
        }
        /// <summary>
        /// Retrieves values for the AcademicRank drop down list
        /// </summary>
        /// <returns></returns>
        public Container<IListEntry> ListAcademicRank()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.AcademicRankRepository.GetAll().OrderBy(x => x.SortOrder);
            container.ModelList = this.FillContainer<AcademicRank>(results, x => x.AcademicRankId, x => x.Rank);

            return container;
        }
        /// <summary>
        /// Retrieves values for the AcademicRankAbbreviation drop down list
        /// </summary>
        /// <returns></returns>
        public Container<IListEntry> ListAcademicRankAbbreviation()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.AcademicRankRepository.GetAll().OrderBy(x => x.SortOrder);
            container.ModelList = this.FillContainer<AcademicRank>(results, x => x.AcademicRankId, x => x.RankAbbreviation);

            return container;
        }
        /// <summary>
        /// Retrieves values for the ProfessionalAffiliation drop down list
        /// </summary>
        /// <returns></returns>
        public Container<IListEntry> ListProfessionalAffiliation()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.ProfessionalAffiliationRepository.GetAll().OrderBy(x => x.SortOrder);
            container.ModelList = this.FillContainer<ProfessionalAffiliation>(results, x => x.ProfessionalAffiliationId, x => x.Type);

            return container;
        }
        /// <summary>
        /// Lists the travel modes.
        /// </summary>
        /// <returns></returns>
        public Container<IListEntry> ListTravelModes()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.TravelModeRepository.GetAll().OrderBy(x => x.SortOrder);
            container.ModelList = this.FillContainer<TravelMode>(results, x => x.TravelModeId, x => x.TravelModeAbbreviation);

            return container;
        }
        /// <summary>
        /// Lists the travel modes with details.
        /// </summary>
        /// <returns></returns>
        public Container<TravelModeModel> ListTravelModesWithDetails()
        {
            var container = new Container<TravelModeModel>();
            var results = UnitOfWork.TravelModeRepository.GetAll().OrderBy(x => x.SortOrder);
            container.ModelList = results.ToList().ConvertAll(x => new TravelModeModel(
                x.TravelModeId, x.TravelModeAbbreviation,
                x.CanContainTravelFlights()));
            return container;
        }
        /// <summary>
        /// List nominee affected entities
        /// </summary>
        /// <returns></returns>
        public Container<IListEntry> ListNomineeAffected()
        {
            var container = new Container<IListEntry>();
            var results = UnitOfWork.NomineeAffectedRepository.GetAll().OrderBy(x => x.SortOrder);
            container.ModelList = this.FillContainer<NomineeAffected>(results, x => x.NomineeAffectedId, x => x.NomineeAffected1);
            return container;
        }
        /// <summary>
        /// List recent years
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public Container<string> ListRecentYears(int number)
        {
            var container = new Container<string>();
            var currentYear = DateTime.Now.Year;
            var yearList = new List<string>();
            for (var i = 0; i < number; i++)
            {
                yearList.Add((currentYear - i).ToString());
            }
            container.ModelList = yearList;
            return container;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Generates a list of IListEntry objects from an enumerable list of generic types.  The types
        /// must have two properties (index & value)
        /// </summary>
        /// <typeparam name="T">Entity object</typeparam>
        /// <param name="repositoryResults">Enumerable list of type T</param>
        /// <param name="indexProperty">The index property (int)</param>
        /// <param name="valueProperty">The value property (string)</param>
        /// <param name="sortProperty">The entity property to sort on</param>
        /// <returns>Enumerable list of IListEntry objects</returns>
        private IEnumerable<IListEntry> FillOrderedContainer<T>(IEnumerable<T> repositoryResults, Func<T, int> indexProperty, Func<T, string> valueProperty, Func<T, string> sortProperty)
        {
            return FillContainer(repositoryResults.OrderBy(x => sortProperty(x)), indexProperty, valueProperty);
        }
        /// <summary>
        /// Generates a list of IListEntry objects from an enumerable list of generic types.  The types
        /// must have two properties (index & value)
        /// </summary>
        /// <typeparam name="T">Entity object</typeparam>
        /// <param name="repositoryResults">Enumerable list of type T</param>
        /// <param name="indexProperty">The index property (int)</param>
        /// <param name="valueProperty">The value property (string)</param>
        /// <param name="sortProperty">The entity property to sort on</param>
        /// <returns>Enumerable list of IListEntry objects</returns>
        private IEnumerable<IListEntry> FillOrderedContainer<T>(IEnumerable<T> repositoryResults, Func<T, int> indexProperty, Func<T, string> valueProperty, Func<T, int?> sortProperty)
        {
            return FillContainer(repositoryResults.OrderBy(x => sortProperty(x)), indexProperty, valueProperty);
        }
        /// <summary>
        /// Generates a list of IListEntry objects from an enumerable list of generic types.  The types
        /// must have two properties (index & value)
        /// </summary>
        /// <typeparam name="T">Entity object</typeparam>
        /// <param name="repositoryResults">Enumerable list of type T</param>
        /// <param name="indexProperty">The index property (int)</param>
        /// <param name="valueProperty">The value property (string)</param>
        /// <param name="sortProperty">The entity property to sort on</param>
        /// <returns>Enumerable list of IListEntry objects</returns>
        private IEnumerable<IListEntry> FillDescendingContainer<T>(IEnumerable<T> repositoryResults, Func<T, int> indexProperty, Func<T, string> valueProperty, Func<T, string> sortProperty)
        {
            return FillContainer(repositoryResults.OrderByDescending(x => sortProperty(x)), indexProperty, valueProperty);
        }
        /// <summary>
        /// Generates a list of IListEntry objects from an enumerable list of generic types.  The types
        /// must have two properties (index & value)
        /// </summary>
        /// <typeparam name="T">Entity object</typeparam>
        /// <param name="repositoryResults">Enumerable list of type T</param>
        /// <param name="indexProperty">The index property (int)</param>
        /// <param name="valueProperty">The value property (string)</param>
        /// <returns>Enumerable list of IListEntry objects</returns>
        private IEnumerable<IListEntry> FillContainer<T>(IEnumerable<T> repositoryResults, Func<T, int> indexProperty, Func<T, string> valueProperty)
        {
            List<IListEntry> result = new List<IListEntry>();
            foreach (var entry in repositoryResults)
            {
                IListEntry anEntry = new ListEntry(indexProperty(entry), valueProperty(entry));
                result.Add(anEntry);
            }

            return result;
        }
        /// <summary>
        /// Filters out profile types that are not manually assignable
        /// </summary>
        /// <param name="results"></param>
        /// <returns>Collection of assignable profile types</returns>
        private IEnumerable<ProfileType> FilterNonAssignableProfileTypes(IEnumerable<ProfileType> results)
        {
            return results.Where(x => x.ProfileTypeId != LookupProfileTypeIdForReviewer);
        }
        /// <summary>
        /// Make a list entry  for the 'De-activate Account' drop down on the 
        /// manage account modal.
        /// </summary>
        /// <param name="index">AccountStatusReason index value</param>
        /// <returns>IListEntry for the specified index</returns>
        private IListEntry MakeDeActivateAccountListEntry(int index)
        {
            AccountStatusReason ineligibleAccountStatusReasonEntity = UnitOfWork.AccountStatusReasonRepository.GetByID(index);
            return new ListEntry(ineligibleAccountStatusReasonEntity.AccountStatusReasonId, FormatDeActivateAccountListEntry(ineligibleAccountStatusReasonEntity));
        }
        /// <summary>
        /// Formats the entry for the 'De-activate Account' drop down on the 
        /// manage account modal.
        /// </summary>
        /// <param name="entity"AccountStatusReason entity></param>
        /// <returns>Formatted value</returns>
        private string FormatDeActivateAccountListEntry(AccountStatusReason entity)
        {
            return string.Format("{0}-{1}", entity.AccountStatu.AccountStatusName, entity.AccountStatusReasonName);
        }
        #endregion
        #region Access to Entity Defined DropDown Values
        /// <summary>
        /// Wrapper to provide access to Entity defined values.
        /// </summary>
        public static int LookupAccountStatusReasonIneligible { get { return AccountStatusReason.Indexes.Ineligible;  } }
        public static int LookupUsCountryId { get { return Country.Indexes.UsCountryId;  } }
        public static int LookupPhoneTypeIdForHome { get { return PhoneType.Home; } }
        public static int LookupPhoneTypeIdForDesk { get { return PhoneType.Desk; } }
        public static int LookupProfileTypeIdForReviewer { get { return ProfileType.Indexes.Reviewer; } }
        public static int LookupTravelModeIdAir { get { return TravelMode.Indexes.Air; } }
        public static int LookupTravelModeIdTrain { get { return TravelMode.Indexes.Train; } }
        public static int LookupAssignmentTypeIdCoi { get { return AssignmentType.COI;  } }
        public static int LookupAssignmentTypeIdReader { get { return AssignmentType.Reader; } }
        public static int LookupReviewStatusDisapproved { get { return ReviewStatu.Disapproved; } }
        public static int LookupReviewStatusTriaged { get { return ReviewStatu.Triaged; } }
        public static int LookupReviewStatusActive { get { return ReviewStatu.Active; } }
        public static int LookupReviewStatusFullReview { get { return ReviewStatu.FullReview; } }
        public static int LookupReviewStatusScored { get { return ReviewStatu.Scored; } }
        public static int LookupReviewStatusScoring { get { return ReviewStatu.Scoring; } }
        #region SystemRole Dropdown Values
        /// <summary>
        /// Wrapper to provide access to SystemRole defined values.
        /// </summary>
        public static int LookupSystemRoleClientId { get { return SystemRole.Indexes.Client; } }
        public static int LookupSystemRoleEditingManagerId { get { return SystemRole.Indexes.EditingManager; } }
        public static int LookupSystemRoleEditorId { get { return SystemRole.Indexes.Editor; } }
        public static int LookupSystemRoleReviewerId { get { return SystemRole.Indexes.Reviewer; } }
        public static int LookupSystemRoleRTAId { get { return SystemRole.Indexes.RTA; } }
        public static int LookupSystemRoleSRMId { get { return SystemRole.Indexes.SRM; } }
        public static int LookupSystemRoleSROId { get { return SystemRole.Indexes.SRO; } }
        public static int LookupSystemRoleStaffId { get { return SystemRole.Indexes.Staff; } }
        public static int LookupSystemRoleWebmasterId { get { return SystemRole.Indexes.Webmaster; } }

        public static string LookupSystemRoleClientName { get { return SystemRole.RoleName.Client; } }
        public static string LookupSystemRoleEditingManagerName { get { return SystemRole.RoleName.EditingManager; } }
        public static string LookupSystemRoleEditorName { get { return SystemRole.RoleName.Editor; } }
        public static string LookupSystemRoleReviewerName { get { return SystemRole.RoleName.Reviewer; } }
        public static string LookupSystemRoleRTAName { get { return SystemRole.RoleName.RTA; } }
        public static string LookupSystemRoleSRMName { get { return SystemRole.RoleName.SRM; } }
        public static string LookupSystemRoleSROName { get { return SystemRole.RoleName.SRO; } }
        public static string LookupSystemRoleStaffName { get { return SystemRole.RoleName.Staff; } }
        public static string LookupSystemRoleWebmasterName { get { return SystemRole.RoleName.Webmaster; } }
        #endregion
        #region SystemProfile Dropdown Values
        public static int LookupSystemProfileTypeProspectId { get { return ProfileType.Indexes.Prospect; } }
        public static int LookupSystemProfileTypeReviewerId { get { return ProfileType.Indexes.Reviewer; } }
        public static int LookupSystemProfileTypeSraStaffId { get { return ProfileType.Indexes.SraStaff; } }
        public static int LookupSystemProfileTypeClientId { get { return ProfileType.Indexes.Client; } }
        public static int LookupSystemProfileTypeMisconductId { get { return ProfileType.Indexes.Misconduct; } }
        #endregion
        #region CommentTypes types
        public static int CommentTypeDiscussionNote { get { return CommentType.Indexes.DiscussionNote; } }
        public static int CommentTypeAdminNote { get { return CommentType.Indexes.AdminNote;  } }
        public static int CommentTypeGeneralNote { get { return CommentType.Indexes.GeneralNote; } }
        #endregion
        #region Application State Values
        public static int ApplicationStatePreAssignment { get { return SessionPanel.ApplicationState.PreAssignment; } }
        public static int ApplicationStatePostAssignment { get { return SessionPanel.ApplicationState.PostAssignment; } }
        public static int ApplicationStateFinal { get { return SessionPanel.ApplicationState.Final; } }
        #endregion
        #region AddressType Dropdown values
        /// <summary>
        /// Wrapper to provide access to Entity defined values
        /// </summary>
        public static int LookupAddressTypeOrganizationId { get { return AddressType.Indexes.Organization; } }
        public static int LookupAddressTypePersonalId { get { return AddressType.Indexes.Personal; } }
        public static int LookupAddressTypeW9Id { get { return AddressType.Indexes.W9; } }
        #endregion
        #region EmailAddressType Dropdown values
        /// <summary>
        /// Wrapper to provide access to Entity defined values
        /// </summary>
        public static int LookupEmailAddressTypePersonalId { get { return EmailAddressType.Personal; } }
        public static int LookupEmailAddressTypeBusinessId { get { return EmailAddressType.Business; } }
        public static int LookupEmailAddressTypeAlternateId { get { return EmailAddressType.Alternate; } }
        #endregion
        #region ProfessionalAffiliation
        public static int LookupProfessionalAffiliationInstitutionOrganizationId { get { return ProfessionalAffiliation.Indexes.InstitutionOrganization; } }
        public static int LookupProfessionalAffiliationNominatingOrganizationId { get { return ProfessionalAffiliation.Indexes.NominatingOrganization; } }
        public static int LookupProfessionalAffiliationOtherId { get { return ProfessionalAffiliation.Indexes.Other; } }
        #endregion
        #region AlternateContact dropdown values
        public static int LookupAlternateContactTypeAssistantId { get { return AlternateContactType.Assistant; } }
        public static int LookupAlternateContactTypeSpouseId { get { return AlternateContactType.Spouse; } }
        public static int LookupAlternateContactTypeOtherId { get { return AlternateContactType.Other; } }
        public static int LookupEmContactTypeId { get { return AlternateContactType.Emergency; } }
        #endregion
        #endregion
        #region 
        /// <summary>
        /// Wrapper to provide access to Nominee type dropdown values  
        /// </summary>
        public static int LookupNomineeTypeIneligible { get { return NomineeType.Indexes.IneligibleNominee; } }
        public static int LookupNomineeTypeEligible { get { return NomineeType.Indexes.EligibleNominee; } }
        public static int LookupNomineeTypeSelectedNovice { get { return NomineeType.Indexes.SelectedNovice; } }
        #endregion
        #region ReviewerRecruitment
        /// <summary>
        /// Retrieves vales for "Participant Type" drop down on the Reviewer Recruitment page.
        /// Only "Participant Types" that are marked as a Reviewer (ReviewerFlag == TRUE) and
        /// are active (ActiveFlag == TRUE) are returned.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListParticipantType(int clientId)
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.ClientParticipantTypeRepository.Get(x => (x.ClientId == clientId) && (x.ReviewerFlag) && (x.ActiveFlag));
            container.ModelList = this.FillOrderedContainer<ClientParticipantType>(results, x => x.ClientParticipantTypeId, x => x.ParticipantTypeName, x => x.ParticipantTypeName);

            return container;
        }
        /// <summary>
        /// Retrieves discreet values for "Panel" drop down on the Reviewer Recruitment page.
        /// Because a program year could have multiple panels with the same name only the 
        /// discreet panel names are returned and no index value.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListParticipantRole(int clientId)
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.ClientRoleRepository.Get(x => (x.ClientId == clientId) && (x.ActiveFlag));
            container.ModelList = this.FillOrderedContainer<ClientRole>(results, x => x.ClientRoleId, x => x.RoleName, x => x.RoleName);

            return container;
        }
        /// <summary>
        /// Retrieves discreet values for "Panel" drop down on the Reviewer Recruitment page.
        /// Because a program year could have multiple panels with the same name only the 
        /// discreet panel names are returned and no index value.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListParticipantRoleAbbreviation(int clientId)
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.ClientRoleRepository.Get(x => (x.ClientId == clientId) && (x.ActiveFlag));
            container.ModelList = this.FillOrderedContainer<ClientRole>(results, x => x.ClientRoleId, x => x.RoleAbbreviation, x => x.RoleAbbreviation);

            return container;
        }

        /// <summary>
        /// Retrieves vales for "Program" drop down on the Reviewer Recruitment page.
        /// All programs (open or closed) are returned.
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListDistinctPanelForProgramYear(int programYearId)
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = RetrieveDistinctPanelForProgramYear(programYearId);
            container.ModelList = results.ToList();
            return container;
        }
        /// <summary>
        /// Retrieve all discreet panel names for the program year.
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <returns>Container of IListEntry models</returns>
        private IEnumerable<IListEntry> RetrieveDistinctPanelForProgramYear(int programYearId)
        {
            //  First we retrieve the Program Year that was selected
            //
            ProgramYear programYearEntity = UnitOfWork.ProgramYearRepository.GetByID(programYearId);
            //
            // First we collect all of the ProgramPanels for the specified ProgramYear
            //
            var result = programYearEntity.ProgramPanels
                //
                //  Then collect all of the session panel abbreviations for each of the program panels
                //               
                .Select(x => new ListEntry(0, x.SessionPanel.PanelAbbreviation))
                //
                // The make them distinct
                //
                .Distinct()
                //
                // And finally order them
                //
                .OrderBy(y => y.Value);

            return result;
        }
        /// <summary>
        /// Retrieves vales for "Program" drop down on the Reviewer Recruitment page.
        /// All programs (open or closed) are returned.
        /// </summary>
        /// <param name="clientProgramId">ClientProgram entity identifier</param>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListDistinctPanelForProgram(int clientProgramId)
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = RetrieveDistinctPanelForProgram(clientProgramId);
            container.ModelList = results.ToList();
            return container;
        }
        /// <summary>
        /// Retrieve all discreet panel names for the program.
        /// </summary>
        /// <param name="clientProgramId">ClientProgram entity identifier</param>
        /// <returns>Container of IListEntry models</returns>
        private IEnumerable<IListEntry> RetrieveDistinctPanelForProgram(int clientProgramId)
        {
            //
            //  First we retrieve the ClientProgram Year that was selected
            //
            ClientProgram clientProgramEntity = UnitOfWork.ClientProgramRepository.GetByID(clientProgramId);
            //
            // First we collect all of the ProgramYears for the specified ClientProgram
            //
            var result = clientProgramEntity.ProgramYears
                //
                // then we collect all of the ProgramPanels from the years
                //
                .SelectMany(y => y.ProgramPanels)
                //
                // gather all of the session panel abbreviations
                //
                .Select(x => new DiscreteListEntry(0, x.SessionPanel.PanelAbbreviation))
                //
                // make them distinct
                //
                .Distinct()
                //
                // And finally order them
                //
                .OrderBy(y => y.Value);

            return result;
        }
        /// <summary>
        /// Retrieves vales for the CommunicationMethod drop down
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListCommunicationMethod()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.CommunicationMethodRepository.GetAll();
            container.ModelList = this.FillOrderedContainer<CommunicationMethod>(results, x => x.CommunicationMethodId, x => x.MethodName, x => x.MethodName);

            return container;
        }
        /// <summary>
        /// Retrieves vales for the ParticipationMethods
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListParticipationMethods()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            var results = UnitOfWork.ParticipationMethodRepository.GetAll();
            container.ModelList = this.FillOrderedContainer<ParticipationMethod>(results, x => x.ParticipationMethodId, x => x.ParticipationMethodLabel, x => x.ParticipationMethodLabel);

            return container;
        }
        /// <summary>
        /// Retrieves vales for the ParticipationMethods
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        public Container<ILogicalListEntry> ListParticipationLevels()
        {
            Container<ILogicalListEntry> container = new Container<ILogicalListEntry>();
            container.ModelList = PopulateParticipationLevels();

            return container;
        }
        /// <summary>
        /// Generate a list of ParticipationLevels.  There is no database table
        /// with these values, so we just construct them here
        /// </summary>
        /// <returns>List of ParticipationLevels</returns>
        private IEnumerable<ILogicalListEntry> PopulateParticipationLevels()
        {
            List<ILogicalListEntry> list = new List<ILogicalListEntry>(2);
            //
            // Basically what is happening here is constructing a list of UI labels. 
            // However associated with the label is the value to return for an entity
            // property.  Do not like doing this (specifying UI labels in the BL) but 
            // the association of an entity value to a specific user selection overrode
            // my prejudice.
            //
            list.Add(new LogicalListEntry(true, Helper.Level(true) + " (Ad Hoc)"));
            list.Add(new LogicalListEntry(false, Helper.Level(false)));
            return list;
        }
        #endregion
        #region Workflow
        /// <summary>
        /// Retrieves values for the Workflow Step dropdown
        /// </summary>
        /// <param name="applicationWorkflowId">ApplicationWorkflow entity identifier</param>
        /// <returns>Container of IListEntry models</returns>
        public Container<IListEntry> ListWorkflowSteps(int applicationWorkflowId)
        {
            ValidateInt(applicationWorkflowId, FullName(nameof(LookupService), nameof(ListWorkflowSteps)), nameof(applicationWorkflowId));

            Container<IListEntry> container = new Container<IListEntry>();
            //
            // First we get the workflow steps for the workflow & then we just fill the container with ListEnty's consisting
            // of the ApplicationWorkflowStepId & StepName properties.
            //
            var results = UnitOfWork.ApplicationWorkflowRepository.GetByID(applicationWorkflowId).ApplicationWorkflowSteps.
                //
                // And select only the active ones
                //
                Where(x => x.Active).
                //
                // Now order them by their step order
                //
                OrderBy(x => x.StepOrder);

            List<IListEntry> list = this.FillContainer<ApplicationWorkflowStep>(results, x => x.ApplicationWorkflowStepId, x => x.StepName).ToList();
            //
            // Well we have a special case here.  They need a step after the last step to signify that the workflow is complete.
            // So we add an imaginary step after the list is constructed.
            //
            list.Add(new ListEntry(ApplicationWorkflowStep.CompleteWorkflow, ApplicationWorkflowStep.CompleteWorkflowStepName));
            container.ModelList = list;

            return container;
        }
        #endregion
        #region Setup
        /// <summary>
        /// Generate a list of award types.  
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IListEntry models to list AwardTypes for the specified client</returns>
        public Container<IListDescription> ListAwardTypes(int clientId)
        {
            Container<IListDescription> container = new Container<IListDescription>();
            Client clientEntity = UnitOfWork.ClientRepository.GetByID(clientId);
            container.ModelList = this.FillOrderedDescriptionContainer<ClientAwardType>(clientEntity.ClientAwardTypes.Where(y => y.MechanismRelationshipTypeId == null), x => x.ClientAwardTypeId, x => $"{x.AwardDescription} - {x.LegacyAwardTypeId}", x => x.AwardAbbreviation, x => $"{x.AwardDescription} - {x.LegacyAwardTypeId}");

            return container;
        }
        /// <summary>
        /// Generate a list of child award types.  
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IListEntry models to list AwardTypes for the specified client</returns>
        public Container<IListDescription> ListChildAwardTypes(int clientId)
        {
            Container<IListDescription> container = new Container<IListDescription>();
            Client clientEntity = UnitOfWork.ClientRepository.GetByID(clientId);
            container.ModelList = this.FillOrderedDescriptionContainer<ClientAwardType>(clientEntity.ClientAwardTypes.Where(y => y.MechanismRelationshipTypeId != null), x => x.ClientAwardTypeId, x => $"{x.AwardDescription} - {x.LegacyAwardTypeId}", x => x.AwardAbbreviation, x => $"{x.AwardDescription} - {x.LegacyAwardTypeId}");

            return container;
        }
        /// <summary>
        /// Lists the award types by client, filtering out existing awards with passed in programYearId and receiptCycle.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <returns>
        /// Container of IListDescription models to list AwardTypes for the specified client, minus existing awards in specified programyear/cycle
        /// </returns>
        public Container<IListDescription> ListAwardTypesByClientWithCycleFilter(int programYearId, int cycle)
        {
            Container<IListDescription> container = new Container<IListDescription>();
            int clientId = UnitOfWork.ProgramYearRepository.GetByID(programYearId).ClientProgram.ClientId;
            var awardList = UnitOfWork.ClientAwardTypeRepository.Select().Where(x => x.ClientId == clientId && x.MechanismRelationshipTypeId == null && !x.ProgramMechanism.Any(y => y.ProgramYearId == programYearId && y.ReceiptCycle.Value == cycle))
                .ToList();
            container.ModelList = this.FillOrderedDescriptionContainer<ClientAwardType>(awardList, x => x.ClientAwardTypeId, x => $"{x.AwardDescription} - {x.LegacyAwardTypeId}", x => x.AwardAbbreviation, x => $"{x.AwardDescription} - {x.LegacyAwardTypeId}");

            return container;
        }
        /// <summary>
        /// Checks if the values entered match a preapp.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <returns></returns>
        public bool CheckForAwardPreApps(int programYearId, int cycle)
        {
            bool preAppMatch = UnitOfWork.ProgramMechanismRepository.Select().Where(x => x.ProgramYearId == programYearId && x.ClientAwardType.MechanismRelationshipType != null
                && x.ReceiptCycle == cycle).Any(y => y.ParentProgramMechanismId != null);
            return preAppMatch;
        }
        /// <summary>
        /// Generates a list of IListDescription objects from an enumerable list of generic types.  The types
        /// must have two properties (index & value)
        /// </summary>
        /// <typeparam name="T">Entity object</typeparam>
        /// <param name="repositoryResults">Enumerable list of type T</param>
        /// <param name="indexProperty">The index property (int)</param>
        /// <param name="valueProperty1">The value property (string)</param>
        /// <param name="valueProperty2">The secpmd value property (string)</param>
        /// <param name="sortProperty">The entity property to sort on</param>
        /// <returns>Enumerable list of IListDescription objects</returns>
        private IEnumerable<IListDescription> FillOrderedDescriptionContainer<T>(IEnumerable<T> repositoryResults, Func<T, int> indexProperty, Func<T, string> valueProperty1, Func<T, string> valueProperty2, Func<T, string> sortProperty)
        {
            return FillDescriptionContainer(repositoryResults.OrderBy(x => sortProperty(x)), indexProperty, valueProperty1, valueProperty2);
        }
        /// <summary>
        /// Generates a list of IListDescription objects from an enumerable list of generic types.  The types
        /// must have three properties (index, value, description)
        /// </summary>
        /// <typeparam name="T">Entity object</typeparam>
        /// <param name="repositoryResults">Enumerable list of type T</param>
        /// <param name="indexProperty">The index property (int)</param>
        /// <param name="valueProperty1">The value property (string)</param>
        /// <param name="valueProperty2">The second value property (string)</param>
        /// <returns>Enumerable list of IListDescription objects</returns>
        private IEnumerable<IListDescription> FillDescriptionContainer<T>(IEnumerable<T> repositoryResults, Func<T, int> indexProperty, Func<T, string> valueProperty1, Func<T, string> valueProperty2)
        {
            List<IListDescription> result = new List<IListDescription>();
            foreach (var entry in repositoryResults)
            {
                IListDescription anEntry = new ListDescription(indexProperty(entry), valueProperty1(entry), valueProperty2(entry));
                result.Add(anEntry);
            }

            return result;
        }
        /// <summary>
        /// List the ScoringTemplates for the specified client.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IList objects</returns>
        public Container<IListEntry> ListScoringTemplates (int clientId)
        {
            Container<IListEntry> container = new Container<IListEntry>();
            IEnumerable<ScoringTemplate> templates = UnitOfWork.ScoringTemplate.Get(x => x.ClientId == clientId);
            container.ModelList = this.FillOrderedContainer<ScoringTemplate>(templates, x => x.ScoringTemplateId, x => x.TemplateName, x => x.TemplateName);

            return container;
        }
        /// <summary>
        /// List the Evaluation Criteria for the specified client.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IList objects</returns>
        public Container<IListEntry> ListClientEvaluationCriteria(int clientId)
        {
            Container<IListEntry> container = new Container<IListEntry>();
            IEnumerable<ClientElement> templates = UnitOfWork.ClientElementRepository.Get(x => x.ClientId == clientId);
            container.ModelList = this.FillOrderedContainer<ClientElement>(templates, x => x.ClientElementId, x => x.ElementAbbreviation, x => x.ElementAbbreviation);

            return container;
        }
        public Container<IGenericDescriptionList<int, string, string>> ListClientEvaluationCriteria2(int clientId)
        {
            Container<IGenericDescriptionList<int, string, string>> container = new Container<IGenericDescriptionList<int, string, string>>();
            IEnumerable<ClientElement> templates = UnitOfWork.ClientElementRepository.Get(x => x.ClientId == clientId);
            container.ModelList = this.FillOrderedGenericContainer<ClientElement>(templates, x => x.ClientElementId, x => x.ElementAbbreviation, x => x.ElementDescription, x => x.ElementDescription);

            return container;
        }
        #region MyRegion
        /// <summary>
        /// Generates a list of IGenericDescriptionList<int, string, string> objects from an enumerable list of generic types.  The types
        /// must have three properties (index, value & description)
        /// </summary>
        /// <typeparam name="T">Entity object</typeparam>
        /// <param name="repositoryResults">Enumerable list of type T</param>
        /// <param name="indexProperty">The index property (int)</param>
        /// <param name="valueProperty1">The value property (string)</param>
        /// <param name="valueProperty2">The second value property (string)</param>
        /// <param name="sortProperty">The entity property to sort on</param>
        /// <returns>Enumerable list of IListDescription objects</returns>
        private IEnumerable<IGenericDescriptionList<int, string, string>> FillOrderedGenericContainer<T>(IEnumerable<T> repositoryResults, Func<T, int> indexProperty, Func<T, string> valueProperty1, Func<T, string> valueProperty2, Func<T, string> sortProperty)
        {
            return FillDescriptionGenericContainer(repositoryResults.OrderBy(x => sortProperty(x)), indexProperty, valueProperty1, valueProperty2);
        }
        /// <summary>
        /// Generates a list of IGenericDescriptionList<int, string, string> objects from an enumerable list of generic types.  The types
        /// must have three properties (index, value & description)
        /// </summary>
        /// <typeparam name="T">Entity object</typeparam>
        /// <param name="repositoryResults">Enumerable list of type T</param>
        /// <param name="indexProperty">The index property (int)</param>
        /// <param name="valueProperty1">The value property (string)</param>
        /// <param name="valueProperty2">The second value property (string)</param>
        /// <returns>Enumerable list of IListDescription objects</returns>
        private IEnumerable<IGenericDescriptionList<int, string, string>> FillDescriptionGenericContainer<T>(IEnumerable<T> repositoryResults, Func<T, int> indexProperty, Func<T, string> valueProperty1, Func<T, string> valueProperty2)
        {
            List<IGenericDescriptionList<int, string, string>> result = new List<IGenericDescriptionList<int, string, string>>();
            foreach (var entry in repositoryResults)
            {
                IGenericDescriptionList<int, string, string> anEntry = new GenericDescriptionList<int, string, string> { Index = indexProperty(entry), Value = valueProperty1(entry), Description = valueProperty2(entry) };
                result.Add(anEntry);
            }

            return result;
        }
        #endregion
        /// <summary>
        /// List the Meeting Types
        /// </summary>
        /// <returns>Container of IList objects</returns>
        public Container<IListEntry> ListMeetingTypes()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            IEnumerable<MeetingType> results = UnitOfWork.MeetingTypeRepository.GetAll();
            container.ModelList = this.FillOrderedContainer<MeetingType>(results, x => x.MeetingTypeId, x => x.MeetingTypeName, x => x.SortOrder);

            return container;
        }
        /// <summary>
        /// List the Meeting Types
        /// </summary>
        /// <returns>Container of IList objects</returns>
        public Container<IListEntry> ListMeetings()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            IEnumerable<ClientMeeting> results = UnitOfWork.ClientMeetingRepository.GetAll();
            container.ModelList = this.FillContainer<ClientMeeting>(results, x => x.MeetingTypeId, x => x.MeetingAbbreviation);

            return container;
        }
        /// <summary>
        /// List the Hotels
        /// </summary>
        /// <returns>Container of IList objects</returns>/
        public Container<IListEntry> ListHotels()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            IEnumerable<Hotel> results = UnitOfWork.HotelRepository.GetAll();
            container.ModelList = this.FillOrderedContainer<Hotel>(results, x => x.HotelId, x => x.HotelName, x => x.HotelName);

            return container;
        }
        /// <summary>
        /// List the Client abbreviation
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IList objects</returns>
        public Container<IListEntry> ListClientAbbreviation(int clientId)
        {
            Container<IListEntry> container = new Container<IListEntry>();
            IEnumerable<Client> results = new List<Client>() { UnitOfWork.ClientRepository.GetByID(clientId) };
            container.ModelList = this.FillOrderedContainer<Client>(results, x => x.ClientID, x => x.ClientAbrv, x => x.ClientAbrv);

            return container;
        }
        /// <summary>
        /// List the sessions for a specific meeting.
        /// </summary>
        /// <param name="clientMeetingId">ClientMeeting entity identifier</param>
        /// <returns>Container of IList objects</returns>
        public Container<IListEntry> ListMeetingSessions(int clientMeetingId)
        {
            Container<IListEntry> container = new Container<IListEntry>();
            //
            // Realize that the Get() call could be replaced by GetById() it does
            // not handle the case where there is no clientMeetingId.  This does.
            //
            IEnumerable<MeetingSession> results = UnitOfWork.ClientMeetingRepository.Get(x => x.ClientMeetingId == clientMeetingId)
                                                    .DefaultIfEmpty(new ClientMeeting()).First()
                                                    .MeetingSessions.ToList();
            container.ModelList = this.FillOrderedContainer<MeetingSession>(results, x => x.MeetingSessionId, x => x.SessionAbbreviation, x => x.SessionAbbreviation);

            return container;
        }

        /// <summary>
        /// List the sessions for a specific meeting and for specific program.
        /// </summary>
        /// <param name="clientMeetingId">ClientMeeting entity identifier</param>
        /// <param name="programYearId">Program identifier</param>
        /// <returns>Container of IList objects</returns>
        public Container<IListEntry> ListMeetingSessions(int clientMeetingId, int programYearId)
        {
            Container<IListEntry> container = new Container<IListEntry>();
            //
            // Realize that the Get() call could be replaced by GetById() it does
            // not handle the case where there is no clientMeetingId.  This does.
            //
            IEnumerable<MeetingSession> results = UnitOfWork.ClientMeetingRepository.Get(x => x.ClientMeetingId == clientMeetingId)
                                                    .DefaultIfEmpty(new ClientMeeting()).First()
                                                    .MeetingSessions.ToList();
            List<MeetingSession> cleanResults = new List<MeetingSession>();
            foreach (MeetingSession currSession in results)
            {
                foreach (SessionPanel currPanel in currSession.SessionPanels)
                {
                    foreach (var prPanel in currPanel.ProgramPanels)
                    {
                        if (prPanel.ProgramYearId == programYearId)
                        {
                            if (!cleanResults.Contains(currSession))
                            {
                                cleanResults.Add(currSession);
                            }
                            break;
                        }

                    }
                }
            }

            container.ModelList = this.FillOrderedContainer<MeetingSession>(cleanResults, x => x.MeetingSessionId, x => x.SessionAbbreviation, x => x.SessionAbbreviation);

            return container;
        }



        /// <summary>
        /// Lists the session panels.
        /// </summary>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <returns></returns>
        public Container<IListEntry> ListSessionPanels(int meetingSessionId, int? programYearId)
        {
            Container<IListEntry> container = new Container<IListEntry>();

            IEnumerable<SessionPanel> results = UnitOfWork.SessionPanelRepository.Select().Where(x => x.MeetingSessionId == meetingSessionId);
            
            if (programYearId.HasValue)
            {
                results = results.Where(x => x.ProgramPanels.Any(y => y.ProgramYearId == programYearId));
            }
            
            container.ModelList = this.FillOrderedContainer<SessionPanel>(results.ToList(), x => x.SessionPanelId, x => x.PanelAbbreviation, x => x.PanelAbbreviation);

            return container;
        }

        /// <summary>
        /// Lists the panels by meeting program.
        /// </summary>
        /// <param name="meetingId">The meeting identifier.</param>
        /// <param name="programId">The program identifier.</param>
        /// <returns></returns>
        public Container<IListEntry> ListPanelsByMeetingProgram(int? meetingId, int? programId)
        {
            if (meetingId.HasValue || programId.HasValue) // must have at least one
            {
                Container<IListEntry> container = new Container<IListEntry>();

                var results = UnitOfWork.SessionPanelRepository.Select();
            
                if (meetingId.HasValue)
                {
                    results = results.Where(x => x.MeetingSession.ClientMeetingId == meetingId);
                }

                if (programId.HasValue)
                {
                    results = results.Where(x => x.ProgramPanels.Any(y => y.ProgramYearId == programId));
                }
            
                container.ModelList = this.FillOrderedContainer<SessionPanel>(results.ToList(), x => x.SessionPanelId, x => x.PanelAbbreviation, x => x.PanelAbbreviation);
                return container;
            } else
            {
                return null;
            }

        }

        /// <summary>
        /// Lists the sessions by meeting program.
        /// </summary>
        /// <param name="meetingId">The meeting identifier.</param>
        /// <param name="programId">The program identifier.</param>
        /// <returns></returns>
        public Container<IListEntry> ListSessionsByMeetingProgram(int? meetingId, int? programId)
        {
            if (meetingId.HasValue || programId.HasValue) // must have at least one
            {
                Container<IListEntry> container = new Container<IListEntry>();

                var results = UnitOfWork.MeetingSessionRepository.Select();

                if (meetingId.HasValue)
                {
                    results = results.Where(x => x.ClientMeetingId == meetingId);
                }

                if (programId.HasValue)
                {
                    results = results.Where(x => x.SessionPanels.SelectMany(y => y.ProgramPanels).Any(y => y.ProgramYearId == programId));
                }

                container.ModelList = this.FillOrderedContainer<MeetingSession>(results.ToList(), x => (int)x.MeetingSessionId, x => x.SessionAbbreviation, x => x.SessionAbbreviation);
                return container;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// List the Policy Types
        /// </summary>
        /// <returns>Container of IList objects</returns>
        public Container<IListEntry> ListPolicyTypes()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            IEnumerable<PolicyType> results = UnitOfWork.PolicyTypeRepository.GetAll();
            container.ModelList = this.FillOrderedContainer<PolicyType>(results, x => x.PolicyTypeId, x => x.Name, x => x.SortOrder);
            return container;
        }
        /// <summary>
        /// List the Policy Restriction Types
        /// </summary>
        /// <returns>Container of IList objects</returns>
        public Container<IListEntry> ListPolicyRestrictionTypes()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            IEnumerable<PolicyRestrictionType> results = UnitOfWork.PolicyRestrictionTypeRepository.GetAll();
            container.ModelList = this.FillOrderedContainer<PolicyRestrictionType>(results, x => x.PolicyRestrictionTypeId, x => x.Name, x => x.SortOrder);
            return container;
        }

        /// <summary>
        ///  List the Weekdays
        /// </summary>
        /// <returns>Container of IList objects</returns>
        public Container<IListEntry> ListWeekDays()
        {
            Container<IListEntry> container = new Container<IListEntry>();
            IEnumerable<WeekDay> results = UnitOfWork.WeekDayRepository.GetAll();
            container.ModelList = this.FillOrderedContainer<WeekDay>(results, x => x.WeekDayId, x => x.Name, x => x.SortOrder);
            return container;
        }

        #endregion
    }
}
