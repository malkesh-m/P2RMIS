using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ProgramRegistration
{
    /// <summary>
    /// Interface for the registration workflow
    /// </summary>
    public interface IRegistrationWorkflow
    {
        /// <summary>
        /// The workflow steps
        /// </summary>
        Dictionary<string, RegistrationStep> WorkflowSteps { get; set; }
    }
}