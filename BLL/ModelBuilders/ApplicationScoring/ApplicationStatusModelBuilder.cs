using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.ApplicationScoring;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.ApplicationScoring
{
    /// <summary>
    /// Constructs a container of ApplicationStatusModels for a single SessionPanel
    /// </summary>
    internal class ApplicationStatusModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        public ApplicationStatusModelBuilder(IUnitOfWork unitOfWork,  int sessionPanelId)
            : base(unitOfWork)
        {
            this.SessionPanelId = sessionPanelId;
            this.Results = new Container<IApplicationStatusModel>();
            this.Scores = new Dictionary<string, uspViewPanelDetails_Result>();
        }
        #endregion
        #region Attributes
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        private int SessionPanelId { get; set; }
        /// <summary>
        /// ContainerModelBuilder results.
        /// </summary>
        public Container<IApplicationStatusModel> Results { get; private set; }
        /// <summary>
        /// SessionPanel scores
        /// </summary>
        protected Dictionary<string, uspViewPanelDetails_Result> Scores { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// Retrieve the SessionPanel applications status and build the model to return.
        /// </summary>
        protected virtual void GetPanelStatus()
        {
            List<IApplicationStatusModel> list = new List<IApplicationStatusModel>();
            //
            // Retrieve the SessionPanel we need to build ApplicationStatusModels for
            //
            SessionPanel sessionPanelEntity = GetThisSessionPanel(SessionPanelId);
            //
            // Then for the SessionPanel.....
            //
            IEnumerable<ApplicationStatusModel> b = sessionPanelEntity.
                //
                // we take all the PanelApplications
                //
                PanelApplications.
                //
                // then all we do is build the web model.  There should only be a single ReviewStatus for the PanelApplication.
                //
                Select(x => new ApplicationStatusModel(x.ApplicationId, x.DetermineApplicationStatus(), x.IsActiveStatus(), x.IsCurrentlyScoring(), x.PanelApplicationId, x.SessionPanelId));
            //
            // Finally set the list of templates to return and Bob is your uncle.
            //
            Results.ModelList = b.ToList();
        }
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            GetPanelStatus();
            GetPanelScores();
            MergeScoresWithStatus();
        }
        /// <summary>
        /// Get the SessionPanel application's scores.
        /// </summary>
        protected virtual void GetPanelScores()
        {
            //
            // Retrieve the scores for each application on the panel.  
            //
            IEnumerable<uspViewPanelDetails_Result> sessionPanelScores = UnitOfWork.ViewPanelDetailsRepository.GetApplicationDetailsForPanel(this.SessionPanelId, this.UserId);
            //
            // Now build a dictionary to merge with the state
            //
            foreach (uspViewPanelDetails_Result applicationDetail in sessionPanelScores)
            {
                Scores.Add(applicationDetail.PanelApplicationId.Value.ToString(), applicationDetail);
            }
        }
        /// <summary>
        /// Merge the application scores with the states.
        /// </summary>
        protected virtual void MergeScoresWithStatus()
        {
            //
            // For each of the status results, we access the scores dictionary
            // by the PanelApplicationId.  If there is one (and there should be)
            // we updated the status object with the scores.
            //
            foreach (IApplicationStatusModel model in Results.ModelList)
            {
                uspViewPanelDetails_Result result = Scores[model.PanelApplicationId];
                if (result != null)
                {
                      model.SetScores(result.PossibleScores, result.ActualScores, result.AverageOE);
                }
            }
        }
        #endregion
    }
}
