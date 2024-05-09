using System;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ApplicationWorkflowStepWorkLog object. 
    /// </summary>
    public partial class ApplicationWorkflowStepWorkLog : IStandardDateFields
    {
        #region Constants
        private const string InitializeErrorMessage = "ApplicationWorkflowStepWorkLog.Initialize received an invalid parameter workflowStepId is [{0}] and userId is [{1}]";
        private const string CompleteErrorMessage = "ApplicationWorkflowStepWorkLog.Complete received an invalid parameter: checkinUserId is [{0}] and modifiedUserId is [{1}]";

        /// <summary>
        /// The checkout action
        /// </summary>
        public const string CheckoutAction = "Check-Out";
        /// <summary>
        /// The checkin action
        /// </summary>
        public const string CheckinAction = "Check-In";
        #endregion
        /// <summary>
        /// Initializes a new ApplicationWorkflowStepWorkLog object.
        /// </summary>
        /// <param name="workflowStepId">Workflow step identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="documentContent">Content of the document.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void Initialize(int workflowStepId, int userId, byte[] documentContent)
        {
            if (IsInitializeParametersValid(workflowStepId, userId))
            {
                this.ApplicationWorkflowStepId = workflowStepId;
                this.UserId = userId;
                this.CheckOutDate = GlobalProperties.P2rmisDateTimeNow;
                this.CheckoutBackupFile = documentContent;
                Helper.UpdateCreatedFields(this, userId);
                Helper.UpdateModifiedFields(this, userId);
            }
            else
            {
                string message = string.Format(InitializeErrorMessage, workflowStepId, userId);
                throw new ArgumentException(message);
            }
        }
        /// <summary>
        /// Marks the workflow step as complete
        /// </summary>
        /// <param name="userId">User identifier that performs check-in</param>
        public void Complete(int userId)
        {
            Complete(userId, userId);
        }
        /// <summary>
        /// Marks the workflow step as complete
        /// </summary>
        /// <param name="userId">User identifier that performs check-in</param>
        /// <param name="backupFile">Backup file</param>
        public void Complete(int userId, byte[] backupFile)
        {
            Complete(userId, userId, backupFile);
        }
        /// <summary>
        /// Marks the workflow step as complete
        /// </summary>
        /// <param name="userId">User identifier that performs check-in</param>
        /// <param name="modifiedUserid">User identifier that modified the record</param>
        public void Complete(int checkinUserId, int modifiedUserid)
        {
            Complete(checkinUserId, modifiedUserid, null);
        }
        /// <summary>
        /// Marks the workflow step as complete
        /// </summary>
        /// <param name="userId">User identifier that performs check-in</param>
        /// <param name="modifiedUserid">User identifier that modified the record</param>
        /// <param name="backupFile">Backup file</param>
        public void Complete(int checkinUserId, int modifiedUserid, byte[] backupFile)
        {
            if (checkinUserId > 0 && modifiedUserid > 0)
            {
                //
                // Mark the workflow step as complete
                //
                this.CheckInUserId = checkinUserId;
                this.CheckinBackupFile = backupFile;
                this.CheckInDate = GlobalProperties.P2rmisDateTimeNow;
                //
                // and indicate who updated it
                //
                Helper.UpdateModifiedFields(this, modifiedUserid);
            }
            else
            {
                string message = string.Format(CompleteErrorMessage, checkinUserId, modifiedUserid);
                throw new ArgumentException(message);
            }
        }
        #region Helpers
        /// <summary>
        /// Tests if the parameters are valid:
        ///   - workflowStepId is greater than 0
        ///   - userId is greater than 0
        /// </summary>
        /// <param name="workflowStepId">Workflow step identifier</param>
        /// <param name="userId">User identifier</param>
        /// <returns>True if parameters valid; false otherwise</returns>
        private bool IsInitializeParametersValid(int workflowStepId, int userId)
        {
            return ((workflowStepId > 0 ) && (userId > 0));
        }
        #endregion
    }
}
