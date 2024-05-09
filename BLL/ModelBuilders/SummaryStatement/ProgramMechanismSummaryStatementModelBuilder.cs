using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.SummaryStatement
{
    internal class ProgramMechanismSummaryStatementModelBuilder : ContainerModelBuilderBase
    {
        #region Constants
        #endregion
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="panelApplicationId">Panel application entity identifier</param>
        /// <param name="programMechanismId">Program mechanism entity identifier</param>
        public ProgramMechanismSummaryStatementModelBuilder(IUnitOfWork unitOfWork, int panelApplicationId, 
            int programMechanismId)
            : base(unitOfWork)
        {
            this.PanelApplicationId = panelApplicationId;
            this.ProgramMechanismId = programMechanismId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Panel application entity identifier.
        /// </summary>
        protected int PanelApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the program mechanism identifier.
        /// </summary>
        /// <value>
        /// The program mechanism identifier.
        /// </value>
        protected int ProgramMechanismId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<ProgramMechanismSummaryStatement> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IProgramMechanismSummaryStatementModel> Results { get; private set; } = new Container<IProgramMechanismSummaryStatementModel>();
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
            var mechanism = UnitOfWork.ProgramMechanismRepository.Select()
                    .Where(x => x.ProgramMechanismId == ProgramMechanismId);
            var reviewStatus = UnitOfWork.ApplicationReviewStatusRepository.Select()
                    .Where(x => x.PanelApplicationId == PanelApplicationId &&
                    x.ReviewStatu.ReviewStatusTypeId == ReviewStatusType.Review);
            this.SearchResults = mechanism.Select(x => x.ProgramMechanismSummaryStatements.FirstOrDefault(y =>
                    y.ReviewStatusId == reviewStatus.FirstOrDefault().ReviewStatusId));
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        protected virtual void MakeModels()
        {
            IQueryable<IProgramMechanismSummaryStatementModel> modelResults = SearchResults.Select(x => new ProgramMechanismSummaryStatementModel
            {
                TemplateLocation = x.ClientSummaryTemplate.TemplateFileName,
                StoredProcedureName = x.ClientSummaryTemplate.StoredProcedureName
            });
            this.Results.ModelList = modelResults.ToList();
        }
        #endregion
    }
}
