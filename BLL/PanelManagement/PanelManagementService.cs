using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.HelperClasses;
using Webmodel = Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.Bll.ModelBuilders.Permissions;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Bll.ReviewerRecruitment;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Bll.PanelManagement
{
    /// <summary>
    /// PanelManagementService provides services to perform business related functions for
    /// the PanelManagement Application.
    /// </summary>
    public partial class PanelManagementService : ServerBase, IPanelManagementService
    {
        MessageService theMessageService = new MessageService();
        /// <summary>
        /// The specialist reviewer abbreviation
        /// </summary>
        public const string SpecialistReviewerAbbreviation = "SPR";

        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public PanelManagementService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion
        #region Services
        /// <summary>
        /// Updates the applications within a panel order of review.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier; identifies the panel being reordered</param>
        /// <param name="collection">Collection of SetOrderOfReviewToSave objects describing the reordering.</param>
        /// <param name="userId">User requesting the action</param>
        public void SetOrderOfReview(int sessionPanelId, ICollection<SetOrderOfReviewToSave> collection, int userId)
        {
            ValidateSetOrderOfReviewParameters(sessionPanelId, collection, userId);

            SessionPanel sessionPanel = this.UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);

            foreach (var item in collection)
            {
                //
                // Get the application and reorder it or set it to triage.
                //
                PanelApplication panelApplication = sessionPanel.GetPanelApplicationByApplicationLogNumber(item.LogNumber);

                panelApplication.Reorder(item.NewOrder, userId);
                panelApplication.SetReviewStatus(item.IsTriaged, userId);
            }
            //
            // Now we save any changes.
            //
            UnitOfWork.Save();
        }

        /// <summary>
        /// Determines whether the user has completed all required expertise ratings
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="userId">User identifier</param>
        /// <returns>True if all expertise on the panel was complete; otherwise false;</returns>
        public bool IsUserExpertiseComplete(int sessionPanelId, int userId, bool manageCritiques)
        {
            var expertiseRequired = this.UnitOfWork.PanelUserAssignmentRepository.Select()
                .Where(x => x.UserId == userId && x.SessionPanelId == sessionPanelId);
            var expertiseCountRequired = this.UnitOfWork.PanelApplicationRepository.Get(x => x.SessionPanelId == sessionPanelId).Count();
            var expertiseComplete = expertiseRequired
                .SelectMany(y => y.PanelApplicationReviewerExpertises.DefaultIfEmpty())
                .Count(a => a.ClientExpertiseRatingId != null);
            return manageCritiques ? true : expertiseComplete >= expertiseCountRequired;
        }
        /// <summary>
        /// Gets the assignment type threshold.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        public Webmodel.IAssignmentTypeThreshold GetAssignmentTypeThreshold(int sessionPanelId)
        {
            ValidateSetAssignmentTypeThreshold(sessionPanelId);
            var clientId = ClientId(sessionPanelId);

            Dal.AssignmentTypeThreshold newAssignment = this.UnitOfWork.AssignmentTypeThresholdRepository.Get(sessionPanelId);
            IEnumerable<ClientAssignmentType> types = this.UnitOfWork.ClientAssignmentTypeRepository.Get(x => x.ClientId == clientId);

            var threshold = new Webmodel.AssignmentTypeThreshold();

            if (newAssignment != null)
            {
                threshold = new Webmodel.AssignmentTypeThreshold(sessionPanelId, newAssignment.ScientistReviewerSortOrder, newAssignment.AdvocateConsumerSortOrder,
                    newAssignment.SpecialistReviewerSortOrder, types.Any(x => x.AssignmentAbbreviation == SpecialistReviewerAbbreviation));
            }

            return threshold;
        }
        /// <summary>
        /// Sets the assignment type threshold.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="scientistReviewerSortOrder">The scientist reviewer sort order.</param>
        /// <param name="advocateConsumerSortOrder">The advocate consumer sort order.</param>
        /// <param name="specialistReviewerSortOrder">The specialist reviewer sort order.</param>
        /// <returns></returns>
        public Webmodel.IAssignmentTypeThreshold SetAssignmentTypeThreshold(int sessionPanelId, int? scientistReviewerSortOrder, int? advocateConsumerSortOrder,
            int? specialistReviewerSortOrder)
        {
            ValidateSetAssignmentTypeThreshold(sessionPanelId);

            Dal.AssignmentTypeThreshold newAssignment = this.UnitOfWork.AssignmentTypeThresholdRepository.Upsert(sessionPanelId, scientistReviewerSortOrder, advocateConsumerSortOrder,
                specialistReviewerSortOrder);

            UnitOfWork.Save();

            var threshold = new Webmodel.AssignmentTypeThreshold(sessionPanelId, newAssignment.ScientistReviewerSortOrder, newAssignment.AdvocateConsumerSortOrder,
                newAssignment.SpecialistReviewerSortOrder);

            return threshold;
        }
        /// <summary>
        /// Create a Reviewer Evaluation and insert it into the ReviewerEvaluation
        /// </summary>
        /// <param name="reviewerEvaluations">Reviewer Evaluations To record</param>
        /// <param name="userId">User identifier</param>
        public void SaveReviewerEvaluation(List<Webmodel.ReviewerEvaluation> reviewerEvaluations, int userId)
        {
            ValidateSaveReviewerEvaluationParameters(userId);

            foreach (var eval in reviewerEvaluations)
            {
                if ((eval.Rating != null) || (!String.IsNullOrEmpty(eval.RatingComments)) || ((eval.ChairFlag == false) && (eval.ConsumerFlag == false) && (eval.Rating != null) && (!String.IsNullOrEmpty(eval.RatingComments))))
                {
                    if (eval.ReviewerEvaluationId.HasValue)
                    {
                        Dal.ReviewerEvaluation evaluation = this.UnitOfWork.ReviewerEvaluationRepository.Modify(eval.ReviewerEvaluationId.Value, eval.Rating, eval.PotentialChairFlag, eval.RatingComments, userId);
                        UnitOfWork.ReviewerEvaluationRepository.Update(evaluation);
                    }
                    else
                    {
                        //
                        // Add it to the repository & save it.
                        //
                        Dal.ReviewerEvaluation evaluation = this.UnitOfWork.ReviewerEvaluationRepository.Populate(eval.PanelUserAssignmentId, eval.Rating, eval.PotentialChairFlag, eval.RatingComments, userId);
                        UnitOfWork.ReviewerEvaluationRepository.Add(evaluation);
                    }
                }
            }

            UnitOfWork.Save();
        }
        /// <summary>
        /// Calculates the individual reviewer presentation order counts for a single panel.
        /// </summary>
        /// <param name="expertise">A panel's ReviewerExpertise objects for a session panel</param>
        /// <returns>Dictionary of OrderOfReviewCounts (basically the header information for the Reviewer Experience tab of the Panel Management application</returns>
        public Dictionary<int, OrderOfReviewCounts> CalculatePresentationOrderCounts(IEnumerable<Webmodel.IReviewerExpertise> expertise)
        {
            //
            // First group by the user id
            //
            var temp = expertise.GroupBy(x => x.UserId);
            //
            // Now we count the number of times the reviewer was the first presenter, then the number of times the user was not
            //
            var temp2 = temp.Select(x => new OrderOfReviewCounts
            {
                UserId = x.FirstOrDefault().UserId,
                FirstReviewerCount = x.Count(xx => (xx.ReviewOrderValue == PanelApplicationReviewerAssignment.PresentationOrder.First)),
                SecondReviewerCount = x.Count(xx => (xx.ReviewOrderValue != PanelApplicationReviewerAssignment.PresentationOrder.First && xx.ReviewOrderValue > PanelApplicationReviewerAssignment.PresentationOrder.Any && AssignmentType.CritiqueAssignments.Contains(xx.AssignmentTypeId.GetValueOrDefault(0)))),
                AllPositionsCount = x.Count(xx => xx.ReviewOrderValue > PanelApplicationReviewerAssignment.PresentationOrder.Any),
                ReviewerFirstName = x.FirstOrDefault().ReviewerFirstName,
                ReviewerLastName = x.FirstOrDefault().ReviewerLastName,
                ConsumerFlag = x.FirstOrDefault().ConsumerFlag,
                SpecialistFlag = x.FirstOrDefault().SpecialistFlag,
                ScientistFlag = x.FirstOrDefault().ScientistFlag
            });
            //
            // Now organize it into a usable structure for the presentation layer.
            //
            Dictionary<int, OrderOfReviewCounts> result = temp2.ToDictionary(p => p.UserId).OrderBy(o => o.Value.ReviewerLastName).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return result;
        }
        /// <summary>
        /// Calculates the reviewer experience for an application on a panel.
        /// </summary>
        /// <param name="expertise">A panel's ReviewerExpertise objects for a session panel</param>
        /// <returns>Dictionary of ExperienceCounts</returns>
        public Dictionary<int, ExperienceCounts> CalculateExpertiseCounts(IEnumerable<Webmodel.IReviewerExpertise> expertise)
        {
            //
            // First group by the user id
            //
            var temp = expertise.GroupBy(x => x.ApplicationId);
            //
            // Now we count the number of times the reviewer was the first presenter, then the number of times the user was not
            //
            var temp2 = temp.Select(x => new ExperienceCounts
            {
                ApplicationId = x.FirstOrDefault().ApplicationId,
                LogNumber = x.FirstOrDefault().LogNumber,
                HighCount = x.Count(xx => (xx.Rating == ClientExpertiseRating.Ratings.High)),
                MediumCount = x.Count(xx => xx.Rating == ClientExpertiseRating.Ratings.Medium),
                LowCount = x.Count(xx => xx.Rating == ClientExpertiseRating.Ratings.Low),
            });
            //
            // Now organize it into a usable structure for the presentation layer.
            //
            Dictionary<int, ExperienceCounts> result = temp2.ToDictionary(p => p.ApplicationId).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return result;
        }
        /// <summary>
        /// Detects if a reviewer has a step assignment associated with an application
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier</param>
        /// <param name="applicationId">The application identifier</param>
        /// <returns>True if the reviewer has a workflow associated with the application. Otherwise return false.</returns>
        public bool ReviewerHasWorkflow(int panelUserAssignmentId, int panelApplicationId)
        {
            ValidateReviewerHasWorkflowParameter(panelUserAssignmentId, panelApplicationId);

            return this.UnitOfWork.ApplicationWorkflowRepository.ReviewerHasWorkflow(panelUserAssignmentId, panelApplicationId);
        }
        /// <summary>
        /// Removes a reviewer from an application and flag step assignment and critique data as deleted
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier</param>
        /// <param name="applicationId">The application identifier</param>
        /// <param name="userId">The current user identifier</param>
        public void UnAssignReviewer(int panelUserAssignmentId, int panelApplicationId, int userId, bool removeMeetingCritiques = false)
        {
            ValidateUnAssignReviewerParameters(panelUserAssignmentId, panelApplicationId, userId);

            // Removes reviewer assignment from application            
            PanelApplicationReviewerAssignment assignment = this.UnitOfWork.PanelApplicationReviewerAssignmentRepository.GetReviewerAssignment(panelUserAssignmentId, panelApplicationId);
            if (assignment != null)
            {
                this.UnitOfWork.PanelApplicationReviewerAssignmentRepository.Delete(assignment, userId);
            }
            DeleteWorkflowsAndContent(panelUserAssignmentId, panelApplicationId, userId, removeMeetingCritiques);

            // Save
            this.UnitOfWork.Save();
        }
        /// <summary>
        /// Determines whether [has assigned applications] [the specified panel user assignment identifier].
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns>
        ///   <c>true</c> if [has assigned applications] [the specified panel user assignment identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasAssignedApplications(int panelUserAssignmentId)
        {
            PanelApplicationReviewerAssignment assignments = this.UnitOfWork.PanelApplicationReviewerAssignmentRepository.GetReviewerAssignments(panelUserAssignmentId).FirstOrDefault();
            return assignments != null;
        }
        /// <summary>
        /// Deletes all workflows and content associated with a panel user assignment and application
        /// </summary>
        /// <param name="panelUserAssignmentId"></param>
        /// <param name="panelApplicationId"></param>
        /// <param name="userId"></param>
        internal void DeleteWorkflowsAndContent(int panelUserAssignmentId, int panelApplicationId, int userId, bool removeMeetingCritiques)
        {
            // Flag critique data as deleted if exists
            var workflows = removeMeetingCritiques ? this.UnitOfWork.ApplicationWorkflowRepository.GetWorkflows(panelUserAssignmentId, panelApplicationId) :
                this.UnitOfWork.ApplicationWorkflowRepository.GetPreMeetingWorkflows(panelUserAssignmentId, panelApplicationId);

            // Loop through the workflow collection and delete workflow entities and associated child entities
            // The order of deletion is important. To be safe, child entities should be deleted first.
            foreach (ApplicationWorkflow workflow in workflows)
            {
                // Delete application workflow step element contents/critiques
                this.UnitOfWork.ApplicationWorkflowStepElementContentRepository.DeleteByWorkflowId(workflow.ApplicationWorkflowId, userId);
                // Delete application workflow step element
                this.UnitOfWork.ApplicationWorkflowStepElementRepository.DeleteByWorkflowId(workflow.ApplicationWorkflowId, userId);
                // Delete application workflow step work log
                this.UnitOfWork.ApplicationWorkflowStepWorkLogRepository.DeleteByWorkflowId(workflow.ApplicationWorkflowId, userId);
                // Delete application workflow step assignment
                this.UnitOfWork.ApplicationWorkflowStepAssignmentRepository.DeleteByWorkflowId(workflow.ApplicationWorkflowId, userId);
                // Delete application workflow step
                this.UnitOfWork.ApplicationWorkflowStepRepository.DeleteByWorkflowId(workflow.ApplicationWorkflowId, userId);
                // Delete application workflow
                this.UnitOfWork.ApplicationWorkflowRepository.Delete(workflow, userId);
            }
        }

        /// <summary>
        /// Checks if the critique can be submitted
        /// </summary>
        /// <param name="applicationWorkflowId">ApplicationWorkflow identifier</param>
        /// <returns>True if the critique can be submitted; false otherwise </returns>
        public bool IsCritiqueSubmittable(int applicationWorkflowId)
        {
            this.ValidateParameter(applicationWorkflowId, "PanelManagementService.IsCritiqueSubmittable", "applicationWorkflowId");

            ApplicationWorkflow theApplicationWorkflow = UnitOfWork.ApplicationWorkflowRepository.GetByID(applicationWorkflowId);

            ApplicationWorkflowStep currentApplicationWorkflowStep = theApplicationWorkflow.CurrentStep();

            return (currentApplicationWorkflowStep != null) ? currentApplicationWorkflowStep.HasContent() : false;
        }
        /// <summary>
        /// Indicates if the SessionPanel is  Online 
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <returns>True if the SessionPanel is  Online; false otherwise</returns>
        public bool IsOnline(int? sessionPanelId)
        {
            bool result = false;

            if (sessionPanelId.HasValue)
            {
                SessionPanel sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId.Value);
                result = sessionPanelEntity.IsOnLineReview();
            }
            return result;
        }

        /// <summary>
        /// Removes the user from their assigned panel.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void RemoveUserFromPanel(int panelUserAssignmentId, int userId)
        {
            ValidateInteger(panelUserAssignmentId, "PanelManagementService.RemoveUserFromPanel", "panelUserAssignmentId");

            UnitOfWork.PanelManagementRepository.RemoveUserFromPanel(panelUserAssignmentId, userId);
        }
        /// <summary>
        /// Get Session Panel Id by Panel Application Id
        /// </summary>
        /// <param name="panelApplicationId"></param>
        /// <returns></returns>
        public int GetSessionPanelIdByPanelApplicationId(int panelApplicationId)
        {
            return UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId).SessionPanelId;
        }
        #region Panel Reviewer Expertise

        /// <summary>
        /// Save a reviewer's COI detail
        /// </summary>
        /// <param name="ClientCoiTypeId">The client coi type identifier</param>
        /// <param name="PanelApplicationReviewerExpertiseId">The panel application reviewer expertise identifier</param>
        /// <param name="userId">The user identifier of the user entering the coi detail</param>
        internal virtual void SavePanelApplicationReviewerCoiDetail(int clientCoiTypeId, int panelApplicationReviewerExpertiseId, int userId)
        {
            PanelApplicationReviewerCoiDetail details;

            details = this.UnitOfWork.PanelApplicationReviewerCoiDetailRepository.Get(u => u.PanelApplicationReviewerExpertiseId == panelApplicationReviewerExpertiseId).FirstOrDefault();

            if (details != null)
            {
                details.Modify(clientCoiTypeId, userId);
                UnitOfWork.PanelApplicationReviewerCoiDetailRepository.Update(details);

            }
            else
            {
                details = new PanelApplicationReviewerCoiDetail();
                details = details.Populate(clientCoiTypeId, panelApplicationReviewerExpertiseId, userId);
                UnitOfWork.PanelApplicationReviewerCoiDetailRepository.Add(details);
            }
        }
        /// <summary>
        /// Save a reviewer's expertise rating
        /// </summary>
        /// <param name="panelAppplicationId">The panel application identifier</param>
        /// <param name="PanelUserAssignmentId">The panel user assignment id</param>
        /// <param name="clientExpertiseRatingId">The expertise rating identifier</param>
        /// <param name="userId">The current user's user id</param>
        /// <param name="comments">The comments for COI</param>
        internal void SaveReviewerExpertise(int panelApplicationId, int panelUserAssignmentId, int? clientExpertiseRatingId, int userId, string comments)
        {
            PanelApplication p = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);

            var existingExpertise = (p != null) ? p.PanelApplicationReviewerExpertises.Where(x => x.PanelUserAssignmentId == panelUserAssignmentId) : new List<PanelApplicationReviewerExpertise>();

            if (existingExpertise.Count() != 0)
            {
                existingExpertise.ToList().ForEach(delegate (PanelApplicationReviewerExpertise expertise)
                {
                    if (((expertise.PanelApplicationId == panelApplicationId) && (expertise.ClientExpertiseRatingId != clientExpertiseRatingId)) || (expertise.ExpertiseComments != comments))
                    {
                        PanelApplicationReviewerCoiDetail coiDetail = expertise.PanelApplicationReviewerCoiDetails.FirstOrDefault();
                        if (coiDetail != null)
                        {
                            UnitOfWork.PanelApplicationReviewerCoiDetailRepository.Delete(coiDetail, userId);
                        }
                        expertise.Modify(clientExpertiseRatingId, comments, userId);
                        UnitOfWork.PanelApplicationReviewerExpertiseRepository.Update(expertise);
                    }
                });
            }
            else
            {
                PanelApplicationReviewerExpertise expertise;
                expertise = new PanelApplicationReviewerExpertise();
                expertise = expertise.Populate(panelApplicationId, panelUserAssignmentId, clientExpertiseRatingId, userId, comments);
                UnitOfWork.PanelApplicationReviewerExpertiseRepository.Add(expertise);
            }
        }
        /// <summary>
        /// Get a session panel's program year information
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>IProgramYearModel model</returns>
        public Webmodel.IProgramYearModel GetProgramYear(int sessionPanelId)
        {
            ValidateGetProgramYearParameters(sessionPanelId);

            return this.UnitOfWork.SessionPanelRepository.GetProgramYear(sessionPanelId);
        }
        /// <summary>
        /// Gets the session panel.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        public Webmodel.ISessionPanelModel GetSessionPanel(int sessionPanelId)
        {
            ValidateGetSessionPanelParameters(sessionPanelId);
            var panel = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            return new Webmodel.SessionPanelModel(sessionPanelId, panel.PanelAbbreviation, panel.GetProgramYearId(),
                panel.GetFiscalYear(), panel.GetProgramAbbreviation(), panel.MeetingSessionId);
        }
        #endregion
        #region Panel Management Assign
        /// <summary>
        /// Saves the assignment.
        /// </summary>
        /// <param name="clientExpertiseRatingId">The client expertise rating identifier.</param>
        /// <param name="clientCoiTypeId">The client coi type identifier.</param>
        /// <param name="presentationOrder">The presentation order.</param>
        /// <param name="clientAssignmentTypeId">The client assignment type identifier.</param>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="isWorkflowDeletionOverride">if set to <c>true</c> [is workflow deletion override].</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="clientExpertiseChanged">whether the client assignment has changed.</param>
        /// <returns></returns>
        /// <remarks>Candidate for re-factoring to better separate expertise, assignment, and un-assignment</remarks>
        public ReviewerAssignmentStatus SaveAssignment(int? clientExpertiseRatingId, int? clientCoiTypeId, int? presentationOrder, int? clientAssignmentTypeId, int panelApplicationId, int panelUserAssignmentId, string comment, bool isWorkflowDeletionOverride, int userId, bool clientAssignmentChanged)
        {
            ReviewerAssignmentStatus result;

            result = AssignOrUnassignReviewer(clientExpertiseRatingId, presentationOrder, clientAssignmentTypeId, clientCoiTypeId, panelApplicationId, panelUserAssignmentId, isWorkflowDeletionOverride, comment, userId, clientAssignmentChanged);
            //Save expertise if the assignment was a success, or there was no assignment
            if (result == ReviewerAssignmentStatus.Success || result == ReviewerAssignmentStatus.Default)
            {
                this.SaveReviewerExpertise(panelApplicationId, panelUserAssignmentId, clientExpertiseRatingId, userId, comment);
                //
                // Save any changes.
                //
                UnitOfWork.Save();
                //
                //  Now see if we need to start a workflow for this reviewer.  We need to do this if the reviewer was assigned
                //  to the panel after the panelApplications were released to reviewers.
                //
                IfNecessaryStartReviewerWorkflow(panelApplicationId, panelUserAssignmentId, userId);
            }
            return result;
        }
        /// <summary>
        /// Saves the assignment with the current presentation order.
        /// </summary>
        /// <
        /// <param name="clientExpertiseRatingId">ClientExpertiseRating entity identifier.</param>
        /// <param name="clientCoiTypeId">ClientCoiType entity identifier.</param>
        /// <param name="clientAssignmentTypeId">ClientAssignmentType entity identifier.</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier.</param>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="isWorkflowDeletionOverride">if set to <c>true</c> [is workflow deletion override].</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="clientAssignmentChanged">Whether the client assignment has changed.</param>
        /// <returns>Assignment status </returns>
        /// <remarks>Candidate for re-factoring to better separate expertise, assignment, and un-assignment</remarks>
        public ReviewerAssignmentStatus SaveAssignmentWithCurrentPresentationOrder(int? clientExpertiseRatingId, int? clientCoiTypeId,
                                                                                    int? clientAssignmentTypeId, int panelApplicationId,
                                                                                    int panelUserAssignmentId, string comment,
                                                                                    bool isWorkflowDeletionOverride, int userId, bool clientAssignmentChanged)
        {
            //
            // First we need to retrieve the user's current presentation order if any.
            //
            int? presentationOrder = RetrieveAssignmentPresentationOrder(panelApplicationId, userId);
            //
            // Now we call the existing service method to do the assignment.
            //
            ReviewerAssignmentStatus result = SaveAssignment(clientExpertiseRatingId, clientCoiTypeId, presentationOrder, clientAssignmentTypeId, panelApplicationId, panelUserAssignmentId, comment, isWorkflowDeletionOverride, userId, clientAssignmentChanged);

            return result;
        }
        /// <summary>
        /// Retrieves the users presentation order on a specification panel application.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Presentation order if it is assigned; null otherwise</returns>
        internal virtual int? RetrieveAssignmentPresentationOrder(int panelApplicationId, int userId)
        {
            PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            PanelApplicationReviewerAssignment panelApplicationReviewerAssignmentEntity = panelApplicationEntity.UsersAssignmentType(userId);
            return panelApplicationReviewerAssignmentEntity?.SortOrder;
        }
        /// <summary>
        /// Performs the Assign or Un-assign function.
        /// </summary>
        /// <param name="clientExpertiseRatingId">Expertise reviewer id</param>
        /// <param name="presentationOrder">Presentation order</param>
        /// <param name="clientAssignmentTypeId">Client Type identifier</param>
        /// <param name="clientCoiTypeId">Client coi type identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="isWorkflowDeletionOverride">User indicated that workflow should be removed.</param>
        /// <param name="comment">Comment</param>
        /// <param name="userId">User identifier of user making the change</param>
        /// <param name="clientAssignmentChanged">whether the client assignment changed</param>
        /// <returns>
        /// True if Assign or UnAssign was done; False if the slot is already taken
        /// </returns>
        internal virtual ReviewerAssignmentStatus AssignOrUnassignReviewer(int? clientExpertiseRatingId, int? presentationOrder, int? clientAssignmentTypeId, int? clientCoiTypeId, int panelApplicationId, int panelUserAssignmentId, bool isWorkflowDeletionOverride, string comment, int userId, bool clientAssignmentChanged)
        {
            ReviewerAssignmentStatus result = ReviewerAssignmentStatus.Default;

            ClientAssignmentType clientAssignmentType = this.UnitOfWork.ClientAssignmentTypeRepository.GetByID(clientAssignmentTypeId);

            if (clientAssignmentType != null && clientAssignmentTypeId.HasValue && clientAssignmentType.IsCoi)
            {
                result = AssignReviewerAsCoi(panelApplicationId, panelUserAssignmentId, clientAssignmentTypeId, clientCoiTypeId, clientExpertiseRatingId, comment, userId);
            }
            else if ((presentationOrder.HasValue) && (!clientAssignmentTypeId.HasValue))
            {
                //
                // Well we have a presentation order but no assignment type so this
                // is an error so just return
                //
                result = ReviewerAssignmentStatus.IncompleteAssignmentData;
            }
            else if (clientAssignmentType != null)
            {
                //
                // First see if we can do any the assignment (if any)  If assignment fails we do not want to save anything else.
                //
                result = Assign(presentationOrder, clientAssignmentTypeId.Value, panelApplicationId, panelUserAssignmentId, userId);
            }
            else if (clientAssignmentType == null && clientAssignmentChanged)
            {
                //
                // If the reviewer has a workflow with critiques we send back a status indicating such.  THe UI can display
                // a message in which they are given the opportunity to override the check.  If they respond yes we go ahead and
                // removes the reviewer assignment.
                //
                if ((!isWorkflowDeletionOverride) && (ReviewerHasWorkflow(panelUserAssignmentId, panelApplicationId)))
                {
                    result = ReviewerAssignmentStatus.ReviewerHasWorkflow;
                }
                else
                {
                    //
                    // Client type does not have a value which signifies un-assign the reviewer
                    //
                    this.UnAssignReviewer(panelUserAssignmentId, panelApplicationId, userId);
                    result = ReviewerAssignmentStatus.Success;
                }
            }
            else
            {
                COIToExpertiseCheck(panelApplicationId, panelUserAssignmentId, clientExpertiseRatingId, userId);
            }
            return result;
        }
        /// <summary>
        /// Check if the rating went from COI to a specific rating.  In which case the reviewer assignment is removed.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="clientExpertiseRatingId">ClientExpertiseRating entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        internal virtual void COIToExpertiseCheck(int panelApplicationId, int panelUserAssignmentId, int? clientExpertiseRatingId, int userId)
        {
            PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            PanelApplicationReviewerAssignment panelApplicationReviewerAssignmentEntity = panelApplicationEntity.UsersAssignmentType(userId);
            ClientExpertiseRating clientExpertiseRatingEntity = UnitOfWork.ClientExpertiseRatingRepository.GetByID(clientExpertiseRatingId);
            //
            // Basically we retrieve the current PanelApplicationReviewerAssignment for the user.  If it is a COI and we are changing the 
            // rating we delete the PanelApplicationReviewerAssignment.
            //
            if (
                (panelApplicationReviewerAssignmentEntity != null) && (panelApplicationReviewerAssignmentEntity.ClientAssignmentType.IsCoi) &&
                  (
                    //
                    // The expertise rating is not a COI or there is no expertise rating
                    //
                    (clientExpertiseRatingEntity != null) && (!clientExpertiseRatingEntity.ConflictFlag) ||
                    (clientExpertiseRatingEntity == null)
                  )
                )
            {
                this.UnAssignReviewer(panelUserAssignmentId, panelApplicationId, userId);
            }
        }
        /// <summary>
        /// Assigns a reviewer type & sort order to a PanelApplicationReviewerAssignment entity object. Assignment
        /// </summary>
        /// <param name="presentationOrder">Presentation order</param>
        /// <param name="clientAssignmentTypeId">Client Type identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="userId">User identifier of user making the change</param>
        /// <returns>True if the presentation order & assignment type were set or no change; False if the presentation order was already set.</returns>
        internal ReviewerAssignmentStatus Assign(int? presentationOrder, int clientAssignmentTypeId, int panelApplicationId, int panelUserAssignmentId, int userId)
        {
            ValidateAssignParameters(presentationOrder, clientAssignmentTypeId, panelApplicationId, panelUserAssignmentId, userId);
            int calculatedPresentationOrder = CalculatePresentationOrder(presentationOrder, panelApplicationId);

            ReviewerAssignmentStatus result = ReviewerAssignmentStatus.Default;

            ClientAssignmentType aClientAssignmentType = UnitOfWork.ClientAssignmentTypeRepository.GetByID(clientAssignmentTypeId);
            if (calculatedPresentationOrder == 0)
            {
                calculatedPresentationOrder = 100;
            }

            if (aClientAssignmentType.IsCoi || aClientAssignmentType.IsReader)
            {
                int? noPresentationOrder = null;

                DeleteCoiAssignment(panelApplicationId, panelUserAssignmentId, userId);

                //
                // For COI & Readers there will not be a presentation order, so just assign the reviewer
                //
                AssignReviewer(noPresentationOrder, clientAssignmentTypeId, panelApplicationId, panelUserAssignmentId, userId);
                result = ReviewerAssignmentStatus.Success;
            }
            else if (calculatedPresentationOrder > 0)
            {
                //
                // Now locate the assignment for this reviewer.
                //
                PanelApplicationReviewerAssignment thisUsersPanelApplicationReviewerAssignment = UnitOfWork.PanelApplicationRepository.GetPanelApplicationReviewerAssignmentForSpecificReviewer(panelApplicationId, panelUserAssignmentId);

                //
                // First thing we need to do is to check if the presentation order is set already
                //
                PanelApplicationReviewerAssignment aPanelApplicationReviewerAssignment = UnitOfWork.PanelApplicationRepository.GetPanelApplicationReviewerAssignment(calculatedPresentationOrder, panelApplicationId);
                //
                // If there were no changes we are done
                //
                if (thisUsersPanelApplicationReviewerAssignment != null && thisUsersPanelApplicationReviewerAssignment.IsSame(presentationOrder, clientAssignmentTypeId))
                {
                    result = ReviewerAssignmentStatus.Success;
                }
                //
                // If there is no one assigned or no one assigned to the slot then we can do the assign
                //
                else if ((aPanelApplicationReviewerAssignment == null) ||
                        ((aPanelApplicationReviewerAssignment != null) && (!aPanelApplicationReviewerAssignment.IsPresentationOrderSetForOtherUser(calculatedPresentationOrder, panelUserAssignmentId))))
                {
                    result = ReviewerAssignmentStatus.Success;
                    // for previous assignment of coi, delete the coi assignment (e.g. so it is backed up) before updating assignment
                    DeleteCoiAssignment(panelApplicationId, panelUserAssignmentId, userId);
                    //
                    // Now we can do the assign
                    //
                    AssignReviewer(calculatedPresentationOrder, clientAssignmentTypeId, panelApplicationId, panelUserAssignmentId, userId);
                    result = ReviewerAssignmentStatus.Success;
                }
                else
                {
                    result = ReviewerAssignmentStatus.PositionOccupied;
                }
            }

            return result;
        }
        /// <summary>
        /// Auto calculates the presentation order.
        /// </summary>
        /// <param name="presentationOrder">Supplied presentation order, if any</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>Calculated presentation order if one available or 0 if note exists</returns>
        internal int CalculatePresentationOrder(int? presentationOrder, int panelApplicationId)
        {
            int calculatedPresentationOrder = 0;

            if (!presentationOrder.HasValue)
            {
                //
                // So we go to the panel & retrieve all of it's presentation order.  BTW 
                // they come out sorted, highest last
                //
                PanelApplication p = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
                List<int> list = p.CurrentPresentationOrders();
                calculatedPresentationOrder = (list.Count() > 0) ? list.Last<int>() : 0;
                calculatedPresentationOrder = 0;
                //
                // Now see if we can increment it.  To do so get the maximum number of presentation places 
                // from the configuration manager & increment if there is room left.
                //
                //calculatedPresentationOrder = (calculatedPresentationOrder >= ConfigManager.PresentationOrderMaximum)? 0: ++calculatedPresentationOrder;
            }
            else
            {
                calculatedPresentationOrder = presentationOrder.Value;
            }
            return calculatedPresentationOrder;

        }
        /// <summary>
        /// Deletes the current assignment if it is a COI assignment
        /// </summary>
        /// <param name="panelApplicationId">Panel Application identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="userId">User identifier of performing the coi assignment deletion</param>
        internal virtual void DeleteCoiAssignment(int panelApplicationId, int panelUserAssignmentId, int userId)
        {
            //
            // Now locate the assignment for this reviewer.
            //
            PanelApplicationReviewerAssignment thisUsersPanelApplicationReviewerAssignment = UnitOfWork.PanelApplicationRepository.GetPanelApplicationReviewerAssignmentForSpecificReviewer(panelApplicationId, panelUserAssignmentId);
            // for previous assignment of coi, delete the COI assignment (e.g. so it is backed up) before updating assignment
            if (thisUsersPanelApplicationReviewerAssignment != null && thisUsersPanelApplicationReviewerAssignment.ClientAssignmentType.IsCoi)
            {
                this.UnitOfWork.PanelApplicationReviewerAssignmentRepository.Delete(thisUsersPanelApplicationReviewerAssignment, userId);
            }

        }
        /// <summary>
        /// Verifies that that the assignment actually needs to be done & makes the assignment.
        /// </summary>
        /// <param name="aPanelApplicationReviewerAssignment"></param>
        /// <param name="presentationOrder">New sort order</param>
        /// <param name="clientAssignmentTypeId">New client assignment type identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="userId">User identifier of user making the change</param>
        internal virtual void AssignReviewer(int? presentationOrder, int clientAssignmentTypeId, int panelApplicationId, int panelUserAssignmentId, int userId)
        {
            //
            // Now locate the assignment for this reviewer.
            //
            PanelApplicationReviewerAssignment aPanelApplicationReviewerAssignment = UnitOfWork.PanelApplicationRepository.GetPanelApplicationReviewerAssignmentForSpecificReviewer(panelApplicationId, panelUserAssignmentId);
            //
            // If not found we create one otherwise check if there was a change or if there was modify the assignment.
            //
            if (presentationOrder == 100)
            {
                presentationOrder = null;
            }
            if ((aPanelApplicationReviewerAssignment == null) || (!aPanelApplicationReviewerAssignment.IsSame(presentationOrder, clientAssignmentTypeId)))
            {
                MakeAssignment(aPanelApplicationReviewerAssignment, presentationOrder, clientAssignmentTypeId, panelApplicationId, panelUserAssignmentId, userId);
            }
        }
        /// <summary>
        /// Assign a user to a assignment type & presentation order.
        /// </summary>
        /// <param name="aPanelApplicationReviewerAssignment">PanelApplicationReviewerAssignment entity object or null if none exists</param>
        /// <param name="presentationOrder">New sort order</param>
        /// <param name="clientAssignmentTypeId">New client assignment type identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="userId">User identifier of user making the change</param>
        private void MakeAssignment(PanelApplicationReviewerAssignment aPanelApplicationReviewerAssignment, int? presentationOrder, int clientAssignmentTypeId, int panelApplicationId, int panelUserAssignmentId, int userId)
        {
            //
            // If there is a PanelApplicationReviewerAssignment entity then
            // it must be an update because we already checked for sort order & made sure something changed
            //
            if (aPanelApplicationReviewerAssignment != null)
            {
                aPanelApplicationReviewerAssignment.Modify(presentationOrder, clientAssignmentTypeId, userId);
                UnitOfWork.PanelApplicationReviewerAssignmentRepository.Update(aPanelApplicationReviewerAssignment);
            }
            //
            // Else we do not have a PanelApplicationReviewerAssignment entity so we need to create it
            //
            else
            {
                PanelApplicationReviewerAssignment aNewPanelApplicationReviewerAssignment = new PanelApplicationReviewerAssignment();
                aNewPanelApplicationReviewerAssignment.Populate(presentationOrder, clientAssignmentTypeId, panelApplicationId, panelUserAssignmentId, userId);
                UnitOfWork.PanelApplicationReviewerAssignmentRepository.Add(aNewPanelApplicationReviewerAssignment);
            }
        }
        /// <summary>
        /// Starts the workflow for a newly assigned PanelApplication reviewer.  This method only starts the workflow
        /// for the newly assigned PanelReviewer if the SessionPanel has already been released.
        /// </summary>
        /// <param name="panelApplicationid">PanelApplication entity identifier of panel the panel user is being assigned to</param>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier of panel user being assigned as a reviewer</param>
        /// <param name="userId">User entity identifier</param>
        internal virtual void IfNecessaryStartReviewerWorkflow(int panelApplicationid, int panelUserAssignmentId, int userId)
        {
            PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationid);
            //
            // We start the users workflow if the PanelApplication was released (i.e. viable to the reviewer) and if the
            // critique does NOT already exist
            //
            if (panelApplicationEntity.IsReleased() && !panelApplicationEntity.CritiqueExistsForSpecificReviewer(panelUserAssignmentId))
            {
                this.StartReviewerWorkflow(panelUserAssignmentId, panelApplicationid, userId);
            }
        }
        #endregion
        #region ReleaseApplications
        /// <summary>
        /// Determines if all applications in a panel can be released.  If the applications can be 
        /// released then it releases them.  
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <param name="userId">User identifier of user making the change</param>
        /// <returns>ReleaseStatus indicating status of release</returns>
        public ReleaseStatus ReleaseApplications(int sessionPanelId, int userId)
        {
            ValidateReleaseApplicationsParameters(sessionPanelId);

            ReleaseStatus result = ReleaseStatus.Default;
            //
            // The application can be released if:
            //   - scoring has been set up
            //   - each panel application has at least 1 reviewer
            //
            if (!IsScoringSetup(sessionPanelId))
            {
                result = ReleaseStatus.ScoringNotSetUp;
            }
            else
            {
                //
                // Now release the applications
                //
                Release(sessionPanelId, userId);
                //
                // Save
                //
                UnitOfWork.Save();
                result = ReleaseStatus.Success;
                //
                // Now that we have released the applications we should start the workflows
                //
                StartWorkflowsForReleasedApplications(sessionPanelId, userId);
            }
            return result;
        }
        /// <summary>
        /// Check if scoring is set up on all applications.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>True if the application can be released; false if not</returns>
        internal virtual bool IsScoringSetup(int sessionPanelId)
        {
            //
            // Get the indication if the application's mechanism is set up for scoring
            //
            var releaseableApplications = UnitOfWork.PanelManagementRepository.AreApplicationsReadyForRelease(sessionPanelId);
            //
            // If there is one or more WebModels that have a MechanismTemplateId equal to null then scoring is set up
            //
            return (releaseableApplications.ModelList.All(x => x.MechanismTemplateId != null));
        }
        /// <summary>
        /// Release any application that can be released
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <param name="userId">User entity identifier</param>
        internal void Release(int sessionPanelId, int userId)
        {
            //
            // Time to use for the Date/Time of release.  All applications on the panel should have 
            // the same date & time for release.
            //
            DateTime releaseDateTime = GlobalProperties.P2rmisDateTimeNow;
            //
            // Get the session panel from which we can get all applications
            //
            SessionPanel aSessionPanel = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            //
            //  Now go through each panelApplication
            //
            aSessionPanel.PanelApplications.ToList().ForEach(delegate (PanelApplication pa)
                    {
                        //
                        // For each stage of the application
                        //
                        pa.ApplicationStages.ToList().ForEach(delegate (ApplicationStage stage)
                                {
                                    //
                                    // Tell the entity object to test itself.  If it can be activated then 
                                    // set it to active.
                                    //
                                    stage.SetActive(releaseDateTime, userId);
                                }
                            );

                    }
            );
        }
        /// <summary>
        /// Determines whether [is meeting phase started] [the specified panel application identifier].
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is meeting phase started] [the specified panel application identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMeetingPhaseStarted(int panelApplicationId)
        {
            ValidateIsMeetingPhaseStarted(panelApplicationId);
            PanelApplication panelApplication = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);

            var meetingPhase = panelApplication.GetSyncPanelStep();
            bool isMeetingPhaseStarted = meetingPhase != null && meetingPhase.StartDate != null && meetingPhase.StartDate <= GlobalProperties.P2rmisDateTimeNow;

            return isMeetingPhaseStarted;
        }
        /// <summary>
        /// Constructs a web model indicating if the Applications were released & the date time of their release.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <returns>ReleasePanelModel containing an indication if the Applications were released & the date time of there release</returns>
        public Webmodel.IReleasePanelModel NewIsReleased(int sessionPanelId)
        {
            ValidateIsReleasedParameters(sessionPanelId);
            SessionPanel sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            bool isReleased = UnitOfWork.PanelApplicationRepository.IsReleased(sessionPanelId);
            DateTime? releaseDateTime = null;
            if (isReleased)
            {
                releaseDateTime = sessionPanelEntity.ReleaseDate();
            }
            Webmodel.IReleasePanelModel result = new Webmodel.ReleasePanelModel(isReleased, releaseDateTime);

            return result;
        }
        /// <summary>
        /// Gets the meeting session id for this session panel
        /// </summary>
        /// <param name="sessionPanelId"></param>
        /// <returns>Meeting Session Identifier</returns>
        public int? GetMeetingSessionId(int sessionPanelId)
        {
            ValidateGetSessionMeetingIdParameters(sessionPanelId);
            SessionPanel aSessionPanel = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);

            return aSessionPanel.MeetingSessionId;
        }
        /// <summary>
        /// Determines whether [is meeting current] [the specified session panel identifier].
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is meeting current] [the specified session panel identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMeetingCurrent(int sessionPanelId)
        {
            Validate("PanelManagementService.IsMeetingCurrent", sessionPanelId, nameof(sessionPanelId));
            SessionPanel sessionPanel = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            return (sessionPanel != null && sessionPanel.MeetingSession != null &&
                sessionPanel.MeetingSession.StartDate <= GlobalProperties.P2rmisDateTimeNow && GlobalProperties.P2rmisDateTimeNow < sessionPanel.MeetingSession.EndDate);
        }
        /// <summary>
        /// Update reviewer assignment to Coi or insert new assignment as Coi
        /// </summary>
        /// <param name="panelApplicationId">The application identifier</param>
        /// <param name="panelUserAssignmentId">The panel user assignment</param>
        /// <param name="clientAssignment">The client Assignment Type identifier</param>
        /// <param name="clientCoiTypeId"></param>
        /// <param name="clientExpertiseRatingId">The client expertise rating id</param> 
        /// <param name="comment">The comment associated with the assignment</param>
        /// <param name="userId">The user identifier making the assignment</param>
        /// <returns>ReviewerAssignmentStatus.IncompleteAssignmentData if comment, clientAssignmentTypeId or clientEpertiseRatingId do not have values,
        /// ReviewerAssignmentStatus.Success otherwise</returns>
        private ReviewerAssignmentStatus AssignReviewerAsCoi(int panelApplicationId, int panelUserAssignmentId, int? clientAssignmentTypeId, int? clientCoiTypeId, int? clientExpertiseRatingId, string comment, int userId)
        {
            ReviewerAssignmentStatus result = (!string.IsNullOrWhiteSpace(comment) && clientAssignmentTypeId.HasValue && clientExpertiseRatingId.HasValue && clientCoiTypeId.HasValue) ?
                ReviewerAssignmentStatus.Success :
                (string.IsNullOrWhiteSpace(comment) && !clientCoiTypeId.HasValue) ? ReviewerAssignmentStatus.MissingCoiTypeAndComments :
                (!clientCoiTypeId.HasValue) ? ReviewerAssignmentStatus.MissingCoiType :
                (string.IsNullOrWhiteSpace(comment)) ? ReviewerAssignmentStatus.MissingComments : ReviewerAssignmentStatus.IncompleteAssignmentData;
            if (result == ReviewerAssignmentStatus.Success)
            {
                int? coiSortOrder = null;

                // first remove existing assignment, any critiques will be preserved by flagging the records as deleted
                UnAssignReviewer(panelUserAssignmentId, panelApplicationId, userId, true);

                // make new COI assignment
                AssignReviewer(coiSortOrder, clientAssignmentTypeId.Value, panelApplicationId, panelUserAssignmentId, userId);

                SaveReviewerExpertise(panelApplicationId, panelUserAssignmentId, clientExpertiseRatingId, userId, comment);
                //
                // Save any changes.  We need the PanelApplicationReviewerExpertiseId.  This is generated by the database when the record is saved
                //
                UnitOfWork.Save();

                // get the required id
                PanelApplicationReviewerExpertise expertise = this.UnitOfWork.PanelApplicationReviewerExpertiseRepository.Get(u => u.PanelApplicationId == panelApplicationId && u.PanelUserAssignmentId == panelUserAssignmentId).FirstOrDefault();

                SavePanelApplicationReviewerCoiDetail(clientCoiTypeId.Value, expertise.PanelApplicationReviewerExpertiseId, userId);

                UnitOfWork.Save();
            }

            return result;
        }

        /// <summary>
        /// Validates the parameters for ReleaseApplications.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <param name="userId">User identifier of user making the change</param>
        internal virtual void StartWorkflowsForReleasedApplications(int sessionPanelId, int userId)
        {
            StartReviewerWorkflowForPanel(sessionPanelId, userId);
        }
        /// <summary>
        /// Determines if a SessionPanel's Applications have been set up for scoring.
        /// </summary>
        /// <param name="sessionPanelId"></param>
        /// <returns>ISessionApplicationScoringSetupModel model indicating if the session's applications have been set up for scoring</returns>
        public Webmodel.ISessionApplicationScoringSetupModel IsSessionApplicationsScoringSetUp(int sessionPanelId)
        {
            ValidateInt(sessionPanelId, FullName(nameof(PanelManagementService), nameof(IsSessionApplicationsScoringSetUp)), nameof(sessionPanelId));

            Webmodel.ISessionApplicationScoringSetupModel result = new Webmodel.SessionApplicationScoringSetupModel();
            SessionPanel sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);

            bool isSetUp = sessionPanelEntity.IsScoringSetup();
            result.IsScoringSetUp = isSetUp;
            return result;
        }
        #endregion
        #region StartReviewerWorkflow
        /// <summary>
        /// Sets up all the necessary information for a reviewer to evaluate an application
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier</param>
        /// <param name="panelApplicationId">The application identifier</param>
        /// <param name="userId">User identifier</param>
        public void StartReviewerWorkflow(int panelUserAssignmentId, int panelApplicationId, int userId)
        {
            ValidateStartReviewerWorkflow(panelUserAssignmentId, panelApplicationId, userId);

            PanelApplicationReviewerAssignment assignment = this.UnitOfWork.PanelApplicationReviewerAssignmentRepository.GetReviewerAssignmentForCritique(panelUserAssignmentId, panelApplicationId);
            if (assignment != null)
            {
                if (assignment.PanelApplication.ApplicationStages.Where(m => m.AssignmentVisibilityFlag == true && m.ReviewStageId == ReviewStage.Asynchronous) != null)
                {
                    this.UnitOfWork.PanelApplicationReviewerAssignmentRepository.StartReviewerWorkflow(assignment.PanelApplicationReviewerAssignmentId, userId);
                }
            }
        }
        /// <summary>
        /// Sets up all the necessary information for a reviewer to evaluate an application for an entire panel
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier</param>
        /// <param name="panelApplicationId">The application identifier</param>
        /// <param name="userId">User identifier</param>
        public void StartReviewerWorkflowForPanel(int sessionPanelId, int userId)
        {
            ValidateInt(sessionPanelId, nameof(StartReviewerWorkflowForPanel), nameof(sessionPanelId));
            ValidateInt(userId, nameof(StartReviewerWorkflowForPanel), nameof(userId));

            this.UnitOfWork.PanelApplicationReviewerAssignmentRepository.StartReviewerWorkflowForPanel(sessionPanelId, userId);
        }
        /// <summary>
        /// Validates the parameters for StartReviewerWorkflow.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier</param>
        /// <param name="applicationId">The application identifier</param>
        /// <param name="userId">User identifier</param>
        private void ValidateStartReviewerWorkflow(int panelUserAssignmentId, int panelApplicationId, int userId)
        {
            this.ValidateInteger(panelUserAssignmentId, "PanelManagementService.StartReviewerWorkflow", "panelUserAssignmentId");
            this.ValidateInteger(panelApplicationId, "PanelManagementService.StartReviewerWorkflow", "panelApplicationId");
            this.ValidateInteger(userId, "PanelManagementService.StartReviewerWorkflow", "userId");
        }
        #endregion
        #region Communications
        /// <summary>
        /// Retrieves the content of the specified email
        /// </summary>
        /// <param name="communicationsLogId"></param>
        /// <returns></returns>
        public Webmodel.IEmailContent GetEmailContent(int communicationsLogId)
        {
            ValidateGetEmailContentParameters(communicationsLogId);

            Webmodel.EmailContent content = new Webmodel.EmailContent();
            CommunicationLog log = this.UnitOfWork.CommunicationLogRepository.GetByID(communicationsLogId);
            List<IEmailAttachment> attachments = new List<IEmailAttachment>();

            User userEntity = this.UnitOfWork.UserRepository.GetByID(log.CreatedBy.Value);
            UserInfo userInfoEntity = userEntity.UserInfoEntity();

            content.Date = log.CreatedDate;
            content.Subject = log.Subject;
            content.Message = log.Message;

            // get the email address information for the sender and recipients
            content.From = new EmailAddress(userInfoEntity.FirstName, userInfoEntity.LastName, userEntity.PrimaryUserEmailAddress(), log.ParticipantTypeAbbreviation(), log.SessionPanelId, log.PanelUserAssignmentId());
            var to = UnitOfWork.EmailRepository.FillEmailAddressRecipientList(log.CommunicationLogRecipients, CommunicationLogRecipientType.ToRecipientType, log.SessionPanelId);
            content.To = to.ModelList.ToList();
            var cc = UnitOfWork.EmailRepository.FillEmailAddressRecipientList(log.CommunicationLogRecipients, CommunicationLogRecipientType.CCRecipientType, log.SessionPanelId);
            content.Cc = cc.ModelList.ToList();
            content.Bcc = log.BCC;

            var listOfObjects = log.CommunicationLogAttachments.ToList().Select(x => new EmailAttachment(x.AttachmentFileName, x.AttachmentLocation));
            content.Attachments = new List<IEmailAttachment>(listOfObjects.Cast<IEmailAttachment>());

            return content;
        }
        /// <summary>
        /// Returns a list of the emails for the indicated session panel
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier</param>
        /// <returns></returns>
        public Container<Webmodel.ISessionPanelCommunicationsList> ListPanelCommunicationMessages(int sessionPanelId)
        {
            ValidateListPanelCommunicationMessagesParameters(sessionPanelId);

            Container<Webmodel.ISessionPanelCommunicationsList> container = new Container<Webmodel.ISessionPanelCommunicationsList>();
            var results = new List<Webmodel.ISessionPanelCommunicationsList>();

            this.UnitOfWork.CommunicationLogRepository.Get(x => x.SessionPanelId == sessionPanelId).Select(x =>
                new Webmodel.SessionPanelCommunicationsList(x.CreatedDate, (int)x.CreatedBy, x.Subject, x.CommunicationLogId)).OrderByDescending(y => y.CommunicationLogId)
                .ToList().ForEach(delegate (Webmodel.SessionPanelCommunicationsList y)
                {
                    y.FromEmailAddress = this.UnitOfWork.UserRepository.GetByID(y.FromUserId).PrimaryUserEmailAddress();
                    results.Add(y);
                });

            container.ModelList = results;
            return container;
        }

        #endregion
        #region Critiques
        /// <summary>
        /// This is a convenience feature that implements the Submit link processing for all submittable critiques
        /// on a panel.
        /// </summary>
        /// <param name="theWorkflowService">An instance of the workflow service</param>
        /// <param name="sessionPanelId">The panel being 'submitted'</param>
        /// <param name="stepTypeId">The step type identifier.</param>
        /// <param name="userId">User requesting the action</param>
        public void FinalizeCritique(IWorkflowService theWorkflowService, int sessionPanelId, int stepTypeId, int userId)
        {
            ValidateFinalizeCritiqueParameters(sessionPanelId, stepTypeId, userId);
            //
            // First things first, get the list of workflow steps for this session panel
            // that have content but are not finalized.
            //
            IEnumerable<ApplicationWorkflowStep> steps = UnitOfWork.ApplicationWorkflowStepRepository.ListSessionPanelSubmittableWorkflowSteps(sessionPanelId, stepTypeId);

            foreach (var applicationWorkflowStep in steps)
            {
                if (applicationWorkflowStep.IsCritiqueSubmittable())
                {
                    theWorkflowService.ExecuteSubmitWorkflow(applicationWorkflowStep.ApplicationWorkflowId, userId);
                }
            }
        }
        /// <summary>
        /// Retrieves contents of the reviewers critique
        /// </summary>
        /// <param name="applicationWorkflowStepId">The applicationWorkflowStep identifier</param>
        /// <returns>IApplicationCritiqueDetailsModel</returns>
        public Webmodel.IApplicationCritiqueDetailsModel GetApplicationCritiqueDetails(int applicationWorkflowStepId)
        {
            this.ValidateParameter(applicationWorkflowStepId, "PanelManagementService.GetApplicationCritiqueDetails", "applicationWorkflowStepId");

            var model = this.UnitOfWork.PanelManagementRepository.GetApplicationCritiqueDetails(applicationWorkflowStepId);

            return model;
        }
        /// <summary>
        /// Update the phase ReOpen date & Close date.  Dates are validated (endDate <= reOpenDate <= closeDate)
        /// </summary>
        /// <param name="endDate">The EndDate of the phase</param>
        /// <param name="reOpenDate">The new ReOpen date of the phase</param>
        /// <param name="closeDate">The new Close date of the phase<</param>
        /// <param name="meetingSessionId">Meeting session identifier</param>
        /// <param name="stageTypeId">Stage type identifier indicating the phase</param>
        /// <param name="userId">User identifier of the person making the change</param>
        /// <returns>PanelStageDateUpdateStatus enum value indicating the result of the validation/update</returns>
        public PanelStageDateUpdateStatus UpdatePanelStageDates(DateTime endDate, DateTime reOpenDate, DateTime closeDate, int meetingSessionId, int stageTypeId, int userId)
        {
            ValidateUpdatePanelStageDatesParameters(endDate, reOpenDate, closeDate, meetingSessionId, stageTypeId, userId);
            //
            // The first thing we need to do is to validate the dates (actually their relationship to each other)
            //
            PanelStageDateUpdateStatus result = ArePanelStageDatesValid(endDate, reOpenDate, closeDate);
            if (result == PanelStageDateUpdateStatus.Success)
            {
                //
                // Get the meeting session
                //
                MeetingSession theMeetingSession = this.UnitOfWork.MeetingSessionRepository.GetByID(meetingSessionId);
                //
                // Now get all the panel stage steps for the meeting session
                //
                var thePanelStageSteps = theMeetingSession.ListStageSteps(stageTypeId);
                //
                // Now comes the fun part.  Instead of updated a single record, the dates have to be updated in all PanelStageSteps.
                //
                thePanelStageSteps.ToList().ForEach(delegate (PanelStageStep panelStageStep)
                    {
                        panelStageStep.UpdateDates(reOpenDate, closeDate, userId);
                        UnitOfWork.PanelStageStepRepository.Update(panelStageStep);
                    }
                );
                //
                // Now we save everything at once
                //
                UnitOfWork.Save();
            }
            return result;
        }
        /// <summary>
        /// Sends the critique reset email.
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="mailService">The mail service.</param>
        /// <param name="userId">The user identifier.</param>
        public void SendCritiqueResetEmail(int applicationWorkflowId, IMailService mailService, int userId)
        {
            ValidateSendCritiqueResetEmail(applicationWorkflowId, userId);

            ApplicationWorkflow applicationWorkflow = this.UnitOfWork.ApplicationWorkflowRepository.GetByID(applicationWorkflowId);
            User reviewer = this.UnitOfWork.UserRepository.GetByID(applicationWorkflow.PanelUserAssignment.UserId);
            UserInfo reviewerInfo = this.UnitOfWork.UserInfoRepository.GetByID(reviewer.UserInfoes.FirstOrDefault().UserInfoID);
            string reviewerName = ViewHelpers.ConstructNameWithPrefix(reviewerInfo.Prefix?.PrefixName.Trim(), reviewerInfo.FirstName, reviewerInfo.LastName);
            SessionPanel sessionPanel = applicationWorkflow.PanelUserAssignment.SessionPanel;
            ProgramYear programYear = sessionPanel.ProgramPanels.FirstOrDefault().ProgramYear;
            var sroModel = mailService.ListPanelSroEmailAddresses(sessionPanel.SessionPanelId).ModelList.FirstOrDefault();
            string sroEmail = ConfigManager.HelpDeskEmailAddress;
            if (sroModel != null)
            {
                sroEmail = sroModel.UserEmailAddress;
            }   
                
            mailService.CritiqueReset(userId, reviewerName, reviewer.PrimaryUserEmailAddress(), programYear.Year,
                programYear.ClientProgram.ProgramDescription, sessionPanel.PanelName,
                sessionPanel.PanelAbbreviation, applicationWorkflow.ApplicationStage.PanelApplication.Application.LogNumber, sroEmail);
        }
        /// <summary>
        /// Checks the relationship between the phase dates.
        /// </summary>
        /// <param name="endDate">The EndDate of the phase</param>
        /// <param name="reOpenDate">The new ReOpen date of the phase</param>
        /// <param name="closeDate">The new Close date of the phase<</param>
        internal virtual PanelStageDateUpdateStatus ArePanelStageDatesValid(DateTime endDate, DateTime reOpenDate, DateTime closeDate)
        {
            PanelStageDateUpdateStatus result = PanelStageDateUpdateStatus.Success;
            //
            // Now to validations.  There are several validations:
            // 1) the Reopen date must be greater than or equal to the EndDate
            //
            if (!(reOpenDate > endDate))
            {
                result = PanelStageDateUpdateStatus.ReOpenDateInvalid;
            }
            //
            // 2) the Close date must be greater than or equal to the Reopen
            //
            if (!(closeDate >= reOpenDate))
            {
                result = (result == PanelStageDateUpdateStatus.Success) ? PanelStageDateUpdateStatus.CloseDateInvalid : PanelStageDateUpdateStatus.BothDatesInvalid;
            }
            //
            // 3) Save dates
            //
            if (closeDate == reOpenDate)
            {
                result = PanelStageDateUpdateStatus.SameDates;
            }
            return result;
        }
        /// <summary>
        /// Validates the parameters for UpdatePanelStageDates
        /// </summary>
        /// <param name="endDate">The EndDate of the phase</param>
        /// <param name="reOpenDate">The new ReOpen date of the phase</param>
        /// <param name="closeDate">The new Close date of the phase<</param>
        /// <param name="meetingSessionId"></param>
        /// <param name="stageTypeId"></param>
        /// <param name="userId"></param>
        private void ValidateUpdatePanelStageDatesParameters(DateTime endDate, DateTime reOpenDate, DateTime closeDate, int meetingSessionId, int stageTypeId, int userId)
        {
            this.ValidateDateTime(endDate, "PanelManagementService.UpdatePanelStageDates", "endDate");
            this.ValidateDateTime(reOpenDate, "PanelManagementService.UpdatePanelStageDates", "reOpenDate");
            this.ValidateDateTime(closeDate, "PanelManagementService.UpdatePanelStageDates", "closeDate");
            this.ValidateInteger(meetingSessionId, "PanelManagementService.UpdatePanelStageDates", "meetingSessionId");
            this.ValidateInteger(stageTypeId, "PanelManagementService.UpdatePanelStageDates", "stageTypeId");
            this.ValidateInteger(userId, "PanelManagementService.UpdatePanelStageDates", "userId");
        }
        #endregion
        #region Panel Overview
        /// <summary>
        /// Retrieves the PanelApplication Summary text.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <returns>PanelApplicationSummary text</returns>
        public string GetPanelSummary(int panelApplicationId)
        {
            PanelApplicationSummary panelApplicationSummaryEntity = GetPanelApplicationSummaryByApplicationId(panelApplicationId);
            return (panelApplicationSummaryEntity != null) ? panelApplicationSummaryEntity.SummaryText : string.Empty;
        }
        /// <summary>
        /// Save the summary statement overview paragraph.
        /// </summary>
        /// <param name="panelApplicationId">Application entity identifier</param>
        /// <param name="panelOverview">Overview content</param>
        /// <param name="userId">User identifier of submitting user</param>
        /// <returns>True indicates the overview was saved successfully successfully; false otherwise</returns>
        public bool SavePanelOverview(int panelApplicationId, string panelOverview, int userId)
        {
            ValidateSavePanelOverviewParameters(panelApplicationId, userId);

            bool result = false;

            PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);

            if (!panelApplicationEntity.IsSummaryStarted())
            {
                PanelApplicationSummary panelApplicationSummary = GetPanelApplicationSummaryByApplicationId(panelApplicationId);

                if (panelApplicationSummary == null)
                {
                    AddPanelApplicationSummary(panelApplicationId, panelOverview, userId);
                }
                else
                {
                    ModifyPanelApplicationSummary(panelApplicationId, panelOverview, userId);
                }
                result = true;
            }
            return result;
        }
        /// <summary>
        /// Adds the panel applications.
        /// </summary>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="applicationIds">The application ids.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public bool AddPanelApplications(int panelId, List<int> applicationIds, int userId)
        {
            ValidateAddPanelApplications(panelId, applicationIds, userId);
            //Get panel setup info for application assignment
            var panelInfo = UnitOfWork.PanelManagementRepository.GetPanelStageInformation(panelId);
            foreach (int applicationId in applicationIds)
            {
                AddPanelApplication(panelId, applicationId, userId, panelInfo, false);
            }
            UnitOfWork.Save();
            return true;
        }
        /// <summary>
        /// Adds the batch panel applications.
        /// </summary>
        /// <param name="panelApplicationsList">The panel applications list.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public bool AddBatchPanelApplications(List<KeyValuePair<int, List<int>>> panelApplicationsList, int userId)
        {
            foreach (var panelApplications in panelApplicationsList)
            {
                ValidateAddPanelApplications(panelApplications.Key, panelApplications.Value, userId);
                //Get panel setup info for application assignment
                var panelInfo = UnitOfWork.PanelManagementRepository.GetPanelStageInformation(panelApplications.Key);
                foreach (int applicationId in panelApplications.Value)
                {
                    AddPanelApplication(panelApplications.Key, applicationId, userId, panelInfo, false);
                }
            }
            UnitOfWork.Save();
            return true;
        }
        /// <summary>
        /// Retrieves the PanelApplicationSummary entity by PanelApplicationId id
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <returns>PanelApplicationSummary entity</returns>
        /// <remarks>
        /// Assumes the PanelApplication entity identifier is valid resulting in a PanelApplicationSummary
        /// </remarks>
        internal virtual PanelApplicationSummary GetPanelApplicationSummaryByApplicationId(int panelApplicationId)
        {
            return UnitOfWork.PanelApplicationSummaryRepository.Get(x => x.PanelApplicationId == panelApplicationId).FirstOrDefault();
        }
        /// <summary>
        /// Adds a new summary text for the panel application.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="summaryText">Summary text to add</param>
        /// <param name="userId">User identifier of user making the change</param>
        internal virtual void AddPanelApplicationSummary(int panelApplicationId, string summaryText, int userId)
        {
            PanelApplicationSummary panelApplicationSummaryEntity = new PanelApplicationSummary();
            panelApplicationSummaryEntity.Populate(panelApplicationId, summaryText);

            Helper.UpdateCreatedFields(panelApplicationSummaryEntity, userId);
            Helper.UpdateModifiedFields(panelApplicationSummaryEntity, userId);

            UnitOfWork.PanelApplicationSummaryRepository.Add(panelApplicationSummaryEntity);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Adds the panel application.
        /// </summary>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="saveUoW">if set to <c>true</c> [save uo w].</param>
        internal virtual void AddPanelApplication(int panelId, int applicationId, int userId, IPanelStageResultModel panelStageInfo, bool saveUoW)
        {
            PanelApplication panelApplicationEntity = new PanelApplication();
            panelApplicationEntity.SetPanelApplication(panelId, applicationId, userId);
            UnitOfWork.PanelApplicationRepository.Add(panelApplicationEntity);
            AddApplicationStageAndStepsForReview(panelApplicationEntity, userId, panelStageInfo);
            AddNewPanelApplicationReviewStatus(panelApplicationEntity, userId);
            if (saveUoW)
            {
                UnitOfWork.Save();
            }
        }

        /// <summary>
        /// Adds the new panel application review status.
        /// </summary>
        /// <param name="panelApplicationEntity">The panel application entity.</param>
        internal void AddNewPanelApplicationReviewStatus(PanelApplication panelApplicationEntity, int userId)
        {
            ApplicationReviewStatu appReviewStatus = new ApplicationReviewStatu();
            appReviewStatus.Populate(ReviewStatu.DefaultReviewStatus);

            Helper.UpdateCreatedFields(appReviewStatus, userId);
            Helper.UpdateModifiedFields(appReviewStatus, userId);

            panelApplicationEntity.ApplicationReviewStatus.Add(appReviewStatus);
            UnitOfWork.ApplicationReviewStatusRepository.Add(appReviewStatus);
        }

        /// <summary>
        /// Adds the application stage and steps for review.
        /// </summary>
        /// <param name="panelApplicationEntity">The panel application entity.</param>
        internal void AddApplicationStageAndStepsForReview(PanelApplication panelApplicationEntity, int userId, IPanelStageResultModel panelStageInfo)
        {
            //Loop over stages and add them
            foreach (var stage in panelStageInfo.PanelStageAndSteps)
            {
                AddStageAndSteps(panelApplicationEntity, userId, panelStageInfo.AssignmentsReleased, stage);
            }
        }

        /// <summary>
        /// Adds the stage and steps.
        /// </summary>
        /// <param name="panelApplicationEntity">The panel application entity.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="assignmentsReleased">if set to <c>true</c> [assignments released].</param>
        /// <param name="stageToAdd">The stage to add.</param>
        internal void AddStageAndSteps(PanelApplication panelApplicationEntity, int userId, bool assignmentsReleased, PanelStage stageToAdd)
        {
            ApplicationStage applicationStage = new ApplicationStage();
            //release date only gets set if assignments were prior released on the panel
            DateTime? assignmentReleaseDate = assignmentsReleased ? (DateTime?)GlobalProperties.P2rmisDateTimeNow : null;
            applicationStage.Populate(stageToAdd.ReviewStageId, stageToAdd.StageOrder, stageToAdd.ReviewStageId == ReviewStage.Asynchronous, assignmentsReleased, assignmentReleaseDate);

            //loop through and add the stage steps at the application level
            foreach (var step in stageToAdd.PanelStageSteps)
            {
                AddApplicationStageStep(step, applicationStage, userId);
            }

            Helper.UpdateCreatedFields(applicationStage, userId);
            Helper.UpdateModifiedFields(applicationStage, userId);

            panelApplicationEntity.ApplicationStages.Add(applicationStage);
            UnitOfWork.ApplicationStageRepository.Add(applicationStage);
        }

        internal void AddApplicationStageStep(PanelStageStep step, ApplicationStage applicationStage, int userId)
        {
            ApplicationStageStep applicationStageStepEntity = new ApplicationStageStep();
            applicationStageStepEntity.Populate(step.PanelStageStepId);

            Helper.UpdateCreatedFields(applicationStageStepEntity, userId);
            Helper.UpdateModifiedFields(applicationStageStepEntity, userId);

            applicationStage.ApplicationStageSteps.Add(applicationStageStepEntity);
            UnitOfWork.ApplicationStageStepRepository.Add(applicationStageStepEntity);
        }

        /// <summary>
        /// Modifies an existing PanelApplication's summary text
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="summaryText">Summary text</param>
        /// <param name="userId">User identifier of user making the change</param>
        internal virtual void ModifyPanelApplicationSummary(int panelApplicationId, string summaryText, int userId)
        {
            PanelApplicationSummary panelApplicationSummaryEntity = GetPanelApplicationSummaryByApplicationId(panelApplicationId);
            panelApplicationSummaryEntity.Populate(panelApplicationId, summaryText);
            //
            // The code will update the modified time fields even if there was no changes to the text. The record will 
            // be update with a new time because we updated the time.  There is a method in the Helper for Avocado
            // that actually checks the status of the entity and will update the time field accordingly.  When the
            // branches are merged the helper method should be updated
            //
            Helper.UpdateModifiedFields(panelApplicationSummaryEntity, userId);

            UnitOfWork.PanelApplicationSummaryRepository.Update(panelApplicationSummaryEntity);
            UnitOfWork.Save();
        }
        #endregion
        #region Permission Validation Support
        /// <summary>
        /// Determines if a specified user has an existing assignment or potential assignment 
        /// to an SRO's panel.
        /// </summary>
        /// <param name="sroUserId">User entity identifier of an SRO</param>
        /// <param name="reviewerUserInfoId">UserInfo entity identifier of target user</param>
        /// <returns>True if the user is has an assignment or potential assignment to the SRO's panel; false otherwise</returns>
        public bool IsUserAssignedToSroPanel(int sroUserId, int reviewerUserInfoId)
        {
            string name = FullName(nameof(PanelManagementService), nameof(IsUserAssignedToSroPanel));
            ValidateInteger(sroUserId, name, nameof(sroUserId));
            ValidateInteger(reviewerUserInfoId, name, nameof(reviewerUserInfoId));

            SroAssignmentPermissionBuilder builder = new SroAssignmentPermissionBuilder(this.UnitOfWork, sroUserId, reviewerUserInfoId);
            bool result = builder.Check();
            return result;
        }
        #endregion
        #region Remove/Transfer Application from Panel
        /// <summary>
        /// Removes the application from panel.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void RemoveApplicationFromPanel(int panelApplicationId, int userId)
        {
            ValidateInteger(panelApplicationId, "PanelManagementService.RemoveApplicationFromPanel", "panelApplicationId");
            ValidateInteger(userId, "PanelManagementService.RemoveApplicationFromPanel", "userId");

            UnitOfWork.PanelManagementRepository.RemoveApplicationFromPanel(panelApplicationId, userId);
        }

        /// <summary>
        /// Transfers the application to a specified panel.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="newSessionPanelId">The new session panel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="System.ArgumentException">Invalid newSessionPanelId</exception>
        public void TransferApplicationToPanel(int panelApplicationId, int applicationId, int newSessionPanelId, int userId)
        {
            ValidateInteger(panelApplicationId, "PanelManagementService.TransferApplicationToPanel", "panelApplicationId");
            ValidateInteger(newSessionPanelId, "PanelManagementService.TransferApplicationToPanel", "newSessionPanelId");
            ValidateInteger(applicationId, "PanelManagementService.TransferApplicationToPanel", "applicationId");
            ValidateInteger(userId, "PanelManagementService.RemoveApplicationFromPanel", "userId");

            var panelInfo = UnitOfWork.PanelManagementRepository.GetPanelStageInformation(newSessionPanelId);
            if (panelInfo != null)
            {
                UnitOfWork.PanelManagementRepository.RemoveApplicationFromPanel(panelApplicationId, userId);
                AddPanelApplication(newSessionPanelId, applicationId, userId, panelInfo, true);
            }
            else
                throw new ArgumentException("Invalid newSessionPanelId");
        }
        #endregion
        #endregion
        #region Helpers
        /// <summary>
        /// Validates SetOrderOfReviewParameters
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="collection">Collection of SetOrderOfReviewToSave objects</param>
        /// <param name="userId">User identifier</param>
        private void ValidateSetOrderOfReviewParameters(int sessionPanelId, ICollection<SetOrderOfReviewToSave> collection, int userId)
        {
            this.ValidateInteger(userId, "PanelManagementService.SetOrderOfReview", "userId");
            this.ValidateInteger(sessionPanelId, "PanelManagementService.SetOrderOfReview", "sessionPanelId");
            this.ValidateCollection(collection, "PanelManagementService.SetOrderOfReview", "collection");
        }
        /// <summary>
        /// Validates SetOrderOfReviewParameters
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="collection">Collection of SetOrderOfReviewToSave objects</param>
        /// <param name="userId">User identifier</param>
        private void ValidateSetAssignmentTypeThreshold(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementService.SetOrderOfReview", "sessionPanelId");
        }
        /// <summary>
        /// Validates SaveReviewerEvaluationParameters
        /// </summary>
        /// <param name="userId">User identifier</param>
        private void ValidateSaveReviewerEvaluationParameters(int userId)
        {
            this.ValidateInteger(userId, "PanelManagementService.SaveReviewerEvaluation", "userId");
        }
        /// <summary>
        /// Validates ReviewerHasWorkflow parameters
        /// </summary>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        private void ValidateReviewerHasWorkflowParameter(int panelUserAssignmentId, int panelApplicationId)
        {
            this.ValidateInteger(panelUserAssignmentId, "PanelManagementService.ReviewerHasWorkflow", "panelUserAssignmentId");
            this.ValidateInteger(panelApplicationId, "PanelManagementService.ReviewerHasWorkflow", "panelApplicationId");
        }
        /// <summary>
        /// Validates UnAssignReviewer parameters
        /// </summary>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="userId">User identifier of the currently logged-in user</param>
        private void ValidateUnAssignReviewerParameters(int panelUserAssignmentId, int panelApplicationId, int userId)
        {
            this.ValidateInteger(panelUserAssignmentId, "PanelManagementService.UnAssignReviewer", "panelUserAssignmentId");
            this.ValidateInteger(panelApplicationId, "PanelManagementService.UnAssignReviewer", "panelApplicationId");
            this.ValidateInteger(userId, "PanelManagementService.UnAssignReviewer", "userId");
        }
        /// <summary>
        /// Validates Assign parameters
        /// </summary>
        /// <param name="presentationOrder">New sort order</param>
        /// <param name="clientAssignmentTypeId">New client assignment type identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="userId">User identifier of user making the change</param>
        private void ValidateAssignParameters(int? presentationOrder, int clientAssignmentTypeId, int panelApplicationId, int panelUserAssignmentId, int userId)
        {
            this.ValidateNullableInteger(presentationOrder, "PanelManagementService.Assign", "presentationOrder");
            this.ValidateInteger(clientAssignmentTypeId, "PanelManagementService.Assign", "clientAssignmentTypeId");
            this.ValidateInteger(panelApplicationId, "PanelManagementService.Assign", "panelApplicationId");
            this.ValidateInteger(panelUserAssignmentId, "PanelManagementService.Assign", "panelUserAssignmentId");
            this.ValidateInteger(userId, "PanelManagementService.Assign", "userId");
        }
        /// <summary>
        /// Validates the parameters for ReleaseApplications.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <exception cref="ArgumentException">Thrown if sessionPanelId is invalid</exception>
        private void ValidateReleaseApplicationsParameters(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementService.ReleaseApplications", "sessionPanelId");
        }
        /// <summary>
        /// Validates the parameters for IsReleased.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <exception cref="ArgumentException">Thrown if sessionPanelId is invalid</exception>
        private void ValidateIsReleasedParameters(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementService.IsReleased", "sessionPanelId");
        }
        /// <summary>
        /// Validates GetProgamYear parameters
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        private void ValidateGetProgramYearParameters(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementService.GetProgramYear", "sessionPanelId");
        }
        /// <summary>
        /// Validates the get session panel parameters.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        private void ValidateGetSessionPanelParameters(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementService.GetSessionPanel", "sessionPanelId");
        }
        /// <summary>
        /// Validates GetEmailContent parameters
        /// </summary>
        /// <param name="communicationsLogId">Communications log identifier</param>
        private void ValidateGetEmailContentParameters(int communicationsLogId)
        {
            this.ValidateInteger(communicationsLogId, "PanelManagementService.GetEmailContent", "communicationsLogId");
        }
        /// <summary>
        /// Validates FinalizeCritique Parameters
        /// </summary>
        /// <param name="applicationWorkflowStepIds">Collection of ApplicationWorkflowStep identifiers</param>
        /// <param name="userId">User identifier</param>
        /// <exception cref="ArgumentException">Thrown if applicationWorkflowStepIds or userId is invalid</exception>
        private void ValidateFinalizeCritiqueParameters(int sessionPanelId, int stepTypeId, int userId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementService.FinalizeCritique", "sessionPanelId");
            this.ValidateInteger(stepTypeId, "PanelManagementService.FinalizeCritique", "stepTypeId");
            this.ValidateInteger(userId, "PanelManagementService.FinalizeCritique", "userId");
        }
        /// <summary>
        /// Validates the parameters for ListListPanelCommunicationMessages.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <exception cref="ArgumentException">Thrown if sessionPanelId is invalid (<0)</exception>
        private void ValidateListPanelCommunicationMessagesParameters(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "MailService.ListPanelCommunicationMessages", "sessionPanelId");
        }
        /// <summary>
        /// Validate the parameters for SavePanelOverview
        /// </summary>
        /// <param name="panelApplicationId">Application entity identifier</param>
        /// <param name="userId">User identifier of submitting user</param>
        private void ValidateSavePanelOverviewParameters(int panelApplicationId, int userId)
        {
            ValidateInteger(panelApplicationId, "PanelManagementService.SavePanelOverview", "panelApplicationId");
            ValidateInteger(userId, "PanelManagementService.SavePanelOverview", "userId");
        }
        /// <summary>
        /// Validates the add panel applications.
        /// </summary>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="applicationIds">The application ids.</param>
        /// <param name="userId">The user identifier.</param>
        private void ValidateAddPanelApplications(int panelId, List<int> applicationIds, int userId)
        {
            ValidateInteger(panelId, "PanelManagementService.AddPanelApplications", "panelId");
            foreach (var applicationId in applicationIds)
            {
                ValidateInteger(applicationId, "PanelManagementService.AddPanelApplications", "applicationId");
            }
            ValidateInteger(userId, "PanelManagementService.AddPanelApplications", "userId");
        }
        /// <summary>
        /// Validates GetSessionMeetingId parameters
        /// </summary>
        /// <param name="communicationsLogId">Session Panel identifier</param>
        private void ValidateGetSessionMeetingIdParameters(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "PanelManagementService.GetSessionMeetingId", "sessionPanelId");
        }
        /// <summary>
        /// Validates the send critique reset email.
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="userId">The user identifier.</param>
        private void ValidateSendCritiqueResetEmail(int applicationWorkflowId, int userId)
        {
            ValidateInteger(applicationWorkflowId, "PanelManagementService.SendCritiqueResetEmail", "applicationWorkflowId");
            ValidateInteger(userId, "PanelManagementService.SendCritiqueResetEmail", "userId");
        }
        /// <summary>
        /// Validates the is meeting phase started.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        private void ValidateIsMeetingPhaseStarted(int panelApplicationId)
        {
            ValidateInteger(panelApplicationId, "PanelManagementService.IsMeetingPhaseStarted", "panelApplicationId");
        }
        #endregion
        #region Expertise/Assignment
        /// <summary>
        /// Process a request for reviewer transfer between panels.
        /// </summary>
        /// <param name="theMailService">MailService</param>
        /// <param name="reviewerName">Reviewer name to transfer</param>
        /// <param name="sourcePanelId">Reviewer's SessionPanel entity identifier</param>
        /// <param name="sourcePanelName">Reviewer's SessionPanel name</param>
        /// <param name="sourcePanelAbbr">Reviewer's SessionPanel abbreviation</param>
        /// <param name="targetPanelId">Target SessionPanel entity identifier</param>
        /// <param name="comment">Comments on the transfer</param>
        /// <param name="userId">User entity identifier of the user requesting transfer</param>
        public void RequestReviewerTransferProcess(IMailService theMailService, string reviewerName, int sourcePanelId, string sourcePanelName, string sourcePanelAbbr, int? targetPanelId, string comment, int userId)
        {
            var panelNames = ListSiblingPanelNames(sourcePanelId);
            var targetPanel = panelNames.ModelList.Where(u => u.PanelId == targetPanelId).FirstOrDefault();

            var programYear = GetProgramYear(sourcePanelId);

            string message = theMailService.TransferReviewerRequest(userId, reviewerName, sourcePanelName, sourcePanelAbbr,
                targetPanel.Name, targetPanel.Abbreviation, programYear.FY, programYear.ProgramDescription, comment);

            LogReviewerTranferRequest(message, userId);
        }
        /// <summary>
        /// Process a request for reviewer release from a panels.
        /// </summary>
        /// <param name="thePanelManagementService">PanelManagementService</param>
        /// <param name="theMailService">MailService</param>
        /// <param name="reviewerName">Reviewer name to transfer</param>
        /// <param name="sourcePanelId">Reviewer's SessionPanel entity identifier</param>
        /// <param name="sourcePanelName">Reviewer's SessionPanel name</param>
        /// <param name="sourcePanelAbbr">Reviewer's SessionPanel abbreviation</param>
        /// <param name="comment">Comments on the transfer</param>
        /// <param name="userId">User entity identifier of the user requesting transfer</param>
        public void RequestReviewerReleaseProcess(IMailService theMailService, string reviewerName, int sourcePanelId, string sourcePanelName, string sourcePanelAbbr, string comment, int userId)
        {
            var programYear = GetProgramYear(sourcePanelId);
            string message = theMailService.ReleaseReviewerRequest(userId, sourcePanelId, reviewerName, sourcePanelName, sourcePanelAbbr, programYear.FY, programYear.ProgramDescription, comment);
            LogReviewerTranferRequest(message, userId);
        }
        /// <summary>
        /// Adds the staff to panel.
        /// </summary>
        /// <param name="assignedUserId">The user identifier to be assigned.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="actorUserId">The user identifier of the user making the assignment.</param>
        /// <returns>
        /// Name of the participant type the user was assigned.
        /// </returns>
        /// <exception cref="ArgumentException">Supplied assigned user could not be mapped to a valid participant type. Verify mapping exists in RoleParticipantType.</exception>
        public string AddStaffToPanel(int assignedUserId, int sessionPanelId, int actorUserId)
        {
            string newAssignmentName = string.Empty;
            //first get the appropriate participant type given the panel and user's role
            ClientParticipantType clientParticipantType = UnitOfWork.PanelManagementRepository.GetMappedParticipantType(assignedUserId, sessionPanelId);
            var participantMethod = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId).ParticipationMethodId();
            if (clientParticipantType != null)
            {
                // Create a PanelUserAssignment entity
                PanelUserAssignmentServiceAction serviceAction = new PanelUserAssignmentServiceAction();
                serviceAction.InitializeAction(this.UnitOfWork, UnitOfWork.PanelUserAssignmentRepository, ServiceAction<PanelUserAssignment>.DoNotUpdate, actorUserId);
                serviceAction.Populate(sessionPanelId, assignedUserId, clientParticipantType.ClientParticipantTypeId, participantMethod);
                serviceAction.Execute();
                //
                // Now finally save the results
                //
                UnitOfWork.Save();
                newAssignmentName = clientParticipantType.ParticipantTypeAbbreviation;

                //
                // If new assignment, we need to set up registration for the user
                //
                var panelAssignmentId = serviceAction.CreatedPanelUserAssignment.PanelUserAssignmentId;
                if (panelAssignmentId > 0 && clientParticipantType.IsCpritChair())
                {
                    UnitOfWork.PanelManagementRepository.SetupNewRegistration(panelAssignmentId, assignedUserId);
                }
            }
            else
                throw new ArgumentException("Supplied user could not be mapped to a valid participant type. Verify mapping exists in RoleParticipantType.");
            return newAssignmentName;

        }
        /// <summary>
        /// Get referral mapping error message
        /// </summary>
        /// <param name="referrals">The referrals.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="userId">The logged-in user identifier.</param>
        /// <returns></returns>
        public KeyValuePair<List<string>, int> SaveReferralMapping(List<KeyValuePair<string, string>> referrals, int programYearId, int receiptCycle, int userId)
        {
            List<string> errorMessages = new List<string>();
            int referralMappingId = 0;

            ReferralMapping referralMapping = new ReferralMapping();
            List<ReferralMappingData> referralMappingDataList = new List<ReferralMappingData>();

            var programYear = UnitOfWork.ProgramYearRepository.GetProgramYearWithProgram(programYearId).FirstOrDefault();
            var referralMappingDataVal = UnitOfWork.ReferralMappingRepository.GetReferralMappingDataByReferrals(referrals, programYearId, receiptCycle).ToList();

            errorMessages = GetReferralMappingUploadErrorMessages(referralMappingDataVal, programYearId, receiptCycle,
                        programYear.ClientProgram.ProgramAbbreviation, programYear.Year);
            if (errorMessages.Count == 0)
            {
                foreach (var item in referralMappingDataVal)
                {
                    var referralMappingData = new ReferralMappingData
                    {
                        ApplicationId = (int)item.ApplicationId,
                        SessionPanelId = (int)item.SessionPanelId,    
                    };
                    Helper.UpdateCreatedFields(referralMappingData, userId);
                    Helper.UpdateModifiedFields(referralMappingData, userId);
                    referralMappingDataList.Add(referralMappingData);
                }
                referralMapping.ProgramYearId = programYearId;
                referralMapping.ReceiptCycle = receiptCycle;
                referralMapping.ReferralMappingDatas = referralMappingDataList;
                Helper.UpdateCreatedFields(referralMapping, userId);
                Helper.UpdateModifiedFields(referralMapping, userId);

                UnitOfWork.ReferralMappingRepository.AddReferralMapping(referralMapping);
                UnitOfWork.Save();
                referralMappingId = referralMapping.ReferralMappingId;
            }
            return new KeyValuePair<List<string>, int>(errorMessages, referralMappingId);
        }
        /// <summary>
        /// Validates the referral mapping.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <param name="sessionPanelIds">The session panel ids.</param>
        /// <returns></returns>
        public List<string> ValidateReferralMapping(int referralMappingId, List<int> sessionPanelIds)
        {
            List<ReferralMappingData> referralMappingDataList = new List<ReferralMappingData>();
            var referralMapping = UnitOfWork.ReferralMappingRepository.GetByID(referralMappingId);
            System.Diagnostics.Debug.WriteLine(referralMapping);
            System.Diagnostics.Debug.WriteLine(sessionPanelIds);
            var programYear = UnitOfWork.ProgramYearRepository.GetProgramYearWithProgram(referralMapping.ProgramYearId).FirstOrDefault();
            var referralMappingDataVal = UnitOfWork.ReferralMappingRepository.GetReferralMappingDataById(referralMappingId)
                .Where(x => x.SessionPanelId != null && sessionPanelIds.Contains((int)x.SessionPanelId))
                .ToList();

            var errorMessages = GetReferralMappingReleaseErrorMessages(referralMappingDataVal,
                        referralMapping.ProgramYearId, referralMapping.ReceiptCycle,
                        programYear.ClientProgram.ProgramAbbreviation, programYear.Year);
            return errorMessages;
        }
        /// <summary>
        /// Get panel existing application
        /// </summary>
        /// <param name="programYearId"></param>
        /// <param name="receiptCycle"></param>
        /// <returns></returns>
        public List<ReferralMappingModel> GetReferralMappingApplications(int programYearId, int receiptCycle)
        {
            return UnitOfWork.ApplicationRepository.GetExistingApplications(programYearId, receiptCycle).ToList();
        }
        /// <summary>
        /// get applications by params
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="fiscalYear"></param>
        /// <param name="programYearId"></param>
        /// <param name="panelId"></param>
        /// <param name="receiptCycle"></param>
        /// <param name="awardId"></param>
        /// <param name="logNumber"></param>
        /// <returns></returns>
        public List<IApplicationsManagementModel> GetApplications(int? clientId, string fiscalYear, int? programYearId, int? panelId, int? receiptCycle, int? awardId, string logNumber, int userId)
        {
            return UnitOfWork.PanelManagementRepository.GetApplications(clientId, fiscalYear, programYearId, panelId,
                receiptCycle, awardId, logNumber, userId).ModelList.ToList();
        }

                /// <summary>
        /// Gets the referral mapping error.
        /// </summary>
        /// <param name="referralMappingData">The referral mapping exist.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="programAbbreviation">The program abbreviation.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public List<string> GetReferralMappingUploadErrorMessages(List<ReferralMappingDataModel> referralMappingData, int programYearId, int receiptCycle,
                string programAbbreviation, string year)
        {
            List<string> errorMessages = new List<string>();
            var i = 1;
            try
            {
                foreach (var referral in referralMappingData)
                {
                    var msgs = new List<string>();

                    if (referral.PanelApplicationId != null)
                    {
                        msgs.Add(string.Format("{0} is currently on {1} and cannot be included in a referral mapping", referral.AppLogNumber, referral.PanelAbbreviation));
                    }
                    if (referral.Cycle != receiptCycle)
                    {
                        msgs.Add(string.Format("{0} is not a valid application log number for this cycle", referral.AppLogNumber));
                    }
                    if (referral.SessionPanelId == null)
                    {
                        msgs.Add(string.Format("{0} is not a valid panel for {1} {2}", referral.PanelAbbreviation, programAbbreviation, year));
                    }
                    if (msgs.Count > 0)
                    {
                        var msgCombined = string.Join(", ", msgs.ToArray());
                        errorMessages.Add(string.Format("Row {0}: {1}.", i, msgCombined));
                    }
                    i++;
                }
                return errorMessages;
            }
            catch (Exception)
            {
                errorMessages.Add(string.Format("Row {0}: There was an error uploading the referral mapping data. Please try again.", i));
                return errorMessages;
            }
        }
        ///// <summary>
        ///// Gets the referral mapping release error messages.
        ///// </summary>
        ///// <param name="referralMappingData">The referral mapping data.</param>
        ///// <param name="programYearId">The program year identifier.</param>
        ///// <param name="receiptCycle">The receipt cycle.</param>
        ///// <param name="programAbbreviation">The program abbreviation.</param>
        ///// <param name="year">The year.</param>
        ///// <returns></returns>
        public List<string> GetReferralMappingReleaseErrorMessages(List<ReferralMappingDataModel> referralMappingData, int programYearId, int receiptCycle,
                string programAbbreviation, string year)
        {
            List<string> errorMessages = new List<string>();
            try
            {
                foreach (var referral in referralMappingData)
                {
                    var msg = default(string);
                    if (referral.PanelApplicationId != null)
                        msg = string.Format("An application you are trying to assign to {0} is already assigned to a panel. Please manage applications for this panel through Panel Management.", referral.PanelAbbreviation);

                    else if (referral.Cycle != receiptCycle || referral.SessionPanelId != null)
                        msg = string.Format("There was an error assigning applications to Panel {0}. Please try again.", referral.PanelAbbreviation);

                    if (!string.IsNullOrEmpty(msg) && !errorMessages.Contains(msg))
                        errorMessages.Add(msg);
                }
                return errorMessages;
            }
            catch (Exception)
            {
                errorMessages.Add("There was an unexpected error assigning applications. Please try again.");
                return errorMessages;
            }
        }
    }
    #endregion
}

