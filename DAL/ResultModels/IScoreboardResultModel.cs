namespace Sra.P2rmis.Dal.ResultModels
{
    interface IScoreboardResultModel
    {
        IApplicationDetail ApplicationDetails { get; }
        System.Collections.Generic.IEnumerable<Sra.P2rmis.Dal.ReviewerCritiques_Result> CritiqueDetails { get; }
        System.Collections.Generic.IEnumerable<Sra.P2rmis.Dal.ReviewerInfo_Result> ReviewerDetails { get; }
    }
}
