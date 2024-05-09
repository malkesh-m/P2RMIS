using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.CrossCuttingServices.FileServices;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.Files;
using Sra.P2rmis.WebModels.SummaryStatement;
using System.IO;
using System.Text;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// Controller methods specific to the Staged (SSM-100) view
    /// </summary>
    public partial class SummaryStatementController
    {
        #region Services
        /// <summary>
        /// start of controller method for previewing the summary statement
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="applicationWorkflowId">Application workflow identifier</param>
        /// <returns>Partial View (</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrProcessOrReview)]
        public ActionResult Preview(int panelApplicationId, int? applicationWorkflowId)
        {
            EditSummaryStatementViewModel theViewModel = new EditSummaryStatementViewModel();
            try
            {
                if (applicationWorkflowId != null && applicationWorkflowId > 0)
                {
                    GetDataForEditing(theViewModel, (int)applicationWorkflowId);
                }
                else
                {
                    // calls to the BLL to get DL content for step content and app details
                    var stepContent = theSummaryManagementService.GetPreviewApplicationStepContent(panelApplicationId);
                    var appDetail = theSummaryManagementService.GetPreviewApplicationInfoDetail(panelApplicationId);

                    // sort and order the content for presentation
                    List<IStepContentModel> criteria = new List<IStepContentModel>(stepContent.ModelList);
                    var sortedCriteria = ControllerHelpers.OrderContentForPresentation(criteria);
                    var newSortedCriteria = ControllerHelpers.NewOrderContentForPresentation(criteria);
                    theViewModel.OverallCriteria = newSortedCriteria.Item1;
                    theViewModel.ScoredCriteria = newSortedCriteria.Item2;
                    theViewModel.UnScoredCriteria = newSortedCriteria.Item3;
                    theViewModel.DoDisplayAverageScoreTable = DetermineIfAverageScoreTableShouldDisplay(appDetail.IsTriaged, criteria);
                    //if displaying average score table, indvidual score table can be empty, else average score table can be empty
                    if (theViewModel.DoDisplayAverageScoreTable)
                    {
                        theViewModel.AverageScoreTableData = ExtractScoresForAverageScoreTable(criteria);
                        theViewModel.IndividualScoreTableData = new List<SummaryIndividualScoreModel>();
                    }
                    else
                    {
                        theViewModel.IndividualScoreTableData = ExtractScoresForIndividualScoreTable(criteria);
                        theViewModel.AverageScoreTableData = new SummaryAverageScoreModel();
                    }
                    // set the properties in the view model
                    theViewModel.Criteria = sortedCriteria;
                    theViewModel.ApplicationDetails = appDetail;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            // return the populated view model
            return PartialView(ViewNames.Preview, theViewModel);        
        }
        /// <summary>
        /// Gets the document preview.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="isPreview">if set to <c>true</c> [the document is a preview version].</param>
        /// <returns>
        /// File content result
        /// </returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrProcessOrReview)]
        public ActionResult DownloadDocument(int panelApplicationId, bool isPreview)
        {
            var fileUrl = $"/SummaryStatement/DownloadDocumentView?panelApplicationId={panelApplicationId}&isPreview={isPreview}";
            var downloadUrl = $"/SummaryStatement/DownloadDocumentOriginal?panelApplicationId={panelApplicationId}&isPreview={isPreview}";
            return PdfViewer(fileUrl, downloadUrl);

        }
        /// <summary>
        /// Gets the document preview.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="isPreview">if set to <c>true</c> [the document is a preview version].</param>
        /// <returns>
        /// File content result
        /// </returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrProcessOrReview)]
        public ActionResult DownloadDocumentView(int panelApplicationId, bool isPreview)
        {
            byte[] fileContents;
            ActionResult result;
            try
            {
                var fileWithStatus = theSummaryManagementService.GetSummaryDocumentFile(panelApplicationId);
                ISummaryDocumentFileModel file = fileWithStatus.Key;
                fileContents = file.FileContent;
                string fileAffix = isPreview ? Constants.PreviewAffix : string.Empty;
                if (fileWithStatus.Value.IsSuccessful)
                {
                    if (fileContents != null && fileContents.Length > 0)
                    {
                        Stream outPutFile = PdfServices.ConvertToPdf(fileContents, $"{file.LogNumber}{fileAffix}.{FileConstants.FileExtensions.Docx}");
                        result = new FileStreamResult(outPutFile, FileConstants.MimeTypes.Pdf);
                    }
                    else
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(MessageService.FileNoContents));
                        result = File(PdfServices.CreatePdf(MessageService.FileNoContents, string.Empty, BaseUrl,
                        DepPath), FileConstants.MimeTypes.Pdf);
                    }
                }
                else
                {
                    var messages = fileWithStatus.Value.Messages.ToList();
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(messages[0]));
                    result = File(PdfServices.CreatePdf(messages[0], string.Empty, BaseUrl,
                        DepPath), FileConstants.MimeTypes.Pdf);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                result = Content(MessageService.CouldNotGetDocument);
            }
            return result;
        }
        /// <summary>
        /// Gets the document preview.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="isPreview">if set to <c>true</c> [the document is a preview version].</param>
        /// <returns>
        /// File content result
        /// </returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrProcessOrReview)]
        public ActionResult DownloadDocumentOriginal(int panelApplicationId, bool isPreview)
        {
            byte[] fileContents;
            ActionResult result;
            try
            {
                var fileWithStatus = theSummaryManagementService.GetSummaryDocumentFile(panelApplicationId);
                ISummaryDocumentFileModel file = fileWithStatus.Key;
                fileContents = file.FileContent;
                string fileAffix = isPreview ? Constants.PreviewAffix : string.Empty;
                if (fileWithStatus.Value.IsSuccessful)
                {
                    if (fileContents != null && fileContents.Length > 0)
                    {
                        result = File(fileContents, FileConstants.MimeTypes.Docx, $"{file.LogNumber}{fileAffix}.{FileConstants.FileExtensions.Docx}");
                    }
                    else
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(MessageService.FileNoContents));
                        result = Content(MessageService.FileNoContents);
                    }
                }
                else
                {
                    var messages = fileWithStatus.Value.Messages.ToList();
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(messages[0]));
                    result = Content(messages[0]);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                result = Content(MessageService.CouldNotGetDocument);
            }
            return result;
        }
        #endregion
    }
}