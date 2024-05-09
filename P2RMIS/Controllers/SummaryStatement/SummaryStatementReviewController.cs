using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.ViewModels.SummaryStatementReview;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// Controller Summary Statement Client Processing page tabs:
    ///   - SSM-800 - Summary Statement Client View
    ///   - Included in this file: common and overhead items
    /// </summary>
    public partial class SummaryStatementReviewController : SummaryStatementBaseController
    {
        /// <summary>
        /// Contains names for the views created by this controller.
        /// </summary>
        public class ViewNames
        {
            public const string ReviewView = "Review";
            public const string RequestReviewView = "RequestReview";
        }

        #region Construction; Setup & Disposal

        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private SummaryStatementReviewController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryStatementReviewController"/> class.
        /// </summary>
        /// <param name="theSummaryManagementService">The summary management service.</param>
        /// <param name="theClientSummaryService">The client summary service.</param>
        /// <param name="theCriteriaService">The criteria service.</param>
        /// <param name="theWorkflowService">The workflow service.</param>
        /// <param name="theSummaryProcessingService">The summary processing service.</param>
        /// <param name="theApplicationManagementService">The application management service.</param>
        public SummaryStatementReviewController(ISummaryManagementService theSummaryManagementService, IClientSummaryService theClientSummaryService,
            ICriteriaService theCriteriaService, IWorkflowService theWorkflowService, ISummaryProcessingService theSummaryProcessingService,
            IApplicationManagementService theApplicationManagementService)
        {
            this.theClientSummaryService = theClientSummaryService;
            this.theCriteriaService = theCriteriaService;
            this.theSummaryManagementService = theSummaryManagementService;
            this.theWorkflowService = theWorkflowService;
            this.theSummaryProcessingService = theSummaryProcessingService;
            this.theApplicationManagementService = theApplicationManagementService;
        }

        /// <summary>
        /// Sets the filter dropdown menus from the session variables
        /// </summary>
        /// <param name="theViewModel">the view model</param>
        /// <returns>SummaryStatement request view model with populated selection values</returns>
        private RequestReviewViewModel SetFilterDropdownsFromSession(RequestReviewViewModel theViewModel)
        {
            //
            // Test for a null session.  This is necessary to support unit testing
            //
            if (Session != null)
            {
                theViewModel.SelectedProgram = (int)Session[SessionVariables.ClientProgramId];
                //populate the fiscal years list from the programs selected in the session
                var fys = theCriteriaService.GetAllProgramYears((int)Session[SessionVariables.ClientProgramId]);
                theViewModel.FiscalYears = fys.ModelList.OrderByDescending(o => o.Year).ToList();
                //populate the panel list from the fiscal years selected in the session
                var panels = this.theCriteriaService.GetSessionPanels((int)Session[SessionVariables.ProgramYearId]);
                theViewModel.Panels = panels.ModelList.OrderBy(x => x.PanelAbbreviation).ToList();
                //populate the cycle list from the program and fiscal year selected in the session
                var cycles = this.theCriteriaService.GetProgramYearCycles((int)Session[SessionVariables.ProgramYearId]);
                theViewModel.Cycles = cycles.ModelList.OrderBy(x => x).ToList();
                if ((Session[SessionVariables.Cycle] != null) || (Session[SessionVariables.PanelId] != null))
                {
                    //populate the award list from the program/fiscal year/cycle/panel in the session
                    var awards = this.theCriteriaService.GetAwards((int)Session[SessionVariables.ProgramYearId], (int?)Session[SessionVariables.Cycle], (int?)Session[SessionVariables.PanelId]);
                    theViewModel.Awards = awards.ModelList.OrderBy(x => x.AwardAbbreviation).ToList();
                }
            }
            return theViewModel;
        }
        #endregion
    }
}
