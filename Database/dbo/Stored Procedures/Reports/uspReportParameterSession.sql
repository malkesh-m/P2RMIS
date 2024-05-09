CREATE PROCEDURE [dbo].[uspReportParameterSession]
	-- Add the parameters for the stored procedure here
--	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
--	@PanelList varchar(4000),
--	@CycleList varchar(4000) = NULL,
	@MeetingList varchar(4000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	WITH 
--		ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)), 
		FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
--		PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList)),
--		CycleParams(Cycle) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@CycleList)),
		MeetingParams(ClientMeetingId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@MeetingList))
	SELECT DISTINCT MeetingSession.MeetingSessionId, MeetingSession.SessionAbbreviation, MeetingSession.SessionDescription, MeetingSession.StartDate, MeetingSession.EndDate
	FROM ViewMeetingSession MeetingSession INNER JOIN
		ViewSessionPanel SessionPanel ON MeetingSession.MeetingSessionId = SessionPanel.MeetingSessionId INNER JOIN
		ViewProgramPanel ProgramPanel ON SessionPanel.SessionPanelId = ProgramPanel.SessionPanelId INNER JOIN
		ViewProgramYear ProgramYear ON ProgramPanel.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
		ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
		ViewProgramMechanism ProgramMechanism ON ProgramYear.ProgramYearId = ProgramMechanism.ProgramYearId INNER JOIN
--		ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
		FiscalYearParams ON ProgramYear.Year = FiscalYearParams.FY INNER JOIN
--		PanelParams ON PanelParams.PanelId = 0 OR ProgramPanel.SessionPanelId = PanelParams.PanelId INNER JOIN
--		CycleParams ON CycleParams.Cycle = 0 OR ProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
		MeetingParams ON MeetingSession.ClientMeetingId = MeetingParams.ClientMeetingId
END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportParameterSession] TO [NetSqlAzMan_Users]
    AS [dbo];