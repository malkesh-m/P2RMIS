using System;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    /// <summary>
    /// Wrapper containing an applications changeable values.
    /// </summary>
    public class ChangeToSave
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor for capturing SS changes via string
        /// </summary>
        /// <param name="panelApplicationId">Panel application Id</param>
        /// <param name="priority1">Priority 1 value</param>
        /// <param name="priority2">Priority 2 value</param>
        /// <param name="workflowId">Workflow identifier as a string</param>
        /// <param name="userId">User identifier</param>
        public ChangeToSave(string panelApplicationId, string logNumber, string priority1, string priority2, string workflowId, int userId)
        {
            this.PanelApplicationId = Convert.ToInt32(panelApplicationId);
            this.LogNumber = logNumber;
            this.Priority1 = Convert.ToBoolean(Convert.ToInt32(priority1));
            this.Priority2 = Convert.ToBoolean(Convert.ToInt32(priority2));
            this.UserId = userId;
            this.WorkflowId = Convert.ToInt32(workflowId);
        }
        /// <summary>
        /// Constructor for capturing SS changes
        /// </summary>
        /// <param name="panelApplicationId">Panel application Id</param>
        /// <param name="priority1">Priority 1 value</param>
        /// <param name="priority2">Priority 2 value</param>
        /// <param name="workflowId">Workflow identifier as a string</param>
        /// <param name="userId">User identifier</param>
        public ChangeToSave (int panelApplicationId, string logNumber, string priority1, string priority2, int? workflowId, int userId)
        {
            this.PanelApplicationId = panelApplicationId;
            this.LogNumber = logNumber;
            this.Priority1 = Convert.ToBoolean(priority1);
            this.Priority2 = Convert.ToBoolean(priority2);
            this.UserId = userId;
            this.WorkflowId =  workflowId ?? 0;
        }
	    #endregion
        #region Attributes
        /// <summary>
        /// Application Identifier
        /// </summary>
        internal int PanelApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        internal string LogNumber { get; set; }
        /// <summary>
        /// Priority 1 value
        /// </summary>
        internal bool Priority1 { get; set; }
        /// <summary>
        /// Priority 2 value
        /// </summary>
        internal bool Priority2 { get; set; }
        /// <summary>
        /// Workflow identifier
        /// </summary>
        internal int WorkflowId { get; set; }
        /// <summary>
        /// User Id
        /// </summary>
        internal int UserId { get; set; }
        #endregion
    }
}
