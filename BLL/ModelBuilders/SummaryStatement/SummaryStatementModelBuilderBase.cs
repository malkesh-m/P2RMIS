using Sra.P2rmis.Dal;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;

namespace Sra.P2rmis.Bll.ModelBuilders.SummaryStatement
{
    internal class SummaryStatementModelBuilderBase : ContainerModelBuilderBase
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
        public SummaryStatementModelBuilderBase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        #endregion
        #region Attributes        
        /// <summary>
        /// Gets or sets the application workflow identifier.
        /// </summary>
        /// <value>
        /// The application workflow identifier.
        /// </value>
        internal int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// Panel application entity identifier.
        /// </summary>
        protected int PanelApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the template full path.
        /// </summary>
        /// <value>
        /// The template full path.
        /// </value>
        public string TemplateFullPath { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
        }
        /// <summary>
        /// Gets the application workflow.
        /// </summary>
        /// <returns></returns>
        internal IQueryable<ApplicationWorkflow> GetApplicationWorkflow()
        {
            return UnitOfWork.PanelApplicationRepository
                .Select()
                .Where(x => x.PanelApplicationId == PanelApplicationId)
                .GetSummaryWorkflow();
        }
        /// <summary>
        /// Gets the application templates.
        /// </summary>
        /// <returns></returns>
        internal IQueryable<ApplicationTemplate> GetApplicationTemplates()
        {
            return UnitOfWork.PanelApplicationRepository
                .Select()
                .Where(x => x.PanelApplicationId == PanelApplicationId)
                .GetApplicationTemplates();
        }
        /// <summary>
        /// Determines whether [has application template].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has application template]; otherwise, <c>false</c>.
        /// </returns>
        internal bool HasApplicationTemplate()
        {
            return GetApplicationTemplates().Count() > 0;
        }
        /// <summary>
        /// Determines whether [has document template].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has document template]; otherwise, <c>false</c>.
        /// </returns>
        internal bool HasDocumentTemplate()
        {
            return TemplateFullPath != null;
        }
        /// <summary>
        /// Gets the template path.
        /// </summary>
        internal void GetTemplatePath()
        {
            var panelApplicationEntity =
                UnitOfWork.PanelApplicationRepository.Select()
                    .Where(x => x.PanelApplicationId == PanelApplicationId);
            TemplateFullPath =
                ConfigManager.GetTemplateFullPath(panelApplicationEntity.SelectMany(x => x.Application.ProgramMechanism.ProgramMechanismSummaryStatements)
                    .Where(
                        x =>
                            x.ReviewStatusId ==
                            panelApplicationEntity.Select(
                                y =>
                                    y.ApplicationReviewStatus.FirstOrDefault(
                                        z => z.ReviewStatu.ReviewStatusTypeId == ReviewStatusType.Review).ReviewStatusId)
                                .FirstOrDefault())
                    .Select(x => x.ClientSummaryTemplate.TemplateFileName).FirstOrDefault());
        }
        #endregion
    }
}
