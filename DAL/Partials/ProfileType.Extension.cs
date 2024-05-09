
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom properties for Entity Framework's Profile Type object. Contains lookup values within ProfileType lookup table.
    /// </summary>

    public partial class ProfileType
    {
         /// <summary>
        /// Specific index values.
        /// </summary>
        public class Indexes
        {
            public const int Prospect = 1;
            public const int Reviewer = 2;
            public const int SraStaff = 3;
            public const int Client = 4;
            public const int Misconduct = 5;
        }

        /// <summary>
        /// Sevice method to determine if credentials can be sent, 
        /// when the user is initially created, for the indicated profile type 
        /// </summary>
        /// <param name="profileTypeId">The profile type identifier</param>
        /// <returns>returns true if credentials can be sent for this profile type, false otherwise</returns>
        public static bool IsSendCredentialsEnabled(int profileTypeId)
        {
            return profileTypeId == ProfileType.Indexes.SraStaff || profileTypeId == ProfileType.Indexes.Client;
        }

    }
}
