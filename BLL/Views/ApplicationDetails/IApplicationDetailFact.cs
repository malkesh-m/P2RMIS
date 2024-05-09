namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    public interface IApplicationDetailFact
    {
        string ApplicationId { get; set; }
        string ApplicationTitle { get; set; }
        string AwardDesccription { get; set; }
        string AwardShortDesc { get; set; }
        string PiOrgName { get; set; }
        string PrincipalInvestigatorName { get; set; }
        int PanelId { get; set; }
        bool isSessionOpen { get; }
        string SessionClosedMessage { get; set; }
        int ProgramId { get; set; }
    }
}
