CREATE PROCEDURE [dbo].[uspReportRereviewApplications]
	--Add the parameters for the stored procedure here
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@CycleList varchar(4000)
AS
BEGIN
SET NOCOUNT ON;
WITH ProgramParams(ClientProgramId)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
	FiscalYearParams(FY)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
	CycleParams(Cycle)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@CycleList))
	SELECT Application.LogNumber, ClientAwardType.AwardAbbreviation, SessionPanel.PanelAbbreviation, SessionPanel.StartDate, SessionPanel.EndDate, PanelApplicationReviewerAssignment.SortOrder,
	UserInfo.LastName, UserInfo.FirstName, ClientParticipantType.ParticipantTypeAbbreviation, ROUND(FinalCritiquePhaseAverageOverall.AvgScore,1) AS AvgScore, ROUND(ROUND(FinalCritiquePhaseAverageOverall.StDev, 2), 1) AS StdDev, 
	PanelApplication.PanelApplicationId, PanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId, ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ProgramYear.Year
	FROM ClientProgram INNER JOIN
		ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId INNER JOIN
		ViewProgramMechanism ProgramMechanism ON ProgramYear.ProgramYearId = ProgramMechanism.ProgramYearId INNER JOIN
		ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN 
        FiscalYearParams ON ProgramYear.Year = FiscalYearParams.FY INNER JOIN 
		CycleParams ON CycleParams.Cycle = 0 OR ProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
		ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId INNER JOIN
		ViewApplication Application ON ProgramMechanism.ProgramMechanismId = Application.ProgramMechanismId INNER JOIN
		ViewPanelApplication PanelApplication ON Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
		ViewSessionPanel SessionPanel ON PanelApplication.SessionPanelId = SessionPanel.SessionPanelId OUTER APPLY
		udfFinalCritiquePhaseAverageOverall(PanelApplication.PanelApplicationId) AS FinalCritiquePhaseAverageOverall INNER JOIN
		ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
		ClientAssignmentType ON PanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
		ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
		ViewUserInfo UserInfo ON PanelUserAssignment.UserId = UserInfo.UserID INNER JOIN
		ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId
	WHERE ClientAssignmentType.AssignmentTypeId IN (5, 6, 9) AND (EXISTS
	--This filters the list to only re-review applications
	--Should be removed once 2.0 support applications being reviewed multiple times/panels
	(Select 'X'
	FROM ViewPanelApplication INNER JOIN
		ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId INNER JOIN
		ViewApplication ViewApplication2 ON ViewApplication.LogNumber = STUFF(ViewApplication2.LogNumber, LEN(ViewApplication2.LogNumber), 1, '') INNER JOIN
		ViewPanelApplication ViewPanelApplication2 ON ViewApplication2.ApplicationId = ViewPanelApplication2.ApplicationId
	WHERE ViewPanelApplication.PanelApplicationId = PanelApplication.PanelApplicationId AND RIGHT(ViewApplication2.LogNumber, 1) = 'A') OR EXISTS
	(Select 'X'
	FROM ViewPanelApplication INNER JOIN
		ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId INNER JOIN
		ViewApplication ViewApplication2 ON ViewApplication.LogNumber = STUFF(ViewApplication2.LogNumber, LEN(ViewApplication2.LogNumber), 1, '') INNER JOIN
		ViewPanelApplication ViewPanelApplication2 ON ViewApplication2.ApplicationId = ViewPanelApplication2.ApplicationId
	WHERE ViewPanelApplication2.PanelApplicationId = PanelApplication.PanelApplicationId AND RIGHT(ViewApplication2.LogNumber, 1) = 'A'))
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportRereviewApplications] TO [NetSqlAzMan_Users]
    AS [dbo];
