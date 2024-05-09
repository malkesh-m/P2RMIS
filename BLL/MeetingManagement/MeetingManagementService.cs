using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;
using Webmodel = Sra.P2rmis.WebModels.MeetingManagement.MeetingCommentModel;
using Sra.P2rmis.WebModels.MeetingManagement;
using System;
using static Sra.P2rmis.WebModels.MeetingManagement.MeetingCommentModel;
using Sra.P2rmis.WebModels.HotelAndTravel;
using Sra.P2rmis.Bll.HotelAndTravel;
using System.Data.Entity;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.WebModels.Lists;

namespace Sra.P2rmis.Bll.MeetingManagement
{
    public partial interface IMeetingManagementService
    {
        /// <summary>
        /// Gets the meeting registration list.
        /// </summary>
        /// <param name="clientIdList">The client identifier list.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        List<MeetingAttendanceModel> GetMeetingAttendanceList(List<int> clientIdList, string firstName, string lastName, string fiscalYear, int? programYearId,
            int? clientMeetingId, int? meetingSessionId, int? sessionPanelId);
        /// <summary>
        /// Gets the non reviewer meeting attendance list.
        /// </summary>
        /// <param name="clientIdList">The client identifier list.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <returns></returns>
        List<MeetingAttendanceModel> GetNonReviewerMeetingAttendanceList(List<int> clientIdList, string firstName, string lastName, string fiscalYear, int? programYearId,
            int? clientMeetingId, int? meetingSessionId);
        /// <summary>
        /// Gets carrier list.
        /// </summary>
        /// <returns></returns>
        List<string> GetCarrierList();
        /// <summary>
        /// Gets airport list.
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<string, string>> GetAirportList();
        /// <summary>
        /// Gets the meeting hotel.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        MeetingHotelModel GetMeetingHotel(int? panelUserAssignmentId, int? sessionUserAssignmentId);
        /// <summary>
        /// Gets the meeting travel.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <returns></returns>
        MeetingTravelModel GetMeetingTravel(int? panelUserAssignmentId, int? sessionUserAssignmentId);
        /// <summary>
        /// Gets the meeting comment.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        MeetingCommentModel GetMeetingComment(int? panelUserAssignmentId, int? sessionUserAssignmentId);
        /// <summary>
        /// Saves the hotel details.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="attendanceStartDate">The attendance start date.</param>
        /// <param name="attendanceEndDate">The attendance end date.</param>
        /// <param name="hotelNotRequiredFlag">if set to <c>true</c> hotel is not required.</param>
        /// <param name="checkInDate">The check in date.</param>
        /// <param name="checkOutDate">The check out date.</param>
        /// <param name="hotelId">The hotel identifier.</param>
        /// <param name="doubleOccupancy">if set to <c>true</c> [double occupancy].</param>
        /// <param name="hotelAndFoodRequestComments">The hotel and food request comments.</param>
        /// <param name="cancellationDate">The cancellation date.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedByName">Name of the modified by.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        void SaveHotelDetails(int? panelUserAssignmentId, int? sessionUserAssignmentId, DateTime? attendanceStartDate, DateTime? attendanceEndDate, bool hotelNotRequiredFlag,
            DateTime? checkInDate, DateTime? checkOutDate, int? hotelId, bool doubleOccupancy, string hotelAndFoodRequestComments,
            DateTime? cancellationDate, DateTime? modifiedDate, string modifiedByName, int userId);
        /// <summary>
        /// Saves the travel details.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="travelModeId">The travel mode identifier.</param>
        /// <param name="fare">The fare.</param>
        /// <param name="agentFee">The agent fee.</param>
        /// <param name="agentFee2">The 2nd agent fee.</param>
        /// <param name="changeFee">The change fee.</param>
        /// <param name="ground">if set to <c>true</c> [ground].</param>
        /// <param name="nteAmount">The nte amount.</param>
        /// <param name="gsaRate">The gsa rate.</param>
        /// <param name="noGsa">if set to <c>true</c> [no gsa].</param>
        /// <param name="clientApprovedAmount">The client approved amount.</param>
        /// <param name="cancelledDate">The cancelled date.</param>
        /// <param name="travelRequestComments">The travel request comments.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedByName">Name of the modified by.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        void SaveTravelDetails(int? panelUserAssignmentId, int? sessionUserAssignmentId, int? travelModeId, decimal? fare, decimal? agentFee, decimal? agentFee2, decimal? changeFee, bool ground, decimal? nteAmount, decimal? gsaRate,
            bool noGsa, decimal? clientApprovedAmount, DateTime? cancelledDate, string travelRequestComments, DateTime? modifiedDate, string modifiedByName, string reservation, int userId);

        /// <summary>
        /// Marks the hotel meeting details as complete.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        bool MarkHotelDataComplete(int? panelUserAssignmentId, int? sessionUserAssignmentId, int userId);

        /// <summary>
        /// Marks the travel meeting details as complete.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        bool MarkTravelDataComplete(int? panelUserAssignmentId, int? sessionUserAssignmentId, int userId);
        /// <summary>
        /// Gets flights.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <returns></returns>
        List<TravelFlightModel> GetFlights(int? panelUserAssignmentId, int? sessionUserAssignmentId);
        /// <summary>
        /// Saves travel flight.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="flightId">The meeting registration travel flight identifier.</param>
        /// <param name="carrierName">The carrier name.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="departureCity">The departure city.</param>
        /// <param name="departureDate">The departure date.</param>
        /// <param name="arrivalCity">The arrival city.</param>
        /// <param name="arrivalDate">The arrival date.</param>
        /// <param name="userId">The logged-in user identifier.</param>
        /// <returns></returns>
        int? SaveTravelFlight(int? panelUserAssignmentId, int? sessionUserAssignmentId, int? flightId,
            string carrierName, string flightNumber, 
            string departureCity, DateTime departureDate,
            string arrivalCity, DateTime arrivalDate, int userId);
        /// <summary>
        /// Saves travel flights
        /// </summary>
        /// <param name="flights">The flights.</param>
        /// <param name="userId">The user identifier.</param>
        void SaveTravelFlights(List<TravelFlightModel> flights, int userId);
        /// <summary>
        /// Deletes travel flight.
        /// </summary>
        /// <param name="flightId">The flight identifier.</param>
        /// <param name="userId">The logged-in user identifier.</param>
        void DeleteTravelFlight(int flightId, int userId);
        /// <summary>
        /// Saves the comments details.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <param name="internalComments">The internal comments.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        bool SaveCommentsDetails(int? panelUserAssignmentId, int? sessionUserAssignmentId, string internalComments, int userId);
        /// <summary>
        /// Imports the travel flights.
        /// </summary>
        /// <param name="travelFlights">The travel flights.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        ICollection<KeyValuePair<int, SaveTravelFlightStatus>> ImportTravelFlights(IList<TravelFlightImportModel> travelFlights, int userId);
        /// <summary>
        /// Gets the meeting details header based on whether panelUserAssignmentId or sessionUserAssignmentId has value.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <returns></returns>
        IMeetingDetailsHeader GetMeetingDetailsHeader(int? panelUserAssignmentId, int? sessionUserAssignmentId);
        /// <summary>
        /// Gets the meeting details header.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        IMeetingDetailsHeader GetMeetingDetailsHeaderReviewer(int panelUserAssignmentId);
        /// <summary>
        /// Gets the meeting details header non reviewer.
        /// </summary>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <returns></returns>
        IMeetingDetailsHeader GetMeetingDetailsHeaderNonReviewer(int sessionUserAssignmentId);
        /// <summary>
        /// Gets the non reviewer assigned sessions.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        List<int> GetNonReviewerAssignedSessions(int clientMeetingId, int userId);
        /// <summary>
        /// Saves the non reviewer assignments.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="assignments">The assignments.</param>
        /// <param name="assignmentRemovals">The assignment removals.</param>
        /// <param name="currUser">The curr user.</param>
        /// <returns></returns>
        bool SaveNonReviewerAssignments(int clientMeetingId, int userId, List<int> assignments, List<int> assignmentRemovals, int currUser);
        /// <summary>
        /// Retrieves the session fee schedule meeting list.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="onSiteOnly">If required to restrict meeting to on site</param>
        /// <returns></returns>
        List<GenericListEntry<int, string>> RetrieveMeetingList(int programYearId, bool onSiteOnly = false);
        /// <summary>
        /// Retrieves the hoteland travel meeting list. Overloading to take a string FY instead of the program year id.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientList">The client list.</param>
        /// <param name="onSiteOnly">if set to <c>true</c> [on site only].</param>
        /// <returns></returns>
        List<GenericListEntry<int, string>> RetrieveMeetingList(string fiscalYear, List<int> clientList, bool onSiteOnly = false);
        /// <summary>
        /// Retrieves the hoteland travel program list.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="onSiteOnly">if set to <c>true</c> [on site only].</param>
        /// <returns></returns>
        List<GenericListEntry<int, string>> RetrieveProgramList(int clientMeetingId, bool onSiteOnly = false);
        /// <summary>
        /// Overload that takes fiscaYear string and user's client list.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientList">The client list.</param>
        /// <returns></returns>
        List<GenericListEntry<int, string>> RetrieveProgramList(string fiscalYear, List<int> clientList);

    }

    public partial class MeetingManagementService : ServerBase, IMeetingManagementService
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public MeetingManagementService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion

        /// <summary>
        /// Gets the meeting registration list.
        /// </summary>
        /// <param name="clientIdList">The client identifier list.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="nonReviewerOnly">The non reviewer only.</param>
        /// <returns></returns>
        public List<MeetingAttendanceModel> GetMeetingAttendanceList(List<int> clientIdList, string firstName, string lastName, string fiscalYear, int? programYearId,
            int? clientMeetingId, int? meetingSessionId, int? sessionPanelId)
        {
            bool onSiteOnly = true; // will retrieve only onsite meetings
            var os1 = UnitOfWork.MeetingRegistrationRepository.GetReviewerAssignmentList(clientIdList, firstName, lastName, fiscalYear, programYearId, clientMeetingId,
                meetingSessionId, sessionPanelId, onSiteOnly).ToList();
            var os2 = UnitOfWork.MeetingRegistrationRepository.GetNonReviewerAssignmentList(clientIdList, firstName, lastName, fiscalYear, programYearId, clientMeetingId,
                meetingSessionId, sessionPanelId, onSiteOnly).ToList();
            // Convert to list before concat to avoid unsupported nested query 
            var os = os1 != null ? os1.Concat(os2).OrderBy(x => x.LastName).ToList() : os2;
            return os;
        }

        /// <summary>
        /// Gets the non reviewer meeting attendance list.
        /// </summary>
        /// <param name="clientIdList">The client identifier list.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <returns></returns>
        public List<MeetingAttendanceModel> GetNonReviewerMeetingAttendanceList(List<int> clientIdList, string firstName, string lastName, string fiscalYear, int? programYearId,
            int? clientMeetingId, int? meetingSessionId)
        {
            bool onSiteOnly = false; // no restriction for onsite meetings only
            var os = UnitOfWork.MeetingRegistrationRepository.GetNonReviewerAssignmentList(clientIdList, firstName, lastName, fiscalYear, programYearId, clientMeetingId,
                meetingSessionId, null, onSiteOnly).OrderBy(x => x.LastName).ToList();

            // Build distinct list of non-reviewer users
            List<MeetingAttendanceModel> list = new List<MeetingAttendanceModel>();
            foreach (var item in os)
            {
                if (list.Where(x => x.ReviewerUserId == item.ReviewerUserId).Count() == 0)
                {
                    MeetingAttendanceModel newItem = new MeetingAttendanceModel(
                        item.ReviewerUserId,
                        item.FirstName, item.LastName,
                        (programYearId.HasValue) ? item.PanelAbbreviation : null,
                        (!string.IsNullOrEmpty(fiscalYear)) ? item.FiscalYear : null,
                        (clientMeetingId.HasValue) ? item.MeetingAbbreviation : null,
                        (meetingSessionId.HasValue) ? item.SessionName : null,
                        item.Institution,
                        item.Email,
                        item.Role
                        );
                    list.Add(newItem);
                }
            }
            return list;
        }


        /// <summary>
        /// Gets the non reviewer assigned sessions.
        /// </summary>
        /// <param name="fiscalYear"></param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public List<int> GetNonReviewerAssignedSessions(int clientMeetingId, int userId)
        {
            var result = UnitOfWork.SessionUserAssignmentRepository.Select().Where(x => x.MeetingSession.ClientMeetingId == clientMeetingId && x.UserId == userId)
                .Select(x => (int)x.MeetingSessionId).Distinct();

            return result.ToList();
        }

        /// <summary>
        /// Gets the meeting details header reviewer.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <returns></returns>
        public IMeetingDetailsHeader GetMeetingDetailsHeader(int? panelUserAssignmentId, int? sessionUserAssignmentId)
        {
            if (panelUserAssignmentId.HasValue)
            {
                return GetMeetingDetailsHeaderReviewer((int)panelUserAssignmentId);
            } else
            {
                return GetMeetingDetailsHeaderNonReviewer((int)sessionUserAssignmentId);
            }
        }

        /// <summary>
        /// Gets the meeting details header.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        public IMeetingDetailsHeader GetMeetingDetailsHeaderReviewer(int panelUserAssignmentId)
        {
            var panelAssignment = UnitOfWork.PanelUserAssignmentRepository.GetByID(panelUserAssignmentId);
            var result =  new MeetingDetailsHeader {
                    Attendee = panelAssignment.User.FullName(),
                    Email = panelAssignment.PrimaryUserEmailAddress(),
                    Meeting = panelAssignment.SessionPanel.MeetingSession.ClientMeeting.MeetingAbbreviation,
                    MeetingStart = panelAssignment.SessionPanel.MeetingSession.ClientMeeting.StartDate,
                    MeetingEnd = panelAssignment.SessionPanel.MeetingSession.ClientMeeting.EndDate,
                    Panel = panelAssignment.GetPanelAbbreviation(),
                    ParticipantType = panelAssignment.ClientParticipantType.ParticipantTypeName,
                    Phone = panelAssignment.PrimaryPhoneNumber(),
                    Session = panelAssignment.SessionPanel.MeetingSession.SessionAbbreviation,
                    SessionStart = panelAssignment.SessionPanel.MeetingSession.StartDate,
                    SessionEnd = panelAssignment.SessionPanel.MeetingSession.EndDate,
                    ReviewerFlag = panelAssignment.ClientParticipantType.ReviewerFlag
                };

            return result;
        }

        /// <summary>
        /// Gets the meeting details header non reviewer.
        /// </summary>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <returns></returns>
        public IMeetingDetailsHeader GetMeetingDetailsHeaderNonReviewer(int sessionUserAssignmentId)
        {
            var sessionAssignment = UnitOfWork.SessionUserAssignmentRepository.GetByID(sessionUserAssignmentId);
            var result = new MeetingDetailsHeader
            {
                Attendee = sessionAssignment.User.FullName(),
                Email = sessionAssignment.User.PrimaryUserEmailAddress(),
                Meeting = sessionAssignment.MeetingSession.ClientMeeting.MeetingAbbreviation,
                MeetingStart = sessionAssignment.MeetingSession.ClientMeeting.StartDate,
                MeetingEnd = sessionAssignment.MeetingSession.ClientMeeting.EndDate,
                Panel = null,
                ParticipantType = null,
                Phone = sessionAssignment.PrimaryPhoneNumber(),
                Session = sessionAssignment.MeetingSession.SessionAbbreviation,
                SessionStart = sessionAssignment.MeetingSession.StartDate,
                SessionEnd = sessionAssignment.MeetingSession.EndDate,
                ReviewerFlag = false
            };

            return result;
        }

        /// <summary>
        /// Gets the meeting comment.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <returns></returns>
        public MeetingCommentModel GetMeetingComment(int? panelUserAssignmentId, int? sessionUserAssignmentId)
        {
            var model = new MeetingCommentModel();

            if (panelUserAssignmentId.HasValue)
            {
                model = GetMeetingCommentReviewer((int)panelUserAssignmentId);
            }
            else if (sessionUserAssignmentId.HasValue)
            {
                model = GetMeetingCommentNonReviewer((int)sessionUserAssignmentId);
            }

            return model;
        }


        /// <summary>
        /// Gets the meeting comment.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        public MeetingCommentModel GetMeetingCommentReviewer(int panelUserAssignmentId)
        {
            var model = new MeetingCommentModel();
            var userAssignment = UnitOfWork.PanelUserAssignmentRepository.GetByID(panelUserAssignmentId);
            if (userAssignment != null)
            {
                var comments = userAssignment.MeetingRegistrations.FirstOrDefault()?.MeetingRegistrationComments.FirstOrDefault();
                if (comments != null)
                {
                    string modifiedByName = comments.ModifiedBy != null ? UnitOfWork.UserRepository.GetByID(comments.ModifiedBy).FullName() : null;
                    model = new MeetingCommentModel(userAssignment.User.FirstName(), userAssignment.User.LastName(), userAssignment.GetPanelAbbreviation(),
                        comments.InternalComments, comments.MeetingRegistrationId, comments.ModifiedDate, modifiedByName );
                }
                else
                {
                     model = new MeetingCommentModel(userAssignment.User.FirstName(), userAssignment.User.LastName(), userAssignment.GetPanelAbbreviation());
                }
            }
            return model;
        }

        /// <summary>
        /// Gets the meeting comment non reviewer.
        /// </summary>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <returns></returns>
        public MeetingCommentModel GetMeetingCommentNonReviewer(int sessionUserAssignmentId)
        {
            var model = new MeetingCommentModel();
            var userAssignment = UnitOfWork.SessionUserAssignmentRepository.GetByID(sessionUserAssignmentId);
            if (userAssignment != null)
            {
                var comments = userAssignment.MeetingRegistrations.FirstOrDefault()?.MeetingRegistrationComments.FirstOrDefault();
                if (comments != null)
                {
                    string modifiedByName = comments.ModifiedBy != null ? UnitOfWork.UserRepository.GetByID(comments.ModifiedBy).FullName() : null;
                    model = new MeetingCommentModel(userAssignment.User.FirstName(), userAssignment.User.LastName(), 
                        comments.InternalComments, comments.MeetingRegistrationId, comments.ModifiedDate, modifiedByName);
                }
                else
                {
                    model = new MeetingCommentModel(userAssignment.User.FirstName(), userAssignment.User.LastName());
                }
            }
            return model;
        }

        /// <summary>
        /// Gets the meeting hotel.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId"></param>
        /// <returns></returns>
        public MeetingHotelModel GetMeetingHotel(int? panelUserAssignmentId, int? sessionUserAssignmentId)
        {
            var model = new MeetingHotelModel();

            if (panelUserAssignmentId.HasValue)
            {
                model = GetMeetingHotelReviewer((int)panelUserAssignmentId);
            } else if (sessionUserAssignmentId.HasValue)
            {
                model = GetMeetingHotelNonReviewer((int)sessionUserAssignmentId);
            }

            return model;
        }

        /// <summary>
        /// Gets the meeting hotel reviewer.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        private MeetingHotelModel GetMeetingHotelReviewer(int panelUserAssignmentId)
        {
            var model = new MeetingHotelModel();
            var o = UnitOfWork.PanelUserAssignmentRepository.GetByID(panelUserAssignmentId);

            if (o != null)
            {
                var attendance = o.MeetingRegistrations.FirstOrDefault()?.MeetingRegistrationAttendances.FirstOrDefault();
                var hotel = o.MeetingRegistrations.FirstOrDefault()?.MeetingRegistrationHotels.FirstOrDefault();
                if (attendance != null && hotel != null)
                {
                    string modifiedByName = hotel.ModifiedBy != null ? UnitOfWork.UserRepository.GetByID(hotel.ModifiedBy).FullName() : null;
                    model = new MeetingHotelModel(o.User.FirstName(), o.User.LastName(), o.GetPanelAbbreviation(),
                        attendance.AttendanceStartDate, attendance.AttendanceEndDate, hotel.HotelNotRequiredFlag, hotel.HotelCheckInDate,
                        hotel.HotelCheckOutDate, hotel.HotelId, hotel.HotelDoubleOccupancy, hotel.HotelAndFoodRequestComments, hotel.IsDataComplete,
                        hotel.CancellationDate, hotel.ParticipantModifiedDate, hotel.ModifiedDate, modifiedByName, o.SessionPanel.StartDate,
                        o.SessionPanel.EndDate, o.SessionPanel.MeetingSession.ClientMeeting.HotelId, panelUserAssignmentId, model.SubTab1Link, model.SubTab2Link, model.SubTab3Link);
                }
                else
                {
                    model = new MeetingHotelModel(o.User.FirstName(), o.User.LastName(), o.GetPanelAbbreviation(),
                        o.SessionPanel.StartDate, o.SessionPanel.EndDate, o.SessionPanel.MeetingSession.ClientMeeting.HotelId, panelUserAssignmentId);
                }
            }
            return model;
        }

        /// <summary>
        /// Gets the meeting hotel non reviewer.
        /// </summary>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <returns></returns>
        private MeetingHotelModel GetMeetingHotelNonReviewer(int sessionUserAssignmentId)
        {
            var model = new MeetingHotelModel();
            var o = UnitOfWork.SessionUserAssignmentRepository.GetByID(sessionUserAssignmentId);

            if (o != null)
            {
                var attendance = o.MeetingRegistrations.FirstOrDefault()?.MeetingRegistrationAttendances.FirstOrDefault();
                var hotel = o.MeetingRegistrations.FirstOrDefault()?.MeetingRegistrationHotels.FirstOrDefault();
                if (attendance != null && hotel != null)
                {
                    string modifiedByName = hotel.ModifiedBy != null ? UnitOfWork.UserRepository.GetByID(hotel.ModifiedBy).FullName() : null;
                    model = new MeetingHotelModel(o.User.FirstName(), o.User.LastName(),
                        attendance.AttendanceStartDate, attendance.AttendanceEndDate, hotel.HotelNotRequiredFlag, hotel.HotelCheckInDate,
                        hotel.HotelCheckOutDate, hotel.HotelId, hotel.HotelDoubleOccupancy, hotel.HotelAndFoodRequestComments, hotel.IsDataComplete,
                        hotel.CancellationDate, hotel.ParticipantModifiedDate, hotel.ModifiedDate, modifiedByName, o.MeetingSession.ClientMeeting.HotelId, sessionUserAssignmentId, model.SubTab1Link, model.SubTab2Link, model.SubTab3Link);
                }
                else
                {
                    model = new MeetingHotelModel(o.User.FirstName(), o.User.LastName(), 
                        o.MeetingSession.ClientMeeting.HotelId, sessionUserAssignmentId);
                }
            }
            return model;
        }


        /// <summary>
        /// Gets the meeting travel.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <returns></returns>
        public MeetingTravelModel GetMeetingTravel(int? panelUserAssignmentId, int? sessionUserAssignmentId)
        {
            var model = new MeetingTravelModel();

            if (panelUserAssignmentId.HasValue)
            {
                model = GetMeetingTravelReviewer((int)panelUserAssignmentId);
            }
            else if (sessionUserAssignmentId.HasValue)
            {
                model = GetMeetingTravelNonReviewer((int)sessionUserAssignmentId);
            }

            return model;
        }


        /// <summary>
        /// Gets the meeting travel.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        private MeetingTravelModel GetMeetingTravelReviewer(int panelUserAssignmentId)
        {
            var model = new MeetingTravelModel();
            var panelAssignment = UnitOfWork.PanelUserAssignmentRepository.GetByID(panelUserAssignmentId);
            if (panelAssignment != null)
            {
                model = new MeetingTravelModel(panelAssignment.User.FirstName(), panelAssignment.User.LastName(), panelAssignment.GetPanelAbbreviation(), panelUserAssignmentId);

                var hotel = panelAssignment.MeetingRegistrations.FirstOrDefault()?.MeetingRegistrationHotels.FirstOrDefault();
                var travel = panelAssignment.MeetingRegistrations.FirstOrDefault()?.MeetingRegistrationTravels.FirstOrDefault();
                if (hotel != null)
                {
                    model.PopulateHotel(hotel.CancellationDate);
                } 
                if (travel != null)
                {
                    string modifiedByName = travel.ModifiedBy != null ? UnitOfWork.UserRepository.GetByID(travel.ModifiedBy).FullName() : null;
                    model.PopulateTravel(travel.ReservationCode, travel.TravelMode?.TravelModeId, travel.Fare, travel.AgentFee, travel.AgentFee2, travel.ChangeFee,
                        travel.Ground, travel.NteAmount, travel.GsaRate, travel.NoGsa, travel.ClientApprovedAmount, travel.CancellationDate, travel.TravelRequestComments, travel.IsDataComplete,
                        travel.MeetingRegistrationId, travel.ModifiedDate, modifiedByName);
                }

                model.PopulateSublinks(model.SubTab1Link, model.SubTab2Link, model.SubTab3Link, panelUserAssignmentId, null);

            }
            return model;
        }

        /// <summary>
        /// Gets the meeting travel non reviewer.
        /// </summary>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <returns></returns>
        private MeetingTravelModel GetMeetingTravelNonReviewer(int sessionUserAssignmentId)
        {
            var model = new MeetingTravelModel();
            var sessionAssignment = UnitOfWork.SessionUserAssignmentRepository.GetByID(sessionUserAssignmentId);
            if (sessionAssignment != null)
            {
                model = new MeetingTravelModel(sessionAssignment.User.FirstName(), sessionAssignment.User.LastName(), sessionUserAssignmentId);

                var hotel = sessionAssignment.MeetingRegistrations.FirstOrDefault()?.MeetingRegistrationHotels.FirstOrDefault();
                var travel = sessionAssignment.MeetingRegistrations.FirstOrDefault()?.MeetingRegistrationTravels.FirstOrDefault();
                if (hotel != null)
                {
                    model.PopulateHotel(hotel.CancellationDate);
                }
                if (travel != null)
                {
                    string modifiedByName = travel.ModifiedBy != null ? UnitOfWork.UserRepository.GetByID(travel.ModifiedBy).FullName() : null;
                    model.PopulateTravel(travel.ReservationCode, travel.TravelMode?.TravelModeId, travel.Fare, travel.AgentFee, travel.AgentFee2, travel.ChangeFee,
                        travel.Ground, travel.NteAmount, travel.GsaRate, travel.NoGsa, travel.ClientApprovedAmount, travel.CancellationDate, travel.TravelRequestComments, travel.IsDataComplete,
                        travel.MeetingRegistrationId, travel.ModifiedDate, modifiedByName);
                }

                model.PopulateSublinks(model.SubTab1Link, model.SubTab2Link, model.SubTab3Link, null, sessionUserAssignmentId);

            }
            return model;
        }
        /// <summary>
        /// Gets carrier list.
        /// </summary>
        /// <returns></returns>
        public List<string> GetCarrierList()
        {
            var list = new List<string>();
            var carriers = UnitOfWork.CarrierRepository.GetAll();
            list = carriers.ToList().ConvertAll(x => x.Name);
            return list;
        }
        /// <summary>
        /// Gets airport list.
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetAirportList()
        {
            var list = new List<KeyValuePair<string, string>>();
            var airports = UnitOfWork.AirportRepository.GetAll();
            list = airports.OrderBy(y => y.Description).ToList().ConvertAll(x => new KeyValuePair<string, string>(
                x.Code, string.Format("{0}, {1}", x.Description, x.Code)));
            return list;
        }

        /// <summary>
        /// Saves the hotel details.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="attendanceStartDate">The attendance start date.</param>
        /// <param name="attendanceEndDate">The attendance end date.</param>
        /// <param name="hotelNotRequiredFlag">if set to <c>true</c> hotel is not required.</param>
        /// <param name="checkInDate">The check in date.</param>
        /// <param name="checkOutDate">The check out date.</param>
        /// <param name="hotelId">The hotel identifier.</param>
        /// <param name="doubleOccupancy">if set to <c>true</c> [double occupancy].</param>
        /// <param name="hotelAndFoodRequestComments">The hotel and food request comments.</param>
        /// <param name="cancellationDate">The cancellation date.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedByName">Name of the modified by.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public void SaveHotelDetails(int? panelUserAssignmentId, int? sessionUserAssignmentId, DateTime? attendanceStartDate, DateTime? attendanceEndDate, bool hotelNotRequiredFlag,
            DateTime? checkInDate, DateTime? checkOutDate, int? hotelId, bool doubleOccupancy, string hotelAndFoodRequestComments,
            DateTime? cancellationDate, DateTime? modifiedDate, string modifiedByName, int userId)
        {
            UpsertMeetingManagementHotel(panelUserAssignmentId, sessionUserAssignmentId, attendanceStartDate, attendanceEndDate, hotelNotRequiredFlag, checkInDate, checkOutDate, hotelId, doubleOccupancy, hotelAndFoodRequestComments, 
                    cancellationDate, modifiedDate, modifiedByName, userId);
            UnitOfWork.Save();
        }

        /// <summary>
        /// Saves the non reviewer assignments.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="assignments">The assignments.</param>
        /// <param name="assignmentRemovals">The assignment removals.</param>
        /// <returns></returns>
        public bool SaveNonReviewerAssignments(int clientMeetingId, int userId, List<int> assignments, List<int> assignmentRemovals, int currUser)
        {
            try
            {
                var currentAssignments = UnitOfWork.SessionUserAssignmentRepository.Select()
                    .Where(x => x.UserId == userId && x.MeetingSession.ClientMeetingId == clientMeetingId).ToList();

                if (assignmentRemovals != null)
                {
                    RemoveNonReviewerAssignments(currentAssignments, assignmentRemovals, currUser);
                }
                if (assignments != null)
                {
                    AddNonReviewerAssignments(currentAssignments, assignments, userId, currUser);
                }
                UnitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Removes the non reviewer assignments.
        /// </summary>
        /// <param name="currentAssignments">The current assignments.</param>
        /// <param name="assignmentRemovals">The assignment removals.</param>
        private void RemoveNonReviewerAssignments(List<SessionUserAssignment> currentAssignments, List<int> assignmentRemovals, int currUser)
        {
            foreach (var item in currentAssignments)
            {
                if (assignmentRemovals.Contains(item.MeetingSessionId)) {
                    Helper.UpdateDeletedFields(item, currUser);
                    UnitOfWork.SessionUserAssignmentRepository.Delete(item);
                }
            }
        }

        /// <summary>
        /// Adds the non reviewer assignments.
        /// </summary>
        /// <param name="currentAssignments">The current assignments.</param>
        /// <param name="assignments">The assignments.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="currUser">The current system user.</param>
        private void AddNonReviewerAssignments(List<SessionUserAssignment> currentAssignments, List<int> assignments, int userId, int currUser)
        {
            foreach (var item in assignments)
            {
                bool isCurrentAssignment = currentAssignments.Where(x => x.MeetingSessionId == item).Count() > 0;
                if (!isCurrentAssignment)
                {
                    SessionUserAssignment newAssignment = new SessionUserAssignment() { MeetingSessionId = item, UserId = userId, CreatedDate = GlobalProperties.P2rmisDateTimeNow, CreatedBy = currUser };
                    UnitOfWork.SessionUserAssignmentRepository.Add(newAssignment);
                }
            }
        }

        /// <summary>
        /// Saves the travel details.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment id.</param>
        /// <param name="travelModeId">The travel mode identifier.</param>
        /// <param name="fare">The fare.</param>
        /// <param name="agentFee">The agent fee.</param>
        /// <param name="agentFee2">The agent fee #2.</param>
        /// <param name="changeFee">The change fee.</param>
        /// <param name="ground">if set to <c>true</c> [ground].</param>
        /// <param name="nteAmount">The nte amount.</param>
        /// <param name="gsaRate">The gsa rate.</param>
        /// <param name="noGsa">if set to <c>true</c> [no gsa].</param>
        /// <param name="clientApprovedAmount">The client approved amount.</param>
        /// <param name="cancelledDate">The cancelled date.</param>
        /// <param name="travelRequestComments">The travel request comments.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedByName">Name of the modified by.</param>
        /// <param name="reservation"></param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public void SaveTravelDetails(int? panelUserAssignmentId, int? sessionUserAssignmentId, int? travelModeId, decimal? fare, decimal? agentFee, decimal? agentFee2, decimal? changeFee, bool ground, decimal? nteAmount, decimal? gsaRate,
                    bool noGsa, decimal? clientApprovedAmount, DateTime? cancelledDate, string travelRequestComments, DateTime? modifiedDate, string modifiedByName, string reservation, int userId)
        {
            UpsertMeetingManagementTravel(panelUserAssignmentId, sessionUserAssignmentId, travelModeId, fare, agentFee, agentFee2, changeFee, ground, nteAmount, gsaRate, noGsa, 
                clientApprovedAmount, cancelledDate, travelRequestComments, modifiedDate, modifiedByName, reservation, userId);
            UnitOfWork.Save();
        }

        /// <summary>
        /// Marks the hotel meeting details as complete.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public bool MarkHotelDataComplete(int? panelUserAssignmentId, int? sessionUserAssignmentId, int userId)
        {
            bool result = false;
            var reg = UpsertMeetingRegistration(panelUserAssignmentId, sessionUserAssignmentId, userId);

            if (reg.MeetingRegistrationHotels.Count > 0)
            {
                var regHotel = reg.MeetingRegistrationHotels.First();
                regHotel.IsDataComplete = true;
                Helper.UpdateModifiedFields(regHotel, userId);
                UnitOfWork.Save();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Marks the travel meeting details as complete.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public bool MarkTravelDataComplete(int? panelUserAssignmentId, int? sessionUserAssignmentId, int userId)
        {
            bool result = false;
            var reg = UpsertMeetingRegistration(panelUserAssignmentId, sessionUserAssignmentId, userId);

            if (reg.MeetingRegistrationTravels.Count > 0)
            {
                var regTravel = reg.MeetingRegistrationTravels.First();
                regTravel.IsDataComplete = true;
                Helper.UpdateModifiedFields(regTravel, userId);
                UnitOfWork.Save();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Saves the comments details.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <param name="internalComments">The internal comments.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public bool SaveCommentsDetails(int? panelUserAssignmentId, int? sessionUserAssignmentId, string internalComments, int userId)
        {
            bool result = false;

            UpsertMeetingManagementComment(panelUserAssignmentId, sessionUserAssignmentId, internalComments, userId);
            UnitOfWork.Save();

            result = true;

            return result;
        }
        /// <summary>
        /// Gets flights.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        public List<TravelFlightModel> GetFlights(int? panelUserAssignmentId, int? sessionUserAssignmentId)
        {
            var flights = new List<TravelFlightModel>();

            IEnumerable<MeetingRegistrationTravelFlight> models = null;

            if (panelUserAssignmentId.HasValue)
            {
                models = UnitOfWork.MeetingRegistrationTravelFlightRepository.Get(
                    x => x.MeetingRegistrationTravel.MeetingRegistration.PanelUserAssignmentId == panelUserAssignmentId)
                    .OrderBy(y => y.DepartureDate);
            } else
            {
                models = UnitOfWork.MeetingRegistrationTravelFlightRepository.Get(
                    x => x.MeetingRegistrationTravel.MeetingRegistration.SessionUserAssignmentId == sessionUserAssignmentId)
                    .OrderBy(y => y.DepartureDate);
            }

            flights = models.ToList().ConvertAll(x => new TravelFlightModel(
                panelUserAssignmentId, sessionUserAssignmentId, x.MeetingRegistrationTravelFlightId,
                x.CarrierName, x.FlightNumber, x.DepartureCity, (DateTime)x.DepartureDate,
                x.ArrivalCity, (DateTime)x.ArrivalDate, x.ModifiedByUser?.UserLogin, (DateTime)x.ModifiedDate));
            return flights;
        }
        /// <summary>
        /// Saves travel flight.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="flightId">The meeting registration travel flight identifier.</param>
        /// <param name="carrierName">The carrier name.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="departureCity">The departure city.</param>
        /// <param name="departureDate">The departure date.</param>
        /// <param name="arrivalCity">The arrival city.</param>
        /// <param name="arrivalDate">The arrival date.</param>
        /// <param name="userId">The logged-in user identifier.</param>
        /// <returns></returns>
        public int? SaveTravelFlight(int? panelUserAssignmentId, int? sessionUserAssignmentId, int? flightId,
            string carrierName, string flightNumber, 
            string departureCity, DateTime departureDate,
            string arrivalCity, DateTime arrivalDate, int userId)
        {
            flightId = UpsertMeetingManagementTravelFlight(panelUserAssignmentId, sessionUserAssignmentId, flightId,
                    carrierName, flightNumber, departureCity, departureDate, arrivalCity, arrivalDate, userId, true);
            return flightId;
        }
        /// <summary>
        /// Saves travel flights
        /// </summary>
        /// <param name="flights">The flights.</param>
        /// <param name="userId">The user identifier.</param>
        public void SaveTravelFlights(List<TravelFlightModel> flights, int userId)
        {
            for (var i = 0; i < flights.Count; i++)
            {
                UpsertMeetingManagementTravelFlight(flights[i].PanelUserAssignmentId, flights[i].SessionUserAssignmentId,
                    flights[i].FlightId, flights[i].CarrierName, flights[i].FlightNumber,
                    flights[i].DepartureCity, flights[i].DepartureDate,
                    flights[i].ArrivalCity, flights[i].ArrivalDate, userId, false);
            }
            UnitOfWork.Save();
        }

        /// <summary>
        /// Deletes travel flights.
        /// </summary>
        /// <param name="flightId">The flight identifier.</param>
        /// <param name="userId">The logged-in user identifier.</param>
        public void DeleteTravelFlight(int flightId, int userId)
        {
            var flight = UnitOfWork.MeetingRegistrationTravelFlightRepository.GetByID(flightId);
            UnitOfWork.MeetingRegistrationTravelFlightRepository.DeleteFlight(flight, userId);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Imports the travel flights.
        /// </summary>
        /// <param name="travelFlights">The travel flights.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ICollection<KeyValuePair<int, SaveTravelFlightStatus>> ImportTravelFlights(IList<TravelFlightImportModel> travelFlights, int userId)
        {
            var statusList = new List<KeyValuePair<int, SaveTravelFlightStatus>>();
            var panelUserAssignmentIds = new List<int>();
            for (var i = 0; i < travelFlights.Count; i++)
            {
                var tf = travelFlights[i];
                PanelUserAssignment panelUserAssignment = default(PanelUserAssignment);
                if (tf.PanelUserAssignmentId == -1)
                {
                    statusList.Add(new KeyValuePair<int, SaveTravelFlightStatus>(i + 1, SaveTravelFlightStatus.InvalidPanelUserAssignmentId));
                } else
                {
                    panelUserAssignment = UnitOfWork.PanelUserAssignmentRepository.GetByID((int)tf.PanelUserAssignmentId);
                    if (panelUserAssignment == null)
                        statusList.Add(new KeyValuePair<int, SaveTravelFlightStatus>(i + 1, SaveTravelFlightStatus.InvalidPanelUserAssignmentId));
                }
                if (tf.DepartureDate == null)
                    statusList.Add(new KeyValuePair<int, SaveTravelFlightStatus>(i + 1, SaveTravelFlightStatus.InvalidDepartureDate));
                if (tf.ArrivalDate == null)
                    statusList.Add(new KeyValuePair<int, SaveTravelFlightStatus>(i + 1, SaveTravelFlightStatus.InvalidArrivalDate));
                if (statusList.Count == 0)
                {
                    panelUserAssignment = UnitOfWork.PanelUserAssignmentRepository.GetByID((int)tf.PanelUserAssignmentId);
                    if (panelUserAssignment == null)
                        statusList.Add(new KeyValuePair<int, SaveTravelFlightStatus>(i + 1, SaveTravelFlightStatus.InvalidPanelUserAssignmentId));
                    if (panelUserAssignment.MeetingRegistrations.Count == 0)
                    {
                        var meetingRegistration = UnitOfWork.MeetingRegistrationRepository.AddDefaultRegistration((int)tf.PanelUserAssignmentId, null, userId);
                        panelUserAssignment.MeetingRegistrations.Add(meetingRegistration);
                    }
                    var reg = panelUserAssignment.MeetingRegistrations.First();
                    if (reg.MeetingRegistrationTravels.Count == 0)
                    {
                        var meetingRegistrationTravel = UnitOfWork.MeetingRegistrationTravelRepository.AddDefaultTravel(userId);
                        reg.MeetingRegistrationTravels.Add(meetingRegistrationTravel);
                    }

                    var travel = reg.MeetingRegistrationTravels.First();
                    UnitOfWork.MeetingRegistrationTravelRepository.UpdateReservationCode(travel, tf.ReservationCode, userId);
                    travel.Fare = tf.Fare;
                    // Delete any existing flights with the same PanelUserAssignmentId
                    if (!panelUserAssignmentIds.Contains((int)tf.PanelUserAssignmentId))
                    {
                        var flightsToDelete = travel.MeetingRegistrationTravelFlights.ToList();
                        foreach (var flight in flightsToDelete)
                        {
                            UnitOfWork.MeetingRegistrationTravelFlightRepository.DeleteFlight(flight, userId);
                        }
                        panelUserAssignmentIds.Add((int)tf.PanelUserAssignmentId);
                    }
                    // Add flights
                    var meetingRegistrationTravelFlight = UnitOfWork.MeetingRegistrationTravelFlightRepository.AddDefaultFlight(tf.CarrierName, tf.FlightNumber,
                        tf.DepartureCity, tf.DepartureDate, tf.ArrivalCity, tf.ArrivalDate, userId);
                    travel.MeetingRegistrationTravelFlights.Add(meetingRegistrationTravelFlight);
                }
            }
            if (statusList.Count == 0)
                UnitOfWork.Save();
            return statusList;
        }

        /// <summary>
        /// Retrieves a container for the Meeting dropdown on the Session Fee Schedule view.
        /// </summary>
        /// <param name="clientId">ClientId entity identifier</param>
        /// <param name="year">Selected year value</param>
        /// <returns>Container of IFeeScheduleModel model for the given MeetingSession entity identifier</returns>
        public virtual List<GenericListEntry<int, string>> RetrieveMeetingList(int programYearId, bool onSiteOnly = false)
        {
            string name = FullName(nameof(MeetingManagementService), nameof(RetrieveMeetingList));
            ValidateInt(programYearId, name, nameof(programYearId));

            var meetingList = UnitOfWork.ProgramYearRepository.Select()
                                 .Where(x => x.ProgramYearId == programYearId).SelectMany(x => x.ProgramMeetings)
                                 .Select(x => x.ClientMeeting);
            if (onSiteOnly)
            {
                meetingList = meetingList.Where(x => x.MeetingTypeId == MeetingType.Onsite);
            }

            var list = meetingList.Where(x => x!= null).Select(x => new GenericListEntry<int, string>
            {
                Index = x.ClientMeetingId,
                Value = x.MeetingAbbreviation
            }).Distinct().OrderBy(x => x.Value).ToList();
            return list;
        }

        /// <summary>
        /// Retrieves the hotel and travel meeting list. Overloading to take a string FY instead of the program year id.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientList">The client list.</param>
        /// <param name="onSiteOnly">if set to <c>true</c> [on site only].</param>
        /// <returns></returns>
        public virtual List<GenericListEntry<int, string>> RetrieveMeetingList(string fiscalYear, List<int> clientList, bool onSiteOnly = false)
        {
            string name = FullName(nameof(MeetingManagementService), nameof(RetrieveMeetingList));

            var meetingList = UnitOfWork.ProgramYearRepository.Select()
                                 .Where(x => x.Year == fiscalYear && clientList.Contains(x.ClientProgram.ClientId)).SelectMany(x => x.ProgramMeetings)
                                 .Select(x => x.ClientMeeting);
            if (onSiteOnly)
            {
                meetingList = meetingList.Where(x => x.MeetingTypeId == MeetingType.Onsite);
            }

            var list = meetingList.Where(x => x!= null).Select(x => new GenericListEntry<int, string>
            {
                Index = x.ClientMeetingId,
                Value = x.MeetingAbbreviation
            }).Distinct().OrderBy(x => x.Value).ToList();
            return list;
        }

        /// <summary>
        /// Retrieves the hoteland travel program list.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="onSiteOnly">if set to <c>true</c> [on site only].</param>
        /// <returns></returns>
        public virtual List<GenericListEntry<int, string>> RetrieveProgramList(int clientMeetingId, bool onSiteOnly = false)
        {
            string name = FullName(nameof(MeetingManagementService), nameof(RetrieveProgramList));
            ValidateInt(clientMeetingId, name, nameof(clientMeetingId));

            var programList = UnitOfWork.ProgramYearRepository.Select()
                                 .Where(x => x.ProgramMeetings.Any(y => y.ClientMeetingId == clientMeetingId));
            if (onSiteOnly)
            {
                programList = programList.Where(x => x.ProgramMeetings.Any(y => y.ClientMeeting.MeetingTypeId == MeetingType.Onsite));
            }

            var list = programList.Select(x => new GenericListEntry<int, string>
            {
                Index = x.ProgramYearId,
                Value = x.ClientProgram.ProgramAbbreviation
            }).Distinct().OrderBy(x => x.Value).ToList();
            return list;
        }

        /// <summary>
        /// Overload that takes fiscal year string and user's client list.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientList">The client list.</param>
        /// <returns></returns>
        public virtual List<GenericListEntry<int, string>> RetrieveProgramList(string fiscalYear, List<int> clientList)
        {
            string name = FullName(nameof(MeetingManagementService), nameof(RetrieveProgramList));

            var programList = UnitOfWork.ProgramYearRepository.Select()
                .Where(x => x.Year == fiscalYear && clientList.Contains(x.ClientProgram.ClientId)).Select(x => new GenericListEntry<int, string>
                {
                    Index = x.ProgramYearId,
                    Value = x.ClientProgram.ProgramAbbreviation
                }).OrderBy(x => x.Value).ToList();
            return programList;
        }

        /// <summary>
        /// Adds or modifies the meeting management hotel.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="attendanceStartDate">The attendance start date.</param>
        /// <param name="attendanceEndDate">The attendance end date.</param>
        /// <param name="hotelNotRequiredFlag">if set to <c>true</c> hotel is not required.</param>
        /// <param name="checkInDate">The check in date.</param>
        /// <param name="checkOutDate">The check out date.</param>
        /// <param name="hotelId">The hotel identifier.</param>
        /// <param name="doubleOccupancy">if set to <c>true</c> [double occupancy].</param>
        /// <param name="hotelAndFoodRequestComments">The hotel and food request comments.</param>
        /// <param name="cancellationDate">The cancellation date.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedByName">Name of the modified by.</param>
        /// <param name="userId">The user identifier.</param>
        internal virtual void UpsertMeetingManagementHotel(int? panelUserAssignmentId, int? sessionUserAssignmentId , DateTime? attendanceStartDate, DateTime? attendanceEndDate, bool hotelNotRequiredFlag,
            DateTime? checkInDate, DateTime? checkOutDate, int? hotelId, bool doubleOccupancy, string hotelAndFoodRequestComments,
            DateTime? cancellationDate, DateTime? modifiedDate, string modifiedByName, int userId)
        {
            var reg = UpsertMeetingRegistration(panelUserAssignmentId, sessionUserAssignmentId, userId);
            // Add or modify hotel
            if (reg.MeetingRegistrationAttendances.Count == 0)
            {
                var meetingRegistrationAttendance = new MeetingRegistrationAttendance();
                Helper.UpdateCreatedFields(meetingRegistrationAttendance, userId);
                reg.MeetingRegistrationAttendances.Add(meetingRegistrationAttendance);
            }
            var regAttendance = reg.MeetingRegistrationAttendances.First();
            regAttendance.Populate(attendanceStartDate, attendanceEndDate);
            Helper.UpdateModifiedFields(regAttendance, userId);
            // Add or modify hotel
            if (reg.MeetingRegistrationHotels.Count == 0)
            {
                var meetingRegistrationHotel = new MeetingRegistrationHotel();
                Helper.UpdateCreatedFields(meetingRegistrationHotel, userId);
                reg.MeetingRegistrationHotels.Add(meetingRegistrationHotel);
            }
            var regHotel = reg.MeetingRegistrationHotels.First();
            regHotel.Populate(checkInDate, checkOutDate, hotelNotRequiredFlag, doubleOccupancy, hotelId, hotelAndFoodRequestComments, cancellationDate);
            Helper.UpdateModifiedFields(regHotel, userId);

            // mark as submitted when admin saves hotel record
            reg.RegistrationSubmittedDate = GlobalProperties.P2rmisDateTimeNow;
            reg.RegistrationSubmittedFlag = true;
        }

        /// <summary>
        /// Upserts the meeting management travel.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <param name="travelModeId">The travel mode identifier.</param>
        /// <param name="fare">The fare.</param>
        /// <param name="agentFee">The agent fee.</param>
        /// <param name="agentFee2">The 2nd agent fee.</param>
        /// <param name="changeFee">The change fee.</param>
        /// <param name="ground">if set to <c>true</c> [ground].</param>
        /// <param name="nteAmount">The nte amount.</param>
        /// <param name="gsaRate">The gsa rate.</param>
        /// <param name="noGsa">if set to <c>true</c> [no gsa].</param>
        /// <param name="clientApprovedAmount">The client approved amount.</param>
        /// <param name="cancelledDate">The cancelled date.</param>
        /// <param name="travelRequestComments">The travel request comments.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedByName">Name of the modified by.</param>
        /// <param name="reservation">The reservation.</param>
        /// <param name="userId">The user identifier.</param>
        internal virtual void UpsertMeetingManagementTravel(int? panelUserAssignmentId, int? sessionUserAssignmentId, int? travelModeId, decimal? fare, decimal? agentFee, decimal? agentFee2, decimal? changeFee, bool ground, decimal? nteAmount, decimal? gsaRate,
                    bool noGsa, decimal? clientApprovedAmount, DateTime? cancelledDate, string travelRequestComments, DateTime? modifiedDate, string modifiedByName, string reservation, int userId)
        {
            var reg = UpsertMeetingRegistration(panelUserAssignmentId, sessionUserAssignmentId, userId);
            // Add or modify travel
            if (reg.MeetingRegistrationTravels.Count == 0)
            {
                var meetingRegistrationTravel = new MeetingRegistrationTravel();
                Helper.UpdateCreatedFields(meetingRegistrationTravel, userId);
                reg.MeetingRegistrationTravels.Add(meetingRegistrationTravel);
            }
            var regTravel = reg.MeetingRegistrationTravels.First();
            regTravel.Populate(travelModeId, (decimal?)fare, (decimal?)agentFee, (decimal?)agentFee2, (decimal?)changeFee, ground, (decimal?)nteAmount, (decimal?)gsaRate, noGsa, (decimal?)clientApprovedAmount, 
                (DateTime?)cancelledDate, travelRequestComments, reservation);
            Helper.UpdateModifiedFields(regTravel, userId);
        }
        /// <summary>
        /// Updates or inserts meeting management travle flight.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="meetingRegistrationTravelFlightId">The flight identifier.</param>
        /// <param name="carrierName">The carrier name.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="departureCity">The departure city.</param>
        /// <param name="departureTime"></param>
        /// <param name="arrivalCity"></param>
        /// <param name="arrivalTime"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal virtual int? UpsertMeetingManagementTravelFlight(int? panelUserAssignmentId, int? sessionUserAssignmentId,
            int? meetingRegistrationTravelFlightId, string carrierName, string flightNumber,
            string departureCity, DateTime departureTime, string arrivalCity, DateTime arrivalTime,
            int userId, bool saveUofW)
        {
            var flight = default(MeetingRegistrationTravelFlight);
            if (meetingRegistrationTravelFlightId != null)
            {
                // Modifiy
                flight = UnitOfWork.MeetingRegistrationTravelFlightRepository.GetByID(meetingRegistrationTravelFlightId);
                UnitOfWork.MeetingRegistrationTravelFlightRepository.UpdateFlight(flight, carrierName, flightNumber,
                    departureCity, departureTime, arrivalCity, arrivalTime, userId);
            }
            else
            {
                var reg = UpsertMeetingRegistration(panelUserAssignmentId, sessionUserAssignmentId, userId);
                // Add or modify travel
                if (reg.MeetingRegistrationTravels.Count == 0)
                {
                    var meetingRegistrationTravel = new MeetingRegistrationTravel();
                    Helper.UpdateCreatedFields(meetingRegistrationTravel, userId);
                    Helper.UpdateModifiedFields(meetingRegistrationTravel, userId);
                    reg.MeetingRegistrationTravels.Add(meetingRegistrationTravel);
                }
                var regTravel = reg.MeetingRegistrationTravels.First();
                // Add
                flight = UnitOfWork.MeetingRegistrationTravelFlightRepository
                    .AddDefaultFlight(carrierName, flightNumber, departureCity, departureTime,
                    arrivalCity, arrivalTime, userId);
                regTravel.MeetingRegistrationTravelFlights.Add(flight);
                meetingRegistrationTravelFlightId = flight.MeetingRegistrationTravelFlightId;
            }
            if (saveUofW)
                UnitOfWork.Save();
            return flight.MeetingRegistrationTravelFlightId;
        }

        /// <summary>
        /// Upserts the meeting management comment.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <param name="internalComments">The internal comments.</param>
        /// <param name="userId">The user identifier.</param>
        internal virtual void UpsertMeetingManagementComment(int? panelUserAssignmentId, int? sessionUserAssignmentId, string internalComments, int userId)
        {
            var reg = UpsertMeetingRegistration(panelUserAssignmentId, sessionUserAssignmentId, userId);
            // Add or modify comment
            if (reg.MeetingRegistrationComments.Count == 0)
            {
                var meetingRegistrationComment = new MeetingRegistrationComment();
                Helper.UpdateCreatedFields(meetingRegistrationComment, userId);
                reg.MeetingRegistrationComments.Add(meetingRegistrationComment);
            }
            var regComment = reg.MeetingRegistrationComments.First();
            regComment.Populate(internalComments);
            Helper.UpdateModifiedFields(regComment, userId);            
        }

        /// <summary>
        /// Upserts the meeting registration.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private MeetingRegistration UpsertMeetingRegistration(int panelUserAssignmentId, int userId)
        {
            return UpsertMeetingRegistration(panelUserAssignmentId, null, userId);
        }
        /// <summary>
        /// Updates or inserts meeting registration.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private MeetingRegistration UpsertMeetingRegistration(int? panelUserAssignmentId, 
            int? sessionUserAssignmentId, int userId)
        {
            var reg = default(MeetingRegistration);
            if (panelUserAssignmentId != null)
            {
                var panelUserAssignment = UnitOfWork.PanelUserAssignmentRepository.GetByID(panelUserAssignmentId);
                if (panelUserAssignment.MeetingRegistrations.Count == 0)
                {
                    var meetingRegistration = UnitOfWork.MeetingRegistrationRepository.AddDefaultRegistration(panelUserAssignmentId, sessionUserAssignmentId, userId);
                    panelUserAssignment.MeetingRegistrations.Add(meetingRegistration);
                }
                reg = panelUserAssignment.MeetingRegistrations.First();
            }
            else
            {
                var sessionUserAssignment = UnitOfWork.SessionUserAssignmentRepository.GetByID(sessionUserAssignmentId);
                if (sessionUserAssignment.MeetingRegistrations.Count == 0)
                {
                    var meetingRegistration = UnitOfWork.MeetingRegistrationRepository.AddDefaultRegistration(null, (int)sessionUserAssignmentId, userId);
                    sessionUserAssignment.MeetingRegistrations.Add(meetingRegistration);
                }
                reg = sessionUserAssignment.MeetingRegistrations.First();
            }
            return reg;
        }
        /// <summary>
        /// Retrieves the Meeting Registration Hotel entity by meetingRegistrationId
        /// </summary>
        /// <param name="meetingRegistrationId">MeetingRegistrationId entity identifier</param>
        /// <returns>MeetingRegistrationHotel entity</returns>
        internal virtual MeetingRegistrationHotel GetMeetingRegistrationHotelById(int meetingRegistrationId)
        {
            return UnitOfWork.MeetingRegistrationHotelRepository.Get(x => x.MeetingRegistrationId == meetingRegistrationId).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the Meeting Registration Travel entity by meetingRegistrationId
        /// </summary>
        /// <param name="meetingRegistrationId">MeetingRegistrationId entity identifier</param>
        /// <returns>MeetingRegistrationHotel entity</returns>
        internal virtual MeetingRegistrationTravel GetMeetingTravelHotelById(int meetingRegistrationId)
        {
            return UnitOfWork.MeetingRegistrationTravelRepository.Get(x => x.MeetingRegistrationId == meetingRegistrationId).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the Meeting Registration Attendance entity by meetingRegistrationId
        /// </summary>
        /// <param name="meetingRegistrationId">MeetingRegistrationId entity identifier</param>
        /// <returns>MeetingRegistrationAttendance entity</returns>
        internal virtual MeetingRegistrationAttendance GetMeetingAttendanceHotelById(int meetingRegistrationId)
        {
            return UnitOfWork.MeetingRegistrationAttendanceRepository.Get(x => x.MeetingRegistrationId == meetingRegistrationId).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the Meeting Registration Attendance entity by meetingRegistrationId
        /// </summary>
        /// <param name="meetingRegistrationId">MeetingRegistrationId entity identifier</param>
        /// <returns>MeetingRegistrationAttendance entity</returns>
        internal virtual MeetingRegistrationComment GetMeetingAttendanceCommentById(int meetingRegistrationId)
        {
            return UnitOfWork.MeetingRegistrationCommentRepository.Get(meetingRegistrationId);
        }
    }
}
