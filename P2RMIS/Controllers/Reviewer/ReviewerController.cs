using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.Web.ViewModels.Reviewer;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Web.ViewModels.PanelManagement;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.Bll.ReviewerRecruitment;
using Sra.P2rmis.WebModels.ReviewerRecruitment;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sra.P2rmis.Bll.ApplicationScoring;

namespace Sra.P2rmis.Web.Controllers.PanelManagement
{
    public partial class ReviewerController : ReviewerBaseController
    {
        #region Properties
        /// <summary>
        /// Controller Name
        /// </summary>
        public static string Name { get { return "PanelManagement"; } }
        public static string MethodRequestTransfer { get { return "RequestTransfer"; } }
        #endregion
        #region Construction; Setup & Disposal

        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private ReviewerController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewerController"/> class.
        /// </summary>
        /// <param name="theSummaryProcessingService">The summary processing service.</param>
        /// <param name="theMailService">The mail service.</param>
        /// <param name="thePanelManagementService">The panel management service.</param>
        /// <param name="theWorkflowService">The workflow service.</param>
        /// <param name="theUserProfileManagementService">The user profile management service.</param>
        /// <param name="theFileService">The file service.</param>
        /// <param name="theLookupService">The lookup service.</param>
        /// <param name="theCriteriaService">The criteria service.</param>
        /// <param name="theRecruitmentService">The recruitment service.</param>
        /// <param name="theApplicationScoringService">The application scoring service</param>
        public ReviewerController(ISummaryProcessingService theSummaryProcessingService,
                                        IMailService theMailService,
                                        IPanelManagementService thePanelManagementService,
                                        IWorkflowService theWorkflowService,
                                        IUserProfileManagementService theUserProfileManagementService,
                                        IFileService theFileService,
                                        ILookupService theLookupService,
                                        ICriteriaService theCriteriaService,
                                        IReviewerRecruitmentService theRecruitmentService,
                                        IApplicationScoringService theApplicationScoringService)
        {
            this.theSummaryProcessingService = theSummaryProcessingService;
            this.theMailService = theMailService;
            this.thePanelManagementService = thePanelManagementService;
            this.theWorkflowService = theWorkflowService;
            this.theUserProfileManagementService = theUserProfileManagementService;
            this.theFileService = theFileService;
            this.theLookupService = theLookupService;
            this.theCriteriaService = theCriteriaService;
            this.theRecruitmentService = theRecruitmentService;
            this.theApplicationScoringService = theApplicationScoringService;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// action result for the Reviewers page
        /// </summary>
        /// <returns>the view of the reviews</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Reviewer.ViewReviewers)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets the panel reviewers json.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Reviewer.ViewReviewers)]
        public ActionResult GetPanelReviewersJson(int sessionPanelId)
        {
            bool flag = false;
            string reviewersJson = string.Empty;
            try
            {
                var reviewersView = new List<PanelReviewerViewModel>();
                var reviewers = thePanelManagementService.GetPanelReviewers(sessionPanelId).ModelList.ToList();
                reviewers = reviewers.Where(x => x.TypeName == Routing.PanelManagement.AssignmentType.PanelUserAssignment)
                        .OrderBy(y => y.LastName).ToList();
                reviewersView = reviewers.ConvertAll(x => new PanelReviewerViewModel(x));
                flag = true;
                reviewersJson = JsonConvert.SerializeObject(new { flag, reviewers = reviewersView });
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Content(reviewersJson, "application/json");
        }
        #endregion
    }
}