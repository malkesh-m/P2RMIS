namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ParticipationMethod object. 
    /// </summary>
    public partial class ParticipationMethod
    {
        public class Indexes
        {
            /// <summary>
            /// Participation type is in person.
            /// </summary>
            public const int InPerson = 1;
            /// <summary>
            /// Participation type is in remote.
            /// </summary>
            public const int Remote = 2;
        }
    }
}
