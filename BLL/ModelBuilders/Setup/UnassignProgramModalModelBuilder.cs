using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct one model for the UnassignProgramModal.
    /// </summary>
    internal class UnassignProgramModalModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="clientMeetingId">ClientMeeting entity identifier</param>
        public UnassignProgramModalModelBuilder(IUnitOfWork unitOfWork, int clientId, int clientMeetingId)
            : base(unitOfWork)
        {
            this.ClientId = clientId;
            this.ClientMeetingId = clientMeetingId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Wrapper to return the client abbreviation
        /// </summary>
        protected string ClientAbbreviation { get { return (Meeting != null) ? Meeting.Client.ClientAbrv : string.Empty; } }
        /// <summary>
        /// Wrapper to return the meeting abbreviation
        /// </summary>
        protected string MeetingName { get { return (Meeting != null) ? Meeting.MeetingDescription : string.Empty; } } 
        protected string MeetingAbbreviation { get { return (Meeting != null) ? Meeting.MeetingAbbreviation : string.Empty; } }
        /// <summary>
        /// Client Meeting
        /// </summary>
        protected ClientMeeting Meeting { get; set; }
        /// <summary>
        /// Client entity identifier. Identifies whose meeting should be displayed
        /// </summary>
        protected int ClientId { get; set; }
        /// <summary>
        /// ClientMeeting entity identifier
        /// </summary>
        protected int ClientMeetingId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<ProgramMeeting> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IUnassignProgramModalModel> Results { get; private set; } = new Container<IUnassignProgramModalModel>();
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
            this.SearchResults = UnitOfWork.ClientMeetingRepository.Select()
                                //
                                // Get the meeting identified by the ClientMeetingId
                                //
                                .Where(x => x.ClientMeetingId == this.ClientMeetingId)
                                //
                                // then for each of the ProgramMeeting's
                                //
                                .SelectMany(x => x.ProgramMeetings);
            //
            // Finally retrieve the ClientMeeting itself.  Needed to retrieve the information
            // from the header.
            //
            this.Meeting = UnitOfWork.ClientMeetingRepository.GetByID(ClientMeetingId);
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
            IQueryable<IUnassignProgramListModel> listModelResults = SearchResults.Select(x => new UnassignProgramListModel
            {
                //
                // Pull out the data for the model
                //
                ProgramAbbreviation = x.ProgramYear.ClientProgram.ProgramAbbreviation,
                Year = x.ProgramYear.Year,
                IsPanelAssigned = x.ClientMeeting.MeetingSessions
                    .SelectMany(y => y.SessionPanels)
                    .SelectMany(z => z.ProgramPanels)
                    .Where(z2 => z2.ProgramYearId == x.ProgramYearId)
                    .Count() != 0,
                //
                // and now pull out the identifiers
                //
                ProgramMeetingId = x.ProgramMeetingId,
                ClientMeetingId = this.ClientMeetingId,
                ClientProgramId = x.ProgramYear.ClientProgramId,
                ProgramYearId = x.ProgramYearId
            });
            //
            // And now we sort it
            //  - first by the program abbreviation (A,B, C ...)
            //  - second then by fiscal year (2016, 2015, ....)
            //
            listModelResults = listModelResults.OrderBy(x => x.ProgramAbbreviation).ThenByDescending(x => x.Year);
            //
            // Set up the model to return & return it.
            //
            UnassignProgramModalModel result = new UnassignProgramModalModel(this.ClientAbbreviation,
                                                                             this.MeetingAbbreviation,
                                                                             this.MeetingName,
                                                                             listModelResults.ToList());
            this.Results.ModelList = new List<IUnassignProgramModalModel>() { result };
        }
        #endregion
    }
}
