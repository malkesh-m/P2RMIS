using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    /// <summary>
    /// Services provided for Summary Assignments.
    /// </summary>
    public partial class SummaryManagementService
    {
        #region Constants
        /// <summary>
        /// Exception messages thrown when parameter validation fails.
        /// </summary>
        private partial class ExceptionMessages
        {
            public const string GetAssignedUsers = "SummaryManagementService.GetAssignedUsers() received an invalid parameter.  applicationId collection is null? = {0}; number of entries = {1}; any entries invalid? {2}";
            public const string GetAutoCompleteUsers = "SummaryManagementService.GetAutoCompleteUsers() received an invalid parameter.  clientCollection collection is null? = {0}; number of entries = {1}; search string null or empty? {2}; any entries invalid? {3}";
            public const string GetAApplicationWorkflowId = "SummaryManagementService.GetAApplicationWorkflowId() received an invalid parameter.  applicationIds is null? = {0}; number of entries = {1}";
            public const string GetReportAppInfoParameters = "SummaryManagementService.GetReportAppInfoParameters() received an invalid parameter.  applicationIds is null? = {0}; number of entries = {1}; any entries invalid? = {2}";
        }
        #endregion
        #region Services Provided
        /// <summary>
        /// Retrieve the assigned user for the current phase for one or more applications.
        /// </summary>
        /// <param name="applicationIds">Collection of application identifiers</param>
        /// <returns>Container of applications & their assigned users.</returns>
        public Container<IUserApplicationModel> GetAssignedUsers(ICollection<int> applicationIds)
        {
            //
            // Create the container to return and validate the parameters
            // 
            Container<IUserApplicationModel> container = new Container<IUserApplicationModel>();
            ValidateGetAssignedUsersParameters(applicationIds);
            //
            // Since we have valid parameters now get the data & return it.
            // 
            var results = UnitOfWork.SummaryManagementRepository.GetAssignedUsers(applicationIds);
            container.SetModelList(results);
            return container;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="models"></param>
        public static void CompleteWorkflowsSteps(List<IUserApplicationModel> models)
        {
            
            foreach(var m in models)
            {
                List<IApplicationWorkflowStepModel> list = new List<IApplicationWorkflowStepModel>(m.Steps);
                list.Add(new ApplicationWorkflowStepModel { ApplicationWorkflowStepId = ApplicationWorkflowStep.CompleteWorkflow, StepName = ApplicationWorkflowStep.CompleteWorkflowStepName });
                m.Steps = list;
            }
        }

        /// <summary>
        /// Retrieve the user information based on a partial string to search
        /// </summary>
        /// <param name="program">Program abbreviation</param>
        /// <param name="fiscalYear">Fiscal Year</param>
        /// <returns>Container holding zero or more workflow assignments</returns>
        public Container<IUserModel> GetAutoCompleteUsers(string searchString, ICollection<int> clientCollection)
        {
            //
            // Create the container to return and validate the parameters
            // 
            Container<IUserModel> container = new Container<IUserModel>();
            ValidateGetAutoCompleteUsersParameters(searchString, clientCollection);
            //
            // Since we have valid parameters now get the data & return it.
            // 
            var results = UnitOfWork.SummaryManagementRepository.GetAutoCompleteUsers(searchString, clientCollection);
            container.SetModelList(results);
            return container;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Validate parameters for GetAssignedUsers
        /// </summary>
        /// <param name="program">Program abbreviation</param>
        /// <param name="fiscalYear">Fiscal Year</param>
        private void ValidateGetAssignedUsersParameters(ICollection<int> applicationIds)
        {
            if (
                (applicationIds == null) ||
                (applicationIds.Count == 0) ||
                (applicationIds.Min() <= 0)
                )
            {
                bool isNull = applicationIds == null;
                int theCount = (applicationIds != null)? applicationIds.Count: 0;
                string anyEntriesInvalid = (applicationIds != null)? (applicationIds.Min() <= 0).ToString() : "Unknown" ;

                throw new ArgumentException(string.Format(ExceptionMessages.GetAssignedUsers, isNull, theCount, anyEntriesInvalid));
            }
        }
        /// <summary>
        /// Validate parameters for GetAutoCompleteUsers
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="clientCollection"></param>
        private void ValidateGetAutoCompleteUsersParameters(string searchString, ICollection<int> clientCollection)
        {
            if (
                (clientCollection == null) ||
                (clientCollection.Count == 0) ||
                (string.IsNullOrWhiteSpace(searchString)) ||
                (clientCollection.Min() <= 0)
                )
            {
                bool isNull = clientCollection == null;
                int theCount = (clientCollection != null) ? clientCollection.Count : 0;
                string anyEntriesInvalid = (clientCollection != null) ? (clientCollection.Min() <= 0).ToString() : "Unknown";

                throw new ArgumentException(string.Format(ExceptionMessages.GetAutoCompleteUsers, isNull, theCount, string.IsNullOrWhiteSpace(searchString), anyEntriesInvalid));
            }
        }		 
	    #endregion
    }
}
