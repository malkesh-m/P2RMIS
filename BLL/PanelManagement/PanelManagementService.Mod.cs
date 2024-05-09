using Sra.P2rmis.Bll.ApplicationScoring;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.PanelManagement;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.PanelManagement
{
    /// <summary>
    /// PanelManagementService services supporting MOD functionality.
    /// </summary>
    public partial class PanelManagementService
    {

        /// <summary>
        /// Determines if an Online discussion can be started.
        /// </summary>
        /// <param name="applicationStageStepEntityId">ApplicationStageStep entity identifier</param>
        /// <returns>True if the on-line discussion can be start; false otherwise.</returns>
        internal bool CanOnLineDiscussionBeStarted(int applicationStageStepEntityId)
        {
            ApplicationStageStep applicationStageStepEntity = UnitOfWork.ApplicationStageStepRepository.GetByID(applicationStageStepEntityId);

            return ((applicationStageStepEntity.IsDiscussionNotStarted()) && (applicationStageStepEntity.AreAllReviewersCritiquesSubmitted()));
        }
        /// <summary>
        /// Saves a MOD comment to a discussion (container).  If a discussion container is not instantiated currently 
        /// an ApplicationStageStepDiscussion container is created to hold the comment.
        /// </summary>
        /// <param name="applicationStageStepEntityId">ApplicationStageStep entity identifier</param>
        /// <param name="applicationStageStepDiscussionEntityId">ApplicationStageStepDiscussion entity identifier</param>
        /// <param name="comment">MOD comment</param>
        /// <param name="userId">User entity identifier of user entering comment</param>
        /// <returns>ApplicationStageStepDiscussionComment entity identifier of created comment entity.</returns>
        public CommentTypeModel SaveModComment(int applicationStageStepEntityId, int? applicationStageStepDiscussionEntityId, string comment, int userId, bool isNew)
        {
            string name = FullName(nameof(PanelManagementService), nameof(SaveModComment));
            ValidateInt(applicationStageStepEntityId, name, nameof(applicationStageStepEntityId));
            ValidateString(comment, name, nameof(comment));
            ValidateInt(userId, name, nameof(userId));

            //
            // Set up data for MOD if first comment and MOD is available
            //
            if (CanOnLineDiscussionBeStarted(applicationStageStepEntityId))
            {
                StartMod(applicationStageStepEntityId, userId);
                isNew = true;
            }
                //
                // Add a discussion if one does not exist.
                //
                ApplicationStageStepDiscussion applicationStageStepDiscussionEntity = (!applicationStageStepDiscussionEntityId.HasValue) ?
                                        CreateApplicationStageStepDiscussionContainer(applicationStageStepEntityId, userId) :
                                        UnitOfWork.ApplicationStageStepDiscussionRepository.GetByID(applicationStageStepDiscussionEntityId.Value);

            return CreateApplicationStageStepDiscussionComment(applicationStageStepDiscussionEntity, comment, userId, isNew);
        }
        /// <summary>
        /// Creates a container (ApplicationStageStepDiscussion entity) to hold MOD
        /// comments.
        /// </summary>
        /// <param name="applicationStageStepEntityId">ApplicationStageStep entity identifier</param>
        /// <param name="userId">User entering comment</param>
        internal virtual ApplicationStageStepDiscussion CreateApplicationStageStepDiscussionContainer(int applicationStageStepEntityId, int userId)
        {
            ApplicationStageStepDiscussionServiceAction serviceAction = new ApplicationStageStepDiscussionServiceAction();
            serviceAction.InitializeAction(UnitOfWork, UnitOfWork.ApplicationStageStepDiscussionRepository, false, 0, userId);
            serviceAction.Initialize(applicationStageStepEntityId);
            serviceAction.Execute();
            return serviceAction.TheCreatedEntity;
        }
        /// <summary>
        /// Add a MOD comment to a ApplicationStageStepDiscussion container
        /// </summary>
        /// <param name="applicationStageStepDiscussionEntity">ApplicationStageStepDiscussion entity</param>
        /// <param name="comment">MOD comment</param>
        /// <param name="userId">User entity identifier of user entering comment</param>
        /// <remarks>ApplicationStageStepDiscussionComment entity identifier of created comment</remarks>
        internal virtual CommentTypeModel CreateApplicationStageStepDiscussionComment(ApplicationStageStepDiscussion applicationStageStepDiscussionEntity, string comment, int userId, bool isNew)
        {
            CommentTypeModel commentType = new CommentTypeModel();
            var serviceAction = new ApplicationStageStepDiscussionCommentServiceAction();
            serviceAction.InitializeAction(UnitOfWork, UnitOfWork.ApplicationStageStepDiscussionCommentRepository, true, 0, userId);
            serviceAction.Initialize(applicationStageStepDiscussionEntity.ApplicationStageStepDiscussionId, applicationStageStepDiscussionEntity, comment);
            serviceAction.Execute();
            commentType.CommentId = serviceAction.ApplicationStageStepDiscussionCommentEntity.ApplicationStageStepDiscussionCommentId;
            commentType.CommentType = isNew;
            return commentType;
        }
        /// <summary>
        /// Start a MOD session.
        /// </summary>
        /// <param name="applicationStageStepId">ApplicationStageStep entity identifier</param>
        /// <param name="userId">User entity identifier of user entering comment</param>
        internal void StartMod(int applicationStageStepId, int userId)
        {
            string name = FullName(nameof(PanelManagementService), nameof(StartMod));
            ValidateInt(applicationStageStepId, name, nameof(applicationStageStepId));
            ValidateInt(userId, name, nameof(userId));
            //
            // Retrieve the ApplicationStageStep since everything starts from there
            //
            ApplicationStageStep a = UnitOfWork.ApplicationStageStepRepository.GetByID(applicationStageStepId);
            List<ApplicationWorkflow> list = a.RetrieveStepWorkflows().ToList();
            //
            // We reopen each workflow since they were closed when the previous step was completed.
            //
            list.ForEach(x => x.ReOpen(userId));
            //
            // And activate each workflow step for the final workflow step.
            //
            list.SelectMany(x => x.ApplicationWorkflowSteps).
            //
            // Filter by the workflow StepType (we only want the final ones & make a list of them 
            Where(x => x.StepTypeId == StepType.Indexes.Final).ToList().
            //
            // Then reactivate each final workflow step.
            //
            ForEach(x => x.ReActivate(userId));
            //
            // Note that we do not save these updates.  They will be saved when the comment is saved because we 
            // want everything to be saved as a single transaction.
            //
        }
    }
}
