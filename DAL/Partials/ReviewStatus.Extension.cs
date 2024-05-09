using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ReviewStatus object. Contains lookup values within ReviewerStatus table.
    /// </summary>
    public partial class ReviewStatu
    {
        /// <summary>
        /// Not discussed/expedited
        /// </summary>
        public const int Triaged = 1;
        /// <summary>
        /// Application is "Ready to score"
        /// </summary>
        public const int ReadyToScore = 2;
        /// <summary>
        /// To be fully reviewed/ready for scoring
        /// </summary>
        public const int FullReview = 2;
        public const int PriorityOne = 3;
        public const int PriorityTwo = 4;
        public static readonly int? NoPriority = null;
        /// <summary>
        /// Application was found morally unacceptable
        /// </summary>
        public const int Disapproved = 5;
        /// <summary>
        /// Application has completed scoring
        /// </summary>
        public const int Scored = 6;
        /// <summary>
        /// Application is currently scoring
        /// </summary>
        public const int Scoring = 7;
        /// <summary>
        /// Application is in discussion
        /// </summary>
        public const int Active = 8;
        /// <summary>
        /// List of review statuses that indicate an application has been activated
        /// </summary>
        public static IList<int> ActiveScoringStatuses = new ReadOnlyCollection<int>
            (new List<int> {Scored, Scoring});

        /// <summary>
        /// List of review statuses that indicate an application was not expedited/triaged
        /// </summary>
        public static IList<int> NonExpeditedReviewStatuses = new ReadOnlyCollection<int>
            (new List<int> { Scored, Scoring, Active, ReadyToScore });
        /// <summary>
        /// Indicates if this ReviewStatus is Triaged
        /// </summary>
        /// <returns>True if the ReviewStatus is Triaged; false otherwise</returns>
        public bool IsTriaged()
        {
            return (this.ReviewStatusId == Triaged);
        }

        /// <summary>
        /// The default review status for a new panel application
        /// </summary>
        public const int DefaultReviewStatus = ReadyToScore;
    }
}
