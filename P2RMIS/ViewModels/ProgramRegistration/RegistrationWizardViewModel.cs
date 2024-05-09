using Sra.P2rmis.WebModels.ProgramRegistration;
using Sra.P2rmis.WebModels.UserProfileManagement;
using System.Collections.Generic;


namespace Sra.P2rmis.Web.ViewModels.ProgramRegistration
{
    /// <summary>
    /// The view model for the registration wizard
    /// </summary>
    public class RegistrationWizardViewModel : RegistrationFormViewModel
    {
        /// <summary>
        /// Data for the workflow
        /// </summary>
        public IRegistrationWorkflow Workflow { get; set; }
        /// <summary>
        /// Form's partial view name
        /// </summary>
        public string PartialViewName { get; set; }
        /// <summary>
        /// The panel user assignment identifier
        /// </summary>
        public int ParticipationId { get; set; }
        /// <summary>
        /// Gets or sets the alternate contact persons.
        /// </summary>
        /// <value>
        /// The alternate contact persons.
        /// </value>
        public IUserAlternateContactPersonModel AlternateContactPersons { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public RegistrationWizardViewModel() {
            this.AlternateContactPersons = AlternateContactPersons;
        }
    }
}