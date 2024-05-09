using Sra.P2rmis.Bll.ApplicationScoring;
using Sra.P2rmis.Bll.ModelBuilders.PanelManagement;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;
using System;
using System.Collections.Generic;
using System.Linq;
using Webmodel = Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.Bll.ModelBuilders.ReviewerRecruitment;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Bll.PanelManagement
{
    /// <summary>
    /// PanelManagementService provides services to return collections of model
    /// data specific to the PanelManagement Application.
    /// </summary>
    public partial class PanelManagementService : ServerBase, IPanelManagementService
    {
        #region Server classes
        /// <summary>
        /// Exception messages thrown when parameter validation fails.
        /// </summary>
        private class ExceptionMessages
        {
            public const string ListApplicationInformation = "PanelManagementService.ListApplicationInformation received an invalid parameter: sessionPanelId: [{0}].";
            public const string ListPanelSignification = "PanelManagementService.ListPanelSignifications received an invalid parameter: userId: [{0}].";
        }
        #endregion
        #region Services
        /// <summary>
        /// Retrieves lists of application information for the specified panel.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <returns>Container of IApplicationInformationModel models</returns>
        public Container<Webmodel.IApplicationInformationModel> ListApplicationInformation(int sessionPanelId)
        {
            ValidateListApplicationInformationParameters(sessionPanelId);

            Container<Webmodel.IApplicationInformationModel> container = new Container<Webmodel.IApplicationInformationModel>();
            var results = UnitOfWork.PanelManagementRepository.ListApplicationInformation(sessionPanelId);
            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Gets the applications by panel.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetApplicationsByPanel(int sessionPanelId)
        {
            ValidateInt(sessionPanelId, nameof(GetApplicationsByPanel), nameof(sessionPanelId));
            var models = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            var apps = models.PanelApplications.ToList().ConvertAll(x => new KeyValuePair<int, string>(x.ApplicationId, x.Application.LogNumber));
            return apps;
        }
        /// <summary>
        /// Retrieves list of panel significations (FY, Panel Name andId, Application Id, Program Abbreviation and role).
        /// Returns an empty lit if the user is not an SRO
        /// </summary>
        /// <returns></returns>
        public Container<Webmodel.IPanelSignificationsModel> ListPanelSignifications(int userId)
        {
            ValidateListPanelSignificationParameters(userId);

            Container<Webmodel.IPanelSignificationsModel> container = new Container<Webmodel.IPanelSignificationsModel>();
            var results = UnitOfWork.PanelManagementRepository.ListPanelSignifications(userId);
            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Retrieves list of Program/Years for the specified user.
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <returns>Container of IProgramYearModel models</returns>
        /// <remarks>When this method is reused from another function (i.e. User Management, Criteria etc.) this and any other methods should be refactored to the CriteriaService.</remarks>
        public Container<Webmodel.IProgramYearModel> ListProgramYears(int userId)
        {
            ValidateListProgramYears(userId);

            Container<Webmodel.IProgramYearModel> container = new Container<Webmodel.IProgramYearModel>();
            var results = UnitOfWork.PanelManagementRepository.ListProgramYears(userId);
            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Retrieves list of Active Program/Years for the specified user.
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <returns>Container of IProgramYearModel models of active programs</returns>
        /// <remarks>When this method is reused from another function (i.e. User Management, Criteria etc.) this and any other methods should be refactored to the CriteriaService.</remarks>
        public Container<Webmodel.IProgramYearModel> ListActiveProgramYears(int userId)
        {
            //
            // First find the programs this user is associated with
            //
            Container<Webmodel.IProgramYearModel> container = this.ListProgramYears(userId);
            //
            // then get the users assignments
            //
            IEnumerable<int> targets = GetProgramAssignments(userId);
            //
            // Then filter the programs by the assignments
            //
            container.ModelList = FilterProgramYears(container.ModelList, targets);

            return container;
        }
        /// <summary>
        /// Filters the list of ProgramYears to only the those entries that the User has
        /// an assignment to.
        /// </summary>
        /// <param name="modelList">List of IProgramYearModel Web Models indicating the Program Years</param>
        /// <param name="targets">List of IProgramYearModel entity identifier that the user has an assignment to</param>
        /// <returns>Filtered list of IProgramYearModel models</returns>
        private List<Webmodel.IProgramYearModel> FilterProgramYears(IEnumerable<Webmodel.IProgramYearModel> modelList, IEnumerable<int> targets)
        {
            List<Webmodel.IProgramYearModel> list = new List<WebModels.PanelManagement.IProgramYearModel>();
            return modelList.Where(x => targets.Contains(x.ProgramYearId)).ToList<Webmodel.IProgramYearModel>();
        }
        /// <summary>
        /// Determines the ProgramYear that the user has assignments in
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <returns>Enumeration of ProgramYear entity identifiers</returns>
        private IEnumerable<int> GetProgramAssignments(int userId)
        {
            User userEntity = UnitOfWork.UserRepository.GetByID(userId);

            IEnumerable<int> results = userEntity.PanelUserAssignments.Select(x => x.SessionPanel).
                //
                // then select the ProgramPanels for each of the SessionPanels 
                //
                SelectMany(x => x.ProgramPanels).
                //
                // and filter out the Programs that are closed
                //
                Where(x => x.ProgramYear.DateClosed == null).
                //
                // and return the ProgramYear entity Id
                //
                Select(x => x.ProgramYearId).
                //
                // but only a single copy
                //
                Distinct();

            return results;
        }
        /// <summary>
        /// Retrieves list of panel significations for the specific user and Program/Year.
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <param name="programYearId">ProgramYear identifier</param>
        /// <returns>Container of IPanelSignificationsModel models</returns>
        public Container<Webmodel.IPanelSignificationsModel> ListPanelSignifications(int userId, int programYearId)
        {
            ValidateListPanelSignificationParameters(userId, programYearId);

            Container<Webmodel.IPanelSignificationsModel> container = new Container<Webmodel.IPanelSignificationsModel>();
            var results = UnitOfWork.PanelManagementRepository.ListPanelSignifications(userId, programYearId);
            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Lists the panels with meeting types.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        public IEnumerable<Tuple<int, string, string>> ListPanelsWithMeetingTypes(int userId, int programYearId)
        {
            var results = UnitOfWork.SessionPanelRepository.Get(x => x.ProgramPanels.Any(y => y.ProgramYearId == programYearId));
            var panels = results.Select(y => new Tuple<int, string, string>(y.SessionPanelId, y.PanelAbbreviation, y.MeetingSession.ClientMeeting.MeetingType.MeetingTypeName));

            return panels;
        }
        /// <summary>
        /// Retrieves list of panel names that the application could be transferred to.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="currentPanelId">Panel identifier of the applications current panel</param>
        /// <returns>Container of IApplicationInformationModel models</returns>
        public Container<Webmodel.ITransferPanelModel> ListPanelNames(int applicationId, int currentPanelId)
        {
            ValidateListPanelNamesParameter(applicationId, currentPanelId);

            Container<Webmodel.ITransferPanelModel> container = new Container<Webmodel.ITransferPanelModel>();
            var results = UnitOfWork.PanelManagementRepository.ListPanelNames(applicationId, currentPanelId);
            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Retrieves list of panel names that the application could be transferred to.
        /// </summary>
        /// <param name="currentPanelId">Panel identifier of the applications current panel</param>
        /// <returns>Container of ITransferPanelModel models</returns>
        public Container<Webmodel.ITransferPanelModel> ListSiblingPanelNames(int currentPanelId)
        {
            ValidateListSiblingPanelNamesParameter(currentPanelId);

            Container<Webmodel.ITransferPanelModel> container = new Container<Webmodel.ITransferPanelModel>();
            var results = UnitOfWork.PanelManagementRepository.ListSiblingPanelNames(currentPanelId);
            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Retrieves list of reviewer names that are associated with a session panel.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Container of IUserModel models</returns>
        public Container<IUserModel> ListReviewerNames(int sessionPanelId)
        {
            ValidateListReviewerNamesParameter(sessionPanelId);

            Container<IUserModel> container = new Container<IUserModel>();
            var results = UnitOfWork.PanelUserAssignmentRepository.ListReviewerNames(sessionPanelId);
            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Retrieves list of reasons that the application could be transferred for.
        /// </summary>
        /// <returns>Container of IApplicationInformationModel models</returns>
        public Container<Webmodel.IReasonModel> ListTransferReasons()
        {
            Container<Webmodel.IReasonModel> container = new Container<Webmodel.IReasonModel>();
            var reasons = UnitOfWork.PanelManagementRepository.ListApplicationTransferReasons();
            container.SetModelList(reasons);

            return container;
        }
        /// <summary>
        /// Retrieves list of personnels with conflict of interest.
        /// </summary>
        /// <param name="applicationId">the application identifier</param>
        /// <returns>Container of IPersonnelWithCoi models</returns>
        public Container<Webmodel.IPersonnelWithCoi> ListPersonnelWithCoi(int applicationId)
        {
            ValidateListPersonnelWithCoi(applicationId);

            Container<Webmodel.IPersonnelWithCoi> container = new Container<Webmodel.IPersonnelWithCoi>();
            var results = UnitOfWork.PanelManagementRepository.ListPersonnelWithCoi(applicationId);
            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Retrieves list of reviewer expertise.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="userId">User identifier for the logged in user</param>
        /// <returns>Container of IReviewerExpertise models</returns>
        public Container<Webmodel.IReviewerExpertise> ListReviewerExpertise(int sessionPanelId, int userId)
        {
            ValidateListReviewerExpertiseParameters(sessionPanelId);

            Container<Webmodel.IReviewerExpertise> container = new Container<Webmodel.IReviewerExpertise>();
            var results = UnitOfWork.PanelManagementRepository.ListReviewerExpertise(sessionPanelId, userId);

            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Retrieves PI information with lists of documents and partners.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <returns>ResultModel of IApplicationPIInformation models containing PI information and lists of documents and partners</returns>
        public Container<Webmodel.IApplicationPIInformation> ApplicationPIInformation(int applicationId)
        {
            this.ValidateInteger(applicationId, "PanelManagementService.ApplicationPIInformation", "applicationId");

            Container<Webmodel.IApplicationPIInformation> container = new Container<Webmodel.IApplicationPIInformation>();
            var results = UnitOfWork.PanelManagementRepository.ApplicationPIInformation(applicationId);

            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Retrieves PI information with lists of documents and partners with blinding.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <returns>ResultModel of IApplicationPIInformation models containing PI information and lists of documents and partners</returns>
        public Container<Webmodel.IApplicationPIInformation> ApplicationPIInformationWithBlinding(int applicationId)
        {
            this.ValidateInteger(applicationId, "PanelManagementService.ApplicationPIInformation", "applicationId");

            string blindedString = ConfigManager.BlindedReplacementString;

            Container<Webmodel.IApplicationPIInformation> container = new Container<Webmodel.IApplicationPIInformation>();
            var results = UnitOfWork.PanelManagementRepository.ApplicationPIInformation(applicationId);

            // get the list of the results to modify if blinded
            IEnumerable<Webmodel.IApplicationPIInformation> piInfo = results.ModelList.ToList();

            // modify if blinded
            foreach (Webmodel.IApplicationPIInformation r in piInfo)
            {
                if (r.Blinded)
                {
                    r.AwardMechanism = ApplicationScoringService.IsBlindedValue(r.Blinded, blindedString, r.AwardMechanism);
                    r.ResearchArea = ApplicationScoringService.IsBlindedValue(r.Blinded, blindedString, r.ResearchArea);
                    r.OrganizationName = ApplicationScoringService.IsBlindedValue(r.Blinded, blindedString, r.OrganizationName);
                    r.FirstName = ApplicationScoringService.IsBlindedValue(r.Blinded, blindedString, r.FirstName);
                    r.FirstName = ApplicationScoringService.IsBlindedValue(r.Blinded, blindedString, r.FirstName);
                    r.LastName = ApplicationScoringService.IsBlindedValue(r.Blinded, blindedString, r.LastName);

                    foreach (Webmodel.IApplicationPartnerPIInformation pi in r.PartnerPIInformation)
                    {
                        pi.FirstName = ApplicationScoringService.IsBlindedValue(r.Blinded, blindedString, pi.FirstName);
                        pi.FullName = ApplicationScoringService.IsBlindedValue(r.Blinded, blindedString, pi.FullName);
                        pi.LastName = ApplicationScoringService.IsBlindedValue(r.Blinded, blindedString, pi.LastName);
                        pi.OrganizationName = ApplicationScoringService.IsBlindedValue(r.Blinded, blindedString, pi.OrganizationName);
                    }
                }
                // use the potentially modified list for creating the container
                results.ModelList = piInfo;
            }

            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Retrieves list of abstracts contained in the database for the specified application.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <returns>ResultModel of IApplicationAbstractDocumentModel models containing PI information and lists of documents and partners</returns>
        public Container<Webmodel.IApplicationAbstractDocumentModel> ApplicationAbstract(int applicationId)
        {
            this.ValidateInteger(applicationId, "PanelManagementService.ApplicationAbstract", "applicationId");

            Container<Webmodel.IApplicationAbstractDocumentModel> container = new Container<Webmodel.IApplicationAbstractDocumentModel>();
            var results = UnitOfWork.PanelManagementRepository.ApplicationAbstract(applicationId);

            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Indicates if the application's abstract is contained in the database.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <returns>True if the abstract is contained in the database; false otherwise</returns>
        public bool IsAbstractInDatabase(int applicationId)
        {
            this.ValidateInteger(applicationId, "PanelManagementService.IsAbstractInDatabase", "applicationId");

            bool result;
            result = UnitOfWork.PanelManagementRepository.IsAbstractInDatabase(applicationId);

            return result;
        }

        /// <summary>
        /// Create a log message and insert it into the ActionLog for an application transfer
        /// request between panel.
        /// </summary>
        /// <param name="message">Text of message to log</param>
        /// <param name="userId">User identifier</param>
        public void LogApplicationTranferRequest(string message, int userId)
        {
            ValidateLogApplicationTranferRequestParameters(userId);
            //
            // Create the message we want to log & populate it.
            //
            ActionLog logMessage = new ActionLog();
            logMessage.PopulateForApplicationTransfer(message, userId);
            //
            // Add it to the repository & save it.
            //
            UnitOfWork.ActionLogRepository.Add(logMessage);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Create a log message and insert it into the ActionLog for an application reviewer transfer
        /// request between panel.
        /// </summary>
        /// <param name="message">Text of message to log</param>
        /// <param name="userId">User identifier</param>
        public void LogReviewerTranferRequest(string message, int userId)
        {
            ValidateLogReviewerTranferRequestParameters(userId);
            //
            // Create the message we want to log & populate it.
            //
            ActionLog logMessage = new ActionLog();
            logMessage.PopulateForReviewerTransfer(message, userId);
            //
            // Add it to the repository & save it.
            //
            UnitOfWork.ActionLogRepository.Add(logMessage);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Retrieves the panel's applications to display their review order.
        /// </summary>
        /// <param name="sessionPanelId">Selected session panel identifier </param>
        /// <returns>Container of IOrderOfReview models</returns>
        public Container<Webmodel.IOrderOfReview> ListOrderOfReview(int sessionPanelId)
        {
            ValidateListOrderOfReviewParameters(sessionPanelId);

            Container<Webmodel.IOrderOfReview> container = new Container<Webmodel.IOrderOfReview>();
            var results = UnitOfWork.PanelManagementRepository.ListOrderOfReview(sessionPanelId);
            container.SetModelList(results);

            return container;
        }
        /// <summary>
 	    /// Retrieves the rating guidance by which the evaluator will rate the reviewers.
 	    /// <param name="context">P2RMIS database context</param>
 	    /// <param name="panelId">the panels identifer</param>
 	    /// <returns>Container of IRatingGuidance models</returns>
 	    /// </summary>
 	    public Container<Webmodel.IRatingGuidance> ListReviewerRatingGuidance(int panelId)
        {
            ValidateListReviewerRatingGuidanceParameters(panelId);

            Container<Webmodel.IRatingGuidance> container = new Container<Webmodel.IRatingGuidance>();
            var results = UnitOfWork.PanelManagementRepository.ListReviewerRatingGuidance(panelId);
            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Retrieves the reviewer evaluation provided by the current user for this panel.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelId">Session Panel identifier</param>
        /// <param name="userId">current user identifier</param>
        /// <returns>Container of IReviewerEvaluation models</returns>
        public Container<Webmodel.IReviewerEvaluation> ListUserPanelReviewerEvaluations(int sessionPanelId, int userId)
        {
            ValidateListUserPanelReviewerEvaluationsParameters(sessionPanelId, userId);

            Container<Webmodel.IReviewerEvaluation> container = new Container<Webmodel.IReviewerEvaluation>();
            var results = UnitOfWork.PanelManagementRepository.ListUserPanelReviewerEvaluations(sessionPanelId, userId);
            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Retrieves lists of presentation order values
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>Container of webModelInterfaceType models</returns>
        public Container<int> ListPresentationOrder(int panelApplicationId)
        {
            ValidateListPresentationOrderParameters(panelApplicationId);

            PanelApplication panelApplication = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);

            List<int> currentPresentationOrders = panelApplication.CurrentPresentationOrders();

            Container<int> container = new Container<int>();
            //
            // Get the maximum number of presentation places from the configuration manager.
            // & create the list to return
            //
            int presentationMaximum = ConfigManager.PresentationOrderMaximum;
            List<int> list = new List<int>(presentationMaximum);
            container.ModelList = list;
            //
            // Now populate it.  Would you belove there is not a linq method to do this.
            // (At I could not find a quick way to do it.
            //
            for (int i = 1; i <= presentationMaximum; i++)
            {
                if (!currentPresentationOrders.Contains(i))
                {
                    list.Add(i);
                }
            }

            return container;
        }
        /// <summary>
        /// Retrieves lists of application information for the specified panel.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="paneApplicationInformation">PanelApplication entity identifier</param>
        /// <returns>Container of IApplicationInformationModel models</returns>
        public Webmodel.IApplicationInformationModel ListApplicationInformation(int sessionPanelId, int paneApplicationInformation)
        {
            ValidateListApplicationInformationParameters(sessionPanelId);

            Container<Webmodel.IApplicationInformationModel> container = new Container<Webmodel.IApplicationInformationModel>();
            var results = UnitOfWork.PanelManagementRepository.ListApplicationInformation(sessionPanelId, paneApplicationInformation);
            container.SetModelList(results);

            return container.ModelList.FirstOrDefault();
        }
        /// <summary>
        /// Retrieves the application information for the specified application.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="paneApplicationInformation">PanelApplication entity identifier</param>
        /// <returns>Container of IApplicationInformationModel models</returns>
        public Webmodel.IApplicationInformationModel GetApplicationInformation(int paneApplicationId)
        {
            ValidateListApplicationInformationParameters(paneApplicationId);

            PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(paneApplicationId);

            Container<Webmodel.IApplicationInformationModel> container = new Container<Webmodel.IApplicationInformationModel>();
            var results = UnitOfWork.PanelManagementRepository.ListApplicationInformation(panelApplicationEntity.SessionPanelId, paneApplicationId);
            container.SetModelList(results);

            return container.ModelList.FirstOrDefault();
        }
        /// <summary>
        /// Retrieves the application information for the specified application with blinding.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="paneApplicationInformation">PanelApplication entity identifier</param>
        /// <returns>Container of IApplicationInformationModel models</returns>
        public Webmodel.IApplicationInformationModel GetApplicationInformationWithBlinding(int paneApplicationId)
        {
            var r = GetApplicationInformation(paneApplicationId);
            string blindedString = ConfigManager.BlindedReplacementString;
            if (r.Blinded)
            {
                r.PiFirstName = ApplicationScoringService.IsBlindedValue(r.Blinded, blindedString, r.PiFirstName);
                r.PiLastName = ApplicationScoringService.IsBlindedValue(r.Blinded, string.Empty, r.PiLastName);
                r.PiOrganization = ApplicationScoringService.IsBlindedValue(r.Blinded, string.Empty, r.PiOrganization);
            }
            return r;
        }

        #endregion
        #region Communications Services
        /// <summary>
        /// Retrieves lists of panel administrator names and Email addresses
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <returns>Container of IPanelAdministrators models</returns>
        public Container<Webmodel.IPanelAdministrators> GetPanelAdministrators(int sessionPanelId)
        {
            ValidateGetPanelAdministratorsParameters(sessionPanelId);

            Container<Webmodel.IPanelAdministrators> container = new Container<Webmodel.IPanelAdministrators>();
            var results = UnitOfWork.PanelManagementRepository.GetPanelAdministrators(sessionPanelId);
            container.SetModelList(results);

            return container;
        }

        public Container<Webmodel.IAssignmentTypeDropdownList> GetPanelSessionAssignmentTypeList(int sessionPanelId)
        {
            ValidateGetPanelSessionAssignmentTypeList(sessionPanelId);

            Container<Webmodel.IAssignmentTypeDropdownList> container = new Container<Webmodel.IAssignmentTypeDropdownList>();
            var results = UnitOfWork.PanelManagementRepository.GetPanelSessionAssignmentTypeList(sessionPanelId);
            container.SetModelList(results);

            return container;
        }

        public Container<Webmodel.IClientCoiDropdownList> GetPanelSessionCoiTypeList(int sessionPanelId)
        {
            ValidateGetPanelSessionCoiTypeList(sessionPanelId);

            Container<Webmodel.IClientCoiDropdownList> container = new Container<Webmodel.IClientCoiDropdownList>();
            var results = UnitOfWork.PanelManagementRepository.GetPanelSessionCoiTypeList(sessionPanelId);
            container.SetModelList(results);

            return container;
        }

        public Container<Webmodel.IClientExpertiseRatingDropdownList> GetPanelSessionClientExpertiseRatingList(int sessionPanelId)
        {
            ValidateGetPanelSessionClientExpertiseRatingList(sessionPanelId);

            Container<Webmodel.IClientExpertiseRatingDropdownList> container = new Container<Webmodel.IClientExpertiseRatingDropdownList>();
            var results = UnitOfWork.PanelManagementRepository.GetPanelSessionClientExpertiseRatingList(sessionPanelId);
            container.SetModelList(results);

            return container;
        }
        #endregion
        #region Expertise/Assignment Services
        /// <summary>
        /// Retrieves the data for the modal pop-up on the Panel Management Expertise/Assignments tab.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier for the reviewer</param>
        /// <returns>Container of webModelInterfaceType models</returns>
        public Container<Webmodel.IExpertiseAssignments> GetExpertiseAssignments(int sessionPanelId, int panelApplicationId, int panelUserAssignmentId)
        {
            ValidateGetExpertiseAssignmentsParameters(sessionPanelId, panelApplicationId, panelUserAssignmentId);

            Container<Webmodel.IExpertiseAssignments> container = new Container<Webmodel.IExpertiseAssignments>();
            var results = UnitOfWork.SessionPanelRepository.GetExpertiseAssignments(sessionPanelId, panelApplicationId, panelUserAssignmentId);
            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Validates the parameters for GetExpertiseAssignments
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier for the reviewer</param>
        /// <exception cref="ArgumentException">Thrown if sessionPanelId is invalid (>= 0)</exception>
        /// <exception cref="ArgumentException">Thrown if panelApplicationId is invalid (>= 0)</exception>
        /// <exception cref="ArgumentException">Thrown if panelUserAssignmentId is invalid (>= 0)</exception>
        private void ValidateGetExpertiseAssignmentsParameters(int sessionPanelId, int panelApplicationId, int panelUserAssignmentId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementListServer.GetExpertiseAssignments", "sessionPanelId");
            this.ValidateInteger(panelApplicationId, "PanelManagementListServer.GetExpertiseAssignments", "panelApplicationId");
            this.ValidateInteger(panelUserAssignmentId, "PanelManagementListServer.GetExpertiseAssignments", "panelUserAssignmentId");
        }

        #endregion
        #region Reviewers Services
        /// <summary>
        /// Gets the assigned staffs.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>Container of IAssignedStaffModel objects</returns>
        public Container<Webmodel.IAssignedStaffModel> GetAssignedStaffs(int sessionPanelId)
        {
            ValidateInt(sessionPanelId, nameof(GetAssignedStaffs), nameof(sessionPanelId));

            var builder = new ViewStaffModelBuilder(this.UnitOfWork, sessionPanelId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Gets the panel reviewers.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        public Container<Webmodel.IPanelReviewerModel> GetPanelReviewers(int sessionPanelId)
        {
            ValidateInt(sessionPanelId, nameof(GetPanelReviewers), nameof(sessionPanelId));

            var builder = new ViewReviewerModelBuilder(this.UnitOfWork, sessionPanelId);
            builder.BuildContainer();
            return builder.Results;
        }
        #endregion
        #region Reviewer Search Services
        /// <summary>
        /// Gets the search results.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <returns>List of users fullfilling supplied search criteria</returns>
        public Container<Webmodel.IReviewerSearchResultModel> GetSearchReviewerResults(Webmodel.ISearchForReviewersModel searchModel, int panelId)
        {
            Container<Webmodel.IReviewerSearchResultModel> container = new Container<Webmodel.IReviewerSearchResultModel>();
            ValidateInt(panelId, nameof(GetSearchReviewerResults), nameof(panelId));
            var results = UnitOfWork.PanelManagementRepository.SearchForReviewers(searchModel, panelId);
            container.SetModelList(results);
            return container;
        }
        /// <summary>
        /// Gets the search staff results.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <returns></returns>
        public Container<Webmodel.IStaffSearchResultModel> GetSearchStaffResults(Webmodel.ISearchForStaffsModel searchModel, int panelId)
        {
            Container<Webmodel.IStaffSearchResultModel> container = new Container<Webmodel.IStaffSearchResultModel>();
            ValidateInt(panelId, nameof(GetSearchStaffResults), nameof(panelId));
            var results = UnitOfWork.PanelManagementRepository.SearchForStaffs(searchModel, panelId);
            container.SetModelList(results);
            return container;
        }
        /// <summary>
        /// Gets the applications.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetApplications(int programYearId, int cycle)
        {
            ValidateInt(programYearId, nameof(GetApplications), nameof(programYearId));
            ValidateInt(cycle, nameof(GetApplications), nameof(cycle));
            var py = UnitOfWork.ApplicationRepository.Select().Where(x => x.ProgramMechanism.ProgramYearId == programYearId && x.ProgramMechanism.ReceiptCycle == cycle);
            //filter the IQueryable of apps that have no panel assignments
            var apps = py.Where(x => x.PanelApplications.Count() == 0).OrderBy(y => y.LogNumber);
            var results = apps.ToList().ConvertAll(x => new KeyValuePair<int, string>(x.ApplicationId, x.LogNumber));
            return results;
        }

        public List<string> GetErrorMessages(List<List<string>> referrals, int programYearId, int? receiptCycle, string fiscalYear)
        {
            List<string> errorMessage = new List<string>();
            try
            {
                int i = 1;
                List<ReferralMapping> referralMappings = new List<ReferralMapping>();
                foreach (var ap in referrals)
                {
                    var application = UnitOfWork.ApplicationRepository.GetByLogNumber(ap[0]);
                    if (application != null)
                    {
                        List<ReferralMappingData> referralMappingList = new List<ReferralMappingData>();
                        var sessionPanel = application.PanelApplications.FirstOrDefault(x => x.Application == application).SessionPanelId;
                        var panelAbbreviation = UnitOfWork.SessionPanelRepository.GetByID(sessionPanel).PanelAbbreviation;
                        errorMessage.Add("Row " + i + ": " + ap[0] + " is currently on " + panelAbbreviation + "and cannot be included in a referral mapping.");
                        var programMechanism = UnitOfWork.ProgramMechanismRepository.GetByID(application.ProgramMechanismId);
                        var programYear = programMechanism.ProgramYearId;
                        var year = UnitOfWork.ProgramYearRepository.GetByID(programYear).Year;
                        var clientProgramId = UnitOfWork.ProgramYearRepository.GetByID(programYear).ClientProgramId;
                        int cycle = (int)programMechanism.ReceiptCycle;
                        var program = UnitOfWork.ProgramYearRepository.GetByID(clientProgramId).ClientProgram.ProgramAbbreviation;
                        if ((programYear != programYearId) || (fiscalYear != year) || (receiptCycle != cycle))
                        {
                            errorMessage.Add("Row " + i + ": " + ap[0] + " is not a valid application log number for this cycle.");
                        }
                        if (ap[1] != panelAbbreviation)
                        {
                            errorMessage.Add("Row " + i + ": " + ap[1] + " is not a valid panel for " + program + " " + fiscalYear + " selection.");
                        }
                        var referralMappingData = new ReferralMappingData
                        {
                            ApplicationId = application.ApplicationId,
                            SessionPanelId = sessionPanel,
                            CreatedDate = GlobalProperties.P2rmisDateTimeNow

                        };
                        referralMappingList.Add(referralMappingData);
                        var referralMapping = new ReferralMapping
                        {
                            ProgramYearId = programYear,
                            ReceiptCycle = cycle,
                            ReferralMappingDatas = referralMappingList,
                            CreatedDate = GlobalProperties.P2rmisDateTimeNow

                        };
                        referralMappings.Add(referralMapping);
                    }
                    i++;
                }
                if(errorMessage.Count == 0)
                {
                    foreach(var item in referralMappings)
                    {
                        UnitOfWork.ReferralMappingRepository.AddReferralMapping(item);
                    }
                }
                return errorMessage;
            }
            catch (Exception)
            {
                errorMessage.Add("There was an error uploading the referral mapping data. Please try again.");
                return errorMessage;

            }

        }

        #endregion
        #region Helpers
        /// <summary>
        /// Validates the parameters for ListApplicationInformation.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <exception cref="ArgumentException">Thrown SessionPanel identifier fails validation</exception>
        private void ValidateListApplicationInformationParameters(int sessionPanelId)
        {
            if (sessionPanelId <= 0)
            {
                string message = string.Format(ExceptionMessages.ListApplicationInformation, sessionPanelId);
                throw new ArgumentException(message);
            }
        }
        /// <summary>
        /// Validates the parameters for ListPanelSignifications.
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <exception cref="ArgumentException">Thrown User identifier fails validation</exception>
        private void ValidateListPanelSignificationParameters(int userId)
        {
            this.ValidateInteger(userId, "PanelManagementService.ListPanelSignifications", "userId");
        }
        /// <summary>
        /// Validates the parameters for ListPanelSignifications.
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <param name="programYearId">Program/Year identifier</param>
        /// <exception cref="ArgumentException">Thrown User identifier or ProgramYear identifier fails validation</exception>
        private void ValidateListPanelSignificationParameters(int userId, int programYearId)
        {
            this.ValidateInteger(userId, "PanelManagementService.ListPanelSignifications", "userId");
            this.ValidateInteger(programYearId, "PanelManagementService.ListPanelSignifications", "programYearId");
        }
        /// <summary>
        /// Validates the parameters for ListprogramYears.
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <exception cref="ArgumentException">Thrown User identifier fails validation</exception>
        private void ValidateListProgramYears(int userId)
        {
            this.ValidateInteger(userId, "PanelManagementService.ListProgramYears", "userId");
        }
        /// <summary>
        /// Validates the parameters for ListPanelNames().
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="currentPanelId">Panel identifier of the applications current panel</param>
        private void ValidateListPanelNamesParameter(int applicationId, int currentPanelId)
        {
            this.ValidateInteger(applicationId, "PanelManagementService.ListPanelNames", "applicationId");
            this.ValidateInteger(currentPanelId, "PanelManagementService.ListPanelNames", "currentPanelId");
        }
        /// <summary>
        /// Validates the parameters for ListPersonnelWithCoi.
        /// </summary>
        /// <param name="applicationId">the application identifier</param>
        /// <exception cref="ArgumentException">Thrown User identifier fails validation</exception>
        private void ValidateListPersonnelWithCoi(int applicationId)
        {
            this.ValidateInteger(applicationId, "PanelManagementService.ListPersonnelWithCoi", "applicationId");
        }
        /// <summary>
        /// Validates the parameters for ListReviewerExpertiseParameters.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <exception cref="ArgumentException">Thrown SessionPanel identifier fails validation</exception>
        private void ValidateListReviewerExpertiseParameters(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementService.ListReviewerExpertise", "sessionPanelId");
        }
        /// <summary>
        /// Validates the parameters for ListOrderOfReview.
        /// <param name="currentPanelId">-- panel identifier --</param>
        /// <exception cref="ArgumentException">Thrown if currentPanelId fails validation</exception>
        /// </summary>
        private void ValidateListOrderOfReviewParameters(int currentPanelId)
        {
            this.ValidateInteger(currentPanelId, "PanelManagementService.ListOrderOfReview", "currentPanelId");
        }
        /// <summary>
        /// Validates the parameters for ListUserPanelReviewerEvaluations.
        /// <param name="sessionPanelId">-- session panel identifier --</param>
        /// <param name="userId">-- user identifier --</param>
        /// <exception cref="ArgumentException">Thrown if currentPanelId fails validation</exception>
        /// </summary>
        private void ValidateListUserPanelReviewerEvaluationsParameters(int sessionPanelId, int userId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementService.ListUserPanelReviewerEvaluations", "sessionPanelId");
            this.ValidateInteger(userId, "PanelManagementService.ListUserPanelReviewerEvaluations", "userId");
        }
        /// <summary>
        /// Validates the parameters for ValidateListReviewerRatingGuidanceParameters.
        /// <param name="clientId">-- client identifier --</param>
        /// <exception cref="ArgumentException">Thrown if currentPanelId fails validation</exception>
        /// </summary>
        private void ValidateListReviewerRatingGuidanceParameters(int clientId)
        {
            this.ValidateInteger(clientId, "PanelManagementService.ListReviewerRatingGuidance", "clientId");
        }
        /// <summary>
        /// Validates parameters for LogApplicationTranferRequest()
        /// </summary>
        /// <param name="userId">User identifier</param>
        private void ValidateLogApplicationTranferRequestParameters(int userId)
        {
            this.ValidateInteger(userId, "PanelManagementService.LogApplicationTranferRequest", "userId");
        }
        private void ValidateLogReviewerTranferRequestParameters(int userId)
        {
            this.ValidateInteger(userId, "PanelManagementService.LogReviewerTranferRequest", "userId");
        }
        /// <summary>
        /// Validates the parameters for GetPanelAdministrators.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <exception cref="ArgumentException">Thrown if sessionPanelId is invalid </exception>
        private void ValidateGetPanelAdministratorsParameters(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementService.GetPanelAdministrators", "sessionPanelId");
        }

        private void ValidateGetPanelSessionAssignmentTypeList(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementService.GetPanelSessionAssignmentTypeList", "sessionPanelId");
        }

        private void ValidateGetPanelSessionCoiTypeList(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementService.GetPanelSessionCoiTypeList", "sessionPanelId");
        }

        private void ValidateGetPanelSessionClientExpertiseRatingList(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementService.GetPanelSessionClientExpertiseRatingList", "sessionPanelId");
        }
        /// <summary>
        /// Validates the parameters for ListPresentationOrder.
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        private void ValidateListPresentationOrderParameters(int panelApplicationId)
        {
            this.ValidateInteger(panelApplicationId, "PanelManagementService.ListPresentationOrder", "panelApplicationId");
        }
        /// <summary>
        /// Validates the parameters for ListSiblingPanelNames.
        /// </summary>
        /// <param name="currentPanelId">Panel identifier of the applications current panel</param>
        private void ValidateListSiblingPanelNamesParameter(int currentPanelId)
        {
            this.ValidateInteger(currentPanelId, "PanelManagementService.ListSiblingPanelNames", "currentPanelId");
        }
        /// <summary>
        /// Validates the parameters for ListReviewerNames.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        private void ValidateListReviewerNamesParameter(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementService.ListReviewerNames", "sessionPanelId");
        }
        #endregion
    }

}
