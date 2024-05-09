namespace Sra.P2rmis.Dal
{
    public partial class SystemConfiguration
    {
        /// <summary>
        /// Specific index values. 
        /// /// </summary>
        public class Indexes
        {
            /// <summary>
            /// Indicates if the contract is reset upon update 
            /// when the reviewer has been assigned.
            /// </summary>
            public const int ResetContractOnUpdate = 1;
        }
    }
}
