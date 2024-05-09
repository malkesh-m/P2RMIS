using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    public class PreMeetingCriteriaModel : IPreMeetingCriteriaModel
    {
        /// <summary>
        /// The name of the criteria
        /// </summary>
        public string CriteriaName { get; set; }
        /// <summary>
        /// The criteria sort order
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// The criterion's score flag
        /// </summary>
        public bool ScoreFlag { get; set; }
        /// <summary>
        /// the criterion's overall score flag
        /// </summary>
        public bool OverallFlag { get; set; }
        /// <summary>
        ///  The criterion's premeeting scorce type
        /// </summary>
        public string PreMeetingScoreType { get; set; }
        /// <summary>
        /// The criterion's meeting score type
        /// </summary>
        public string MeetingScoreType { get; set; }
        /// <summary>
        /// The panel user assignement identifier
        /// </summary>
        public int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// The criteria evaluation
        /// </summary>
        public IEnumerable<CriteriaEvaluation> CriteriaEvaluations { get; set; }
        /// <summary>
        /// The premeeting adjectival scale
        /// </summary>
        public IEnumerable<AdjectivalScale> PremeetingAdjectivalScale { get; set; }
        /// <summary>
        /// The meeting adjectival scale
        /// </summary>
        public IEnumerable<AdjectivalScale> MeetingAdjectivalScale { get; set; }
    }
}
