using System;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ProgramMechanismSummaryStatement object.
    /// </summary>
    public partial class ProgramMechanismSummaryStatement : IStandardDateFields
    {
        /// <summary>
        /// Populates an instance of a ProgramMechanismSummaryStatement
        /// </summary>
        /// <param name="reviewStatusId">ReviewStatus identifier</param>
        /// <param name="summaryTemplateId">ClientSummaryTemplate identifier</param>
        public void Populate(int reviewStatusId, int summaryTemplateId)
        {
            ReviewStatusId = reviewStatusId;
            ClientSummaryTemplateId = summaryTemplateId;
        }
    }
}
