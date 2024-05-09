
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom properties for Entity Framework's Nominee Type object. Contains lookup values within NomineeType lookup table.
    /// </summary>

    public partial class NomineeType
    {
        /// <summary>
        /// Specific index values.
        /// </summary>
        public class Indexes
        {
            public const int EligibleNominee = 1;
            public const int SelectedNovice = 2;
            public const int IneligibleNominee = 3;
        }
    }
}
