

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's MeetingType object.
    /// </summary>
    public partial class MeetingType
    {
        public class Indexes
        {
            /// <summary>
            /// On line review meeting type
            /// </summary>
            public const int OnLineReview = 3;
         }
        /// <summary>
        /// Identifier for an onsite meeting
        /// </summary>
        public const int Onsite = 1;
        /// <summary>
        /// Identifier for a teleconference meeting
        /// </summary>
        public const int Teleconference = 2;
        /// <summary>
        /// Video conference meeting type
        /// </summary>
        public const int VideoConference = 4;
        /// <summary>
        /// Returns the ParticipationType for the meeting type.
        /// </summary>
        /// <param name="meetingTypeId">MeetingType entity identifier</param>
        /// <returns>ParticipationMethod entity identifier</returns>
        public static int MeetingTypeToParticipationType(int meetingTypeId)
        {
            return (meetingTypeId == Onsite) ? ParticipationMethod.Indexes.InPerson : ParticipationMethod.Indexes.Remote;
        }
        /// <summary>
        /// Indicates if the meeting type is Online.
        /// </summary>
        /// <param name="meetingTypeId">MeetingType entity identifier</param>
        /// <returns>True if the meeting type is OnLine; false otherwise</returns>
        public static bool IsOnline(int? meetingTypeId)
        {
            return ((meetingTypeId.HasValue) && (meetingTypeId.Value == Indexes.OnLineReview)) ? true : false;
        }
        /// <summary>
        /// Determines the number of pre-meeting phases for the MeetingType
        /// </summary>
        /// <param name="meetingTypeId">Meeting type identifier</param>
        /// <returns>Number of phases for the pre-meeting</returns>
        public static int DeterminePreMeetingPhaseCountForMeetingType(int meetingTypeId)
        {
            //
            // By definition OnLineReviews have three phases; all the rest have two
            //
            return (meetingTypeId == Indexes.OnLineReview) ? 3 : 2;
        }

        /// <summary>
        /// Determines the number of meeting phases for the MeetingType
        /// </summary>
        /// <param name="meetingTypeId">Meeting type identifier</param>
        /// <returns>Number of phases for the meeting</returns>
        public static int DetermineMeetingPhaseCountForMeetingType(int meetingTypeId)
        {
            //
            // By definition OnLineReviews doesn't have a meeting phase
            //
            return (meetingTypeId == Indexes.OnLineReview) ? 0 : 1;
        }
    }
}
