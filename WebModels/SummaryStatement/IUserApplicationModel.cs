using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Contains User and Application information for an ApplicationWorkflowStep assignment
    /// </summary>
    public interface IUserApplicationModel
    {
        /// <summary>
        /// PanelApplication entity identifier
        /// </summary>
        int PanelApplicationId { get; set; }
        /// <summary>
        /// Unique identifier for an application workflow step
        /// </summary>
        int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// unique identifier for the application
        /// </summary>
        int ApplicationId { get; set; }
        /// <summary>
        /// Business identifier for an application
        /// </summary>
        string LogNumber { get; set; }
        /// <summary>
        /// Unique identifier for a user
        /// </summary>
        int? UserId { get; set; }
        /// <summary>
        /// First name of the user 
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// Last name of the user
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// The current and the previous steps
        /// </summary>
        IEnumerable<IApplicationWorkflowStepModel> Steps { get; set; }
    }
}