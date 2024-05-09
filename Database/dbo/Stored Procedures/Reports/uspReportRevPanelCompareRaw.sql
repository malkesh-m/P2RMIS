CREATE PROCEDURE [dbo].[uspReportRevPanelCompareRaw]
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@PanelList varchar(4000)	
AS
BEGIN
SET NOCOUNT ON;
WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@PanelList))
SELECT ViewApplication.ApplicationId, ViewApplication.LogNumber, ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ViewProgramYear.Year, ViewSessionPanel.PanelAbbreviation, ViewSessionPanel.PanelName,
	ClientAwardType.AwardAbbreviation, ClientAwardType.AwardDescription, ViewUserInfo.FirstName, ViewUserInfo.LastName, ViewApplicationWorkflowStepElementContent.Score,
	ROW_NUMBER() OVER (Partition By ViewPanelApplication.PanelApplicationId Order By ViewUserInfo.LastName, ViewUserInfo.FirstName) AS ReviewerSeed,
	DENSE_RANK() OVER (Partition By ViewSessionPanel.SessionPanelId Order By ClientAwardType.AwardAbbreviation) AS MechanismSeed, ClientScoringScale.ScoreType, ClientScoringScaleAdjectival.ScoreLabel
FROM ViewApplication INNER JOIN
ViewPanelApplication ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId INNER JOIN
ViewSessionPanel ON ViewPanelApplication.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
ViewProgramMechanism ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId INNER JOIN
ViewProgramYear ON ViewProgramMechanism.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
ViewPanelUserAssignment ON ViewSessionPanel.SessionPanelId = ViewPanelUserAssignment.SessionPanelId INNER JOIN
ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId INNER JOIN
ClientProgram ON ViewProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId INNER JOIN
ClientAwardType ON ViewProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId INNER JOIN
ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId AND ViewApplicationStage.ReviewStageId = 2 LEFT OUTER JOIN
ViewApplicationWorkflow ON ViewApplicationStage.ApplicationStageId = ViewApplicationWorkflow.ApplicationStageId AND ViewPanelUserAssignment.PanelUserAssignmentId = ViewApplicationWorkflow.PanelUserAssignmentId INNER JOIN
ViewUser ON ViewPanelUserAssignment.UserId = ViewUser.UserID INNER JOIN
ViewUserInfo ON ViewUser.UserID = ViewUserInfo.UserID LEFT OUTER JOIN
ViewApplicationWorkflowStep ON ViewApplicationWorkflow.ApplicationWorkflowId = ViewApplicationWorkflowStep.ApplicationWorkflowId LEFT OUTER JOIN
(Select ApplicationWorkflowStepId, ApplicationWorkflowStepElementId, ClientScoringId FROM ViewApplicationWorkflowStepElement INNER JOIN
	ViewApplicationTemplateElement ON ViewApplicationWorkflowStepElement.ApplicationTemplateElementId = ViewApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
	ViewMechanismTemplateElement ON ViewApplicationTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElement.MechanismTemplateElementId
	WHERE ViewMechanismTemplateElement.OverallFlag = 1) ViewApplicationWorkflowStepElementOverall ON ViewApplicationWorkflowStep.ApplicationWorkflowStepId = ViewApplicationWorkflowStepElementOverall.ApplicationWorkflowStepId LEFT OUTER JOIN
ViewApplicationWorkflowStepElementContent ON ViewApplicationWorkflowStepElementOverall.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId LEFT OUTER JOIN
ClientScoringScale ON ViewApplicationWorkflowStepElementOverall.ClientScoringId = ClientScoringScale.ClientScoringId LEFT OUTER JOIN
ClientScoringScaleAdjectival ON ClientScoringScale.ClientScoringId = ClientScoringScaleAdjectival.ClientScoringId AND ViewApplicationWorkflowStepElementContent.Score = ClientScoringScaleAdjectival.NumericEquivalent
WHERE ClientParticipantType.ReviewerFlag = 1
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportRevPanelCompareRaw] TO [NetSqlAzMan_Users]
    AS [dbo];
