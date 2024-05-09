

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's AccountStatu object. 
    /// </summary>
    public partial class AccountStatu
    {
        /// <summary>
        /// Specific index values. 
        /// </summary>
        public class Indexes
        {
            /// <summary>
            /// Account status active
            /// </summary>
            public const int Active = 3;
            /// <summary>
            /// Account status inactive
            /// </summary>
            public const int Inactive = 13;
        }

    }
}
