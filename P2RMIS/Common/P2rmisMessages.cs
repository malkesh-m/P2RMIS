using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.HotelAndTravel;

namespace Sra.P2rmis.Web.Common
{
    /// <summary>
    /// User facing messages are located in this file.
    /// </summary>
    public class P2rmisMessages
    {
        /// <summary>
        /// Report messages defined here.
        /// </summary>
        public class Reports
        {
            /// <summary>
            /// When the report group description cannot be located.
            /// </summary>
            public const string REPORT_DESCRIPTION_NOT_FOUND = "A description could not be located for this report group.";
            /// <summary>
            /// When the report title cannot be located.
            /// </summary>
            public const string REPORT_TITLE_NOT_FOUND = "A title could not be located for this report.";
            /// <summary>
            /// When the report group or report has not id
            /// </summary>
            public const int NO_REPORT_ID = 0;
        }
        /// <summary>
        /// PanelManagement messages defined here
        /// </summary>
        public class PanelManagement
        {
            /// <summary>
            /// Failure during application release.
            /// </summary>
            public const string ApplicationsReleasedFailure = "An error occurred during releasing, please try again";
            /// <summary>
            /// Converts the status returned from the service Release method to a displayable string.
            /// </summary>
            /// <param name="status">Status returned from service layer Release method</param>
            /// <returns>Text message for the status value</returns>
            public static string ReleaseStatusMessage(ReleaseStatus status)
            {
                string result = "Applications were not released.";
                switch (status)
                {
                    //
                    // Determine the message to display based on the ReleaseStatus
                    //
                    case ReleaseStatus.ScoringNotSetUp:
                        {
                            result = "Scoring hasn't been set up so applications cannot be released.";
                            break;
                        }
                    case ReleaseStatus.Success:
                        {
                            result = "Applications are successfully released to panel reviewers.";
                            break;
                        }
                }

                return result;
            }
        }
        /// <summary>
        /// MyWorkspace messages
        /// </summary>
        public class MyWorkspace
        {
            /// <summary>
            /// Saves the session attendance details message.
            /// </summary>
            /// <param name="statusId">The status identifier.</param>
            /// <returns></returns>
            public static string SaveSessionAttendanceDetailsMessage(int statusId)
            {
                string message = string.Empty;
                switch (statusId)
                {
                    case HotelAndTravelService.HotelAndTravelStatusValue.InvalidAttendanceStartDate:
                        message = "Attendance Start Date must be 2 or less days before the session start.";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.InvalidAttendanceEndDate:
                        message = "Attendance End Date must be 2 or less days after the session end date.";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.InvalidCheckinDate:
                        message = "Check-in Date must be 2 or less days before the session start.";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.InvalidCheckoutDate:
                        message = "Check-out Date must be 2 or less days after the session end date.";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.InvalidAttandanceDateRange:
                        message = "The To date cannot be earlier than the From date.";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.InvalidHotelRange:
                        message = "The Check-out date cannot be earlier than the Check-in date.";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.InvalidAttandanceDate:
                        message = "The From date cannot be earlier than today.";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.InvalidCheckinStartDate:
                        message = "The Check-in date cannot be earlier than today.";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.InvalidCheckinDateFormat:
                        message = "Check-in date is formatted incorrectly";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.InvalidCheckoutDateFormat:
                        message = "Check-out date is formatted incorrectly";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.InvalidAttendanceStartDateFormat:
                        message = "Attendance From date is formatted incorrectly";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.InvalidAttendanceEndDateFormat:
                        message = "Attendance To date is formatted incorrectly";
                        break;

                    case HotelAndTravelService.HotelAndTravelStatusValue.AttendanceStartDateIsRequired:
                        message = "Attendance From date is required";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.AttendanceEndDateIsRequired:
                        message = "Attendance To date is required";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.HotelCheckinDateIsRequired:
                        message = "Check-in date is required";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.HotelCheckoutDateIsRequired:
                        message = "Check-out date is required";
                        break;
                    case HotelAndTravelService.HotelAndTravelStatusValue.TravelModeIsRequired:
                        message = "Travel Mode is required";
                        break;
        }
                return message;
            }
        }
    }
}