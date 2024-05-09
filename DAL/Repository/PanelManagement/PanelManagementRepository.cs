using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for PanelManagement objects.  This repository provides only
    /// list retrieval services.  No CRUD methods are supported.
    /// </summary>
    public class PanelManagementRepository : IPanelManagementRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// The database context.
        /// </summary>
        internal P2RMISNETEntities context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public PanelManagementRepository(P2RMISNETEntities context)
        {
            this.context = context;
        }
        #endregion
        #region Services Provided
        /// <summary>
        /// Retrieves lists of application information for the specified panel.
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <returns>Container of IApplicationInformationModel models</returns>
        /// </summary>
        public ResultModel<IApplicationInformationModel> ListApplicationInformation(int sessionPanelId)
        {
            ResultModel<IApplicationInformationModel> result = new ResultModel<IApplicationInformationModel>();
            result.ModelList = RepositoryHelpers.ListApplicationInformation(context, sessionPanelId);
            return result;
        }
        /// <summary>
        /// Retrieves lists of panel significations for the specified user.
        /// Return empty list if the user is not an SRO
        /// <param name="userId">user identifier</param>
        /// <returns>ResultModel of IPanelSignificationsModel models</returns>
        /// </summary>
        public ResultModel<IPanelSignificationsModel> ListPanelSignifications(int userId)
        {
            ResultModel<IPanelSignificationsModel> result = new ResultModel<IPanelSignificationsModel>();
            result.ModelList = RepositoryHelpers.ListPanelSignifications(context, userId);

            return result;
        }

        /// <summary>
        /// Gets the panel stage information.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>PanelStageResultModel object</returns>
        public IPanelStageResultModel GetPanelStageInformation(int sessionPanelId)
        {
            IPanelStageResultModel result = new PanelStageResultModel();
            result = RepositoryHelpers.GetPanelStageInformation(context, sessionPanelId);

            return result;
        }
        /// <summary>
        /// Retrieves list of Program/Years for the specified user.
        /// <param name="userId">user identifier</param>
        /// <returns>ResultModel of IProgramYearModel models</returns>
        /// </summary>
        /// <remarks>When this method is reused from another function (i.e. User Management, Criteria etc.) this and any other methods should be refactored to the CriteriaService.</remarks>
        public ResultModel<IProgramYearModel> ListProgramYears(int userId)
        {
            ResultModel<IProgramYearModel> result = new ResultModel<IProgramYearModel>();
            result.ModelList = RepositoryHelpers.ListProgramYears(context, userId);

            return result;
        }
        /// <summary>
        /// Retrieves list of panel significations for the specific user and Program/Year.
        /// <param name="userId">user identifier</param>
        /// <param name="programYearId">ProgramYear identifier</param>
        /// <returns>ResultModel of IPanelSignificationsModel models</returns>
        /// </summary>
        public ResultModel<IPanelSignificationsModel> ListPanelSignifications(int userId, int programYearId)
        {
            ResultModel<IPanelSignificationsModel> result = new ResultModel<IPanelSignificationsModel>();
            result.ModelList = RepositoryHelpers.ListPanelSignifications(context, userId, programYearId);

            return result;
        }

        /// <summary>
        /// Retrieves list of personnels with conflict of interest.
        /// <param name="applicationId">the application identifier</param>
        /// <returns>ResultModel of IPersonnelWithCoi models</returns>
        /// </summary>
        public ResultModel<IPersonnelWithCoi> ListPersonnelWithCoi(int applicationId)
        {
            ResultModel<IPersonnelWithCoi> result = new ResultModel<IPersonnelWithCoi>();
            result.ModelList = RepositoryHelpers.ListPersonnelWithCoi(context, applicationId);

            return result;
        }
        /// <summary>
        /// Retrieves list of panel names that the application could be transferred to.
        /// <param name="applicationId">Application identifier</param>
        /// <param name="currentPanelId">Panel identifier of the applications current panel</param>
        /// <returns>ResultModel of ITransferPanelModel models</returns>
        /// </summary>
        public ResultModel<ITransferPanelModel> ListPanelNames(int applicationId, int currentPanelId)
        {
            ResultModel<ITransferPanelModel> result = new ResultModel<ITransferPanelModel>();
            result.ModelList = RepositoryHelpers.ListPanelNames(context, applicationId, currentPanelId);

            return result;
        }
        /// <summary>
        /// Retrieves list of panel names that the application could be transferred to.
        /// </summary>
        /// <param name="currentPanelId">Panel identifier of the applications current panel</param>
        /// <returns>Container of ITransferPanelModel models</returns>
        public ResultModel<ITransferPanelModel> ListSiblingPanelNames(int currentPanelId)
        {
            ResultModel<ITransferPanelModel> result = new ResultModel<ITransferPanelModel>();
            result.ModelList = RepositoryHelpers.ListSiblingPanelNames(context, currentPanelId);

            return result;
        }
        /// <summary>
        /// Retrieves list of application transfer reasons.
        /// <returns>ResultModel of IReasonModel models</returns>
        /// </summary>
        public ResultModel<IReasonModel> ListApplicationTransferReasons()
        {
            ResultModel<IReasonModel> result = new ResultModel<IReasonModel>();
            result.ModelList = RepositoryHelpers.ListTransferReasons(context, TransferReason.ApplicationType);

            return result;
        }
        /// <summary>
        /// Retrieves list of reviewer expertise.
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="userId">User identifier for the logged in user</param>
        /// <returns>ResultModel of IReviewerExpertise models</returns>
        /// </summary>
        public ResultModel<IReviewerExpertise> ListReviewerExpertise(int sessionPanelId, int userId)
        {
            ResultModel<IReviewerExpertise> result = new ResultModel<IReviewerExpertise>();
            result.ModelList = RepositoryHelpers.ListReviewerExpertise(context, sessionPanelId, userId);

            return result;
        }
        /// <summary>
        /// Retrieves PI information with lists of documents and partners.
        /// <param name="applicationId">Application identifier</param>
        /// <returns>ResultModel of IApplicationPIInformation models containing PI information and lists of documents and partners</returns>
        /// </summary>
        public ResultModel<IApplicationPIInformation> ApplicationPIInformation(int applicationId)
        {
            ResultModel<IApplicationPIInformation> result = new ResultModel<IApplicationPIInformation>();
            result.ModelList = RepositoryHelpers.ApplicationPIInformation(context, applicationId);

            return result;
        }
        /// <summary>
        /// Retrieves an application's abstract text from the database.
        /// <param name="applicationId">Application identifier</param>
        /// <returns>ResultModel of IApplicationAbstractDocumentModel models</returns>
        /// </summary>
        public ResultModel<IApplicationAbstractDocumentModel> ApplicationAbstract(int applicationId)
        {
            ResultModel<IApplicationAbstractDocumentModel> result = new ResultModel<IApplicationAbstractDocumentModel>();
            result.ModelList = RepositoryHelpers.ApplicationAbstract(context, applicationId);

            return result;
        }
        /// <summary>
        /// Retrieves an application's abstract text from the database.
        /// <param name="applicationId">Application identifier</param>
        /// <returns>bool, true if abstract in database, false otherwise</returns>
        /// </summary>
        public bool IsAbstractInDatabase(int applicationId)
        {
            bool result;
            var list = RepositoryHelpers.ApplicationAbstract(context, applicationId);

            result = (list.ToList().Count > 0) ? true : false;

            return result;
        }

        /// <summary>
        /// Retrieves the reviewer evaluation provided by the current user for this panel.
        /// <param name="sessionPanelId">Session Panel identifier</param>
        /// <param name="curUserId">current user identifier</param>
        /// <returns>ResultModel of IReviewerEvaluation models</returns>
        /// </summary>
        public ResultModel<IReviewerEvaluation> ListUserPanelReviewerEvaluations(int sessionPanelId, int curUserId)
        {
            ResultModel<IReviewerEvaluation> result = new ResultModel<IReviewerEvaluation>();
            result.ModelList = RepositoryHelpers.ListUserPanelReviewerEvaluations(context, sessionPanelId, curUserId);

            return result;
        }

        /// <summary>
        /// Retrieves the rating guidance by which the evaluator will rate the reviewers.
        /// <param name="panelId">panel identifier</param>
        /// <returns>ResultModel of IRatingGuidance models</returns>
        /// </summary>
        public ResultModel<IRatingGuidance>  ListReviewerRatingGuidance(int panelId)
        {
            ResultModel<IRatingGuidance> result = new ResultModel<IRatingGuidance>();
            result.ModelList = RepositoryHelpers.ListReviewerRatingGuidance(context, panelId);

            return result;
        }
        /// <summary>
        /// Retrieves reviewer assignment information for the indicated assignement and application.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="panelUserAssignmentId">Panel User Assignment identifier</param>
        /// <param name="applicationId">Application identifier</param>
        /// <returns> IPanelReviewerApplicationAssignmentInformation </returns>        
        public IPanelReviewerApplicationAssignmentInformation GetCurrentPanelReviewerApplicationAssignmentInformation(int panelUserAssignmentId, int applicationId)
        {
            var result = RepositoryHelpers.GetCurrentPanelReviewerApplicationAssignmentInformation(context, panelUserAssignmentId, applicationId);

            return result;
        }

        /// <summary>
        /// Removes the application from panel.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void RemoveApplicationFromPanel(int panelApplicationId, int userId)
        {
            context.uspRemovePanelApplication(panelApplicationId, userId);
        }

        /// <summary>
        /// Removes the user from panel.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void RemoveUserFromPanel(int panelUserAssignmentId, int userId)
        {
            context.uspRemovePanelUserAssignment(panelUserAssignmentId, userId);
        }

        /// <summary>
        /// Gets the participant type of the supplied participant information.
        /// </summary>
        /// <param name="assignedUserId">The user identifier to be assigned.</param>
        /// <param name="sessionPanelId">The session panel identifier to be assigned.</param>
        /// <returns>The mapped participant type</returns>
        public ClientParticipantType GetMappedParticipantType(int assignedUserId, int sessionPanelId)
        {
            var theParticipantType = RepositoryHelpers.GetMappedParticipantType(context, assignedUserId, sessionPanelId);
            return theParticipantType;
        }
        #endregion
        #region Order of Review Services
        /// <summary>
        /// Retrieves the panel's applications to display their review order.
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <returns>ResultModel of IOrderOfReview models</returns>
        /// </summary>
        public ResultModel<IOrderOfReview> ListOrderOfReview(int sessionPanelId)
        {
            ResultModel<IOrderOfReview> result = new ResultModel<IOrderOfReview>();
            result.ModelList = RepositoryHelpers.ListOrderOfReview(context, sessionPanelId);

            return result;
        }
        /// <summary>
        /// Retrieves the list of applications types for the specified session panel.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>ResultModel of IAssignmentTypeDropdownList</returns>        
        public ResultModel<IAssignmentTypeDropdownList> GetPanelSessionAssignmentTypeList(int sessionPanelId)
        {
            ResultModel<IAssignmentTypeDropdownList> result = new ResultModel<IAssignmentTypeDropdownList>();
            result.ModelList = RepositoryHelpers.GetPanelSessionAssignmentTypeList(context, sessionPanelId);

            return result;
        }
        /// <summary>
        /// Retrieves the list of Coi types for the specified session panel.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>ResultModel of IClientCoiDropdownList</returns>        
        public ResultModel<IClientCoiDropdownList> GetPanelSessionCoiTypeList(int sessionPanelId)
        {
            ResultModel<IClientCoiDropdownList> result = new ResultModel<IClientCoiDropdownList>();
            result.ModelList = RepositoryHelpers.GetPanelSessionCoiTypeList(context, sessionPanelId);

            return result;
        }

        /// <summary>
        /// Retrieves the list of expertise types for the specified session panel.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Enumerable collection of ClientExpertiseRatingDropdownList</returns>        
        public ResultModel<IClientExpertiseRatingDropdownList> GetPanelSessionClientExpertiseRatingList(int sessionPanelId)
        {
            ResultModel<IClientExpertiseRatingDropdownList> result = new ResultModel<IClientExpertiseRatingDropdownList>();
            result.ModelList = RepositoryHelpers.GetPanelSessionClientExpertiseRatingList(context, sessionPanelId);

            return result;
        }
        #endregion
        #region Communication Services
        /// <summary>
        /// Retrieves lists of panel administrator names and Email addresses
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <returns>ResultModel of IPanelAdministrators models</returns>
        public ResultModel<IPanelAdministrators> GetPanelAdministrators(int sessionPanelId)
        {
            ResultModel<IPanelAdministrators> result = new ResultModel<IPanelAdministrators>();
            List<PanelUserAssignment> p = new List<PanelUserAssignment>();
            result.ModelList = RepositoryHelpers.GetPanelAdministrators(context.PanelUserAssignments, sessionPanelId);
            return result;
        }

        #endregion
        #region Application Release Services
        /// <summary>
        /// Retrieves a list of ApplicationReleasableStatus objects 
        /// which indicate whether or not the application is ready for release to the reviewers
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>ResultModel<IApplicationReleasableStatus> collection of ApplicationReleasableStatus objects indicating what applications are ready for release to reviewers</returns>
        public ResultModel<IApplicationReleasableStatus> AreApplicationsReadyForRelease(int sessionPanelId)
        {
            ResultModel<IApplicationReleasableStatus> result = new ResultModel<IApplicationReleasableStatus>();
            result.ModelList = RepositoryHelpers.AreApplicationsReadyForRelease(context, sessionPanelId);

            return result;
        }
        #endregion
        #region Critique Management Services

        /// <summary>
        /// Retrieves an application's critique information
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>ApplicationCritiqueModel containing information about Reviewer Critiques for a single application</returns>
        public IApplicationCritiqueModel GetApplicationCritiques(int panelApplicationId)
        {
            var result = RepositoryHelpers.GetApplicationCritiques(context, null, panelApplicationId);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Gets the application critiques for  a panel.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>ApplicationCritiqueModel collection containing information about Reviewer Critiques for a panel</returns>
        public IEnumerable<IApplicationCritiqueModel> GetApplicationCritiquesForPanel(int sessionPanelId)
        {
            var result = RepositoryHelpers.GetApplicationCritiques(context, sessionPanelId, null);
            return result;
        }
        /// <summary>
        /// Retrieves contents of the reviewers critique
        /// </summary>
        /// <param name="applicationWorkflowStepId">ApplicationWorkflowStep identifier</param>
        /// <returns>IMeetingCritiqueModel containing the current contents of the reviewer's critique</returns>
        public IApplicationCritiqueDetailsModel GetApplicationCritiqueDetails(int applicationWorkflowStepId)
        {
            return RepositoryHelpers.GetApplicationCritiqueDetails(context, applicationWorkflowStepId);
        }
        #endregion

        /// <summary>
        /// Retrieves lists of application information for the specified panel.
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <returns>Container of IApplicationInformationModel models</returns>
        /// </summary>
        public ResultModel<IApplicationInformationModel> ListApplicationInformation(int sessionPanelId, int panelApplicationId)
        {
            ResultModel<IApplicationInformationModel> result = new ResultModel<IApplicationInformationModel>();
            var allPanelApplicationsInSession = RepositoryHelpers.ListApplicationInformation(context, sessionPanelId);
            result.ModelList = allPanelApplicationsInSession.Where(x => x.PanelApplicationId == panelApplicationId);

            return result;
        }

        /// <summary>
        /// Sets up the new registration for a specified panel user assignment id.
        /// </summary>
        /// <param name="panelUserAssignmentId">The new assignment identifier.</param>
        /// <param name="userId">The user identifier for the user making the change.</param>
        public void SetupNewRegistration(int panelUserAssignmentId, int userId)
        {
            context.uspCreatePanelUserRegistration(panelUserAssignmentId, userId);
        }
        #region Reviewer Search Repository Services

        /// <summary>
        /// Searches for reviewers.
        /// </summary>
        /// <param name="searchModel">The search criteria model.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <returns>Collection of search results matching search criteria</returns>
        public ResultModel<IReviewerSearchResultModel> SearchForReviewers(ISearchForReviewersModel searchModel,
            int panelId)
        {
            ResultModel<IReviewerSearchResultModel> result = new ResultModel<IReviewerSearchResultModel>();
            //Construct the primary IQueryable object
            var baseResult = RepositoryHelpers.GetReviewerSearchResults(context, panelId);
            //Filter the search results based on search parameters
            result.ModelList = FilterSearchResults(baseResult, searchModel).Take(ConfigManager.PanelManagementReviewerSearchLimit);
            return result;
        }

        /// <summary>
        /// Filters the search results by on supplied search criteria.
        /// </summary>
        /// <param name="baseResult">The base queryable result.</param>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        internal IQueryable<IReviewerSearchResultModel> FilterSearchResults(IQueryable<IReviewerSearchResultModel> baseResult, ISearchForReviewersModel searchModel)
        {
            //the result gets sent through a series of filters by behavior
            var filteredResult = ApplyDiscreteFilters(baseResult, searchModel.EthinicityId, searchModel.ParticipantRoleId, searchModel.ParticipantTypeId, searchModel.GenderId, searchModel.IsPotentialChair, searchModel.UserId, searchModel.AcademicRankId);
            //starts with match
            filteredResult = ApplyPartialFilters(filteredResult, searchModel.LastName, searchModel.FirstName, searchModel.Username);
            //contains match
            filteredResult = ApplyContainsFilter(filteredResult, searchModel.Organization);
            //program dependent filters
            filteredResult = ApplyProgramFilters(filteredResult, searchModel.ProgramId, searchModel.YearId,
                searchModel.PanelName, searchModel.SessionPanelAbbreviation);
            filteredResult = ApplyRatingFilter(filteredResult, searchModel.Rating);
            filteredResult = ApplyStateFilter(filteredResult, searchModel.StateId, searchModel.IsStateExcluded);
            filteredResult = ApplyExpertiseMatch(filteredResult, searchModel.Expertise);
            filteredResult = RepositoryHelpers.ApplyResumeSearch(context, filteredResult, searchModel.Resume);
            return filteredResult;
        }
		        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="panelId"></param>
        /// <returns></returns>
        public ResultModel<IStaffSearchResultModel> SearchForStaffs(ISearchForStaffsModel searchModel,
            int panelId)
        {
            ResultModel<IStaffSearchResultModel> result = new ResultModel<IStaffSearchResultModel>();
            // Construct the primary IQueryable object
            var baseResult = RepositoryHelpers.GetStaffSearchResults(context, panelId);
            // Filter the search results based on search parameters
            result.ModelList = FilterSearchResults(baseResult, searchModel).Take(ConfigManager.PanelManagementStaffSearchLimit);
            return result;
        }

        /// <summary>
        /// Filters the search results by on supplied search criteria.
        /// </summary>
        /// <param name="baseResult">The base queryable result.</param>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        internal IEnumerable<IStaffSearchResultModel> FilterSearchResults(IQueryable<IStaffSearchResultModel> baseResult, ISearchForStaffsModel searchModel)
        {
            // A series of filters by behavior
            var filteredResult = ApplyUserIdFilter(baseResult, searchModel.UserId);
            // Starts with match
            filteredResult = ApplyPartialFilters(filteredResult, searchModel.LastName, searchModel.FirstName, searchModel.Username);
            return filteredResult;
        }

        /// <summary>
        /// Applies the state filter.
        /// </summary>
        /// <param name="filteredResult">The filtered result.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="isStateExcluded">if set to <c>true</c> users of this state are excluded.</param>
        /// <returns>IQueryable with state filters applied</returns>
        internal IQueryable<IReviewerSearchResultModel> ApplyStateFilter(IQueryable<IReviewerSearchResultModel> filteredResult, int? stateId, bool isStateExcluded)
        {
            if (stateId != null)
                filteredResult =
                    filteredResult.Where(x => isStateExcluded ? x.StateId != stateId : x.StateId == stateId);
            return filteredResult;
        }

        /// <summary>
        /// Applies the filter for search criteria expecting "contains" behavior.
        /// </summary>
        /// <param name="filteredResult">The filtered result.</param>
        /// <param name="organization">The organization.</param>
        /// <returns>IQueryable with contains filters applied</returns>
        internal IQueryable<IReviewerSearchResultModel> ApplyContainsFilter(IQueryable<IReviewerSearchResultModel> filteredResult, string organization)
        {
            if (!string.IsNullOrWhiteSpace(organization))
            {
                filteredResult = filteredResult.Where(x => x.Organization.Contains(organization));
            }
            return filteredResult;
        }
		/// <summary>
        /// Applies the partial filters.
        /// </summary>
        /// <param name="filteredResult">The filtered result.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        internal IQueryable<IStaffSearchResultModel> ApplyPartialFilters(IQueryable<IStaffSearchResultModel> filteredResult, string lastName, string firstName, string username)
        {
            if (!string.IsNullOrWhiteSpace(lastName))
                filteredResult = filteredResult.Where(x => x.LastName.StartsWith(lastName));
            if (!string.IsNullOrWhiteSpace(firstName))
                filteredResult = filteredResult.Where(x => x.FirstName.StartsWith(firstName));
            if (!string.IsNullOrWhiteSpace(username))
                filteredResult = filteredResult.Where(x => x.UserName.StartsWith(username));
            return filteredResult;
        }
        /// <summary>
        /// Applies the partial filters for search criteria to use startswith behavior.
        /// </summary>
        /// <param name="filteredResult">The filtered result.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="username">The username.</param>
        /// <returns>Queryable result with startswith filters applied</returns>
        internal IQueryable<IReviewerSearchResultModel> ApplyPartialFilters(IQueryable<IReviewerSearchResultModel> filteredResult, string lastName, string firstName, string username)
        {
            if (!string.IsNullOrWhiteSpace(lastName))
                filteredResult = filteredResult.Where(x => x.LastName.StartsWith(lastName));
            if (!string.IsNullOrWhiteSpace(firstName))
                filteredResult = filteredResult.Where(x => x.FirstName.StartsWith(firstName));
            if (!string.IsNullOrWhiteSpace(username))
                filteredResult = filteredResult.Where(x => x.UserName.StartsWith(username));
            return filteredResult;
        }

        /// <summary>
        /// Applies the program filters.
        /// </summary>
        /// <param name="filteredResult">The filtered result.</param>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="panelName">Name of the panel.</param>
        /// <returns>Queryable result with participation filters applied</returns>
        internal IQueryable<IReviewerSearchResultModel> ApplyProgramFilters(
            IQueryable<IReviewerSearchResultModel> filteredResult, int? programId, int? yearId, string panelName, string panelAbbreviation)
        {
            if (!string.IsNullOrWhiteSpace(panelName) || (programId != null && programId > 0) ||
                (yearId != null && yearId > 0))
            {
                if (panelAbbreviation == "0") panelAbbreviation = null;
                if(panelAbbreviation != null)
                {
                    panelName = panelAbbreviation;
                }
                bool panelNameSubmitted = !string.IsNullOrWhiteSpace(panelName);
                filteredResult = filteredResult.Where(
                    x =>
                        x.Participation.Any(
                            y => ((programId == null || programId < 1) || y.ClientProgramId == programId) &&
                                 ((yearId == null || yearId < 1) || y.ProgramYearId == yearId) &&
                                 (!panelNameSubmitted || y.PanelName.StartsWith(panelName) ||
                                 y.PanelAbbreviation.StartsWith(panelName))));
            }
            return filteredResult;
        }
		        /// <summary>
        /// Applies the user identifier filter.
        /// </summary>
        /// <param name="theResult">The result.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        internal IQueryable<IStaffSearchResultModel> ApplyUserIdFilter(IQueryable<IStaffSearchResultModel> theResult, int? userId)
        {
            if (userId != null)
                theResult = theResult.Where(x => x.UserId == userId);
            return theResult;
        }
        /// <summary>
        /// Applies the standard filters to the queryable.
        /// </summary>
        /// <param name="theResult">The result.</param>
        /// <param name="ethinicityId">The ethinicity identifier.</param>
        /// <param name="participantRoleId">The participant role identifier.</param>
        /// <param name="participantTypeId">The participant type identifier.</param>
        /// <param name="genderId">The gender identifier.</param>
        /// <param name="isPotentialChair">if set to <c>true</c> [is potential chair].</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="academicRankId">The academic rank.</param>
        /// <returns>
        /// Search result queryable with standard filters applied
        /// </returns>
        internal IQueryable<IReviewerSearchResultModel> ApplyDiscreteFilters(IQueryable<IReviewerSearchResultModel> theResult, int? ethinicityId, int? participantRoleId, int? participantTypeId, int? genderId, bool isPotentialChair, int? userId, int? academicRankId)
        {
            if (ethinicityId != null)
                theResult = theResult.Where(x => x.EthnicityId == ethinicityId);
            if (participantRoleId != null)
                theResult = theResult.Where(x => x.Participation.Any(y => y.ClientRoleId == participantRoleId));
            if (participantTypeId != null)
                theResult = theResult.Where(x => x.Participation.Any(y => y.ClientParticipantTypeId == participantTypeId));
            if (genderId != null)
                theResult = theResult.Where(x => x.GenderId == genderId);
            if (isPotentialChair)
                theResult = theResult.Where(x => x.IsPotentialChair);
            if (userId != null)
                theResult = theResult.Where(x => x.UserId == userId);
            if (academicRankId != null)
                theResult = theResult.Where(x => x.AcademicRankId == academicRankId);
            return theResult;
        }

        

        /// <summary>
        /// Applies the expertise match.
        /// </summary>
        /// <param name="filteredResult">The filtered result.</param>
        /// <param name="expertise">The expertise term(s) to search for, comma delimited.</param>
        /// <returns>Queryable result filtered to only include users with expertise terms match supplied value</returns>
        internal IQueryable<IReviewerSearchResultModel> ApplyExpertiseMatch(IQueryable<IReviewerSearchResultModel> filteredResult, string expertise)
        {
            if (!string.IsNullOrWhiteSpace(expertise))
            {
                List<string> expertiseList = expertise.Split(',').Select(x => x.Trim()).ToList();
                foreach (var item in expertiseList)
                {
                    filteredResult = filteredResult.Where(x => x.Expertise.Contains(item));
                }
            }
            return filteredResult;
        }

        /// <summary>
        /// Applies the rating filter.
        /// </summary>
        /// <param name="filteredResult">The filtered result.</param>
        /// <param name="rating">The rating to filter on.</param>
        /// <returns>Queryable result of users with ratings greater than or equal to the supplied value</returns>
        internal IQueryable<IReviewerSearchResultModel> ApplyRatingFilter(IQueryable<IReviewerSearchResultModel> filteredResult, string rating)
        {
            int ratingValue;
            if (rating != null && int.TryParse(rating, out ratingValue))
            {
                filteredResult = filteredResult.Where(x => x.Rating >= ratingValue);
            }
            return filteredResult;
        }
        /// <summary>
        /// get applicat by param
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="fiscalYear"></param>
        /// <param name="programYearId"></param>
        /// <param name="panelId"></param>
        /// <param name="receiptCycle"></param>
        /// <param name="awardId"></param>
        /// <param name="logNumber"></param>
        /// <returns></returns>
        public ResultModel<IApplicationsManagementModel> GetApplications(int? clientId, string fiscalYear, int? programYearId, int? panelId, int? receiptCycle, int? awardId, string logNumber, int userId)
        {
            List<int> userclientIds = new List<int>();
            var userClient = context.UserClients.Where(x => x.UserID == userId).ToList();
            foreach (var client in userClient)
            {
                userclientIds.Add(client.ClientID);
            }
            ResultModel<IApplicationsManagementModel> result = new ResultModel<IApplicationsManagementModel>();
            result.ModelList = RepositoryHelpers.GetApplications(context, clientId, fiscalYear, programYearId, panelId, receiptCycle, awardId, logNumber, userclientIds);

            return result;
        }
        #endregion
    }
}
