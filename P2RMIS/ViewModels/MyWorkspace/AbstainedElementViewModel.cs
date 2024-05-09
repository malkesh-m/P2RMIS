using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.ApplicationScoring;

namespace Sra.P2rmis.Web.UI.Models
{
    public class AbstainedElementViewModel
    {
        // <summary>
        /// Initializes a new instance of the AbstainedElementViewModel class.
        /// </summary>
        public AbstainedElementViewModel() { }
        /// <summary>
        /// Initializes a new instance of the AbstainedElementViewModel class.
        /// </summary>
        /// <param name="incompleteCriteriaNameModel">The incomplete criteria name model.</param>
        public AbstainedElementViewModel(ISaveReviewersCritiquePostAssignmentResultsModel saveReviewersCritiquePostAssignmentResultsModel)
        {
            ApplicationWorkflowStepElementId = saveReviewersCritiquePostAssignmentResultsModel.ApplicationWorkflowStepElementId;
            ApplicationWorkflowStepElementContentId = saveReviewersCritiquePostAssignmentResultsModel.ApplicationWorkflowStepElementContentId;
        }

        #region Attributes
        /// <summary>
        /// ApplicaitonWorkflowStepElement entity identifier
        /// </summary>
        public int ApplicationWorkflowStepElementId { get; set; }
        /// <summary>
        /// ApplicaitonWorkflowStepElementContent entity identifier
        /// </summary>
        public int ApplicationWorkflowStepElementContentId { get; set; }
        #endregion
    }
}