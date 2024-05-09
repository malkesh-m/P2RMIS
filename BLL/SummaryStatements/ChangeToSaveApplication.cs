using System;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    /// <summary>
    /// applications changeable values.
    /// </summary>
    public class ChangeToSaveApplication
    {
        #region Constructor & Setup	 
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationId">Application Id; can be a string or an integer</param>
        /// <param name="priority1">Priority 1 value</param>
        /// <param name="priority2">Priority 2 value</param>
        /// <param name="userId">User identifier</param>
        public ChangeToSaveApplication(string applicationId, string priority1, string priority2, int userId)
        {
            this.ApplicationId = Convert.ToInt32(applicationId);
            this.Priority1 = Convert.ToBoolean(Convert.ToInt32(priority1));
            this.Priority2 = Convert.ToBoolean(Convert.ToInt32(priority2));
            this.UserId = userId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Application Identifier
        /// </summary>
        internal int ApplicationId { get; set; }
        /// <summary>
        /// Priority 1 value
        /// </summary>
        internal bool Priority1 { get; set; }
        /// <summary>
        /// Priority 2 value
        /// </summary>
        internal bool Priority2 { get; set; }
        /// <summary>
        /// User Id
        /// </summary>
        internal int UserId { get; set; }
        #endregion
    }
}
