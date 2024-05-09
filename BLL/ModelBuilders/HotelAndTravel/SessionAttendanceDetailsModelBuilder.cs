using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.HotelAndTravel;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.HotelAndTravel
{
    internal class SessionAttendanceDetailsModelBuilder : ModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public SessionAttendanceDetailsModelBuilder(IUnitOfWork unitOfWork, int panelUserAssignmentId, int userId)
            : base(unitOfWork, userId)
        {
            this.PanelUserAssignmentId = panelUserAssignmentId;
        }
        #endregion
        #region Attributes        
        /// <summary>
        /// Gets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int PanelUserAssignmentId { get; private set; }
        #endregion
        #region Services                
        /// <summary>
        /// Build the model.  Default implementation.
        /// </summary>
        /// <returns>
        /// Null
        /// </returns>
        public override IBuiltModel Build()
        {
            var panelUserAssignmentEntity = UnitOfWork.PanelUserAssignmentRepository.GetByID(PanelUserAssignmentId);

            SessionAttendanceDetailsModel model = new SessionAttendanceDetailsModel();
            model.PopulateSessionInformation(panelUserAssignmentEntity.SessionPanel.GetFiscalYear(), panelUserAssignmentEntity.SessionPanel.GetProgramAbbreviation(), panelUserAssignmentEntity.SessionPanel.PanelAbbreviation, panelUserAssignmentEntity.SessionPanel.StartDate, panelUserAssignmentEntity.SessionPanel.EndDate);

            MeetingRegistration meetingRegistrationnEntity = panelUserAssignmentEntity.MeetingRegistrations.FirstOrDefault();
            if (meetingRegistrationnEntity != null)
            {
                PopulateModelWithRegistration(model, meetingRegistrationnEntity, panelUserAssignmentEntity);
            }
            else
            {
                PopulateModelWithoutRegistration(model, panelUserAssignmentEntity);
            }
            return model;
        }
        /// <summary>
        /// Populates a ISessionAttendanceDetailsModel without a meeting registration exists
        /// </summary>
        /// <param name="model">ISessionAttendanceDetailsModel model to populate</param>
        /// <param name="panelUserAssignmentEntity">PanelUserAssignment entity</param>
        private void PopulateModelWithoutRegistration(ISessionAttendanceDetailsModel model, PanelUserAssignment panelUserAssignmentEntity)
        {
            model.PopulateHotel(panelUserAssignmentEntity.SessionPanel.HotelName(), panelUserAssignmentEntity.SessionPanel.HotelId(), panelUserAssignmentEntity.SessionPanel.StartDate, panelUserAssignmentEntity.SessionPanel.EndDate);
            model.PopulatePanelUserAssignment(PanelUserAssignmentId);
            model.PopulateRegistration(panelUserAssignmentEntity.MeetingRegistrations.FirstOrDefault()?.MeetingRegistrationId, panelUserAssignmentEntity.SessionPanel.StartDate, panelUserAssignmentEntity.SessionPanel.EndDate, false);
        }
        /// <summary>
        /// Populates a ISessionAttendanceDetailsModel when the meeting registration exists
        /// </summary>
        /// <param name="model">ISessionAttendanceDetailsModel model to populate</param>
        /// <param name="meetingRegistrationEntity">MeetingRegistration entity</param>
        /// <param name="panelUserAssignmentEntity">PanelUserAssignment entity</param>
        private void PopulateModelWithRegistration(ISessionAttendanceDetailsModel model, MeetingRegistration meetingRegistrationEntity, PanelUserAssignment panelUserAssignmentEntity)
        {
            //
            // By default we use the hotel name from the registration.  If there is not
            // one set there we use the hotel from the session
            //
            int? hotelId = meetingRegistrationEntity.HotelId() ?? panelUserAssignmentEntity.SessionPanel.HotelId();
            string hotelName = meetingRegistrationEntity.HotelName() ?? panelUserAssignmentEntity.SessionPanel.HotelName();

            model.PopulateHotelRegistration(hotelId, hotelName, meetingRegistrationEntity.HotelCheckInDate(), meetingRegistrationEntity.HotelCheckOutDate(),
                meetingRegistrationEntity.TravelModeId(), meetingRegistrationEntity.HotelNotRequired(), meetingRegistrationEntity.HotelDoubleOccupancy());
            model.PopulateRegistration(meetingRegistrationEntity.MeetingRegistrationId, meetingRegistrationEntity.AttenendanceStartDate(), meetingRegistrationEntity.AttenendanceEndDate(), meetingRegistrationEntity.RegistrationSubmittedFlag);
            model.PopulateSpecialNeeds(meetingRegistrationEntity.HotelAndFoodRequestComments(), meetingRegistrationEntity.TravelRequestComments());
            model.PopulatePanelUserAssignment(PanelUserAssignmentId);
        }
        #endregion
    }
}
