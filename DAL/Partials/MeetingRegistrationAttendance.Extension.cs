using Sra.P2rmis.Dal.Interfaces;
using System;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's MeetingRegistrationAttendance object. 
    /// </summary>
    public partial class MeetingRegistrationAttendance : IStandardDateFields
    {
        /// <summary>
        /// Populates the specified attendance start date.
        /// </summary>
        /// <param name="attendanceStartDate">The attendance start date.</param>
        /// <param name="attendanceEndDate">The attendance end date.</param>
        public void Populate(DateTime? attendanceStartDate, DateTime? attendanceEndDate)
        {
            this.AttendanceStartDate = attendanceStartDate;
            this.AttendanceEndDate = attendanceEndDate;
        }
    }
}
