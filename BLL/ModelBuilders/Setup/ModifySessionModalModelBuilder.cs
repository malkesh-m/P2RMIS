using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.Setup;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    internal class ModifySessionModalModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work providing access to the database</param>
        /// <param name="meetingSessionId">MeetingSession entity identifier being modified</param>
        public ModifySessionModalModelBuilder(IUnitOfWork unitOfWork, int meetingSessionId)
            : base(unitOfWork)
        {
            this.MeetingSessionId = meetingSessionId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// MeetingSession entity identifier target 
        /// </summary>
        protected int MeetingSessionId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<MeetingSession> SearchResults { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal Container<IModifySessionModalModel> Results { get; private set; } = new Container<IModifySessionModalModel>();
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            Search();
            MakeModel();
            SetReleasedFlag();
            SetScoringSetupFlag();
            CompleteModelPhases();
        }
        /// <summary>
        /// Does all the heavy lifting for retrieving the Award/Mechanism Setup grid data.
        /// </summary>
        internal virtual void Search()
        {
            //
            // Locate the meeting.
            //
            this.SearchResults = UnitOfWork.MeetingSessionRepository.Select()
                                //
                                // Get the session identified by the MeetingSessionId
                                //
                                .Where(x => x.MeetingSessionId == this.MeetingSessionId);
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        internal virtual void MakeModel()
        {
            //
            // Now that we have the records from the database that will populate the grid
            // we can model the data & return it.
            //
            IQueryable<IModifySessionModalModel> modelResults = SearchResults.Select(x => new ModifySessionModalModel
            {
                //
                // Pull out the model base data
                //
                ClientId = x.ClientMeeting.ClientId,
                ClientAbrv = x.ClientMeeting.Client.ClientAbrv,
                MeetingDescription = x.ClientMeeting.MeetingDescription,
                MeetingTypeName = x.ClientMeeting.MeetingType.MeetingTypeName,
                MeetingTypeId = x.ClientMeeting.MeetingTypeId,
                MeetingStartDate = x.ClientMeeting.StartDate,
                MeetingEndDate = x.ClientMeeting.EndDate,
                //
                // Even though they belong to both Add & Modify the phase lists come from different sources
                // In modify they can be filled from the MeetingSession
                //
                PreMeetingPhases = x.SessionPhases.Where(y => y.StepType.ReviewStage.ReviewStageId == ReviewStage.Asynchronous)
                                                  .Select(z => new GenericListEntry<Nullable<int>, IPhaseModel> { Index = z.SessionPhaseId, Value = new PhaseModel { ReviewPhase = z.StepType.StepTypeName, StepTypeId = z.StepTypeId, ReviewStageId = z.StepType.ReviewStageId, StartDate = z.StartDate, EndDate = z.EndDate, ReopenDate = z.ReopenDate, CloseDate = z.CloseDate } }),
                MeetingPhases = x.SessionPhases.Where(y => y.StepType.ReviewStage.ReviewStageId == ReviewStage.Synchronous)
                                                  .Select(z => new GenericListEntry<Nullable<int>, IPhaseModel> { Index = z.SessionPhaseId, Value = new PhaseModel { ReviewPhase = z.StepType.StepTypeName, StepTypeId = z.StepTypeId, ReviewStageId = z.StepType.ReviewStageId, StartDate = z.StartDate, EndDate = z.EndDate, ReopenDate = z.ReopenDate, CloseDate = z.CloseDate } }),
                //
                // and now the Modify specific data
                //
                SessionAbbreviation = x.SessionAbbreviation,
                SessionDescription = x.SessionDescription,
                SessionStart = x.StartDate,
                SessionEnd = x.EndDate,
                //
                // and now pull out the identifiers
                //
                ClientMeetingId = x.ClientMeetingId,
                MeetingSessionId = this.MeetingSessionId
            });
            //
            // And finally we execute the query:
            //
            this.Results.ModelList = modelResults.ToList();
        }
        /// <summary>
        /// Sets the flag whether any applications associated with have been released.
        /// </summary>
        protected virtual void SetReleasedFlag()
        {
            IModifySessionModalModel model = this.Results.Model;
            model.HasApplicationsBeenReleased = SearchResults.SelectMany(x => x.SessionPanels)
                    .SelectMany(x => x.PanelApplications)
                    .SelectMany(x => x.ApplicationStages)
                    .Any(x => x.AssignmentVisibilityFlag);
        }
        /// <summary>
        /// Sets a flag to indicate whether scoring has been set up.
        /// </summary>
        protected virtual void SetScoringSetupFlag()
        {
            IModifySessionModalModel model = this.Results.Model;
            model.HasScoringBeenSetup = SearchResults.SelectMany(x => x.SessionPanels)
                    .SelectMany(x => x.PanelApplications)
                    .Select(x => x.Application)
                    .Select(x => x.ProgramMechanism)
                    .SelectMany(x => x.MechanismTemplates)
                    .SelectMany(x => x.MechanismTemplateElements)
                    .SelectMany(x => x.MechanismTemplateElementScorings).Count() > 0;
        }
        /// <summary>
        /// Legacy data does not a ways have a complete set of phases.  In which case we need
        /// to complete the list.
        /// </summary>
        protected void CompleteModelPhases()
        {
            //
            // Retrieve a list of phase names for the pre-meeting phases (in order)
            //
            int phaseCount = MeetingType.DeterminePreMeetingPhaseCountForMeetingType(SearchResults.FirstOrDefault().ClientMeeting.MeetingTypeId);
            List<KeyValuePair<int, string>> defaultPreMeetingPhase = UnitOfWork.StepTypeRepository.Select()
                               .Where(x => x.ReviewStageId == ReviewStage.Asynchronous)
                               .OrderBy(x => x.SortOrder).Take(phaseCount).ToList()
                               .ConvertAll(x => new KeyValuePair<int, string>(x.StepTypeId, x.StepTypeName));
            //
            // Then we get a list of the current pre-meeting phase names & create a new list 
            //
            var currentPhases = this.Results.Model.PreMeetingPhases.Select(x => x.Value.ReviewPhase).ToList();
            var newPhaseList = this.Results.Model.PreMeetingPhases.ToList();
            //
            // Now we just check and see if any of the default phases are missing in the current list
            // and add one if it is not there.
            //
            foreach (var idAndName in defaultPreMeetingPhase)
            {
                if (!currentPhases.Contains(idAndName.Value))
                {
                    newPhaseList.Add(new GenericListEntry<Nullable<int>, IPhaseModel> { Value = new PhaseModel { StepTypeId = idAndName.Key, ReviewPhase = idAndName.Value } });
                    this.Results.Model.PreMeetingPhases = newPhaseList;
                }
            }

        }
        #endregion
    }

}
