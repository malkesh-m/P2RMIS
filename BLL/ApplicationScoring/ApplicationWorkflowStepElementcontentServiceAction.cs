using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete ApplicationWorkflowStepElementContent represented
    /// by WebModel CritiqueContent
    /// </summary>
    public class ApplicationWorkflowStepElementContentServiceAction: ServiceAction<ApplicationWorkflowStepElementContent>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationWorkflowStepElementContentServiceAction()
        {
        }
        public void Populate(string contextText)
        {
            this.ContextText = contextText;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Critique text
        /// </summary>
        public string ContextText { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the ApplicationWorkflowStepElementContent entity with information from the model.
        /// </summary>
        /// <param name="entity">ApplicationWorkflowStepElementContent entity</param>
        protected override void Populate(ApplicationWorkflowStepElementContent entity)
        {
            entity.Populate(ContextText);
        }
        /// <summary>
        /// Indicates if the ApplicationWorkflowStepElementContent has data.
        /// </summary>
        protected override bool HasData
        {
            get
            {
                //
                // By definition we are expecting that it will have data.  Even
                // null or the empty string should be stored.
                //
                return true;
            }
        }
        #endregion
    }
    public class CreateAbstainedApplicationWorkflowStepElementContentServiceAction : ApplicationWorkflowStepElementContentServiceAction
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public CreateAbstainedApplicationWorkflowStepElementContentServiceAction()
        {
        }
        /// <summary>
        /// Parent entity for the newly created ApplicationWorkflowStepElementContent
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">ApplicationWorkflowStepElementContent entity identifier</param>
        public void Populate(int applicationWorkflowStepElementId, string contentText, decimal? score, bool isAbstained)
        {
            this.ApplicationWorkflowStepElementEntityId = applicationWorkflowStepElementId;
            this.ContextText = contentText;
            this.IsAbstained = isAbstained;
            this.Score = score;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Parent ApplicationWorkflowStepElement entity that will contain any ApplicationWorkflowStepElement
        /// created.
        /// </summary>
        protected int ApplicationWorkflowStepElementEntityId { get; set; }
        /// <summary>
        /// Indicates if the critique is abstained.
        /// </summary>
        protected bool IsAbstained { get; set; }
        /// <summary>
        /// Critique score
        /// </summary>
        protected decimal? Score { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the ApplicationWorkflowStepElementContent entity with information from the model.
        /// </summary>
        /// <param name="entity">ApplicationWorkflowStepElementContent entity</param>
        protected override void Populate(ApplicationWorkflowStepElementContent entity)
        {
            entity.Populate(ApplicationWorkflowStepElementEntityId, this.ContextText, this.Score, this.IsAbstained);
        }
        #endregion
    }
}
