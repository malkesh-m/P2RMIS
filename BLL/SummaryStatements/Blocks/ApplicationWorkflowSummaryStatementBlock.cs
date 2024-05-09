using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.SummaryStatements.Blocks
{
    public class ApplicationWorkflowSummaryStatementBlock : CrudBlock<ApplicationWorkflowSummaryStatement>, ICrudBlock
    {
        public ApplicationWorkflowSummaryStatementBlock(int applicationWorkflowid)
        {
            ApplicationWorkflowId = applicationWorkflowid;
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
        /// Gets the application workflow identifier.
        /// </summary>
        /// <value>
        /// The application workflow identifier.
        /// </value>
        internal int ApplicationWorkflowId { get; private set; }
        /// <summary>
        /// Gets the document file.
        /// </summary>
        /// <value>
        /// The document file.
        /// </value>
        internal byte[] DocumentFile { get; private set; }
        /// <summary>
        /// Sets the document file.
        /// </summary>
        /// <param name="documentFile">The document file.</param>
        internal void SetDocumentFile(byte[] documentFile)
        {
            DocumentFile = documentFile;
        }
        /// <summary>
        /// Populates the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="entity">The entity.</param>
        internal override void Populate(int userId, ApplicationWorkflowSummaryStatement entity)
        {
            if (!IsDelete)
            {
                entity.ApplicationWorkflowId = this.ApplicationWorkflowId;
                entity.DocumentFile = this.DocumentFile;
            }
        }
    }
}
