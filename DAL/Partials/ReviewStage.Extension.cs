
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ReviewStage object 
    /// </summary>		
    public partial class ReviewStage
    {
        /// <summary>
        /// Specific index values.
        /// </summary>
        public class Indexes
        {
            public const int Asynchronous = 1;
            public const int Synchronous = 2;
            public const int Summary = 3;
        }
        //
        // Use the above Indexes definitions in place of these constants.  Eventually these 
        // will be re-factored out.
        //
        public const int Asynchronous = 1;
        public const int Synchronous = 2;
        public const int Summary = 3;
    }
}
