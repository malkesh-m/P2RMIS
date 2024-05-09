using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Service action class to add ApplicationStageStepDiscussion entities.
    /// </summary>
    internal class ApplicationStageStepDiscussionServiceAction: ServiceAction<ApplicationStageStepDiscussion>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationStageStepDiscussionServiceAction() { }
        /// <summary>
        /// Initialize the action.  Parameters supply the necessary environmental information
        /// to interact with the entity framework.
        /// </summary>
        /// <param name="applicationStageStepEntityId">ApplicationStageStep entity identifier</param>
        public void Initialize(int applicationStageStepEntityId)
        {
            this.ApplicationStageStepEntityId = applicationStageStepEntityId;
        }
        #endregion
        #region Properties
        /// <summary>
        /// ApplicationStageStep entity identifier.  This entity will hold any created ApplicationStageStepDiscussion objects.
        /// </summary>
        private int ApplicationStageStepEntityId { get; set; }
        /// <summary>
        /// The ApplicationStageStepDiscussion entity created
        /// </summary>
        internal ApplicationStageStepDiscussion TheCreatedEntity { get; private set; }
        #endregion
        #region Overrides
        /// <summary>
        /// Populate the ApplicationStageStepDiscussion entity with information from the ServiceAction.
        /// </summary>
        /// <param name="entity">ApplicationStageStepDiscussion entity</param>
        protected override void Populate(ApplicationStageStepDiscussion entity)
        {
            entity.Populate(ApplicationStageStepEntityId);
        }
        /// <summary>
        /// Indicates if the UserApplicationComment has data.
        /// </summary>
        protected override bool HasData
        {
            get
            {
                //
                // This is a bit backward.  Since we are a container we add stuff to it but 
                // don't make any modifications.  So HasData really indicates if we are to add.
                //
                return (this.EntityIdentifier == 0);
            }
        }
        /// <summary>
        /// Optional post add processing.  Add any additional processing necessary after the entity object
        /// is added to the framework. For MOD we need to make the newly created entity visible to the next
        /// ServiceAction.
        /// </summary>
        protected override void PostAdd(ApplicationStageStepDiscussion entity)
        {
            this.TheCreatedEntity = entity;
        }
        #endregion
    }
}
