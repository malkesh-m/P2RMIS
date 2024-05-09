using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.HotelAndTravel;
using System.Linq;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.ModelBuilders.HotelAndTravel
{
    /// <summary>
    /// Builds one or more MeetingList web models for the specified user.
    /// </summary>
    internal class MeetingListModelBuilder: ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="userId">User entity identifier</param>
        public MeetingListModelBuilder(IUnitOfWork unitOfWork, int userId)
            : base(unitOfWork, userId)
        {
            this.Results = new Container<IMeetingListModel>();
            this.FactSheetResults = new Container<IFactSheetModel>();
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ContainerModelBuilder results.
        /// </summary>
        public Container<IMeetingListModel> Results { get; private set; }
        /// <summary>
        /// Container of FactSheets
        /// </summary>
        public Container<IFactSheetModel> FactSheetResults { get; private set; }
        #endregion
        #region Services
        /// <summary>
        /// Builds the container of MeetingList models to populate the grid.
        /// </summary>
        public override void BuildContainer()
        {
            User userEnity = GetThisUser(UserId);
            //
            // We only want to display certain reviewer assignments.  So find those assignments then 
            // determine which fact sheets should be shown
            //
            IEnumerable<PanelUserAssignment> reviewerAssignments = FilterAssignments(userEnity);
            IQueryable<PeerReviewDocument> factSheet = GetFactSheets(reviewerAssignments);

            //
            // Now we just populate the two containers of data.
            //
            PopulateContainer(factSheet);
            PopulateContainer(reviewerAssignments);
        }
        /// <summary>
        /// Populates a IMeetingListModel without a meeting registration exists
        /// </summary>
        /// <param name="model">IMeetingListModel model to populate</param>
        /// <param name="panelUserAssignmentEntity">PanelUserAssignment entity</param>
        private void PopulateModelWithoutRegistration(IMeetingListModel model, PanelUserAssignment panelUserAssignmentEntity)
        {
            model.PopulateStartAction();
            model.PopulateHotel(panelUserAssignmentEntity.SessionPanel.HotelName());
            model.PopulateAttendanceDates(panelUserAssignmentEntity.SessionPanel.StartDate, panelUserAssignmentEntity.SessionPanel.EndDate);
            model.PopulatePanelUserAssignment(panelUserAssignmentEntity.PanelUserAssignmentId);
        }
        /// <summary>
        /// Populates a IMeetingListModel when the meeting registration exists
        /// </summary>
        /// <param name="model">IMeetingListModel model to populate</param>
        /// <param name="meetingRegistrationEntity">MeetingRegistration entity</param>
        /// <param name="panelUserAssignmentEntity">PanelUserAssignment entity</param>
        private void PopulateModelWithRegistration(IMeetingListModel model, MeetingRegistration meetingRegistrationEntity, PanelUserAssignment panelUserAssignmentEntity)
        {
            //
            // By default we use the hotel name from the registration.  If there is not
            // one set there we use the hotel from the session
            //
            string hotelName = meetingRegistrationEntity.HotelName() ?? panelUserAssignmentEntity.SessionPanel.HotelName();

            model.PopulateHotelRegistration(hotelName, meetingRegistrationEntity.HotelCheckInDate(), meetingRegistrationEntity.HotelCheckOutDate());
            model.PopulateRegistration(meetingRegistrationEntity.AttenendanceStartDate(), meetingRegistrationEntity.AttenendanceEndDate());
            model.PopulateActions(meetingRegistrationEntity.CanEdit(), meetingRegistrationEntity.CanView());
            model.PopulateMeetingRegistrationEntityIdentifiers(panelUserAssignmentEntity.PanelUserAssignmentId,
                                                               meetingRegistrationEntity.MeetingRegistrationId,
                                                               meetingRegistrationEntity.MeetingRegistrationTravelId(),
                                                               meetingRegistrationEntity.MeetingRegistrationHotelId(),
                                                               meetingRegistrationEntity.MeetingRegistrationAttendanceId());
        }
        /// <summary>
        /// Filters panel assignments by applicable business rules.
        /// </summary>
        /// <param name="userEnity">User entity</param>
        /// <returns>Enumeration of applicable PanelUserAssignment</returns>
        protected IEnumerable<PanelUserAssignment> FilterAssignments(User userEnity)
        {
            IEnumerable<PanelUserAssignment> reviewerAssignments = userEnity.PanelUserAssignments.Where(x =>
                //
                // We only want reviewers if they are participating "in person"
                //
                (x.ParticipationMethodId == ParticipationMethod.Indexes.InPerson) &&
                //
                // and is not an "Adhoc" user (indicated by the RestrictedAssignedFlag)
                //
                (!x.RestrictedAssignedFlag) &&
                //
                // and there registration is complete
                //
                (x.IsRegistrationComplete()) &&
                //
                // and the end date has not passed
                //
                (x.SessionPanel.IsIncludeForHotelReservation()) &&
                //
                // and finally the reviewer attends "in person"
                //
                (x.SessionPanel.MeetingSession.ClientMeeting.MeetingTypeId == MeetingType.Onsite)
            );

            return reviewerAssignments;
        }
        /// <summary>
        /// Filters panel assignments by applicable business rules.
        /// </summary>
        /// <param name="reviewerAssignments">Enumeration of PanelUserAssignments</param>
        /// <returns>Enumeration of applicable ProgramMeetingInformation</returns>
        protected IQueryable<PeerReviewDocument> GetFactSheets(IEnumerable<PanelUserAssignment> reviewerAssignments)
        {
            var programIds = reviewerAssignments.SelectMany(y => y.SessionPanel.ProgramPanels).Select(y => y.ProgramYearId).ToList();
            IQueryable<ProgramYear> programs = UnitOfWork.ProgramYearRepository.Select().Where(x => programIds.Contains(x.ProgramYearId));
            IQueryable<PeerReviewDocument> docs = UnitOfWork.PeerReviewDocumentRepository.Select()
                .Where(x => programs.Select(y => y.ClientProgram.ClientId).Contains(x.ClientId) &&
                (x.ClientProgramId == null || programs.Select(y => y.ClientProgramId).Contains((int)x.ClientProgramId)) &&
                (x.FiscalYear == null || programs.Select(y => y.Year).Contains(x.FiscalYear)) &&
                (!x.ArchivedFlag) && x.PeerReviewDocumentTypeId == PeerReviewDocumentType.Lookups.MeetingInformation);

            return FilterFactSheets(docs, reviewerAssignments);
        }

        /// <summary>
        /// Filters the Fact Sheets based on configured access settings
        /// </summary>
        /// <returns>Enumeration of PeerReviewDocuments</returns>
        private IQueryable<PeerReviewDocument> FilterFactSheets(IQueryable<PeerReviewDocument> possibleDocs, IEnumerable<PanelUserAssignment> userParticipations)
        {
            ///
            // Get the documents the user does not have access to
            // NULL serve as wildcard and indicate there are no restrictions
            // 
            var participationList = userParticipations.Select(x => new
            {
                ClientParticipantTypeId = x.ClientParticipantTypeId,
                MeetingTypeId = x.SessionPanel.MeetingSession.ClientMeeting.MeetingTypeId,
                ParticipationMethodId = x.ParticipationMethodId,
                RestrictedAssignedFlag = x.RestrictedAssignedFlag

            }).ToList();

            var docList = possibleDocs.ToList().SelectMany(x => x.PeerReviewDocumentAccesses).Select(x => new
            {
                AllowedParticipantTypes = x.ClientParticipantTypeIds != null ? x.ClientParticipantTypeIds.Split(',').Select(int.Parse).ToList() : null,
                AllowedMeetings = x.MeetingTypeIds != null ? x.MeetingTypeIds.Split(',').Select(int.Parse).ToList() : null,
                AllowedParticipationMethods = x.ParticipationMethodIds != null ? x.ParticipationMethodIds.Split(',').Select(int.Parse).ToList() : null,
                RestrictedAssignedFlag = x.RestrictedAssignedFlag,
                PeerReviewDocumentId = x.PeerReviewDocumentId
            }).ToList();

            List<int> deniedDocuments = docList.Where(x => (x.AllowedParticipantTypes != null && !participationList.Any(y => x.AllowedParticipantTypes.Contains(y.ClientParticipantTypeId)))
            || (x.AllowedMeetings != null && !participationList.Any(y => x.AllowedMeetings.Contains(y.MeetingTypeId)))
            || (x.AllowedParticipationMethods != null && !participationList.Any(y => x.AllowedParticipationMethods.Contains(y.ParticipationMethodId)))
            || (x.RestrictedAssignedFlag != null && !participationList.Any(y => y.RestrictedAssignedFlag == x.RestrictedAssignedFlag))).Select(x => x.PeerReviewDocumentId).ToList();

            var allowedDocs = deniedDocuments != null ? possibleDocs.Where(x => !deniedDocuments.Contains(x.PeerReviewDocumentId)) : possibleDocs;
            return allowedDocs;
        }
        /// <summary>
        /// Retrieve the fact sheets
        /// </summary>
        /// <param name="factSheets">Enumerable list of applicable ProgramMeetingInformation entities</param>
        protected void PopulateContainer(IQueryable<PeerReviewDocument> factSheets)
        {
            //
            // First things first.  Create a list to hold the models
            //
            List<IFactSheetModel> list = new List<IFactSheetModel>();
            //
            // Now we just iterate over the ProgramMeetingInformation entities and construct the models.  There are two
            // versions of the models that can be created.  There is one for external fact sheets & one for internal (located
            // in P2RMIS V1.
            //
            factSheets.ToList().ForEach(x => list.Add(new FactSheetModel(x.PeerReviewDocumentId, x.Heading, x.ContentUrl, x.PeerReviewContentTypeId == PeerReviewContentType.Video, x.PeerReviewContentTypeId == PeerReviewContentType.Link)));
            //
            // Then we just set it into the container that is returned
            //
            FactSheetResults.ModelList = list;
        }
        /// <summary>
        /// Populates a container with IMeetingListModel models
        /// </summary>
        /// <param name="reviewerAssignments">Enumeration of PanelUserAssignment</param>
        protected void PopulateContainer(IEnumerable<PanelUserAssignment> reviewerAssignments)
        {
            List<IMeetingListModel> list = new List<IMeetingListModel>();

            foreach (var panelUserAssignmentEntity in reviewerAssignments)
            {
                //
                // Create a model & populate it with information about the panel.
                //
                IMeetingListModel model = new MeetingListModel();
                model.PopulateSessionInformation(panelUserAssignmentEntity.SessionPanel.GetFiscalYear(), panelUserAssignmentEntity.SessionPanel.GetProgramAbbreviation(), panelUserAssignmentEntity.SessionPanel.PanelAbbreviation, panelUserAssignmentEntity.SessionPanel.StartDate, panelUserAssignmentEntity.SessionPanel.EndDate);
                list.Add(model);
                //
                // If there is a registration for the reviewer's assignment, populate the model with the registration information.  Otherwise there
                // is no data to populate the model. In which case all we need to do is set the action to 'Start'.
                // 
                MeetingRegistration meetingRegistrationnEntity = panelUserAssignmentEntity.MeetingRegistrations.FirstOrDefault();
                if (meetingRegistrationnEntity != null)
                {
                    PopulateModelWithRegistration(model, meetingRegistrationnEntity, panelUserAssignmentEntity);
                }
                else
                {
                    PopulateModelWithoutRegistration(model, panelUserAssignmentEntity);
                }
            }
            Results.ModelList = list;

        }
        #endregion
    }
}
