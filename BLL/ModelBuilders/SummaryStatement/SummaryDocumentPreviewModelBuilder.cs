using Sra.P2rmis.Dal;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices.Models;
using Sra.P2rmis.CrossCuttingServices.DocumentServices;
using System.IO;
using Sra.P2rmis.WebModels.Files;

namespace Sra.P2rmis.Bll.ModelBuilders.SummaryStatement
{
    internal class SummaryDocumentPreviewModelBuilder : SummaryStatementModelBuilderBase
    {
        #region Constructor
        public SummaryDocumentPreviewModelBuilder(IUnitOfWork unitOfWork, int panelApplicationId)
            : base(unitOfWork)
        {
            this.PanelApplicationId = panelApplicationId;
        }
        #endregion
        #region Properties
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
        /// <summary>
        /// Gets the document file data.
        /// </summary>
        /// <value>
        /// The document file data.
        /// </value>
        public ISummaryDocumentFileModel Result { get; private set; }
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            GetTemplatePath();
            Search();
            GenerateFile();
            MakeModel();
        }
        /// <summary>
        /// Determines whether [has required application data].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has required application data]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasRequiredApplicationData()
        {
            Search();
            return SearchResults.App != null;
        }

        /// <summary>
        /// Sets the search results for the model.
        /// </summary>
        private void Search()
        {
            SearchResults = UnitOfWork.SummaryManagementRepository.GetSummaryPreviewDocumentData(PanelApplicationId);
        }

        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        private void GenerateFile()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                WordServices.CreateReport(TemplateFullPath, SearchResults, stream);
                DocumentFileData = stream.ToArray();
            }
        }
        
        /// <summary>
        /// Makes the return model.
        /// </summary>
        private void MakeModel()
        {
            Result = UnitOfWork.PanelApplicationRepository.Select().Where(x => x.PanelApplicationId == PanelApplicationId).Select(x => new SummaryDocumentFileModel()
            {
                LogNumber = x.Application.LogNumber,
                FileContent = DocumentFileData
            }).FirstOrDefault();
        }
        #endregion

    }
}