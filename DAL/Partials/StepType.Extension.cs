
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's StepType object. Contains lookup values within StepType table.
    /// </summary>
    public partial class StepType
    {
        /// <summary>
        /// Step types index values by name
        /// </summary>
        public class Indexes
        {
            public const int Writing = 1;
            public const int Editing = 2;
            public const int Review = 3;
            public const int Standard = 4;
            public const int Preliminary = 5;
            public const int Revised = 6;
            public const int Final = 7;
            public const int Meeting = 8;
            public const int ReviewSupport = 9;
        }
        /// <summary>
        /// Indicates if the specified StepTypeId is a ClientReview step type.
        /// </summary>
        /// <param name="stepTypeId">StepType entity identifier</param>
        /// <returns>True if the stepTypeId is a ClientReview step; false otherwise</returns>
        public bool IsClientReviewStepType()
        {
            return ((this.StepTypeId == Indexes.Review) || (this.StepTypeId == Indexes.ReviewSupport));
        }
    }
}
