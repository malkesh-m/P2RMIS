using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll.HotelAndTravel;
using Sra.P2rmis.WebModels.Files;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.CrossCuttingServices;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Web.Common;
using Newtonsoft.Json;

namespace Sra.P2rmis.Web.Controllers.MyWorkspace
{
    #region HotelAndTravelController
    /// <summary>
    /// Controller for the Hotel & Travel main view.
    /// </summary>
    public partial class MyWorkspaceController
    {
        #region Actions 
        /// <summary>
        /// Populates the reviewer hotel and travel view
        /// </summary>
        /// <returns>View to observe and make changes to hotel and travel details</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.AccessMyWorkspace)]
        public ActionResult HotelAndTravel()
        {
            HotelAndTravelViewModel model = new HotelAndTravelViewModel();
            try
            {
                int userId = GetUserId();
                var meetingListEntries = theHotelAndTravelService.RetrieveMeetingListEntries(userId);
                model = new HotelAndTravelViewModel(meetingListEntries.Item1.ModelList, meetingListEntries.Item2.ModelList);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                throw e;
            }
            //Set the tab navigation
            model.SetTabContext(GetUserId(), GetUserId(), null, true, true, HasPermission(Permissions.MyWorkspace.AccessMyWorkspace));
            model.SetMenuTitle(GetUserId(), GetUserId(), HasPermission(Permissions.MyWorkspace.AccessMyWorkspace));
            model.LastPageUrl = GetBackButtonUrl();
            return View(model);
        }

        /// <summary>
        /// Gets the meeting list.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetMeetingList()
        {
            List<MeetingListViewModel> model = new List<MeetingListViewModel>();
            string results = String.Empty;
            try
            {
                int userId = GetUserId();
                var meetingListEntries = theHotelAndTravelService.RetrieveMeetingListEntries(userId);
                model = meetingListEntries.Item1.ModelList.ToList().ConvertAll(x => new MeetingListViewModel(x));
                results = JsonConvert.SerializeObject(model);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                throw e;
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Populates the session attendance details modal
        /// </summary>
        /// <returns>modal view partial</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.AccessMyWorkspace)]
        public ActionResult SessionAttendanceDetails(int panelUserAssignmentId)
        {
            SessionAttendanceDetailsViewModel model = new SessionAttendanceDetailsViewModel();
            try
            {
                int userId = GetUserId();
                var details = theHotelAndTravelService.GetSessionAttendanceDetails(panelUserAssignmentId, userId);
                model = new SessionAttendanceDetailsViewModel(details);
                model.TravelModeDropdown = theLookupService.ListTravelModes().ModelList.ToList();
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                model = new SessionAttendanceDetailsViewModel();
                HandleExceptionViaElmah(e);
            }
            return PartialView("_SessionAttendanceDetails", model);
        }

        /// <summary>
        /// Saves the session attendance details.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="meetingRegistrationId">The meeting registration identifier.</param>
        /// <param name="attendanceStartDate">The attendance start date.</param>
        /// <param name="attendanceEndDate">The attendance end date.</param>
        /// <param name="hotelId">The hotel identifier.</param>
        /// <param name="hotelCheckInDate">The hotel check in date.</param>
        /// <param name="hotelCheckOutDate">The hotel check out date.</param>
        /// <param name="travelModeId">The travel mode identifier.</param>
        /// <param name="hotelNotRequired">if set to <c>true</c> [hotel not required].</param>
        /// <param name="hotelDoubleOccupancy">if set to <c>true</c> [hotel double occupancy].</param>
        /// <param name="hotelAndFoodRequestComments">The hotel and food request comments.</param>
        /// <param name="travelRequestComments">The travel request comments.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.AccessMyWorkspace)]
        public ActionResult SaveSessionAttendanceDetails(int? panelUserAssignmentId, int? meetingRegistrationId, string attendanceStartDate, string attendanceEndDate,
            int? hotelId, string hotelCheckInDate, string hotelCheckOutDate, int? travelModeId, bool hotelNotRequired, bool hotelDoubleOccupancy,
            string hotelAndFoodRequestComments, string travelRequestComments)
        {
            bool isSuccessful = false;
            List<string> messages = new List<string>();
            try
            {
                var vm = new SessionAttendanceDetailsViewModel(panelUserAssignmentId, meetingRegistrationId, attendanceStartDate, attendanceEndDate,
                    hotelId, hotelCheckInDate, hotelCheckOutDate, travelModeId, hotelNotRequired, hotelDoubleOccupancy,
                    hotelAndFoodRequestComments, travelRequestComments);
                var model = vm.GetSessionAttendanceDetailsModel();
                var result = theHotelAndTravelService.SaveSessionAttendance(model, GetUserId()).ModelList.ToList();
                if (result.Count == 0)
                {
                    isSuccessful = true;
                }
                else
                {
                    foreach (var error in result)
                    {
                        var msg = P2rmisMessages.MyWorkspace.SaveSessionAttendanceDetailsMessage(error);
                        if (!messages.Contains(msg))
                        {
                            messages.Add(msg);
                        };
                    }
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { success = isSuccessful, messages = messages });
        }
        /// <summary>
        /// Submits the session attendance details.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="meetingRegistrationId">The meeting registration identifier.</param>
        /// <param name="attendanceStartDate">The attendance start date.</param>
        /// <param name="attendanceEndDate">The attendance end date.</param>
        /// <param name="hotelId">The hotel identifier.</param>
        /// <param name="hotelCheckInDate">The hotel check in date.</param>
        /// <param name="hotelCheckOutDate">The hotel check out date.</param>
        /// <param name="travelModeId">The travel mode identifier.</param>
        /// <param name="hotelNotRequired">if set to <c>true</c> [hotel not required].</param>
        /// <param name="hotelDoubleOccupancy">if set to <c>true</c> [hotel double occupancy].</param>
        /// <param name="hotelAndFoodRequestComments">The hotel and food request comments.</param>
        /// <param name="travelRequestComments">The travel request comments.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.AccessMyWorkspace)]
        public ActionResult SubmitSessionAttendanceDetails(int? panelUserAssignmentId, int? meetingRegistrationId, string attendanceStartDate, string attendanceEndDate,
            int? hotelId, string hotelCheckInDate, string hotelCheckOutDate, int? travelModeId, bool hotelNotRequired, bool hotelDoubleOccupancy,
            string hotelAndFoodRequestComments, string travelRequestComments)
        {
            bool isSuccessful = false;
            List<string> messages = new List<string>();
            try
            {
                var vm = new SessionAttendanceDetailsViewModel(panelUserAssignmentId, meetingRegistrationId, attendanceStartDate, attendanceEndDate,
                    hotelId, hotelCheckInDate, hotelCheckOutDate, travelModeId, hotelNotRequired, hotelDoubleOccupancy,
                    hotelAndFoodRequestComments, travelRequestComments);
                var model = vm.GetSessionAttendanceDetailsModel();
                var result = theHotelAndTravelService.SubmitSessionAttendance(model, GetUserId()).ModelList.ToList();
                if (result.Count == 0)
                {
                    isSuccessful = true;
                }
                else
                {
                    foreach (var error in result)
                    {
                        var msg = P2rmisMessages.MyWorkspace.SaveSessionAttendanceDetailsMessage(error);
                        if (!messages.Contains(msg))
                        {
                            messages.Add(msg);
                        };
                    }
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { success = isSuccessful, messages = messages });
        }
        #endregion
    }
    #endregion
}