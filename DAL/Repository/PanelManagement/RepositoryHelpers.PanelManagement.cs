using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.SummaryStatement;
using Datalayer = Sra.P2rmis.Dal;
using Webmodel = Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.CrossCuttingServices;

[assembly: InternalsVisibleTo("DLLTest")]

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Panel Management repository helpers.  
    /// </summary>
    internal partial class RepositoryHelpers
    {
        #region View Application Repository Helpers
        /// <summary>
        /// Retrieves lists of application information for the specified panel.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <returns>Enumeration of IApplicationInformationModel models</returns>
        internal static IEnumerable<Webmodel.IApplicationInformationModel> ListApplicationInformation(P2RMISNETEntities context, int sessionPanelId)
        {
            var results = (from SessionPanel in context.SessionPanels
                           join PanelApplication in context.PanelApplications on SessionPanel.SessionPanelId equals PanelApplication.SessionPanelId
                           join Application in context.Applications on PanelApplication.ApplicationId equals Application.ApplicationId
                           join ApplicationBudget in context.ApplicationBudgets on Application.ApplicationId equals ApplicationBudget.ApplicationId into ApplicationBudget2
                           from ApplicationBudget in ApplicationBudget2.DefaultIfEmpty()
                           join ProgMechanism in context.ProgramMechanism on Application.ProgramMechanismId equals ProgMechanism.ProgramMechanismId
                           join ClientAwardType in context.ClientAwardTypes on ProgMechanism.ClientAwardTypeId equals ClientAwardType.ClientAwardTypeId
                           join PrimaryPersonnel in
                               (from ApplicationPersonnel in context.ApplicationPersonnels where ApplicationPersonnel.PrimaryFlag select ApplicationPersonnel) on Application.ApplicationId equals PrimaryPersonnel.ApplicationId
                           join AppReviewStatus in context.ApplicationReviewStatus on PanelApplication.PanelApplicationId equals AppReviewStatus.PanelApplicationId
                           where SessionPanel.SessionPanelId == sessionPanelId && AppReviewStatus.ReviewStatu.ReviewStatusTypeId == ReviewStatusType.Review
                           orderby Application.LogNumber
                           select new Webmodel.ApplicationInformationModel
                           {
                               ApplicationId = Application.ApplicationId,
                               LogNumber = Application.LogNumber,
                               PiFirstName = PrimaryPersonnel.FirstName,
                               PiLastName = PrimaryPersonnel.LastName,
                               Title = Application.ApplicationTitle,
                               ClientProgramId = ProgMechanism.ProgramYear.ClientProgramId,
                               ProgramMechanismId = ProgMechanism.ProgramMechanismId,
                               AwardMechanism = ClientAwardType.AwardAbbreviation,
                               PiOrganization = PrimaryPersonnel.OrganizationName,
                               PanelName = SessionPanel.PanelName,
                               PanelApplicationId = PanelApplication.PanelApplicationId,
                               FiscalYear = SessionPanel.ProgramPanels.OrderByDescending(x => x.CreatedDate).FirstOrDefault().ProgramYear.Year,
                               ProgramAbbreviation = SessionPanel.ProgramPanels.OrderByDescending(x => x.CreatedDate).FirstOrDefault().ProgramYear.ClientProgram.ProgramAbbreviation,
                               Blinded = Application.ProgramMechanism.BlindedFlag,
                               SessionPanelId = SessionPanel.SessionPanelId,
                               PanelAbbreviation = SessionPanel.PanelAbbreviation,
                               PanelStartDate = SessionPanel.StartDate ?? DateTime.MaxValue,
                               PanelEndDate = SessionPanel.EndDate ?? DateTime.MaxValue,
                               ReviewDiscussionComplete = (AppReviewStatus.ReviewStatusId == ReviewStatu.Scored || AppReviewStatus.ReviewStatusId == ReviewStatu.Scoring),
                               HasSummaryText = PanelApplication.PanelApplicationSummaries.Count > 0,
                               //
                               // Do not really like putting this here.  Preference would have been to just return the count then in the BL make that determination.
                               // However given that:
                               //  - any property in the web model is view able
                               //  - results is really a deferred query and is not executed until expanded.  In my experience one can expand it but any changes made to
                               //    the models would be lost
                               // So we find the number Asynchronous workflows (which indicates if the summary statement is started).  
                               //
                               IsSummaryStarted = (PanelApplication.ApplicationStages.Where(x => x.ReviewStageId == ReviewStage.Summary).Count() >= Datalayer.PanelApplication.MinimumNumberSummaryWorkflows),
                               HasAssignedReviewers = PanelApplication.PanelApplicationReviewerAssignments.Any(q => AssignmentType.CritiqueAssignments.Contains(q.ClientAssignmentType.AssignmentTypeId)),
                               HasAdminNotes = (String.IsNullOrEmpty(ApplicationBudget.Comments)) ? true : false
                           });
            return results;
        }

        /// <summary>
        /// Retrieves list of panel significations for the specified user. An empty list is returned if the user is not an SRO
        /// </summary>
        /// <param name="userlId">user identifier</param>
        /// <returns>Enumeration of IPanelSignificationsModel models</returns>
        internal static IEnumerable<Webmodel.IPanelSignificationsModel> ListPanelSignifications(P2RMISNETEntities context, int userId)
        {
            var results = (from PanelUserAssignment in context.PanelUserAssignments
                           join SessionPanel in context.SessionPanels on PanelUserAssignment.SessionPanelId equals SessionPanel.SessionPanelId
                           join ProgramPanel in context.ProgramPanels on SessionPanel.SessionPanelId equals ProgramPanel.SessionPanelId
                           join ProgramYear in context.ProgramYears on ProgramPanel.ProgramYearId equals ProgramYear.ProgramYearId
                           join ClientProgram in context.ClientPrograms on ProgramYear.ClientProgramId equals ClientProgram.ClientProgramId
                           join ClientParticipantType in context.ClientParticipantTypes on PanelUserAssignment.ClientParticipantTypeId equals ClientParticipantType.ClientParticipantTypeId
                           where PanelUserAssignment.UserId == userId && (ClientParticipantType.SROFlag ?? false || ClientParticipantType.ElevatedChairpersonFlag)
                           orderby ProgramYear.Year descending, ClientProgram.ProgramAbbreviation, SessionPanel.PanelName
                           select new Webmodel.PanelSignificationsModel
                           {
                               PanelId = PanelUserAssignment.SessionPanelId,
                               PanelName = SessionPanel.PanelName,
                               FY = ProgramYear.Year,
                               ProgramAbbreviation = ClientProgram.ProgramAbbreviation,
                               Role = ClientParticipantType.ParticipantTypeAbbreviation,
                               PanelAbbreviation = SessionPanel.PanelAbbreviation
                           });

            return results;
        }
        /// <summary>
        /// Retrieves list of Program/Years for the specified user.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="userId">user identifier</param>
        /// <returns>Enumeration of IProgramYearModel models</returns>
        /// <remarks>When this method is reused from another function (i.e. User Management, Criteria etc.) this and any other methods should be refactored to the CriteriaService.</remarks>
        internal static IEnumerable<Webmodel.IProgramYearModel> ListProgramYears(P2RMISNETEntities context, int userId)
        {
            var results = (from ProgramYear in context.ProgramYears
                           join ClientProgram in context.ClientPrograms on ProgramYear.ClientProgramId equals ClientProgram.ClientProgramId
                           join UserClient in context.UserClients on ClientProgram.ClientId equals UserClient.ClientID
                           where UserClient.UserID == userId
                           orderby ProgramYear.Year descending, ClientProgram.ProgramAbbreviation
                           select new Webmodel.ProgramYearModel
                           {
                               ProgramYearId = ProgramYear.ProgramYearId,
                               FY = ProgramYear.Year,
                               ProgramDescription = ClientProgram.ProgramDescription,
                               ProgramAbbreviation = ClientProgram.ProgramAbbreviation,
                               DateClosed = ProgramYear.DateClosed
                           });
            return results;
        }
        /// <summary>
        /// Retrieves a session panel's program year information
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>IProgramYearModel model</returns>
        internal static Webmodel.IProgramYearModel GetProgramYear(P2RMISNETEntities context, int sessionPanelId)
        {
            SessionPanel sp = context.SessionPanels.Where(x => x.SessionPanelId == sessionPanelId).FirstOrDefault();
            ProgramYear py = sp.GetProgramYear();
            var results = new Webmodel.ProgramYearModel
            {
                ProgramYearId = py.ProgramYearId,
                FY = py.Year,
                ProgramDescription = py.ClientProgram.ProgramDescription,
                ProgramAbbreviation = py.ClientProgram.ProgramAbbreviation
            };
            return results;
        }
        /// <summary>
        /// Retrieves list of panel significations for the specified user and ProgramYear.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="userId">user identifier</param>
        /// <param name="programYearId">Program/Year identifier</param>
        /// <returns>Enumeration of IPanelSignificationsModel models</returns>
        internal static IEnumerable<Webmodel.IPanelSignificationsModel> ListPanelSignifications(P2RMISNETEntities context, int userId, int programYearId)
        {
            var results = (from SessionPanel in context.SessionPanels
                           join ProgramPanel in context.ProgramPanels on SessionPanel.SessionPanelId equals ProgramPanel.SessionPanelId
                           join ProgramYear in context.ProgramYears on ProgramPanel.ProgramYearId equals ProgramYear.ProgramYearId
                           join ClientProgram in context.ClientPrograms on ProgramYear.ClientProgramId equals ClientProgram.ClientProgramId
                           join UserClient in context.UserClients on ClientProgram.ClientId equals UserClient.ClientID
                           where UserClient.UserID == userId && ProgramYear.ProgramYearId == programYearId
                           orderby ProgramYear.Year descending, ClientProgram.ProgramAbbreviation, SessionPanel.PanelName
                           select new Webmodel.PanelSignificationsModel
                           {
                               PanelId = SessionPanel.SessionPanelId,
                               PanelName = SessionPanel.PanelName,
                               PanelAbbreviation = SessionPanel.PanelAbbreviation,
                               FY = ProgramYear.Year,
                               ProgramAbbreviation = ClientProgram.ProgramAbbreviation
                           });
            return results;
        }

        /// <summary>
        /// Retrieves list of personnel with conflict of interest in the review.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="applicationId">the application identifier</param>
        /// <returns>Enumeration of IPersonnelWithCoi models</returns>
        internal static IEnumerable<Webmodel.IPersonnelWithCoi> ListPersonnelWithCoi(P2RMISNETEntities context, int applicationId)
        {
            var results = (from ApplicationPersonnel in context.ApplicationPersonnels
                           join ClientApplicationPersonnelType in context.ClientApplicationPersonnelTypes
                           on ApplicationPersonnel.ClientApplicationPersonnelTypeId equals ClientApplicationPersonnelType.ClientApplicationPersonnelTypeId
                           where ApplicationPersonnel.ApplicationId == applicationId && ClientApplicationPersonnelType.CoiFlag == true
                           select new Webmodel.PersonnelWithCoi
                           {
                               FirstName = ApplicationPersonnel.FirstName,
                               LastName = ApplicationPersonnel.LastName,
                               Organization = ApplicationPersonnel.OrganizationName,
                               CoiType = ClientApplicationPersonnelType.ApplicationPersonnelType,
                               CoiSource = ApplicationPersonnel.Source
                           });
            return results;
        }
        /// <summary>
        /// Retrieves lists of panels that an application could be transferred to.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="currentPanelId">Panel identifier of the applications current panel</param>
        /// <returns>Enumeration of IApplicationInformationModel models</returns>
        internal static IEnumerable<Webmodel.ITransferPanelModel> ListSiblingPanelNames(P2RMISNETEntities context, int currentPanelId)
        {
            var results = (from SessionPanel in context.SessionPanels
                           join ProgramPanel in context.ProgramPanels on SessionPanel.SessionPanelId equals ProgramPanel.SessionPanelId
                           join cpp in context.ProgramPanels on new { ProgramPanel.ProgramYearId, SessionPanelId = currentPanelId }
                            equals new { cpp.ProgramYearId, cpp.SessionPanelId }
                           where ProgramPanel.SessionPanelId != currentPanelId
                           orderby SessionPanel.PanelName
                           select new Webmodel.TransferPanelModel
                           {
                               PanelId = SessionPanel.SessionPanelId,
                               Name = SessionPanel.PanelName,
                               Abbreviation = SessionPanel.PanelAbbreviation
                           });
            return results;
        }
        /// <summary>
        /// Retrieves lists of panels that an application could be transferred to.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="currentPanelId">Panel identifier of the applications current panel</param>
        /// <returns>Enumeration of IApplicationInformationModel models</returns>
        internal static IEnumerable<Webmodel.ITransferPanelModel> ListPanelNames(P2RMISNETEntities context, int applicationId, int currentPanelId)
        {
            var results = (from Application in context.Applications
                           join ProgMechanism in context.ProgramMechanism on Application.ProgramMechanismId equals ProgMechanism.ProgramMechanismId
                           join ProgramYear in context.ProgramYears on ProgMechanism.ProgramYearId equals ProgramYear.ProgramYearId
                           join ProgramPanel in context.ProgramPanels on ProgramYear.ProgramYearId equals ProgramPanel.ProgramYearId
                           join SessionPanel in context.SessionPanels on ProgramPanel.SessionPanelId equals SessionPanel.SessionPanelId
                           //
                           // we want all panels that the application could go to but the current panel
                           //
                           where Application.ApplicationId == applicationId && SessionPanel.SessionPanelId != currentPanelId
                           orderby SessionPanel.PanelName
                           select new Webmodel.TransferPanelModel
                           {
                               Name = SessionPanel.PanelName,
                               PanelId = SessionPanel.SessionPanelId,
                               Abbreviation = SessionPanel.PanelAbbreviation
                           }
                 );
            return results;
        }
        /// <summary>
        /// Retrieves lists of transfer reasons for the specified type.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="type">Transfer type</param>
        /// <returns>Enumeration of IReasonModel models</returns>
        internal static IEnumerable<Webmodel.IReasonModel> ListTransferReasons(P2RMISNETEntities context, string type)
        {
            var results = from reason in context.TransferReasons
                          where reason.TransferType == type
                          orderby (reason.SortOrder)
                          select new Webmodel.ReasonModel
                          {
                              ReasonId = reason.TransferReasonId,
                              Reason = reason.Reason
                          };
            return results;
        }
        /// <summary>
        /// Retrieves list of reviewer expertise.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="userId">User identifier for the logged in user</param>
        /// <returns></returns>
        internal static IEnumerable<Webmodel.IReviewerExpertise> ListReviewerExpertise(P2RMISNETEntities context, int sessionPanelId, int userId)
        {
            var results = (from PanelUserAssignment in context.PanelUserAssignments
                           join PanelApplication in context.PanelApplications
                            on PanelUserAssignment.SessionPanelId equals PanelApplication.SessionPanelId
                           join Application in context.Applications
                            on PanelApplication.ApplicationId equals Application.ApplicationId
                           join ProgMechanism in context.ProgramMechanism on Application.ProgramMechanismId equals ProgMechanism.ProgramMechanismId
                           join ClientAwardType in context.ClientAwardTypes on ProgMechanism.ClientAwardTypeId equals ClientAwardType.ClientAwardTypeId

                           join UserInfo in context.UserInfoes
                            on PanelUserAssignment.UserId equals UserInfo.UserID
                           join PanelApplicationReviewerExpertise in context.PanelApplicationReviewerExpertises
                            on new { PanelApplication.PanelApplicationId, PanelUserAssignment.PanelUserAssignmentId } equals
                            new { PanelApplicationReviewerExpertise.PanelApplicationId, PanelApplicationReviewerExpertise.PanelUserAssignmentId }
                            into pare
                           from PanelApplicationReviewerExpertise in pare.DefaultIfEmpty()
                           join ClientExpertiseRating in context.ClientExpertiseRatings
                            on PanelApplicationReviewerExpertise.ClientExpertiseRatingId equals ClientExpertiseRating.ClientExpertiseRatingId
                            into cer
                           from ClientExpertiseRating in cer.DefaultIfEmpty()
                           join ClientParticipantType in context.ClientParticipantTypes
                            on PanelUserAssignment.ClientParticipantTypeId equals ClientParticipantType.ClientParticipantTypeId
                           join ClientRole in context.ClientRoles
                            on PanelUserAssignment.ClientRoleId equals ClientRole.ClientRoleId
                           into ClientRoleG
                           from ClientRole in ClientRoleG.DefaultIfEmpty()
                           join PanelApplicationReviewerAssignment in context.PanelApplicationReviewerAssignments
                            on new { PanelApplication.PanelApplicationId, PanelUserAssignment.PanelUserAssignmentId } equals new { PanelApplicationReviewerAssignment.PanelApplicationId, PanelApplicationReviewerAssignment.PanelUserAssignmentId }
                            into para
                           from PanelApplicationReviewerAssignment in para.DefaultIfEmpty()
                           join ClientAssignmentType in context.ClientAssignmentTypes
                            on PanelApplicationReviewerAssignment.ClientAssignmentTypeId equals ClientAssignmentType.ClientAssignmentTypeId
                            into cat
                           from ClientAssignmentType in cat.DefaultIfEmpty()
                           join CurrentUserPanelUserAssignment in context.PanelUserAssignments
                              on new {SessionPanelId = sessionPanelId, UserId = userId} equals new { CurrentUserPanelUserAssignment.SessionPanelId, CurrentUserPanelUserAssignment.UserId }
                              into userpua
                            from CurrentUserPanelUserAssignment in userpua.DefaultIfEmpty()
                           join CurrentUserPanelApplicationReviewerAssignment in context.PanelApplicationReviewerAssignments
                             on new { PanelApplication.PanelApplicationId, CurrentUserPanelUserAssignment.PanelUserAssignmentId } equals new { CurrentUserPanelApplicationReviewerAssignment.PanelApplicationId, CurrentUserPanelApplicationReviewerAssignment.PanelUserAssignmentId }
                             into userpara
                           from CurrentUserPanelApplicationReviewerAssignment in userpara.DefaultIfEmpty()
                           where PanelUserAssignment.SessionPanelId == sessionPanelId
                            && (ClientParticipantType.ReviewerFlag || ClientParticipantType.ElevatedChairpersonFlag)
                           orderby Application.LogNumber
                           select new Webmodel.ReviewerExpertise
                           {
                               ApplicationId = PanelApplication.ApplicationId,
                               LogNumber = Application.LogNumber,
                               AwardAbbrev = ClientAwardType.AwardAbbreviation,
                               ReviewerFirstName = UserInfo.FirstName,
                               ReviewerLastName = UserInfo.LastName,
                               ParticipantId = PanelUserAssignment.PanelUserAssignmentId,
                               ParticipantType = ClientParticipantType.ParticipantTypeAbbreviation,
                               ParticipantTypeId = ClientParticipantType.ClientParticipantTypeId,
                               ParticipantTypeName = ClientParticipantType.ParticipantTypeName,
                               ScientistFlag = (bool?)(ClientParticipantType.ParticipantTypeAbbreviation != Datalayer.ClientParticipantType.SPR && !ClientParticipantType.ConsumerFlag) ?? false,
                               SpecialistFlag = (bool?)(ClientParticipantType.ParticipantTypeAbbreviation == Datalayer.ClientParticipantType.SPR) ?? false,
                               ConsumerFlag = (bool?)ClientParticipantType.ConsumerFlag ?? false,
                               ElevatedChairpersonFlag = (bool?)ClientParticipantType.ElevatedChairpersonFlag ?? false,
                               ClientRoleId = (int?)ClientRole.ClientRoleId ?? 0,
                               Rating = (ClientExpertiseRating.RatingAbbreviation ?? Datalayer.ClientExpertiseRating.Ratings.NoSelection),
                               RatingId = (int?)ClientExpertiseRating.ClientExpertiseRatingId ?? 0,
                               ReviewOrder = PanelApplicationReviewerAssignment.SortOrder,
                               ParticipationRoleAbbrev = string.IsNullOrEmpty(ClientAssignmentType.AssignmentAbbreviation) ? string.Empty : ClientAssignmentType.AssignmentAbbreviation,
                               UserId = UserInfo.UserID,
                               PanelApplicationId = PanelApplication.PanelApplicationId,
                               AssignmentTypeId = (int?)ClientAssignmentType.AssignmentTypeId,
                               IsCurrentUserCoi = CurrentUserPanelApplicationReviewerAssignment != null ? CurrentUserPanelApplicationReviewerAssignment.ClientAssignmentType.AssignmentTypeId == AssignmentType.COI : false,
                               ReviewerExpertiseText = UserInfo.Expertise
                           });
            return results;
        }
        /// <summary>
        /// Retrieves list of reviewer names.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <returns>Enumeration of IUserModel models</returns>
        internal static IEnumerable<IUserModel> ListReviewerNames(P2RMISNETEntities context, int sessionPanelId)
        {
            var results = (from PanelUserAssignment in context.PanelUserAssignments
                           join UserInfo in context.UserInfoes
                            on PanelUserAssignment.UserId equals UserInfo.UserID
                           where PanelUserAssignment.SessionPanelId == sessionPanelId &&
                                 PanelUserAssignment.ClientParticipantType.ReviewerFlag == true
                           orderby UserInfo.LastName
                           select new UserModel
                           {
                               FirstName = UserInfo.FirstName,
                               LastName = UserInfo.LastName,
                               UserId = UserInfo.UserID
                           });
            return results;
        }
        /// <summary>
        /// Retrieves application information for the specified application.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelId">Application identifier</param>
        /// <returns>Enumeration of IApplicationInformationModel models</returns>
        internal static IEnumerable<Webmodel.IApplicationPIInformation> ApplicationPIInformation(P2RMISNETEntities context, int applicationId)
        {
            var results = (from parApp in context.Applications
                           join ProgMechanism in context.ProgramMechanism on parApp.ProgramMechanismId equals ProgMechanism.ProgramMechanismId
                           join ClientAwardType in context.ClientAwardTypes on ProgMechanism.ClientAwardTypeId equals ClientAwardType.ClientAwardTypeId
                           join ApplicationPersonnel in context.ApplicationPersonnels on parApp.ApplicationId equals ApplicationPersonnel.ApplicationId
                           join ClientAppPersonnelType in context.ClientApplicationPersonnelTypes on
                           ApplicationPersonnel.ClientApplicationPersonnelTypeId equals ClientAppPersonnelType.ClientApplicationPersonnelTypeId
                           where parApp.ApplicationId == applicationId && ApplicationPersonnel.PrimaryFlag == true

                           select new Webmodel.ApplicationPIInformation
                           {
                               //PI Information
                               applicationId = parApp.ApplicationId,
                               LogNumber = parApp.LogNumber,
                               ApplicationTitle = parApp.ApplicationTitle,
                               ResearchArea = parApp.ResearchArea,
                               FirstName = ApplicationPersonnel.FirstName,
                               LastName = ApplicationPersonnel.LastName,
                               AwardMechanism = ClientAwardType.AwardAbbreviation,
                               OrganizationName = ApplicationPersonnel.OrganizationName,
                               Blinded = parApp.ProgramMechanism.BlindedFlag,

                               // Partner Information
                               PartnerPIInformation = from app in context.Applications
                                                      join childApp in context.Applications on app.ApplicationId equals childApp.ParentApplicationId

                                                      join childApplicationPersonnel in context.ApplicationPersonnels on childApp.ApplicationId equals childApplicationPersonnel.ApplicationId
                                                      join childClientApplicationPersonnelType in context.ClientApplicationPersonnelTypes on childApplicationPersonnel.ClientApplicationPersonnelTypeId equals childClientApplicationPersonnelType.ClientApplicationPersonnelTypeId

                                                      where
                                                      childApplicationPersonnel.PrimaryFlag == true
                                                      && childApplicationPersonnel.PrimaryFlag && app.ApplicationId == applicationId
                                                      orderby childApp.LogNumber
                                                      select new Webmodel.ApplicationPartnerPIInformation
                                                      {
                                                          FirstName = childApplicationPersonnel.FirstName,
                                                          LastName = childApplicationPersonnel.LastName,
                                                          OrganizationName = childApplicationPersonnel.OrganizationName
                                                      }
                           });


            return results;
        }

        /// <summary>
        /// Retrieves application abstract for the specified application.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="applicationId">Application identifier</param>
        /// <returns>Enumeration of IApplicationAbstractDocumentModel models</returns>
        internal static IEnumerable<Webmodel.IApplicationAbstractDocumentModel> ApplicationAbstract(P2RMISNETEntities context, int applicationId)
        {
            var results = from appText in context.ApplicationTexts
                          where appText.ApplicationId == applicationId && appText.AbstractFlag == true
                          select new Webmodel.ApplicationAbstractDocumentModel
                          {
                              AbstractText = appText.BodyText
                          };

            return results;
        }
        /// <summary>
        /// Retrieves the rating guidance by which the evaluator will rate the reviewers.
        /// <param name="context">P2RMIS database context</param>
        /// <param name="panelId">panel identifier</param>
        /// <returns>Enumeration of IRatingGuidance models</returns>
        /// </summary>
        internal static IEnumerable<Webmodel.IRatingGuidance> ListReviewerRatingGuidance(P2RMISNETEntities context, int panelId)
        {
            var results = from ClientReviewerEvaluationGroup in context.ClientReviewerEvaluationGroups
                          join ReviewerEvaluationGroupGuidance in context.ReviewerEvaluationGroupGuidances
                            on ClientReviewerEvaluationGroup.ClientReviewerEvaluationGroupId equals ReviewerEvaluationGroupGuidance.ClientReviewerEvaluationGroupId
                          join ClientProgram in context.ClientPrograms
                            on ClientReviewerEvaluationGroup.ClientId equals ClientProgram.ClientId
                          join ProgramYear in context.ProgramYears
                            on ClientProgram.ClientProgramId equals ProgramYear.ClientProgramId
                          join ProgramPanel in context.ProgramPanels
                            on ProgramYear.ProgramYearId equals ProgramPanel.ProgramYearId
                          where ProgramPanel.SessionPanelId == panelId
                          select new Webmodel.RatingGuidance
                          {
                              RatingGroupId = ReviewerEvaluationGroupGuidance.ClientReviewerEvaluationGroupId,
                              RatingGroupName = ClientReviewerEvaluationGroup.ReviewerEvaluationGroupName,
                              Rating = ReviewerEvaluationGroupGuidance.Rating,
                              RatingDescription = ReviewerEvaluationGroupGuidance.RatingDescription
                          };

            return results;
        }
        /// <summary>
        /// Retrieves the reviewer evaluation provided by the current user for this panel.
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelId">Session Panel identifier</param>
        /// <param name="curUserId">current user identifier</param>
        /// <returns>Enumeration of IReviewerEvaluation models</returns>
        /// </summary>
        internal static IEnumerable<Webmodel.IReviewerEvaluation> ListUserPanelReviewerEvaluations(P2RMISNETEntities context, int sessionPanelId, int curUserId)
        {
            var results = from SessionPanel in context.SessionPanels
                          join PanelUserAssignment in context.PanelUserAssignments on SessionPanel.SessionPanelId equals PanelUserAssignment.SessionPanelId
                          join User in context.Users on PanelUserAssignment.UserId equals User.UserID
                          join UserInfo in context.UserInfoes on User.UserID equals UserInfo.UserID
                          join ClientParticipantType in context.ClientParticipantTypes on PanelUserAssignment.ClientParticipantTypeId equals ClientParticipantType.ClientParticipantTypeId
                          join ReviewerEvaluation in context.ReviewerEvaluations on new { PanelUserAssignment.PanelUserAssignmentId, UserId = curUserId } equals new { ReviewerEvaluation.PanelUserAssignmentId, UserId = ReviewerEvaluation.CreatedBy }
                          into a
                          from ReviewerEvaluation in a.DefaultIfEmpty()
                          where SessionPanel.SessionPanelId == sessionPanelId && ClientParticipantType.ReviewerFlag == true
                          orderby UserInfo.LastName, UserInfo.FirstName
                          select new Webmodel.ReviewerEvaluation
                          {
                              ReviewerEvaluationId = ReviewerEvaluation.ReviewerEvaluationId,
                              PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId,
                              ReviewerFirstName = UserInfo.FirstName,
                              ReviewerLastName = UserInfo.LastName,
                              ParticipantTypeName = ClientParticipantType.ParticipantTypeName,
                              ChairFlag = (bool?)ClientParticipantType.ChairpersonFlag,
                              Rating = ReviewerEvaluation.Rating,
                              PotentialChairFlag = (bool?)ReviewerEvaluation.RecommendChairFlag,
                              RatingComments = ReviewerEvaluation.Comments,
                              ConsumerFlag = ClientParticipantType.ConsumerFlag,
                              PanelName = SessionPanel.PanelName
                          };
            return results;
        }

        #endregion
        #region Order of Review Repository Helpers
        /// <summary>
        /// Retrieves the panel's applications to display their review order.
        /// <param name="context">P2RMIS database context</param>
        /// <param name="currentPanelId">SessionPanel identifier</param>
        /// <returns>Enumeration of IOrderOfReview models</returns>
        /// <remarks>
        /// This is a fairly SQL style query could be improved by sending base data to BL then using BL to 
        /// call correct methods for processing needs of usage within application
        /// </remarks>
        /// </summary>
        internal static IEnumerable<Webmodel.IOrderOfReview> ListOrderOfReview(P2RMISNETEntities context, int sessionPanelId)
        {
            var result = (from SessionPanel in context.SessionPanels
                          join MeetingSession in context.MeetingSessions on SessionPanel.MeetingSessionId equals MeetingSession.MeetingSessionId
                          join PanelApplication in context.PanelApplications on SessionPanel.SessionPanelId equals PanelApplication.SessionPanelId
                          join Application in context.Applications on PanelApplication.ApplicationId equals Application.ApplicationId
                          join ApplicationPersonnel in context.ApplicationPersonnels on PanelApplication.ApplicationId equals ApplicationPersonnel.ApplicationId
                          join ProgramMechanism in context.ProgramMechanism on Application.ProgramMechanismId equals ProgramMechanism.ProgramMechanismId
                          join ClientAwardType in context.ClientAwardTypes on ProgramMechanism.ClientAwardTypeId equals ClientAwardType.ClientAwardTypeId
                          join ApplicationReviewStatu in context.ApplicationReviewStatus on PanelApplication.PanelApplicationId equals ApplicationReviewStatu.PanelApplicationId
                          join RvwStatu in context.ReviewStatus on ApplicationReviewStatu.ReviewStatusId equals RvwStatu.ReviewStatusId
                          orderby ApplicationReviewStatu.ReviewStatusId == ReviewStatu.Triaged, PanelApplication.ReviewOrder, Application.LogNumber
                          where SessionPanel.SessionPanelId == sessionPanelId && ApplicationPersonnel.PrimaryFlag == true && RvwStatu.ReviewStatusType.ReviewStatusTypeId == ReviewStatusType.Review
                          select new Webmodel.OrderOfReview
                          {
                              Order = PanelApplication.ReviewOrder.HasValue ?
                                         ((ApplicationReviewStatu.ReviewStatusId == ReviewStatu.Triaged) ? MessageService.TriagedAbbreviation :
                                         PanelApplication.ReviewOrder.Value.ToString())
                                         : (ApplicationReviewStatu.ReviewStatusId == ReviewStatu.Triaged) ? MessageService.TriagedAbbreviation : string.Empty,
                              IsTriaged = ApplicationReviewStatu.ReviewStatusId == ReviewStatu.Triaged,
                              LogNumber = Application.LogNumber,
                              FirstName = ApplicationPersonnel.FirstName,
                              LastName = ApplicationPersonnel.LastName,
                              AwardMechanism = ClientAwardType.AwardAbbreviation,
                              ApplicationReviewers = from AppRvwAssign in context.PanelApplicationReviewerAssignments
                                                     join PnlUserAssign in context.PanelUserAssignments on AppRvwAssign.PanelUserAssignmentId equals PnlUserAssign.PanelUserAssignmentId
                                                     join userInfo in context.UserInfoes on PnlUserAssign.UserId equals userInfo.UserID
                                                     join ClientAssignmentType in context.ClientAssignmentTypes on AppRvwAssign.ClientAssignmentTypeId equals ClientAssignmentType.ClientAssignmentTypeId
                                                     orderby AppRvwAssign.SortOrder
                                                     where PnlUserAssign.SessionPanelId == SessionPanel.SessionPanelId && AppRvwAssign.PanelApplicationId == PanelApplication.PanelApplicationId &&
                                                     (AssignmentType.CritiqueAssignments.Contains(ClientAssignmentType.AssignmentTypeId))
                                                     select new Webmodel.ApplicationPanelReviewers
                                                     {
                                                         Order = AppRvwAssign.SortOrder ?? 0,
                                                         ReviewerFirstName = userInfo.FirstName,
                                                         ReviewerLastName = userInfo.LastName,
                                                         PartRoleId = PnlUserAssign.ClientRoleId,
                                                         ParticipantRoleName = PnlUserAssign.ClientRole.RoleAbbreviation,
                                                         ParticipantAssignment = ClientAssignmentType.AssignmentLabel,
                                                         PanelUserAssignmentId = PnlUserAssign.PanelUserAssignmentId
                                                     },
                              PartType = from AppRvwAssign in context.PanelApplicationReviewerAssignments
                                         join PnlUsrAssign in context.PanelUserAssignments on AppRvwAssign.PanelUserAssignmentId equals PnlUsrAssign.PanelUserAssignmentId
                                         join ClntAssignType in context.ClientAssignmentTypes on AppRvwAssign.ClientAssignmentTypeId equals ClntAssignType.ClientAssignmentTypeId
                                         orderby AppRvwAssign.SortOrder
                                         where PnlUsrAssign.SessionPanelId == SessionPanel.SessionPanelId && AppRvwAssign.PanelApplicationId == PanelApplication.PanelApplicationId
                                          && (AssignmentType.CritiqueAssignments.Contains(ClntAssignType.AssignmentTypeId))
                                         select ClntAssignType.AssignmentAbbreviation,
                              //These should not be Application COIs but COI Assignments
                              CoiFirstName = from AppRvwAssign in context.PanelApplicationReviewerAssignments
                                             join PnlUsrAssign in context.PanelUserAssignments on AppRvwAssign.PanelUserAssignmentId equals PnlUsrAssign.PanelUserAssignmentId
                                             join ClntAssignType in context.ClientAssignmentTypes on AppRvwAssign.ClientAssignmentTypeId equals ClntAssignType.ClientAssignmentTypeId
                                             join User in context.Users on PnlUsrAssign.UserId equals User.UserID
                                             join UserInfo in context.UserInfoes on User.UserID equals UserInfo.UserID
                                             orderby UserInfo.LastName, UserInfo.FirstName
                                             where PnlUsrAssign.SessionPanelId == SessionPanel.SessionPanelId && AppRvwAssign.PanelApplicationId == PanelApplication.PanelApplicationId
                                              && (ClntAssignType.AssignmentTypeId == AssignmentType.COI)
                                             select UserInfo.FirstName,
                              CoiLastName = from AppRvwAssign in context.PanelApplicationReviewerAssignments
                                            join PnlUsrAssign in context.PanelUserAssignments on AppRvwAssign.PanelUserAssignmentId equals PnlUsrAssign.PanelUserAssignmentId
                                            join ClntAssignType in context.ClientAssignmentTypes on AppRvwAssign.ClientAssignmentTypeId equals ClntAssignType.ClientAssignmentTypeId
                                            join User in context.Users on PnlUsrAssign.UserId equals User.UserID
                                            join UserInfo in context.UserInfoes on User.UserID equals UserInfo.UserID
                                            orderby UserInfo.LastName, UserInfo.FirstName
                                            where PnlUsrAssign.SessionPanelId == SessionPanel.SessionPanelId && AppRvwAssign.PanelApplicationId == PanelApplication.PanelApplicationId
                                             && (ClntAssignType.AssignmentTypeId == AssignmentType.COI)
                                            select UserInfo.LastName,
                              PreMeetingScores = from AppRvwAssign in context.PanelApplicationReviewerAssignments
                                                 join PnlUsrAssign in context.PanelUserAssignments on AppRvwAssign.PanelUserAssignmentId equals PnlUsrAssign.PanelUserAssignmentId
                                                 join ClntAssignType in context.ClientAssignmentTypes on AppRvwAssign.ClientAssignmentTypeId equals ClntAssignType.ClientAssignmentTypeId
                                                 join PanApp in context.PanelApplications on AppRvwAssign.PanelApplicationId equals PanApp.PanelApplicationId
                                                 join AppStage in context.ApplicationStages on PanApp.PanelApplicationId equals AppStage.PanelApplicationId
                                                 join AppWorkflow in context.ApplicationWorkflows on new { AppStage.ApplicationStageId, b = (int?)PnlUsrAssign.PanelUserAssignmentId } equals new { AppWorkflow.ApplicationStageId, b = AppWorkflow.PanelUserAssignmentId }
                                                 join AppWorkflowStep in context.ApplicationWorkflowSteps on AppWorkflow.ApplicationWorkflowId equals AppWorkflowStep.ApplicationWorkflowId
                                                 join AppWorkflowStepElement in context.ApplicationWorkflowStepElements on AppWorkflowStep.ApplicationWorkflowStepId equals AppWorkflowStepElement.ApplicationWorkflowStepId
                                                 join AppTemplateElement in context.ApplicationTemplateElements on AppWorkflowStepElement.ApplicationTemplateElementId equals AppTemplateElement.ApplicationTemplateElementId
                                                 join ScoringScale in context.ClientScoringScales on AppWorkflowStepElement.ClientScoringId equals ScoringScale.ClientScoringId
                                                 join AppWorkflowStepElementContent in context.ApplicationWorkflowStepElementContents on AppWorkflowStepElement.ApplicationWorkflowStepElementId equals AppWorkflowStepElementContent.ApplicationWorkflowStepElementId into g1
                                                 from AppWorkflowStepElementContent in g1.DefaultIfEmpty()
                                                 join ScoringScaleAdj in context.ClientScoringScaleAdjectivals on new { ScoringScale.ClientScoringId, Score = AppWorkflowStepElementContent.Score } equals new { ScoringScaleAdj.ClientScoringId, Score = (decimal?)ScoringScaleAdj.NumericEquivalent } into g2
                                                 from ScoringScaleAdj in g2.DefaultIfEmpty()
                                                 orderby AppRvwAssign.SortOrder
                                                 where PnlUsrAssign.SessionPanelId == SessionPanel.SessionPanelId && AppRvwAssign.PanelApplicationId == PanelApplication.PanelApplicationId
                                                  && AppStage.ReviewStageId == ReviewStage.Asynchronous && AppTemplateElement.MechanismTemplateElement.OverallFlag == true
                                                  && (AssignmentType.CritiqueAssignments.Contains(ClntAssignType.AssignmentTypeId))
                                                  && (from activeStep in context.udfApplicationWorkflowLastStep(AppWorkflow.ApplicationWorkflowId) select activeStep.ApplicationWorkflowStepId).Contains(AppWorkflowStep.ApplicationWorkflowStepId)
                                                 select new Webmodel.ReviewerScoreModel
                                                 {
                                                     Score = AppWorkflowStepElementContent.Score ?? 0,
                                                     AdjectivalRating = ScoringScaleAdj.ScoreLabel,
                                                     IsCritiqueSubmitted = AppWorkflowStep.Resolution,
                                                     ScoreType = ScoringScale.ScoreType,
                                                     PanelUserAssignmentId = PnlUsrAssign.PanelUserAssignmentId
                                                 }
                          });

            return result;
        }
        /// <summary>
        /// Retrieves the panel's administrators.
        /// </summary>
        /// <param name="thePanelUserAssignments">Entity framework Query-able context entry for PanelUserAssignments</param>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Enumerable collection of PanelAdministrators</returns>        
        internal static IEnumerable<Webmodel.IPanelAdministrators> GetPanelAdministrators(IQueryable<PanelUserAssignment> thePanelUserAssignments, int sessionPanelId)
        {
            var result = thePanelUserAssignments.Where(x => x.SessionPanelId == sessionPanelId && !x.ClientParticipantType.ReviewerFlag)
                                                     .SelectMany(y => y.User.UserInfoes
                                                                                       .Select(z => new Webmodel.PanelAdministrators
                                                                                       {
                                                                                           EmailAddress = z.UserEmails.FirstOrDefault(n => n.PrimaryFlag).Email,
                                                                                           LastName = z.LastName,
                                                                                           FirstName = z.FirstName
                                                                                       })).Where(z => z.EmailAddress != string.Empty);
            return result;
        }
        #endregion
        #region Panel Management Dropdown Lists
        /// <summary>
        /// Retrieves the list of application assignment types for the year and session panel.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Enumerable collection of IAssignmentTypeDropdownList</returns>        
        internal static IEnumerable<Webmodel.IAssignmentTypeDropdownList> GetPanelSessionAssignmentTypeList(P2RMISNETEntities context, int sessionPanelId)
        {
            var result = from ClientAssignmentType in context.ClientAssignmentTypes
                         join ClientProgram in context.ClientPrograms
                            on ClientAssignmentType.ClientId equals ClientProgram.ClientId
                         join ProgramYear in context.ProgramYears
                            on ClientProgram.ClientProgramId equals ProgramYear.ClientProgramId
                         join ProgramPanel in context.ProgramPanels
                            on ProgramYear.ProgramYearId equals ProgramPanel.ProgramYearId
                         where ProgramPanel.SessionPanelId == sessionPanelId
                            && ClientAssignmentType.AssignmentTypeId != AssignmentType.Reader
                         select new Webmodel.AssignmentTypeDropdownList
                         {
                             ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId,
                             ClientAssignmentTypeAbbreviation = ClientAssignmentType.AssignmentAbbreviation
                         };

            return result;
        }
        /// <summary>
        /// Retrieves the list of Coi types for the specified session panel.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Enumerable collection of ClientCoiDropdownList</returns>        
        internal static IEnumerable<Webmodel.IClientCoiDropdownList> GetPanelSessionCoiTypeList(P2RMISNETEntities context, int sessionPanelId)
        {
            var result = from ClientCoiType in context.ClientCoiTypes
                         join ClientProgram in context.ClientPrograms
                             on ClientCoiType.ClientId equals ClientProgram.ClientId
                         join ProgramYear in context.ProgramYears
                             on ClientProgram.ClientProgramId equals ProgramYear.ClientProgramId
                         join ProgramPanel in context.ProgramPanels
                             on ProgramYear.ProgramYearId equals ProgramPanel.ProgramYearId
                         where (ProgramPanel.SessionPanelId == sessionPanelId)
                         orderby ClientCoiType.ClientCoiTypeId
                         select new Webmodel.ClientCoiDropdownList
                         {
                             ClientCoiTypeId = ClientCoiType.ClientCoiTypeId,
                             CoiTypeDescription = ClientCoiType.CoiTypeDescription
                         };

            return result;
        }
        /// <summary>
        /// Retrieves the list of expertise types for the specified session panel.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Enumerable collection of ClientExpertiseRatingDropdownList</returns>        
        internal static IEnumerable<Webmodel.IClientExpertiseRatingDropdownList> GetPanelSessionClientExpertiseRatingList(P2RMISNETEntities context, int sessionPanelId)
        {
            var result = from ClientExpertiseRating in context.ClientExpertiseRatings
                         join ClientProgram in context.ClientPrograms
                             on ClientExpertiseRating.ClientId equals ClientProgram.ClientId
                         join ProgramYear in context.ProgramYears
                             on ClientProgram.ClientProgramId equals ProgramYear.ClientProgramId
                         join ProgramPanel in context.ProgramPanels
                             on ProgramYear.ProgramYearId equals ProgramPanel.ProgramYearId
                         where (ProgramPanel.SessionPanelId == sessionPanelId)
                         orderby ClientExpertiseRating.SortOrder,
                                 ClientExpertiseRating.ClientExpertiseRatingId
                         select new Webmodel.ClientExpertiseRatingDropdownList
                         {
                             ClientExpertiseRatingId = ClientExpertiseRating.ClientExpertiseRatingId,
                             ClientExpertiseRatingAbbreviation = ClientExpertiseRating.RatingAbbreviation
                         };

            return result;
        }
        #endregion
        #region Panel Assignment Helpers
        /// <summary>
        /// Retrieves reviewer assignment information for the indicated assignement and application.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="panelUserAssignmentId">Panel User Assignment identifier</param>
        /// <param name="applicationId">Application identifier</param>
        /// <returns IPanelReviewerApplicationAssignmentInformation </returns>        
        internal static Webmodel.IPanelReviewerApplicationAssignmentInformation GetCurrentPanelReviewerApplicationAssignmentInformation(P2RMISNETEntities context, int panelUserAssignmentId, int applicationId)
        {
            var result = from PanelUserAssignment in context.PanelUserAssignments
                         join PanelApplicationReviewerAssignment in context.PanelApplicationReviewerAssignments
                             on PanelUserAssignment.PanelUserAssignmentId equals PanelApplicationReviewerAssignment.PanelUserAssignmentId
                         join PanelApplication in context.PanelApplications
                             on PanelApplicationReviewerAssignment.PanelApplicationId equals PanelApplication.PanelApplicationId
                         join ClientAssignmentType in context.ClientAssignmentTypes
                             on PanelApplicationReviewerAssignment.ClientAssignmentTypeId equals ClientAssignmentType.ClientAssignmentTypeId
                         join PanelApplicationReviewerExpertise in context.PanelApplicationReviewerExpertises
                             on new { PanelUserAssignment.PanelUserAssignmentId, PanelApplication.PanelApplicationId }
                                 equals new { PanelApplicationReviewerExpertise.PanelUserAssignmentId, PanelApplicationReviewerExpertise.PanelApplicationId }
                         join ClientExpertiseRating in context.ClientExpertiseRatings
                             on PanelApplicationReviewerExpertise.ClientExpertiseRatingId equals ClientExpertiseRating.ClientExpertiseRatingId
                         join PanelApplicationReviewerCoiDetail in context.PanelApplicationReviewerCoiDetails
                             on PanelApplicationReviewerExpertise.PanelApplicationReviewerExpertiseId equals PanelApplicationReviewerCoiDetail.PanelApplicationReviewerExpertiseId
                             into coi1
                         from withCoi1 in coi1.DefaultIfEmpty()
                         join ClientCoiType in context.ClientCoiTypes
                             on withCoi1.ClientCoiTypeId equals ClientCoiType.ClientCoiTypeId into coi
                         from withCoi in coi.DefaultIfEmpty()

                         where PanelUserAssignment.PanelUserAssignmentId == panelUserAssignmentId && PanelApplication.ApplicationId == applicationId
                         select new Webmodel.PanelReviewerApplicationAssignmentInformation
                         {
                             ClientAssignmentTypeId = PanelApplicationReviewerAssignment.ClientAssignmentTypeId,
                             AssignmentTypeAbbreviation = ClientAssignmentType.AssignmentAbbreviation,

                             ReviewerPresentationPosition = PanelApplicationReviewerAssignment.SortOrder ?? 0,
                             IsCoi = ClientAssignmentType.AssignmentTypeId == AssignmentType.COI,
                             PanelApplicationReviewerExpertiseRatingId = PanelApplicationReviewerExpertise.PanelApplicationReviewerExpertiseId,
                             ClientExperienceRatingId = ClientExpertiseRating.ClientExpertiseRatingId,
                             RatingAbbreviation = ClientExpertiseRating.RatingAbbreviation,
                             PanelApplicationReviewerCoiDetailId = withCoi1.PanelApplicationReviewerCoiDetailId,
                             ClientCoiTypeId = withCoi.ClientCoiTypeId,
                             CoiTypeName = withCoi.CoiTypeName,
                             Comments = PanelApplicationReviewerExpertise.ExpertiseComments
                         };
            return result.FirstOrDefault();
        }
        /// <summary>
        /// Retrieves a list of ApplicationReleasableStatus objects 
        /// which indicate whether or not the application is ready for release to the reviewers
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>IEnumerable<IApplicationReleasableStatus> collection of ApplicationReleasableStatus objects indicating what applications are ready for release to reviewers</returns>
        internal static IEnumerable<Webmodel.IApplicationReleasableStatus> AreApplicationsReadyForRelease(P2RMISNETEntities context, int sessionPanelId)
        {

            var result =
                        from Application in context.Applications
                        join PanelApplication in context.PanelApplications
                        on Application.ApplicationId equals PanelApplication.ApplicationId
                        join ProgramMech in context.ProgramMechanism
                        on Application.ProgramMechanismId equals ProgramMech.ProgramMechanismId
                        join ApplicationStage in context.ApplicationStages
                        on PanelApplication.PanelApplicationId equals ApplicationStage.PanelApplicationId
                        join MechanismTemplate in context.MechanismTemplates
                            on ProgramMech.ProgramMechanismId equals MechanismTemplate.ProgramMechanismId
                            into mt
                        from MechTemplate in mt.DefaultIfEmpty()
                        where PanelApplication.SessionPanelId == sessionPanelId && ApplicationStage.ReviewStageId == ReviewStage.Asynchronous
                        select new Webmodel.ApplicationReleasableStatus
                        {
                            ApplicationId = Application.ApplicationId,
                            MechanismTemplateId = MechTemplate.MechanismTemplateId
                        };

            return result;
        }

        #endregion
        #region Expertise/Assignments Helpers
        /// <summary>
        /// Retrieve the data for a specific user assignment
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelId">Session panel identifier (tells which session)</param
        /// <param name="panelApplicationId">Panel application identifier (tells which application)</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier (tells which reviewer)</param>
        /// <returns>Single IExpertiseAssignments object</returns>
        internal static IEnumerable<Webmodel.IExpertiseAssignments> GetExpertiseAssignments(P2RMISNETEntities context, int sessionPanelId, int panelApplicationId, int panelUserAssignmentId)
        {
            //
            // Get the session panel by Id
            //
            SessionPanel theSession = context.SessionPanels.FirstOrDefault(x => x.SessionPanelId == sessionPanelId);
            //
            // Then find the application from all applications within the panel.  Since we have the application id there will be
            // on one.
            //
            var thePanelApplication = theSession.PanelApplications.Where(s => s.PanelApplicationId == panelApplicationId);

            var thePanelUserAssignment = context.PanelUserAssignments.Where(s => s.PanelUserAssignmentId == panelUserAssignmentId);
            //
            // Now find the reviewer's experience on this application.73969
            //
            var thePanelApplicationReviewerExpertise = theSession.PanelApplications.FirstOrDefault(s => s.PanelApplicationId == panelApplicationId).PanelApplicationReviewerExpertises.Where(pare => pare.PanelUserAssignmentId == panelUserAssignmentId).FirstOrDefault();
            var result = thePanelApplication.Select(
               z => new Webmodel.ExpertiseAssignments
               {
                   //
                   // Set the identifying information
                   //
                   SessionPanelId = sessionPanelId,
                   PanelApplicationId = panelApplicationId,
                   PanelApplicationReviewerExpertiseId = z.PanelApplicationReviewerExpertises.Any(pare => pare.PanelUserAssignmentId == panelUserAssignmentId) ?
                        z.PanelApplicationReviewerExpertises.First(pare => pare.PanelUserAssignmentId == panelUserAssignmentId).PanelUserAssignmentId : 0,

                   //
                   // Data to fill the controls
                   //
                   ClientParticipantTypeId = thePanelUserAssignment.FirstOrDefault().ClientParticipantTypeId,
                   ClientAssignmentTypeId = thePanelUserAssignment.FirstOrDefault().PanelApplicationReviewerAssignments.Where(q => q.PanelApplicationId == panelApplicationId).DefaultIfEmpty(PanelApplicationReviewerAssignment.Default).First().ClientAssignmentTypeId,
                   SortOrder = thePanelUserAssignment.FirstOrDefault().PanelApplicationReviewerAssignments.Where(q => q.PanelApplicationId == panelApplicationId).DefaultIfEmpty(PanelApplicationReviewerAssignment.Default).First().NullableSortOrder,
                   ClientExpertiseRatingId = (thePanelApplicationReviewerExpertise != null) ? thePanelApplicationReviewerExpertise.ClientExpertiseRatingId : null,
                   CoiTypelId = (thePanelApplicationReviewerExpertise != null) ? thePanelApplicationReviewerExpertise.PanelApplicationReviewerCoiDetails.Where(w => w.PanelApplicationReviewerExpertiseId == thePanelApplicationReviewerExpertise.PanelApplicationReviewerExpertiseId).DefaultIfEmpty(PanelApplicationReviewerCoiDetail.Default).First().NullableClientCoiTypeId : null,

                   ExpertiseComments = (thePanelApplicationReviewerExpertise != null) ? thePanelApplicationReviewerExpertise.ExpertiseComments : string.Empty,
                   AssignmentTypeId = thePanelUserAssignment.FirstOrDefault().PanelApplicationReviewerAssignments.Where(q => q.PanelApplicationId == panelApplicationId).DefaultIfEmpty(PanelApplicationReviewerAssignment.Default).First().ClientAssignmentType.NullableAssignmentTypeId
               }
            );

            return result;
        }
        /// <summary>
        /// Returns a list of all the panel reviewers emails
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelId">Session panel identifier (tells which session)</param
        /// <returns>Enumerable list of IEmailAddress objects</returns>
        internal static IEnumerable<IEmailAddress> ListPanelUserEmailAddresses(IQueryable<SessionPanel> entityContext, int sessionPanelId)
        {
            //
            // Get the session panel by Id
            //
            SessionPanel theSession = entityContext.FirstOrDefault(x => x.SessionPanelId == sessionPanelId);
            //
            // Then for each reviewer on the panel get there email address
            //
            var result = theSession.PanelUserAssignments.Select(
                z => new EmailAddress
                {
                    //
                    // Set the identifying information
                    //
                    SessionPanelId = sessionPanelId,
                    PanelUserAssignmentId = z.PanelUserAssignmentId,
                    ParticipantTypeAbbreviation = z.ClientParticipantType.ParticipantTypeAbbreviation,
                    //
                    // Data to fill the controls
                    // 

                    UserEmailAddress = z.User.PrimaryUserEmailAddress(),
                    FirstName = z.User.UserInfoes.DefaultIfEmpty(UserInfo.Default).First().FirstName,
                    LastName = z.User.UserInfoes.DefaultIfEmpty(UserInfo.Default).First().LastName
                }
                );
            return result;
        }
        #endregion
        #region Critique Management Services
        /// Returns a panel's Applications Critique Information by panel or application
        /// </summary>
        /// <param name="context">P2RMISNetEntities dbContext</param>
        /// <param name="panelApplicationId">Identifier for a panel application assignment</param>
        /// <returns>ApplicationCritique query</returns>
        /// <remarks>At least sessionPanelId or panelApplicationId are required</remarks>
        internal static IEnumerable<Webmodel.IApplicationCritiqueModel> GetApplicationCritiques(
            P2RMISNETEntities context, int? sessionPanelId, int? panelApplicationId)
        {
            if (!sessionPanelId.HasValue && !panelApplicationId.HasValue)
            {
                throw new ArgumentException($"Either sessionPanelId or panelApplicationId must not be null. sessionPanelId is: {sessionPanelId.ToString()} panelApplicationId is: {sessionPanelId.ToString()}");
            }
            var result = (from panApp in context.PanelApplications
                          join app in context.Applications on panApp.ApplicationId equals app.ApplicationId
                          join panStage in context.PanelStages on
                              new { panApp.SessionPanelId, ReviewStageId = ReviewStage.Asynchronous } equals
                              new { panStage.SessionPanelId, panStage.ReviewStageId }
                          join appStage in context.ApplicationStages on new { panApp.PanelApplicationId, panStage.ReviewStageId }
                              equals new { appStage.PanelApplicationId, appStage.ReviewStageId }
                          where (panApp.SessionPanelId == sessionPanelId || sessionPanelId == null) && (panApp.PanelApplicationId == panelApplicationId || panelApplicationId == null)
                          select new Webmodel.ApplicationCritiqueModel
                          {
                              PanelApplicationId = panApp.PanelApplicationId,
                              PiLastName = app.ApplicationPersonnels.FirstOrDefault(x => x.PrimaryFlag).LastName,
                              PiFirstName = app.ApplicationPersonnels.FirstOrDefault(x => x.PrimaryFlag).FirstName,
                              PiInstitution = app.ApplicationPersonnels.FirstOrDefault(x => x.PrimaryFlag).OrganizationName,
                              AwardAbbreviation = app.ProgramMechanism.ClientAwardType.AwardAbbreviation,
                              ReviewerCritiques = (from para in panApp.PanelApplicationReviewerAssignments
                                                   join pua in context.PanelUserAssignments on para.PanelUserAssignmentId equals
                                                       pua.PanelUserAssignmentId
                                                   join user in context.Users on pua.UserId equals user.UserID
                                                   join userInfo in context.UserInfoes on user.UserID equals userInfo.UserID
                                                   orderby para.SortOrder ?? PanelApplicationReviewerAssignment.DefaultSortOrder, userInfo.LastName
                                                   select new Webmodel.ReviewerCritiquePhase
                                                   {
                                                       ReviewerFirstName = userInfo.FirstName,
                                                       ReviewerLastName = userInfo.LastName,
                                                       ReviewerId = userInfo.UserID,
                                                       AssignmentAbbreviation = para.ClientAssignmentType.AssignmentAbbreviation,
                                                       AssignmentDescription = para.ClientAssignmentType.AssignmentLabel,
                                                       IsCoi = para.ClientAssignmentType.AssignmentType.AssignmentTypeId == AssignmentType.COI,
                                                       IsReader = para.ClientAssignmentType.AssignmentType.AssignmentTypeId == AssignmentType.Reader,
                                                       AssignmentOrder = para.SortOrder ?? 0,
                                                       ReviewerEmailAddress = userInfo.UserEmails.FirstOrDefault(e => e.PrimaryFlag).Email,
                                                       CritiquePhases = (from aw in context.ApplicationWorkflows
                                                                         join aws in context.ApplicationWorkflowSteps on aw.ApplicationWorkflowId equals
                                                                             aws.ApplicationWorkflowId
                                                                         join pss in context.PanelStageSteps on new { panStage.PanelStageId, aws.StepTypeId }
                                                                             equals new { pss.PanelStageId, pss.StepTypeId }
                                                                         join awse in context.ApplicationWorkflowStepElements on aws.ApplicationWorkflowStepId equals awse.ApplicationWorkflowStepId into awseg
                                                                         from awse in awseg.Where(x => x.ApplicationTemplateElement.MechanismTemplateElement.OverallFlag).DefaultIfEmpty()
                                                                         join awsec in context.ApplicationWorkflowStepElementContents on awse.ApplicationWorkflowStepElementId equals awsec.ApplicationWorkflowStepElementId into awsecg
                                                                         from awsec in awsecg.DefaultIfEmpty()
                                                                         join css in context.ClientScoringScales on awse.ClientScoringId equals css.ClientScoringId into cssg
                                                                         from css in cssg.DefaultIfEmpty()
                                                                         join cssa in context.ClientScoringScaleAdjectivals on new { css.ClientScoringId, awsec.Score } equals new { cssa.ClientScoringId, Score = (decimal?)cssa.NumericEquivalent } into cssag
                                                                         from cssa in cssag.DefaultIfEmpty()
                                                                         orderby pss.StepOrder
                                                                         where
                                                                             aw.ApplicationStageId == appStage.ApplicationStageId &&
                                                                             aw.PanelUserAssignmentId == pua.PanelUserAssignmentId
                                                                         select new Webmodel.CritiquePhaseInformation
                                                                         {
                                                                             ApplicationWorkflowId = aw.ApplicationWorkflowId,
                                                                             ApplicationWorkflowStepId = aws.ApplicationWorkflowStepId,
                                                                             StepOrder = aws.StepOrder,
                                                                             StepTypeId = aws.StepTypeId,
                                                                             PhaseStartDate = pss.StartDate ?? DateTime.MinValue,
                                                                             PhaseEndDate = pss.EndDate ?? DateTime.MaxValue,
                                                                             ReOpenStartDate = pss.ReOpenDate,
                                                                             ReOpenEndDate = pss.ReCloseDate,
                                                                             IsSubmitted = aws.Resolution,
                                                                             DateSubmitted = aws.ResolutionDate,
                                                                             ScoreRating = awsec.Score ?? 0,
                                                                             AdjectivalRating = cssa.ScoreLabel,
                                                                             ScoreType = css.ScoreType,
                                                                             MaxStepOrder = (int?)aw.ApplicationWorkflowSteps.Where(o => o.ApplicationWorkflowStepElements.SelectMany(x => x.ApplicationWorkflowStepElementContents).Any()).Max(x => x.StepOrder) ?? 0,
                                                                             ContentExists = aws.ApplicationWorkflowStepElements.SelectMany(x => x.ApplicationWorkflowStepElementContents).Any(),
                                                                             MaxSubmittedStepOrder = (int?)aw.ApplicationWorkflowSteps.Where(x => x.Resolution && panStage.PanelStageSteps.Where(y => y.StepTypeId == x.StepTypeId).FirstOrDefault().StartDate < GlobalProperties.P2rmisDateTimeNow).OrderByDescending(y => y.StepOrder).FirstOrDefault().StepOrder ?? 0,
                                                                         })
                                                   })
                          });
            return result;
        }
        /// <summary>
        /// Gets the panel stage step's status data.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>PanelStageStepModel containing step and application status information</returns>
        internal static IEnumerable<Webmodel.PanelStageStepModel> GetPanelStageStepStatus (P2RMISNETEntities context, int sessionPanelId)
        {
            var results = from pss in context.PanelStageSteps
                          join ps in context.PanelStages on pss.PanelStageId equals ps.PanelStageId
                          join sp in context.SessionPanels on ps.SessionPanelId equals sp.SessionPanelId
                          join pa in context.PanelApplications on sp.SessionPanelId equals pa.SessionPanelId into paG
                          from pa in paG.DefaultIfEmpty()
                          join appStage in context.ApplicationStages on new { pa.PanelApplicationId, ps.ReviewStageId } equals new { appStage.PanelApplicationId, appStage.ReviewStageId } into appStageG
                          from appStage in appStageG.DefaultIfEmpty()
                          join appStageStep in context.ApplicationStageSteps on new { appStage.ApplicationStageId, pss.PanelStageStepId } equals new { appStageStep.ApplicationStageId, appStageStep.PanelStageStepId } into appStageStepG
                          from appStageStep in appStageStepG.DefaultIfEmpty()
                          where sp.SessionPanelId == sessionPanelId && ps.ReviewStageId == ReviewStage.Asynchronous
                          select new Webmodel.PanelStageStepModel
                          {
                              StageStepId = pss.PanelStageStepId,
                              StageTypeId = pss.StepTypeId,
                              StepOrder = pss.StepOrder,
                              StartDate = pss.StartDate,
                              EndDate = pss.EndDate,
                              ReOpenDate = pss.ReOpenDate,
                              ReCloseDate = pss.ReCloseDate,
                              CritiqueAssignmentCount = pa.PanelApplicationReviewerAssignments.Count(x => AssignmentType.CritiqueAssignments.Contains(x.ClientAssignmentType.AssignmentTypeId)),
                              CritiqueAssignmentSubmittedCount = appStage.ApplicationWorkflows.SelectMany(x => x.ApplicationWorkflowSteps).Count(x => x.StepTypeId == pss.StepTypeId && x.Resolution),
                              StepName = pss.StepName,
                              IsModPhase = pss.StepTypeId == StepType.Indexes.Final,
                              IsModActive = pss.StepTypeId == StepType.Indexes.Final && (((GlobalProperties.P2rmisDateTimeNow >= pss.StartDate.Value && GlobalProperties.P2rmisDateTimeNow < pss.EndDate.Value) ||
                              (pss.ReOpenDate != null && GlobalProperties.P2rmisDateTimeNow >= pss.ReOpenDate && pss.ReCloseDate != null && GlobalProperties.P2rmisDateTimeNow < pss.ReCloseDate)) && appStageStep.ApplicationStageStepDiscussions.Select(x => x.ApplicationStageStepDiscussionComments).Count() > 0),
                              IsModReady = pss.StepTypeId == StepType.Indexes.Final && ((GlobalProperties.P2rmisDateTimeNow >= pss.StartDate.Value && GlobalProperties.P2rmisDateTimeNow < pss.EndDate.Value) ||
                              (pss.ReOpenDate != null && GlobalProperties.P2rmisDateTimeNow >= pss.ReOpenDate && pss.ReCloseDate != null && GlobalProperties.P2rmisDateTimeNow < pss.ReCloseDate)),
                              IsModClosed = pss.StepTypeId == StepType.Indexes.Final && (GlobalProperties.P2rmisDateTimeNow > (pss.ReCloseDate ?? DateTime.MinValue) && GlobalProperties.P2rmisDateTimeNow > pss.EndDate.Value),
                              ApplicationStageStepId = appStageStep.ApplicationStageStepId,
                              PanelApplicationId = pa.PanelApplicationId
                          };
            return results;
        }
        /// <summary>
        /// Retrieves the Applications Critique Details
        /// </summary>
        /// <param name="context">P2RMISNetEntities dbContext</param>
        /// <param name="applicationWorkflowStepId">ApplicationWorkflowStep identifier</param>
        /// <returns>A single ApplicationCritiqueDetails object</returns>
        internal static Webmodel.IApplicationCritiqueDetailsModel GetApplicationCritiqueDetails(P2RMISNETEntities context, int applicationWorkflowStepId)
        {
            var results = (from aws in context.ApplicationWorkflowSteps
                join aw in context.ApplicationWorkflows on aws.ApplicationWorkflowId equals aw.ApplicationWorkflowId
                join pua in context.PanelUserAssignments on aw.PanelUserAssignmentId equals pua.PanelUserAssignmentId
                join sp in context.SessionPanels on pua.SessionPanelId equals sp.SessionPanelId
                join ms in context.MeetingSessions on sp.MeetingSessionId equals ms.MeetingSessionId
                join cm in context.ClientMeetings on ms.ClientMeetingId equals cm.ClientMeetingId
                join appStage in context.ApplicationStages on aw.ApplicationStageId equals appStage.ApplicationStageId
                join panApp in context.PanelApplications on appStage.PanelApplicationId equals panApp.PanelApplicationId
                join app in context.Applications on panApp.ApplicationId equals app.ApplicationId
                join pm in context.ProgramMechanism on app.ProgramMechanismId equals pm.ProgramMechanismId
                join py in context.ProgramYears on pm.ProgramYearId equals py.ProgramYearId
                join cat in context.ClientAwardTypes on pm.ClientAwardTypeId equals cat.ClientAwardTypeId
                join ui in context.UserInfoes on pua.UserId equals ui.UserID
                join ap in context.ApplicationPersonnels on new { app.ApplicationId, PrimaryFlag = true } equals new { ap.ApplicationId, ap.PrimaryFlag }
                join awse in context.ApplicationWorkflowStepElements on aws.ApplicationWorkflowStepId equals awse.ApplicationWorkflowStepId into awseg
                 from awse in awseg.Where(x => x.ApplicationTemplateElement.MechanismTemplateElement.OverallFlag).DefaultIfEmpty()		
                join awsec in context.ApplicationWorkflowStepElementContents on awse.ApplicationWorkflowStepElementId equals awsec.ApplicationWorkflowStepElementId into awsecg
                 from awsec in awsecg.DefaultIfEmpty()
                join css in context.ClientScoringScales on awse.ClientScoringId equals css.ClientScoringId into cssg
                from css in cssg.DefaultIfEmpty()
                join cssa in context.ClientScoringScaleAdjectivals on new { css.ClientScoringId, Score = awsec.Score}
                    equals new { cssa.ClientScoringId, Score = (decimal?)cssa.NumericEquivalent} into cssag
                from cssa in cssag.DefaultIfEmpty()


                where aws.ApplicationWorkflowStepId == applicationWorkflowStepId

                select new Webmodel.ApplicationCritiqueDetailsModel
                { 
				    MeetingDescription = cm.MeetingDescription,
                    ProgramYear  = py.Year,
                    AwardTitle  = cat.AwardDescription,
                    IsSubmitted = aws.Resolution,
                    ScoreRating = awsec.Score ?? 0,
                    AdjectivalRating = cssa.ScoreLabel,
                    ScoreType = css.ScoreType,
                    ReviewerLastName  = ui.LastName,
                    ReviewerFirstName  = ui.FirstName,
                    PiLastName  = ap.LastName,
                    ApplicationLogNumber  = app.LogNumber,
                    ApplicationTitle = app.ApplicationTitle,

                    ReviewerCritiques = from ate in context.ApplicationTemplateElements 		
						join awseFull in context.ApplicationWorkflowStepElements on new { ApplicationWorkflowStepId = aws.ApplicationWorkflowStepId, ate.ApplicationTemplateElementId } equals 
							new { awseFull.ApplicationWorkflowStepId, awseFull.ApplicationTemplateElementId } into awsegFull
							from awseFull in awsegFull.Where(x => !x.ApplicationTemplateElement.MechanismTemplateElement.OverallFlag).DefaultIfEmpty() 
						join mte in context.MechanismTemplateElements on ate.MechanismTemplateElementId equals mte.MechanismTemplateElementId 
						join awsecFull in context.ApplicationWorkflowStepElementContents on awseFull.ApplicationWorkflowStepElementId equals awsecFull.ApplicationWorkflowStepElementId
						join ce in context.ClientElements on mte.ClientElementId equals ce.ClientElementId
						where awsecFull.ContentText != null
                               orderby mte.SortOrder
                        select new Webmodel.CritiqueSection
                        {
         					Title = ce.ElementDescription,
							Instructions = mte.InstructionText,
                            TextFlag = mte.TextFlag,
							Text = 	awsecFull.ContentText,
                            ScoreFlag = mte.ScoreFlag,
				            Score = awsecFull.Score
						}
                }).FirstOrDefault();
                return results;
        }
        /// <summary>
        /// List all of the ApplicationWorkFlowSteps that have submit-able critiques.
        /// </summary>
        /// <param name="PanelUserAssignments">Entity framework Query-able context entry for PanelUserAssignments</param>
        /// <param name="sessionPanelId">SessionPanel identifier (identifies the panel)</param>
        /// <param name="stepTypeId">StepType identifier (identifies the phase)</param>
        /// <returns>Enumerable collection of workflow steps with workflow steps that could have submit-able critiques</returns>
        internal static IEnumerable<ApplicationWorkflowStep> ListSessionPanelSubmittableWorkflowSteps(IQueryable<PanelUserAssignment> PanelUserAssignments, int sessionPanelId, int stepTypeId)
        {
            //
            // First find all of the user assignments for this specified session panel
            //
            var results = PanelUserAssignments.Where(s => s.SessionPanelId == sessionPanelId)
                //
                // Then get all of the workflow's
                //
            .SelectMany(s => s.ApplicationWorkflows)
                //
                // Then all of the individual workflow's steps
                //
            .SelectMany(x => x.ApplicationWorkflowSteps)
                //
                // then filter by the stepType
                //
            .Where(t => t.StepTypeId == stepTypeId)
                //
                // And finally filter out the workflow steps that already have their critiques submitted
                //
            .Where(t => t.Resolution != true);
            
            return results;
        }
        #endregion
        #region Reviewer Search Helpers

        /// <summary>
        /// Gets the reviewer search results as query-able.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <returns>Query-able representation of a pre-filtered reviewer search</returns>
        internal static IQueryable<Webmodel.IReviewerSearchResultModel> GetReviewerSearchResults(P2RMISNETEntities context, int panelId)
        {
            // Grabs the clientProgramIds related to the referenced panelId
            var panelClientPrograms = (from panel in context.SessionPanels
                                  join programPanel in context.ProgramPanels on panel.SessionPanelId equals programPanel.SessionPanelId
                                  join programYear in context.ProgramYears on programPanel.ProgramYearId equals programYear.ProgramYearId
                                       where panel.SessionPanelId == panelId
                                       select programYear.ClientProgramId);
            var clientId = context.SessionPanels.Where(x => x.SessionPanelId == panelId).FirstOrDefault().ClientId();
            // Main search results query
            var result = (from user in context.Users
                join userInfo in context.UserInfoes on user.UserID equals userInfo.UserID
                join academicRank in context.AcademicRanks on userInfo.AcademicRankId equals academicRank.AcademicRankId into academicRank2
                from academicRank in academicRank2.DefaultIfEmpty()
                join gender in context.Genders on userInfo.GenderId equals gender.GenderId into gender2
                from gender in gender2.DefaultIfEmpty()
                join ethnicity in context.Ethnicities on userInfo.EthnicityId equals ethnicity.EthnicityId into ethnicity2
                from ethnicity in ethnicity2.DefaultIfEmpty()
                join userResume in context.UserResumes on userInfo.UserInfoID equals userResume.UserInfoId into userResume2
                from userResume in userResume2.DefaultIfEmpty()
                join userWebsite in context.UserWebsites on new { UserInfoId = userInfo.UserInfoID, WebsiteTypeId = WebsiteType.PrimaryWebsiteTypeId } equals new { userWebsite.UserInfoId, userWebsite.WebsiteTypeId} into userWebsite2
                from userWebsite in userWebsite2.DefaultIfEmpty()
                join militaryRank in context.MilitaryRanks on userInfo.MilitaryRankId equals militaryRank.MilitaryRankId into militaryRank2
                from militaryRank in militaryRank2.DefaultIfEmpty()
                join militaryStatus in context.MilitaryStatusTypes on userInfo.MilitaryStatusTypeId equals militaryStatus.MilitaryStatusTypeId into militaryStatus2
                from militaryStatus in militaryStatus2.DefaultIfEmpty()
                join address in context.UserAddresses on new { userInfo.UserInfoID, PrimaryFlag = true } equals new { address.UserInfoID, address.PrimaryFlag } into address2
                from address in address2.DefaultIfEmpty()
                where user.UserSystemRoles.Any(x => x.SystemRole.SystemRoleId == SystemRole.Indexes.Reviewer
                            || x.SystemRole.SystemRoleId == SystemRole.Indexes.CpritChair)
                select new Webmodel.ReviewerSearchResultModel
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Suffix = userInfo.SuffixText,
                    UserId = user.UserID,
                    UserInfoId = userInfo.UserInfoID,
                    UserName = user.UserLogin,
                    IsBlocked = userInfo.UserClientBlocks.Any(x => x.ClientId == clientId),
                    AcademicRank = academicRank.Rank,
                    AcademicRankId = userInfo.AcademicRankId,
                    EthnicityId = userInfo.EthnicityId,
                    Ethnicity = ethnicity.EthnicityLabel,
                    Expertise = userInfo.Expertise,
                    Gender = gender.Gender1,
                    GenderId = userInfo.GenderId,
                    StateId = address.StateId,
                    IsPotentialChair = user.PanelUserAssignments.SelectMany(x => x.ReviewerEvaluations).Any(x => x.RecommendChairFlag),
                    IsPreviouslyParticipated = user.PanelUserAssignments.Any(x => x.SessionPanel.ProgramPanels.Any(y => panelClientPrograms.Contains(y.ProgramYear.ClientProgramId))),
                    IsProgramUser = user.ProgramUserAssignments.Any(x => panelClientPrograms.Contains(x.ProgramYear.ClientProgramId)),
                    UserResumeId = userResume.UserResumeId,
                    UserResumeFileName = userResume.FileName,
                    PreferredWebsiteAddress = userWebsite.WebsiteAddress,
                    HasCommunicationLog = user.UserCommunicationLogs.Any(),
                    Rating = user.PanelUserAssignments.SelectMany(x => x.ReviewerEvaluations).Average(x => x.Rating),
                    Organization = userInfo.Institution,
                    Position = userInfo.Position,
                    MilitaryRank = militaryRank.MilitaryRankName,
                    MilitaryBranch = militaryRank.Service,
                    MilitaryStatus = militaryStatus.StatusType,
                    IsOnPanel = user.PanelUserAssignments.Any(x => x.SessionPanelId == panelId) || user.PanelUserPotentialAssignments.Any(x => x.SessionPanelId == panelId),
                    Participation = (from userPanelAssignment in user.PanelUserAssignments
                                    join sessionPanel in context.SessionPanels on userPanelAssignment.SessionPanelId equals sessionPanel.SessionPanelId
                                    join programPanel in context.ProgramPanels on sessionPanel.SessionPanelId equals programPanel.SessionPanelId
                                    select new Webmodel.ParticipationModel
                                    {
                                            PanelName = sessionPanel.PanelName,
                                            PanelAbbreviation = sessionPanel.PanelAbbreviation,
                                            ClientProgramId = programPanel.ProgramYear.ClientProgramId,
                                            ProgramYearId = programPanel.ProgramYearId,
                                            ClientRoleId = userPanelAssignment.ClientRoleId,
                                            ClientParticipantTypeId = userPanelAssignment.ClientParticipantTypeId,         
                                    })
                                     
            });
            return result;
        }
        /// <summary>
        /// Gets the staff search results.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Webmodel.IStaffSearchResultModel> GetStaffSearchResults(P2RMISNETEntities context, int panelId)
        {
            // Grabs the clientProgramIds related to the referenced panelId
            int clientId = (from panel in context.SessionPanels
                                    join programPanel in context.ProgramPanels on panel.SessionPanelId equals programPanel.SessionPanelId
                                    join programYear in context.ProgramYears on programPanel.ProgramYearId equals programYear.ProgramYearId
                                    where panel.SessionPanelId == panelId
                                    select programYear.ClientProgram.ClientId).First();
            // Main search results query
            var result = (from user in context.Users
                          join userInfo in context.UserInfoes on user.UserID equals userInfo.UserID
                          join userClient in context.UserClients on user.UserID equals userClient.UserID
                          where user.UserSystemRoles.Any(x => x.SystemRole.SystemRoleId == SystemRole.Indexes.SRO || x.SystemRole.SystemRoleId == SystemRole.Indexes.RTA
                            || x.SystemRole.SystemRoleId == SystemRole.Indexes.CpritChair) && userClient.ClientID == clientId
                          select new Webmodel.StaffSearchResultModel
                          {
                              FirstName = userInfo.FirstName,
                              LastName = userInfo.LastName,
                              UserId = user.UserID,
                              UserInfoId = userInfo.UserInfoID,
                              UserName = user.UserLogin,
                              Organization = userInfo.Institution,
                              Email = userInfo.UserEmails.Where(x => x.PrimaryFlag == true).FirstOrDefault() != null ?
                                    userInfo.UserEmails.Where(x => x.PrimaryFlag == true).FirstOrDefault().Email : String.Empty,
                              Role = user.UserSystemRoles.FirstOrDefault().SystemRole.SystemRoleName,
                              IsOnPanel = user.PanelUserAssignments.Any(x => x.SessionPanelId == panelId),
                          });
            return result;
        }
        /// <summary>
        /// Applies the resume search.
        /// </summary>
        /// <param name="filteredResult">The filtered result.</param>
        /// <param name="resume">The resume text to search for, comma delimited.</param>
        /// <returns>Query-able result filtered to include only results found within the resume</returns>
        /// <remarks>
        /// EF does not let us loop over a table valued function call or make a contains call explicitly. The workaround is to define a set number of max search terms for
        /// the function and call once providing all parameters...
        /// </remarks>
        internal static IQueryable<Webmodel.IReviewerSearchResultModel> ApplyResumeSearch(P2RMISNETEntities context, IQueryable<Webmodel.IReviewerSearchResultModel> filteredResult, string resume)
        {
            if (!string.IsNullOrWhiteSpace(resume))
            {
                var searchParameters = new string[5];
                List<string> resumeList = resume.Split(',').Select(x => $"\"{x.Trim()}\"").ToList();
                for (var item=0; item < resumeList.Count() && item < searchParameters.Length; item++ )
                {
                    searchParameters[item] = resumeList[item];
                }
                var fullTextResult = context.uspResumeFullTextSearch(searchParameters[0], searchParameters[1], searchParameters[2], searchParameters[3], searchParameters[4]).Select(x => x.UserInfoId).ToList();
                filteredResult = filteredResult.Where(x => fullTextResult.Contains(x.UserInfoId));
            }
            
            return filteredResult;
        }

        /// <summary>
        /// Gets the panel stage information.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>Result model containing panel stage info</returns>
        internal static PanelStageResultModel GetPanelStageInformation(P2RMISNETEntities context, int sessionPanelId)
        {
            var result = new PanelStageResultModel
            {
                AssignmentsReleased = context.PanelApplications.Where(x => x.SessionPanelId == sessionPanelId).SelectMany(x => x.ApplicationStages).Any(x => x.AssignmentVisibilityFlag),
                PanelStageAndSteps = context.PanelStages.Include("PanelStageSteps").Where(x => x.SessionPanelId == sessionPanelId).ToList()
            };
            return result;
        }
        #endregion
    }
}
