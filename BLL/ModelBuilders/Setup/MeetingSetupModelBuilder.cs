using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct one model for the Meeting Setup grid.
    /// </summary>
    internal class MeetingSetupModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="clientId">Client entity identifier</param>
        public MeetingSetupModelBuilder(IUnitOfWork unitOfWork, int clientId)
            : base(unitOfWork)
        {
            this.ClientId = clientId;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="fiscalYear">Fiscal year</param>
        /// <param name="programYearId">Program year identifier</param>
        public MeetingSetupModelBuilder(IUnitOfWork unitOfWork, int clientId, string fiscalYear, int? programYearId)
            : this(unitOfWork, clientId)
        {
            this.FiscalYear = fiscalYear;
            this.ProgramYearId = programYearId;
        }

        #endregion
        #region Attributes
        /// <summary>
        /// Client entity identifier. Identifies whose meeting should be displayed
        /// </summary>
        protected int ClientId { get; set; }
        /// <summary>
        /// Fiscal year.
        /// </summary>
        protected string FiscalYear { get; set; }
        /// <summary>
        /// Program year identifier.
        /// </summary>
        protected int? ProgramYearId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<ClientMeeting> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IMeetingSetupModel> Results { get; private set; } = new Container<IMeetingSetupModel>();
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
        /// Does all the heavy lifting for retrieving the Meeting Setup grid data.
        /// </summary>
        protected virtual void Search()
        {
            this.SearchResults = UnitOfWork.ClientMeetingRepository.Select()
                    //
                    // Go and get the meetings for this client.
                    //
                    .Where(x => x.ClientId == this.ClientId);
            if (ProgramYearId != null)
            {
                this.SearchResults = this.SearchResults.Where(
                    x => x.ProgramMeetings.Count(y => y.ProgramYearId == (int)ProgramYearId) > 0);
            }
            else if (!string.IsNullOrEmpty(FiscalYear))
            {
                this.SearchResults = this.SearchResults.Where(
                    x => x.ProgramMeetings.Count(y => y.ProgramYear.Year == FiscalYear) > 0);
            }
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        protected virtual void MakeModels()
        {
            //
            // Now that we have the records from the database that will populate the grid
            // we can model the data & return it.
            //
            IQueryable<IMeetingSetupModel> modelResults = SearchResults.Select(x => new MeetingSetupModel
            {
                //
                // Pull out the data for the model
                //
                MeetingAbbreviation = x.MeetingAbbreviation,
                MeetingDescription = x.MeetingDescription,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                HotelName = (x.HotelId != null)? x.Hotel.HotelName: string.Empty,
                MeetingTypeName = x.MeetingType.MeetingTypeName,
                ProgramCount = x.ProgramMeetings.Count(),
                SessionCount = x.MeetingSessions.Count(),
                PanelCount = x.MeetingSessions.AsQueryable().SelectMany(y => y.SessionPanels).Count(),
                HasPanelPassed = (x.EndDate < GlobalProperties.P2rmisDateToday),
                ModifiedDate = x.ModifiedDate,
                IsSessionsAssigned = (x.MeetingSessions.Count() > 0),                
                //
                // and now pull out the identifiers
                //
                ClientId = this.ClientId,
                ClientMeetingId = x.ClientMeetingId
            });
            //
            // And finally we execute it.
            //
            this.Results.ModelList = modelResults.ToList();
        }
        #endregion
    }
    /// <summary>
    /// Model builder to construct a model for the Add/Edit Meeting modal.
    /// </summary>
    internal class MeetingSetupModalModelBuilder : MeetingSetupModelBuilder
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="clientMeetingId">ClientMeeting entity identifier</param>
        public MeetingSetupModalModelBuilder(IUnitOfWork unitOfWork, int clientId, int clientMeetingId)
            : base(unitOfWork, clientId)
        {
            this.ClientMeetingId = clientMeetingId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ClientMeeting entity identifier
        /// </summary>
        protected virtual int ClientMeetingId {get; set;}
        /// <summary>
        /// Builder results.
        /// </summary>
        internal new Container<IMeetingSetupModalModel> Results { get; private set; } = new Container<IMeetingSetupModalModel>();
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            Search();
            FilterSearchResults();
            MakeModels();
        }
        /// <summary>
        /// Filter the search results for a specific Client Meeting.
        /// </summary>
        protected virtual void FilterSearchResults()
        {
            this.SearchResults = this.SearchResults.Where(y => y.ClientMeetingId == this.ClientMeetingId);
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        protected override void MakeModels()
        {
            //
            // Now that we have the records from the database that will populate the grid
            // we can model the data & return it.
            //
            IQueryable<IMeetingSetupModalModel> modelResults = SearchResults.Select(x => new MeetingSetupModalModel
            {
                //
                // Pull out the data for the model
                //
                ClientAbrv = x.Client.ClientAbrv,
                MeetingAbbreviation = x.MeetingAbbreviation,
                MeetingDescription = x.MeetingDescription,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                MeetingTypeId = x.MeetingTypeId,
                MeetingLocation = x.MeetingLocation,
                HotelId = x.HotelId,
                CreatedDate = x.CreatedDate,
                IsOnSite = x.MeetingTypeId == MeetingType.Onsite,
                //
                // and now pull out the identifiers
                //
                ClientMeetingId = this.ClientMeetingId,
                ClientId = this.ClientId,
                HasSessions = x.MeetingSessions.Count() > 0
            });
            //
            // And finally we run the query.
            //
            this.Results.ModelList = modelResults;
        }
        #endregion
    }

}
