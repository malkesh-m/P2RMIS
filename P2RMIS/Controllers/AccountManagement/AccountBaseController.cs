using System.Web.Mvc;
using Sra.P2rmis.Bll.AccessAccountManagement;
using Sra.P2rmis.Bll.ProgramRegistration;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Bll.Mail;

namespace Sra.P2rmis.Web.Controllers.AccountManagement
{
    public class AccountBaseController : BaseController
    {
        /// <summary>
        /// Service providing access to the user AccessAccount management service
        /// </summary>
        protected IAccessAccountManagementService theAccessAccountService { get; set; }
        /// <summary>
        /// Service providing access to the user profile manage service
        /// </summary>
        protected IUserProfileManagementService theUserProfileManagementService { get; set; }
        /// <summary>
        /// Service providing access to the ProgramRegistration application
        /// </summary>
        protected IProgramRegistrationService theProgramRegistrationService { get; set; }
        /// <summary>
        /// Service providing access to the MailService
        /// </summary>
        protected IMailService theMailService { get; set; }
    }
}