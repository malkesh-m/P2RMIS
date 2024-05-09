using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using System.Linq;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct zero or more models for the SessionSetup Meeting drop down list.
    /// </summary>
    internal class SessionSetupMeetingListEntryModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        public SessionSetupMeetingListEntryModelBuilder(IUnitOfWork unitOfWork, int clientId)
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
        protected IQueryable<ClientMeeting> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<ISessionSetupMeetingListEntryModel> Results { get; private set; } = new Container<ISessionSetupMeetingListEntryModel>();
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
            //
            // Locate the program meetings.  From which we can retrieve the information 
            // in the list.
            //
            this.SearchResults = UnitOfWork.ClientRepository.Select()
                                //
                                // Get the meeting identified by the ClientMeetingId
                                //
                                .Where(x => x.ClientID == this.ClientId)
                                //
                                // then for each of the ProgramMeeting's
                                //
                                .SelectMany(x => x.ClientMeetings);
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
            IQueryable<ISessionSetupMeetingListEntryModel> modelResults = SearchResults.Select(x => new SessionSetupMeetingListEntryModel
            {
                //
                // Pull out the data for the model
                //
                MeetingAbbreviation = x.MeetingAbbreviation,
                MeetingDescription = x.MeetingDescription,
                MeetingTypeName = x.MeetingType.MeetingTypeName,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                HasEndDatePassed = x.EndDate < GlobalProperties.P2rmisDateTimeNow,
                //
                // and now pull out the identifiers
                //
                ClientId = this.ClientId,
                ClientMeetingId = x.ClientMeetingId,
                ActiveProgram = x.ProgramMeetings.Select(y => y.ProgramYear.DateClosed == null).Count() > 0
            });
            //
            // And finally we order & execute it:
            //
            this.Results.ModelList = modelResults
                                    //
                                    // Order the results by the meeting abbreviation
                                    //
                                    .OrderBy(x => x.MeetingAbbreviation)
                                    //
                                    // then execute it
                                    //
                                    .ToList();
        }
        #endregion
    }

}
