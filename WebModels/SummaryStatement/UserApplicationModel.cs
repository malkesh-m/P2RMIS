using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Contains User and Application information for an ApplicationWorkflowStep assignment
    /// </summary>
    public class UserApplicationModel : IUserApplicationModel
    {
        /// <summary>
        /// PanelApplication entity identifier
        /// </summary>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// Unique identifier for an application workflow step
        /// </summary>
        public int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// unique identifier for the application
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Business identifier for an application
        /// </summary>
        public string LogNumber { get; set; }
        /// <summary>
        /// Unique identifier for a user
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// First name of the user 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the user
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The current and the previous steps
        /// </summary>
        public IEnumerable<IApplicationWorkflowStepModel> Steps { get; set; }
    }
}
