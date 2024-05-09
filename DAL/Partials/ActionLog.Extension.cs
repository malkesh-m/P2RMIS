using Sra.P2rmis.Dal.Interfaces;
using Entity = Sra.P2rmis.Dal;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ActionLog object.
    /// </summary>
    public partial class ActionLog: IDateFields
    {
        /// <summary>
        /// Populate the ActionLog object because an application transfer was requested.
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="userId">User identifier</param>
        /// <returns>ActionLog model</returns>
        public ActionLog PopulateForApplicationTransfer(string message, int userId)
        {
            return Populate(message, userId, Entity.ActionLogReason.ApplicationTransfer);
        }
        /// <summary>
        /// Populate the ActionLog object because an application reviewer transfer was requested.
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="userId">User identifier</param>
        /// <returns>ActionLog model</returns>
        public ActionLog PopulateForReviewerTransfer(string message, int userId)
        {
            return Populate(message, userId, Entity.ActionLogReason.ReviewerTransfer);
        }
        #region Helpers
        /// <summary>
        /// Populates an ActionLog entity object.  This does all the heavy lifting for all 
        /// types of log message
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="userId">Who requested it be logged</param>
        /// <param name="reasonId">Why this is being loggged</param>
        /// <returns>ActionLog model</returns>
        private ActionLog Populate(string message, int userId, int reasonId)
        {
            this.ActionLogReasonId = reasonId;
            this.Message = message;
            Helper.UpdateCreatedFields(this, userId);
            Helper.UpdateModifiedFields(this, userId);
            return this;
        }
        #endregion
    }
}
