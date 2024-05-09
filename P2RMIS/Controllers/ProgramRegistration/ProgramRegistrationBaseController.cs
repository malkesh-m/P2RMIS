using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll.ProgramRegistration;
using Sra.P2rmis.Bll.UserProfileManagement;

namespace Sra.P2rmis.Web.Controllers.ProgramRegistration
{
    public class ProgramRegistrationBaseController : BaseController
    {
        #region Constants
        /// <summary>
        /// The View names in user profile management
        /// </summary>
        public class ViewNames
        {
            public const string RegistrationWizard = "_RegistrationWizard";
            public const string AcknowledgeNda = "_AcknowledgeNda";
            public const string AcknowledgeNdaCprit = "_AcknowledgeNdaCprit";
            public const string BiasCoi = "_BiasCoi";
            public const string BiasCoiCprit = "_BiasCoiCprit";
            public const string Contract = "_Contract";
            public const string ContractCprit = "_ContractCprit";
            public const string EmContact = "_EmContact";
            public const string ConfirmSign = "_ConfirmSign";
            public const string EmergencyContact = "_EmergencyContact";
            public const string CustomizeContract = "_CustomizeContract";

        }
        /// <summary>
        /// The document form content HTML id
        /// </summary>
        public const string DocumentFormContent = "documentFormContent";
        #endregion
        #region Properties
        /// <summary>
        /// Access to the ProgramRegistrationService
        /// </summary>
        protected IProgramRegistrationService theProgramRegistrationService { get; set; }
        /// <summary>
        /// Access to the UserProfileManagementService
        /// </summary>
        protected IUserProfileManagementService theUserProfileManagementService { get; set; }
        /// <summary>
        /// Service providing access to the common search criteria services.
        /// </summary>
        protected ICriteriaService theCriteriaService { get; set; }
        /// <summary>
        /// Gets or sets the file service.
        /// </summary>
        /// <value>
        /// The file service which provides methods for file manipulation.
        /// </value>
        protected IFileService theFileService { get; set; }
        /// <summary>
        /// Gets or sets the lookup service.
        /// </summary>
        /// <value>
        /// The lookup service.
        /// </value>
        protected ILookupService theLookupService { get; set; }
        /// <summary>
        /// Service providing access to the user profile management service
        /// </summary>
        protected IUserProfileManagementService theProfileService { get; set; }
        #endregion
    }
}