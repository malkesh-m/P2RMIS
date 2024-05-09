CREATE PROCEDURE [dbo].[uspReportSummaryPhaseTracking]
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@CycleList varchar(4000)	
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
SET NOCOUNT ON;
WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	CycleParams(Cycle) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@CycleList))
--Top query pulls in counts by phase
SELECT ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ProgramYear.[Year], SessionPanel.PanelAbbreviation, SessionPanel.PanelName, ProgramMechanism.ReceiptCycle, ApplicationWorkflowStep.StepName, ApplicationWorkflowStep.StepOrder,
SUM(CASE WHEN ActiveApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId AND Priority1.ApplicationReviewStatusId IS NOT NULL AND Priority2.ApplicationReviewStatusId IS NOT NULL OR 
ActiveApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId AND Priority1.ApplicationReviewStatusId IS NOT NULL AND Priority2.ApplicationReviewStatusId IS NULL THEN 1 ELSE 0 END) AS Priority1,SUM(CASE WHEN ActiveApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId AND Priority1.ApplicationReviewStatusId IS NULL AND Priority2.ApplicationReviewStatusId IS NOT NULL THEN 1 ELSE 0 END) AS Priority2,
SUM(CASE WHEN ActiveApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId AND Priority1.ApplicationReviewStatusId IS NULL AND Priority2.ApplicationReviewStatusId IS NULL THEN 1 ELSE 0 END) AS NoPriority
FROM  ClientProgram INNER JOIN
               ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId INNER JOIN
               ViewProgramMechanism ProgramMechanism ON ProgramYear.ProgramYearId = ProgramMechanism.ProgramYearId INNER JOIN
			   ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN 
               FiscalYearParams ON ProgramYear.Year = FiscalYearParams.FY INNER JOIN 
			   CycleParams ON CycleParams.Cycle = 0 OR ProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
               ViewApplication [Application] ON ProgramMechanism.ProgramMechanismId = Application.ProgramMechanismId INNER JOIN
               ViewPanelApplication PanelApplication ON Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
               ViewSessionPanel SessionPanel ON PanelApplication.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
               ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
               ViewApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
               ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId LEFT OUTER JOIN
               ViewApplicationReviewStatus Priority1 ON PanelApplication.PanelApplicationId = Priority1.PanelApplicationId AND Priority1.ReviewStatusId = 3 LEFT OUTER JOIN
               ViewApplicationReviewStatus Priority2 ON PanelApplication.PanelApplicationId = Priority2.PanelApplicationId AND Priority2.ReviewStatusId = 4 OUTER APPLY
               udfApplicationWorkflowActiveStep(ApplicationWorkflow.ApplicationWorkflowId) AS ActiveApplicationWorkflowStep
WHERE ApplicationStage.ReviewStageId = 3
GROUP BY ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ProgramYear.[Year], SessionPanel.PanelAbbreviation, SessionPanel.PanelName, ProgramMechanism.ReceiptCycle,
ApplicationWorkflowStep.StepName, ApplicationWorkflowStep.StepOrder
UNION ALL
--Bottom query pulls in counts for completed workflows
SELECT ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ProgramYear.[Year], SessionPanel.PanelAbbreviation, SessionPanel.PanelName, ProgramMechanism.ReceiptCycle, 'COMPLETE', 99,
SUM(CASE WHEN ApplicationWorkflow.ApplicationWorkflowId IS NOT NULL AND Priority1.ApplicationReviewStatusId IS NOT NULL AND Priority2.ApplicationReviewStatusId IS NOT NULL OR ApplicationWorkflow.ApplicationWorkflowId IS NOT NULL AND Priority1.ApplicationReviewStatusId IS NOT NULL AND Priority2.ApplicationReviewStatusId IS NULL THEN 1 ELSE 0 END) AS Priority1,
SUM(CASE WHEN ApplicationWorkflow.ApplicationWorkflowId IS NOT NULL AND Priority1.ApplicationReviewStatusId IS NULL AND Priority2.ApplicationReviewStatusId IS NOT NULL THEN 1 ELSE 0 END) AS Priority2,
SUM(CASE WHEN ApplicationWorkflow.ApplicationWorkflowId IS NOT NULL AND Priority1.ApplicationReviewStatusId IS NULL AND Priority2.ApplicationReviewStatusId IS NULL THEN 1 ELSE 0 END) AS NoPriority
FROM  ClientProgram INNER JOIN
               ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId INNER JOIN
               ViewProgramMechanism ProgramMechanism ON ProgramYear.ProgramYearId = ProgramMechanism.ProgramYearId INNER JOIN
			   ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN 
               FiscalYearParams ON ProgramYear.Year = FiscalYearParams.FY INNER JOIN 
			   CycleParams ON CycleParams.Cycle = 0 OR ProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
               ViewApplication Application ON ProgramMechanism.ProgramMechanismId = Application.ProgramMechanismId INNER JOIN
               ViewPanelApplication PanelApplication ON Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
               ViewSessionPanel SessionPanel ON PanelApplication.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
               ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId LEFT OUTER JOIN
               ViewApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId AND ApplicationWorkflow.DateClosed IS NOT NULL LEFT OUTER JOIN
               ViewApplicationReviewStatus Priority1 ON PanelApplication.PanelApplicationId = Priority1.PanelApplicationId AND Priority1.ReviewStatusId = 3 LEFT OUTER JOIN
               ViewApplicationReviewStatus Priority2 ON PanelApplication.PanelApplicationId = Priority2.PanelApplicationId AND Priority2.ReviewStatusId = 4
WHERE ApplicationStage.ReviewStageId = 3
GROUP BY ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ProgramYear.[Year], SessionPanel.PanelAbbreviation, SessionPanel.PanelName, ProgramMechanism.ReceiptCycle
ORDER BY ProgramAbbreviation, [Year], [ReceiptCycle], [PanelAbbreviation], StepOrder
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportSummaryPhaseTracking] TO [NetSqlAzMan_Users]
    AS [dbo];
