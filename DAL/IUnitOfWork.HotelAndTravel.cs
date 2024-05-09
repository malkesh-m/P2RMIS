using Sra.P2rmis.Dal.Repository.MeetingManagement;
using static Sra.P2rmis.WebModels.MeetingManagement.MeetingCommentModel;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Identifies the access points to the database entities for Hotel and Travel
    /// crud operations on entity objects
    /// </summary>
    public partial interface IUnitOfWork
    {
        /// <summary>
        /// Provides database access for MeetingRegistrationRepository functions. 
        /// </summary>
        IMeetingRegistrationRepository MeetingRegistrationRepository { get; }
        /// <summary>
        /// Provides database access for MeetingRegistrationTravelRepository functions. 
        /// </summary>
        IMeetingRegistrationTravelRepository MeetingRegistrationTravelRepository { get; }
        /// <summary>
        /// Provides database access for MeetingRegistrationHotelRepository functions. 
        /// </summary>
        IGenericRepository<MeetingRegistrationHotel> MeetingRegistrationHotelRepository { get; }
        /// <summary>
        /// Provides database access for MeetingRegistrationAttendanceRepository functions. 
        /// </summary>
        IGenericRepository<MeetingRegistrationAttendance> MeetingRegistrationAttendanceRepository { get; }
        /// <summary>
        /// Provides database access for MeetingRegistrationAttendanceRepository functions. 
        /// </summary>
        IMeetingRegistrationCommentRepository MeetingRegistrationCommentRepository { get; }
        /// <summary>
        /// Gets the meeting registration travel flight repository.
        /// </summary>
        /// <value>
        /// The meeting registration travel flight repository.
        /// </value>
        IMeetingRegistrationTravelFlightRepository MeetingRegistrationTravelFlightRepository { get; }
    }
}
