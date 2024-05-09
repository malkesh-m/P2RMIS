using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;

namespace Sra.P2rmis.Web.ViewModels.PanelManagement
{
    public interface IPanelCritiqueSummariesViewModel
    {
        /// <summary>
        /// Collection of the critique information for the applications on the panel
        /// </summary>
        List<IPanelCritiqueSummaryViewModel> PanelCritiques { get; set; }
        /// <summary>
        /// Collection of general phase information
        /// </summary>
        List<KeyValuePair<int, IPanelStageStepViewModel>> PhaseHeaders { get; set; }
        /// <summary>
        /// The meeting session identifier containing the session panel.
        /// </summary>
        int? MeetingSessionId { get; set; }
    }

    public class PanelCritiqueSummariesViewModel : IPanelCritiqueSummariesViewModel
    {
        public PanelCritiqueSummariesViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelCritiqueSummariesViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        internal PanelCritiqueSummariesViewModel(PanelCritiqueSummaryModel model, bool hasManageCritiquesPermission, int currentUserId)
        {
            foreach(var header in model.PhaseHeaders)
            {
                PhaseHeaders.Add(new KeyValuePair<int, IPanelStageStepViewModel>(header.Key, new PanelStageStepViewModel((PanelStageStepModel)header.Value)));
            }
            if (model.PanelCritiques != null)
            {
                foreach(var pc in model.PanelCritiques)
                {
                    if (pc.ApplicationCritique.ReviewerCritiques != null)
                    {
                        var isCurrentUserCoi = pc.ApplicationCritique.ReviewerCritiques.Any(x => x.IsCoi && x.ReviewerId == currentUserId);
                        var coiFlag = false;
                        foreach (var rc in pc.ApplicationCritique.ReviewerCritiques)
                        {
                            if (!isCurrentUserCoi)
                            {
                                var critiqueSummaryViewModel = new PanelCritiqueSummaryViewModel(pc.LogNumber, pc.ApplicationCritique.AwardAbbreviation, pc.ApplicationCritique.PiLastName,
                                    pc.ApplicationCritique.PiFirstName, rc.ReviewerLastName, rc.ReviewerFirstName, rc.ReviewerId, rc.AssignmentDescription, rc.AssignmentOrder,
                                    rc.IsCoi, rc.IsReader, pc.PanelApplicationId, isCurrentUserCoi);
                                var phases = rc.CritiquePhases.ToList(); // Critique phase data
                                var pcPhases = pc.Phases.ToList(); // Panel stage step data
                                if (phases.Count > 0)
                                {
                                    for (var i = 0; i < phases.Count; i++)
                                    {
                                        var action = DetermineCritiqueActions(pc.IsMeetingPhaseStarted, phases[i], hasManageCritiquesPermission);
                                        critiqueSummaryViewModel.SetPhase(phases[i], action.ToList(), i, pcPhases[i].IsModPhase,
                                            pcPhases[i].IsModActive, pcPhases[i].IsModReady, pcPhases[i].IsModClosed,
                                            pcPhases[i].ApplicationStageStepId);
                                    }
                                    PanelCritiques.Add(critiqueSummaryViewModel);
                                }
                            }
                            else if (!coiFlag)
                            {
                                var critiqueSummaryViewModel = new PanelCritiqueSummaryViewModel(pc.LogNumber, rc.IsCoi, rc.IsReader, pc.PanelApplicationId, isCurrentUserCoi);
                                PanelCritiques.Add(critiqueSummaryViewModel);
                                coiFlag = true;
                            }
                        }
                    }
                }
            }
            MeetingSessionId = model.MeetingSessionId;
        }

        /// <summary>
        /// Collection of the critique information for the applications on the panel
        /// </summary>
        public List<IPanelCritiqueSummaryViewModel> PanelCritiques { get; set; } = new List<IPanelCritiqueSummaryViewModel>();
        /// <summary>
        /// Collection of general phase information
        /// </summary>
        public List<KeyValuePair<int, IPanelStageStepViewModel>> PhaseHeaders { get; set; } = new List<KeyValuePair<int, IPanelStageStepViewModel>>();
        /// <summary>
        /// The meeting session identifier containing the session panel.
        /// </summary>
        public int? MeetingSessionId { get; set; }

        #region Helpers
        /// <summary>
        /// Determine the critique actions available to the user in this view
        /// </summary>
        /// <param name="isMeetingPhaseStarted">Whether the meeting phase has been started</param>
        /// <param name="thisCritiquesInformation">Critique information object</param>
        /// <returns>Collection of actions available(View, Submit or Reset to Edit)</returns>
        /// <remarks>To be refactored</remarks>
        private ICollection<string> DetermineCritiqueActions(bool isMeetingPhaseStarted, ICritiquePhaseInformation thisCritiquesInformation, bool hasManageCritiquesPermission)
        {
            List<string> result = new List<string>(3);

            if (IsAbleToEditCritique(thisCritiquesInformation.DateSubmitted, thisCritiquesInformation.PhaseStartDate, thisCritiquesInformation.PhaseEndDate, thisCritiquesInformation.ReOpenStartDate, thisCritiquesInformation.ReOpenEndDate, thisCritiquesInformation.MaxSubmittedStepOrder, thisCritiquesInformation.StepOrder, hasManageCritiquesPermission))
            {
                result.Add(Invariables.Labels.PanelManagement.Critiques.Edit);
            }
            else if (ViewHelpers.IsAbleToViewCritique(thisCritiquesInformation.ContentExists, thisCritiquesInformation.PhaseStartDate))
            {
                result.Add(Invariables.Labels.PanelManagement.Critiques.View);
            }
            if (!isMeetingPhaseStarted && ViewHelpers.IsAbleToResetCritique(thisCritiquesInformation.ContentExists, thisCritiquesInformation.DateSubmitted,
                    thisCritiquesInformation.StepOrder, thisCritiquesInformation.MaxSubmittedStepOrder,
                    thisCritiquesInformation.PhaseStartDate, thisCritiquesInformation.PhaseEndDate,
                    thisCritiquesInformation.ReOpenStartDate, thisCritiquesInformation.ReOpenEndDate, hasManageCritiquesPermission))
            {
                result.Add(Invariables.Labels.PanelManagement.Critiques.ResetToEdit);
            }
            else if (ViewHelpers.IsAbleToSubmitCritique(thisCritiquesInformation.ContentExists, thisCritiquesInformation.DateSubmitted,
                    thisCritiquesInformation.StepOrder, thisCritiquesInformation.MaxStepOrder,
                    thisCritiquesInformation.PhaseStartDate, thisCritiquesInformation.PhaseEndDate,
                    thisCritiquesInformation.ReOpenStartDate, thisCritiquesInformation.ReOpenEndDate, hasManageCritiquesPermission))
            {
                result.Add(Invariables.Labels.PanelManagement.Critiques.Submit);
            }
            return result;
        }
        /// <summary>
        /// Helper checking if the critique is able to be edited.
        /// </summary>
        /// <param name="dateSubmitted">Date/time critique was edited</param>
        /// <param name="startDate">Date/time phase was started</param>
        /// <param name="endDate">Date/time phase was ended</param>
        /// <param name="reOpenStartDate">Date/time phase was reopened</param>
        /// <param name="reOpenEndDate">Date/time reopen phase was ended</param>
        /// <param name="lastSubmittedStep">Last step that has a submitted critique</param>
        /// <param name="currentStep">Step to determine if critique can be edited</param>
        /// <returns></returns>
        private bool IsAbleToEditCritique(Nullable<DateTime> dateSubmitted, DateTime startDate, DateTime endDate, DateTime? reOpenStartDate, DateTime? reOpenEndDate, int lastSubmittedStep, int currentStep, bool hasManageCritiquesPermission)
        {
            bool result = false;
            //
            // if the critique was already submitted for this step or the user does not have permission then we don't need to allow edit
            //
            if (ViewHelpers.IsCritiqueSubmitted(dateSubmitted) || !hasManageCritiquesPermission)
            { }
            //
            // Otherwise the phase must be open.  If it is open then if this step is 1 greater than the last submitted step.
            //
            else if (ViewHelpers.WithInDates(dateSubmitted, startDate, endDate, reOpenStartDate, reOpenEndDate))
            {
                result = currentStep == (lastSubmittedStep + 1);
            }
            return result;
        }
        #endregion
    }
}