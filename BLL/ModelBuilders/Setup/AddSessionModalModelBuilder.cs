using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.Setup;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    internal class AddSessionModalModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work providing access to the database</param>
        /// <param name="clientMeetingId">ClientMeeting entity identifier to Add the session to.</param>
        public AddSessionModalModelBuilder(IUnitOfWork unitOfWork, int clientMeetingId)
            : base(unitOfWork)
        {
            this.ClientMeetingId = clientMeetingId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ClientMeeting entity identifier target for the new session
        /// </summary>
        protected int ClientMeetingId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<ClientMeeting> SearchResults { get; set; }
        /// <summary>
        /// StepType Search results for the Pre-Meeting
        /// </summary>
        protected IQueryable<StepType> StepTypeSearchResultsForPreMeeting { get; set; }
        /// <summary>
        /// StepType Search results for the Meeting
        /// </summary>
        protected IQueryable<StepType> StepTypeSearchResultsForMeeting { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal Container<IAddSessionModalModel> Results { get; private set; } = new Container<IAddSessionModalModel>();
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            Search();
            MakeBase();
        }
        /// <summary>
        /// Does all the heavy lifting for retrieving the Award/Mechanism Setup grid data.
        /// </summary>
        internal virtual void Search()
        {
            //
            // Locate the meeting.
            //
            this.SearchResults = UnitOfWork.ClientMeetingRepository.Select()
                                //
                                // Get the meeting identified by the ClientMeetingId
                                //
                                .Where(x => x.ClientMeetingId == this.ClientMeetingId);
            //
            // Now pull out the Pre-Meeting steps.
            //
            this.StepTypeSearchResultsForPreMeeting = UnitOfWork.StepTypeRepository.Select()
                                .Where(x => x.ReviewStageId == ReviewStage.Asynchronous)
                                .OrderBy(x => x.SortOrder);
            //
            // Now pull out the Meeting steps. 
            //
            this.StepTypeSearchResultsForMeeting = UnitOfWork.StepTypeRepository.Select()
                               .Where(x => x.ReviewStageId == ReviewStage.Synchronous)
                               .OrderBy(x => x.SortOrder);
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        internal virtual void MakeBase()
        {
            //
            // Now that we have the records from the database that will populate the grid
            // we can model the data & return it.
            //
            IQueryable<IAddSessionModalModel> modelResults = SearchResults.Select(x => new AddSessionModalModel
            {
                //
                // Pull out the data for the model
                //
                ClientAbrv = x.Client.ClientAbrv,
                MeetingDescription = x.MeetingDescription,
                MeetingTypeName = x.MeetingType.MeetingTypeName,
                MeetingTypeId = x.MeetingTypeId,
                MeetingStartDate = x.StartDate,
                MeetingEndDate = x.EndDate,
                //
                // and now pull out the identifiers
                //
                ClientMeetingId = x.ClientMeetingId
            });
            //
            // Finally we execute the query & populate the Phase grids
            //
            IAddSessionModalModel model = modelResults.ToList().First();
            int phaseCount = MeetingType.DeterminePreMeetingPhaseCountForMeetingType(model.MeetingTypeId);
            //
            // The Pre-Meeting phases can be variable depending upon the meeting type
            //
            model.PreMeetingPhases = this.StepTypeSearchResultsForPreMeeting.Take(phaseCount).Select(x => new GenericListEntry<Nullable<int>, IPhaseModel> {Value = new PhaseModel { StepTypeId = x.StepTypeId, ReviewPhase = x.StepTypeName } }).ToList();
            //
            // Determine the Meeting phase count
            //
            int meetingPhaseCount = MeetingType.DetermineMeetingPhaseCountForMeetingType(model.MeetingTypeId);
            if (meetingPhaseCount > 0)
            {
                model.MeetingPhases = this.StepTypeSearchResultsForMeeting.Take(meetingPhaseCount).Select(x => new GenericListEntry<Nullable<int>, IPhaseModel> { Value = new PhaseModel { StepTypeId = x.StepTypeId, ReviewPhase = x.StepTypeName } }).ToList();          
            }
            this.Results.ModelList = new List<IAddSessionModalModel>() { model };
        }
        #endregion
    }
}
