namespace Sra.P2rmis.Dal.ResultModels
{
    public interface IUserApplicationComments
    {
        int CommentID { get; set; }
        int UserID { get; set; }
        string ApplicationID { get; set; }
        string UserPrefix { get; set; }
        string UserFirstName { get; set; }
        string UserLastName { get; set; }
        string Comments { get; set; }
        int? CreatedBy { get; set; }
        System.DateTime? CreatedDate { get; set; }
        int? ModifiedBy { get; set; }
        System.DateTime? ModifiedDate { get; set; }
        int CommentLkpId { get; set; }
        string CommentLkpDescription { get; set; }
    }
}
