using System;
using Sra.P2rmis.Bll.ModelBuilders.HotelAndTravel;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.HotelAndTravel;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.Validations;

namespace Sra.P2rmis.Bll.HotelAndTravel
{
    /// <summary>
    /// HotelAndTravelService provides services for the reviewers Hotel & Travel application.
    /// </summary>
    public interface IHotelAndTravelService
    {
        /// <summary>
        /// Builds a container of MeetingListModels & FactSheetModels populate the Hotel & Travel MeetingList grid.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Tuple of models: MeetingListModel & FactSheetModel</returns>
        Tuple<Container<IMeetingListModel>, Container<IFactSheetModel>> RetrieveMeetingListEntries(int userId);
        /// <summary>
        /// Gets the session attendance details.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        ISessionAttendanceDetailsModel GetSessionAttendanceDetails(int panelUserAssignmentId, int userId);
        /// <summary>
        /// Save the contents of the ISessionAttendanceDetailsModel model.  Four tables are updated; the
        /// MeetingRegistration; MeetingRegistrationTravel; MeetingRegistrationHotel & MeetingRegistrationAttendance.
        /// </summary>
        /// <param name="model">ISessionAttendanceDetailsModel object containing information from the modal</param>
        /// <param name="userId">User entity identifier</param>
        Container<int> SaveSessionAttendance(ISessionAttendanceDetailsStringDateModel model, int userId);
        /// <summary>
        /// Save the contents of the ISessionAttendanceDetailsModel model.  Four tables are updated; the
        /// MeetingRegistration; MeetingRegistrationTravel; MeetingRegistrationHotel & MeetingRegistrationAttendance.
        /// </summary>
        /// <param name="model">ISessionAttendanceDetailsModel object containing information from the modal</param>
        /// <param name="userId">User entity identifier</param>
        Container<int> SubmitSessionAttendance(ISessionAttendanceDetailsStringDateModel model, int userId);
    }
    /// <summary>
    /// HotelAndTravelService provides services for the reviewers Hotel & Travel application.
    /// </summary>
    public class HotelAndTravelService : ServerBase, IHotelAndTravelService
    {
        /// <summary>
        /// Validation status for HotelAndTravel
        /// </summary>
        public static class HotelAndTravelStatusValue
        {
            /// <summary>
            /// Indicates the AttendanceStartDate is less than the SessionPanel start date
            /// </summary>
            public const int InvalidAttendanceStartDate = 1;
            /// <summary>
            /// Indicates the AttendanceEndDate is greater than the SessionPanel end date
            /// </summary>
            public const int InvalidAttendanceEndDate = 2;
            /// <summary>
            /// Indicates the CheckinDate is less than the SessionPanel start date
            /// </summary>
            public const int InvalidCheckinDate = 3;
            /// <summary>
            /// Indicates the CheckoutDate is greater than the SessionPanel end date
            /// </summary>
            public const int InvalidCheckoutDate = 4;
            /// <summary>
            /// Indicates the Attendance date range is invalid (start > end)
            /// </summary>
            public const int InvalidAttandanceDateRange = 5;
            /// <summary>
            /// Indicates the Check-in/Check-out date range is invalid (start > end)
            /// </summary>
            public const int InvalidHotelRange = 6;
            /// <summary>
            /// AttendanceStartDate cannot be sooner than now.
            /// </summary>
            public const int InvalidAttandanceDate = 7;
            /// <summary>
            /// CheckinStartDate cannot be sooner than now.
            /// </summary>
            public const int InvalidCheckinStartDate = 8;
            /// <summary>
            /// Checkin date is formatted incorrectly
            /// </summary>
            public const int InvalidCheckinDateFormat = 9;
            /// <summary>
            /// Checkout date is formatted incorrectly
            /// </summary>
            public const int InvalidCheckoutDateFormat = 10;
            /// <summary>
            /// Attendance start date is formatted incorrectly
            /// </summary>
            public const int InvalidAttendanceStartDateFormat = 11;
            /// <summary>
            /// Attendance end date is formatted incorrectly
            /// </summary>
            public const int InvalidAttendanceEndDateFormat = 12;
            /// <summary>
            /// Attendance start date is required
            /// </summary>
            public const int AttendanceStartDateIsRequired = 13;
            /// <summary>
            /// Attendance end date is required
            /// </summary>
            public const int AttendanceEndDateIsRequired = 14;
            /// <summary>
            /// Hotel check-in date is required
            /// </summary>
            public const int HotelCheckinDateIsRequired = 15;
            /// <summary>
            /// Hotel check-out date is required
            /// </summary>
            public const int HotelCheckoutDateIsRequired = 16;
            /// <summary>
            /// Travel Mode is required
            /// </summary>
            public const int TravelModeIsRequired = 17;
        }
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public HotelAndTravelService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion
        #region Services
        /// <summary>
        /// Builds a container of MeetingListModels & FactSheetModels populate the Hotel & Travel MeetingList grid.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Tuple of models: MeetingListModel & FactSheetModel</returns>
        public Tuple<Container<IMeetingListModel>, Container<IFactSheetModel>> RetrieveMeetingListEntries(int userId)
        {
            ValidateInt(userId, FullName(nameof(HotelAndTravelService), nameof(RetrieveMeetingListEntries)), nameof(userId));
            //
            // So we create a model builder to do all the heavy lifting then sit back and just return the results.
            //
            MeetingListModelBuilder builder = new MeetingListModelBuilder(UnitOfWork, userId);
            builder.BuildContainer();

            Tuple<Container<IMeetingListModel>, Container<IFactSheetModel>> results = Tuple.Create<Container<IMeetingListModel>, Container<IFactSheetModel>>(builder.Results, builder.FactSheetResults);
            return results;
        }
        /// <summary>
        /// Gets the session attendance details.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ISessionAttendanceDetailsModel GetSessionAttendanceDetails(int panelUserAssignmentId, int userId)
        {
            ValidateInt(panelUserAssignmentId, FullName(nameof(HotelAndTravelService), nameof(GetSessionAttendanceDetails)), nameof(panelUserAssignmentId));

            var builder = new SessionAttendanceDetailsModelBuilder(UnitOfWork, panelUserAssignmentId, userId);
            return builder.Build() as ISessionAttendanceDetailsModel;
        }
        /// <summary>
        /// Save the contents of the ISessionAttendanceDetailsModel model.  Four tables are updated; the
        /// MeetingRegistration; MeetingRegistrationTravel; MeetingRegistrationHotel & MeetingRegistrationAttendance.
        /// </summary>
        /// <param name="model">ISessionAttendanceDetailsStringDateModel object containing information from the modal</param>
        /// <param name="userId">User entity identifier</param>
        public Container<int> SaveSessionAttendance(ISessionAttendanceDetailsStringDateModel model, int userId)
        {
            //
            // First we validate the date coming in for consistency
            // Then if everything is OK we save the session
            //
            List<int> list = ValidateSessionAttendance(model);
            if (list.Count == 0)
            {
                SaveSessionAttendance(model, false, userId);
            }

            Container<int> result = new Container<int>();
            result.ModelList = list;
            return result;
        }
        /// <summary>
        /// Save the contents of the ISessionAttendanceDetailsModel model.  Four tables are updated; the
        /// MeetingRegistration; MeetingRegistrationTravel; MeetingRegistrationHotel & MeetingRegistrationAttendance.
        /// </summary>
        /// <param name="model">ISessionAttendanceDetailsStringDateModel object containing information from the modal</param>
        /// <param name="userId">User entity identifier</param>
        public Container<int> SubmitSessionAttendance(ISessionAttendanceDetailsStringDateModel model, int userId)
        {
            //
            // First we validate the date coming in for consistency
            // Then if everything is OK we submit the session
            //
            List<int> list = ValidateSessionAttendance(model);
            if (list.Count == 0)
            {
                SaveSessionAttendance(model, true, userId);
            }

            Container<int> result = new Container<int>();
            result.ModelList = list;
            return result;
        }
        /// <summary>
        /// Performs validation on the ISessionAttendanceDetailsModel entries.
        /// </summary>
        /// <param name="model">ISessionAttendanceDetailsStringDateModel object containing information from the modal</param>
        /// <returns>Collection of status values.</returns>
        internal virtual List<int> ValidateSessionAttendance(ISessionAttendanceDetailsStringDateModel model)
        {
            List<int> list = new List<int>();

            PanelUserAssignment p = UnitOfWork.PanelUserAssignmentRepository.GetByID(model.PanelUserAssignmentId);
            SessionPanel sp = p.SessionPanel;
            //
            //  Now that we have the base information we can validate the attendance dates.
            //
            list = ValidateAttendanceDates(model, sp, list);

            //
            // If the hotel is required we check the check-in/check-out dates and travel mode also
            //
            if (!model.HotelNotRequired)
            {
                list = ValidateHotelDates(model, sp, list);
                list = ValidateTravelMode(model, list);
            }
            return list;
        }
        /// <summary>
        /// Validates Attendance Dates
        /// </summary>
        /// <param name="model">ISessionAttendanceDetailsStringDateModel model object</param>
        /// <param name="sp">Session panel entiry</param>
        /// <param name="list">Current list of errors</param>
        /// <returns>Updated list of errors</returns>
        internal List<int> ValidateAttendanceDates(ISessionAttendanceDetailsStringDateModel model, SessionPanel sp, List<int> list)
        {
            bool validStartDate;
            bool validEndDate;

            bool missingStartDate = string.IsNullOrWhiteSpace(model.FormattedAttendanceStartDate);
            bool missingEndDate = string.IsNullOrWhiteSpace(model.FormattedAttendanceEndDate);

            list.AddIfFailed(!missingStartDate, HotelAndTravelStatusValue.AttendanceStartDateIsRequired);
            list.AddIfFailed(!missingEndDate, HotelAndTravelStatusValue.AttendanceEndDateIsRequired);


            validStartDate = (missingStartDate) ? false : model.ConvertAttendanceStartDate();
            validEndDate = (missingEndDate) ? false : model.ConvertAttendanceEndDate();

            list.AddIfFailed(validStartDate || missingStartDate, HotelAndTravelStatusValue.InvalidAttendanceStartDateFormat);
            list.AddIfFailed(validEndDate || missingEndDate, HotelAndTravelStatusValue.InvalidAttendanceEndDateFormat);

            if (validStartDate)
            {
                list.AddIfFailed(sp.WithinSessionPanelAllowedDates(model.AttendanceStartDate), HotelAndTravelStatusValue.InvalidAttendanceStartDate);
                list.AddIfFailed(Validations.IsValidDateRange(GlobalProperties.P2rmisDateTimeNow.Date, model.AttendanceStartDate), HotelAndTravelStatusValue.InvalidAttandanceDate);
            }
            if (validEndDate)
            {
                list.AddIfFailed(sp.WithinSessionPanelAllowedDates(model.AttendanceEndDate), HotelAndTravelStatusValue.InvalidAttendanceEndDate);
            }
            if (validStartDate && validEndDate)
            {
                list.AddIfFailed(Validations.IsValidDateRange(model.AttendanceStartDate, model.AttendanceEndDate), HotelAndTravelStatusValue.InvalidAttandanceDateRange);
            }

            return list;
        }
        /// <summary>
        /// Validates Attendance Dates
        /// </summary>
        /// <param name="model">ISessionAttendanceDetailsStringDateModel model object</param>
        /// <param name="sp">Session panel entiry</param>
        /// <param name="list">Current list of errors</param>
        /// <returns>Updated list of errors</returns>
        internal List<int> ValidateHotelDates(ISessionAttendanceDetailsStringDateModel model, SessionPanel sp, List<int> list)
        {
            bool validStartDate;
            bool validEndDate;

            bool missingStartDate = string.IsNullOrWhiteSpace(model.FormattedHotelCheckInDate);
            bool missingEndDate = string.IsNullOrWhiteSpace(model.FormattedHotelCheckOutDate);

            list.AddIfFailed(!missingStartDate, HotelAndTravelStatusValue.HotelCheckinDateIsRequired);
            list.AddIfFailed(!missingEndDate, HotelAndTravelStatusValue.HotelCheckoutDateIsRequired);

            validStartDate = (missingStartDate) ? false : model.ConvertHotelCheckinDate();
            validEndDate = (missingEndDate) ? false : model.ConvertHotelCheckoutDate();

            list.AddIfFailed(validStartDate || missingStartDate, HotelAndTravelStatusValue.InvalidCheckinDateFormat);
            list.AddIfFailed(validEndDate || missingEndDate, HotelAndTravelStatusValue.InvalidCheckoutDateFormat);

            if (validStartDate)
            {
                list.AddIfFailed(sp.WithinSessionPanelAllowedDates(model.HotelCheckInDate), HotelAndTravelStatusValue.InvalidCheckinDate);
                list.AddIfFailed(Validations.IsValidDateRange(GlobalProperties.P2rmisDateTimeNow.Date, model.HotelCheckInDate), HotelAndTravelStatusValue.InvalidCheckinStartDate);
            }
            if (validEndDate)
            {
                list.AddIfFailed(sp.WithinSessionPanelAllowedDates(model.HotelCheckOutDate), HotelAndTravelStatusValue.InvalidCheckoutDate);
            }

            if (validStartDate && validEndDate)
            {
                list.AddIfFailed(Validations.IsValidDateRange(model.HotelCheckInDate, model.HotelCheckOutDate), HotelAndTravelStatusValue.InvalidHotelRange);
            }

            return list;
        }
        /// <summary>
        /// Validates Travel Mode
        /// </summary>
        /// <param name="model">ISessionAttendanceDetailsStringDateModel model object</param>
        /// <param name="list">Current list of errors</param>
        /// <returns>Updated list of errors</returns>
        internal List<int> ValidateTravelMode(ISessionAttendanceDetailsStringDateModel model, List<int> list)
        {
            list.AddIfFailed(model.IsTravelModeValid(), HotelAndTravelStatusValue.TravelModeIsRequired);
            return list;
        }

        /// <summary>
        /// Save the contents of the ISessionAttendanceDetailsModel model.  Four tables are updated; the
        /// MeetingRegistration; MeetingRegistrationTravel; MeetingRegistrationHotel & MeetingRegistrationAttendance.
        /// </summary>
        /// <param name="model">ISessionAttendanceDetailsModel object containing information from the modal</param>
        /// <param name="isRegistrationSubmitted">Indicates if the registration is being submitted</param>
        /// <param name="userId">User entity identifier</param>
        internal virtual void SaveSessionAttendance(ISessionAttendanceDetailsModel model, bool isRegistrationSubmitted, int userId)
        {
            //
            // Update each of the MeetingRegistration tables.
            //
            MeetingRegistration meetingRegistrationEntity = CreateUpdateMeetingRegistration(model, isRegistrationSubmitted, userId);
            CreateUpdateMeetingRegistrationTravel(model, meetingRegistrationEntity, userId);
            CreateUpdateMeetingRegistrationHotel(model, meetingRegistrationEntity, isRegistrationSubmitted, userId);
            CreateUpdateMeetingRegistrationAttendance(model, meetingRegistrationEntity, userId);
            //
            // Now that we have done that we just need to save them all in a transaction.
            //
            UnitOfWork.Save();
        }
        /// <summary>
        /// Updates or creates the MeetingRegistration entity 
        /// </summary>
        /// <param name="model">Registration model</param>
        /// <param name="isRegistrationSubmitted">Indicates if the registration has been submitted?</param>
        /// <param name="userId">User entity identifier</param>
        internal MeetingRegistration CreateUpdateMeetingRegistration(ISessionAttendanceDetailsModel model, bool isRegistrationSubmitted, int userId)
        {
            MeetingRegistrationServiceAction action = new MeetingRegistrationServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.MeetingRegistrationRepository, ServiceAction<MeetingRegistration>.DoNotUpdate, 0, userId);
            action.Populate(model, isRegistrationSubmitted);
            action.Execute();

            return action.GetEntity();
        }

        /// <summary>
        /// Updates or creates the MeetingRegistrationTravel entity 
        /// </summary>
        /// <param name="model">Registration model</param>
        /// <param name="meetingRegistrationEntity">MeetingRegistration entity</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>MeetingRegistrationTravel entity created or updated</returns>
        internal MeetingRegistrationTravel CreateUpdateMeetingRegistrationTravel(ISessionAttendanceDetailsModel model, MeetingRegistration meetingRegistrationEntity, int userId)
        {
            MeetingRegistrationTravelServiceAction action = new MeetingRegistrationTravelServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.MeetingRegistrationTravelRepository, ServiceAction<MeetingRegistrationTravel>.DoNotUpdate, 0, userId);
            action.Populate(model, meetingRegistrationEntity.MeetingRegistrationTravelId());
            action.Execute();
            //
            // Retrieve the entity that was created or updated.  If it was created then we need to add it to
            // the MeetingRegistration
            //
            MeetingRegistrationTravel entity = action.GetEntity();
            meetingRegistrationEntity.AddMeetingRegistrationTravel(entity);
            return entity;
        }
        /// <summary>
        /// Updates or creates the MeetingRegistrationHotel entity 
        /// </summary>
        /// <param name="model">Registration model</param>
        /// <param name="meetingRegistrationEntity">MeetingRegistration entity</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>MeetingRegistrationHotel entity created or updated</returns>
        internal MeetingRegistrationHotel CreateUpdateMeetingRegistrationHotel(ISessionAttendanceDetailsModel model, MeetingRegistration meetingRegistrationEntity, bool isRegistrationSubmitted, int userId)
        {
            MeetingRegistrationHotelServiceAction action = new MeetingRegistrationHotelServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.MeetingRegistrationHotelRepository, ServiceAction<MeetingRegistrationHotel>.DoNotUpdate, 0, userId);
            action.Populate(model, meetingRegistrationEntity.MeetingRegistrationHotelId());
            action.Execute();
            //
            // Retrieve the entity that was created or updated.  If it was created then we need to add it to
            // the MeetingRegistration
            //
            MeetingRegistrationHotel entity = action.GetEntity();

            if (isRegistrationSubmitted) // if registration submitted by the reviewer (as per the boolean variable, then set the ParticipantModifiedDate 
            {
                entity.ParticipantModifiedDate = GlobalProperties.P2rmisDateTimeNow;
            }
            meetingRegistrationEntity.AddMeetingRegistrationHotel(entity);
            return entity;
        }
        /// <summary>
        /// Updates or creates the MeetingRegistrationHotel entity 
        /// </summary>
        /// <param name="model">Registration model</param>
        /// <param name="meetingRegistrationEntity">MeetingRegistration entity</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>MeetingRegistrationHotel entity created or updated</returns>
        internal MeetingRegistrationAttendance CreateUpdateMeetingRegistrationAttendance(ISessionAttendanceDetailsModel model, MeetingRegistration meetingRegistrationEntity, int userId)
        {
            MeetingRegistrationAttendanceServiceAction action = new MeetingRegistrationAttendanceServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.MeetingRegistrationAttendanceRepository, ServiceAction<MeetingRegistrationAttendance>.DoNotUpdate, 0, userId);
            action.Populate(model, meetingRegistrationEntity.MeetingRegistrationAttendanceId());
            action.Execute();
            //
            // Retrieve the entity that was created or updated.  If it was created then we need to add it to
            // the MeetingRegistration
            //
            MeetingRegistrationAttendance entity = action.GetEntity();
            meetingRegistrationEntity.AddMeetingRegistrationAttendance(entity);
            return entity;
        }
        #endregion
    }
}
