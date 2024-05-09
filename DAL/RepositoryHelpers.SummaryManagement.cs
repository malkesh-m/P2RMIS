using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.Reports;
using Sra.P2rmis.WebModels.SummaryStatement;


namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Linq implementation of search methods for Summary Management
    /// </summary>
    internal partial class RepositoryHelpers
    {
        /// <summary>
        /// The default reviewer sort for a summary element that does not have a reviewer.
        /// </summary>
        internal const int DefaultReviewerSort = 99;

        /// <summary>
        /// The sort for the generated row for phase counts, so it can sort second to last.
        /// </summary>
        internal const int GeneratedSort = 99;

        /// <summary>
        /// The sort for the total row for phase counts, so it can sort to last.
        /// </summary>
        internal const int TotalSort = 100;

        /// <summary>
        /// The key value for "command draft".
        /// </summary>
        internal const int CommandDraftKey = 3;

        /// <summary>
        /// The key value for qualifying range.
        /// </summary>
        internal const int QualifyingRangeKey = 4;

        /// <summary>
        /// The label for the generated row of phase counts.
        /// </summary>
        internal const string GeneratedLabel = "Generated";

        /// <summary>
        /// The label for the total row of phase counts.
        /// </summary>
        internal const string TotalLabel = "TOTAL";

        /// <summary>
        /// Label for the transaction which makes workflow step content editable for a user.
        /// </summary>
        internal const string TransactionEditable = "Check-Out";

        /// <summary>
        /// Label for the transaction which makes workflow step content finalized and progresses workflow to next step.
        /// </summary>
        internal const string TransactionFinalized = "Check-In";

        /// <summary>
        /// Folder path for application files on legacy server
        /// </summary>
        internal const string ApplicationFolder = "application_files";

        /// <summary>
        /// Folder path for abstract files on legacy server
        /// </summary>
        internal const string AbstractFolder = "abstracts";

        /// <summary>
        /// Folder path for abstract files on legacy server
        /// </summary>
        internal const string AbstractDisplayLabel = "Abstract";

        /// <summary>
        /// Character used to log number and file name for application files
        /// </summary>
        internal const string ApplicationSuffixSeperator = "_";

        /// <summary>
        /// Extension for pdf files
        /// </summary>
        internal const string ApplicationPdfExtension = ".pdf";

        /// <summary>
        /// Value used to identify the key admin of an application
        /// </summary>
        internal const string AdminAbbreviationKey = "Admin-1";

        /// <summary>
        /// The grant identifier client application information key
        /// </summary>
        internal const int GrantIdInfoKey = 1;
        /// <summary>
        /// Retrieves the applications that are available for summary statement processing based on the user specified
        /// parameters.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <returns>Zero or more Applications</returns>
        /// <remarks>TODO: Remove query grouping</remarks>
        internal static IEnumerable<IAvailableApplications> GetSummaryApplications(P2RMISNETEntities context,
            int panelId, int cycle, int? awardTypeId)
        {
            var results = (from ProgramFY in context.ProgramYears
                           join ProgramPanel in context.ProgramPanels on ProgramFY.ProgramYearId equals ProgramPanel.ProgramYearId
                           join SessionPanel in context.SessionPanels on ProgramPanel.SessionPanelId equals SessionPanel.SessionPanelId
                           join PanelApplication in context.PanelApplications on SessionPanel.SessionPanelId equals PanelApplication.SessionPanelId
                           join Application in context.Applications on PanelApplication.ApplicationId equals
                               Application.ApplicationId
                           join ProgramMechanism in context.ProgramMechanism on Application.ProgramMechanismId equals ProgramMechanism.ProgramMechanismId
                           join ClientAwardType in context.ClientAwardTypes on ProgramMechanism.ClientAwardTypeId equals
                               ClientAwardType.ClientAwardTypeId
                           join ApplicationStage in (from SummaryStage in context.ApplicationStages where SummaryStage.ReviewStageId == ReviewStage.Summary select SummaryStage) on PanelApplication.PanelApplicationId equals ApplicationStage.PanelApplicationId into aaa
                           from ApplicationStage in aaa.DefaultIfEmpty()
                           join ApplicationWorkflow in context.ApplicationWorkflows on ApplicationStage.ApplicationStageId equals
                               ApplicationWorkflow.ApplicationStageId into yyy
                           from ApplicationWorkflow in yyy.DefaultIfEmpty()
                           join ApplicationDefaultWorkflow in context.ApplicationDefaultWorkflows on Application.ApplicationId
                               equals ApplicationDefaultWorkflow.ApplicationId into www
                           from ApplicationDefaultWorkflow in www.DefaultIfEmpty()
                           from PanelScore in context.udfLastUpdatedCritiquePhaseAverageOverall(PanelApplication.PanelApplicationId).DefaultIfEmpty()
                           join CommandDraft in
                               (from AppReviewStatus in context.ApplicationReviewStatus
                                where AppReviewStatus.ReviewStatusId == CommandDraftKey
                                select AppReviewStatus) on PanelApplication.PanelApplicationId equals CommandDraft.PanelApplicationId into
                               gCommandDraft
                           from CommandDraft in gCommandDraft.DefaultIfEmpty()
                           join Qualifying in
                               (from AppReviewStatus in context.ApplicationReviewStatus
                                where AppReviewStatus.ReviewStatusId == QualifyingRangeKey
                                select AppReviewStatus) on PanelApplication.PanelApplicationId equals Qualifying.PanelApplicationId into
                               gQualifying
                           from Qualifying in gQualifying.DefaultIfEmpty()
                           where
                               (SessionPanel.SessionPanelId == panelId) && (cycle == ProgramMechanism.ReceiptCycle) &&
                               ((awardTypeId == null) || (awardTypeId == ProgramMechanism.ClientAwardTypeId))
                           group PanelScore by new
                           {
                               FY = ProgramFY.Year,
                               ProgramAbbreviation = ProgramFY.ClientProgram.ProgramAbbreviation,
                               Cycle = ProgramMechanism.ReceiptCycle,
                               PanelAbrv = SessionPanel.PanelAbbreviation,
                               ApplicationId = Application.LogNumber,
                               MechanismAbbreviation = ClientAwardType.AwardAbbreviation,
                               PanelId = SessionPanel.LegacyPanelId,
                               PiFirstName = Application.ApplicationPersonnels.FirstOrDefault(x => x.PrimaryFlag).FirstName,
                               PiLastName = Application.ApplicationPersonnels.FirstOrDefault(x => x.PrimaryFlag).LastName,
                               ConcatenatedDate = ApplicationWorkflow.DateAssigned,
                               IsCommandDraft = CommandDraft.ApplicationReviewStatusId,
                               IsQualifying = Qualifying.ApplicationReviewStatusId,
                               WorkflowName =
                                   ApplicationWorkflow.ApplicationWorkflowName ?? ApplicationDefaultWorkflow.Workflow.WorkflowName,
                               WorkflowId =
                                   (int?)ApplicationWorkflow.WorkflowId ?? (int?)ApplicationDefaultWorkflow.Workflow.WorkflowId,
                               ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId,
                               ApplicationTemplateId = ApplicationWorkflow.ApplicationTemplateId,
                               Score = PanelScore.AvgScore,
                               PanelApplicationId = PanelApplication.PanelApplicationId
                           }
                into PanelScoreGroup
                           select new AvailableApplications
                           {
                               FY = PanelScoreGroup.Key.FY,
                               ProgramAbbreviation = PanelScoreGroup.Key.ProgramAbbreviation,
                               Cycle = PanelScoreGroup.Key.Cycle,
                               PanelAbbreviation = PanelScoreGroup.Key.PanelAbrv,
                               OverallScore =
                                   PanelScoreGroup.Key.Score,
                               ApplicationId = PanelScoreGroup.Key.ApplicationId,
                               MechanismAbbreviation = PanelScoreGroup.Key.MechanismAbbreviation,
                               PanelId = PanelScoreGroup.Key.PanelId,
                               PiFirstName = PanelScoreGroup.Key.PiFirstName,
                               PiLastName = PanelScoreGroup.Key.PiLastName,
                               ConcatenatedDate = PanelScoreGroup.Key.ConcatenatedDate,
                               IsCommandDraft = PanelScoreGroup.Key.IsCommandDraft == null ? false : true,
                               IsQualifying = PanelScoreGroup.Key.IsQualifying == null ? false : true,
                               Workflow = PanelScoreGroup.Key.WorkflowName ?? "",
                               WorkflowId = PanelScoreGroup.Key.WorkflowId ?? 0,
                               ApplicationWorkflowId = PanelScoreGroup.Key.ApplicationWorkflowId,
                               PanelApplicationId = PanelScoreGroup.Key.PanelApplicationId
                               //ApplicationTemplateId = PanelScoreGroup.Key.ApplicationTemplateId
                           }
                );
            return results;
        }
        /// <summary>
        /// Gets the summary statement applications.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns></returns>
        internal static IEnumerable<ISummaryStatementApplicationModel> GetSummaryStatementApplications(P2RMISNETEntities context, int programId,
            int yearId, int? cycle, int? panelId, int? awardTypeId)
        {
            var results = (from ProgramFY in context.ProgramYears
                           join clientProgram in context.ClientPrograms on ProgramFY.ClientProgramId equals clientProgram.ClientProgramId
                           join ProgramPanel in context.ProgramPanels on ProgramFY.ProgramYearId equals ProgramPanel.ProgramYearId
                           join SessionPanel in context.SessionPanels on ProgramPanel.SessionPanelId equals SessionPanel.SessionPanelId
                           join PanelApplication in context.PanelApplications on SessionPanel.SessionPanelId equals PanelApplication.SessionPanelId
                           join Application in context.Applications on PanelApplication.ApplicationId equals
                               Application.ApplicationId
                           join applicationPersonnel in context.ApplicationPersonnels on new { Application.ApplicationId, PrimaryFlag = true } equals new { applicationPersonnel.ApplicationId, applicationPersonnel.PrimaryFlag } into apg
                           from applicationPersonnel in apg.DefaultIfEmpty()
                           join commandDraft in context.ApplicationReviewStatus on new { PanelApplication.PanelApplicationId, ReviewStatusId = ReviewStatu.PriorityOne } equals new { PanelApplicationId = (int)commandDraft.PanelApplicationId, commandDraft.ReviewStatusId } into cmdg
                           from commandDraft in cmdg.DefaultIfEmpty()
                           join qualifying in context.ApplicationReviewStatus on new { PanelApplication.PanelApplicationId, ReviewStatusId = ReviewStatu.PriorityTwo } equals new { PanelApplicationId = (int)qualifying.PanelApplicationId, qualifying.ReviewStatusId } into qrg
                           from qualifying in qrg.DefaultIfEmpty()
                           join ApplicationReviewStatu in context.ApplicationReviewStatus on new {PanelApplication.PanelApplicationId, ReviewStatusId = ReviewStatu.Triaged} equals new {PanelApplicationId = (int)ApplicationReviewStatu.PanelApplicationId, ApplicationReviewStatu.ReviewStatusId} into ord
                           from ApplicationReviewStatu in ord.DefaultIfEmpty()
                           join ProgramMechanism in context.ProgramMechanism on Application.ProgramMechanismId equals ProgramMechanism.ProgramMechanismId
                           join ClientAwardType in context.ClientAwardTypes on ProgramMechanism.ClientAwardTypeId equals
                               ClientAwardType.ClientAwardTypeId
                           join ApplicationStage in (from SummaryStage in context.ApplicationStages where SummaryStage.ReviewStageId == ReviewStage.Summary select SummaryStage) on PanelApplication.PanelApplicationId equals ApplicationStage.PanelApplicationId into aaa
                           from ApplicationStage in aaa.DefaultIfEmpty()
                           join ApplicationWorkflow in context.ApplicationWorkflows on ApplicationStage.ApplicationStageId equals
                               ApplicationWorkflow.ApplicationStageId into yyy
                           from ApplicationWorkflow in yyy.DefaultIfEmpty()
                           join ApplicationDefaultWorkflow in context.ApplicationDefaultWorkflows on Application.ApplicationId
                               equals ApplicationDefaultWorkflow.ApplicationId into www
                           from ApplicationDefaultWorkflow in www.DefaultIfEmpty()
                           join workflow in context.Workflows on ApplicationDefaultWorkflow.WorkflowId equals workflow.WorkflowId into workflowg
                           from workflow in workflowg.DefaultIfEmpty()
                           join workflowMechanism in context.WorkflowMechanism on new {
                               ProgramMechanism.ProgramMechanismId, StatusId = commandDraft.ApplicationReviewStatusId != null ?
                               ReviewStatu.PriorityOne : (qualifying.ApplicationReviewStatusId != null ? ReviewStatu.PriorityTwo : ReviewStatu.NoPriority) }
                               equals new { ProgramMechanismId = workflowMechanism.MechanismId, StatusId = (int?)workflowMechanism.ReviewStatusId } into wm
                           from workflowMechanism in wm.DefaultIfEmpty()
                           join clientDefaultWorkflow in context.ClientDefaultWorkflows on new {
                               clientProgram.ClientId, StatusId = commandDraft.ApplicationReviewStatusId != null ?
                               ReviewStatu.PriorityOne : (qualifying.ApplicationReviewStatusId != null ? ReviewStatu.PriorityTwo : ReviewStatu.NoPriority)
                               } equals new { clientDefaultWorkflow.ClientId, StatusId = (int?)clientDefaultWorkflow.ReviewStatusId } into cdw
                           from clientDefaultWorkflow in cdw.DefaultIfEmpty()

                           from PanelScore in context.udfLastUpdatedCritiquePhaseAverageOverall(PanelApplication.PanelApplicationId).DefaultIfEmpty()
                           where
                               ProgramFY.ClientProgramId == programId && ProgramFY.ProgramYearId == yearId &&
                               (cycle == null || cycle == ProgramMechanism.ReceiptCycle) &&
                               (panelId == null || panelId == SessionPanel.SessionPanelId) &&
                               (awardTypeId == null || awardTypeId == ProgramMechanism.ClientAwardTypeId)

                           select new SummaryStatementApplicationModel
                           {
                               FY = ProgramFY.Year,
                               ProgramAbbreviation = clientProgram.ProgramAbbreviation,
                               Cycle = ProgramMechanism.ReceiptCycle,
                               PanelAbbreviation = SessionPanel.PanelAbbreviation,
                               ApplicationId = Application.LogNumber,
                               MechanismAbbreviation = ClientAwardType.AwardAbbreviation,
                               PanelId = SessionPanel.LegacyPanelId,
                               PiFirstName = applicationPersonnel.FirstName,
                               PiLastName = applicationPersonnel.LastName,
                               ConcatenatedDate = ApplicationWorkflow.DateAssigned,
                               IsCommandDraft = commandDraft.ApplicationReviewStatusId != null,
                               IsQualifying = qualifying.ApplicationReviewStatusId != null,
                               Workflow = ApplicationWorkflow.ApplicationWorkflowName ?? workflow.WorkflowName ?? workflowMechanism.Workflow.WorkflowName ?? clientDefaultWorkflow.Workflow.WorkflowName,
                               WorkflowId = (int?)ApplicationWorkflow.WorkflowId ?? (int?)workflow.WorkflowId ?? (int?)workflowMechanism.WorkflowId ?? (int?)clientDefaultWorkflow.WorkflowId,
                               ApplicationWorkflowId = (int?)ApplicationWorkflow.ApplicationWorkflowId ?? 0,
                               OverallScore = PanelScore.AvgScore.ToString(),
                               PanelApplicationId = PanelApplication.PanelApplicationId,
                               Order = PanelApplication.ReviewOrder.HasValue ?
                                        ((ApplicationReviewStatu.ReviewStatusId == ReviewStatu.Triaged) ? MessageService.TriagedAbbreviation :
                                        PanelApplication.ReviewOrder.Value.ToString())
                                        : (ApplicationReviewStatu.ReviewStatusId == ReviewStatu.Triaged) ? MessageService.TriagedAbbreviation : string.Empty
                           }
                );
            return results;
        }
        /// <summary>
        /// Gets the summary statement applications.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns></returns>
        internal static IEnumerable<ISummaryStatementProgressModel> GetSummaryStatementApplicationsInProgress(P2RMISNETEntities context, int programId,
            int yearId, int? cycle, int? panelId, int? awardTypeId)
        {
            var results = (from ProgramFY in context.ProgramYears
                           join clientProgram in context.ClientPrograms on ProgramFY.ClientProgramId equals clientProgram.ClientProgramId
                           join ProgramPanel in context.ProgramPanels on ProgramFY.ProgramYearId equals ProgramPanel.ProgramYearId
                           join SessionPanel in context.SessionPanels on ProgramPanel.SessionPanelId equals SessionPanel.SessionPanelId
                           join PanelApplication in context.PanelApplications on SessionPanel.SessionPanelId equals PanelApplication.SessionPanelId
                           join Application in context.Applications on PanelApplication.ApplicationId equals
                               Application.ApplicationId
                           join applicationPersonnel in context.ApplicationPersonnels on new { Application.ApplicationId, PrimaryFlag = true } equals new { applicationPersonnel.ApplicationId, applicationPersonnel.PrimaryFlag } into apg
                           from applicationPersonnel in apg.DefaultIfEmpty()
                           join Budget in context.ApplicationBudgets on Application.ApplicationId equals Budget.ApplicationId into budgetG
                           from Budget in budgetG.DefaultIfEmpty()
                           join commandDraft in context.ApplicationReviewStatus on new { PanelApplication.PanelApplicationId, ReviewStatusId = ReviewStatu.PriorityOne } equals new { PanelApplicationId = (int)commandDraft.PanelApplicationId, commandDraft.ReviewStatusId } into cmdg
                           from commandDraft in cmdg.DefaultIfEmpty()
                           join qualifying in context.ApplicationReviewStatus on new { PanelApplication.PanelApplicationId, ReviewStatusId = ReviewStatu.PriorityTwo } equals new { PanelApplicationId = (int)qualifying.PanelApplicationId, qualifying.ReviewStatusId } into qrg
                           from qualifying in qrg.DefaultIfEmpty()
                           join ProgramMechanism in context.ProgramMechanism on Application.ProgramMechanismId equals ProgramMechanism.ProgramMechanismId
                           join ClientAwardType in context.ClientAwardTypes on ProgramMechanism.ClientAwardTypeId equals
                               ClientAwardType.ClientAwardTypeId
                           join ApplicationStage in context.ApplicationStages on PanelApplication.PanelApplicationId equals ApplicationStage.PanelApplicationId
                           join ApplicationWorkflow in context.ApplicationWorkflows on ApplicationStage.ApplicationStageId equals
                               ApplicationWorkflow.ApplicationStageId
                           from ActiveStep in context.udfApplicationWorkflowActiveStep(ApplicationWorkflow.ApplicationWorkflowId)
                           join ApplicationWorkflowStepAssignment in context.ApplicationWorkflowStepAssignments on
                               ActiveStep.ApplicationWorkflowStepId equals
                               ApplicationWorkflowStepAssignment.ApplicationWorkflowStepId into Awsa
                           from ApplicationWorkflowStepAssignment in Awsa.DefaultIfEmpty()
                           join UserInfo in context.UserInfoes on ApplicationWorkflowStepAssignment.UserId equals UserInfo.UserID
                               into u
                           from UserInfo in u.DefaultIfEmpty()
                           join awswl in
                               (from awswlin in context.ApplicationWorkflowStepWorkLogs
                                where awswlin.CheckInDate == null
                                select awswlin) on ActiveStep.ApplicationWorkflowStepId equals awswl.ApplicationWorkflowStepId
                               into awswlgroup
                           from awswl in awswlgroup.DefaultIfEmpty()
                           from PanelScore in context.udfLastUpdatedCritiquePhaseAverageOverall(PanelApplication.PanelApplicationId).DefaultIfEmpty()
                           join NotesCount in
                               (from Notes in context.UserApplicationComments
                                group Notes by new
                                {
                                    Notes.PanelApplicationId
                                }
                                   into innerGroup
                                select new
                                {
                                    innerGroup.Key.PanelApplicationId,
                                    NoteCount = innerGroup.Count()
                                }) on PanelApplication.PanelApplicationId equals NotesCount.PanelApplicationId into xxx
                           from NotesCount in xxx.DefaultIfEmpty()
                           where
                           ProgramFY.ClientProgramId == programId && ProgramFY.ProgramYearId == yearId &&
                               (cycle == null || cycle == ProgramMechanism.ReceiptCycle) &&
                               (panelId == null || panelId == SessionPanel.SessionPanelId) &&
                               (awardTypeId == null || awardTypeId == ProgramMechanism.ClientAwardTypeId) &&
                               ApplicationStage.ReviewStageId == ReviewStage.Summary
                           select new SummaryStatementProgressModel
                           {
                               FY = ProgramFY.Year,
                               ProgramAbbreviation = clientProgram.ProgramAbbreviation,
                               Cycle = ProgramMechanism.ReceiptCycle,
                               PanelAbbreviation = SessionPanel.PanelAbbreviation,
                               LogNumber = Application.LogNumber,
                               MechanismAbbreviation = ClientAwardType.AwardAbbreviation,
                               PanelId = SessionPanel.LegacyPanelId,
                               PiFirstName = applicationPersonnel.FirstName,
                               PiLastName = applicationPersonnel.LastName,
                               ConcatenatedDate = ApplicationWorkflow.DateAssigned,
                               IsCommandDraft = commandDraft.ApplicationReviewStatusId != null,
                               IsQualifying = qualifying.ApplicationReviewStatusId != null,
                               ApplicationWorkflowId = (int?)ApplicationWorkflow.ApplicationWorkflowId ?? 0,
                               OverallScore = PanelScore.AvgScore,
                               PanelApplicationId = PanelApplication.PanelApplicationId,
                               CurrentStepId = ActiveStep.ApplicationWorkflowStepId,
                               CurrentStepName = ActiveStep.StepName,
                               ApplicationId = Application.ApplicationId,
                               NotesExist = NotesCount.NoteCount != null ? NotesCount.NoteCount > 0 : false,
                               CheckedOutUserFirstName = awswl.CheckOutDate != null ? UserInfo.FirstName : null,
                               CheckedOutUserLastName = awswl.CheckOutDate != null ? UserInfo.LastName : null,
                               CheckOutDateTime = awswl.CheckOutDate,
                               PostDateTime = ApplicationWorkflow.DateAssigned,
                               AdminNotesExist = Budget.Comments == null ? false : true
                           }
                );
            return results;
        }
        /// <summary>
        /// Retrieves the phase counts by panel 
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <param name="userId">the selected user id (optional)</param>
        /// <returns>Zero or more phases and counts</returns>
        internal static IEnumerable<IPhaseCountModel> GetPhaseCounts(P2RMISNETEntities context, int panelId, int cycle,
            int? awardTypeId, int? userId)
        {
            var results = (from aw in context.ApplicationWorkflows
                           join appStage in context.ApplicationStages on aw.ApplicationStageId equals appStage.ApplicationStageId
                           join panApp in context.PanelApplications on appStage.PanelApplicationId equals panApp.PanelApplicationId
                           join app in context.Applications on panApp.ApplicationId equals app.ApplicationId
                           join programMechanism in context.ProgramMechanism on app.ProgramMechanismId equals programMechanism.ProgramMechanismId
                           from ActiveStep in context.udfApplicationWorkflowActiveStep(aw.ApplicationWorkflowId)
                           join qualrange in
                               (from arsq in context.ApplicationReviewStatus
                                where arsq.ReviewStatusId == QualifyingRangeKey
                                select arsq) on panApp.PanelApplicationId equals qualrange.PanelApplicationId into qualrangegroup
                           from qualrange in qualrangegroup.DefaultIfEmpty()
                           join cmddraft in
                               (from arsq in context.ApplicationReviewStatus
                                where arsq.ReviewStatusId == CommandDraftKey
                                select arsq) on panApp.PanelApplicationId equals cmddraft.PanelApplicationId into cmddraftgroup
                           from cmddraft in cmddraftgroup.DefaultIfEmpty()
                           join awswl in
                               (from awswlin in context.ApplicationWorkflowStepWorkLogs
                                where awswlin.CheckInDate == null
                                select awswlin) on ActiveStep.ApplicationWorkflowStepId equals awswl.ApplicationWorkflowStepId into
                               awswlgroup
                           from awswl in awswlgroup.DefaultIfEmpty()
                           where
                               panApp.SessionPanelId == panelId && programMechanism.ReceiptCycle == cycle &&
                               ((awardTypeId == null) || (awardTypeId == programMechanism.ClientAwardTypeId)) &&
                               appStage.ReviewStageId == ReviewStage.Summary
                           group new { ActiveStep, awswl, cmddraft, qualrange, app } by new
                           {
                               ActiveStep.StepName,
                               ActiveStep.StepOrder
                           }
                               into mainGroup
                           select new PhaseCountModel
                           {
                               StepName = mainGroup.Key.StepName,
                               StepOrder = mainGroup.Key.StepOrder,
                               PriorityOneCount =
                                   mainGroup.Count(
                                       x =>
                                           x.awswl.CheckOutDate != null && x.cmddraft.ApplicationReviewStatusId != null &&
                                           x.qualrange.ApplicationReviewStatusId == null),
                               PriorityTwoCount =
                                   mainGroup.Count(
                                       x =>
                                           x.awswl.CheckOutDate != null && x.qualrange.ApplicationReviewStatusId != null &&
                                           x.cmddraft.ApplicationReviewStatusId == null),
                               PriorityOneTwoCount =
                                   mainGroup.Count(
                                       x =>
                                           x.awswl.CheckOutDate != null && x.qualrange.ApplicationReviewStatusId != null &&
                                           x.cmddraft.ApplicationReviewStatusId != null),
                               NoPriorityCount =
                                   mainGroup.Count(
                                       x =>
                                           x.awswl.CheckOutDate != null && x.qualrange.ApplicationReviewStatusId == null &&
                                           x.cmddraft.ApplicationReviewStatusId == null),
                               TotalCheckedCount = mainGroup.Count(x => x.awswl.CheckOutDate != null),
                               NoCheckedCount = mainGroup.Count(x => x.awswl.CheckOutDate == null),
                               TotalCount = mainGroup.Select(x => x.app.ApplicationId).Distinct().Count(),
                               IsDeliverable = false
                           }).Concat
                (from app in context.Applications
                 join panApp in context.PanelApplications on app.ApplicationId equals panApp.ApplicationId
                 join appStage in context.ApplicationStages on panApp.PanelApplicationId equals appStage.PanelApplicationId
                 join aw in context.ApplicationWorkflows on appStage.ApplicationStageId equals aw.ApplicationStageId
                 join programMechanism in context.ProgramMechanism on app.ProgramMechanismId equals programMechanism.ProgramMechanismId
                 join panapp in context.PanelApplications on app.ApplicationId equals panapp.ApplicationId
                 where
                     panapp.SessionPanelId == panelId && programMechanism.ReceiptCycle == cycle &&
                     ((awardTypeId == null) ||
                      (awardTypeId == programMechanism.ClientAwardTypeId)) && appStage.ReviewStageId == ReviewStage.Summary
                 group aw by 1
                     into mainGroup
                 select new PhaseCountModel
                 {
                     StepName = GeneratedLabel,
                     StepOrder = GeneratedSort,
                     PriorityOneCount = 0,
                     PriorityTwoCount = 0,
                     PriorityOneTwoCount = 0,
                     NoPriorityCount = 0,
                     TotalCheckedCount = 0,
                     NoCheckedCount = 0,
                     TotalCount = mainGroup.Count(x => x.DateClosed != null),
                     IsDeliverable = true
                 }).Concat
                (from app in context.Applications
                 join panApp in context.PanelApplications on app.ApplicationId equals panApp.ApplicationId
                 join appStage in context.ApplicationStages on panApp.PanelApplicationId equals appStage.PanelApplicationId
                 join aw in context.ApplicationWorkflows on appStage.ApplicationStageId equals aw.ApplicationStageId
                 join programMechanism in context.ProgramMechanism on app.ProgramMechanismId equals programMechanism.ProgramMechanismId
                 join panapp in context.PanelApplications on app.ApplicationId equals panapp.ApplicationId
                 where
                     panapp.SessionPanelId == panelId && programMechanism.ReceiptCycle == cycle &&
                     ((awardTypeId == null) ||
                     (awardTypeId == programMechanism.ClientAwardTypeId)) && appStage.ReviewStageId == ReviewStage.Summary
                 group aw by 1
                     into mainGroup
                 select new PhaseCountModel
                 {
                     StepName = TotalLabel,
                     StepOrder = TotalSort,
                     PriorityOneCount = 0,
                     PriorityTwoCount = 0,
                     PriorityOneTwoCount = 0,
                     NoPriorityCount = 0,
                     TotalCheckedCount = 0,
                     NoCheckedCount = 0,
                     TotalCount = mainGroup.Count(),
                     IsDeliverable = true
                 }).OrderBy(X => X.StepOrder);
            return results;
        }


        /// <summary>
        /// Retrieves the completed applications.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <param name="userId">the selected user id (optional)</param>
        /// <returns>Zero or more Applications that have completed workflows</returns>
        [Obsolete("Replaced with GetCompletedSummaryStatements")]
        internal static IEnumerable<IApplicationsProgress> GetCompletedProgressApplications(P2RMISNETEntities context,
            int panelId, int cycle, int? awardTypeId, int? userId)
        {
            var results = (from ProgramFY in context.ProgramYears
                           join programPanel in context.ProgramPanels on ProgramFY.ProgramYearId equals programPanel.ProgramYearId
                           join Panel in context.SessionPanels on programPanel.SessionPanelId equals Panel.SessionPanelId
                           join PanelApplication in context.PanelApplications on Panel.SessionPanelId equals PanelApplication.SessionPanelId
                           join Application in context.Applications on PanelApplication.ApplicationId equals Application.ApplicationId
                           join ApplicationStage in context.ApplicationStages on PanelApplication.PanelApplicationId equals ApplicationStage.PanelApplicationId
                           join programMechanism in context.ProgramMechanism on Application.ProgramMechanismId equals programMechanism.ProgramMechanismId
                           join ApplicationWorkflow in
                               context.ApplicationWorkflows
                               on ApplicationStage.ApplicationStageId equals
                               ApplicationWorkflow.ApplicationStageId 
                           join applicationPersonnel in context.ApplicationPersonnels on new { Application.ApplicationId, PrimaryFlag = true } equals new { applicationPersonnel.ApplicationId, applicationPersonnel.PrimaryFlag } into apppGroup
                           from applicationPersonnel in apppGroup.DefaultIfEmpty()
                           from PanelScore in context.udfLastUpdatedCritiquePhaseAverageOverall(PanelApplication.PanelApplicationId).DefaultIfEmpty()
                           join CommandDraft in
                                (from AppReviewStatus in context.ApplicationReviewStatus
                                 where AppReviewStatus.ReviewStatusId == CommandDraftKey
                                 select AppReviewStatus) on PanelApplication.PanelApplicationId equals CommandDraft.PanelApplicationId into
                                gCommandDraft
                           from CommandDraft in gCommandDraft.DefaultIfEmpty()
                           join Qualifying in
                               (from AppReviewStatus in context.ApplicationReviewStatus
                                where AppReviewStatus.ReviewStatusId == QualifyingRangeKey
                                select AppReviewStatus) on PanelApplication.PanelApplicationId equals Qualifying.PanelApplicationId into
                               gQualifying
                           from Qualifying in gQualifying.DefaultIfEmpty()
                           join ApplicationSummaryLog in
                               (from appWorkflowStep in context.ApplicationWorkflowSteps
                                join appSummaryLog in context.ApplicationSummaryLogs
                                on appWorkflowStep.ApplicationWorkflowStepId equals appSummaryLog.ApplicationWorkflowStepId
                                where appSummaryLog.CompletedFlag == true
                                orderby appSummaryLog.ModifiedDate descending
                                select new
                                {
                                    ApplicationWorkflowId = appWorkflowStep.ApplicationWorkflowId,
                                    ModifiedDate = appSummaryLog.ModifiedDate
                                }).Take(1)
                                on ApplicationWorkflow.ApplicationWorkflowId equals ApplicationSummaryLog.ApplicationWorkflowId
                                into gApplicationSummaryLog
                           from ApplicationSummaryLog in gApplicationSummaryLog.DefaultIfEmpty()
                           where
                               (Panel.SessionPanelId == panelId) && ApplicationWorkflow.DateClosed != null &&
                               (awardTypeId == null ||
                                (awardTypeId == programMechanism.ClientAwardTypeId)) &&
                                ApplicationStage.ReviewStageId == ReviewStage.Summary
                           group PanelScore by new
                           {
                               FY = ProgramFY.Year,
                               ProgramAbbreviation = ProgramFY.ClientProgram.ProgramAbbreviation,
                               Cycle = programMechanism.ReceiptCycle,
                               PanelAbbreviation = Panel.PanelAbbreviation,
                               LogNumber = Application.LogNumber,
                               MechanismAbbreviation = programMechanism.ClientAwardType.AwardAbbreviation,
                               PanelId = Panel.SessionPanelId,
                               PiFirstName = applicationPersonnel.FirstName,
                               PiLastName = applicationPersonnel.LastName,
                               IsCommandDraft = CommandDraft.ApplicationReviewStatusId,
                               IsQualifying = Qualifying.ApplicationReviewStatusId,
                               ApplicationId = Application.ApplicationId,
                               PanelApplicationId = PanelApplication.PanelApplicationId,
                               ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId,
                               DateClosed = ApplicationWorkflow.DateClosed,
                               CompletionDate = ApplicationSummaryLog == null ? (DateTime?)null : ApplicationSummaryLog.ModifiedDate
                           }
                           into AppProgressGroup
                           select new ApplicationsProgress
                           {
                               FY = AppProgressGroup.Key.FY,
                               ProgramAbbreviation = AppProgressGroup.Key.ProgramAbbreviation,
                               Cycle = AppProgressGroup.Key.Cycle,
                               PanelAbbreviation = AppProgressGroup.Key.PanelAbbreviation,
                               LogNumber = AppProgressGroup.Key.LogNumber,
                               MechanismAbbreviation = AppProgressGroup.Key.MechanismAbbreviation,
                               PanelId = AppProgressGroup.Key.PanelId,
                               PiFirstName = AppProgressGroup.Key.PiFirstName,
                               PiLastName = AppProgressGroup.Key.PiLastName,
                               IsCommandDraft = AppProgressGroup.Key.IsCommandDraft == null ? false : true,
                               IsQualifying = AppProgressGroup.Key.IsQualifying == null ? false : true,
                               OverallScore = AppProgressGroup.Average(x => x.AvgScore).Value,
                               ApplicationId = AppProgressGroup.Key.ApplicationId,
                               PanelApplicationId = AppProgressGroup.Key.PanelApplicationId,
                               ApplicationWorkflowId = AppProgressGroup.Key.ApplicationWorkflowId,
                               DateClosed = AppProgressGroup.Key.DateClosed,
                               GenerationCompletionDate = AppProgressGroup.Key.CompletionDate
                           }
                );
            return results;
        }
        /// <summary>
        /// Gets the completed applications.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        internal static IEnumerable<IApplicationsProgress> GetCompletedApplications(P2RMISNETEntities context,
            int programId, int yearId, int? panelId, int? cycle, int? awardTypeId, int? userId)
        {
            var results = (from ProgramFY in context.ProgramYears
                           join programPanel in context.ProgramPanels on ProgramFY.ProgramYearId equals programPanel.ProgramYearId
                           join Panel in context.SessionPanels on programPanel.SessionPanelId equals Panel.SessionPanelId
                           join PanelApplication in context.PanelApplications on Panel.SessionPanelId equals PanelApplication.SessionPanelId
                           join Application in context.Applications on PanelApplication.ApplicationId equals Application.ApplicationId
                           join programMechanism in context.ProgramMechanism on Application.ProgramMechanismId equals programMechanism.ProgramMechanismId
                           join AppStage in context.ApplicationStages on PanelApplication.PanelApplicationId equals AppStage.PanelApplicationId
                           join ApplicationWorkflow in
                               context.ApplicationWorkflows
                               on AppStage.ApplicationStageId equals
                               ApplicationWorkflow.ApplicationStageId
                           join applicationPersonnel in context.ApplicationPersonnels on new { Application.ApplicationId, PrimaryFlag = true } equals new { applicationPersonnel.ApplicationId, applicationPersonnel.PrimaryFlag } into apppGroup
                           from applicationPersonnel in apppGroup.DefaultIfEmpty()
                           from PanelScore in context.udfLastUpdatedCritiquePhaseAverageOverall(PanelApplication.PanelApplicationId).DefaultIfEmpty()
                           join CommandDraft in
                                (from AppReviewStatus in context.ApplicationReviewStatus
                                 where AppReviewStatus.ReviewStatusId == CommandDraftKey
                                 select AppReviewStatus) on PanelApplication.PanelApplicationId equals CommandDraft.PanelApplicationId into
                                gCommandDraft
                           from CommandDraft in gCommandDraft.DefaultIfEmpty()
                           join Qualifying in
                               (from AppReviewStatus in context.ApplicationReviewStatus
                                where AppReviewStatus.ReviewStatusId == QualifyingRangeKey
                                select AppReviewStatus) on PanelApplication.PanelApplicationId equals Qualifying.PanelApplicationId into
                               gQualifying
                           from Qualifying in gQualifying.DefaultIfEmpty()
                           join ApplicationSummaryLog in
                               (from appWorkflowStep in context.ApplicationWorkflowSteps
                                join appSummaryLog in context.ApplicationSummaryLogs
                                on appWorkflowStep.ApplicationWorkflowStepId equals appSummaryLog.ApplicationWorkflowStepId
                                where appSummaryLog.CompletedFlag == true
                                group new { appSummaryLog, appWorkflowStep } by new
                                {
                                    AppWorkflowId = appWorkflowStep.ApplicationWorkflowId
                                } into summaryGroup
                                select new
                                {
                                    ApplicationWorkflowId = summaryGroup.Key.AppWorkflowId,
                                    ModifiedDate = summaryGroup.Max(x => x.appSummaryLog.ModifiedDate)
                                })
                                on ApplicationWorkflow.ApplicationWorkflowId equals ApplicationSummaryLog.ApplicationWorkflowId
                                into gApplicationSummaryLog
                           from ApplicationSummaryLog in gApplicationSummaryLog.DefaultIfEmpty()
                           where
                                ProgramFY.ClientProgramId == programId && ProgramFY.ProgramYearId == yearId &&
                               (panelId == null || Panel.SessionPanelId == panelId) && 
                               (cycle == null || programMechanism.ReceiptCycle == cycle) &&
                               (awardTypeId == null || awardTypeId == programMechanism.ClientAwardTypeId) &&
                               ApplicationWorkflow.DateClosed != null && AppStage.ReviewStageId == ReviewStage.Summary
                           group PanelScore by new
                           {
                               FY = ProgramFY.Year,
                               ProgramAbbreviation = ProgramFY.ClientProgram.ProgramAbbreviation,
                               Cycle = programMechanism.ReceiptCycle,
                               PanelAbbreviation = Panel.PanelAbbreviation,
                               LogNumber = Application.LogNumber,
                               MechanismAbbreviation = programMechanism.ClientAwardType.AwardAbbreviation,
                               PanelId = Panel.SessionPanelId,
                               PiFirstName = applicationPersonnel.FirstName,
                               PiLastName = applicationPersonnel.LastName,
                               IsCommandDraft = CommandDraft.ApplicationReviewStatusId,
                               IsQualifying = Qualifying.ApplicationReviewStatusId,
                               ApplicationId = Application.ApplicationId,
                               PanelApplicationId = PanelApplication.PanelApplicationId,
                               ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId,
                               DateClosed = ApplicationWorkflow.DateClosed,
                               CompletionDate = ApplicationSummaryLog == null ? (DateTime?)null : ApplicationSummaryLog.ModifiedDate
                           }
                           into AppProgressGroup
                           select new ApplicationsProgress
                           {
                               FY = AppProgressGroup.Key.FY,
                               ProgramAbbreviation = AppProgressGroup.Key.ProgramAbbreviation,
                               Cycle = AppProgressGroup.Key.Cycle,
                               PanelAbbreviation = AppProgressGroup.Key.PanelAbbreviation,
                               LogNumber = AppProgressGroup.Key.LogNumber,
                               MechanismAbbreviation = AppProgressGroup.Key.MechanismAbbreviation,
                               PanelId = AppProgressGroup.Key.PanelId,
                               PiFirstName = AppProgressGroup.Key.PiFirstName,
                               PiLastName = AppProgressGroup.Key.PiLastName,
                               IsCommandDraft = AppProgressGroup.Key.IsCommandDraft == null ? false : true,
                               IsQualifying = AppProgressGroup.Key.IsQualifying == null ? false : true,
                               OverallScore = AppProgressGroup.Average(x => x.AvgScore).Value,
                               ApplicationId = AppProgressGroup.Key.ApplicationId,
                               PanelApplicationId = AppProgressGroup.Key.PanelApplicationId,
                               ApplicationWorkflowId = AppProgressGroup.Key.ApplicationWorkflowId,
                               DateClosed = AppProgressGroup.Key.DateClosed,
                               GenerationCompletionDate = AppProgressGroup.Key.CompletionDate
                           }
                );
            return results;
        }

        /// <summary>
        /// Retrieves summary information for a panel's applications regardless of whether they have started a summary workflow.
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>Zero or more panels with summary information</returns>
        internal static IEnumerable<ISummaryGroup> GetPanelSummaryAll(P2RMISNETEntities context, int program,
            int fiscalYear, int? cycle, int? panelId, int? awardTypeId)
        {
            var results = (from PrgYr in context.ProgramYears
                           join progPanel in context.ProgramPanels on PrgYr.ProgramYearId equals progPanel.ProgramYearId
                           join Pan in context.SessionPanels on progPanel.SessionPanelId equals Pan.SessionPanelId
                           join PanApp in context.PanelApplications on Pan.SessionPanelId equals PanApp.SessionPanelId
                           join App in context.Applications on PanApp.ApplicationId equals App.ApplicationId
                           join ProgramAward in context.ProgramMechanism on App.ProgramMechanismId equals ProgramAward.ProgramMechanismId
                           join ClientAwardType in context.ClientAwardTypes on ProgramAward.ClientAwardTypeId equals ClientAwardType.ClientAwardTypeId
                           where
                               ((PrgYr.ClientProgramId == program) && (PrgYr.ProgramYearId == fiscalYear)) &&
                               (awardTypeId == null || awardTypeId == ProgramAward.ClientAwardTypeId)
                           group App by new
                           {
                               ProgramAbbreviation = PrgYr.ClientProgram.ProgramAbbreviation,
                               Year = PrgYr.Year,
                               Cycle = ProgramAward.ReceiptCycle,
                               PanelAbbreviation = Pan.PanelAbbreviation,
                               PanelId = Pan.SessionPanelId,
                               Award = (awardTypeId == null) ? string.Empty : ClientAwardType.AwardAbbreviation
                           }
                into TheGroup
                           select new SummaryGroup
                           {
                               ProgramAbbreviation = TheGroup.Key.ProgramAbbreviation,
                               Year = TheGroup.Key.Year,
                               Cycle = TheGroup.Key.Cycle ?? 0,
                               PanelAbbreviation = TheGroup.Key.PanelAbbreviation,
                               PanelId = TheGroup.Key.PanelId,
                               NumberPanelApplications = TheGroup.Count(),
                               Award = TheGroup.Key.Award
                           }
                );
            //
            // Cycle & panelId are optional.  So we check if the query parameters are there then 
            // add then to the query.
            // 
            if (cycle.HasValue)
            {
                results = results.Where(r => r.Cycle == cycle);
            }
            //
            //
            if (panelId.HasValue)
            {
                results = results.Where(r => r.PanelId == panelId);
            }
            //
            return results;
        }

        /// <summary>
        /// Retrieves summary information for a panel's applications that have started a summary workflow.
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>Zero or more panels with summary information</returns>
        internal static IEnumerable<ISummaryGroup> GetPanelSummaries(P2RMISNETEntities context, int program,
            int fiscalYear, int? cycle, int? panelId, int? awardTypeId, int? userId)
        {
            var results = (from PrgYr in context.ProgramYears
                           join progPanel in context.ProgramPanels on PrgYr.ProgramYearId equals progPanel.ProgramYearId
                           join Pan in context.SessionPanels on progPanel.SessionPanelId equals Pan.SessionPanelId
                           join PanApp in context.PanelApplications on Pan.SessionPanelId equals PanApp.SessionPanelId
                           join Application in context.Applications on PanApp.ApplicationId equals Application.ApplicationId
                           join AppStage in context.ApplicationStages on PanApp.PanelApplicationId equals AppStage.PanelApplicationId
                           join ProgramAward in context.ProgramMechanism on Application.ProgramMechanismId equals ProgramAward.ProgramMechanismId
                           join ClientAwardType in context.ClientAwardTypes on ProgramAward.ClientAwardTypeId equals ClientAwardType.ClientAwardTypeId
                           join ApplicationWorkflow in
                    context.ApplicationWorkflows
                    on AppStage.ApplicationStageId equals
                    ApplicationWorkflow.ApplicationStageId 

                           from ActiveStep in context.udfApplicationWorkflowActiveStep(ApplicationWorkflow.ApplicationWorkflowId)
                           join ApplicationWorkflowStepAssignment in context.ApplicationWorkflowStepAssignments on
                               ActiveStep.ApplicationWorkflowStepId equals
                               ApplicationWorkflowStepAssignment.ApplicationWorkflowStepId into Awsa
                           from ApplicationWorkflowStepAssignment in Awsa.DefaultIfEmpty()
                           join UserInfo in context.UserInfoes on ApplicationWorkflowStepAssignment.UserId equals UserInfo.UserID
                               into u
                           from UserInfo in u.DefaultIfEmpty()
                           where
                               ((PrgYr.ClientProgram.ClientProgramId == program) && (PrgYr.ProgramYearId == fiscalYear)) &&
                               (awardTypeId == null ||
                               awardTypeId == ProgramAward.ClientAwardTypeId) &&
                               ((userId == null) || (userId == UserInfo.UserID)) &&
                               AppStage.ReviewStageId == ReviewStage.Summary
                           group Application by new
                           {
                               ProgramAbbreviation = PrgYr.ClientProgram.ProgramAbbreviation,
                               Year = PrgYr.Year,
                               Cycle = ProgramAward.ReceiptCycle,
                               PanelAbbreviation = Pan.PanelAbbreviation,
                               PanelId = Pan.SessionPanelId,
                               Award = (awardTypeId == null) ? string.Empty : ClientAwardType.AwardAbbreviation,
                               FirstName = (userId == null) ? string.Empty : UserInfo.FirstName,
                               LastName = (userId == null) ? string.Empty : UserInfo.LastName
                           }
                into TheGroup
                           select new SummaryGroup
                           {
                               ProgramAbbreviation = TheGroup.Key.ProgramAbbreviation,
                               Year = TheGroup.Key.Year,
                               Cycle = TheGroup.Key.Cycle ?? 0,
                               PanelAbbreviation = TheGroup.Key.PanelAbbreviation,
                               PanelId = TheGroup.Key.PanelId,
                               NumberPanelApplications = TheGroup.Count(),
                               Award = TheGroup.Key.Award,
                               FirstName = TheGroup.Key.FirstName,
                               LastName = TheGroup.Key.LastName
                           }
                );
            //
            // Cycle & panelId are optional.  So we check if the query parameters are there then 
            // add then to the query.
            // 
            if (cycle.HasValue)
            {
                results = results.Where(r => r.Cycle == cycle);
            }
            //
            //
            if (panelId.HasValue)
            {
                results = results.Where(r => r.PanelId == panelId);
            }
            //
            return results;
        }
        /// <summary>
        /// Retrieves summary information for a panel's applications that have started or completed a summary workflow by phase.
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award type identifier (optional)</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>Zero or more panels with summary information</returns>
        internal static IEnumerable<ISummaryGroup> GetPanelSummariesPhases(P2RMISNETEntities context, int program,
            int fiscalYear, int? cycle, int? panelId, int? awardTypeId)
        {
            var results = (from PrgYr in context.ProgramYears
                           join progPanel in context.ProgramPanels on PrgYr.ProgramYearId equals progPanel.ProgramYearId
                           join Pan in context.SessionPanels on progPanel.SessionPanelId equals Pan.SessionPanelId
                           join PanApp in context.PanelApplications on Pan.SessionPanelId equals PanApp.SessionPanelId
                           join Application in context.Applications on PanApp.ApplicationId equals Application.ApplicationId
                           join AppStage in context.ApplicationStages on PanApp.PanelApplicationId equals AppStage.PanelApplicationId
                           join ProgramAward in context.ProgramMechanism on Application.ProgramMechanismId equals ProgramAward.ProgramMechanismId
                           join ClientAwardType in context.ClientAwardTypes on ProgramAward.ClientAwardTypeId equals ClientAwardType.ClientAwardTypeId
                           join ApplicationWorkflow in context.ApplicationWorkflows on AppStage.ApplicationStageId equals
                               ApplicationWorkflow.ApplicationStageId
                           where
                               ((PrgYr.ClientProgramId == program) && (PrgYr.ProgramYearId == fiscalYear)) &&
                               (awardTypeId == null || awardTypeId == ProgramAward.ClientAwardTypeId) &&
                               AppStage.ReviewStageId == ReviewStage.Summary
                           group Application by new
                           {
                               ProgramAbbreviation = PrgYr.ClientProgram.ProgramAbbreviation,
                               Year = PrgYr.Year,
                               Cycle = ProgramAward.ReceiptCycle,
                               PanelAbbreviation = Pan.PanelAbbreviation,
                               PanelId = Pan.SessionPanelId,
                               Award = (awardTypeId == null) ? string.Empty : ClientAwardType.AwardAbbreviation
                           }
                               into TheGroup
                           select new SummaryGroup
                           {
                               ProgramAbbreviation = TheGroup.Key.ProgramAbbreviation,
                               Year = TheGroup.Key.Year,
                               Cycle = TheGroup.Key.Cycle ?? 0,
                               PanelAbbreviation = TheGroup.Key.PanelAbbreviation,
                               PanelId = TheGroup.Key.PanelId,
                               NumberPanelApplications = TheGroup.Count(),
                               Award = TheGroup.Key.Award
                           }
                );
            //
            // Cycle & panelId are optional.  So we check if the query parameters are there then 
            // add then to the query.
            // 
            if (cycle.HasValue)
            {
                results = results.Where(r => r.Cycle == cycle);
            }
            //
            //
            if (panelId.HasValue)
            {
                results = results.Where(r => r.PanelId == panelId);
            }
            //
            return results;
        }

        /// <summary>
        /// Retrieves workflow information for application that user is assigned to one or more child steps.
        /// </summary>
        /// <param name="userId">Identifier for current user</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>Zero or more assigned application workflows</returns>
        internal static IEnumerable<ISummaryAssignedModel> GetAssignedSummaries(P2RMISNETEntities context, int userId)
        {
            var results = (from Usr in context.Users
                           join Awsa in context.ApplicationWorkflowStepAssignments on Usr.UserID equals Awsa.UserId
                           join Aws in context.ApplicationWorkflowSteps on Awsa.ApplicationWorkflowStepId equals
                               Aws.ApplicationWorkflowStepId
                           join Awswl in context.ApplicationWorkflowStepWorkLogs on new { Aws.ApplicationWorkflowStepId, Usr.UserID }
                               equals new { Awswl.ApplicationWorkflowStepId, UserID = Awswl.UserId } into Awswlg
                           from Awswl in Awswlg.DefaultIfEmpty()
                           join Aw in
                               context.ApplicationWorkflows
                               on Aws.ApplicationWorkflowId equals Aw.ApplicationWorkflowId 
                           join AppStage in context.ApplicationStages on Aw.ApplicationStageId equals AppStage.ApplicationStageId
                           join PanApp in context.PanelApplications on AppStage.PanelApplicationId equals PanApp.PanelApplicationId
                           join App in context.Applications on PanApp.ApplicationId equals App.ApplicationId
                           where Usr.UserID == userId && Aw.DateClosed == null && AppStage.ReviewStageId == ReviewStage.Summary
                           group new { Awsa, Awswl } by new
                           {
                               Aw.ApplicationWorkflowId,
                               WorkflowName = Aw.ApplicationWorkflowName,
                               App.LogNumber
                           }
                into TheGroup
                           orderby TheGroup.Key.LogNumber
                           select new SummaryAssignedModel
                           {
                               ApplicationWorkflowId = TheGroup.Key.ApplicationWorkflowId,
                               WorkflowName = TheGroup.Key.WorkflowName,
                               LogNumber = TheGroup.Key.LogNumber,
                               AssignmentDate = TheGroup.Max(x => x.Awsa.CreatedDate ?? default(DateTime)),
                               WorkStartDate = TheGroup.Min(x => x.Awswl.CheckOutDate),
                               WorkEndDate = TheGroup.Max(x => x.Awswl.CheckInDate)
                           });
            return results;
        }

        /// <summary>
        /// Retrieves information about workflow progress for each application that a user is assigned.
        /// </summary>
        /// <param name="userId">Identifier for current user</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>Zero or more workflow steps progress for an assigned application</returns>
        internal static IEnumerable<IWorkflowProgress> GetWorkflowProgress(P2RMISNETEntities context, int userId)
        {
            var results = (from Gas in GetAssignedSummaries(context, userId)
                           join Aws in context.ApplicationWorkflowSteps on Gas.ApplicationWorkflowId equals
                               Aws.ApplicationWorkflowId
                           join Awsa in context.ApplicationWorkflowStepAssignments on
                               new { Aws.ApplicationWorkflowStepId, UserId = userId } equals
                               new { Awsa.ApplicationWorkflowStepId, Awsa.UserId } into Awsag
                           from Awsa in Awsag.DefaultIfEmpty()
                           where Aws.Active == true
                           orderby Aws.ApplicationWorkflowId, Aws.StepOrder
                           select new WorkflowProgress
                           {
                               ApplicationWorkflowId = Aws.ApplicationWorkflowId,
                               StepName = Aws.StepName,
                               StepOrder = Aws.StepOrder,
                               IsCompleted = Aws.Resolution,
                               IsAssigned = (Awsa == null) ? false : (Awsa.AssignmentId == 0) ? false : true
                           });
            return results;
        }

        /// <summary>
        /// Retrieves information about workflow progress for a single application workflow.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application workflow</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>Zero or more workflow steps for a given application workflow</returns>
        internal static IEnumerable<ApplicationWorkflowStepModel> GetSingleWorkflowProgress(P2RMISNETEntities context,
            int applicationWorkflowId)
        {
            var results = (from
                AppWorkflowSteps in context.ApplicationWorkflowSteps
                           where AppWorkflowSteps.ApplicationWorkflowId == applicationWorkflowId &&
                                 AppWorkflowSteps.Active == true
                           orderby AppWorkflowSteps.StepOrder
                           select new ApplicationWorkflowStepModel
                           {
                               ApplicationWorkflowStepId = AppWorkflowSteps.ApplicationWorkflowStepId,
                               ApplicationWorkflowId = AppWorkflowSteps.ApplicationWorkflowId,
                               StepTypeId = AppWorkflowSteps.StepTypeId,
                               StepName = AppWorkflowSteps.StepName,
                               Active = AppWorkflowSteps.Active,
                               StepOrder = AppWorkflowSteps.StepOrder,
                               Resolution = AppWorkflowSteps.Resolution,
                               ResolutionDate = AppWorkflowSteps.ResolutionDate
                           }
                );
            return results;
        }

        /// <summary>
        /// Retrieves information about the application a workflow is for.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <returns>Application details for a single application</returns>
        internal static IApplicationDetailModel GetApplicationDetail(P2RMISNETEntities context,
            int applicationWorkflowId)
        {
            var results = (from AppWorkflow in context.ApplicationWorkflows
                           join AppStage in context.ApplicationStages on AppWorkflow.ApplicationStageId equals AppStage.ApplicationStageId
                           join PanApp in context.PanelApplications on AppStage.PanelApplicationId equals PanApp.PanelApplicationId
                           join App in context.Applications on PanApp.ApplicationId equals App.ApplicationId
                           join programMechanism in context.ProgramMechanism on App.ProgramMechanismId equals programMechanism.ProgramMechanismId
                           join programPanel in context.ProgramPanels on PanApp.SessionPanelId equals programPanel.SessionPanelId
                           join PrgFy in context.ProgramYears on programPanel.ProgramYearId equals PrgFy.ProgramYearId
                           join ClientPrg in context.ClientPrograms on PrgFy.ClientProgramId equals ClientPrg.ClientProgramId

                           join Pan in context.SessionPanels on PanApp.SessionPanelId equals Pan.SessionPanelId
                           join Session in context.MeetingSessions on Pan.MeetingSessionId equals Session.MeetingSessionId
                           join ClientAward in context.ClientAwardTypes on programMechanism.ClientAwardTypeId equals ClientAward.ClientAwardTypeId
                           join Budget in context.ApplicationBudgets on App.ApplicationId equals Budget.ApplicationId into budgetG
                           from Budget in budgetG.DefaultIfEmpty()
                           join applicationPersonnel in context.ApplicationPersonnels on new { App.ApplicationId, PrimaryFlag = true } equals new { applicationPersonnel.ApplicationId, applicationPersonnel.PrimaryFlag } into apppGroup
                           from applicationPersonnel in apppGroup.DefaultIfEmpty()
                           join appInfo in context.ApplicationInfoes on new { App.ApplicationId, ClientApplicationInfoTypeId = GrantIdInfoKey } equals new { appInfo.ApplicationId, appInfo.ClientApplicationInfoTypeId } into appInfoGroup
                           from appInfo in appInfoGroup.DefaultIfEmpty()
                           join applicationAdmin in context.ApplicationPersonnels on new { App.ApplicationId, ApplicationPersonnelTypeAbbreviation = AdminAbbreviationKey } equals new { applicationAdmin.ApplicationId, applicationAdmin.ClientApplicationPersonnelType.ApplicationPersonnelTypeAbbreviation } into appaGroup
                           from applicationAdmin in appaGroup.DefaultIfEmpty()
                           join CommandDraft in
                              (from AppReviewStatus in context.ApplicationReviewStatus
                               where AppReviewStatus.ReviewStatusId == ReviewStatu.PriorityOne
                               select AppReviewStatus) // ReviewStatusId of CommandDraft for this app
                              on PanApp.PanelApplicationId equals CommandDraft.PanelApplicationId into gCommandDraft
                           from CommandDraft in gCommandDraft.DefaultIfEmpty()
                           join Qualifying in
                               (from AppReviewStatus in context.ApplicationReviewStatus
                                where AppReviewStatus.ReviewStatusId == ReviewStatu.PriorityTwo
                                select AppReviewStatus) // ReviewStatusId of Qualifying for this app
                               on PanApp.PanelApplicationId equals Qualifying.PanelApplicationId into gQualifying
                           from Qualifying in gQualifying.DefaultIfEmpty()
                           join Triaged in
                               (from AppReviewStatus in context.ApplicationReviewStatus
                                where AppReviewStatus.ReviewStatusId == ReviewStatu.Triaged
                                select AppReviewStatus) // ReviewStatusId of Qualifying for this app
                               on PanApp.PanelApplicationId equals Triaged.PanelApplicationId into gTriaged
                           from Triaged in gTriaged.DefaultIfEmpty()
                           where AppWorkflow.ApplicationWorkflowId == applicationWorkflowId
                           select new ApplicationDetailModel
                           {
                               LogNumber = App.LogNumber,
                               PanelApplicationId = PanApp.PanelApplicationId,
                               PiLastName = applicationPersonnel.LastName,
                               PiFirstName = applicationPersonnel.FirstName,
                               ApplicationTitle = App.ApplicationTitle,
                               PiOrganization = applicationPersonnel.OrganizationName,
                               ProgramAbbreviation = ClientPrg.ProgramAbbreviation,
                               Year = PrgFy.Year,
                               MechanismName = ClientAward.AwardDescription,
                               PanelName = Pan.PanelName,
                               WorkflowId = AppWorkflow.ApplicationWorkflowId,
                               ApplicationId = App.ApplicationId,
                               PanelAbbreviation = Pan.PanelAbbreviation,
                               Duration = Decimal.Round(DbFunctions.DiffYears(App.ProjectStartDate, App.ProjectEndDate) ?? 0, 2),
                               DirectCosts = (int?)Budget.DirectCosts,
                               IndirectCosts = (int?)Budget.IndirectCosts,
                               TotalBudget = (int?)Budget.TotalFunding,
                               StartDate = Session.StartDate,
                               GrantID = appInfo.InfoText,
                               EndDate = Session.EndDate,
                               AdminOrgName = applicationAdmin.OrganizationName,
                               ClientId = ClientPrg.ClientId,
                               ResearchArea = App.ResearchArea,
                               Cycle = programMechanism.ReceiptCycle,
                               Priority1 = CommandDraft != null,
                               Priority2 = Qualifying != null,
                               IsTriaged = Triaged != null,
                               CheckoutDateTime = AppWorkflow
                                   .ApplicationWorkflowSteps.OrderBy(y => y.StepOrder).FirstOrDefault(x => (x.Active) && (x.Resolution == false))
                                   .ApplicationWorkflowStepWorkLogs.FirstOrDefault(x => x.CheckInDate == null).CheckOutDate,

                           }).FirstOrDefault();
            return results;
        }

        /// <summary>
        /// Retrieves information about an application before it begins a workflow.
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>Application details for a single application</returns>
        internal static IApplicationDetailModel GetPreviewApplicationInfoDetail(P2RMISNETEntities context, int panelApplicationId)  //string logNo)
        {
            var results = (from PanApp in context.PanelApplications
                           join App in context.Applications on PanApp.ApplicationId equals App.ApplicationId
                           join programMechanism in context.ProgramMechanism on App.ProgramMechanismId equals programMechanism.ProgramMechanismId
                           join programPanel in context.ProgramPanels on PanApp.SessionPanelId equals programPanel.SessionPanelId
                           join PrgFy in context.ProgramYears on programPanel.ProgramYearId equals PrgFy.ProgramYearId
                           join ClientPrg in context.ClientPrograms on PrgFy.ClientProgramId equals ClientPrg.ClientProgramId

                           join Pan in context.SessionPanels on PanApp.SessionPanelId equals Pan.SessionPanelId
                           join Session in context.MeetingSessions on Pan.MeetingSessionId equals Session.MeetingSessionId
                           join ClientAward in context.ClientAwardTypes on programMechanism.ClientAwardTypeId equals ClientAward.ClientAwardTypeId
                           join Budget in context.ApplicationBudgets on App.ApplicationId equals Budget.ApplicationId into BudgetGroup
                           from Budget in BudgetGroup.DefaultIfEmpty()
                           join applicationPersonnel in context.ApplicationPersonnels on new { App.ApplicationId, PrimaryFlag = true } equals new { applicationPersonnel.ApplicationId, applicationPersonnel.PrimaryFlag } into apppGroup
                           from applicationPersonnel in apppGroup.DefaultIfEmpty()
                           join appInfo in context.ApplicationInfoes on new { App.ApplicationId, ClientApplicationInfoTypeId = GrantIdInfoKey } equals new { appInfo.ApplicationId, appInfo.ClientApplicationInfoTypeId } into appInfoGroup
                           from appInfo in appInfoGroup.DefaultIfEmpty()
                           join applicationAdmin in context.ApplicationPersonnels on new { App.ApplicationId, ApplicationPersonnelTypeAbbreviation = AdminAbbreviationKey } equals new { applicationAdmin.ApplicationId, applicationAdmin.ClientApplicationPersonnelType.ApplicationPersonnelTypeAbbreviation } into appaGroup
                           from applicationAdmin in appaGroup.DefaultIfEmpty()
                           where PanApp.PanelApplicationId == panelApplicationId
                           select new ApplicationDetailModel
                           {
                               LogNumber = App.LogNumber,
                               ApplicationId = App.ApplicationId,
                               PiLastName = applicationPersonnel.LastName,
                               PiFirstName = applicationPersonnel.FirstName,
                               ApplicationTitle = App.ApplicationTitle,
                               PiOrganization = applicationPersonnel.OrganizationName,
                               ProgramAbbreviation = ClientPrg.ProgramAbbreviation,
                               Year = PrgFy.Year,
                               MechanismName = ClientAward.AwardDescription,
                               PanelName = Pan.PanelName,
                               PanelAbbreviation = Pan.PanelAbbreviation,
                               Duration = Decimal.Round(DbFunctions.DiffYears(App.ProjectStartDate, App.ProjectEndDate) ?? 0, 2),
                               DirectCosts = (int?)Budget.DirectCosts,
                               IndirectCosts = (int?)Budget.IndirectCosts,
                               TotalBudget = (int?)Budget.TotalFunding,
                               GrantID = appInfo.InfoText,
                               StartDate = Session.StartDate,
                               EndDate = Session.EndDate,
                               AdminOrgName = applicationAdmin.OrganizationName,
                               ResearchArea = App.ResearchArea,
                               PanelApplicationId = PanApp.PanelApplicationId
                           }).FirstOrDefault();
            return results;
        }

        /// <summary>
        /// Retrieves the details of an application workflow step including content and element metadata.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>Zero or more application workflow step elements</returns>
        internal static IEnumerable<IStepContentModel> GetApplicationStepContent(P2RMISNETEntities context,
            int applicationWorkflowId)
        {
            //First get the active step or last step if workflow is complete and pass to the main query
            int? step = context.udfApplicationWorkflowActiveStep(applicationWorkflowId).Select(x => (int?)x.ApplicationWorkflowStepId).ToList().FirstOrDefault() ??
                context.udfApplicationWorkflowLastStep(applicationWorkflowId).Select(x => (int?)x.ApplicationWorkflowStepId).ToList().FirstOrDefault();
            var results = (from AppWorkflow in context.ApplicationWorkflows
                           join ActiveStep in context.ApplicationWorkflowSteps on AppWorkflow.ApplicationWorkflowId equals ActiveStep.ApplicationWorkflowId
                           join Awse in context.ApplicationWorkflowStepElements on ActiveStep.ApplicationWorkflowStepId equals
                               Awse.ApplicationWorkflowStepId
                           join Awsec in context.ApplicationWorkflowStepElementContents on Awse.ApplicationWorkflowStepElementId
                               equals Awsec.ApplicationWorkflowStepElementId into Awsecg
                           from Awsec in Awsecg.DefaultIfEmpty()
                           join Css in context.ClientScoringScales on Awse.ClientScoringId equals Css.ClientScoringId into Cssg
                           from Css in Cssg.DefaultIfEmpty()
                           join Cssa in context.ClientScoringScaleAdjectivals on new { Css.ClientScoringId, Score = Awsec.Score }
                               equals new { Cssa.ClientScoringId, Score = (decimal?)Cssa.NumericEquivalent } into Cssag
                           from Cssa in Cssag.DefaultIfEmpty()
                           join Ate in context.ApplicationTemplateElements on Awse.ApplicationTemplateElementId equals
                               Ate.ApplicationTemplateElementId
                           join Mte in context.MechanismTemplateElements on Ate.MechanismTemplateElementId equals
                               Mte.MechanismTemplateElementId
                           join Mt in context.MechanismTemplates on Mte.MechanismTemplateId equals Mt.MechanismTemplateId
                           join Ce in context.ClientElements on Mte.ClientElementId equals Ce.ClientElementId
                           join appStage in context.ApplicationStages on AppWorkflow.ApplicationStageId equals appStage.ApplicationStageId
                           join panApp in context.PanelApplications on appStage.PanelApplicationId equals panApp.PanelApplicationId
                           join App in context.Applications on panApp.ApplicationId equals App.ApplicationId
                           join panel in context.SessionPanels on panApp.SessionPanelId equals panel.SessionPanelId
                           from PanelScore in context.udfLastUpdatedCritiquePhaseAverage(panApp.PanelApplicationId, Ce.ClientElementId).DefaultIfEmpty()
                           join para in context.PanelApplicationReviewerAssignments on new { panApp.PanelApplicationId, Ate.PanelApplicationReviewerAssignmentId } equals new { para.PanelApplicationId, PanelApplicationReviewerAssignmentId = (int?)para.PanelApplicationReviewerAssignmentId } into parag
                           from para in parag.DefaultIfEmpty()
                           join pua in context.PanelUserAssignments on para.PanelUserAssignmentId equals pua.PanelUserAssignmentId into puag
                           from pua in puag.DefaultIfEmpty()
                               //Cross join of SummaryReviewerDescriptions since join must use OR condition which will go in where clause
                           from revDesc in context.udfGetSummaryReviewerDescription(App.ProgramMechanismId, pua.ClientParticipantTypeId, pua.ClientRoleId, para.SortOrder).DefaultIfEmpty()
                           where AppWorkflow.ApplicationWorkflowId == applicationWorkflowId && ActiveStep.ApplicationWorkflowStepId == step
                           orderby Mte.SortOrder, revDesc.CustomOrder
                           select new StepContentModel
                           {
                               ApplicationWorkflowStepElementId = Awse.ApplicationWorkflowStepElementId,
                               ApplicationWorkflowStepContentId = (int?)Awsec.ApplicationWorkflowStepElementContentId,
                               ElementName = Ce.ElementDescription,
                               ElementInstructions = Mte.InstructionText,
                               ElementSortOrder = Mte.SortOrder,
                               ElementScoreFlag = Mte.ScoreFlag,
                               ElementScoreType = Css.ScoreType,
                               ElementOverallFlag = Mte.OverallFlag,
                               ElementTextFlag = Mte.TextFlag,
                               ElementContentText = Awsec.ContentText,
                               ElementContentTextNoMarkup = Awsec.ContentTextNoMarkup,
                               ElementContentOriginalText =
                                   context.udfLastUpdatedCritiquePhase(pua.PanelUserAssignmentId, panApp.PanelApplicationId, Mte.ClientElementId)
                                       .Select(x => x.ContentText)
                                       .FirstOrDefault(),
                               ElementContentScore = Awsec.Score,
                               ElementContentAverageScore = PanelScore.AvgScore,
                               ElementContentAdjectivalLabel = Cssa.ScoreLabel,
                               ElementScaleHighValue = Css.HighValue,
                               ElementScaleLowValue = Css.LowValue,
                               ElementScoreStandardDeviation = PanelScore.StDev,
                               //Reviewer sort order is defaulted to 99 to sort to the bottom.  When we get into template setup this may change
                               ReviewerAssignmentOrder = revDesc.CustomOrder > 0 ? revDesc.CustomOrder : DefaultReviewerSort,
                               ReviewerAssignmentType = revDesc.DisplayName,
                               AccessLevel = Awse.AccessLevel.AccessLevel1,
                               DiscussionNoteFlag = Ate.DiscussionNoteFlag,
                               IsOverview = Ce.ElementTypeId == ElementType.Overview
                           }).OrderBy(o => o.ElementSortOrder).ThenBy(o => o.ReviewerAssignmentOrder);
            return results;
        }

        /// <summary>
        /// Retrieves a preview of an application workflow step.
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>Zero or more application workflow step preview elements</returns>
        internal static IEnumerable<IStepContentModel> GetPreviewApplicationStepContent(P2RMISNETEntities context, int panelApplicationId)
        {
            var results = (from stepcontents in context.uspPreviewApplicationSummaryWorkflow(panelApplicationId)
                           select new StepContentModel
                           {
                               ElementName = stepcontents.ElementName,
                               ElementInstructions = stepcontents.ElementInstructions,
                               ElementSortOrder = stepcontents.ElementSortOrder,
                               ElementScoreFlag = stepcontents.ElementScoreFlag,
                               ElementScoreType = stepcontents.ElementScoreType,
                               ElementOverallFlag = stepcontents.ElementOverallFlag,
                               ElementTextFlag = stepcontents.ElementTextFlag,
                               ElementContentTextNoMarkup = stepcontents.ElementContentText,
                               ElementContentScore = stepcontents.ElementContentScore,
                               ElementContentAverageScore = stepcontents.ElementContentAverageScore,
                               ElementContentAdjectivalLabel = stepcontents.ElementContentAdjectivalLabel,
                               //Reviewer sort order is defaulted to 99 to sort to the bottom.  When we get into template setup this may change
                               ReviewerAssignmentOrder = stepcontents.ReviewerAssignmentOrder,
                               ReviewerAssignmentType = stepcontents.ReviewerAssignmentType,
                               ElementScaleHighValue = stepcontents.ElementScaleHighValue,
                               ElementScaleLowValue = stepcontents.ElementScaleLowValue,
                               DiscussionNoteFlag = stepcontents.DiscussionNoteFlag,
                               IsOverview = stepcontents.IsOverview == 1
                           });
            return results;
        }

        /// <summary>
        /// Retrieves the transaction history for a specified application's workflow.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>Zero or more transactions of an application workflow</returns>
        internal static IEnumerable<IWorkflowTransactionModel> GetWorkflowTransactionHistory(P2RMISNETEntities context,
            int applicationWorkflowId)
        {
            var results = (from Aw in context.ApplicationWorkflows
                           join Aws in context.ApplicationWorkflowSteps on Aw.ApplicationWorkflowId equals
                               Aws.ApplicationWorkflowId
                           join Awswl in context.ApplicationWorkflowStepWorkLogs on Aws.ApplicationWorkflowStepId equals
                               Awswl.ApplicationWorkflowStepId
                           join Ui in context.UserInfoes on Awswl.UserId equals Ui.UserID
                           where Aw.ApplicationWorkflowId == applicationWorkflowId
                           select new WorkflowTransactionModel
                           {
                               ApplicationWorkflowStepWorkLogId = Awswl.ApplicationWorkflowStepWorkLogId,
                               Action = ApplicationWorkflowStepWorkLog.CheckoutAction,
                               PhaseName = Aws.StepName,
                               TransactionDate = (DateTime?)Awswl.CheckOutDate,
                               UserLastName = Ui.LastName,
                               UserFirstName = Ui.FirstName,
                               LogTransactionid = Awswl.ApplicationWorkflowStepWorkLogId,
                               BackupFileExists = Awswl.CheckoutBackupFile != null
                           }).Concat
                (from Aw in context.ApplicationWorkflows
                 join Aws in context.ApplicationWorkflowSteps on Aw.ApplicationWorkflowId equals
                     Aws.ApplicationWorkflowId
                 join Awswl in context.ApplicationWorkflowStepWorkLogs on Aws.ApplicationWorkflowStepId equals
                     Awswl.ApplicationWorkflowStepId
                 join Ui in context.UserInfoes on Awswl.CheckInUserId equals Ui.UserID
                 where Aw.ApplicationWorkflowId == applicationWorkflowId && Awswl.CheckInDate != null
                 select new WorkflowTransactionModel
                 {
                     ApplicationWorkflowStepWorkLogId = Awswl.ApplicationWorkflowStepWorkLogId,
                     Action = ApplicationWorkflowStepWorkLog.CheckinAction,
                     PhaseName = Aws.StepName,
                     TransactionDate = Awswl.CheckInDate,
                     UserLastName = Ui.LastName,
                     UserFirstName = Ui.FirstName,
                     LogTransactionid = Awswl.ApplicationWorkflowStepWorkLogId,
                     BackupFileExists = Awswl.CheckinBackupFile != null
                 }).OrderBy(x => x.LogTransactionid).ThenByDescending(x => x.Action);
            return results;
        }

        /// <summary>
        /// query to get the active step in an applications workflow
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>integer of active workflow step</returns>
        internal static int GetActiveApplicationWorkflowStep(P2RMISNETEntities context, int applicationWorkflowId)
        {
            var activeStep =
                context.ApplicationWorkflows.Where(x => x.ApplicationWorkflowId == applicationWorkflowId)
                    .Select(s => s.ApplicationWorkflowSteps.OrderBy(y => y.StepOrder).FirstOrDefault(x => (x.Active == true) && (x.Resolution == false)))
                    .Select(x => (int?)x.ApplicationWorkflowStepId ?? 0);

            return activeStep.FirstOrDefault();
        }

        /// <summary>
        /// query to name of the user who checked out the summary
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for application workflow Step Id</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>string of the user who checked out the summary statement</returns>
        internal static UserName GetSummaryCheckedOutUserName(P2RMISNETEntities context, int appWorkflowStepId)
        {
            var results = (from ApplicationWorkflowStepWorkLog in context.ApplicationWorkflowStepWorkLogs
                where ApplicationWorkflowStepWorkLog.ApplicationWorkflowStepId == appWorkflowStepId
                join UserInfo in context.UserInfoes on ApplicationWorkflowStepWorkLog.UserId equals UserInfo.UserID
                orderby ApplicationWorkflowStepWorkLog.ApplicationWorkflowStepId descending
                where ApplicationWorkflowStepWorkLog.CheckInDate == null
                select new UserName
                {
                    LastName = string.IsNullOrEmpty(UserInfo.LastName) ? "" : UserInfo.LastName,
                    MiddleName = string.IsNullOrEmpty(UserInfo.MiddleName) ? "" : UserInfo.MiddleName,
                    FirstName = string.IsNullOrEmpty(UserInfo.FirstName) ? "" : UserInfo.FirstName,
                    Prefix = string.IsNullOrEmpty(UserInfo.Prefix.PrefixName)
                        ? ""
                        : UserInfo.Prefix.PrefixName,
                    UsersName = string.IsNullOrEmpty(UserInfo.User.UserLogin)? string.Empty: UserInfo.User.UserLogin
                });

            return results.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves summary information for a panel's applications.  The workflow on the panel
        /// has been closed.
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>Zero or more panels with summary information</returns>
        internal static IEnumerable<ISummaryGroup> GetAllCompletePanelSummaries(P2RMISNETEntities context,
            int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId)
        {
            var results = (from ProgramFY in context.ProgramYears
                           join ProgramPanel in context.ProgramPanels on ProgramFY.ProgramYearId equals ProgramPanel.ProgramYearId
                           join SessionPanel in context.SessionPanels on ProgramPanel.SessionPanelId equals
                               SessionPanel.SessionPanelId
                           join PanelApplication in context.PanelApplications on SessionPanel.SessionPanelId equals
                               PanelApplication.SessionPanelId
                           join Application in context.Applications on PanelApplication.ApplicationId equals
                               Application.ApplicationId
                           join ProgramMechanism in context.ProgramMechanism on Application.ProgramMechanismId equals
                               ProgramMechanism.ProgramMechanismId
                           join ClientAwardType in context.ClientAwardTypes on ProgramMechanism.ClientAwardTypeId equals
                               ClientAwardType.ClientAwardTypeId
                           join ApplicationStage in context.ApplicationStages on PanelApplication.PanelApplicationId equals
                               ApplicationStage.PanelApplicationId into aaa
                           from ApplicationStage in aaa.Where(a => a.ReviewStageId == ReviewStage.Summary).DefaultIfEmpty()
                           join ApplicationWorkflow in context.ApplicationWorkflows on ApplicationStage.ApplicationStageId equals
                               ApplicationWorkflow.ApplicationStageId
                           where
                              ((ProgramFY.ClientProgramId == program) && (ProgramFY.ProgramYearId == fiscalYear) &&
                               (ApplicationWorkflow.DateClosed != null)) &&
                              ((awardTypeId == null)  || (awardTypeId == ProgramMechanism.ClientAwardTypeId))
                           group Application by new
                           {
                               ProgramAbbreviation = ProgramFY.ClientProgram.ProgramAbbreviation,
                               Year = ProgramFY.Year,
                               Cycle = ProgramMechanism.ReceiptCycle,
                               PanelAbbreviation = SessionPanel.PanelAbbreviation,
                               PanelId = SessionPanel.SessionPanelId,
                               Award = (awardTypeId == null) ? string.Empty : ClientAwardType.ClientAwardTypeId.ToString()
                           }
                into TheGroup
                           select new SummaryGroup
                           {
                               ProgramAbbreviation = TheGroup.Key.ProgramAbbreviation,
                               Year = TheGroup.Key.Year,
                               Cycle = TheGroup.Key.Cycle ?? 0,
                               PanelAbbreviation = TheGroup.Key.PanelAbbreviation,
                               PanelId = TheGroup.Key.PanelId,
                               NumberPanelApplications = TheGroup.Count(),
                               Award = TheGroup.Key.Award
                           }
                );
            //
            // Cycle & panelId are optional.  So we check if the query parameters are there then 
            // add then to the query.
            // 
            if (cycle.HasValue)
            {
                results = results.Where(r => r.Cycle == cycle);
            }
            //
            //
            if (panelId.HasValue)
            {
                results = results.Where(r => r.PanelId == panelId);
            }
            //
            return results;
        }

        /// <summary>
        /// Retrieves Application Summary Comment for the indicated application.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="appId">The applicationId for the comment(s) to be retrieved</param>
        /// <returns>Zero or more application summary comments</returns>
        internal static IEnumerable<ISummaryCommentModel> GetApplicationSummaryComments(P2RMISNETEntities context,
            int panelApplicationId)
        {
            // gets the summary comments for the specified application. Associates the comment with the last user to affect the contents of the comment
            var results = (from sumCmts in context.UserApplicationComments
                           join u in context.UserInfoes on sumCmts.ModifiedBy equals u.UserID
                           where
                               ((sumCmts.PanelApplicationId == panelApplicationId) && (sumCmts.CommentTypeID == LookupCommentType.SummaryNoteTypeId))
                           select new SummaryCommentModel
                           {
                               CommentID = sumCmts.UserApplicationCommentID,
                               UserId = u.UserID,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               CommentDate = sumCmts.ModifiedDate,
                               Comment = sumCmts.Comments
                           });

            return results;
        }
        /// <summary>
        /// Retrieves the workflows for the specified client & review stage.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="reviewStageId">ReviewStage entity identifier</param>
        /// <returns></returns>
        internal static IEnumerable<IMenuItem> GetClientWorkflowsByStage(P2RMISNETEntities context, int clientId, int reviewStageId)
        {
            var results = from workflows in context.Workflows
                          where (
                                (workflows.ClientId == clientId) &&
                                (workflows.ReviewStageId == reviewStageId)
                                )
                          select new MenuItem
                          {
                              Id = workflows.WorkflowId,
                              Name = workflows.WorkflowName,
                              Description = workflows.WorkflowDescription
                          };
            return results;
        }
        /// <summary>
        /// Retrieves possible file types for a specified client
        /// </summary>
        /// <param name="context">P2RMIS Database Context</param>
        /// <param name="applicationId">Unique identifier for an application</param>
        /// <returns>Possible file types a client may use</returns>
        internal static IEnumerable<IApplicationFileModel> GetClientFileTypes(P2RMISNETEntities context,
            int applicationId)
        {
            var results = (from app in context.Applications
                           join progMechanism in context.ProgramMechanism on app.ProgramMechanismId equals progMechanism.ProgramMechanismId
                           join progFy in context.ProgramYears on progMechanism.ProgramYearId equals progFy.ProgramYearId
                           join clientProg in context.ClientPrograms on progFy.ClientProgramId equals clientProg.ClientProgramId
                           join client in context.Clients on clientProg.ClientId equals client.ClientID
                           join clientFileTypes in context.ClientFileConfigurations on client.ClientID equals
                               clientFileTypes.ClientId
                           where app.ApplicationId == applicationId
                           select new ApplicationFileModel
                           {
                               DisplayLabel = clientFileTypes.DisplayLabel,
                               LogNumber = app.LogNumber,
                               FileSuffix = ApplicationSuffixSeperator + clientFileTypes.FileSuffix + ApplicationPdfExtension,
                               Folder = clientFileTypes.AbstractFlag ? AbstractFolder : ApplicationFolder
                           }).OrderBy(x => x.DisplayLabel);
            return results;
        }

        /// <summary>
        /// Retrieve a list of summaries already checked out by this user.
        /// </summary>
        /// <param name="context">Entity framework context.</param>
        /// <param name="userId">User identifier.</param>
        /// <param name="commentTypes">Comment types.</param>
        /// <returns>Enumerable list of available summaries.</returns>
        internal static IEnumerable<ISummaryAssignedModel> GetDraftSummmariesCheckedout(P2RMISNETEntities context, 
            int userId, List<int> commentTypes)
        {
            var results = from ProgramFY in context.ProgramYears
                              // Fiscal year
                          join programPanel in context.ProgramPanels on ProgramFY.ProgramYearId equals programPanel.ProgramYearId
                          join Panel in context.SessionPanels on programPanel.SessionPanelId equals Panel.SessionPanelId
                          // Panel by Fiscal year
                          join PanelApplication in context.PanelApplications on Panel.SessionPanelId equals PanelApplication.SessionPanelId
                          // applicationID by PanelID
                          join ApplicationView in context.Applications on PanelApplication.ApplicationId equals
                              ApplicationView.ApplicationId
                          join programMechanism in context.ProgramMechanism on ApplicationView.ProgramMechanismId equals programMechanism.ProgramMechanismId
                          // PAID to ReceiptCycle
                          join ClientAwardType in context.ClientAwardTypes on programMechanism.ClientAwardTypeId equals
                              ClientAwardType.ClientAwardTypeId
                          join appStage in context.ApplicationStages on PanelApplication.PanelApplicationId equals appStage.PanelApplicationId
                          // correlate appid and lognumber
                          join ApplicationWorkflow in
                              context.ApplicationWorkflows
                              on appStage.ApplicationStageId equals
                              ApplicationWorkflow.ApplicationStageId
                          join ApplicationWorkflowStep in context.ApplicationWorkflowSteps on ApplicationWorkflow.ApplicationWorkflowId
                              equals ApplicationWorkflowStep.ApplicationWorkflowId
                          join ApplicationWorkflowStepAssignment in context.ApplicationWorkflowStepAssignments on
                              ApplicationWorkflowStep.ApplicationWorkflowStepId equals
                              ApplicationWorkflowStepAssignment.ApplicationWorkflowStepId
                          join awswl in
                              (from awswlin in context.ApplicationWorkflowStepWorkLogs
                               where awswlin.CheckInDate == null && awswlin.UserId == userId
                               select awswlin)
                              on ApplicationWorkflowStep.ApplicationWorkflowStepId equals awswl.ApplicationWorkflowStepId
                          join UserInfo in context.UserInfoes on awswl.UserId equals UserInfo.UserID
                          join Budget in context.ApplicationBudgets on ApplicationView.ApplicationId equals Budget.ApplicationId into budgetG
                          from Budget in budgetG.DefaultIfEmpty()
                          from AverageScore in context.udfLastUpdatedCritiquePhaseAverageOverall(PanelApplication.PanelApplicationId).DefaultIfEmpty()
                          join CommandDraft in
                              (from AppReviewStatus in context.ApplicationReviewStatus
                               where AppReviewStatus.ReviewStatusId == CommandDraftKey
                               select AppReviewStatus) // ReviewStatusId of CommandDraft for this app
                              on PanelApplication.PanelApplicationId equals CommandDraft.PanelApplicationId into gCommandDraft
                          from CommandDraft in gCommandDraft.DefaultIfEmpty()
                          join Qualifying in
                              (from AppReviewStatus in context.ApplicationReviewStatus
                               where AppReviewStatus.ReviewStatusId == QualifyingRangeKey
                               select AppReviewStatus) // ReviewStatusId of Qualifying for this app
                              on PanelApplication.PanelApplicationId equals Qualifying.PanelApplicationId into gQualifying
                          from Qualifying in gQualifying.DefaultIfEmpty()
                          join AppComment in
                              (from UserApplicationComment in context.UserApplicationComments
                               where commentTypes.Contains(UserApplicationComment.CommentTypeID)
                               group UserApplicationComment by new
                               {
                                   UserApplicationComment.PanelApplicationId
                               }
                                  into commentGroup
                               select new
                               {
                                   commentGroup.Key.PanelApplicationId,
                                   CmtCount = commentGroup.Count()
                               }
                                  )
                              on PanelApplication.PanelApplicationId equals AppComment.PanelApplicationId into gAppComment
                          from AppComment in gAppComment.DefaultIfEmpty()
                          where
                              (awswl.CheckOutDate != null) && 
                              appStage.ReviewStageId == ReviewStage.Summary
                          orderby ApplicationView.LogNumber
                          select new SummaryAssignedModel
                          {
                              ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId,
                              ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId,
                              WorkflowName = ApplicationWorkflow.ApplicationWorkflowName,
                              LogNumber = ApplicationView.LogNumber,
                              ProgramAbbr = ProgramFY.ClientProgram.ProgramAbbreviation,
                              PanelAbbr = Panel.PanelAbbreviation,
                              PanelApplicationId = PanelApplication.PanelApplicationId,
                              ApplicationId = ApplicationView.ApplicationId,
                              Priority1 = CommandDraft != null,
                              Priority2 = Qualifying != null,
                              Award = ClientAwardType.AwardAbbreviation,
                              Score = AverageScore.AvgScore,
                              PostedDate = ApplicationWorkflow.DateAssigned,
                              CurrentStepName = ApplicationWorkflowStep.StepName,
                              NotesExist = AppComment.CmtCount > 0 && AppComment != null,
                              AdminNotesExist = Budget.Comments == null ? false : true,
                              CheckoutDate = awswl.CheckOutDate,
                              CheckedoutUserFirstName = UserInfo.FirstName,
                              CheckedoutUserLastName = UserInfo.LastName,
                              CheckedoutUserId = UserInfo.UserID,
                              ClientProgramId = programPanel.ProgramYear.ClientProgramId,
                          };

            return results;
        }

        /// <summary>
        /// Retrieve a list of summaries available for checkout by the current user.
        /// </summary>
        /// <param name="userId">the current user</param>
        /// <paran name="program">program filter value</paran>
        /// <param name="fiscalYear">Fiscal year filter value</param>
        /// <param name="cycle">Cycle filter value id</param>
        /// <param name="panelId">Panel filter value id</param>
        /// <param name="awardTypeId">Award filter value id</param>
        /// <returns>Enumerable list of available summaries.</returns>
        internal static IEnumerable<ISummaryAssignedModel> GetDraftSummmariesAvailableForCheckout(
            P2RMISNETEntities context, int userId, int program, int fiscalYear, int? cycle, int? panelId,
            int? awardTypeId, List<int> commentTypes)
        {
            var results = from ProgramFY in context.ProgramYears
                              // Fiscal year
                          join programPanel in context.ProgramPanels on ProgramFY.ProgramYearId equals programPanel.ProgramYearId
                          join Panel in context.SessionPanels on programPanel.SessionPanelId equals Panel.SessionPanelId
                          // Panel by Fiscal year
                          join PanelApplication in context.PanelApplications on Panel.SessionPanelId equals PanelApplication.SessionPanelId
                          // applicationID by PanelID
                          join ApplicationView in context.Applications on PanelApplication.ApplicationId equals
                              ApplicationView.ApplicationId
                          join programMechanism in context.ProgramMechanism on ApplicationView.ProgramMechanismId equals programMechanism.ProgramMechanismId
                          // PAID to ReceiptCycle
                          join ClientAwardType in context.ClientAwardTypes on programMechanism.ClientAwardTypeId equals
                              ClientAwardType.ClientAwardTypeId
                          join Budget in context.ApplicationBudgets on ApplicationView.ApplicationId equals Budget.ApplicationId into budgetG
                          from Budget in budgetG.DefaultIfEmpty()
                          from AverageScore in context.udfLastUpdatedCritiquePhaseAverageOverall(PanelApplication.PanelApplicationId).DefaultIfEmpty()
                          join appStage in context.ApplicationStages on new { PanelApplication.PanelApplicationId, ReviewStageId = ReviewStage.Summary } equals new { appStage.PanelApplicationId, appStage.ReviewStageId }
                          // correlate appid and lognumber
                          join ApplicationWorkflow in
                              context.ApplicationWorkflows
                              on appStage.ApplicationStageId equals
                              ApplicationWorkflow.ApplicationStageId
                              // workflow for this application
                          from ActiveStep in context.udfApplicationWorkflowActiveStep(ApplicationWorkflow.ApplicationWorkflowId)
                              // contains step name for current step
                          join currentWfs in context.ApplicationWorkflowSteps on ActiveStep.ApplicationWorkflowStepId
                              equals currentWfs.ApplicationWorkflowStepId
                              // the most recent check in indicates when the app became available (if no check-ins use posted date)
                          join latestCheckIn in (from steps in context.ApplicationWorkflowSteps
                                                 join workLog in context.ApplicationWorkflowStepWorkLogs on steps.ApplicationWorkflowStepId equals workLog.ApplicationWorkflowStepId
                                                 orderby workLog.CheckInDate descending
                                                 select new
                                                 {
                                                     workLog.CheckInDate,
                                                     steps.ApplicationWorkflowId
                                                 }).Take(1) on ApplicationWorkflow.ApplicationWorkflowId equals latestCheckIn.ApplicationWorkflowId into gLatestCheckIn
                          from latestCheckIn in gLatestCheckIn.DefaultIfEmpty()
                          join ApplicationWorkflowStepAssignment in context.ApplicationWorkflowStepAssignments on
                              currentWfs.ApplicationWorkflowStepId equals
                              ApplicationWorkflowStepAssignment.ApplicationWorkflowStepId into awsag
                          from ApplicationWorkflowStepAssignment in awsag.DefaultIfEmpty()
                          join awswl in
                              (from awswlin in
                                  context.ApplicationWorkflowStepWorkLogs
                               where awswlin.CheckInDate == null
                               select awswlin)
                              on ActiveStep.ApplicationWorkflowStepId equals awswl.ApplicationWorkflowStepId into awswlgroup
                          from awswl in awswlgroup.DefaultIfEmpty()
                          join CommandDraft in
                              (from AppReviewStatus in context.ApplicationReviewStatus
                               where AppReviewStatus.ReviewStatusId == CommandDraftKey
                               select AppReviewStatus) // ReviewStatusId of CommandDraft for this app
                              on PanelApplication.PanelApplicationId equals CommandDraft.PanelApplicationId into gCommandDraft
                          from CommandDraft in gCommandDraft.DefaultIfEmpty()
                          join Qualifying in
                              (from AppReviewStatus in context.ApplicationReviewStatus
                               where AppReviewStatus.ReviewStatusId == QualifyingRangeKey
                               select AppReviewStatus) // ReviewStatusId of Qualifying for this app
                              on PanelApplication.PanelApplicationId equals Qualifying.PanelApplicationId into gQualifying
                          from Qualifying in gQualifying.DefaultIfEmpty()
                          join AppComment in
                              (from UserApplicationComment in context.UserApplicationComments
                               where commentTypes.Contains(UserApplicationComment.CommentTypeID)
                               group UserApplicationComment by new
                               {
                                   UserApplicationComment.PanelApplicationId
                               }
                                  into commentGroup
                               select new
                               {
                                   commentGroup.Key.PanelApplicationId,
                                   CmtCount = commentGroup.Count()
                               }
                                  )
                              on PanelApplication.PanelApplicationId equals AppComment.PanelApplicationId into gAppComment
                          from AppComment in gAppComment.DefaultIfEmpty()
                          where 
                    (ProgramFY.ProgramYearId == fiscalYear) &&
                    (ProgramFY.ClientProgramId == program) &&
                    ((awardTypeId == null) || 
                     (awardTypeId == programMechanism.ClientAwardTypeId)) &&
                    ((cycle == null) || (cycle == programMechanism.ReceiptCycle)) &&
                    ((panelId == null) || panelId == (Panel.SessionPanelId)) &&
                    (awswl.CheckOutDate == null) 
                          orderby ApplicationView.LogNumber
                          select new SummaryAssignedModel
                          {
                              ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId,
                              ApplicationWorkflowStepId = currentWfs.ApplicationWorkflowStepId,
                              AvailableDate = latestCheckIn.CheckInDate,
                              WorkflowName = ApplicationWorkflow.ApplicationWorkflowName,
                              LogNumber = ApplicationView.LogNumber,
                              PanelApplicationId = PanelApplication.PanelApplicationId,
                              ApplicationId = ApplicationView.ApplicationId,
                              ProgramAbbr = ProgramFY.ClientProgram.ProgramAbbreviation,
                              PanelAbbr = Panel.PanelAbbreviation,
                              Priority1 = CommandDraft != null,
                              Priority2 = Qualifying != null,
                              Award = ClientAwardType.AwardAbbreviation,
                              Score = AverageScore.AvgScore,
                              PostedDate = ApplicationWorkflow.DateAssigned,
                              CurrentStepName = currentWfs.StepName,
                              NotesExist = AppComment.CmtCount > 0 && AppComment != null,
                              AdminNotesExist = Budget.Comments == null ? false : true,
                              ClientProgramId = programPanel.ProgramYear.ClientProgramId,
                              IsClientReviewStepType = (currentWfs.StepTypeId == StepType.Indexes.Review),
                              IsEditingStepType = (currentWfs.StepTypeId == StepType.Indexes.Editing),
                              IsWritingStepType = (currentWfs.StepTypeId == StepType.Indexes.Writing)
                          };
            return results;
        }
        /// <summary>
        /// Retrieve the assigned user for the current phase of a list of applications
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="applicationIds">List of application ids to find the user assigned (if any)</param>
        /// <returns>Enumerable list of applications and their assigned user for the current phase</returns>
        internal static IEnumerable<IUserApplicationModel> GetAssignedUsers(
            P2RMISNETEntities context, ICollection<int> applicationIds)
        {
            var results = (from panapp in context.PanelApplications
                           join app in context.Applications on panapp.ApplicationId equals app.ApplicationId
                           join appstage in context.ApplicationStages on panapp.PanelApplicationId equals appstage.PanelApplicationId
                           join aw in context.ApplicationWorkflows on appstage.ApplicationStageId equals aw.ApplicationStageId
                           join currentWfs in context.ApplicationWorkflowSteps on aw.ApplicationWorkflowId
                               equals currentWfs.ApplicationWorkflowId
                           join awswl in
                               (from awswlin in context.ApplicationWorkflowStepWorkLogs
                                where awswlin.CheckInDate == null
                                select awswlin) on currentWfs.ApplicationWorkflowStepId equals awswl.ApplicationWorkflowStepId
                               into awswlgroup
                           from awswl in awswlgroup.DefaultIfEmpty()
                           join user in context.Users on awswl.UserId equals user.UserID into userg
                           from user in userg.DefaultIfEmpty()
                           join userinfo in context.UserInfoes on user.UserID equals userinfo.UserID into userinfog
                           from userinfo in userinfog.DefaultIfEmpty()
                           where applicationIds.Contains(panapp.PanelApplicationId)  
                           && appstage.ReviewStageId == ReviewStage.Summary
                           && (from activeStep in context.udfApplicationWorkflowActiveStep(aw.ApplicationWorkflowId) select activeStep.ApplicationWorkflowStepId).Contains(currentWfs.ApplicationWorkflowStepId)
                           select new UserApplicationModel
                           {
                               PanelApplicationId = panapp.PanelApplicationId,
                               ApplicationWorkflowId = currentWfs.ApplicationWorkflowId,
                               LogNumber = app.LogNumber,
                               ApplicationId = app.ApplicationId,
                               UserId = awswl.CheckOutDate != null ? (int?)user.UserID : null,
                               LastName = awswl.CheckOutDate != null ? userinfo.LastName : null,
                               FirstName = awswl.CheckOutDate != null ? userinfo.FirstName : null,
                               Steps = from siblingWfs in context.ApplicationWorkflowSteps
                                       where siblingWfs.ApplicationWorkflowId == currentWfs.ApplicationWorkflowId
                                       && siblingWfs.Active
                                       orderby siblingWfs.StepOrder
                                       select new ApplicationWorkflowStepModel
                                       {
                                           ApplicationWorkflowStepId = siblingWfs.ApplicationWorkflowStepId,
                                           ApplicationWorkflowId = siblingWfs.ApplicationWorkflowId,
                                           StepTypeId = siblingWfs.StepTypeId,
                                           StepName = siblingWfs.StepName,
                                           Active = siblingWfs.Active,
                                           StepOrder = siblingWfs.StepOrder,
                                           Resolution = siblingWfs.Resolution,
                                           ResolutionDate = siblingWfs.ResolutionDate,
                                           IsCurrentStep = (siblingWfs.StepOrder == currentWfs.StepOrder)
                                       }
                           });


            return results;
        }

        /// <summary>
        /// Retrieve the user information based on a partial string to search
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="searchString">Partial string to search within all user's name</param>
        /// <param name="clientCollection">List of client ids the currently logged in user is assigned to</param>
        /// <returns>Enumerable list of applications and their assigned user for the current phase</returns>
        internal static IEnumerable<IUserModel> GetAutoCompleteUsers(
            P2RMISNETEntities context, string searchString, ICollection<int> clientCollection)
        {
            var results = (from user in context.Users
                           join userInfo in context.UserInfoes on user.UserID equals userInfo.UserID
                           join userClient in context.UserClients on user.UserID equals userClient.UserID
                           join userProfile in context.UserProfiles on userInfo.UserInfoID equals userProfile.UserInfoId
                           join userAccountStatus in context.UserAccountStatus on user.UserID equals userAccountStatus.UserId
                           where
                               (userInfo.LastName + ", " + userInfo.FirstName).StartsWith(searchString) &&
                               clientCollection.Contains(userClient.ClientID) && userProfile.ProfileTypeId == ProfileType.Indexes.SraStaff &&
                               userAccountStatus.AccountStatusId == AccountStatu.Indexes.Active
                           select new UserModel
                           {
                               UserId = user.UserID,
                               LastName = userInfo.LastName,
                               FirstName = userInfo.FirstName
                           });
            return results.Distinct().OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
        }

        /// <summary>
        /// Retrieve the user name of users assigned to summary applications based on currently selected search criteria
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="searchString">Partial string to search within user's name</param>
        /// <param name="program">Research program abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of research program</param>
        /// <param name="cycle">Receipt cycle in which mechanism was offered (optional)</param>
        /// <param name="panelId">Unique panel identifier (optional)</param>
        /// <param name="awardTypeId">Unique panel identifier (optional)</param>
        /// <returns>Enumerable list of user information</returns>
        internal static IEnumerable<IUserModel> GetAutoCompleteUsersAssignedActiveStep(
            P2RMISNETEntities context, string searchString, int program, int fiscalYear, int? panelId, int? cycle, int? awardTypeId)
        {
            var results = (from ProgramFY in context.ProgramYears
                           join ProgramPanel in context.ProgramPanels on ProgramFY.ProgramYearId equals ProgramPanel.ProgramYearId
                           join SessionPanel in context.SessionPanels on ProgramPanel.SessionPanelId equals
                               SessionPanel.SessionPanelId
                           join PanelApplication in context.PanelApplications on SessionPanel.SessionPanelId equals
                               PanelApplication.SessionPanelId
                           join Application in context.Applications on PanelApplication.ApplicationId equals
                               Application.ApplicationId
                           join ProgramMechanism in context.ProgramMechanism on Application.ProgramMechanismId equals
                               ProgramMechanism.ProgramMechanismId
                           join ClientAwardType in context.ClientAwardTypes on ProgramMechanism.ClientAwardTypeId equals
                               ClientAwardType.ClientAwardTypeId
                           join ApplicationStage in context.ApplicationStages on PanelApplication.PanelApplicationId equals
                               ApplicationStage.PanelApplicationId into aaa
                           from ApplicationStage in aaa.Where(a => a.ReviewStageId == ReviewStage.Summary).DefaultIfEmpty()
                           join ApplicationWorkflow in context.ApplicationWorkflows on ApplicationStage.ApplicationStageId equals
                               ApplicationWorkflow.ApplicationStageId
                           from activeStep in context.udfApplicationWorkflowActiveStep(ApplicationWorkflow.ApplicationWorkflowId)

                           join stepAssignment in context.ApplicationWorkflowStepAssignments on activeStep.ApplicationWorkflowStepId equals stepAssignment.ApplicationWorkflowStepId
                           join user in context.Users on stepAssignment.UserId equals user.UserID
                           join userInfo in context.UserInfoes on user.UserID equals userInfo.UserID
                           where
                               ProgramFY.ClientProgramId == program && ProgramFY.ProgramYearId == fiscalYear && (panelId == null || SessionPanel.SessionPanelId == panelId) &&
                               (cycle == null || ProgramMechanism.ReceiptCycle == cycle) && (awardTypeId == null || ProgramMechanism.ClientAwardTypeId == awardTypeId) &&
                               (userInfo.LastName + ", " + userInfo.FirstName).StartsWith(searchString)
                           select new UserModel
                           {
                               UserId = user.UserID,
                               LastName = userInfo.LastName,
                               FirstName = userInfo.FirstName
                           });
            return results.Distinct().OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
        }
        /// Retrieve a matrix representation of awards (y-axis) and priorities (x-axis) and (1)assigned or (2)defaulted workflowIds for each pair 
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="program">Program abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of program offering</param>
        /// <returns>Enumerable list of awards and their assigned or defaulted workflow</returns>
        internal static IEnumerable<IAwardWorkflowPriorityModel> GetWorkflowAssignmentOrDefault(
            P2RMISNETEntities context,
            int program, int fiscalYear, int? cycle)
        {
            //Throughout this query we coalesce nulls to 0's, so that we can join tables off of null values to get defaults
            var results = (from progFy in context.ProgramYears
                           join progAward in context.ProgramMechanism on progFy.ProgramYearId equals progAward.ProgramYearId
                           join priority1Default in
                               (from clientDefaultWorkflow in context.ClientDefaultWorkflows
                                where clientDefaultWorkflow.ReviewStatusId == ReviewStatu.PriorityOne
                                select clientDefaultWorkflow) on progFy.ClientProgram.ClientId equals priority1Default.ClientId
                           join priority1Assignment in
                               (from workflowMech in context.WorkflowMechanism
                                where workflowMech.ReviewStatusId == ReviewStatu.PriorityOne
                                select workflowMech) on progAward.ProgramMechanismId equals priority1Assignment.MechanismId into
                               priority1AssignmentGroup
                           from priority1Assignment in priority1AssignmentGroup.DefaultIfEmpty()
                           join priority2Default in
                               (from clientDefaultWorkflow in context.ClientDefaultWorkflows
                                where clientDefaultWorkflow.ReviewStatusId == ReviewStatu.PriorityTwo
                                select clientDefaultWorkflow) on progFy.ClientProgram.ClientId equals priority2Default.ClientId
                           join priority2Assignment in
                               (from workflowMech in context.WorkflowMechanism
                                where workflowMech.ReviewStatusId == ReviewStatu.PriorityTwo
                                select workflowMech) on progAward.ProgramMechanismId equals priority2Assignment.MechanismId into
                               priority2AssignmentGroup
                           from priority2Assignment in priority2AssignmentGroup.DefaultIfEmpty()
                           join noPriorityDefault in
                               (from clientDefaultWorkflow in context.ClientDefaultWorkflows
                                where clientDefaultWorkflow.ReviewStatusId == ReviewStatu.NoPriority
                                select clientDefaultWorkflow) on progFy.ClientProgram.ClientId equals noPriorityDefault.ClientId
                           join noPriorityAssignment in
                               (from workflowMech in context.WorkflowMechanism
                                where workflowMech.ReviewStatusId == ReviewStatu.NoPriority
                                select workflowMech) on progAward.ProgramMechanismId equals noPriorityAssignment.MechanismId into
                               noPriorityAssignmentGroup
                           from noPriorityAssignment in noPriorityAssignmentGroup.DefaultIfEmpty()
                           where progFy.ClientProgramId == program && progFy.ProgramYearId == fiscalYear &&
                                ((cycle == null) || (cycle == progAward.ReceiptCycle))
                           orderby progAward.ClientAwardType.AwardAbbreviation
                           select new AwardWorkflowPriorityModel
                           {
                               MechanismId = progAward.ProgramMechanismId,
                               AwardAbbreviation = progAward.ClientAwardType.AwardAbbreviation,
                               Cycle = progAward.ReceiptCycle,
                               PriorityOneWorkflowId = (int?)priority1Assignment.WorkflowId ?? priority1Default.WorkflowId,
                               PriorityTwoWorkflowId = (int?)priority2Assignment.WorkflowId ?? priority2Default.WorkflowId,
                               NoPriorityWorkflowId = (int?)noPriorityAssignment.WorkflowId ?? noPriorityDefault.WorkflowId,
                           });
            return results;
        }
        /// <summary>
        /// Gets the request review applications.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns></returns>
        internal static IEnumerable<ISummaryStatementRequestReview> GetRequestReviewApplications(P2RMISNETEntities context, int programId, int yearId, int? cycle, int? panelId, int? awardTypeId)
        {
             var results = (from ProgramFY in context.ProgramYears
                           join clientProgram in context.ClientPrograms on ProgramFY.ClientProgramId equals clientProgram.ClientProgramId
                           join ProgramPanel in context.ProgramPanels on ProgramFY.ProgramYearId equals ProgramPanel.ProgramYearId
                           join SessionPanel in context.SessionPanels on ProgramPanel.SessionPanelId equals SessionPanel.SessionPanelId
                           join PanelApplication in context.PanelApplications on SessionPanel.SessionPanelId equals PanelApplication.SessionPanelId
                           join Application in context.Applications on PanelApplication.ApplicationId equals
                               Application.ApplicationId
                           join applicationPersonnel in context.ApplicationPersonnels on new { Application.ApplicationId, PrimaryFlag = true } equals new { applicationPersonnel.ApplicationId, applicationPersonnel.PrimaryFlag } into apg
                           from applicationPersonnel in apg.DefaultIfEmpty()
                           join commandDraft in context.ApplicationReviewStatus on new { PanelApplication.PanelApplicationId, ReviewStatusId = ReviewStatu.PriorityOne } equals new { PanelApplicationId = (int)commandDraft.PanelApplicationId, commandDraft.ReviewStatusId } into cmdg
                           from commandDraft in cmdg.DefaultIfEmpty()
                           join qualifying in context.ApplicationReviewStatus on new { PanelApplication.PanelApplicationId, ReviewStatusId = ReviewStatu.PriorityTwo } equals new { PanelApplicationId = (int)qualifying.PanelApplicationId, qualifying.ReviewStatusId } into qrg
                           from qualifying in qrg.DefaultIfEmpty()
                           join ProgramMechanism in context.ProgramMechanism on Application.ProgramMechanismId equals ProgramMechanism.ProgramMechanismId
                           join ClientAwardType in context.ClientAwardTypes on ProgramMechanism.ClientAwardTypeId equals
                               ClientAwardType.ClientAwardTypeId
                           join ApplicationTopicCode in context.ApplicationTopicCodes on Application.ApplicationId equals ApplicationTopicCode.ApplicationId into topicG
                           from ApplicationTopicCode in topicG.DefaultIfEmpty()
                           join ApplicationStage in (from SummaryStage in context.ApplicationStages where SummaryStage.ReviewStageId == ReviewStage.Summary select SummaryStage) on PanelApplication.PanelApplicationId equals ApplicationStage.PanelApplicationId into aaa
                           from ApplicationStage in aaa.DefaultIfEmpty()
                           join ApplicationWorkflow in context.ApplicationWorkflows on ApplicationStage.ApplicationStageId equals
                               ApplicationWorkflow.ApplicationStageId into yyy
                           from ApplicationWorkflow in yyy.DefaultIfEmpty()
                           from ActiveStep in context.udfApplicationWorkflowActiveStep(ApplicationWorkflow.ApplicationWorkflowId).DefaultIfEmpty()
                           join ApplicationDefaultWorkflow in context.ApplicationDefaultWorkflows on Application.ApplicationId
                               equals ApplicationDefaultWorkflow.ApplicationId into www
                           from ApplicationDefaultWorkflow in www.DefaultIfEmpty()
                           join workflow in context.Workflows on ApplicationDefaultWorkflow.WorkflowId equals workflow.WorkflowId into workflowg
                           from workflow in workflowg.DefaultIfEmpty()
                           from PanelScore in context.udfLastUpdatedCritiquePhaseAverageOverall(PanelApplication.PanelApplicationId).DefaultIfEmpty()
                           join SiblingStep in context.ApplicationWorkflowSteps
                                on ApplicationWorkflow.ApplicationWorkflowId equals SiblingStep.ApplicationWorkflowId into gSiblingStep
                           from PrevReviewStep in gSiblingStep.Where(x => x.StepOrder <= ActiveStep.StepOrder &&
                                (x.StepTypeId == StepType.Indexes.Review || x.StepTypeId == StepType.Indexes.ReviewSupport)).Take(1).DefaultIfEmpty()
                           where
                               ProgramFY.ClientProgramId == programId && ProgramFY.ProgramYearId == yearId &&
                               (cycle == null || cycle == ProgramMechanism.ReceiptCycle) &&
                               (panelId == null || panelId == SessionPanel.SessionPanelId) &&
                               (awardTypeId == null || awardTypeId == ProgramMechanism.ClientAwardTypeId)
                           select new SummaryStatementRequestReview
                           {
                               ApplicationId = Application.ApplicationId,
                               LogNumber = Application.LogNumber,
                               Priority1 = commandDraft.ApplicationReviewStatusId != null,
                               Priority2 = qualifying.ApplicationReviewStatusId != null,
                               AwardMechanismAbbreviation = ClientAwardType.AwardAbbreviation,
                               ResearchTopicArea = ApplicationTopicCode.TopicCode,
                               OverallScore = PanelScore.AvgScore,
                               PanelApplicationId = PanelApplication.PanelApplicationId,
                               HasPrecedingReviewStep = (PrevReviewStep != null),
                               Panel = SessionPanel.PanelAbbreviation,
                               Cycle = ProgramMechanism.ReceiptCycle
                           }
                );
            return results;
        }

        /// <summary>
        /// Retrieve application information for reporting
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="panelApplicationIdCollection">List of panel application ids</param>
        /// <returns>Enumerable list of application info for reporting</returns>
        internal static IEnumerable<IReportAppInfo> GetReportAppInfo(P2RMISNETEntities context, ICollection<int> panelApplicationIdCollection)
        {
            var results = (from ProgramFY in context.ProgramYears
                           join ProgramPanel in context.ProgramPanels on ProgramFY.ProgramYearId equals ProgramPanel.ProgramYearId
                           join SessionPanel in context.SessionPanels on ProgramPanel.SessionPanelId equals
                               SessionPanel.SessionPanelId
                           join PanelApplication in context.PanelApplications on SessionPanel.SessionPanelId equals
                               PanelApplication.SessionPanelId
                           join Application in context.Applications on PanelApplication.ApplicationId equals
                               Application.ApplicationId
                           join ProgramMechanism in context.ProgramMechanism on Application.ProgramMechanismId equals
                               ProgramMechanism.ProgramMechanismId
                           join ClientAwardType in context.ClientAwardTypes on ProgramMechanism.ClientAwardTypeId equals
                               ClientAwardType.ClientAwardTypeId
                           join ApplicationStage in context.ApplicationStages on PanelApplication.PanelApplicationId equals
                               ApplicationStage.PanelApplicationId into aaa
                           from ApplicationStage in aaa.Where(a => a.ReviewStageId == ReviewStage.Summary).DefaultIfEmpty()
                           join ApplicationWorkflow in context.ApplicationWorkflows on ApplicationStage.ApplicationStageId equals
                               ApplicationWorkflow.ApplicationStageId
                           join appTemplate in context.ApplicationTemplates on ApplicationWorkflow.ApplicationTemplateId equals appTemplate.ApplicationTemplateId
                           join mechTemplate in context.MechanismTemplates on appTemplate.MechanismTemplateId equals mechTemplate.MechanismTemplateId
                           join sumDocument in context.SummaryDocuments on mechTemplate.SummaryDocumentId equals sumDocument.SummaryDocumentId into docG
                           from sumDocument in docG.DefaultIfEmpty()
                           where
                               panelApplicationIdCollection.Contains(PanelApplication.PanelApplicationId)
                           select new ReportAppInfo
                           {
                               ProgramAbrv = ProgramFY.ClientProgram.ProgramAbbreviation,
                               FiscalYear = ProgramFY.Year,
                               Cycle = ProgramMechanism.ReceiptCycle ?? 0,
                               AppLogNumber = Application.LogNumber,
                               ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId,
                               ReportFileName = sumDocument.DocumentFile
                           });
            return results;
        }

        /// <summary>
        /// Retrieve workflow information for all workflows belonging to a client
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="program">Program abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of program offering</param>
        /// <returns>Enumerable list of workflows</returns>
        internal static IEnumerable<IWorkflowTemplateModel> GetClientWorkflowAll(
            P2RMISNETEntities context,
            int program, int fiscalYear)
        {
            var results = (from workflow in context.Workflows
                           join ClientProgram in context.ClientPrograms on workflow.ClientId equals ClientProgram.ClientId
                           join programFy in context.ProgramYears on ClientProgram.ClientProgramId equals programFy.ClientProgramId
                           where (
                                    (ClientProgram.ClientProgramId == program) &&
                                    (programFy.ProgramYearId == fiscalYear) &&
                                    (workflow.ReviewStageId == ReviewStage.Indexes.Summary)
                                )
                           orderby workflow.WorkflowName
                           select new WorkflowTemplateModel
                           {
                               WorkflowId = workflow.WorkflowId,
                               WorkflowName = workflow.WorkflowName
                           });
            return results;
        }
        /// <summary>
        /// determines whether an application is checked out or not
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="applicationWorkflowId">the applications workflow Id</param>
        /// <returns>boolean of true or false</returns>
        internal static bool IsSsCheckedOut(P2RMISNETEntities context, int applicationWorkflowId)
        {
            bool results = (context.ApplicationWorkflowStepWorkLogs.Where(x => x.ApplicationWorkflowStep.ApplicationWorkflowId == applicationWorkflowId && x.CheckOutDate != null && x.CheckInDate == null).Select(s => s.ApplicationWorkflowStepId).Count() > 0);

            return results;
        }

    }
}
