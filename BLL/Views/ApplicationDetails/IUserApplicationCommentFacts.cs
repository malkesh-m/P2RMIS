namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    public interface IUserApplicationCommentFacts
    {
        int CommentID { get; }
        int UserID { get; }
        string ApplicationID { get; }
        string UserFullName { get; }
        string Comments { get; }
        string ModifiedDate { get; }
        int CommentLkpId { get;}
        string CommentLkpDescription { get;}
    }
}
