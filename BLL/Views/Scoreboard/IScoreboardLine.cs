namespace Sra.P2rmis.Bll.Views.Scoreboard
{
    interface IScoreboardLine
    {
        string CriteriaName { get; }
        int CriteriaOrder { get; }
        object MeetingScore { get; }
        object PreMeetingScore { get; }
        int PrgPartId { get; }
        string ReviewerName { get; }
        string Role { get; }
        bool IsCritiqueSubmitted { get; }
    }
}
