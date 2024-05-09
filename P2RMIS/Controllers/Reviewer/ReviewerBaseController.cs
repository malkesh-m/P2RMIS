using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.ReviewerRecruitment;
using Sra.P2rmis.Bll.ApplicationScoring;

namespace Sra.P2rmis.Web.Controllers.PanelManagement
{
    /// <summary>
    /// Base controller for P2RMIS Reviewer Information controller.  
    /// Basically a container for Reviewer Information controller common functionality.
    /// </summary>
    public class ReviewerBaseController : BaseController
    {
        #region Properties
        /// <summary>
        /// Service providing access to the Summary management services.
        /// </summary>
        protected ISummaryProcessingService theSummaryProcessingService { get; set; }
        protected IMailService theMailService { get; set; }
        protected IPanelManagementService thePanelManagementService { get; set; }
        protected IWorkflowService theWorkflowService { get; set; }
        protected IUserProfileManagementService theUserProfileManagementService { get; set; }
        protected IFileService theFileService { get; set; }
        protected ILookupService theLookupService { get; set; }
        protected ICriteriaService theCriteriaService { get; set; }
        protected IReviewerRecruitmentService theRecruitmentService { get; set; }
        protected IApplicationScoringService theApplicationScoringService { get; set; }
        #endregion
    }
}