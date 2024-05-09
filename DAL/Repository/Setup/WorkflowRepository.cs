using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IWorkflowRepository : IGenericRepository<Workflow>
    {
        /// <summary>
        /// Gets workflows.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        IQueryable<Workflow> GetWorkflows(int clientId);
        /// <summary>
        /// Gets the workflow that contains all provided step types.
        /// </summary>
        /// <param name="stepTypeIds">The step type identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        Workflow GetWorkflow(List<int> stepTypeIds, int clientId);
    }

    public class WorkflowRepository : GenericRepository<Workflow>, IWorkflowRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public WorkflowRepository(P2RMISNETEntities context)
            : base(context)
        {
        }
        #endregion
        /// <summary>
        /// Gets workflows.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public IQueryable<Workflow> GetWorkflows(int clientId)
        {
            var workflows = context.Workflows.Where(x => x.ClientId == clientId);
            return workflows;
        }
        /// <summary>
        /// Gets the workflow that contains all provided step types.
        /// </summary>
        /// <param name="stepTypeIds">The step type identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public Workflow GetWorkflow(List<int> stepTypeIds, int clientId)
        {
            var workflows = GetWorkflows(clientId);
            for (var i = 0; i < stepTypeIds.Count; i++)
            {
                var stepTypeId = stepTypeIds[i];
                workflows = workflows.Where(x => x.WorkflowSteps.Count(y => y.StepTypeId == stepTypeId) > 0);
            }
            if (workflows.Count() > 1)
            {
                workflows = workflows.Where(x => x.WorkflowSteps.Count() == stepTypeIds.Count);
            }
            return workflows.FirstOrDefault();
        }
    }
}
