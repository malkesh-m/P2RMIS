using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the Critique panel pop-up window
    /// </summary>
    public class CritiquePanelViewModel
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationInformationViewModel class.
        /// </summary>
        public CritiquePanelViewModel() 
        {
            ApplicationInformation = new ApplicationInformationViewModel();
            ReviewerCritiques = new ReviewerCritiquesViewModel();
        }

        /// <summary>
        /// Initializes a new instance of the ApplicationInformationViewModel class.
        /// </summary>
        /// <param name="reviewerCritiques">The reviewer critiques.</param>
        /// <param name="applicationInformation">The application information.</param>
        /// <param name="critiquePhase">The critique phase.</param>
        /// <param name="critiqueReviewerOrderedList">The critique reviewer ordered list.</param>
        /// <param name="showCritiqueIcons">The show critique icons.</param>
        /// <param name="clientScoringScaleLegend">ClientScoringScaleLegendModel for this critique</param>
        public CritiquePanelViewModel(IReviewerCritiques reviewerCritiques, IApplicationInformationModel applicationInformation, IEditCritiquePhaseModel critiquePhase,
            List<ICritiqueReviewerOrderedModel> critiqueReviewerOrderedList, ClientScoringScaleLegendModel clientScoringScaleLegend) : this()
        {
            ApplicationInformation = new ApplicationInformationViewModel(applicationInformation, critiquePhase, critiqueReviewerOrderedList);

            ReviewerCritiques = new ReviewerCritiquesViewModel(reviewerCritiques, false, clientScoringScaleLegend);
        }      

        /// <summary>
        /// List of reviewer critique information
        /// </summary>
        public ReviewerCritiquesViewModel ReviewerCritiques { get; private set; }
        
        /// <summary>
        /// Application information
        /// </summary>
        public ApplicationInformationViewModel ApplicationInformation { get; private set; }

        /// <summary>
        /// Application status
        /// </summary>
        public string ApplicationStatus { get; set; }

        /// <summary>
        /// Panel application identifier
        /// </summary>
        public int PanelApplicationId { get; set; }

        /// <summary>
        /// Session panel identifier
        /// </summary>
        public int SessionPanelId { get; set; }        
    }
}