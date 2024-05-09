namespace Sra.P2rmis.WebModels.HelperClasses
{
    public interface IReviewerModel
    {
        /// <summary>
        /// reviewer unique identifier
        /// </summary>
        int ReviewerId { get; }
        /// <summary>
        /// reviewer name
        /// </summary>
        string ReviewerName { get; }
    }
}
