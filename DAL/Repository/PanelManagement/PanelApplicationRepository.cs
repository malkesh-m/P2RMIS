using System.Linq;
using System.Collections.Generic;
using Sra.P2rmis.Dal.Interfaces;
using System.Data.Entity;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for PanelApplication objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>   
    public class PanelApplicationRepository : GenericRepository<PanelApplication>, IPanelApplicationRepository
    {
        #region Construction & Setup & Disposal                
        /// <summary>
        /// The extended command timeout in seconds
        /// </summary>
        public const int ExtendedCommandTimeout = 120;
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public PanelApplicationRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        /// <summary>
        /// The parameter less constructor is used for testing
        /// </summary>
        public PanelApplicationRepository()
        {

        }
        #endregion
        #region ServicesProvided
        /// <summary>
        /// Retrieve the PanelApplicationReviewerAssignment entity object with the specified sort order
        /// on the PanelApplication 
        /// </summary>
        /// <param name="presentationOrder">Presentation order</param>
        /// <param name="panelApplicationId">Panel Application identifier</param>
        /// <returns>PanelApplicationReviewerAssignment entity object with the specified presentation order</returns>
        public PanelApplicationReviewerAssignment GetPanelApplicationReviewerAssignment(int presentationOrder, int panelApplicationId)
        {
            //
            // Get the application on the specified panel
            //
            var panelApplication = this.GetByID(panelApplicationId);
            //
            // Search the reviewer assignments for the panel application with the same presentation order.  Also want to make sure it is not deleted.
            //
            PanelApplicationReviewerAssignment result = panelApplication.PanelApplicationReviewerAssignments.FirstOrDefault(x => (x.SortOrder == presentationOrder));

            return result;
        }
        /// <summary>
        /// Retrieve the PanelApplicationReviewerAssignment entity object for a specific reviewer
        /// on the PanelApplication 
        /// </summary>
        /// <param name="panelApplicationId">Panel Application identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <returns>PanelApplicationReviewerAssignment entity object for the specified reviewer or null if none found</returns>
        public PanelApplicationReviewerAssignment GetPanelApplicationReviewerAssignmentForSpecificReviewer(int panelApplicationId, int panelUserAssignmentId)
        {
            //
            // Get the application on the specified panel
            //
            var panelApplication = this.GetByID(panelApplicationId);
            //
            // Search the reviewer assignments for the panel application with the same presentation order.  Also want to make sure it is not deleted.
            //
            PanelApplicationReviewerAssignment result = panelApplication.PanelApplicationReviewerAssignments.FirstOrDefault(x => (x.PanelUserAssignmentId == panelUserAssignmentId));

            return result;
        }
        /// <summary>
        /// Gets the panel applications for reviewer.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        public IEnumerable<PanelApplication> GetPanelApplicationsForScoring(int sessionPanelId)
        {
            var pas = GetEager(x => x.SessionPanelId == sessionPanelId, null,
                z1 => z1.Application.ApplicationPersonnels,
                z1 => z1.PanelApplicationReviewerAssignments,
                z1 => z1.ApplicationStages.Select(z2 => z2.ApplicationStageSteps.Select(z3 => z3.PanelStageStep.PanelStage.ReviewStage)),
                z1 => z1.UserApplicationComments,
                z1 => z1.ApplicationReviewStatus.Select(z2 => z2.ReviewStatu),
                z1 => z1.Application.ProgramMechanism.ClientAwardType);
            return pas;
        }
        /// <summary>
        /// Gets the panel applications for pre assigned.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        public IEnumerable<PanelApplication> GetPanelApplicationsForPreAssigned(int sessionPanelId)
        {
            var pas = GetEager(x => x.SessionPanelId == sessionPanelId, null,
                z1 => z1.Application.ApplicationPersonnels,
                z1 => z1.PanelApplicationReviewerExpertises,
                z1 => z1.Application.ProgramMechanism.ClientAwardType);
            return pas;
        }
        /// <summary>
        /// Gets the panel applications for post assigned.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        public IEnumerable<PanelApplication> GetPanelApplicationsForPostAssigned(int sessionPanelId, List<int> assignedPanelApplicationIds)
        {
            var pas = GetEager(x => x.SessionPanelId == sessionPanelId, null,
                z1 => z1.Application.ApplicationPersonnels,
                z1 => z1.PanelApplicationReviewerExpertises,
                z1 => z1.Application.ProgramMechanism.ClientAwardType,
                z1 => z1.ApplicationStages.Select(z2 => z2.ApplicationStageSteps.Select(z3 => z3.PanelStageStep.PanelStage.ReviewStage)),
                z1 => z1.PanelApplicationReviewerAssignments,
                z1 => z1.ApplicationStages.Select(z2 => z2.ApplicationWorkflows),
                z1 => z1.ApplicationReviewStatus.Select(z2 => z2.ReviewStatu));
            if (assignedPanelApplicationIds.Count > 0)
            {
                pas = pas.Where(x => assignedPanelApplicationIds.Contains(x.PanelApplicationId)).ToList();
            }
            return pas;
        }
        /// <summary>
        /// Gets the panel applications for chairs.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="assignedPanelApplicationIds">The assigned panel application ids.</param>
        /// <returns></returns>
        public IEnumerable<PanelApplication> GetPanelApplicationsForChairs(int sessionPanelId, List<int> assignedPanelApplicationIds)
        {
            var pas = GetEager(x => x.SessionPanelId == sessionPanelId, null,
                z1 => z1.Application.ApplicationPersonnels,
                z1 => z1.PanelApplicationReviewerExpertises,
                z1 => z1.Application.ProgramMechanism.ClientAwardType,
                z1 => z1.ApplicationStages);
            if (assignedPanelApplicationIds.Count > 0)
            {
                pas = pas.Where(x => assignedPanelApplicationIds.Contains(x.PanelApplicationId)).ToList();
            }
            return pas;
        }
        /// <summary>
        /// Determines whether the specified session panel identifier is released.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified session panel identifier is released; otherwise, <c>false</c>.
        /// </returns>
        public bool IsReleased(int sessionPanelId)
        {
            var pas = GetEager(x => x.SessionPanelId == sessionPanelId, null,
                z => z.ApplicationStages);
            return pas.FirstOrDefault(pa => pa.IsReleased()) != null;
        }
        /// <summary>
        /// Gets the panel application entity including panel context, workflows, steps, score and reviewer information
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        ///<returns>Panel application entity with additional data eager loaded</returns>
        public PanelApplication GetByIDWithPanelCritiqueInfo(int panelApplicationId)
        {
            var panApp = GetEager(x => x.PanelApplicationId == panelApplicationId, null,
                x => x.SessionPanel.PanelStages.Select(y => y.PanelStageSteps),
                x => x.ApplicationStages.Select(y => y.ApplicationWorkflows.Select(y2 => y2.ApplicationWorkflowSteps.Select(y3 => y3.ApplicationWorkflowStepElements.Select(y4 => y4.ApplicationWorkflowStepElementContents)))),
                x => x.ApplicationReviewStatus.Select(y => y.ReviewStatu),
                x => x.PanelApplicationReviewerAssignments.Select(y => y.PanelUserAssignment.User.UserInfoes),
                x => x.PanelApplicationReviewerAssignments.Select(y => y.PanelUserAssignment.ClientParticipantType),
                x => x.PanelApplicationReviewerAssignments.Select(y => y.ClientAssignmentType.AssignmentType),
                x => x.ApplicationStages.Select(y => y.ApplicationWorkflows.Select(y2 => y2.ApplicationWorkflowSteps.Select(y3 => y3.ApplicationWorkflowStepElements.Select(y4 => y4.ApplicationTemplateElement.MechanismTemplateElement.ClientElement)))),
                x => x.ApplicationStages.Select(y => y.ApplicationWorkflows.Select(y2 => y2.ApplicationWorkflowSteps.Select(y3 => y3.ApplicationWorkflowStepElements.Select(y4 => y4.ClientScoringScale.ClientScoringScaleAdjectivals)))),
                x => x.ApplicationStages.Select(y => y.ApplicationWorkflows.Select(y2 => y2.PanelUserAssignment)));
            return panApp.FirstOrDefault();
        }

        /// <summary>
        /// Gets the by panel application by identifier with score scale and legend information.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns>Panel application entity with legend information eager loaded</returns>
        public PanelApplication GetByIDWithScoreLegend(int panelApplicationId)
        {
            var panApp = GetEager(x => x.PanelApplicationId == panelApplicationId, null,
                x => x.Application.ProgramMechanism.MechanismTemplates.Select(y => y.MechanismTemplateElements.Select(y2 => y2.MechanismTemplateElementScorings.Select(y3 => y3.ClientScoringScale.ClientScoringScaleLegend.ClientScoringScaleLegendItems))));
            return panApp.FirstOrDefault();
        }

        /// <summary>
        /// Gets the with assignments.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns></returns>
        public PanelApplication GetWithAssignments(int panelApplicationId)
        {
            var pa = context.PanelApplications
                .Include(z1 => z1.SessionPanel.PanelUserAssignments)
                .Include(z1 => z1.PanelApplicationReviewerAssignments.Select(z2 => z2.ClientAssignmentType))
                .Include(z1 => z1.ApplicationStages.Select(z2 => z2.ApplicationWorkflows.Select(z3 => z3.ApplicationWorkflowSteps)))
                .Include(z1 => z1.ApplicationReviewStatus)
                .FirstOrDefault(x => x.PanelApplicationId == panelApplicationId);
            return pa;
        }
        /// <summary>
        /// Gets the panel applications.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        public IEnumerable<PanelApplication> GetPanelApplications(int programYearId, int receiptCycle)
        {
            var os = context.PanelApplications
                .Where(x => x.Application.ProgramMechanism.ProgramYearId == programYearId && x.Application.ProgramMechanism.ReceiptCycle == receiptCycle);
            return os;
        }
        /// <summary>
        /// Adds the panel application.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void AddPanelApplication(int sessionPanelId, int applicationId, int userId)
        {
            var o = new PanelApplication();
            o.SessionPanelId = sessionPanelId;
            o.ApplicationId = applicationId;
            Helper.UpdateCreatedFields(o, userId);
            Helper.UpdateModifiedFields(o, userId);
            context.PanelApplications.Add(o);
        }
        /// <summary>
        /// Gets the peer review data.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public List<PeerReviewResultModel> GetPeerReviewData(int clientId)
        {
            context.Database.CommandTimeout = ExtendedCommandTimeout;
            var os = RepositoryHelpers.GetPeerReviewData(context, clientId).ToList();
            return os;
        }
        #endregion
    }
}
