using System;
using System.Linq;
using Sra.P2rmis.WebModels.ApplicationScoring;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the Incomplete Critique partial view
    /// </summary>
    public class IncompleteCritiqueViewModel
    {
        /// <summary>
        /// Initializes a new instance of the IncompleteCritiqueViewModel class.
        /// </summary>
        public IncompleteCritiqueViewModel() { }
        /// <summary>
        /// Initializes a new instance of the IncompleteCritiqueViewModel class.
        /// </summary>
        /// <param name="incompleteCriteriaNameModel">The incomplete criteria name model.</param>
        public IncompleteCritiqueViewModel(IIncompleteCriteriaNameModel incompleteCriteriaNameModel)
        {
            CriteriaName = incompleteCriteriaNameModel.CriteriaName;
            ApplicationWorkflowStepElementId = incompleteCriteriaNameModel.ApplicationWorkflowStepElementId;
        }

        #region Attributes
        /// <summary>
        /// Incomplete criteria name
        /// </summary>
        public string CriteriaName { get; set; }
        /// <summary>
        /// ApplicaitonWorkflowStepElement entity identifier
        /// </summary>
        public int ApplicationWorkflowStepElementId { get; set; }
        #endregion
    }
}