namespace Sra.P2rmis.Dal.ResultModels
{
    public interface IReviewerComments
    {
        int? PanelId { get; set; }
        string ApplicationIDd { get; set; }
        string Comment { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Prefix { get; set; }
        int? PrgPartId { get; set; }
        int ReviewerId { get; set; }
    }
}
