CREATE PROCEDURE [dbo].[uspReportSRORTAList] 
	-- Add the parameters for the stored procedure here
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

SELECT DISTINCT 
                      ClientProgram.ClientProgramId, dbo.ClientProgram.ProgramAbbreviation, dbo.ClientProgram.ProgramDescription, ViewProgramYear.year, 
                      ViewSessionPanel.SessionPanelId, ViewSessionPanel.PanelAbbreviation, ViewSessionPanel.PanelName, 
					  convert(varchar(10),cast(ViewSessionPanel.StartDate as date), 101) StartDate, 
					  convert(varchar(10),cast(ViewSessionPanel.EndDate as date), 101) EndDate, 
                      dbo.ClientParticipantType.SROFlag, dbo.ClientParticipantType.RTAFlag, 
                      CASE WHEN dbo.ClientParticipantType.SROFlag = 1 THEN dbo.ViewUserInfo.FirstName + ' ' + dbo.ViewUserInfo.LastName ELSE dbo.ViewUserInfo.FirstName + ' ' + dbo.ViewUserInfo.LastName
                       END AS Name, dbo.ViewUserInfo.FirstName, dbo.ViewUserInfo.LastName, dbo.ViewMeetingSession.SessionDescription, 
                      dbo.ViewMeetingSession.MeetingSessionId
FROM         dbo.ClientProgram INNER JOIN
                      dbo.ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
                      dbo.ViewProgramPanel ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
                      dbo.ViewSessionPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId INNER JOIN
                      dbo.ViewPanelUserAssignment ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
                      dbo.ClientParticipantType ON ClientParticipantType.ClientParticipantTypeId = ViewPanelUserAssignment.ClientParticipantTypeId INNER JOIN
                      dbo.ViewUserInfo ON dbo.ViewPanelUserAssignment.UserId = dbo.ViewUserInfo.UserID INNER JOIN
                      dbo.ViewMeetingSession ON dbo.ViewSessionPanel.MeetingSessionId = dbo.ViewMeetingSession.MeetingSessionId INNER JOIN   
					  ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN   
					  FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN   
					  PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId 
WHERE    (dbo.ClientParticipantType.SROFlag = 1 OR dbo.ClientParticipantType.RTAFlag = 1)
GROUP BY ClientProgram.ClientProgramId, ViewProgramYear.year, ViewSessionPanel.SessionPanelId, ViewSessionPanel.PanelAbbreviation, ViewSessionPanel.PanelName, 
                      ViewSessionPanel.StartDate, ViewSessionPanel.EndDate, dbo.ClientProgram.ProgramAbbreviation, dbo.ClientProgram.ProgramDescription, 
                      dbo.ClientParticipantType.SROFlag, dbo.ClientParticipantType.RTAFlag, dbo.ViewUserInfo.FirstName, dbo.ViewUserInfo.LastName, 
                      dbo.ViewMeetingSession.SessionDescription, dbo.ViewMeetingSession.MeetingSessionId

END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportSRORTAList] TO [NetSqlAzMan_Users]
    AS [dbo];