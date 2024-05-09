using System;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Workflow service services provided for Workflow instance objects.
    /// </summary>
    public partial class WorkflowService
    {
        #region Instance Services
        /// <summary>
        /// Retrieves the instance identified by the specific id value.
        /// </summary>
        /// <param name="instanceId">Template instance identifier</param>
        /// <returns>Instance object identified by id</returns>
        public object GetInstance(int instanceId)
        {
            object result = null;

            if (IsGetInstanceParameterValid(instanceId))
            {
                //  change repository => result = this.UnitOfWork.WorkflowInstanceRepository.GetByID(instanceId);
            }
            return result;  // type needs to change when Entity framework object defined
        }
        /// <summary>
        /// Populates an existing Instance object and updates it with the user entered values.
        /// </summary>
        /// <param name="templateModel">Template web model from entity framework</param>
        /// <param name="instanceId">Template instance identifier</param>
        /// <param name="userId">User identifier</param>
        /// <returns>TBD ?</returns>
        public bool PopulateInstance(IWorkflowTemplateModel templateModel, int instanceId, int userId)
        {
            if (IsPopulateInstanceParametersValid(templateModel, instanceId, userId))
            {
            }
            return true; // not sure about type to return 
        }
        /// <summary>
        /// Retrieves information about workflow progress for a single application.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application workflow</param>
        /// <returns>Zero or more workflow steps progress for an application's workflow</returns>
        public Container<ApplicationWorkflowStepModel> GetSingleWorkflowProgress(int applicationWorkflowId)
        {
            ///
            /// Set up default return
            /// 
            Container<ApplicationWorkflowStepModel> container = new Container<ApplicationWorkflowStepModel>();

            if (applicationWorkflowId > 0)
            {
                //
                // Call the DL and retrieve the progress for the given applicationWorkflowId
                //
                var results = UnitOfWork.ApplicationWorkflowRepository.GetSingleWorkflowProgress(applicationWorkflowId);
                //
                // Then create the view to return to the PI layer & return
                //
                container.SetModelList(results);
            }

            return container;
        }
        /// <summary>
        /// retrieves the active workflow step for an application
        /// </summary>
        /// <param name="applicationWorkflowId">the applications workflow id</param>
        /// <returns>integer of the active workflow step</returns>
        /// <exception cref="ArgumentException">Exception thrown if invalid application workflow Id detected</exception>
        public int GetActiveApplicationWorkflowStep(int applicationWorkflowId)
        {
            int result = 0;

            if (applicationWorkflowId > 0)
            {
                result = UnitOfWork.ApplicationWorkflowRepository.GetActiveApplicationWorkflowStep(applicationWorkflowId);
            }
            else
            {
                throw new ArgumentException("WorkflowService.GetActiveApplicationWorkflowStep() detected an invalid argument.  applicationWorkflowId parameter is less than or equal to 0");
            }
            return result;
        }
        #endregion
        #region Instance Service Helpers
        /// <summary>
        /// Validates the parameters for method are valid.  Validations are
        ///   - instance object is not null
        /// </summary>
        /// <param name="instanceId">Template instance identifier</param>
        /// <returns>True if the instance object exists; false otherwise</returns>
        private bool IsGetInstanceParameterValid(int instancesId)
        {
            return true;
        }
        /// <summary>
        /// Validates the parameters for method are valid.  Validations are
        ///   - template model is not null
        ///   - userId is valid (greater than 0 & valid userId)
        /// </summary>
        /// <param name="templateModel">Template web model from entity framework</param>
        /// <param name="userId">User identifier</param>
        /// <returns>True if parameters are valid; false otherwise</returns>
        private bool IsPopulateInstanceParametersValid(IWorkflowTemplateModel templateModel, int instanceid, int userId)
        {
            return ((IsPopulateInstanceParametersValid(templateModel, userId)) &&
                    (this.UnitOfWork.WorkflowInstanceRepository.IsInstanceValid(instanceid))
                   );
        }
        /// <summary>
        /// Validates the parameters for method are valid.  Validations are
        ///   - template model is not null
        ///   - userId is valid (greater than 0 & valid userId)
        /// </summary>
        /// <param name="templateModel">Template web model from entity framework</param>
        /// <param name="userId">User identifier</param>
        /// <returns>True if parameters are valid; false otherwise</returns>
        private bool IsPopulateInstanceParametersValid(IWorkflowTemplateModel templateModel, int userId)
        {
            return ((templateModel != null) &&
                    (UnitOfWork.UofwUserRepository.GetUserByID(userId) != null)
                  );
        }
        #endregion
    }
}
