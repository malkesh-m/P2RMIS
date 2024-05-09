using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices.Models;
using Sra.P2rmis.CrossCuttingServices.DocumentServices;
using System.IO;

namespace Sra.P2rmis.Bll.ModelBuilders.SummaryStatement
{
    internal class SummaryDocumentModelBuilder : SummaryStatementModelBuilderBase
    {
        #region Constants
        #endregion
        #region Construction & Setup        
        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryDocumentModelBuilder"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="templateLocation">The template location.</param>
        public SummaryDocumentModelBuilder(IUnitOfWork unitOfWork, int panelApplicationId,
            string storedProcedureName, string templateLocation)
            : base(unitOfWork)
        {
            this.PanelApplicationId = panelApplicationId;
            this.StoredProcedureName = storedProcedureName;
            this.TemplateLocation = templateLocation;
        }
        #endregion
        #region Attributes  
        /// <summary>
        /// Gets or sets the name of the stored procedure.
        /// </summary>
        /// <value>
        /// The name of the stored procedure.
        /// </value>
        protected string StoredProcedureName { get; set; }
        /// <summary>
        /// Gets or sets the template location.
        /// </summary>
        /// <value>
        /// The template location.
        /// </value>
        protected string TemplateLocation { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected ISummaryDocumentModel SearchResults { get; set; }
        /// <summary>
        /// Gets the document file data.
        /// </summary>
        /// <value>
        /// The document file data.
        /// </value>
        internal byte[] DocumentFileData { get; private set; }
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            Search();
            MakeModels();
        }
        /// <summary>
        /// Does all the heavy lifting for retrieving the clientProgram data.
        /// </summary>
        protected virtual void Search()
        {
            var panelApplication = UnitOfWork.PanelApplicationRepository.GetByID(PanelApplicationId);
            ApplicationWorkflowId = panelApplication.GetSummaryWorkflow().ApplicationWorkflowId;
            SearchResults = UnitOfWork.SummaryManagementRepository.GetSummaryDocumentData(ApplicationWorkflowId, PanelApplicationId, StoredProcedureName);
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        protected virtual void MakeModels()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                WordServices.CreateReport(TemplateLocation, SearchResults, stream);
                DocumentFileData = stream.ToArray();
            }
        }
        #endregion
    }
}
