namespace Sra.P2rmis.Bll.Views.CritiqueDetails
{
    public interface ICritiqueFacts
    {
        string ApplicationID { get; }
        string PrincipalInvestigator { get; }
        string PiOrgName { get; }
        string ApplicationTitle { get; }
        string AwardDescription { get; }
        int PanelID { get; }
        string PanelAbrv { get; }
        decimal? PMScore { get; }
        decimal? MeetingScore { get; }
        string PMText { get; }
        string MeetingText { get; }
        string CriteriaName { get; }
        string AdjLabel { get; }
        string ScoringType { get; }
        bool TextFlag { get; }
        bool IsSubmitted { get; }
        int PrgPartId { get; }
        string RevFullName { get; }
        int CriteriaOrder { get; }
        string MeetingAdjLabel { get; }
        string MeetingScoreType { get; }
        string[] OtherAssignedReviewersArray { get; }
        string[] OtherAssignedPartIdsArray { get; }
    }
}
