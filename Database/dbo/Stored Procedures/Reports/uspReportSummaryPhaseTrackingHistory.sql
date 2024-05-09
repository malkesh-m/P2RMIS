CREATE PROCEDURE [dbo].[uspReportSummaryPhaseTrackingHistory]
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@CycleList varchar(4000),
	@PointInTime datetime2
AS
BEGIN
DECLARE @DateTimeToCheck datetime2;
--Set the time part to 5 PM
SELECT @DateTimeToCheck = DATEADD(HOUR, 17, @PointInTime);
-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
SET NOCOUNT ON;
WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	CycleParams(Cycle) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@CycleList))
	SELECT ProgramAbbreviation, ProgramDescription, Year, SessionPanelId, PanelAbbreviation, PanelName, ReceiptCycle, StepName, StepOrder, Priority1, Priority2, NoPriority, LogNumber
FROM (
SELECT        ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ProgramYear.Year, SessionPanel.SessionPanelId, SessionPanel.PanelAbbreviation, SessionPanel.PanelName, ProgramMechanism.ReceiptCycle, 
              CASE WHEN viewApplicationWorkflowStepWorkLog.ApplicationWorkflowStepWorkLogId IS NULL THEN 'Not Started' WHEN ViewApplicationWorkflowStepWorkLog.CheckInDate IS NULL THEN CurrentStep.StepName ELSE ApplicationWorkflowStep.StepName END AS StepName, 
			  CASE WHEN ApplicationWorkflow.ApplicationWorkflowId IS NULL THEN 0 WHEN ViewApplicationWorkflowStepWorkLog.CheckInDate IS NULL THEN CurrentStep.StepOrder ELSE ApplicationWorkflowStep.StepOrder END AS StepOrder, 
			  ROW_NUMBER() OVER (Partition By ApplicationWorkflowStep.ApplicationWorkflowId Order By ViewApplicationWorkflowStepWorkLog.ApplicationWorkflowStepWorkLogId desc) AS Position,
			---  CASE WHEN Priority1.ApplicationReviewStatusId IS NOT NULL AND Priority2.ApplicationReviewStatusId IS NOT NULL THEN 1 ELSE 0 END AS Priority1and2,
		      CASE WHEN  Priority1.ApplicationReviewStatusId IS NOT NULL AND Priority2.ApplicationReviewStatusId IS NOT NULL OR Priority1.ApplicationReviewStatusId IS NOT NULL AND Priority2.ApplicationReviewStatusId IS NULL THEN 1 ELSE 0 END AS Priority1, 
			  CASE WHEN  Priority1.ApplicationReviewStatusId IS NULL AND Priority2.ApplicationReviewStatusId IS NOT NULL THEN 1 ELSE 0 END AS Priority2, 
			  CASE WHEN  Priority1.ApplicationReviewStatusId IS NULL AND Priority2.ApplicationReviewStatusId IS NULL THEN 1 ELSE 0 END AS NoPriority, 
			  Application.LogNumber
FROM           ClientProgram INNER JOIN
               ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId INNER JOIN
               ViewProgramMechanism ProgramMechanism ON ProgramYear.ProgramYearId = ProgramMechanism.ProgramYearId INNER JOIN
			   ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN 
               FiscalYearParams ON ProgramYear.Year = FiscalYearParams.FY INNER JOIN 
			   CycleParams ON CycleParams.Cycle = 0 OR ProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
               ViewApplication AS Application ON ProgramMechanism.ProgramMechanismId = Application.ProgramMechanismId INNER JOIN
               ViewPanelApplication AS PanelApplication ON Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
               ViewSessionPanel AS SessionPanel ON PanelApplication.SessionPanelId = SessionPanel.SessionPanelId LEFT OUTER JOIN
               ViewApplicationStage AS ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId AND ApplicationStage.ReviewStageId = 3 LEFT OUTER JOIN
               ViewApplicationWorkflow AS ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId LEFT OUTER JOIN
			   ViewApplicationWorkflowStep AS ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowID AND ApplicationWorkflowStep.Active = 1 LEFT OUTER JOIN
			   ViewApplicationWorkflowStepWorkLog ON ViewApplicationWorkflowStepWorkLog.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId AND ViewApplicationWorkflowStepWorkLog.CheckOutDate < @DateTimeToCheck OUTER APPLY
				udfApplicationWorkflowActiveStep(ApplicationWorkflowStep.ApplicationWorkflowId) CurrentStep LEFT OUTER JOIN
               ViewApplicationReviewStatus AS Priority1 ON PanelApplication.PanelApplicationId = Priority1.PanelApplicationId AND Priority1.ReviewStatusId = 3 LEFT OUTER JOIN
               ViewApplicationReviewStatus AS Priority2 ON PanelApplication.PanelApplicationId = Priority2.PanelApplicationId AND Priority2.ReviewStatusId = 4 
) Main Where Position = 1
UNION ALL --This just gets all phases for display purposes 
SELECT ProgramAbbreviation, ProgramDescription, Year, SessionPanel.SessionPanelId, PanelAbbreviation, PanelName, ReceiptCycle, StepName, StepOrder, 0 AS Priority1, 0 AS Priority2, 0 AS NoPriority, LogNumber
FROM           ClientProgram INNER JOIN
               ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId INNER JOIN
               ViewProgramMechanism ProgramMechanism ON ProgramYear.ProgramYearId = ProgramMechanism.ProgramYearId INNER JOIN
			   ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN 
               FiscalYearParams ON ProgramYear.Year = FiscalYearParams.FY INNER JOIN 
			   CycleParams ON CycleParams.Cycle = 0 OR ProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
               ViewApplication AS Application ON ProgramMechanism.ProgramMechanismId = Application.ProgramMechanismId INNER JOIN
               ViewPanelApplication AS PanelApplication ON Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
               ViewSessionPanel AS SessionPanel ON PanelApplication.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
               ViewApplicationStage AS ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId AND ApplicationStage.ReviewStageId = 3 INNER JOIN
               ViewApplicationWorkflow AS ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
			   ViewApplicationWorkflowStep AS ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowID
ORDER BY PanelAbbreviation, LogNumber
END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportSummaryPhaseTrackingHistory] TO [NetSqlAzMan_Users]
    AS [dbo];