-- =============================================
-- exec [uspReportParameterProgram] '2013', '351'
-- ===========================================
CREATE PROCEDURE [dbo].[uspReportParameterProgram] 
-- Add the parameters for the stored procedure here
@FiscalYearList varchar(4000),
@MeetingList varchar(4000)

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

	WITH 
		FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
		MeetingParams(ClientMeetingId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@MeetingList))

	SELECT distinct ViewProgramYear.YEAR, 
					ViewClientMeeting.ClientMeetingId,
					ViewClientMeeting.MeetingAbbreviation,
					ViewClientMeeting.MeetingDescription,
				    ClientProgram.ClientProgramId, 
					ClientProgram.ProgramAbbreviation
	FROM ClientProgram
		INNER JOIN ViewProgramYear		ON ClientProgram.ClientProgramId= ViewProgramYear.ClientProgramId
		INNER JOIN ViewProgramPanel		ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId
		INNER JOIN ViewSessionPanel		ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId
		INNER JOIN ViewMeetingSession	ON ViewMeetingSession.MeetingSessionId = ViewSessionPanel.MeetingSessionId
		INNER JOIN ViewClientMeeting	ON ViewClientMeeting.ClientMeetingId = ViewMeetingSession.ClientMeetingId
		INNER JOIN FiscalYearParams		ON ViewProgramYear.[Year] = FiscalYearParams.FY
		INNER JOIN MeetingParams		ON ViewMeetingSession.ClientMeetingId = MeetingParams.ClientMeetingId 

END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportParameterProgram] TO [NetSqlAzMan_Users]
    AS [dbo];                   
                  
