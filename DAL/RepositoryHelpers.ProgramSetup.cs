using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.Setup;
using System.Data.Objects.SqlClient;
using System.Data.Objects;
using System;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Repository helpers related to system setup
    /// </summary>
    internal partial class RepositoryHelpers
    {
        private const string ScienceArea = "Other";
        /// <summary>
        /// Retrieve the ProgramYears for the list of Client entity identifiers
        /// </summary>
        /// <param name="context">P2RMIS database context<</param>
        /// <param name="clientIds">List of Client entity identifiers</param>
        /// <returns>IQueryable container of ProgramYears</returns>
        internal static IQueryable<ProgramYear> RetrieveProgramSetup(P2RMISNETEntities context, IList<int> clientIds)
        {
            IQueryable<ProgramYear> result = context.ClientPrograms.
                //
                // Select all the ClientPrograms that match a given Client identifier
                //
                Where(x => clientIds.Contains(x.ClientId)).
                //
                // Then we select all the ProgramYears for those clients
                // 
                SelectMany(cp => cp.ProgramYears);

            return result;
        }
        /// <summary>
        /// Retrieve the ProgramMechanisms for the ProgramYear specified
        /// </summary>
        /// <param name="context">P2RMIS database context<</param>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <returns>IQueryable container of ProgramMechanisms</returns>
        internal static IQueryable<ProgramMechanism> RetrieveProgramYearProgramMechanisms(P2RMISNETEntities context, int programYearId)
        {
            IQueryable<ProgramMechanism> result = context.ProgramMechanism
                //
                // Select all the ProgramMechanisms that match a given ProgramYear identifier
                //
                .Where(x => x.ProgramYearId == programYearId);

            return result;
        }

        /// <summary>
        /// Gets the budget deliverable.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        internal static IEnumerable<BudgetDeliverableModel> GetBudgetDeliverable(P2RMISNETEntities context, int programYearId, int receiptCycle)
        {
            var result = context.ApplicationBudgets
                .Where(x => x.Application.ProgramMechanism.ProgramYearId == programYearId && 
                    x.Application.ProgramMechanism.ReceiptCycle == receiptCycle &&
                    !x.Application.WithdrawnFlag &&
                    x.Application.ApplicationCompliances.FirstOrDefault().ComplianceStatusId == ApplicationCompliance.Indexes.Compliant)
                .OrderBy(y => y.Application.LogNumber)
                .Select(x => new BudgetDeliverableModel
                {
                    LogNumber = x.Application.LogNumber,
                    BudgetComments = x.Comments ?? BudgetDeliverableModel.DefaultBudgetComment,
                    ProjectStartDate = x.Application.ProjectStartDate,
                    ProjectEndDate = x.Application.ProjectEndDate,
                    RequestedDirect = (float?)x.DirectCosts,
                    RequestedIndirect = (float?)x.IndirectCosts,
                    Triaged = x.Application.PanelApplications.FirstOrDefault().ApplicationReviewStatus.FirstOrDefault(y => y.ReviewStatu.ReviewStatusTypeId == ReviewStatusType.Review).ReviewStatusId == ReviewStatu.Triaged ? 1 : 0
                });
            return result;
        }

        /// <summary>
        /// Gets the scoring deliverable.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        internal static IEnumerable<ScoringDeliverableModel> GetScoringDeliverable(P2RMISNETEntities context, int programYearId, int receiptCycle)
        {
            var result = context.uspGetScoreDeliverableData(programYearId, receiptCycle).ToList().Select(x => new ScoringDeliverableModel
            {
                LogNumber = x.LogNumber,
                PanelAbbreviation = x.PanelAbbreviation,
                PanelName = x.PanelName,
                GlobalScore = x.Triaged != 1 ? (float?)x.GlobalScore : null,
                StandardDeviation = x.Triaged != 1 ? (float?)x.StandDeviation : null,
                Triaged = x.Triaged,
                Disapproved = x.Disapproved != 1 ? false : true,
                Eval1 = x.Triaged != 1 ? (float?)x.Eval1 : null,
                Eval2 = x.Triaged != 1 ? (float?)x.Eval2 : null,
                Eval3 = x.Triaged != 1 ? (float?)x.Eval3 : null,
                Eval4 = x.Triaged != 1 ? (float?)x.Eval4 : null,
                Eval5 = x.Triaged != 1 ? (float?)x.Eval5 : null,
                Eval6 = x.Triaged != 1 ? (float?)x.Eval6 : null,
                Eval7 = x.Triaged != 1 ? (float?)x.Eval7 : null,
                Eval8 = x.Triaged != 1 ? (float?)x.Eval8 : null,
                Eval9 = x.Triaged != 1 ? (float?)x.Eval9 : null,
                Eval10 = x.Triaged != 1 ? (float?)x.Eval10 : null
            });
            return result;
        }

        /// <summary>
        /// Gets the panel deliverable.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        internal static IEnumerable<PanelDeliverableModel> GetPanelDeliverable(P2RMISNETEntities context, int programYearId)
        {
            var result = from SessionPanel in context.SessionPanels
                         join PanelApplication in context.PanelApplications on SessionPanel.SessionPanelId equals
                             PanelApplication.SessionPanelId into pa
                         from PanelApplication2 in pa.DefaultIfEmpty()
                         where PanelApplication2.Application.ProgramMechanism.ProgramYearId == programYearId
                         select new PanelDeliverableModel
                         {
                             PanelId = SessionPanel.LegacyPanelId != null ? SessionPanel.LegacyPanelId.ToString() : string.Empty,
                             PanelName = SessionPanel.PanelName,
                             PanelAbbreviation = SessionPanel.PanelAbbreviation
                };
            return result.Distinct().OrderBy(x => x.PanelId);
        }

        /// <summary>
        /// Gets the panel deliverable.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        internal static IEnumerable<MechanismDeliverableModel> GetCriteriaDeliverable(P2RMISNETEntities context, int programYearId, int receiptCycle)
        {
            var results = new List<MechanismDeliverableModel>();
            var result = context.MechanismTemplateElements
                .Where(x => x.MechanismTemplate.ProgramMechanism.ProgramYearId == programYearId && x.MechanismTemplate.ProgramMechanism.ReceiptCycle == receiptCycle && x.MechanismTemplate.ReviewStageId == 1)
                .Select( x => new CriterionDeliverableModel { 
                             EcmId = x.LegacyEcmId,
                             Name = x.ClientElement.ElementDescription,
                             ElementAbbreviation = x.ClientElement.ElementIdentifier,
                             SortOrder = x.SortOrder,
                             SumStateSortOrder = x.SummarySortOrder != null ? x.SummarySortOrder.ToString() : string.Empty,
                             WordCount = x.RecommendedWordCount != null ? x.RecommendedWordCount.ToString() : string.Empty,
                             IsScoreEnabled = (x.ScoreFlag) ? 1 : 0,
                             IsTextEnabled = (x.TextFlag) ? 1 : 0,
                             IsOverallEnabled = (x.OverallFlag) ? 1 : 0,
                             EvalInstructions = string.Empty,
                             FiscalYear = x.MechanismTemplate.ProgramMechanism.ProgramYear.Year,
                             ReceiptCycle =(int) x.MechanismTemplate.ProgramMechanism.ReceiptCycle,
                             AwardType = x.MechanismTemplate.ProgramMechanism.ClientAwardType.LegacyAwardTypeId,
                             AtmId = (int) x.MechanismTemplate.ProgramMechanism.LegacyAtmId,
                             ShortDescription = x.MechanismTemplate.ProgramMechanism.ClientAwardType.AwardAbbreviation
                         });

            var mechanismResult = result.GroupBy(x => x.AwardType)
                .Select(y => y.OrderBy(y2 => y2.SortOrder).ToList())
                .ToList();

            foreach(var item in mechanismResult)
            {
                var mechanism = new MechanismDeliverableModel();
                mechanism.AtmId = item[0].AtmId;
                mechanism.AwardType = item[0].AwardType;
                mechanism.ShortDescription = item[0].ShortDescription;
                mechanism.CriterionList = item;
                results.Add(mechanism);
            }
            return results;
        }
    }
}
