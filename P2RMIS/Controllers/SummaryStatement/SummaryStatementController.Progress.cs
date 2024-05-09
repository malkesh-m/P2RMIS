using System;
using System.IO;
using System.Web.Mvc;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.FileServices;
using Sra.P2rmis.Web.ViewModels.SummaryStatement;
using Sra.P2rmis.WebModels.Files;

namespace Sra.P2rmis.Web.Controllers
{
    public partial class SummaryStatementController
    {
        /// <summary>
        /// Controller methods specific to the Staged (SSM-100) view
        /// </summary>

        #region Services
        /// <summary>
        /// Gets the comments in JSON format.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetComments(int panelApplicationId)
        {
            CommentsViewModel results = new CommentsViewModel();
            //
            // Ask the service for the comments
            //
            var canAccessAdminNote = IsValidPermission(Permissions.SummaryStatement.AccessAdminNote);
            var canAccessGeneralNote = IsValidPermission(Permissions.SummaryStatement.AccessGeneralNote);
            var canAccessDiscussionNote = IsValidPermission(Permissions.SummaryStatement.AccessDiscussionNote);
            var canAccessUnassignedReviewerNote = IsValidPermission(Permissions.SummaryStatement.AccessUnassignedReviewerNote);
            var comments = theSummaryManagementService.GetUserApplicationComments(panelApplicationId, canAccessAdminNote, canAccessDiscussionNote, 
                canAccessGeneralNote, canAccessUnassignedReviewerNote);
            results = new CommentsViewModel(comments);
            //
            // return the populated view model
            //
            return PartialView(ViewNames.Notes, results);
        }
        /// <summary>
        /// Retrieves the backup from the specified worklogid and action.
        /// </summary>
        /// <param name="applicationWorkflowStepWorkLogId">The application workflow step work log identifier.</param>
        /// <param name="logAction">The log action to retreive a backup file for.</param>
        /// <param name="stepName">Name of the step.</param>
        /// <returns>
        /// File if found, otherwise
        /// </returns>
        /// <exception cref="System.ArgumentException">No file found for specified WorkLogId.</exception>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrProcess)]
        public ActionResult RetrieveBackup(int applicationWorkflowStepWorkLogId, string logAction, string stepName)
        {
            var fileUrl = $"/SummaryStatement/RetrieveBackupFile?applicationWorkflowStepWorkLogId={applicationWorkflowStepWorkLogId}&logAction={logAction}&stepName={stepName}&isPreview=true";
            var downloadUrl = $"/SummaryStatement/RetrieveBackupFile?applicationWorkflowStepWorkLogId={applicationWorkflowStepWorkLogId}&logAction={logAction}&stepName={stepName}&isPreview=false";
            return PdfViewer(fileUrl,downloadUrl);
        }
        /// <summary>
        /// Retrieves the backup from the specified worklogid and action.
        /// </summary>
        /// <param name="applicationWorkflowStepWorkLogId">The application workflow step work log identifier.</param>
        /// <param name="logAction">The log action to retreive a backup file for.</param>
        /// <param name="stepName">Name of the step.</param>
        /// <returns>
        /// File if found, otherwise
        /// </returns>
        /// <exception cref="System.ArgumentException">No file found for specified WorkLogId.</exception>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrProcess)]
        public ActionResult RetrieveBackupFile(int applicationWorkflowStepWorkLogId, string logAction, string stepName,bool isPreview)
        {
            IFileResultModel backupFile;
            try
            {
                //call service to get file info and contents
                backupFile =
                    theSummaryManagementService.GetSummaryDocumentBackupFile(applicationWorkflowStepWorkLogId, logAction);
                if (backupFile.FileContent == null)
                    throw new ArgumentException("No file found for specified WorkLogId.");
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return RedirectToAction("FileNotFound", "ErrorPage");
            }
            if (isPreview)
            {
                Stream outPutFile = PdfServices.ConvertToPdf(backupFile.FileContent, ViewHelpers.ConstructFileName(stepName, backupFile.LogNumber, FileConstants.FileExtensions.Docx));
                return new FileStreamResult(outPutFile, FileConstants.MimeTypes.Pdf);
            }
            else
            {
                return File(backupFile.FileContent, backupFile.MimeType, ViewHelpers.ConstructFileName(stepName, backupFile.LogNumber, FileConstants.FileExtensions.Docx));
            }
        }       
        #endregion
    }
}