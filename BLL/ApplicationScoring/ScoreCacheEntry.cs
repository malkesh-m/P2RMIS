using System.Collections.Generic;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Cache entry for On Line Scoring.  Contains data
    /// required to update the scores when polled.
    /// </summary>
    public class ScoreCacheEntry
    {
        /// <summary>
        /// Initial capacity of the Scores list.  The initial capacity of the list is 0
        /// and will increase to a capacity of 4 when the first score is added.  Because then
        /// maximum number of reviewers is small, typically less than 20 set it to an initial 
        /// value to prevent in most cases the list from being reallocated.
        /// </summary>
        /// <remarks>
        /// The current sizes of 0 & 4 for capacity & increment are not guaranteed.  These numbers
        /// can change with any new release.
        /// </remarks>
        public readonly int ScoreListSize = 15;

        #region Construction & SetUP
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="index">Value to use to match the reviewer on screen location</param>
        public ScoreCacheEntry(string index)
        {
            this.ReviewerIndex = index;
            this.Scores = new List<string>(ScoreListSize);
        }
        #endregion
        /// <summary>
        /// UserId as a string value.  Since only updates will be sent
        /// some way is required to identify which reviewer the updated
        /// scores are for.
        /// </summary>
        public string ReviewerIndex { get; private set; }
        /// <summary>
        /// Overall score value.
        /// </summary>
        public string OverallScore { get; set; }
        /// <summary>
        /// Scores is a list of the individual score values in criteria
        /// order (1 ... n).  Currently n has a maximum of 10 but this
        /// representation provides for any expansion.
        /// </summary>
        public IList<string> Scores { get; private set; }
        #region Methods
        /// <summary>
        /// Resets the score list.
        /// </summary>
        public void Reset()
        {
            Scores.Clear();
        }
        #endregion
    }
}
