namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    public interface IReviewerCommentFacts
    {
        string ApplicationId { get; }
        string Comment { get; }
        int ProgramPartId { get; }
        string ReviewerName { get; }
        int ReviewerId { get; }
    }
}
