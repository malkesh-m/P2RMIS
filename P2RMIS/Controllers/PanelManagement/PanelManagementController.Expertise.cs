using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.Web.ViewModels.PanelManagement;
using Sra.P2rmis.WebModels.SummaryStatement;
using WebModel = Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.Controllers.PanelManagement
{
    public partial class PanelManagementController
    {
        #region Controller Actions
        /// <summary>
        /// action result for the expertise page
        /// </summary>
        /// <returns>the view of the expertise by panel</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ViewReviewerAssignmentExpertise)]
        public ActionResult Expertise()
        {
            ExpertiseViewModel theViewModel = new ExpertiseViewModel();
            try
            {
                // Set data for panel menu(s)
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                RetrieveOrderOfReviewData(theViewModel);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new ExpertiseViewModel();
                HandleExceptionViaElmah(e);
            }
            return View(theViewModel);
        }

        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ViewReviewerAssignmentExpertise)]
        public ActionResult Expertise(int? SelectedProgramYear, int SelectedPanel)
        {
            ExpertiseViewModel theViewModel = new ExpertiseViewModel();
            try
            {
                if (SelectedProgramYear.HasValue)
                {
                    //
                    // Set the selected value so the drop down displays the selected value
                    //
                    SetProgramYearSession((int)SelectedProgramYear);
                }

                //
                // Set the selected value so the drop down displays the selected value
                //
                SetPanelSession(SelectedPanel);
                // Set data for panel menu(s)
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                RetrieveOrderOfReviewData(theViewModel);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new ExpertiseViewModel();
                HandleExceptionViaElmah(e);
            }
            return View(theViewModel);
        }
        /// <summary>
        /// Saves the assignment type threshold.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="scientistReviewerSortOrder">The scientist reviewer sort order.</param>
        /// <param name="advocateConsumerSortOrder">The advocate consumer sort order.</param>
        /// <param name="specialistReviewerSortOrder">The specialist reviewer sort order.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageReviewerAssignmentExpertise)]
        public ActionResult SaveAssignmentTypeThreshold(int sessionPanelId, int? scientistReviewerSortOrder, int? advocateConsumerSortOrder,
            int? specialistReviewerSortOrder)
        {
            bool returnTrue = false;
            int userId = GetUserId();
            try
            {
                thePanelManagementService.SetAssignmentTypeThreshold(sessionPanelId, scientistReviewerSortOrder, advocateConsumerSortOrder,
                    specialistReviewerSortOrder);
                returnTrue = true;
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { returnTrue = returnTrue }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Retrieve reviewer expertise data from the service layer & organize it for presentation.
        /// </summary>
        /// <param name="theViewModel">The view model for the reviewer experience</param>
        private void RetrieveOrderOfReviewData(ExpertiseViewModel theViewModel)
        {
            //
            // If a panel is selected
            //
            if (theViewModel.SelectedPanel > 0)
            {
                SetPanelSession(theViewModel.SelectedPanel);
                //
                // Retrieve the reviewer data from the service layer
                //
                var anotherContainer = thePanelManagementService.ListReviewerExpertise(theViewModel.SelectedPanel, GetUserId());
                var filteredContainer = SanitizeExpertiseForCoi(anotherContainer.ModelList);
                //
                // and now we organize it
                //
                theViewModel.ExpertiseGrid = GroupExpertiseForDisplay(filteredContainer);
                WebModel.ReviewerExpertise.RatingColor = FormatReviewerRating;
                WebModel.ReviewerExpertise.ReviewerColor = FormatReviewerColor;
                //
                // Then we get the numbers 
                //
                theViewModel.PresentationOrderCounts = thePanelManagementService.CalculatePresentationOrderCounts(filteredContainer);
                theViewModel.ExperienceCounts = thePanelManagementService.CalculateExpertiseCounts(filteredContainer);
                //
                // Now set the flag that indicates if the Release button is shown and releases date information.
                //
                WebModel.IReleasePanelModel releasePanelModel = thePanelManagementService.NewIsReleased(theViewModel.SelectedPanel);
                WebModel.ISessionApplicationScoringSetupModel scoringModel = thePanelManagementService.IsSessionApplicationsScoringSetUp(theViewModel.SelectedPanel);
                theViewModel.SetRelease(releasePanelModel, scoringModel);
                //
                // Get current panel's name if exists
                //
                var sessionPanel = theViewModel.Panels.Where(x => x.PanelId == theViewModel.SelectedPanel).FirstOrDefault();
                if (sessionPanel != null)
                {
                    theViewModel.SessionPanelName = sessionPanel.PanelName;
                    theViewModel.SessionPanelAbbr = sessionPanel.PanelAbbreviation;
                }
                var threshold = thePanelManagementService.GetAssignmentTypeThreshold(sessionPanel.PanelId);
                theViewModel.ScientistReviewerSortOrder = threshold.ScientistReviewerSortOrder;
                theViewModel.AdvocateConsumerSortOrder = threshold.AdvocateConsumerSortOrder;
                theViewModel.SpecialistReviewerSortOrder = threshold.SpecialistReviewerSortOrder;
                theViewModel.CanSupportSpecialistReviewer = threshold.CanSupportSpecialistReviewer;
            }
        }
        /// <summary>
        /// Sanitizes sensitive information for records which the current user is a COI
        /// </summary>
        /// <param name="expertise">Collection of reviewer expertise</param>
        /// <returns>List of expertise with sensitive data removed</returns>
        private List<WebModel.IReviewerExpertise> SanitizeExpertiseForCoi (IEnumerable<WebModel.IReviewerExpertise> expertise)
        {
            var sanitizedExpertise = new List<WebModel.IReviewerExpertise>();
            foreach(var item in expertise)
            {
                if (item.IsCurrentUserCoi)
                {
                    sanitizedExpertise.Add(new WebModel.ReviewerExpertise
                    {
                        LogNumber = item.LogNumber,
                        PanelApplicationId = item.PanelApplicationId,
                        ParticipantId = item.ParticipantId,
                        IsCurrentUserCoi = item.IsCurrentUserCoi,
                        ElevatedChairpersonFlag = item.ElevatedChairpersonFlag,
                        ScientistFlag = item.ScientistFlag,
                        SpecialistFlag = item.SpecialistFlag,
                        ReviewerFirstName = item.ReviewerFirstName,
                        ReviewerLastName = item.ReviewerLastName,
                        ParticipantTypeId = item.ParticipantTypeId,
                        ParticipantType = item.ParticipantType,
                        ParticipantTypeName = item.ParticipantTypeName,
                        ParticipationRoleAbbrev = item.ParticipationRoleAbbrev,
                        ApplicationId = item.ApplicationId
                    });
                }
                else
                    sanitizedExpertise.Add(item);
            }
            return sanitizedExpertise;
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageReviewerAssignmentExpertise)]
        public ActionResult ThresholdAssignmentModal()
        {
            return PartialView(ViewNames.Threshold);
        }
        /// <summary>
        /// Get reviewer assignment data
        /// </summary>
        /// <param name="logNumber">the log number</param>
        /// <param name="reviewerName">the reviewer name</param>
        /// <param name="panelApplicationId">the panel application identifier</param>
        /// <param name="panelUserAssignmentId">the panel user assignment identifier</param>
        /// <returns>the partial view for the reviewer assignment modal</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetReviewerAssignment(string logNumber, string reviewerName, int panelApplicationId, int panelUserAssignmentId, bool popupBox, bool isElevatedChairperson)
        {
            ReviewerAssignmentViewModel theViewModel = new ReviewerAssignmentViewModel();
            try
            {
                theViewModel.LogNumber = logNumber;
                theViewModel.ReviewerName = reviewerName;
                theViewModel.PanelApplicationId = panelApplicationId;
                theViewModel.PanelUserAssignmentId = panelUserAssignmentId;

                // Get panel session identifier
                int panelId = GetPanelSession();
                if (panelId > 0)
                {
                    var assignmentTypeListContainer = thePanelManagementService.GetPanelSessionAssignmentTypeList(panelId);
                    theViewModel.ClientAssignmentTypeList = assignmentTypeListContainer.ModelList.ToList();
                    theViewModel.AddClientUnAssignmentType();

                    var presentationOrderContainer = thePanelManagementService.ListPresentationOrder(panelApplicationId);
                    theViewModel.PresentationOrderList = presentationOrderContainer.ModelList.ToList();

                    var clientCoiTypeContainer = thePanelManagementService.GetPanelSessionCoiTypeList(panelId);
                    theViewModel.ClientCoiList = clientCoiTypeContainer.ModelList.ToList();

                    var clientExpertiseRatingContainer = thePanelManagementService.GetPanelSessionClientExpertiseRatingList(panelId);
                    theViewModel.ClientExpertiseRatingList = clientExpertiseRatingContainer.ModelList.ToList();
                    // Remove "COI" from the Expertise drop-down list
                    var coiExpertise = theViewModel.ClientExpertiseRatingList.FirstOrDefault(x => x.ClientExpertiseRatingAbbreviation == Invariables.ReviewerExpertise.CoiExpetise);
                    if (coiExpertise != null)
                    {
                        theViewModel.ClientExpertiseRatingList.Remove(coiExpertise);
                        theViewModel.CurrSessionCoiExpertiseRatingId = coiExpertise.ClientExpertiseRatingId;
                    }

                    var assignmentContainer = thePanelManagementService.GetExpertiseAssignments(panelId, panelApplicationId, panelUserAssignmentId);
                    var assignmentList = assignmentContainer.ModelList.ToList();
                    if (assignmentList.Count > 0)
                    {
                        theViewModel.AssignmentTypeId = assignmentList[0].AssignmentTypeId;
                        theViewModel.ClientAssignmentTypeId = assignmentList[0].ClientAssignmentTypeId;
                        theViewModel.PresentationOrder = assignmentList[0].SortOrder;
                        theViewModel.ClientCoiTypeId = assignmentList[0].CoiTypelId;
                        theViewModel.ClientExpertiseRatingId = assignmentList[0].ClientExpertiseRatingId;
                        theViewModel.Comment = assignmentList[0].ExpertiseComments;
                    }
                    //
                    // We now have a bit of patch up work here.  Since the service layer retrieved all presentation order that are not taken
                    // we need to add in the current reviewer's if they have one.
                    //
                    if (theViewModel.PresentationOrder.HasValue)
                    {
                        theViewModel.PresentationOrderList.Add(theViewModel.PresentationOrder.Value);
                        theViewModel.PresentationOrderList.Sort();
                    }
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                theViewModel = new ReviewerAssignmentViewModel();
                HandleExceptionViaElmah(e);
            }
            // send view model to view page
            if (popupBox)
            {
                return Json(theViewModel, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(isElevatedChairperson ? ViewNames.ElevatedChairpersonAssignment : ViewNames.ReviewerAssignment, theViewModel);
            }
        }

        /// <summary>
        /// Saves the assignment (assign/unassign/expertise/coi)
        /// </summary>
        /// <param name="clientExpertiseRatingId">Expertise reviewer id</param>
        /// <param name="presentationOrder">Presentation order</param>
        /// <param name="clientAssignmentTypeId">Client Type identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="comment">COI comment</param>
        /// <param name="forceUnAssignAction">Whether to force UnAssign action even if there are workflow(s)/critique(s) associated</param>
        /// <param name="clientAssignmentChanged">Indicates if the client assignment has changed</param>
        /// <returns>the updated expertise model back to the expertise page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageReviewerAssignmentExpertise)]
        public ActionResult SaveAssignment(int? clientExpertiseRatingId, int? clientCoiTypeId, int? presentationOrder, int? clientAssignmentTypeId, int panelApplicationId, int panelUserAssignmentId, string comment, bool forceUnAssignAction, bool clientAssignmentChanged)
        {
            string message = string.Empty;
            try
            {
                //call the bl service to decide what to do kick back error/assign/un-assign
                ReviewerAssignmentStatus status = thePanelManagementService.SaveAssignment(clientExpertiseRatingId, clientCoiTypeId, presentationOrder, clientAssignmentTypeId, panelApplicationId,
                        panelUserAssignmentId, comment, forceUnAssignAction, GetUserId(), clientAssignmentChanged);
                message = GetMessage(status);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                HandleExceptionViaElmah(e);
            }
            return string.IsNullOrEmpty(message) ? 
                Json(true, JsonRequestBehavior.AllowGet) : 
                Json(message, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Release a session panel's applications.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Text message indicating status of release request</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageReviewerAssignmentExpertise)]
        public JsonResult ReleaseApplications(int sessionPanelId)
        {
            string message = P2rmisMessages.PanelManagement.ApplicationsReleasedFailure;
            ReleaseStatus result = ReleaseStatus.Default;
            string releaseDateTime = string.Empty;

            try
            {
                result = thePanelManagementService.ReleaseApplications(sessionPanelId, GetUserId());
                WebModel.IReleasePanelModel releasePanelModel = thePanelManagementService.NewIsReleased(sessionPanelId);
                releaseDateTime = (releasePanelModel.IsReleased) ? MessageService.ReleaseAssignmentMessage(releasePanelModel.ReleaseDate.Value) : string.Empty;
                message = P2rmisMessages.PanelManagement.ReleaseStatusMessage(result);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                HandleExceptionViaElmah(e);
            }
            return Json(new { hideButton = HideButton(result), message = message, releaseDate = releaseDateTime }, JsonRequestBehavior.AllowGet);
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageReviewerAssignmentExpertise)]
        public ActionResult NewRequestReviewerTransfer(int currentPanelId, string currentPanelName)
        {
            //
            // Create the view model & set what information we need for display
            //
            RequestTransferViewModel viewModel = new RequestTransferViewModel();
            viewModel.CurrentPanel = currentPanelName;
            try
            {
                //
                // Populate the list of target panels.
                //
                var panelNames = thePanelManagementService.ListSiblingPanelNames(currentPanelId);
                viewModel.AvailablePanels = panelNames.ModelList.ToList<WebModel.ITransferPanelModel>();
                //
                // Populate the list of reviewers.
                ////
                var reviewerNames = thePanelManagementService.ListReviewerNames(currentPanelId);
                viewModel.AvailableReviewers = reviewerNames.ModelList.ToList<IUserModel>();
                UserModel.NameFormatter = ViewHelpers.ConstructNameWithSpace;
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                viewModel = new RequestTransferViewModel();
                HandleExceptionViaElmah(e);
            }
            return PartialView(ViewNames.NewRequestReviewerTransfer, viewModel);
        }
        /// <summary>
        /// Controller action for displaying the modal window to create a transfer reviewer request transfer 
        /// email to the help desk.
        /// </summary>
        /// <param name="currentPanelId">Panel identifier of the panel the application is currently assigned to</param>
        /// <param name="currentPanelName">Panel name of the panel the application is currently assigned to</param>
        /// <returns>Application transfer modal window partial view.</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageReviewerAssignmentExpertise)]
        public ActionResult RequestReviewerTransfer(int currentPanelId, string currentPanelName)
        {
            //
            // Create the view model & set what information we need for display
            //
            RequestTransferViewModel viewModel = new RequestTransferViewModel();
            viewModel.CurrentPanel = currentPanelName;
            try
            {
                //
                // Populate the list of target panels.
                //
                var panelNames = thePanelManagementService.ListSiblingPanelNames(currentPanelId);
                viewModel.AvailablePanels = panelNames.ModelList.ToList<WebModel.ITransferPanelModel>();
                //
                // Populate the list of reviewers.
                ////
                var reviewerNames = thePanelManagementService.ListReviewerNames(currentPanelId);
                viewModel.AvailableReviewers = reviewerNames.ModelList.ToList<IUserModel>();
                UserModel.NameFormatter = ViewHelpers.ConstructNameWithSpace;
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                viewModel = new RequestTransferViewModel();
                HandleExceptionViaElmah(e);
            }
            return PartialView(ViewNames.RequestReviewerTransfer, viewModel);
        }
        /// <summary>
        /// Emails a transfer reviewer request to the P2RMIS help desk and logs the message.
        /// </summary>
        /// <param name="reviewerName">Reviewer's name</param>
        /// <param name="SourcePanelId">Source panel identifier</param>
        /// <param name="sourcePanelName">Source panel's name</param>
        /// <param name="sourcePanelAbbr">Source panel's abbreviation</param>
        /// <param name="targetPanelId">Target panel's identifier</param>
        /// <param name="comment">Comment</param>
        /// <returns>Status indicator</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageReviewerAssignmentExpertise)]
        public JsonResult RequestReviewerTransferProcess(string reviewerName, int sourcePanelId, string sourcePanelName, string sourcePanelAbbr, int? targetPanelId, string comment, bool isRelease)
        {
            bool status = true;
            try
            {
                int userId = GetUserId();
                if (isRelease)
                {
                    thePanelManagementService.RequestReviewerReleaseProcess(theMailService, reviewerName, sourcePanelId, sourcePanelName, sourcePanelAbbr, comment, userId);
                }
                else
                {
                    thePanelManagementService.RequestReviewerTransferProcess(theMailService, reviewerName, sourcePanelId, sourcePanelName, sourcePanelAbbr, targetPanelId, comment, userId);
                }
            }
            catch (Exception e)
            {
                status = false;
                HandleExceptionViaElmah(e);
            }

            return Json(new { status, error = string.Empty }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region helpers
        /// <summary>
        /// Converts the ReleaseStatus to an indication that the Release button should be hidden
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private bool HideButton(ReleaseStatus status)
        {
            return (status == ReleaseStatus.Success);
        }
        /// <summary>
        /// Groups the reviewer expertise for presentation.  The expertise data is grouped so that it can be presented 
        /// in the following order:
        ///      - scientist reviewers ordered alphabetically (A -> Z)
        ///      - specialist reviewers ordered alphabetically (A -> Z)
        ///      - consumer reviewers ordered alphabetically (A -> Z)
        /// for each application.
        /// </summary>
        /// <param name="dataToGroup">Enumerable collection of IReviewer expertise(unsorted) </param>
        /// <returns>Dictionary (keyed & sorted by application log number) of lists of ReviewerExpertise grouped objects</returns>
        private SortedDictionary<string, List<WebModel.IReviewerExpertise>> GroupExpertiseForDisplay(List<WebModel.IReviewerExpertise> dataToGroup)
        {
            SortedDictionary<string, List<WebModel.IReviewerExpertise>> columns = new SortedDictionary<string, List<WebModel.IReviewerExpertise>>();
            if (dataToGroup != null)
            {
                //
                // Sort all the reviewer's expertise by application.  
                //
                foreach (WebModel.IReviewerExpertise item in dataToGroup)
                {
                    if (!columns.Keys.Contains(item.LogNumber))
                    {
                        columns[item.LogNumber] = new List<WebModel.IReviewerExpertise>();
                    }
                    columns[item.LogNumber].Add(item);
                }
                //
                // Now that the reviewer's expertise it sorted into groups (by application) order it
                // into the desired order.  (Linq makes this painless.)  Then put it in it's proper place.  You may
                // be wondering by the loop cannot be just indexed by columns.Keys.  Somewhere along the line they
                // became more strict with changes to the collection within a loop that was controlled by an iterator
                // built directly off of the collection.  
                //
                foreach (var index in columns.Keys.ToList())
                {
                    columns[index] = columns[index].OrderBy(l => l.ElevatedChairpersonFlag).ThenByDescending(m => m.ScientistFlag).ThenByDescending(n => n.SpecialistFlag).ThenByDescending(o => o.ConsumerFlag)
                        .ThenBy(l => l.ReviewerLastName).ThenBy(p => p.ReviewerFirstName).ThenBy(q => q.ParticipantId).ToList();
                }
            }
            return columns;
        }

        /// <summary>
        /// formats the color id for the reviewer expertise page
        /// </summary>
        /// <param name="rating">the rating of the reviewer</param>
        /// <returns>the rating color id to be used in the view</returns>
        public string FormatReviewerRating(string rating)
        {
            string ratingColor = "";

            if (rating == Invariables.ReviewerExpertise.HighExpertise)
            {
                ratingColor = Invariables.ReviewerExpertise.HighExpertiseViewId;
            }
            else if (rating == Invariables.ReviewerExpertise.MediumExpertise)
            {
                ratingColor = Invariables.ReviewerExpertise.MediumExpertiseViewId;
            }
            else if (rating == Invariables.ReviewerExpertise.LowExpertise)
            {
                ratingColor = Invariables.ReviewerExpertise.LowExpertiseViewId;
            }
            else if (rating == Invariables.ReviewerExpertise.NoExpertise)
            {
                ratingColor = Invariables.ReviewerExpertise.NoExpertiseViewId;
            }
            else if (rating == Invariables.ReviewerExpertise.NoSelectionExpertise)
                ratingColor = Invariables.ReviewerExpertise.NoSelectionExpertiseViewId;
            else if (rating == Invariables.ReviewerExpertise.CoiExpetise)
            {
                ratingColor = Invariables.ReviewerExpertise.CoiExpertiseViewId;
            }

            return ratingColor;
        }
        /// <summary>
        /// formats the reviewer color on the view page
        /// </summary>
        /// <param name="scientistFlag">true/false for if the reviewer is a scientist</param>
        /// <param name="specialistFlag">true/false for if the reviewer is a specialist reviewer</param>
        /// <param name="consumerFlag">true/false for if the reviewer is a consumer reviewer</param>
        /// <returns>the view id to be used in the view page</returns>
        public string FormatReviewerColor(bool scientistFlag, bool specialistFlag, bool consumerFlag)
        {
            string reviewerColor = "";

            if (consumerFlag == true)
            {
                reviewerColor = Invariables.ReviewerExpertise.ConsumerViewId;
            }
            else if (specialistFlag == true)
            {
                reviewerColor = Invariables.ReviewerExpertise.SpecialistViewId;
            }
            else if (scientistFlag == true)
            {
                reviewerColor = Invariables.ReviewerExpertise.ScientistViewId;
            }

            return reviewerColor;
        }
        /// <summary>
        /// Gets message for reviewer assignment status
        /// </summary>
        /// <param name="status">the ReviewerAssignmentStatus value</param>
        /// <returns>the message if failed. Otherwise return an empty string.</returns>
        private string GetMessage(ReviewerAssignmentStatus status)
        {
            string message = string.Empty;
            switch (status)
            {
                case ReviewerAssignmentStatus.PositionOccupied:
                    message = Message.PositionOccupiedMessage;
                    break;
                case ReviewerAssignmentStatus.IncompleteAssignmentData:
                    message = Message.IncompleteAssignmentDataMessage;
                    break;
                case ReviewerAssignmentStatus.ReviewerHasWorkflow:
                    message = Message.ReviewerHasWorkflowMessage;
                    break;
                case ReviewerAssignmentStatus.MissingCoiTypeAndComments:
                    message = Message.MissingCoiTypeAndCommentsMessage;
                    break;
                case ReviewerAssignmentStatus.MissingCoiType:
                    message = Message.MissingCoiTypeMessage;
                    break;
                case ReviewerAssignmentStatus.MissingComments:
                    message = Message.MissingCommentsMessage;
                    break;
            }
            return message;
        }
        #endregion
    }
}