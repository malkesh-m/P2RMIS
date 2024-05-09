using System;
using System.Collections.Generic;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    /// <summary>
    /// Services provided for workflow management of summary statements.
    /// </summary>
    public partial class SummaryManagementService
    {
        #region Classes
        /// <summary>
        /// Exception messages thrown when parameter validation fails.
        /// </summary>
        private partial class ExceptionMessages
        {
            public static string ManageWorkflow = "SummaryManagementService.{0} detected an invalid parameter.  program = {1}; fiscalYear = {2}";
            public static string AssignWorkflow = "SummaryManagementService.AssignWorkflow detected an invalid parameter.  mechanismId = {0}; workflowId = {1}";
            public static string AssignWorkflowParameters = "SummaryManagementService.AssignWorkflow detected an invalid parameter.  collection is null? = {0}; userId = {1}";
        } 
        #endregion
        #region Services
        /// <summary>
        /// Retrieve a matrix representation of awards (y-axis) and priorities (x-axis) and (1)assigned or (2)defaulted workflowIds for each pair 
        /// </summary>
        /// <param name="program">Program abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of program offering</param>
        /// <returns>Enumerable list of awards and their assigned or defaulted workflow</returns>
        public Container<IAwardWorkflowPriorityModel> GetWorkflowAssignmentOrDefault(int program, int fiscalYear, int? cycle)
        {
            ValidateManageWorkflowParameters("GetWorkflowAssignmentOrDefault", program, fiscalYear);
            Container<IAwardWorkflowPriorityModel> container = new Container<IAwardWorkflowPriorityModel>();
            var result = UnitOfWork.SummaryManagementRepository.GetWorkflowAssignmentOrDefault(program, fiscalYear, cycle);
            container.ModelList = result;

            return container;
        }
        /// <summary>
        /// Retrieve workflow information for all workflows belonging to a client
        /// </summary>
        /// <param name="program">Program abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of program offering</param>
        /// <returns>Enumerable list of workflows</returns>
        public Container<IWorkflowTemplateModel> GetClientWorkflowAll(int program, int fiscalYear)
        {
            ValidateManageWorkflowParameters("GetClientWorkflowAll", program, fiscalYear);
            Container<IWorkflowTemplateModel> container = new Container<IWorkflowTemplateModel>();
            var result = UnitOfWork.SummaryManagementRepository.GetClientWorkflowAll(program, fiscalYear);
            container.ModelList = result;

            return container;
        }
        /// <summary>
        /// Save or updates as appropriate the workflow assigned to a mechanism.
        /// </summary>
        /// <param name="collection">Collection of mechanismId/workflowId pairs</param>
        /// <param name="userId">User identifier making the change</param>
        public bool AssignWorkflow(ICollection<AssignWorkflowToSave> collection, int userId)
        {
            bool hasChanged = false;

            ValidateAssignWorkflowParameters(collection, userId);

            foreach (var item in collection)
            {
                ValidateAssignWorkflowParameters(item.MechanismId, item.WorkflowId);
                WorkflowMechanism entity = UnitOfWork.WorkflowMechanismRepository.GetByMechanismIdAndReviewStatusId(item.MechanismId, item.ReviewStatusId);
                //
                // If the entity exists & the relationship was changed
                //  - update the workflow id & the indication of who changed it
                //  - set the local flag indicating the UnitOfWork should save
                //  - finally save the modified entity
                //  
                if (WorkflowMechanism.Exists(entity))
                {
                    if (entity.Changed(item.WorkflowId, item.ReviewStatusId))
                    {
                        entity.Populate(item.MechanismId, item.WorkflowId, item.ReviewStatusId, userId);
                        UnitOfWork.WorkflowMechanismRepository.Update(entity);
                        hasChanged = true;
                    }
                }
                //
                // we have a new relationship between the mechanism & workflow
                // create the entity object; update the created & modified fields
                //  - set the local flag indicating the UnitOfWork should save
                //  - finally add the modified entity
                //
                else
                {
                    entity = new WorkflowMechanism();
                    entity.Create(item.MechanismId, item.WorkflowId, item.ReviewStatusId, userId);
                    hasChanged = true;
                    UnitOfWork.WorkflowMechanismRepository.Add(entity);
                }
            }
            //
            // If one or more entities were updated or added then Save them all
            //
            if (hasChanged)
            {
                UnitOfWork.Save();
            }
            return hasChanged;
        }
        #region Helpers
        /// <summary>
        /// Validates parameters for AssignWorkflow
        /// </summary>
        /// <param name="mechanismId">Mechanism identifier</param>
        /// <param name="workflowId">Associated workflow identifier</param>
        /// <exception cref="ArgumentException">Thrown if invalid arguments (Mechanism ID; WorkflowId ) are supplied </exception>
        private void ValidateAssignWorkflowParameters(int mechanismId, int workflowid)
        {
            if ((mechanismId <= 0) || (workflowid <= 0))
            {
                string message = string.Format(ExceptionMessages.AssignWorkflow, mechanismId, workflowid);
                throw new ArgumentException(message);
            }
        }
        /// <summary>
        /// Validates parameters for AssignWorkflow
        /// </summary>
        /// <param name="collection">Collection of mechanismId/workflowId pairs</param>
        /// <param name="userId">User identifier making the change</param>
        /// <exception cref="ArgumentException">Thrown if invalid arguments (collection; userId) are supplied </exception>
        private void ValidateAssignWorkflowParameters(ICollection<AssignWorkflowToSave> collection, int userId)
        {
            if ((collection == null) || (userId <= 0))
            {
                string message = string.Format(ExceptionMessages.AssignWorkflowParameters, (collection == null), userId);
                throw new ArgumentException(message);
            }
        }
        /// <summary>
        /// Validates parameters for GetClientWorkflowAll & GetWorkflowAssignmentOrDefault
        ///  - program is not null; empty string or all white space
        ///  - fiscal year is not null; empty string or all white space
        /// </summary>
        /// <param name="methodName">Name of caller</param>
        /// <param name="program">Program abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of program offering</param>
        private void ValidateManageWorkflowParameters(string methodName, int program, int fiscalYear)
        {
            ServerBase.ValidateInt(program, methodName, nameof(program));
            ServerBase.ValidateInt(fiscalYear, methodName, nameof(fiscalYear));
        }
        #endregion
        #endregion
    }
}
