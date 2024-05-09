using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.LibraryService;
using Sra.P2rmis.Bll.Setup;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Bll.MeetingManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.UI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.MeetingManagement;
using Sra.P2rmis.Web.ViewModels.MeetingManagement;
using Newtonsoft.Json;
using Sra.P2rmis.WebModels.HotelAndTravel;
using Sra.P2rmis.Dal;
using System.Web;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using System.IO;
using Sra.P2rmis.CrossCuttingServices.OpenXmlServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.WebModels.MeetingManagement;

namespace Sra.P2rmis.Web.Controllers
{
    public class MeetingManagementController : MeetingManagementBaseController
    {
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingManagementController"/> class.
        /// </summary>
        public MeetingManagementController() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupController"/> class.
        /// </summary>
        /// <param name="setupService">The setup service.</param>
        /// <param name="criteriaService">The criteria service.</param>
        /// <param name="userProfileManagementService">The user profile management service.</param>
        /// <param name="lookupService">The lookup service.</param>
        /// <param name="libraryService">The library service.</param>
        public MeetingManagementController(ISetupService setupService, ICriteriaService criteriaService,
            IUserProfileManagementService userProfileManagementService, ILookupService lookupService, ILibraryService libraryService,
            IMeetingManagementService meetingManagementService)
        {
            theSetupService = setupService;
            theCriteriaService = criteriaService;
            theUserProfileManagementService = userProfileManagementService;
            theLookupService = lookupService;
            theLibraryService = libraryService;
            theMeetingManagementService = meetingManagementService;
        }
        #endregion

        /// <summary>
        /// Meeting Management Hotel and Travel page.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult Index(string fiscalYear)
        {
            MeetingManagementViewModel vm = new MeetingManagementViewModel();
            try
            {
                int userId = GetUserId();
                var yearResults = new List<int>();
                var clientResults = new List<int>();
                var clients = GetUsersClientList();

                foreach(var client in clients)
                {
                    var list = theSetupService.RetrieveClientProgramYears(client);
                    var results = list.ModelList.Where(x => x.IsActive).ToList();
                    foreach(var result in results)
                    {
                        yearResults.Add(Int32.Parse(result.Year));
                        clientResults.Add(result.ClientProgramId);
                    }
                }
                vm.ActiveYears = yearResults.OrderByDescending(x => x).Distinct().ToList();
                vm.ClientList = clientResults.OrderByDescending(x => x).Distinct().ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return View(vm);
        }
        /// <summary>
        /// Gets the program years json.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult GetProgramYearsJson(string fiscalYear)
        {
            List<MeetingManagementViewModel> results = new List<MeetingManagementViewModel>();
            var clients = GetUsersClientList();
            ArrayList programResults = new ArrayList();
            try
            {
                    var list = theMeetingManagementService.RetrieveProgramList(fiscalYear, GetUsersClientList());
                    programResults.AddRange(list);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(programResults, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the meeting types.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult GetMeetings(int programYearId)
        {
            List<GenericListEntry<int, string>> results = new List<GenericListEntry<int, string>>();
            try
            {
                // make sure only onsite meetings are pulled.
                var list = theMeetingManagementService.RetrieveMeetingList(programYearId, true).ToList();
                results = list;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the programs by meeting.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult GetProgramsByMeeting(int clientMeetingId)
        {
            List<GenericListEntry<int, string>> results = new List<GenericListEntry<int, string>>();
            try
            {
                // make sure only onsite meetings are pulled.
                var list = theMeetingManagementService.RetrieveProgramList(clientMeetingId, true).ToList();
                results = list;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets all meetings from Fiscal Year json.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="onSiteOnly">if set to <c>true</c> [on site only].</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult GetMeetingsFromYearJson(string fiscalYear, bool onSiteOnly)
        {
            List<GenericListEntry<int, string>> results = new List<GenericListEntry<int, string>>();
            try
            {
                // make sure only onsite meetings are pulled.
                var list = theMeetingManagementService.RetrieveMeetingList(fiscalYear, GetUsersClientList(), onSiteOnly).ToList();
                results = list;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets all sessions from FY and Meeting json.
        /// </summary>
        /// <param name="meetingId">The meeting identifier.</param>
        /// <param name="programIdr">The program idr.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult GetSessionsByMeetingProgramJson(int? clientMeetingId, int? programYearId)
        {
            List<KeyValuePair<int, string>> results = new List<KeyValuePair<int, string>>();
            List<IListEntry> totals = new List<IListEntry>();

            var clients = GetUsersClientList();
            try
            {
                if (clientMeetingId.HasValue || programYearId.HasValue)
                {
                    var list = theLookupService.ListSessionsByMeetingProgram(clientMeetingId, programYearId).ModelList.ToList();
                    var result = list.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));

                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return null;
        }
        /// </summary>
        /// <param name="programYearId">Program Year Id selected </param>
        /// <returns>List of panels in Json format</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult GetPanelsByMeetingProgramJson(int? clientMeetingId, int? programYearId)
        {
            List<KeyValuePair<int, string>> results = new List<KeyValuePair<int, string>>();
            List<IListEntry> totals = new List<IListEntry>();

            var clients = GetUsersClientList();
            try {
                if  (clientMeetingId.HasValue || programYearId.HasValue)
                {
                    var list = theLookupService.ListPanelsByMeetingProgram(clientMeetingId, programYearId).ModelList.ToList();
                    var result = list.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return null;
        }
        /// <summary>
        /// Gets the fee schedule sessions json.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult GetSessionsJson(int clientMeetingId)
        {
            List<KeyValuePair<int, string>> results = new List<KeyValuePair<int, string>>();
            try
            {
                var list = theLookupService.ListMeetingSessions(clientMeetingId).ModelList.ToList();
                results = list.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the programs with assignments json.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult GetSessionsWithAssignmentsJson(int clientMeetingId, int userId)
        {
            MeetingAssignmentModel model = new MeetingAssignmentModel();
            try
            {
                var list = theLookupService.ListMeetingSessions(clientMeetingId).ModelList.ToList();
                model.Sessions = list.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
                model.Assignments = theMeetingManagementService.GetNonReviewerAssignedSessions(clientMeetingId, userId);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Retrieve the panels for the selected program and fiscal years.
        /// </summary>
        /// <param name="programYearId">Program Year Id selected </param>
        /// <returns>List of panels in Json format</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult GetPanelsJson(int sessionId, int? programYearId)
        {
            var list = theLookupService.ListSessionPanels(sessionId, programYearId).ModelList.ToList();
            var result = list.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the registration attendance list.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="meetingId">The meeting identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult GetRegistrationAttendanceJsonString(string firstName, string lastName, string fiscalYear, int? programYearId, int? meetingId,
            int? sessionId, int? panelId, bool? nonReviewerOnly)
        {
            var results = new List<MeetingAttendanceViewModel>();
            try
            {
                var clients = GetUsersClientList();
                var list = theMeetingManagementService.GetMeetingAttendanceList(clients, firstName, lastName, fiscalYear, programYearId, meetingId,
                    sessionId, panelId);
                results = list.ConvertAll(x => new MeetingAttendanceViewModel(x.ReviewerUserId, x.FirstName, x.LastName, x.RestrictedAssignedFlag,
                    x.ParticipationMethod, string.Join(", ", x.Programs), x.FiscalYear, x.PanelAbbreviation, x.MeetingAbbreviation, x.SessionName, x.HotelModifiedByUserId,
                    x.TravelModifiedByUserId, x.InternalComments, x.MeetingRegistrationId, x.PanelUserAssignmentId, x.SessionUserAssignmentId, x.ParticipantType));
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Content(JsonConvert.SerializeObject(results));
        }

        /// <summary>
        /// Gets the non reviewer registration attendance json string.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="meetingId">The meeting identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult GetNonReviewerRegistrationAttendanceJsonString(string firstName, string lastName, string fiscalYear, int? programYearId, int? meetingId,
            int? sessionId)
        {
            var results = new List<MeetingAttendanceViewModel>();
            try
            {
                var clients = GetUsersClientList();
                var list = theMeetingManagementService.GetNonReviewerMeetingAttendanceList(clients, firstName, lastName, fiscalYear, programYearId, meetingId,
                    sessionId);
                results = list.ConvertAll(x => new MeetingAttendanceViewModel(x.ReviewerUserId, x.FirstName, x.LastName, x.Program, x.FiscalYear, x.MeetingAbbreviation, 
                    x.SessionName, x.MeetingRegistrationId, x.Institution, x.Email, x.Role));
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Content(JsonConvert.SerializeObject(results));
        }

        /// <summary>
        /// Upload Page.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult Upload()
        {
            MeetingManagementViewModel vm = new MeetingManagementViewModel();
            try
            {

            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(vm);
        }
        /// <summary>
        /// Edit Hotel Page.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult EditHotel(int? panelUserAssignmentId, int? sessionUserAssignmentId)
        {
            MeetingHotelViewModel vm = new MeetingHotelViewModel();
            try {
                if (!panelUserAssignmentId.HasValue && !sessionUserAssignmentId.HasValue)
                {
                    throw new Exception("Neither assignment parameter (panelUserAssignmentId/sessionUserAssignmentId) has a value.");
                } 
                var hotel = theMeetingManagementService.GetMeetingHotel(panelUserAssignmentId, sessionUserAssignmentId);
                vm = new MeetingHotelViewModel(hotel.FirstName, hotel.LastName, hotel.PanelAbbreviation, hotel.AttendanceStartDate, hotel.AttendanceEndDate, hotel.HotelNotRequiredFlag,
                    hotel.CheckInDate, hotel.CheckOutDate, hotel.HotelId, hotel.DoubleOccupancy, hotel.HotelAndFoodRequestComments, hotel.IsDataComplete, hotel.CancellationDate,
                    hotel.ParticipantModifiedDate, hotel.ModifiedDate, hotel.ModifiedByName, hotel.PanelStart, hotel.PanelEnd, hotel.DefaultHotelId, hotel.PanelUserAssignmentId, hotel.SessionUserAssignmentId, 
                    MMSubTabbedMenuViewModel.SubTab1Link + vm.SubTab1Link, MMSubTabbedMenuViewModel.SubTab2Link + vm.SubTab2Link, MMSubTabbedMenuViewModel.SubTab3Link + vm.SubTab3Link);
                vm.TravelModeDropdown = theLookupService.ListTravelModes().ModelList.ToList();
                vm.HotelDropdown = theLookupService.ListHotels().ModelList.ToList();

                IMeetingDetailsHeader details = null;

                if (panelUserAssignmentId.HasValue)
                {
                    details = theMeetingManagementService.GetMeetingDetailsHeaderReviewer((int)panelUserAssignmentId);
                } else if (sessionUserAssignmentId.HasValue)
                {
                    details = theMeetingManagementService.GetMeetingDetailsHeaderNonReviewer((int)sessionUserAssignmentId);
                }

                vm.DetailsHeader = new MeetingDetailsHeaderModel(details.Attendee, details.ParticipantType, details.Panel, details.Phone, details.Meeting, details.Session,
                    details.Email, details.MeetingStart, details.MeetingEnd, details.SessionStart, details.SessionEnd, details.ReviewerFlag);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(vm);
        }
        /// <summary>
        /// Edit Travel Page.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult EditTravel(int? panelUserAssignmentId, int? sessionUserAssignmentId)
        {
            MeetingTravelViewModel vm = new MeetingTravelViewModel();
            try
            {
                if (!panelUserAssignmentId.HasValue && !sessionUserAssignmentId.HasValue)
                {
                    throw new Exception("Neither assignment parameter (panelUserAssignmentId/sessionUserAssignmentId) has a value.");
                }
                var travel = theMeetingManagementService.GetMeetingTravel(panelUserAssignmentId, sessionUserAssignmentId);
                vm = new MeetingTravelViewModel(travel.FirstName, travel.LastName, travel.PanelName, travel.ReservationCode, travel.TravelId,
                    travel.Fare, travel.AgentFee, travel.AgentFee2, travel.ChangeFee, travel.Ground, travel.NteAmount, travel.GsaRate, travel.NoGsa,
                    travel.ClientAmount, travel.CancelledDate, travel.SpecialRequest, travel.IsDataComplete, travel.ModifiedDate, travel.ModifiedDateBy, travel.MeetingRegistrationId, travel.PanelUserAssignmentId,
                    travel.SessionUserAssignmentId, travel.SubTab1Link, travel.SubTab2Link, travel.SubTab3Link);
                vm.TravelModeDropdown = theLookupService.ListTravelModesWithDetails().ModelList.ToList()
                    .ConvertAll(x => new TravelModeViewModel(x.TravelModeId, x.Abbrevication, x.CanContainTravelFlights));
                vm.Flights = theMeetingManagementService.GetFlights(panelUserAssignmentId, sessionUserAssignmentId).ConvertAll(
                    x => new TravelFlightViewModel(panelUserAssignmentId, sessionUserAssignmentId, x.FlightId, x.CarrierName, x.FlightNumber,
                    x.DepartureCity, x.DepartureDate, x.ArrivalCity, x.ArrivalDate,
                    x.LastModifiedBy, x.LastModifiedDate));
                var details = theMeetingManagementService.GetMeetingDetailsHeader(panelUserAssignmentId, sessionUserAssignmentId);
                vm.DetailsHeader = new MeetingDetailsHeaderModel(details.Attendee, details.ParticipantType, details.Panel, details.Phone, details.Meeting, details.Session,
                    details.Email, details.MeetingStart, details.MeetingEnd, details.SessionStart, details.SessionEnd, details.ReviewerFlag);
                vm.CanManageTravelFlights = HasPermission(Permissions.MeetingManagement.ManageTravelFlights);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(vm);
        }
        /// <summary>
        /// Edit Comments Page.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult EditComments(int? panelUserAssignmentId, int? sessionUserAssignmentId)
        {
            MeetingCommentViewModel vm = new MeetingCommentViewModel();
            try
            {
                var comments = theMeetingManagementService.GetMeetingComment(panelUserAssignmentId, sessionUserAssignmentId);
                vm = new MeetingCommentViewModel(comments.FirstName, comments.LastName, comments.PanelName, comments.InternalComments, comments.MeetingRegistrationId, comments.ModifiedDate, comments.ModifiedByName, panelUserAssignmentId, sessionUserAssignmentId, "", "", "");

                var details = theMeetingManagementService.GetMeetingDetailsHeader(panelUserAssignmentId, sessionUserAssignmentId);
                vm.DetailsHeader = new MeetingDetailsHeaderModel(details.Attendee, details.ParticipantType, details.Panel, details.Phone, details.Meeting, details.Session,
                    details.Email, details.MeetingStart, details.MeetingEnd, details.SessionStart, details.SessionEnd, details.ReviewerFlag);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(vm);
        }
        /// <summary>
        /// Saves the hotel meeting details.
        /// </summary>
        /// <param name="mhvm">The meeting hotel view model.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult SaveHotelMeetingDetails(MeetingHotelViewModel mhvm)
        {
            try
            {
                theMeetingManagementService.SaveHotelDetails(mhvm.PanelUserAssignmentId, mhvm.SessionUserAssignmentId, mhvm.AttendanceStart, mhvm.AttendanceEnd, 
                    mhvm.HotelNotRequiredFlag, mhvm.CheckInDate, mhvm.CheckOutDate, mhvm.HotelId, mhvm.DoubleOccupancy, mhvm.SpecialRequest, mhvm.CancelDate,
                    mhvm.LastUpdated, mhvm.LastUpdatedBy, GetUserId());
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return RedirectToAction(ViewNames.EditHotel, new { panelUserAssignmentId = mhvm.PanelUserAssignmentId });
        }

        /// <summary>
        /// Saves the travel meeting details.
        /// </summary>
        /// <param name="mtvm">The meeting travel view model.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult SaveTravelMeetingDetails(MeetingTravelViewModel mtvm)
        {
            try
            {
                theMeetingManagementService.SaveTravelDetails(mtvm.PanelUserAssignmentId, mtvm.SessionUserAssignmentId, mtvm.TravelModeId, mtvm.Fare, mtvm.AgentFee,
                    mtvm.AgentFee2, mtvm.ChangeFee, mtvm.Ground, mtvm.NteAmount, mtvm.GsaRate, mtvm.NoGsa, mtvm.ClientApprovedAmount, mtvm.CancelledDate, mtvm.SpecialRequest,
                    mtvm.ModifiedDate, mtvm.ModifiedDateBy, mtvm.Reservation, GetUserId());
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return RedirectToAction(ViewNames.EditTravel, new { panelUserAssignmentId = mtvm.PanelUserAssignmentId });
        }

        /// <summary>
        /// Saves the non reviewer session assignments.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="assignedValues">The assigned values.</param>
        /// <param name="unassignedValues">The unassigned values.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult SaveNonReviewerSessionAssignments(int clientMeetingId, int userId, List<int> assignedValues, List<int> unassignedValues)
        {
            bool success = false;
            try
            {
                success = theMeetingManagementService.SaveNonReviewerAssignments(clientMeetingId, userId, assignedValues, unassignedValues, GetUserId());
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { success = success }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Marks the hotel meeting details as complete.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult MarkHotelDataComplete(int? panelUserAssignmentId, int? sessionUserAssignmentId)
        {
            bool result = false;
            try
            {
                result = theMeetingManagementService.MarkHotelDataComplete(panelUserAssignmentId, sessionUserAssignmentId, GetUserId());
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Marks the travel meeting details as complete.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult MarkTravelDataComplete(int? panelUserAssignmentId, int? sessionUserAssignmentId)
        {
            bool result = false;
            try
            {
                result = theMeetingManagementService.MarkTravelDataComplete(panelUserAssignmentId, sessionUserAssignmentId, GetUserId());
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Saves the comments meeting details.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <param name="internalComments">The internal comments.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult SaveCommentsMeetingDetails(int? panelUserAssignmentId, int? sessionUserAssignmentId, string internalComments)
        {
            bool returnTrue = false;
            try
            {
                theMeetingManagementService.SaveCommentsDetails(panelUserAssignmentId, sessionUserAssignmentId, internalComments, GetUserId());

                returnTrue = true;
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { returnTrue = returnTrue}, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Uploads the w9 addresses.
        /// </summary>
        /// <param name="addresses">The addresses.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.ManageTravelFlights)]
        public ActionResult UploadMMTravel(List<TravelFlightImportViewModel> flights)
        {
            ICollection<string> errors = null;
            try
            {
                // Grab any error messages
                var models = flights.ConvertAll(x => x.GetFlight());
                var flightStatuses = theMeetingManagementService.ImportTravelFlights(models, GetUserId()).ToList();
                errors = MessageService.GetErrorMessages(flightStatuses);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { results = errors });
        }
        /// <summary>
        /// Travel leg partial view
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.ManageTravelFlights)]
        public ActionResult TravelLeg(int? panelUserAssignmentId, int? sessionUserAssignmentId)
        {
            var viewModels = new List<TravelFlightViewModel>();
            try
            {
                var carrierList = theMeetingManagementService.GetCarrierList();
                var airportList = theMeetingManagementService.GetAirportList();
                var flights = theMeetingManagementService.GetFlights(panelUserAssignmentId, sessionUserAssignmentId);
                var flightModels = flights.ConvertAll(x => new TravelFlightViewModel(
                    x.PanelUserAssignmentId, x.SessionUserAssignmentId, x.FlightId, x.CarrierName, x.FlightNumber,
                    x.DepartureCity, x.DepartureDate, x.ArrivalCity, x.ArrivalDate,
                    carrierList, airportList));
                viewModels.AddRange(flightModels);
                // Add new
                var viewModel = new TravelFlightViewModel();
                viewModel.CarrierList = carrierList;
                viewModel.AirportList = airportList.ConvertAll(x =>
                    new KeyValuePair<string, string>(x.Key, x.Value));
                viewModels.Add(viewModel);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return PartialView(ViewNames.TravelLeg, viewModels);
        }
        /// <summary>
        /// Adds travel leg.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="programUserAssignmentId">The program user assignment identifier.</param>
        /// <param name="carrierName">The carrier name.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="departureCity">The depature city code.</param>
        /// <param name="departureTime">The departure time.</param>
        /// <param name="arrivalCity">The arrival city code.</param>
        /// <param name="arrivalTime">The arrival time.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.ManageTravelFlights)]
        [HttpPost]
        public ActionResult AddTravelLeg(int? panelUserAssignmentId, int? sessionUserAssignmentId,
            string carrierName, string flightNumber, 
            string departureCity, DateTime departureDate,
            string arrivalCity, DateTime arrivalDate)
        {
            var flag = false;
            var flightId = default(int?);
            try
            {
                flightId = theMeetingManagementService.SaveTravelFlight(panelUserAssignmentId, sessionUserAssignmentId, null,
                    carrierName, flightNumber, departureCity, departureDate,
                    arrivalCity, arrivalDate, GetUserId());
                flag = true;
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { flag = flag, flightId = flightId });
        }
        /// <summary>
        /// Saves travel legs.
        /// </summary>
        /// <param name="flights">The flights.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.ManageTravelFlights)]
        [HttpPost]
        public ActionResult SaveTravelLegs(List<TravelFlightViewModel> flights)
        {
            var flag = false;
            try
            {
                var flightsModel = flights.ConvertAll(x =>
                    new TravelFlightModel(x.PanelUserAssignmentId, x.SessionUserAssignmentId, x.FlightId,
                    x.CarrierName, x.FlightNumber, x.DepartureCity, Convert.ToDateTime(x.DepartureDate),
                    x.ArrivalCity, Convert.ToDateTime(x.ArrivalDate)));
                theMeetingManagementService.SaveTravelFlights(flightsModel, GetUserId());
                flag = true;
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { flag = flag });
        }
        /// <summary>
        /// Deletes travel leg.
        /// </summary>
        /// <param name="flightId">The flight identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.ManageTravelFlights)]
        [HttpPost]
        public ActionResult DeleteTravelLeg(int flightId)
        {
            var flag = false;
            try
            {
                theMeetingManagementService.DeleteTravelFlight(flightId, GetUserId());
                flag = true;
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { flag = flag });
        }
        /// <summary>
        /// Saves the file location.
        /// </summary>
        /// <param name="formData">The form data.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public JsonResult ProcessExcelFile(HttpPostedFileBase fileBase)
        {
            // This is final object
            List<List<string>> finalFile = new List<List<string>>();
            try
            {
                var tbl = ExcelServices.GetExcelData(fileBase.InputStream, 0, true, 37);
                finalFile = ExcelServices.SetMMRows(tbl, 37);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(JsonConvert.SerializeObject(finalFile));
        }
        /// <summary>
        /// Non-reviewer attendees page.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult NonReviewerAttendees()
        {
            MeetingManagementViewModel vm = new MeetingManagementViewModel();
            try
            {
                int userId = GetUserId();
                var yearResults = new List<int>();
                var clients = GetUsersClientList();

                foreach(var client in clients)
                {
                    var list = theSetupService.RetrieveClientProgramYears(client);
                    var results = list.ModelList.Where(x => x.IsActive).ToList();
                    foreach(var result in results)
                    {
                        yearResults.Add(Int32.Parse(result.Year));
                    }
                }
                vm.ActiveYears = yearResults.OrderByDescending(x => x).Distinct().ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(vm);
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MeetingManagement.MeetingManagementAccess)]
        public ActionResult UpdateAssignment()
        {
            try
            {

            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.UpdateAssignment);
        }
    }
}