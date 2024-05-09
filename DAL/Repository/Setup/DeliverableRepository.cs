using System.Collections.Generic;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    /// <summary>
    /// Interface for data methods to populate deliverables
    /// </summary>
    /// <seealso cref="Sra.P2rmis.Dal.IGenericRepository{Sra.P2rmis.Dal.ProgramCycleDeliverable}" />
    public interface IDeliverableRepository : IGenericRepository<ProgramCycleDeliverable>
    {
        /// <summary>
        /// Gets the budget deliverable.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        IEnumerable<BudgetDeliverableModel> GetBudgetDeliverable(int programYearId, int receiptCycle);

        /// <summary>
        /// Gets the score deliverable.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        IEnumerable<ScoringDeliverableModel> GetScoreDeliverable(int programYearId, int receiptCycle);

        /// <summary>
        /// Gets the panel deliverable.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        IEnumerable<PanelDeliverableModel> GetPanelDeliverable(int programYearId);

        /// <summary>
        /// Gets the criteria deliverable.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        IEnumerable<MechanismDeliverableModel> GetCriteriaDeliverable(int programYearId, int receiptCycle);
    }

    /// <summary>
    /// Data methods to populate deliverables
    /// </summary>
    /// <seealso cref="Sra.P2rmis.Dal.GenericRepository{Sra.P2rmis.Dal.ProgramCycleDeliverable}" />
    public class DeliverableRepository : GenericRepository<ProgramCycleDeliverable>, IDeliverableRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public DeliverableRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion
        #region Services
        /// <summary>
        /// Gets the score deliverable data.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        public IEnumerable<ScoringDeliverableModel> GetScoreDeliverable(int programYearId, int receiptCycle)
        {
            var result = RepositoryHelpers.GetScoringDeliverable(context, programYearId, receiptCycle);
            return result;
        }

        /// <summary>
        /// Gets the budget deliverable data.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        public IEnumerable<BudgetDeliverableModel> GetBudgetDeliverable(int programYearId, int receiptCycle)
        {
            var result = RepositoryHelpers.GetBudgetDeliverable(context, programYearId, receiptCycle);
            return result;
        }

        /// <summary>
        /// Gets the panel deliverable data.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        public IEnumerable<PanelDeliverableModel> GetPanelDeliverable(int programYearId)
        {
            var result = RepositoryHelpers.GetPanelDeliverable(context, programYearId);
            return result;
        }

        /// <summary>
        /// Gets the criteria deliverable data.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        public IEnumerable<MechanismDeliverableModel> GetCriteriaDeliverable(int programYearId, int receiptCycle)
        {
            var result = RepositoryHelpers.GetCriteriaDeliverable(context, programYearId, receiptCycle);
            return result;
        }
        #endregion
    }
}
