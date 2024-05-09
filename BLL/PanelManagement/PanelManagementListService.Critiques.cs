using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Bll.PanelManagement
{
    /// <summary>
    /// PanelManagementService provides services to return collections of model
    /// data specific to the PanelManagement Application critique processing.
    /// </summary>
    public partial class PanelManagementService
    {
        /// <summary>
        /// Retrieves lists of critiques for the specified panel
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Container of webModelInterfaceType models</returns>
        public PanelCritiqueSummaryModel ManageCritiques(int sessionPanelId)
        {
            ValidateParameter(sessionPanelId, "PanelManagementService.ManageCritiques", "sessionPanelId");

            SessionPanel theSessionPanel = UnitOfWork.SessionPanelRepository.GetByIDWithPanelApplicationInfo(sessionPanelId);
            PanelCritiqueSummaryModel panel = new PanelCritiqueSummaryModel();
            panel.MeetingSessionId = theSessionPanel.MeetingSessionId;

            var panelCritiques = GetApplicationCritiquesForPanel(sessionPanelId).ToList();
            var stageStepModels = UnitOfWork.SessionPanelRepository.GetPanelStageStepsStatus(sessionPanelId);
            var isMeetingPhaseStarted = IsMeetingPhaseStarted(stageStepModels.FirstOrDefault().PanelApplicationId);

            foreach (PanelApplication pa in theSessionPanel.PanelApplications)
            {
                //
                // Add the general panel application
                //
                CritiqueSummaryModel row = new CritiqueSummaryModel(pa.PanelApplicationId, pa.Application.LogNumber, pa.CountOfReviewers());
                row.ApplicationCritique = panelCritiques.FirstOrDefault(x => x.PanelApplicationId == pa.PanelApplicationId);
                row.IsMeetingPhaseStarted = isMeetingPhaseStarted;

                panel.PanelCritiques.Add(row);
                //
                //  Get the counts and dates for the phases
                //
                var d = stageStepModels.Where(x => x.PanelApplicationId == pa.PanelApplicationId).ToList();
                //
                // So we pull the individual phase numbers out and create a new model for it 
                // (Might not need to do the create but just use it - try during integration)
                //
                d.ForEach(q => row.Phases.Add(new PanelStageStepModel(q)));
                //
                // Finally create/update the header information with the counts
                //
                d.ForEach(q => UpdateHeaderInformation(panel.PhaseHeaders, q));
            }

            return panel;
        }
        /// <summary>
        /// Gets application's reviewer and critique information
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>IApplicationCritiqueModel model</returns>
        public IApplicationCritiqueModel GetApplicationCritiques(int panelApplicationId)
        {
            ValidateParameter(panelApplicationId, "PanelManagementService.GetApplicationCritiques", "panelApplicationId");

            var result = UnitOfWork.PanelManagementRepository.GetApplicationCritiques(panelApplicationId);
            return result;
        }

        /// <summary>
        /// Gets application's reviewer and critique information for a panel
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>IApplicationCritiqueModel model collection</returns>
        public IEnumerable<IApplicationCritiqueModel> GetApplicationCritiquesForPanel(int sessionPanelId)
        {
            ValidateParameter(sessionPanelId, "PanelManagementService.GetApplicationCritiques", "sessionPanelId");

            var result = UnitOfWork.PanelManagementRepository.GetApplicationCritiquesForPanel(sessionPanelId);
            return result;
        }
        /// <summary>
        /// Creates if necessary the object representing the header and sum up the criteria counts.
        /// </summary>
        /// <param name="header">Dictionary containing the header structures</param>
        /// <param name="model">View model containing counts</param>
        internal virtual void UpdateHeaderInformation(IDictionary<int, IPanelStageStepModel> header, PanelStageStepModel model)
        {
            // Check and see if the header structure representing the header is already there
            // If not add it
            if (!header.Keys.Contains(model.StepOrder))
            {
                // the constructor copies the necessary counts to initialize
                header[model.StepOrder] = new PanelStageStepModel(model);
            }
            else
            {
                // Update submitted critique count
                header[model.StepOrder].CritiqueAssignmentSubmittedCount += model.CritiqueAssignmentSubmittedCount;
            }
            // Now retrieve the header information & increment the submitted count if applicable
            IPanelStageStepModel headerEntry = header[model.StepOrder];
            headerEntry.ApplicationSubmittedCount += ((model.CritiqueAssignmentCount > 0 ) && (model.CritiqueAssignmentCount == model.CritiqueAssignmentSubmittedCount)) ? 1 : 0;
        }
   }
}
