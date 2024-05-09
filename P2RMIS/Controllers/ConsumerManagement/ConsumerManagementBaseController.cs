using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.UserProfileManagement;

namespace Sra.P2rmis.Web.Controllers.ConsumerManagement
{
    public class ConsumerManagementBaseController : BaseController
    {
        #region Properties
        /// <summary>
        /// Service providing access to the Consumer Management services.
        /// </summary>
        protected IConsumerManagementService theConsumerManagementService { get; set; }
        /// <summary>
        /// Service providing access to the lookup services.
        /// </summary>
        protected ILookupService theLookupService { get; set; }
        /// <summary>
        /// Service providing access to the criteria services.
        /// </summary>
        protected ICriteriaService theCriteriaService { get; set; }
        /// <summary>
        /// Service providing access to the user profile management services.
        /// </summary>
        protected IUserProfileManagementService theUserProfileManagementService { get;set; }
        #endregion
    }
}