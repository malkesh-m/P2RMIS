CREATE PROCEDURE [dbo].[uspReportPanelParticipantCount]
	-- Add the parameters for the stored procedure here
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@PanelList varchar(4000)	
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))

SELECT				ClientProgram.ClientProgramId,
					ClientProgram.ProgramAbbreviation, 
					ClientProgram.ProgramDescription,
					ViewProgramYear.Year, 
					ViewProgramPanel.SessionPanelId,
					ViewSessionPanel.PanelAbbreviation,
					ViewSessionPanel.PanelName,
					ClientParticipantType.ParticipantTypeName,
					COUNT(ClientParticipantType.ParticipantTypeName) ClientParticipantTypeCOUNT
FROM			ClientProgram 
					INNER JOIN ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId
					INNER JOIN ViewProgramPanel ON ViewProgramYear.ProgramYearId = ViewProgramPanel.ProgramYearId 
					INNER JOIN ViewSessionPanel ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId
					INNER JOIN ViewPanelUserAssignment ON ViewSessionPanel.SessionPanelId = ViewPanelUserAssignment.SessionPanelId
					INNER JOIN ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId

					INNER JOIN

					ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
					FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
					PanelParams ON PanelParams.PanelId = 0 OR ViewProgramPanel.SessionPanelId = PanelParams.PanelId

	GROUP BY		ClientProgram.ClientProgramId,
					ClientProgram.ProgramAbbreviation, 
					ClientProgram.ProgramDescription,
					ViewProgramYear.Year, 
					ViewProgramPanel.SessionPanelId, 
					ViewSessionPanel.PanelName,
					ViewSessionPanel.SessionPanelId, 
					ViewSessionPanel.PanelAbbreviation,
					ClientParticipantType.ParticipantTypeName

		--		HAVING COUNT(ClientParticipantType.ParticipantTypeName) > 1
				
	ORDER BY		ClientProgram.ClientProgramId,
					ViewProgramYear.Year, 
					ViewProgramPanel.SessionPanelId,
					ClientParticipantType.ParticipantTypeName

END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportPanelParticipantCount] TO [NetSqlAzMan_Users]
    AS [dbo];	