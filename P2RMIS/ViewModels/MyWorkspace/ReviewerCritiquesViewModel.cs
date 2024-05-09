using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.WebModels.ApplicationScoring;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ReviewerCritiquesViewModel
    {
        /// <summary>
        /// Initializes a new instance of the ReviewerCritiquesViewModel class.
        /// </summary>
        public ReviewerCritiquesViewModel() 
        { 
            Critiques = new List<CritiqueContentViewModel>();
        }

        /// <summary>
        /// Initializes a new instance of the ReviewerCritiquesViewModel class.
        /// </summary>
        /// <param name="reviewerCritiques">Reviewer critiques</param>
        /// <param name="clientScoringScaleLegend">ClientScoringScaleLegendModel for this critique</param>
        public ReviewerCritiquesViewModel(IReviewerCritiques reviewerCritiques, ClientScoringScaleLegendModel clientScoringScaleLegend) : this()
        {
            foreach (var critique in reviewerCritiques.Critiques)
            {
                var critiqueContentViewModel = new CritiqueContentViewModel(critique, reviewerCritiques.IsPanelStarted, reviewerCritiques.IsCurrentReviewStageAsync, reviewerCritiques.IsComplete);
                Critiques.Add(critiqueContentViewModel);
            }
            WorkflowStepName = reviewerCritiques.WorkflowStepName;
            WorkflowId = reviewerCritiques.ApplicationWorkflowId;
            HasNoCritiquesSubmitted = reviewerCritiques.IsCurrentReviewStageAsync && (!reviewerCritiques.IsComplete);
            PrevApplicationWorkflowSteps = reviewerCritiques.PrevApplicationWorkflowSteps.ToList().ConvertAll(x =>  
                new KeyValuePair<int?, string>(x.IsCompleted ? (int?)x.ApplicationWorkflowStepId : null, x.StepName));
            IsCurrentUser = reviewerCritiques.IsCurrentUser;
            ReviewerId = reviewerCritiques.ReviewerId;
            FormattedReviewerName = ViewHelpers.MakeReviewerInformation(reviewerCritiques.ReviewerFirstName, reviewerCritiques.ReviewerLastName, reviewerCritiques.AssignmentAbbreviation,
                reviewerCritiques.ReviewerSlot, reviewerCritiques.Role);
            CanEdit = reviewerCritiques.CanEdit;
            this.ClientScoringScaleLegend = clientScoringScaleLegend;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewerCritiquesViewModel"/> class.
        /// </summary>
        /// <param name="reviewerCritiques">The reviewer critiques.</param>
        /// <param name="clientScoringScaleLegend">The client scoring scale legend.</param>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        public ReviewerCritiquesViewModel(IReviewerCritiques reviewerCritiques, ClientScoringScaleLegendModel clientScoringScaleLegend,
            int panelApplicationId, int sessionPanelId) : this(reviewerCritiques, clientScoringScaleLegend)
        {
            PanelApplicationId = panelApplicationId;
            SessionPanelId = sessionPanelId;
            CanNavigateToPrevSteps = true;
        }
        /// <summary>
        /// Initializes a new instance of the ReviewerCritiquesViewModel class.
        /// </summary>
        /// <param name="reviewerCritiques">The reviewer critiques.</param>
        /// <param name="canEdit">Flag to indicate if the critique can be edited.</param>
        /// <param name="clientScoringScaleLegend">ClientScoringScaleLegendModel for this critique</param>
        public ReviewerCritiquesViewModel(IReviewerCritiques reviewerCritiques, bool canEdit, ClientScoringScaleLegendModel clientScoringScaleLegend)
            : this(reviewerCritiques, clientScoringScaleLegend)
        {
            CanEdit = canEdit;
        }

        /// <summary>
        /// Reviewer name formatted 
        /// </summary>
        public string FormattedReviewerName { get; private set; }

        /// <summary>
        /// Reviewers critiques
        /// </summary>
        public List<CritiqueContentViewModel> Critiques { get; private set; }

        /// <summary>
        /// Flag to indicate if there are no critiques submitted
        /// </summary>
        public bool HasNoCritiquesSubmitted { get; private set;}

        /// <summary>
        /// Workflow identifier
        /// </summary>
        public int WorkflowId { get; private set; }

        /// <summary>
        /// Phase the critique is in
        /// </summary>
        public string WorkflowStepName { get; private set; }

        /// <summary>
        /// Flag to indicate if the user is the current signed-on user
        /// </summary>
        public bool IsCurrentUser { get; private set; }

        /// <summary>
        /// Gets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        public int PanelApplicationId { get; private set; }

        /// <summary>
        /// Gets the session panel identifier.
        /// </summary>
        /// <value>
        /// The session panel identifier.
        /// </value>
        public int SessionPanelId { get; private set; }

        /// <summary>
        /// Reviewer user identifier
        /// </summary>
        public int ReviewerId { get; private set; }

        /// <summary>
        /// Flag to indicate if the current user can edit critiques
        /// </summary>
        public bool CanEdit { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance can navigate to previous steps.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can navigate to previous steps; otherwise, <c>false</c>.
        /// </value>
        public bool CanNavigateToPrevSteps { get; private set; }
        /// <summary>
        /// Client scoring scale legend
        /// </summary>
        public ClientScoringScaleLegendModel ClientScoringScaleLegend { get; set; }
        /// <summary>
        /// Gets or sets the previous application workflow steps.
        /// </summary>
        /// <value>
        /// The previous application workflow steps.
        /// </value>
        public List<KeyValuePair<int?, string>> PrevApplicationWorkflowSteps { get; set; }
        /// <summary>
        /// Sets the IsCurrentReviwer indicator
        /// </summary>
        public void SetIsCurrentReviewer()
        {
            this.IsCurrentUser = true;
        }
        /// <summary>
        /// Formats the scoring instructions line.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string FormatScaleInstructions(int index)
        {
            string result = string.Empty;

            var thisCritique = this.Critiques[index];

            //
            // If there is some type of ScoreType then we need to format an instruction line
            //
            if ((!string.IsNullOrWhiteSpace(thisCritique.ScoreType)) && (thisCritique.ScoreScales.Count() > 1))
            {
                int last = thisCritique.ScoreScales.Count() - 1;

                string low = thisCritique.ScoreScales[0].Value;
                string high = thisCritique.ScoreScales[last].Value;
                result = MessageService.CriterionScoreingScaleInstructions(low, high);
            }
            return result;
        }
        /// <summary>
        /// Formats the scoring label line
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string ScoringLabel(int index)
        {
            string result = string.Empty;
            var thisCritique = this.Critiques[index];
            //
            // If there is some type of ScoreType then we need to format an scoring label line
            //
            if (!string.IsNullOrWhiteSpace(thisCritique.ScoreType))
            {
                result = (thisCritique.IsOverall) ? MakeLabel(this.ClientScoringScaleLegend.Overall) : MakeLabel(this.ClientScoringScaleLegend.Criterion);
            }
            return result;
        }
        /// <summary>
        /// Constructs the scale legend line.
        /// </summary>
        /// <param name="scaleLegends">Enumeration of IScoringScaleLegendModel models</param>
        /// <returns>Scale legend line to display</returns>
        private string MakeLabel(IEnumerable<IScoringScaleLegendModel> scaleLegends)
        {
            string result = string.Empty;
            if (!scaleLegends.IsEmpty())
            {
                IScoringScaleLegendModel highModel = scaleLegends.First();
                IScoringScaleLegendModel lowModel = scaleLegends.Last();

                result = MessageService.CriterionScoreingScaleLabel(lowModel.LowValue, lowModel.LegendItemLabel, highModel.HighValue, highModel.LegendItemLabel);
            }
            return result;
        }
    }
}