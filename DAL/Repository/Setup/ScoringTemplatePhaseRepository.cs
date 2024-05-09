using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    /// <summary>
    /// Provides database access to ScoringTemplatePhase entities.
    /// </summary>
    public interface IScoringTemplatePhaseRepository : IGenericRepository<ScoringTemplatePhase>
    {
        /// <summary>
        /// Gets the by scoring template identifier.
        /// </summary>
        /// <param name="scoringTemplateId">The scoring template identifier.</param>
        /// <returns></returns>
        IEnumerable<ScoringTemplatePhase> GetByScoringTemplateId(int scoringTemplateId);
    }

    public class ScoringTemplatePhaseRepository : GenericRepository<ScoringTemplatePhase>, IScoringTemplatePhaseRepository
    {
        public ScoringTemplatePhaseRepository(P2RMISNETEntities context) : base(context) { }
        /// <summary>
        /// Gets the by scoring template identifier.
        /// </summary>
        /// <param name="scoringTemplateId">The scoring template identifier.</param>
        /// <returns></returns>
        public IEnumerable<ScoringTemplatePhase> GetByScoringTemplateId(int scoringTemplateId)
        {
            var phases = Get(x => x.ScoringTemplateId == scoringTemplateId);
            return phases;
        }
    }
}
