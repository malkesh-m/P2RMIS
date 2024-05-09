using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using System.Linq;
using System;

namespace Sra.P2rmis.Bll.ModelBuilders.ClientManagement
{
    /// <summary>
    /// Model builder to construct one model for the client program.
    /// </summary>
    internal class ClientProgramModelBuilder : ContainerModelBuilderBase
    {
        #region Constants
        public const int SsWebBasedModeId = 1;
        #endregion
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="clientProgramId">Client program entity identifier</param>
        public ClientProgramModelBuilder(IUnitOfWork unitOfWork, int clientProgramId)
            : base(unitOfWork)
        {
            this.ClientProgramId = clientProgramId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Client program entity identifier.
        /// </summary>
        protected int ClientProgramId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<ClientProgram> SearchResults { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance is summary statement web based.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is summary statement web based; otherwise, <c>false</c>.
        /// </value>
        internal bool IsSummaryStatementWebBased { get; private set; }
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
            this.SearchResults = UnitOfWork.ClientProgramRepository.Select()
                    .Where(x => x.ClientProgramId == this.ClientProgramId);
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        protected virtual void MakeModels()
        {
            int modeId = SearchResults.Select(x => x.Client.SummaryStatementModeId).FirstOrDefault();
            this.IsSummaryStatementWebBased = (modeId == SsWebBasedModeId);
        }
        #endregion
    }
}
