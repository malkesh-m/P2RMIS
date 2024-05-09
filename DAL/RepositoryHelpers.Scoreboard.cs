using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.ApplicationScoring;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Linq implementation of scoreboard Repository methods.
    /// </summary>
    internal partial class RepositoryHelpers
    {

        /// <summary>
        /// Retrieves an Application Pre Meetings Reviewers
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="panelApplicationId">panelApplication identifier</param>
        /// <returns>Enumerable collection of PreMeetingReviewerModel objects</returns>
        internal static IEnumerable<PreMeetingReviewerModel> GetPreMtgReviewers(P2RMISNETEntities context, int panelApplicationId)
        {
            var results = (from pa in context.PanelApplications
                           join pua in context.PanelUserAssignments on pa.SessionPanelId equals pua.SessionPanelId
                           join pava in context.PanelApplicationReviewerAssignments
                           on new { pa.PanelApplicationId, pua.PanelUserAssignmentId } equals new { pava.PanelApplicationId, pava.PanelUserAssignmentId }
                           join appStage in context.ApplicationStages on pava.PanelApplicationId equals appStage.PanelApplicationId
                           join ui in context.UserInfoes on pua.UserId equals ui.UserID
                           join cat in context.ClientAssignmentTypes on pava.ClientAssignmentTypeId equals cat.ClientAssignmentTypeId
                           where pa.PanelApplicationId == panelApplicationId && appStage.ReviewStageId == ReviewStage.Asynchronous
                            && cat.AssignmentTypeId != AssignmentType.COI
                           orderby pava.SortOrder
                           select new PreMeetingReviewerModel
                           {
                               ReviewerFirstName = ui.FirstName,
                               ReviewerLastName = ui.LastName,
                               AssignmentType = cat.AssignmentAbbreviation,
                               PanelUserAssignmentId = pua.PanelUserAssignmentId,
                               AssignmentOrder = pava.SortOrder
                           });
            return results;
        }
        /// <summary>
        /// Retrieves an Application Pre Meetings scores and criteria
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="panelApplicationId">panelApplication identifier</param>
        /// <returns>Enumerable collection of PreMeetingCriteriaModel objects</returns>
        internal static IEnumerable<PreMeetingCriteriaModel> GetPreMtgReviewerScores(P2RMISNETEntities context, int panelApplicationId)
        {
            var results = (from panApp in context.PanelApplications
                           join app in context.Applications on panApp.ApplicationId equals app.ApplicationId
                           join mt in context.MechanismTemplates on app.ProgramMechanismId equals mt.ProgramMechanismId
                           join mte in context.MechanismTemplateElements on mt.MechanismTemplateId equals mte.MechanismTemplateId
                           join panStage in context.PanelStages on panApp.SessionPanelId equals panStage.SessionPanelId
                           join panStageStep in context.PanelStageSteps on panStage.PanelStageId equals panStageStep.PanelStageId into pss
                           //ideally we use the function udfPanelStageLastStep however it was throwing a damn error!
                           from panStageStep in pss.OrderByDescending(o => o.StepOrder).Take(1)
                           join ce in context.ClientElements on mte.ClientElementId equals ce.ClientElementId
                           join mtes in context.MechanismTemplateElementScorings on new { mte.MechanismTemplateElementId, panStageStep.StepTypeId }
                               equals new { mtes.MechanismTemplateElementId, mtes.StepTypeId } into mtesg
                           from mtes in mtesg.DefaultIfEmpty()
                           join css in context.ClientScoringScales on mtes.ClientScoringId equals css.ClientScoringId into cssg
                           from css in cssg.DefaultIfEmpty()
                           join appStage in context.ApplicationStages on panApp.PanelApplicationId equals appStage.PanelApplicationId
                           join at in context.ApplicationTemplates on appStage.ApplicationStageId equals at.ApplicationStageId
                           join ate in context.ApplicationTemplateElements on new { at.ApplicationTemplateId, mte.MechanismTemplateElementId } equals new { ate.ApplicationTemplateId, ate.MechanismTemplateElementId }
                           join mtM in context.MechanismTemplates on app.ProgramMechanismId equals mtM.ProgramMechanismId
                           join mteM in context.MechanismTemplateElements on new { mtM.MechanismTemplateId, ce.ClientElementId } equals new { mteM.MechanismTemplateId, mteM.ClientElementId }
                           join mtesM in context.MechanismTemplateElementScorings on mteM.MechanismTemplateElementId equals mtesM.MechanismTemplateElementId into mtesMg
                           from mtesM in mtesMg.DefaultIfEmpty()
                           join cssM in context.ClientScoringScales on mtesM.ClientScoringId equals cssM.ClientScoringId into cssMg
                           from cssM in cssMg.DefaultIfEmpty()
                           where appStage.PanelApplicationId == panelApplicationId && appStage.ReviewStageId == ReviewStage.Asynchronous
                               && panStage.ReviewStageId == ReviewStage.Asynchronous && mtM.ReviewStageId == ReviewStage.Synchronous
                           orderby mte.SortOrder
                           select new PreMeetingCriteriaModel
                           {
                               CriteriaName = (mte.ShowAbbreviationOnScoreboard)? ce.ElementAbbreviation: ce.ElementDescription,
                               SortOrder = mte.SortOrder,
                               ScoreFlag = mte.ScoreFlag,
                               OverallFlag = mte.OverallFlag,
                               PreMeetingScoreType = css.ScoreType,
                               MeetingScoreType = cssM.ScoreType,
                               PremeetingAdjectivalScale = css.ClientScoringScaleAdjectivals.Select(x => new AdjectivalScale
                               {
                                   ScoringLabel = x.ScoreLabel,
                                   NumericEquivalent = x.NumericEquivalent
                               }),
                               MeetingAdjectivalScale = cssM.ClientScoringScaleAdjectivals.Select(x => new AdjectivalScale
                               {
                                   ScoringLabel = x.ScoreLabel,
                                   NumericEquivalent = x.NumericEquivalent
                               }),
                               CriteriaEvaluations = appStage.ApplicationWorkflows.Select(x => new WebModels.ApplicationScoring.CriteriaEvaluation
                               {
                                   PanelUserAssignmentId = x.PanelUserAssignmentId ?? 0,
                                   Score = x.ApplicationWorkflowSteps.DefaultIfEmpty().OrderByDescending(o => o.StepOrder).FirstOrDefault()
                                    .ApplicationWorkflowStepElements.FirstOrDefault(w => w.ApplicationTemplateElementId == ate.ApplicationTemplateElementId)
                                    .ApplicationWorkflowStepElementContents.FirstOrDefault().Score,
                                   Abstain = (bool?)x.ApplicationWorkflowSteps.DefaultIfEmpty().OrderByDescending(o => o.StepOrder).FirstOrDefault()
                                    .ApplicationWorkflowStepElements.FirstOrDefault(w => w.ApplicationTemplateElementId == ate.ApplicationTemplateElementId)
                                    .ApplicationWorkflowStepElementContents.FirstOrDefault().Abstain ?? false,
                                   IntegerFlag = x.ApplicationWorkflowSteps.DefaultIfEmpty().OrderByDescending(o => o.StepOrder).FirstOrDefault()
                                    .ApplicationWorkflowStepElements.FirstOrDefault(w => w.ApplicationTemplateElementId == ate.ApplicationTemplateElementId)
                                    .ClientScoringScale != null ? x.ApplicationWorkflowSteps.DefaultIfEmpty().OrderByDescending(o => o.StepOrder).FirstOrDefault()
                                    .ApplicationWorkflowStepElements.FirstOrDefault(w => w.ApplicationTemplateElementId == ate.ApplicationTemplateElementId)
                                    .ClientScoringScale.ScoreType == ClientScoringScale.ScoringType.Integer.ToLower() : false
                               })
                           });

            return results;
        }
    }
}
