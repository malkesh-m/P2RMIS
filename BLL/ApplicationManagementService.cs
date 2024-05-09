using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.ApplicationManagement;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Bll.Views.ApplicationDetails;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.Bll.ApplicationScoring;
using Application = Sra.P2rmis.Dal.Application;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Application Management Service.  Services provided are:
    ///      - Retrieval of the open program for the specified user
    /// </summary>
    public class ApplicationManagementService : ServerBase, IApplicationManagementService
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationManagementService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion
        #region Provided Services
        /// <summary>
        /// Generate a list of open Programs.
        /// </summary>
        /// <param name="clientList">TODO:: document</param>
        public ApplicationManagementView GetOpenProgramsList(List<int> clientList)
        {
            //
            // Create the service layer view
            // 
            var view = new ApplicationManagementView();
            //
            // Retrieve the data we are supposed to retrieve & shove it into the 
            // our view
            // 
            view.Programs = UnitOfWork.ProgramRepository.GetOpenProgramsList(clientList);
            //
            // return the view to the controller
            // 
            return view;
        }
        /// <summary>
        /// Generate a list of assigned open Programs.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        public ApplicationManagementView GetAssignedOpenProgramsList(int userId)
        {
            //Retrieve the users person Id
            var user = UnitOfWork.UofwUserRepository.GetUserByID(userId);
            
            //
            // Create the service layer view
            // 
            var view = new ApplicationManagementView();
            //
            // Retrieve the data we are supposed to retrieve & shove it into the 
            // our view
            // 
            view.Programs = UnitOfWork.ProgramRepository.GetAssignedOpenProgramsList(userId);
            //
            // return the view to the controller
            // 
            return view;
        }
        /// <summary>
        /// Retrieve the application's details.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="panelId">Panel identifier</param>
        /// <param name="commentTypes">TODO:: document</param>
        /// <returns>TODO:: document</returns>
        public ApplicationDetailsContainer GetApplicationDetails(int panelApplicationId, int panelId, List<int> commentTypes)
        {
            //
            // Retrieve the data we are supposed to retrieve & shove it into our view
            //  
            var results = UnitOfWork.ProgramRepository.GetApplicationDetail(panelApplicationId, panelId, commentTypes);
            var view = new ApplicationDetailsContainer(results, AdjectivalLabelForScore);
            //
            // return the view to the controller
            // 
            return view;
        }
        /// <summary>
        /// Determines the adjectival value for a numeric score.
        /// </summary>
        /// <param name="clientScoringId">ClientScoring entity identifier</param>
        /// <param name="numericEquivalent">Adjectival numeric equivalent</param>
        /// <returns>Adjectival value</returns>
        public string AdjectivalLabelForScore(int clientScoringId, int numericEquivalent)
        {
            var clientScoringScaleAdjectivalEntity = UnitOfWork.ClientScoringScaleAdjectivalRepository.Get(x => ((x.ClientScoringId == clientScoringId) && (x.NumericEquivalent == numericEquivalent))).FirstOrDefault();
            return (clientScoringScaleAdjectivalEntity != null)?  clientScoringScaleAdjectivalEntity.ScoreLabel: string.Empty;
        }
        /// <summary>
        /// Add a comment
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="comments">Comments</param>
        /// <param name="commentType">CommentType entity identifier</param>
        public void AddComment(int userId, int panelApplicationId, string comments, int commentType)
        {
            //
            // Convert the business layer comment view into a data layer object.
            // Then add it to the context & save it.
            // 
            UserApplicationComment newComment = ServiceHelpers.CreateComment(userId, panelApplicationId, comments, commentType);

            UnitOfWork.UserApplicationCommentRepository.Add(newComment);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Edit a user comment.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="commentId">TODO:: document</param>
        /// <param name="comments">User comments</param>
        /// <param name="commentType">TODO:: document</param>
        public void EditComment(int userId, int commentId, string comments, int commentType)
        {
            //
            // Get the current comment & updated the context then save 
            // 
            UserApplicationComment comment = ServiceHelpers.EditComment(userId, commentId, comments, commentType, UnitOfWork.UserApplicationCommentRepository);
            UnitOfWork.UserApplicationCommentRepository.Update(comment);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Delete a user's comment
        /// </summary>
        /// <param name="commentId">Comment identifier</param>
        public void DeleteComment(int commentId)
        {
            UnitOfWork.UserApplicationCommentRepository.Delete(commentId);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Get the scoring legend for this panel application for the synchronous (online scoring) stage
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>ClientScoringScaleLegendModel</returns>
        public ClientScoringScaleLegendModel GetScoringLegendSyncStage(int panelApplicationId)
        {
            return GetScoringLegend(panelApplicationId, ReviewStage.Synchronous);
        }
        /// <summary>
        /// Get the scoring legend for this panel application's current review stage
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>ClientScoringScaleLegendModel</returns>
        public ClientScoringScaleLegendModel GetScoringLegendCurrentStage(int panelApplicationId)
        {
            var currentStageId = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId).GetCurrentReviewStage();
            return GetScoringLegend(panelApplicationId, currentStageId);
        }
        /// <summary>
        /// Get the scoring legend for this client
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <param name="reviewStageId">The review stage identifier</param>
        /// <returns>ClientScoringScaleLegendModel</returns>
        internal ClientScoringScaleLegendModel GetScoringLegend(int panelApplicationId, int reviewStageId)
        {
            ValidateInt(panelApplicationId, "ApplicationManagementService.GetScoringLegend", "panelApplicationId");
            ValidateInt(reviewStageId, "ApplicationManagementService.GetScoringLegend", "reviewStageId");
            // get base query

            var elements = UnitOfWork.PanelApplicationRepository.GetByIDWithScoreLegend(panelApplicationId).Application.ProgramMechanism.MechanismTemplates.Where(y => y.ReviewStageId == reviewStageId).FirstOrDefault().
               MechanismTemplateElements;
            IEnumerable<IScoringScaleLegendModel> overall = Enumerable.Empty<IScoringScaleLegendModel>();

            // overall query
            var overallLabel = String.Empty;
            if (elements.Any(x => x.OverallFlag && x.ScoreFlag))
            {
                ClientScoringScale clientScoringScaleEntity = elements.FirstOrDefault(x => x.OverallFlag && x.ScoreFlag).MechanismTemplateElementScorings.FirstOrDefault()?.ClientScoringScale;
                if (clientScoringScaleEntity?.ClientScoringScaleLegend != null)
                {
                    overall = elements.FirstOrDefault(x => x.OverallFlag && x.ScoreFlag).MechanismTemplateElementScorings.FirstOrDefault().ClientScoringScale.ClientScoringScaleLegend.ClientScoringScaleLegendItems.Select(x => new ScoringScaleLegendModel
                    {
                        HighValue = x.HighValueLabel,
                        LowValue = x.LowValueLabel,
                        LegendItemLabel = x.LegendItemLabel
                    });
                    overallLabel = elements.FirstOrDefault(x => x.OverallFlag && x.ScoreFlag).MechanismTemplateElementScorings.FirstOrDefault().ClientScoringScale.ClientScoringScaleLegend.LegendName;
                }
            }

            // criterion query
            IEnumerable<IScoringScaleLegendModel> criterion = Enumerable.Empty<IScoringScaleLegendModel>();
            var criterionLabel = String.Empty;
            if (elements.Any(x => !x.OverallFlag && x.ScoreFlag))
            {
                var legend = elements.FirstOrDefault(x => !x.OverallFlag && x.ScoreFlag).MechanismTemplateElementScorings.FirstOrDefault().ClientScoringScale.ClientScoringScaleLegend;
                if (legend != null)
                {
                    criterion = legend.ClientScoringScaleLegendItems.Select(x => new ScoringScaleLegendModel
                    {
                        HighValue = x.HighValueLabel,
                        LowValue = x.LowValueLabel,
                        LegendItemLabel = x.LegendItemLabel
                    });
                    criterionLabel = legend.LegendName;
                }
            }

            var result = new ClientScoringScaleLegendModel
            {
                Overall = overall,
                OverallScaleLabel = overallLabel,
                Criterion = criterion,
                CriterionScaleLabel = criterionLabel
            };
            return result;              
        }
        /// <summary>
        /// Get the scoring legend by mechanism template identifier.
        /// </summary>
        /// <param name="mechanismTemplateId">Mechanism template identifier.</param>
        /// <returns></returns>
        public ClientScoringScaleLegendModel GetScoringLegendByMechanismTemplateId(int mechanismTemplateId)
        {
            ValidateInt(mechanismTemplateId, "SetupService.GetScoringLegend", "mechanismTemplateId");
            var elements = UnitOfWork.MechanismTemplateRepository.GetByID(mechanismTemplateId).MechanismTemplateElements;
            return GetScoringLegendByMechanismTemplateElements(elements);
        }
        /// <summary>
        /// Get the scoring legend by mechanism template elements.
        /// </summary>
        /// <param name="elements">Mechanism template elements.</param>
        /// <returns></returns>
        private ClientScoringScaleLegendModel GetScoringLegendByMechanismTemplateElements(ICollection<MechanismTemplateElement> elements)
        {
            IEnumerable<IScoringScaleLegendModel> overall = Enumerable.Empty<IScoringScaleLegendModel>();

            // Overall query
            var overallLabel = String.Empty;
            if (elements.Any(x => x.OverallFlag && x.ScoreFlag))
            {
                ClientScoringScale clientScoringScaleEntity = elements.FirstOrDefault(x => x.OverallFlag && x.ScoreFlag).MechanismTemplateElementScorings.FirstOrDefault()?.ClientScoringScale;
                if (clientScoringScaleEntity?.ClientScoringScaleLegend != null)
                {
                    overall = elements.FirstOrDefault(x => x.OverallFlag && x.ScoreFlag).MechanismTemplateElementScorings.FirstOrDefault().ClientScoringScale.ClientScoringScaleLegend.ClientScoringScaleLegendItems.Select(x => new ScoringScaleLegendModel
                    {
                        HighValue = x.HighValueLabel,
                        LowValue = x.LowValueLabel,
                        LegendItemLabel = x.LegendItemLabel
                    });
                    overallLabel = elements.FirstOrDefault(x => x.OverallFlag && x.ScoreFlag).MechanismTemplateElementScorings.FirstOrDefault().ClientScoringScale.ClientScoringScaleLegend.LegendName;
                }
            }

            // Criterion query
            IEnumerable<IScoringScaleLegendModel> criterion = Enumerable.Empty<IScoringScaleLegendModel>();
            var criterionLabel = String.Empty;
            if (elements.Any(x => !x.OverallFlag && x.ScoreFlag))
            {
                var legend = elements.FirstOrDefault(x => !x.OverallFlag && x.ScoreFlag).MechanismTemplateElementScorings.FirstOrDefault().ClientScoringScale.ClientScoringScaleLegend;
                if (legend != null)
                {
                    criterion = legend.ClientScoringScaleLegendItems.Select(x => new ScoringScaleLegendModel
                    {
                        HighValue = x.HighValueLabel,
                        LowValue = x.LowValueLabel,
                        LegendItemLabel = x.LegendItemLabel
                    });
                    criterionLabel = legend.LegendName;
                }
            }

            var result = new ClientScoringScaleLegendModel
            {
                Overall = overall,
                OverallScaleLabel = overallLabel,
                Criterion = criterion,
                CriterionScaleLabel = criterionLabel
            };
            return result;
        }
        #endregion
        /// <summary>
        /// List the user's assigned open panels.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of IListEntry entities</returns>
        public Container<IListEntry> ListUsersOpenPanels(int userId)
        {
            ValidateInt(userId, "ApplicationManagementService.ListUsersOpenPanels", "userId");

            Container<IListEntry> container = new Container<IListEntry>();
            List<IListEntry> resultList = new List<IListEntry>();
            container.ModelList = resultList;

            var puas = UnitOfWork.PanelUserAssignmentRepository.GetPanelUserAssignmentsForOpenPanels(userId);
            foreach (var x in puas)
            {
                //TODO: Refactor with C# 6 ?. code
                if ((x.ClientParticipantType.ReviewerFlag || x.ClientParticipantType.IsCpritChair()) && x.SessionPanel.IsPanelOpen())
                {
                    string display = FormatUserOpenPanelDisplay(x.SessionPanel);
                    IListEntry listEntry = new ListEntry(x.SessionPanel.SessionPanelId, display);
                    resultList.Add(listEntry);
                }
            }

            return container;
        }
        /// <summary>
        /// List the user's assigned open panels for which they are not a reviewer.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of IListEntry entities</returns>
        /// <remarks>Mostly code clone of ListUsersOpenPanels</remarks>
        public Container<IListEntry> ListAdminUsersOpenPanels(int userId)
        {
            ValidateInt(userId, "ApplicationManagementService.ListUsersOpenPanels", "userId");

            Container<IListEntry> container = new Container<IListEntry>();
            List<IListEntry> resultList = new List<IListEntry>();
            container.ModelList = resultList;

            User userEntity = UnitOfWork.UofwUserRepository.GetByIDWithAssignments(userId);

            foreach (var x in userEntity.PanelUserAssignments)
            {
                //
                // Only show the panel if:
                //  - They are not a Reviewer
                //  - The session is open
                //  - The session is not an Online review
                //
                if (!x.ClientParticipantType.ReviewerFlag && x.SessionPanel.IsPanelOpen() && !x.SessionPanel.IsOnLineReview())
                {
                    string display = FormatUserOpenPanelDisplay(x.SessionPanel);
                    IListEntry listEntry = new ListEntry(x.SessionPanel.SessionPanelId, display);
                    resultList.Add(listEntry);
                }
            }

            return container;
        }
        /// <summary>
        /// Format the display string for the open panel drop down.
        /// </summary>
        /// <param name="sessionPanel">User's SessionPanel entity</param>
        /// <returns>Display string</returns>
        internal string FormatUserOpenPanelDisplay(SessionPanel sessionPanel)
        {
            ProgramYear programYearEntity = sessionPanel.GetProgramYear();
            ClientProgram clientProgramEntity = programYearEntity.ClientProgram;
            return string.Format("{0} {1} ({2}) {3}", programYearEntity.Year, clientProgramEntity.ProgramDescription, clientProgramEntity.ProgramAbbreviation, sessionPanel.PanelAbbreviation);
        }
        /// <summary>
        /// Retrieves the status of the specified SessionPanel.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>PanelStatus entity describing the SessionPanel's status</returns>
        public PanelStatus OpenPanelStatus(int sessionPanelId, int userId)
        {
            ValidateInt(sessionPanelId, "ApplicationManagementService.OpenPanelStatus", "sessionPanelId");
            //
            // Retrieve a the SessionPanel which is where we get all of our information from
            //
            SessionPanel sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            var isReleased = UnitOfWork.PanelApplicationRepository.IsReleased(sessionPanelId);
            int applicationState = sessionPanelEntity.PanelApplicationState(isReleased);
            //
            // Create a model & populate it
            //
            PanelStatus result = new PanelStatus(sessionPanelId);
            result.Populate(applicationState == SessionPanel.ApplicationState.PostAssignment, isReleased, sessionPanelEntity.CurrentPhaseDate(), sessionPanelEntity.GetUsersParticipantType(userId), sessionPanelEntity.GetUsersRoleName(userId), sessionPanelEntity.CurrentPhaseName(), sessionPanelEntity.ClientId());

            return result;
        }
        /// <summary>
        /// Retrieves a specific application budget note
        /// </summary>
        /// <param name="applicationBudgetId">The application budget identifier</param>
        /// <returns>AdminBudgetNoteModel</returns>
        public IAdminBudgetNoteModel GetSpecificAdminBudgetNote(int applicationBudgetId)
        {
            ValidateInt(applicationBudgetId, FullName(nameof(ApplicationManagementService), nameof(GetSpecificAdminBudgetNote)), nameof(applicationBudgetId));
            var note = new AdminBudgetNoteModel();
            var applicationBudget = UnitOfWork.ApplicationBudgetRepository.GetByID(applicationBudgetId);

            //get the modified by information
            var userInfo = UnitOfWork.UserInfoRepository.Get(x => x.UserID == applicationBudget.CommentModifiedBy).FirstOrDefault();
            note.Populate(applicationBudget.ApplicationBudgetId, applicationBudget.ApplicationId, applicationBudget.Comments, applicationBudget.CommentModifiedBy, applicationBudget.CommentModifiedDate, userInfo?.LastName, userInfo?.FirstName);

            return note;
        }
        /// <summary>
        /// Retrieves all the application budget notes for a specific 
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns>AdminBudgetNoteModel</returns>
        public IAdminBudgetNoteModel GetApplicationAdminBudgetNote(int applicationId)
        {
            ValidateInt(applicationId, FullName(nameof(ApplicationManagementService), nameof(GetApplicationAdminBudgetNote)), nameof(applicationId));
            var note = new AdminBudgetNoteModel();
            //get the budget
            var applicationBudget = UnitOfWork.ApplicationBudgetRepository.Get(x => x.ApplicationId == applicationId).FirstOrDefault();
            if (applicationBudget != null)
            {
                //get the modified by information
                var userInfo = UnitOfWork.UserInfoRepository.Get(x => x.UserID == applicationBudget.CommentModifiedBy).FirstOrDefault();
                note.Populate(applicationBudget.ApplicationBudgetId, applicationBudget.ApplicationId, applicationBudget.Comments, applicationBudget.CommentModifiedBy, applicationBudget.CommentModifiedDate, userInfo?.LastName, userInfo?.FirstName);
            }
            return note;
        }
        /// <summary>
        /// Create or add's an admin note.  If the containing ApplicationBudget entity does
        /// not exist, it will be created.
        /// </summary>
        /// <param name="applicationBudgetId">Application entity identifier</param>
        /// <param name="applicationId">Application entity identifier</param>
        /// <param name="note">Administration note text</param>
        /// <param name="userId">User entity identifier of user making change</param>
        public void AddModifyAdminNote(int applicationBudgetId, int applicationId, string note, int userId)
        {
            ValidateInt(userId, FullName(nameof(ApplicationManagementService), nameof(AddModifyAdminNote)), nameof(userId));
            if (note == string.Empty) note = null;

            ApplicationBudgetServiceAction action = new ApplicationBudgetServiceAction();
            action.InitializeAction(this.UnitOfWork, UnitOfWork.ApplicationBudgetRepository, ServiceAction<ApplicationBudget>.DoUpdate, applicationBudgetId, userId);
            action.Populate(applicationId, note, userId);
            action.Execute();

        }
        public void DeleteAdminNote(int applicationBudgetId, int userId)
        {
            ValidateInt(applicationBudgetId, FullName(nameof(ApplicationManagementService), nameof(DeleteAdminNote)), nameof(applicationBudgetId));
            ValidateInt(userId, FullName(nameof(ApplicationManagementService), nameof(DeleteAdminNote)), nameof(userId));

            ApplicationBudget entity = UnitOfWork.ApplicationBudgetRepository.GetByID(applicationBudgetId);
            entity.UpdateComment(null, userId);

            UnitOfWork.ApplicationBudgetRepository.Update(entity);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Modify withdraw status
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="withdrawBy"></param>
        /// <param name="withdrawFlag"></param>
        public void ModifyWithdrawStatus(int applicationId, int? withdrawnBy, bool withdrawnFlag, DateTime? withdrawnDate)
        {
            var application = UnitOfWork.ApplicationRepository.GetByID(applicationId);

            application.WithdrawnFlag = withdrawnFlag;
            application.WithdrawnBy = withdrawnBy;
            application.WithdrawnDate = withdrawnDate;

            UnitOfWork.ApplicationRepository.Update(application);
            UnitOfWork.Save();

        }
        /// <summary>
        /// Find application by log number
        /// </summary>
        /// <param name="logNumber"></param>
        /// <returns></returns>
        public Application FindApplicationById(int applicationId)
        {
            var application = UnitOfWork.ApplicationRepository.GetByID(applicationId);

            return application;
        }
    }
}
