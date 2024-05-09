using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.ApplicationScoring;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Service Action method to Add/Modify ApplicationWorkflowStepElementContent represented
    /// by WebModel ReviewerScores
    /// </summary>
    public class ScoringServiceAction : ServiceModelActionForWebModel<ApplicationWorkflowStepElementContent, ReviewerScores>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public ScoringServiceAction()
        {
            //
            // Normally one would set Index here.  However since the model value is null able
            // we override EntityId() method instead.
            //
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work object</param>
        /// <param name="repository">Entity repository</param>
        /// <param name="saveThis">Flag indicating if the framework should be saved after the operation</param>
        /// <param name="userId">Entity identifier of the user performing the operation</param>
        /// <param name="model">WebModel class object containing data</param>
        public void InitializeAction(IUnitOfWork unitOfWork, IGenericRepository<ApplicationWorkflowStepElementContent> repository, IGenericRepository<ApplicationWorkflowStepElement> applicationWorkflowStepElementRepository, bool saveThis, int userId, IEditable model)
        {
            base.InitializeAction(unitOfWork, repository, saveThis, userId, model);
            this.ApplicationWorkflowStepElementRepository = applicationWorkflowStepElementRepository;
            this.ApplicationWorkflowStepElementEntity = applicationWorkflowStepElementRepository.GetByID(ReviewerScoresModel.ApplicationWorkflowStepElementId);
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Return the Model as a specific model.
        /// </summary>
        protected ReviewerScores ReviewerScoresModel
        {
            get { return Model as ReviewerScores; }
        }
        /// <summary>
        /// Parent ApplicationWorkflowStepElement entity for the score.
        /// </summary>
        protected ApplicationWorkflowStepElement ApplicationWorkflowStepElementEntity { get; set; }
        /// <summary>
        /// Entity object repository
        /// </summary>
        protected IGenericRepository<ApplicationWorkflowStepElement> ApplicationWorkflowStepElementRepository { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the UserSystemRole entity with information from the GeneralInfo model.
        /// </summary>
        /// <param name="entity">ApplicationWorkflowStepElementContent entity</param>
        protected override void Populate(ApplicationWorkflowStepElementContent entity)
        {
            decimal parsedScore = 0;
            decimal.TryParse(ReviewerScoresModel.Score, out parsedScore);

            bool abstain = (ReviewerScoresModel.Score == "A");
            decimal? score = (abstain) ? (decimal?)null : parsedScore;

            entity.Populate(score, abstain);
        }
        /// <summary>
        /// Indicates if the WebModel is an add
        /// </summary>
        public override bool IsAdd
        {
            get
            {
                return !this.ApplicationWorkflowStepElementEntity.HasScore();
            }
        }
        #endregion
        #region Optional Overrides
        /// <summary>
        /// Override how the ApplicationWorkflowStepElementContent entity is determined.
        /// </summary>
        /// <returns>ApplicationWorkflowStepElementContent entity identifier; 0 if no entity exists.</returns>
        protected override int EntityId()
        {
            return this.ApplicationWorkflowStepElementEntity.ApplicationWorkflowStepElementContentId();
        }
        /// <summary>
        /// Indicates if the WebModel has a score to save.
        /// </summary>
        protected override bool HasData
        {
            get { return ReviewerScoresModel.HasData(); }
        }
        /// <summary>
        /// Override of the default PostAdd.  The entity was created but not linked to the 
        /// parent ApplicationWorkflowStepElement.
        /// </summary>
        protected override void PostAdd(ApplicationWorkflowStepElementContent entity)
        {
            //
            // Add the element to the parent ApplicationWorkflowStepElement entity & 
            // mark the element to "update"
            //
            Helper.UpdateModifiedFields(this.ApplicationWorkflowStepElementEntity, this.UserId);
            this.ApplicationWorkflowStepElementEntity.ApplicationWorkflowStepElementContents.Add(entity);
            this.ApplicationWorkflowStepElementRepository.Update(this.ApplicationWorkflowStepElementEntity);
        }
        #endregion
    }
}
