using System;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.Web.ViewModels.PanelManagement;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.Controllers.PanelManagement
{
    public partial class PanelManagementController
    {
        #region Controller Actions
        /// <summary>
        /// action result for the PI Modal
        /// <param name="applicationId">Application identifier</param>    
        /// </summary>
        /// <returns>the view of the PI Modal</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult PIInformation(int applicationId)
        {
            ApplicationPIViewModel theViewModel = new ApplicationPIViewModel();
            try
            { 
                // will be one or none ApplicationPIInformation objects in the list
                ApplicationPIInformation app = (ApplicationPIInformation)thePanelManagementService.ApplicationPIInformation(applicationId).ModelList.ToList().SingleOrDefault();
                theViewModel = new ApplicationPIViewModel(app);

                //theViewModel.ApplicationAbstractDocument = thePanelManagementService.ApplicationAbstract(applicationId).ModelList.ToList();
                theViewModel.IsAbstractInDatabase = thePanelManagementService.IsAbstractInDatabase(applicationId);

                theViewModel.ApplicationDocumentFiles = theFileService.GetFileInfo(applicationId).ModelList.OrderBy(x => x.FileInfo.DisplayLabel).ToList();
            }
            catch (Exception e) 
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new ApplicationPIViewModel();
                HandleExceptionViaElmah(e);
            }

            return PartialView(ViewNames.PIInformation, theViewModel);
        }
        #endregion
    }
}