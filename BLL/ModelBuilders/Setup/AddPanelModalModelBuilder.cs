using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.Setup;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct a model for the AddPanel modal.
    /// </summary>
    internal class AddPanelModalModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work provides access to the database</param> 
        /// <param name="meetingSessionId">MeetingSession entity id (identifies the Session which the Panel will be added to.</param>
        public AddPanelModalModelBuilder(IUnitOfWork unitOfWork, int meetingSessionId)
            : base(unitOfWork)
        {
            this.MeetingSessionId = meetingSessionId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// MeetingSession entity identifier
        /// </summary>
        protected int MeetingSessionId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<MeetingSession> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IAddPanelModel> Results { get; private set; } = new Container<IAddPanelModel>();
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
            // Locate the meeting session.  We can retrieve everything we need from it.
            //
            this.SearchResults = UnitOfWork.MeetingSessionRepository.Select().
                                Where(x => x.MeetingSessionId == this.MeetingSessionId);
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
            IQueryable<IAddPanelModel> modelResults = SearchResults.Select(x => new AddPanelModel
            {
                //
                // Pull out the data for the mode.  (This is for the base)
                //
                ClientAbrv = x.ClientMeeting.Client.ClientAbrv,
                MeetingDescription = x.ClientMeeting.MeetingDescription,
                SessionAbbreviation = x.SessionAbbreviation,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                //
                // and now pull out the identifiers
                //
                MeetingSessionId = this.MeetingSessionId,
                ClientMeetingId = x.ClientMeetingId,
                //
                // And this is specific to the Add panel
                //
                Panels = x.SessionPanels.Select(y => new GenericListEntry<string, string> { Index =
                 y.ProgramPanels.FirstOrDefault().ProgramYear.Year + " - " + y.ProgramPanels.FirstOrDefault().ProgramYear.ClientProgram.ProgramAbbreviation, Value = y.PanelName }).OrderBy(z => z.Index),
                Programs = x.ClientMeeting.ProgramMeetings
                    .Select(y => new GenericListEntry<int, string> { Index = y.ProgramYearId, Value = y.ProgramYear.Year + " - " + y.ProgramYear.ClientProgram.ProgramAbbreviation }).OrderBy(z => z.Index).OrderBy(y => y.Value)
            });
            //
            // And finally execute it:
            //
            this.Results.ModelList = modelResults.ToList();
        }
        #endregion
    }

    /// <summary>
    /// Model builder to construct a model for the Update Panel modal.
    /// </summary>
    internal class UpdatePanelModalModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work provides access to the database</param> 
        /// <param name="sessionPanelId">SessionPanel entity id (identifies the SessionPanel is being updated)</param>
        public UpdatePanelModalModelBuilder(IUnitOfWork unitOfWork, int sessionPanelId)
            : base(unitOfWork)
        {
            this.SessionPanelId = sessionPanelId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// MeetingSession entity identifier
        /// </summary>
        protected int SessionPanelId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<SessionPanel> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IUpdatePanelModel> Results { get; private set; } = new Container<IUpdatePanelModel>();
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
        /// Does all the heavy lifting for retrieving the Update Panel modal data.
        /// </summary>
        internal virtual void Search()
        {
            //
            // Locate the meeting session.  We can retrieve everything we need from it.
            //
            this.SearchResults = UnitOfWork.SessionPanelRepository.Select().
                                Where(x => x.SessionPanelId == this.SessionPanelId);
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        internal virtual void MakeModels()
        {
            //
            // Now that we have the records from the database that will populate the modal
            // we can model the data & return it.
            //
            IQueryable<IUpdatePanelModel> modelResults = SearchResults.Select(x => new UpdatePanelModel
            {
                //
                // Pull out the data for the model.  (This is for the base)
                //
                ClientAbrv = x.MeetingSession.ClientMeeting.Client.ClientAbrv,
                MeetingDescription = x.MeetingSession.ClientMeeting.MeetingDescription,
                SessionAbbreviation = x.MeetingSession.SessionAbbreviation,
                StartDate = x.MeetingSession.StartDate,
                EndDate = x.MeetingSession.EndDate,
                ProgramAbbr = x.ProgramPanels.FirstOrDefault().ProgramYear.ClientProgram.ProgramAbbreviation,
                Year = x.ProgramPanels.FirstOrDefault().ProgramYear.Year,
                //
                // and now pull out the identifiers
                //
                MeetingSessionId = x.MeetingSessionId.Value,
                SessionPanelId = x.SessionPanelId,
                //
                // And this is specific to the Modify Panel modal
                //
                AreApplicationsReleased = x.PanelApplications.SelectMany(y => y.ApplicationStages).Where(y => y.ReviewStageId == ReviewStage.Indexes.Asynchronous).Any(y => y.AssignmentVisibilityFlag),
                AreApplicationsAssigned = x.PanelApplications.Count() != 0,
                AreUsersAssigned = x.PanelUserAssignments.Count() != 0,
                PanelAbbreviation = x.PanelAbbreviation,
                PanelName = x.PanelName,
                Sessions = x.MeetingSession.ClientMeeting.MeetingSessions.Where(y => y.MeetingSessionId != x.MeetingSessionId.Value).Select(y => new GenericListEntry<int, string> {Index = y.MeetingSessionId, Value = y.SessionAbbreviation })
            });
            //
            // And finally execute it:
            //
            this.Results.ModelList = modelResults.ToList();
        }
        #endregion
    }
}
