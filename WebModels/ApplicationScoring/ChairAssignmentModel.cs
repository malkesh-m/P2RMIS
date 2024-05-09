
namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    #region Interface
    public interface IChairAssignmentModel : IPreAssignmentModel
    {
        /// <summary>
        /// Gets a value indicating whether this instance is summary started.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is summary started; otherwise, <c>false</c>.
        /// </value>
        bool IsSummaryStarted { get; }
        /// <summary>
        /// Gets a value indicating whether this instance has summary text.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has summary text; otherwise, <c>false</c>.
        /// </value>
        bool HasSummaryText { get; }
        /// <summary>
        /// Indicates if the reviewer is a chairperson
        /// </summary>
        bool IsChairPerson { get; }
    }
    #endregion

    public class ChairAssignmentModel : PreAssignmentModel, IChairAssignmentModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostAssignmentModel"/> class.
        /// </summary>
        /// <param name="blinded">whether the mechanism is blinded</param>
        /// <param name="isChairPerson">Indicates if the reviewer is a chairperson</param>
        public ChairAssignmentModel(bool blinded, bool isChairPerson) : base(blinded)
        {
            this.IsChairPerson = isChairPerson;
        }

        #region Properties        
        /// <summary>
        /// Gets a value indicating whether this instance is summary started.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is summary started; otherwise, <c>false</c>.
        /// </value>
        public bool IsSummaryStarted { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance has summary text.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has summary text; otherwise, <c>false</c>.
        /// </value>
        public bool HasSummaryText { get; private set; }
        /// <summary>
        /// Indicates if the reviewer is a chairperson
        /// </summary>
        public bool IsChairPerson { get; private set; }
        /// <summary>
        /// Sets the summary status.
        /// </summary>
        /// <param name="isSummmaryStarted">if set to <c>true</c> [is summmary started].</param>
        /// <param name="hasSummaryText">if set to <c>true</c> [has summary text].</param>
        public void SetSummaryStatus(bool isSummmaryStarted, bool hasSummaryText)
        {
            IsSummaryStarted = isSummmaryStarted;
            HasSummaryText = hasSummaryText;
        }
        #endregion
    }
}
