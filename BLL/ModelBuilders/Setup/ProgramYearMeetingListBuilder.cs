using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Lists;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct models for the Session Fee Schedule drop down list
    /// </summary>
    internal class ProgramYearMeetingListBuilder: ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="clientId">Client entity identifier of selected client</param>
        /// <param name="year">Selected year value</param>
        public ProgramYearMeetingListBuilder(IUnitOfWork unitOfWork, int programYearId)
            : base(unitOfWork)
        {
            this.ProgramYearId = programYearId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ProgramYear identifier of selected client
        /// </summary>
        protected int ProgramYearId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<ClientMeeting> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IGenericListEntry<int, string>> Results { get; private set; } = new Container<IGenericListEntry<int, string>>();
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
        /// Does all the heavy lifting for retrieving the Program Setup grid data.
        /// </summary>
        internal virtual void Search()
        {
            this.SearchResults = UnitOfWork.ProgramYearRepository.Select()
                                 //
                                 // First we get all the program years for the selected year & pull out their programs
                                 //
                                 .Where(x => x.ProgramYearId == this.ProgramYearId).SelectMany(x => x.ProgramMeetings)
                                 //
                                 // Then we filter on the selected programyearid meetings
                                 //
                                 .Select(x => x.ClientMeeting);
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
            IQueryable<IGenericListEntry<int, string>> modelResults = SearchResults.Select(x => new GenericListEntry<int, string>
            {
                Index = x.ClientMeetingId,
                Value = x.MeetingAbbreviation
            });
            //
            // then we order it & execute
            //
            this.Results.ModelList = modelResults.Distinct().OrderBy(x => x.Value).ToList();
        }
        #endregion
    }
}
