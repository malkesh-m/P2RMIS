using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Lists;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct the list of a client's programs
    /// </summary>
    internal class AssignProgramFiscalYearModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="clientId">Client entity identifier</param>
        public AssignProgramFiscalYearModelBuilder(IUnitOfWork unitOfWork, int clientId)
            : base(unitOfWork)
        {
            this.ClientId = clientId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Client entity identifier
        /// </summary>
        protected int ClientId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<ProgramYear> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IGenericActiveProgramListEntry<int, string>> Results { get; private set; } = new Container<IGenericActiveProgramListEntry<int, string>>();
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
        /// Does all the heavy lifting for retrieving the Award/Mechanism Setup grid data.
        /// </summary>
        internal virtual void Search()
        {
            this.SearchResults = UnitOfWork.ProgramYearRepository.Select()
                                //
                                // we only want the ProgramYears for this client
                                //
                                .Where(x => x.ClientProgram.ClientId == this.ClientId);
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        internal virtual void MakeModels()
        {
            //
            // Now that we have the records from the database that will populate the grid
            // we can model the data & return it.
            //
            IQueryable<IGenericActiveProgramListEntry<int, string>> modelResults = SearchResults.Select(x => new ActiveProgramListEntry<int, string>
            {
                //
                // Pull out the data for the model
                //
                Value = x.Year,
                IsActive = !x.DateClosed.HasValue,
                ProgramAbbreviation = x.ClientProgram.ProgramAbbreviation,
                //
                // and now pull out the identifiers
                //
                Index = x.ClientProgram.ClientProgramId,
                ProgramYearId = x.ProgramYearId
            });
            //
            // And finally we order it 
            //    - by year (in descending order 2016, 2015 ...)
            //    - then by the ProgramAbbreviation (A, B, C ....)
            // and then execute.
            //
            this.Results.ModelList = modelResults
                                    .OrderByDescending(x => x.Value)
                                    .ThenBy(x => x.ProgramAbbreviation)
                                    .ToList();
        }
        #endregion
    }
}
