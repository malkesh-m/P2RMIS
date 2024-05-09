using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Service action class to add ApplicationStageStepDiscussionComment entities.
    /// </summary>
    internal class ApplicationStageStepDiscussionCommentServiceAction : ServiceAction<ApplicationStageStepDiscussionComment>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationStageStepDiscussionCommentServiceAction() { }
        /// <summary>
        /// Initialize the action.  Parameters supply the necessary environmental information
        /// to interact with the entity framework.
        /// </summary>
        /// <param name="applicationStageStepDiscussionEntityId">ApplicationStageStepDiscussion entity identifier</param>
        public void Initialize(int applicationStageStepDiscussionEntityId, ApplicationStageStepDiscussion applicationStageStepDiscussionEntity, string commentText)
        {
            this.ApplicationStageStepDiscussionEntityId = applicationStageStepDiscussionEntityId;
            this.ApplicationStageStepDiscussionEntity = applicationStageStepDiscussionEntity;
            this.CommentText = commentText;
            //
            // MOD should only do adds (at least until things change).  So in the event that an entity identifier 
            // is passed in with the base's InitializeAction it is zeroed out to force the add.
            //
            this.EntityIdentifier = 0;
        }
        #endregion
        #region Properties
        /// <summary>
        /// ApplicationStageStepDiscussion entity identifier.  The container for the discussion.
        /// </summary>
        private int ApplicationStageStepDiscussionEntityId { get; set; }
        /// <summary>
        /// ApplicationStageStepDiscussion entity  The container for the discussion.  (We need to deal
        /// with the case where the discussion & the comment are created in the same service action.
        /// </summary>
        private ApplicationStageStepDiscussion ApplicationStageStepDiscussionEntity { get; set; }
        /// <summary>
        /// The MOD comment
        /// </summary>
        private string CommentText { get; set; }
        /// <summary>
        /// The newly created ApplicationStageStepDiscussionComment entity
        /// 
        /// </summary>
        public ApplicationStageStepDiscussionComment ApplicationStageStepDiscussionCommentEntity { get; private set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the ApplicationStageStepDiscussionComment entity with information from the ServiceAction.
        /// </summary>
        /// <param name="entity">ApplicationStageStepDiscussionComment entity</param>
        protected override void Populate(ApplicationStageStepDiscussionComment entity)
        {
            entity.Populate(ApplicationStageStepDiscussionEntityId, CommentText);
        }
        /// <summary>
        /// Indicates if the service action has data.
        /// </summary>
        /// <returns>True if the action contains data; false otherwise</returns>
        protected override bool HasData
        {
            get { return (!string.IsNullOrWhiteSpace(CommentText)); }
        }
        /// <summary>
        /// Optional pre-add processing logic.
        /// The case exists where we will be adding both a ApplicationStageStepDiscussion container & an ApplicationStageStepDiscussionComment.
        /// In which case we will not have an entity identifier for the ApplicationStageStepDiscussion since a save has not been done yet.  This
        /// handles the case.
        /// </summary>
        /// <param name="entity">ApplicationStageStepDiscussionComment entity that was created</param>
        protected override void PostAdd(ApplicationStageStepDiscussionComment entity)
        {
            this.ApplicationStageStepDiscussionCommentEntity = entity;
            //
            // if the ApplicationStageStepDiscussion entity is created at the same time that the ApplicationStageStepDiscussionComment
            // is then we will not have an entity identifier to set in the Populate method.  In which case we will have to add it to the
            // created entity.
            //
            if (ApplicationStageStepDiscussionEntityId == 0)
            {
                ApplicationStageStepDiscussionEntity.ApplicationStageStepDiscussionComments.Add(entity);
            }
        }
        #endregion
    }
}
