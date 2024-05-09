using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    public interface IPreMeetingCriteriaModel
    {
        /// <summary>
        /// The name of the criteria
        /// </summary>
        string CriteriaName { get; set; }
        /// <summary>
        /// The criteria sort order
        /// </summary>
        int SortOrder { get; set; }
        /// <summary>
        /// The criterion's score flag
        /// </summary>
        bool ScoreFlag { get; set; }
        /// <summary>
        /// the criterion's overall flag
        /// </summary>
        bool OverallFlag { get; set; }
        /// <summary>
        ///  The criterion's premeeting scorce type
        /// </summary>
        string PreMeetingScoreType { get; set; }
        /// <summary>
        /// The criterion's meeting score type
        /// </summary>
        string MeetingScoreType { get; set; }
        /// <summary>
        /// The panel user assignement identifier
        /// </summary>
        int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// The criteria evaluation
        /// </summary>
        IEnumerable<CriteriaEvaluation> CriteriaEvaluations { get; set; }
        /// <summary>
        /// The premeeting adjectival scale
        /// </summary>
        IEnumerable<AdjectivalScale> PremeetingAdjectivalScale { get; set; }
        /// <summary>
        /// The meeting adjectival scale
        /// </summary>
        IEnumerable<AdjectivalScale> MeetingAdjectivalScale { get; set; }
    }
}
