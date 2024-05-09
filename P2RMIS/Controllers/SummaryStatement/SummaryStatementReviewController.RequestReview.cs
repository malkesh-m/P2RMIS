using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.Web.ViewModels.SummaryStatement;
using Sra.P2rmis.Web.ViewModels.SummaryStatementReview;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// Controller Summary Statement Client Review page tabs:
    ///   - SSM-820 - Client Specify Priority for review
    ///   - Included in this file: actions related to SSM-820
    /// </summary>
    public partial class SummaryStatementReviewController : SummaryStatementBaseController
    {
        public class RequestReviewConstants
        {
            public const string SubmitPriorityList = "Submit Priority List";
        }
        /// <summary>
        /// Requests the review.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrProcessOrReview)]
        public ActionResult RequestReview()
        {
            ViewModels.Shared.TabMenuViewModel.HasPermission = HasPermission;
            var vm = new RequestReviewViewModel();
            try
            {
                //
                // Sets the client list for the specific user & get the user's
                // list of programs then populate the view model with this list.
                //
                List<int> clientList = GetUsersClientList();
                // get users program list based off of their client list
                var programs = theCriteriaService.GetAllClientPrograms(clientList);
                vm.Programs = programs.ModelList.OrderBy(x => x.ProgramAbbreviation).ToList();

                vm.HideUserSearchCriteria = true;
                //
                // Get selected filters from session 
                if (IsSessionParametersExisting())
                {
                    GetSsPanelFilterVars(vm);
                }

                if (IsGetSummaryApplicationsParametersValid(vm.SelectedProgram, vm.SelectedFy))
                {
                    SetSsPanelFilterVars(vm.SelectedProgram, vm.SelectedFy, vm.SelectedCycle, vm.SelectedPanel, vm.SelectedAward);
                    vm = SetFilterDropdownsFromSession(vm);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return View(vm);
        }
        /// <summary>
        /// Gets the request review applications json.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrReview)]
        public ActionResult GetRequestReviewApplicationsJson(int programId,
            int yearId, int? cycle, int? panelId, int? awardTypeId)
        {
            RequestReviewApplicationsViewModel results = new RequestReviewApplicationsViewModel();
            try
            {
                if (programId >= 0)
                {
                    SetSsPanelFilterVars(programId, yearId, cycle, panelId, awardTypeId);
                    var applications = this.theClientSummaryService.GetRequestReviewApplications(programId, yearId,
                        cycle, panelId, awardTypeId);
                    results = new RequestReviewApplicationsViewModel(applications.ModelList.ToList());
                    results.RefreshTime = ViewHelpers.FormatDateTime(GlobalProperties.P2rmisDateTimeNow);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            //
            // and format the results into Json format.
            //
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #region Helpers
        /// <summary>
        /// Retrieve the fiscal years for the selected program.
        /// </summary>
        /// <param name="selectedProgram">Selected program abbreviation</param>
        /// <returns>List of fiscal years in Json format</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult GetFiscalYearsJson(int selectedProgram)
        {
            var container = theCriteriaService.GetAllProgramYears(selectedProgram);
            var result = container.ModelList.OrderByDescending(x => x.Year);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

       