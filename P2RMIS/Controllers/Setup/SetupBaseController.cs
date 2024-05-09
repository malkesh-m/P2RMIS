using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Sra.P2rmis.Bll.Setup;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Bll.LibraryService;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Bll.PanelManagement;
using System.Linq;
using Sra.P2rmis.WebModels.Library;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// Base controller for P2RMIS Setup controller.  
    /// Basically a container for Setup controller common functionality.
    /// </summary>
    public class SetupBaseController : BaseController
    {
        #region Properties
        /// <summary>
        /// Service providing access to the Setup services.
        /// </summary>
        protected ISetupService theSetupService { get; set; }

        protected ICriteriaService theCriteriaService { get; set; }

        protected IUserProfileManagementService theUserProfileManagementService { get; set; }

        protected ILookupService theLookupService { get; set; }

        protected ILibraryService theLibraryService { get; set; }
        protected IPanelManagementService thePanelManagementService { get; set; }
        protected IApplicationManagementService theApplicationManagementService {get; set;}
        /// <summary>
        /// Gets or sets the meeting management service.
        /// </summary>
        /// <value>
        /// The meeting management service.
        /// </value>
        protected Bll.MeetingManagement.IMeetingManagementService theMeetingManagementService { get; set; }

        #endregion

        /// <summary>
        /// Contains names for the views created by this controller.
        /// </summary>
        public class ViewNames
        {
            public const string ProgramWizard = "_ProgramWizard";
            public const string RemoveWarning = "_RemoveWarning";
            public const string AwardWizard = "_AwardWizard";
            public const string RemoveDisallowed = "_RemoveDisallowed";
            public const string CriterionWizard = "_CriterionWizard";
            public const string UploadScoringTemplate = "_UploadScoringTemplate";
            public const string MeetingWizard = "_MeetingWizard";
            public const string SessionWizard = "_SessionWizard";
            public const string AssignPrograms = "_AssignPrograms";
            public const string UnAssignPrograms = "_UnAssignPrograms";
            public const string PreviewCriteria = "_PreviewCriteria";
            public const string PanelWizard = "_PanelWizard";
            public const string UpdatePanel = "_UpdatePanel";
            public const string AddPanelNoPrograms = "_AddPanelNoPrograms";
            public const string UploadFeeSchedule = "_UploadFeeSchedule";
            public const string EditDocument = "EditDocument";
            public const string VimeoModal = "_VimeoModal";
            public const string PersonnelManagementHistoryModal = "_PersonnelManagementHistoryModal";
            public const string ImportErrorMessages = "_ImportErrorMessages";
            public const string WithdrawApplication = "_WithdrawApplication";
            public const string ResetWithdraw = "_ResetWithdraw";
        }

        public class Constants
        {
            public const string NoDeliverableResultsFound = "No deliverables have been configured.  Please contact P2RMIS IT.";
        }
        /// <summary>
        /// Sets the filter dropdown menus from the session variables
        /// </summary>
        /// <param name="theViewModel">the view model</param>
        /// <returns>Populated model for dropdowns</returns>
        internal void SetFilterDropdownsFromSession(DataTransferFilterMenuViewModel theViewModel)
        {
            if (Session != null)
            {
                theViewModel.SelectedProgram = (int?)Session[SessionVariables.ClientProgramId] ?? 0;
                theViewModel.SelectedFy = (int?)Session[SessionVariables.ProgramYearId] ?? 0;
                theViewModel.SelectedCycle = (int?)Session[SessionVariables.Cycle] ?? 0;
                var programs = theCriteriaService.GetAllClientPrograms(GetUsersClientList());
                theViewModel.Programs = programs.ModelList.OrderBy(x => x.ProgramAbbreviation).ToList();
                if (theViewModel.SelectedProgram > 0)
                {
                    //populate the fiscal years list from the programs selected in the session
                    var fys = theCriteriaService.GetAllProgramYears(theViewModel.SelectedProgram);
                    theViewModel.FiscalYears = fys.ModelList.OrderByDescending(o => o.Year).ToList();
                }
                if (theViewModel.SelectedFy > 0)
                {
                    //populate the cycle list from the program and fiscal year selected in the session
                    var cycles = this.theCriteriaService.GetProgramYearCycles(theViewModel.SelectedFy);
                    theViewModel.Cycles = cycles.ModelList.OrderBy(x => x).ToList();
                }
            }
        }
        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="fileBase">The file base.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        internal string SaveFile(HttpPostedFileBase fileBase, int clientId)
        {
            string directoryPath = String.Format(@"{0}\{1}", ConfigManager.DocumentDirectoryPath, clientId);
            var fi = new DirectoryInfo(directoryPath);
            if (!fi.Exists)
                fi.Create();
            string fileLocation = String.Format(@"{0}.{1}{2}", Path.GetFileNameWithoutExtension(fileBase.FileName),
                (Guid.NewGuid()).ToString(), Path.GetExtension(fileBase.FileName));
            fileBase.SaveAs(String.Format(@"{0}\{1}", directoryPath, fileLocation));
            return fileLocation;
        }
    }
}