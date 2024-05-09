using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.SummaryStatements.Blocks
{
    public class ApplicationWorkflowStepElementContentBlock : CrudBlock<ApplicationWorkflowStepElementContent>, ICrudBlock
    {
        public ApplicationWorkflowStepElementContentBlock(int applicationWorkflowStepElementid)
        {
            ApplicationWorkflowStepElementid = applicationWorkflowStepElementid;
        }
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureAdd()
        {
            IsAdd = true;
        }
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureModify()
        {
            IsModify = true;
        }
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureDelete()
        {
            IsDelete = true;
        }
        /// <summary>
        /// Gets the appliation workflow identifier.
        /// </summary>
        /// <value>
        /// The appliation workflow identifier.
        /// </value>
        internal int ApplicationWorkflowStepElementid { get; private set; }
        /// <summary>
        /// Gets the content text.
        /// </summary>
        /// <value>
        /// The content text.
        /// </value>
        internal string ContentText { get; private set; }
        /// <summary>
        /// Gets the content text no markup.
        /// </summary>
        /// <value>
        /// The content text no markup.
        /// </value>
        internal string ContentTextNoMarkup { get; private set; }
        /// <summary>
        /// Sets the content.
        /// </summary>
        /// <param name="contentText">The content text.</param>
        /// <param name="contentTextNoMarkup">The content text no markup.</param>
        internal void SetContent(string contentText, string contentTextNoMarkup)
        {
            ContentText = contentText;
            ContentTextNoMarkup = contentTextNoMarkup;
        }
        /// <summary>
        /// Populates the ApplicationWorkflowStepElementContent entity.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="entity">The entity.</param>
        internal override void Populate(int userId, ApplicationWorkflowStepElementContent entity)
        {
            if (!IsDelete)
            {
                entity.ApplicationWorkflowStepElementId = this.ApplicationWorkflowStepElementid;
                entity.ContentText = this.ContentText;
                entity.ContentTextNoMarkup = this.ContentTextNoMarkup;
            }
        }
    }
}
