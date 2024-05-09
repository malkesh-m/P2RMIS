CREATE PROCEDURE [dbo].[uspReportEditorProductivityParameter]
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@CycleList varchar(4000),
	@MinDate datetime2(0),
	@MaxDate datetime2(0)	
AS
BEGIN
SET NOCOUNT ON;
WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	CycleParams(Cycle) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@CycleList))
SELECT DISTINCT UserInfo.UserID, UserSystemRole.SystemRoleId,UserInfo.FirstName, UserInfo.LastName
FROM  ClientProgram INNER JOIN
               ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId INNER JOIN
               ViewProgramMechanism ProgramMechanism ON ProgramYear.ProgramYearId = ProgramMechanism.ProgramYearId INNER JOIN
			   ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN 
               FiscalYearParams ON ProgramYear.Year = FiscalYearParams.FY INNER JOIN 
			   CycleParams ON CycleParams.Cycle = 0 OR ProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
               ViewApplication [Application] ON ProgramMechanism.ProgramMechanismId = Application.ProgramMechanismId INNER JOIN
               ViewPanelApplication PanelApplication ON Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
               ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
               ViewApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId AND 
               ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
               ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
               ViewApplicationWorkflowStepWorkLog ApplicationWorkflowStepWorkLog ON 
               ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepWorkLog.ApplicationWorkflowStepId INNER JOIN
               ViewUser [User] ON ApplicationWorkflowStepWorkLog.UserId = [User].UserID INNER JOIN
			   ViewUserInfo UserInfo ON [User].UserID = UserInfo.UserID INNER JOIN
			   ViewUserSystemRole UserSystemRole ON [User].UserID = UserSystemRole.UserID
WHERE (ApplicationStage.ReviewStageId = 3) AND (UserSystemRole.SystemRoleId IN (20,23,11)) AND ApplicationWorkflowStepWorkLog.CheckOutDate >= @MinDate AND ApplicationWorkflowStepWorkLog.CheckOutDate <= @MaxDate
ORDER by UserInfo.LastName
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportEditorProductivityParameter] TO [NetSqlAzMan_Users]
    AS [dbo];