using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct zero or more models for the SessionSetup grid.
    /// </summary>
    internal class SessionSetupModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="clientMeetingId">ClientMeeting entity identifier</param>
        public SessionSetupModelBuilder(IUnitOfWork unitOfWork, int clientMeetingId)
            : base(unitOfWork)
        {
            this.ClientMeetingId = clientMeetingId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Sessions are retrieved for this ClientMeeting
        /// </summary>
        public int ClientMeetingId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<MeetingSession> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<ISessionSetupModel> Results { get; private set; } = new Container<ISessionSetupModel>();
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
        /// Does all the heavy lifting for retrieving the Session Setup grid data.
        /// </summary>
        internal virtual void Search()
        {
            this.SearchResults = UnitOfWork.ClientMeetingRepository.Select()
                                //
                                // Get the meeting identified by the ClientMeetingId
                                //
                                .Where(x => x.ClientMeetingId == this.ClientMeetingId)
                                //
                                // then retrieve each session
                                //
                                .SelectMany(x => x.MeetingSessions);
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
            IQueryable<ISessionSetupModel> modelResults = SearchResults.Select(x => new SessionSetupModel
            {
                //
                // Pull out the data for the model
                //
                SessionAbbreviation = x.SessionAbbreviation,
                SessionDescription = x.SessionDescription,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                PanelHasApplications = (x.SessionPanels.SelectMany(y => y.PanelApplications).Count() > 0),
                ReviewersAssigned = (x.SessionPanels.SelectMany(y => y.PanelUserAssignments).Count() > 0),
                SessionPanelList = x.SessionPanels.Select(y => new SessionPanelListModel {
                                                                                        //
                                                                                        // Pull out the data for the model
                                                                                        //
                                                                                        ProgramAbbreviation = y.ProgramPanels.FirstOrDefault().ProgramYear.ClientProgram.ProgramAbbreviation,
                                                                                        PanelAbbreviation = y.PanelAbbreviation,
                                                                                        //
                                                                                        // and now pull out the identifiers
                                                                                        //
                                                                                        SessionPanelId = y.SessionPanelId
                //
                // Then give it an order
                //
                }).OrderBy(z => z.ProgramAbbreviation),
                //
                // and now pull out the identifiers
                //
                ClientId = x.ClientMeeting.ClientId,
                ClientMeetingId = this.ClientMeetingId,
                MeetingSessionId = x.MeetingSessionId,
                HasProgramMeetings = x.ClientMeeting.ProgramMeetings.Count > 0
            }).OrderBy(x => x.MeetingSessionId);
            //
            // And finally we order & execute it
            //
            this.Results.ModelList = modelResults.OrderBy(x => x.SessionAbbreviation).ToList();
        }
        #endregion
    }

}
