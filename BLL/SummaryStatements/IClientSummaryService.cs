using System.Collections.Generic;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    /// <summary>
    /// Services provided by the client only portion of the summary statement service.
    /// </summary>
    public interface IClientSummaryService: IServerBase
    {
        /// <summary>
        /// Gets the request review applications.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns></returns>
        Container<ISummaryStatementRequestReview> GetRequestReviewApplications(int programId, int yearId, int? cycle, int? panelId, int? awardTypeId);
    }
}
