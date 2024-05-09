using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    /// <summary>
    /// Create an ApplicationReviewStatu entry for a Priority change
    /// </summary>
    public class PriorityChangeServiceAction : ServiceAction<ApplicationReviewStatu>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public PriorityChangeServiceAction() { }
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        /// <param name="applicationId">Application entity identifier</param>
        /// <param name="reviewStatusId">ReviewStatus entity identifier</param>
        /// <param name="panelApplicaitonId">PanelApplicaiton entity identifier</param>
        public void Populate(int applicationId, int reviewStatusId, int panelApplicaitonId)
        {
            this.ApplicationId = applicationId;
            this.ReviewStatusId = reviewStatusId;
            this.PanelApplicationId = panelApplicaitonId;
        }
        /// <summary>
        /// Initialize the action.  Parameters supply the necessary environmental information
        /// to interact with the entity framework.
        /// </summary>
        /// <param name="unitOfWork">Unit of work object</param>
        /// <param name="repository">Entity repository</param>
        /// <param name="saveThis">Flag indicating if the framework should be saved after the operation</param>
        /// <param name="userId">Entity identifier of the user performing the operation</param>
        /// <remarks>
        /// We use this version of InitializeAction (intended for WebModels) because to indicate an ApplicationReviewStatu needs to be added.
        /// </remarks>
        public new void InitializeAction(IUnitOfWork unitOfWork, IGenericRepository<ApplicationReviewStatu> repository, bool saveThis, int userId)
        {
            base.InitializeAction(unitOfWork, repository, saveThis, userId);
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ReviewStatus entity identifier
        /// </summary>
        public int ReviewStatusId { get; private set; }
        /// <summary>
        /// Application entity identifier.
        /// </summary>
        /// <remarks>
        /// This will eventually be OBE
        /// </remarks>
        public int ApplicationId { get; private set; }
        /// <summary>
        /// PanelApplication entity identifier.
        /// </summary>
        public int PanelApplicationId { get; private set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the UserApplicationComment entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">UserApplicationComment entity</param>
        protected override void Populate(ApplicationReviewStatu entity)
        {
            entity.Populate(this.ApplicationId, this.ReviewStatusId, this.PanelApplicationId);
        }
        /// <summary>
        /// Indicates if the UserApplicationComment has data.
        /// </summary>
        protected override bool HasData
        {
            get
            {
                //
                // By definition we are expecting that it will have data.  
                //
                return true;
            }
        }
        #endregion
        #region Optional Overrides
        /// <summary>
        /// Overridden IsAdd.  Indicates when a new ReviewStatu entity should be
        /// created.
        /// </summary>
        public override bool IsAdd
        {
            get
            {
                return (EntityId() == 0);
            }
        }
        /// <summary>
        /// Overridden IsDelete.  Indicates if the data represents a delete.
        /// </summary>
        protected override bool IsDelete
        {
            get { return !IsAdd; }
        }
        #endregion
    }
}
