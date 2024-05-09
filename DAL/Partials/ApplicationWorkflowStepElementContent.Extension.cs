using System;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ApplicationWorkflowStepElementContent object.
    /// </summary>
    public partial class ApplicationWorkflowStepElementContent: IStandardDateFields
    {
        #region Constants
        private const string PromoteErrorMessage = "ApplicationWorkflowStepElementContent.Promote received an invalid parameter nextWorkflowStepElementId is [{0}] and userId is [{1}]";
        #endregion
        /// <summary>
        /// Creates an ApplicationWorkflowStepElementContent entity that is populated with the 
        /// current's step values.
        /// </summary>
        /// <param name="nextWorkflowStepElementId">ApplicationWorkflowStepElementId of the next active workflow step</param>
        /// <param name="userId">User identifier</param>
        /// <param name="shouldRemoveMarkup">Whether to remove the markup</param>
        /// <returns>Copy of current ApplicationWorkflowStepElementContent promoted to next workflow step</returns>
        /// <exception cref="ArgumentException">If workflowStepId is less than or equal to zero; userId is less than or equal to zero</exception>
        public virtual ApplicationWorkflowStepElementContent Promote(int nextWorkflowStepElementId, int userId, bool shouldRemoveMarkup)
        {
            ApplicationWorkflowStepElementContent result = null;
            if (IsPromoteParametersValid(nextWorkflowStepElementId, userId))
            {
                //
                // Create a new entity
                //
                result = new ApplicationWorkflowStepElementContent();
                //
                // populate it by copying the data
                //
                result.ApplicationWorkflowStepElementId = nextWorkflowStepElementId;
                result.Score = this.Score;
                result.ContentText = CleanContextText(this.ContentText, shouldRemoveMarkup);
                result.ContentTextNoMarkup = CleanContextText(this.ContentText, true);
                result.Abstain = this.Abstain;
                Helper.UpdateCreatedFields(result, userId);
                Helper.UpdateModifiedFields(result, userId);
            }
            else
            {
                string message = string.Format(PromoteErrorMessage, nextWorkflowStepElementId, userId);
                throw new ArgumentException(message);
           }
            return result;
        }
        /// <summary>
        /// Gets a clean copy of the context text.
        /// </summary>
        /// <param name="contentText">The markup version of the context text</param>
        /// <param name="shouldRemoveMarkup">Whether to remove the markup</param>
        /// <returns>A clean copy of the context text</returns>
        private string CleanContextText(string contentText, bool shouldRemoveMarkup)
        {
            string newContentText = contentText;
            if (shouldRemoveMarkup)
            {
                newContentText = HtmlServices.GetHtmlContentByAcceptingTrackedChanges(contentText);
            }
            return newContentText;
        }
        /// <summary>
        /// Replaces the content text with new content text. Also updates
        /// the associated fields indicated the time & user who did the updating.
        /// </summary>
        /// <param name="contentText"></param>
        /// <param name="userId"></param>
        public void SaveModifiedContent(string contentText, int userId)
        {
            this.ContentText = contentText;
            this.ContentTextNoMarkup = CleanContextText(contentText, true);
            Helper.UpdateModifiedFields(this, userId);
        }
        /// <summary>
        /// Returns the template id associated with this content.
        /// </summary>
        /// <returns><Template identifier/returns>
        public int GetTemplateId()
        {
            return this.ApplicationWorkflowStepElement.ApplicationTemplateElementId;
        }
        /// <summary>
        /// Populates the necessary fields when an ApplicationWorkflowStepElementContent
        /// is created.
        /// </summary>
        /// <param name="stepElemenId">ApplicationWorkflowStepElement identifier</param>
        /// <param name="contextText">Context text to use</param>
        /// <param name="userId">User identifier</param>
        public void Populate(int stepElemenId, string contextText, int userId)
        {
            ApplicationWorkflowStepElementId = stepElemenId;
            ContentText = contextText;
            ContentTextNoMarkup = CleanContextText(contextText, true);
            Helper.UpdateCreatedFields(this, userId);
            Helper.UpdateModifiedFields(this, userId);
        }
        /// <summary>
        /// Populates the necessary fields when an ApplicationWorkflowStepElementContent
        /// is modified.  Text is expected to not contain mark up.
        /// </summary>
        /// <param name="contextText">Context text to use</param>
        public void Populate(string contextText)
        {
            CritiqueRevised = !string.IsNullOrWhiteSpace(ContentText) ? true : false;
            ContentText = contextText;
        }
        /// <summary>
        /// Populates the necessary fields when an ApplicationWorkflowStepElementContent
        /// is modified.  Text is expected to not contain mark up.
        /// </summary>
        /// <param name="contextText">Context text to use</param>
        /// <param name="isRevised">Indicates if the content should be considered revised</param>
        public void Populate(string contextText, bool isRevised)
        {
            CritiqueRevised = !string.IsNullOrWhiteSpace(ContentText) ? isRevised : false;
            ContentText = contextText;
        }
        /// <summary>
        /// Populates the necessary fields for scoring.
        /// </summary>
        /// <param name="score">User entered score value</param>
        /// <param name="abstain">Abstain flag</param>
        public void Populate(decimal? score, bool abstain)
        {
            this.Score = score;
            this.Abstain = abstain;
        }
        /// <summary>
        /// Populate the necessary fields when adding/editing a critique for Post Assignment processing.
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">Parent ApplicationWorkflowStepElement</param>
        /// <param name="contextText">Context text to use</param>
        /// <param name="score">User entered score value</param>
        /// <param name="abstain">Abstain flag</param>
        public void Populate(int applicationWorkflowStepElementId, string contextText, decimal? score, bool abstain)
        {
            this.ApplicationWorkflowStepElementId = applicationWorkflowStepElementId;
            Populate(contextText);
            Populate(score, abstain);
        }
        /// <summary>
        /// Populate the necessary fields when adding/editing a critique for Post Assignment processing.
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">Parent ApplicationWorkflowStepElement</param>
        /// <param name="contextText">Context text to use</param>
        /// <param name="score">User entered score value</param>
        /// <param name="abstain">Abstain flag</param>
        public void Populate(int applicationWorkflowStepElementId, string contextText, decimal? score, bool abstain, bool isRevised)
        {
            this.ApplicationWorkflowStepElementId = applicationWorkflowStepElementId;
            Populate(contextText, isRevised);
            Populate(score, abstain);
        }
        /// <summary>
        /// Get the adjectival the equivalent for an adjectivally scored content.
        /// </summary>
        /// <returns>Adjectival equivalent for adjectivally scored content</returns>
        public string AdjectivalEquivalent()
        {
            return (
                    this.ApplicationWorkflowStepElement != null && 
                    this.ApplicationWorkflowStepElement.ClientScoringScale != null && 
                    ClientScoringScale.IsSameScale(this.ApplicationWorkflowStepElement.ClientScoringScale.ScoreType, ClientScoringScale.ScoringType.Adjectival)
                    )
                ? this.ApplicationWorkflowStepElement.ClientScoringScale.ClientScoringScaleAdjectivals.Where(
                    x => x.NumericEquivalent == this.Score).DefaultIfEmpty(new ClientScoringScaleAdjectival()).First().ScoreLabel
                : string.Empty;
        }
        #region Helpers
        /// <summary>
        /// Tests if the parameters are valid:
        ///  - workflowStepId is greater than 0
        ///  - userId is greater than 0
        /// </summary>
        /// <param name="workflowStepId">ApplicationWorkflowStepElementId of the next active workflow step</param>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
		private bool IsPromoteParametersValid(int workflowStepId, int userId)
        {
            return (
                    (workflowStepId > 0) &&
                    (userId > 0)
                   );
        }
	    #endregion

        
    }
}
