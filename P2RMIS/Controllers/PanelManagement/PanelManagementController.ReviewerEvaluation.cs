using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.UI.Models;
using WebModel = Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.Controllers.PanelManagement
{
    public partial class PanelManagementController
    {
        #region Controller Actions

        /// <summary>
        /// The reviewer evaulation page
        /// </summary>
        /// <returns>the view model to the page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.EvaluateReviewers)]
        public ActionResult ReviewerEvaluation()
        {
            ReviewerEvaluationViewModel theViewModel = new ReviewerEvaluationViewModel();

            // Set data for panel menu(s)
            SetPanelMenu(theViewModel);
            SetTabs(theViewModel);
            if (theViewModel.SelectedPanel > 0)
            {
                //populate the view model
                SetReviewerEvaluationViewModel(theViewModel);
            }

            return View(theViewModel);
        }

        /// <summary>
        /// The reviewer evaulation page from the http post
        /// </summary>
        /// <returns>the view model to the page</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.EvaluateReviewers)]
        public ActionResult ReviewerEvaluation(int? SelectedProgramYear, int SelectedPanel)
        {
            ReviewerEvaluationViewModel theViewModel = new ReviewerEvaluationViewModel();

            if (SelectedProgramYear.HasValue)
            {
                //
                // Set the selected value so the drop down displays the selected value
                //
                SetProgramYearSession((int)SelectedProgramYear);
            }

            int sessionPanelId = (int)SelectedPanel;
            //
            // Set the selected value so the drop down displays the selected value
            //
            SetPanelSession(sessionPanelId);
            // Set data for panel menu(s)
            SetPanelMenu(theViewModel);
            SetTabs(theViewModel);
            //populate the view model
            SetReviewerEvaluationViewModel(theViewModel);

            return View(theViewModel);
        }
        /// <summary>
        /// Saves the reviewer evaluation inputs by the user
        /// </summary>
        /// <param name="form">the form submitted</param>
        /// <returns>the user to the reviewer evaluation page with updated values</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.EvaluateReviewers)]
        public ActionResult SaveEvaluations(FormCollection form)
        {
            ReviewerEvaluationViewModel theViewModel = new ReviewerEvaluationViewModel();

            var reviewerEvalIds = form.GetValues(ReviewerEvaluationViewModel.FormValues.ReviewerEvalId);
            var assignmentIds = form.GetValues(ReviewerEvaluationViewModel.FormValues.PanelUserAssignment);
            var ratings = form.GetValues(ReviewerEvaluationViewModel.FormValues.Rating);
            var potentialChairFlags = form.GetValues(ReviewerEvaluationViewModel.FormValues.PotentialChairFlag);
            var ratingComments = form.GetValues(ReviewerEvaluationViewModel.FormValues.RatingComments);
            var isChair = form.GetValues(ReviewerEvaluationViewModel.FormValues.IsChair);
            
            List<WebModel.ReviewerEvaluation> saveModel = BuildEvaluationSaveList(reviewerEvalIds, assignmentIds, ratings, potentialChairFlags, ratingComments, isChair);

            thePanelManagementService.SaveReviewerEvaluation(saveModel, GetUserId());

            // Set data for panel menu(s)
            SetPanelMenu(theViewModel);
            SetTabs(theViewModel);
            //populate the view model
            SetReviewerEvaluationViewModel(theViewModel);

            return View("ReviewerEvaluation", theViewModel);
        }

        #endregion

        #region Helpers
        /// <summary>
        /// Sets the reviewer evaluation model with data
        /// </summary>
        /// <param name="theViewModel">the reviewer evaluation view model</param>
        /// <returns>the reviewer evaluation view model populated</returns>
        private ReviewerEvaluationViewModel SetReviewerEvaluationViewModel(ReviewerEvaluationViewModel theViewModel)
        {
            //populate guidance
            var guidance = thePanelManagementService.ListReviewerRatingGuidance(theViewModel.SelectedPanel);
            theViewModel.RatingGuidance = BuildEvaluationGuidanceList(guidance.ModelList);

            //populate the reviewer evaluation data
            var results = thePanelManagementService.ListUserPanelReviewerEvaluations(theViewModel.SelectedPanel, GetUserId());
            theViewModel.ReviewerEvaluation = results.ModelList.ToList<WebModel.IReviewerEvaluation>();

            return theViewModel;
        }

        /// <summary>
        /// Builds save list for saving reviewer evaluation data
        /// </summary>
        /// <param name="reviewerEvalIds">the reviewer evaluation id (will be null if there is not an existing evaluation for that reviewer by that user)</param>
        /// <param name="assignmentIds">the reviewers assignment id</param>
        /// <param name="ratings">the reviewers rating</param>
        /// <param name="potentialChairFlags">whether the user recommends the reviewer to be a potential chair</param>
        /// <param name="ratingComments">the reviewers rating comments</param>
        /// <returns>the built list </returns>
        private List<WebModel.ReviewerEvaluation> BuildEvaluationSaveList(string[] reviewerEvalIds, string[] assignmentIds, string[] ratings, string[] potentialChairFlags, string[] ratingComments, string[] isChair)
        {
            List<WebModel.ReviewerEvaluation> saveModel = new List<WebModel.ReviewerEvaluation>();
            //build the list
            for (int i = 0; i < assignmentIds.Length; i++)
            {
                int? tmpEvalId = (String.IsNullOrEmpty(reviewerEvalIds[i])) ? (int?)null : Convert.ToInt32(reviewerEvalIds[i]);
                int? tmpRating = (String.IsNullOrEmpty(ratings[i])) ? (int?)null : Convert.ToInt32(ratings[i]);

                saveModel.Add(new WebModel.ReviewerEvaluation { ReviewerEvaluationId = (int?)tmpEvalId, PanelUserAssignmentId = Convert.ToInt32(assignmentIds[i]), Rating = (int?)tmpRating, PotentialChairFlag = Convert.ToBoolean(potentialChairFlags[i]), RatingComments = ratingComments[i], ChairFlag = Convert.ToBoolean(isChair[i]) });
            }
            //return the list
            return saveModel;
        }
        /// <summary>
        /// building the evaluation guidance list to be grouped by rating group name
        /// </summary>
        /// <param name="dataToGroup">the data to group</param>
        /// <returns>list grouped by rating group name</returns>
        private SortedDictionary<int, List<WebModel.IRatingGuidance>> BuildEvaluationGuidanceList (IEnumerable<WebModel.IRatingGuidance> dataToGroup)
        {
            SortedDictionary<int, List<WebModel.IRatingGuidance>> columns = new SortedDictionary<int, List<WebModel.IRatingGuidance>>();

            if (dataToGroup != null)
            {
                //
                // Sort all the reviewer's expertise by application.  
                //
                foreach (WebModel.IRatingGuidance item in dataToGroup)
                {
                    if (!columns.Keys.Contains(item.Rating))
                    {
                        columns[item.Rating] = new List<WebModel.IRatingGuidance>();
                    }
                    columns[item.Rating].Add(item);
                }
            }
            return columns;
        }

        #endregion
    }
}