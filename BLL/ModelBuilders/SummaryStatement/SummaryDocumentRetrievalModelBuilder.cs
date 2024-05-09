
using System.Linq;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Files;

namespace Sra.P2rmis.Bll.ModelBuilders.SummaryStatement
{
    /// <summary>
    /// Class for retreival of an existing summary document
    /// </summary>
    /// <seealso cref="Sra.P2rmis.Bll.ModelBuilders.ContainerModelBuilderBase" />
    internal class SummaryDocumentRetrievalModelBuilder : SummaryStatementModelBuilderBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryDocumentRetrievalModelBuilder"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        public SummaryDocumentRetrievalModelBuilder(IUnitOfWork unitOfWork, int panelApplicationId) : base(unitOfWork)
        {
            PanelApplicationId = panelApplicationId;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the document file data.
        /// </summary>
        /// <value>
        /// The document file data.
        /// </value>
        public ISummaryDocumentFileModel Result { get; private set; }

        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<ApplicationWorkflow> SearchResults { get; set; }
        /// <summary>
        /// Gets or sets the application templates.
        /// </summary>
        /// <value>
        /// The application templates.
        /// </value>
        protected IQueryable<ApplicationTemplate> ApplicationTemplates { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Result.  
        /// </summary>
        public override void BuildContainer()
        {
            GetTemplatePath();
            SearchResults = GetApplicationWorkflow();
            MakeModel();
        }
        /// <summary>
        /// Makes the model.
        /// </summary>
        private void MakeModel()
        {
            Result = SearchResults.Select(x => new SummaryDocumentFileModel()
            {
                LogNumber = x.ApplicationStage.PanelApplication.Application.LogNumber,
                ProgramAbbreviation = x.ApplicationStage.PanelApplication.Application.ProgramMechanism.ProgramYear.ClientProgram.ProgramAbbreviation,
                FiscalYear = x.ApplicationStage.PanelApplication.Application.ProgramMechanism.ProgramYear.Year,
                ReceiptCycle = x.ApplicationStage.PanelApplication.Application.ProgramMechanism.ReceiptCycle ?? 0,
                FileContent = x.ApplicationWorkflowSummaryStatements.FirstOrDefault().DocumentFile
            }).FirstOrDefault();
        }        
        #endregion
    }
}
