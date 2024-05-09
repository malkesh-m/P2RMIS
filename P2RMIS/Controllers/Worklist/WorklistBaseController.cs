

using Sra.P2rmis.Bll.ReviewerRecruitment;
using Sra.P2rmis.Bll.UserProfileManagement;

namespace Sra.P2rmis.Web.Controllers.Worklist
{
    /// <summary>
    /// Base controller for P2RMIS Worklist controller.  
    /// Container for Worklist controller common functionality.
    /// </summary>
    public class WorklistBaseController : BaseController
    {
        #region Properties

        protected IReviewerRecruitmentService theRecruitmentService { get; set; }
        protected IUserProfileManagementService theProfileManagementService { get; set; }
        #endregion
        #region Constants
        /// <summary>
        /// Worklist view names
        /// </summary>
        public class ViewNames
        {
            public const string ProfileUpdateModal = "_ProfileUpdateModal";
        }
        #endregion
    }
}