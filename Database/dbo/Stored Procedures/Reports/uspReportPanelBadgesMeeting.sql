--EXEC [dbo].[uspReportPanelBadgesMeeting] '2013','1','347'
--EXEC [dbo].[uspReportPanelBadgesMeeting] '2013','1','167'



create PROCEDURE [dbo].[uspReportPanelBadgesMeeting]
	-- Add the parameters for the stored procedure here
	@FiscalYearList varchar(4000),
	@MeetingTypeList varchar(4000),
	
	@MeetingList varchar(4000)

AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
WITH 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	MeetingTypeParams(MeetingTypeID) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@MeetingTypeList)), 	
	MeetingParams(ClientMeetingId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@MeetingList))


 
select distinct  ViewMeetingSession.MeetingSessionId, ViewMeetingSession.SessionDescription 
from			
				clientprogram 
				join ViewProgramYear on ViewProgramYear.ClientProgramId = clientprogram.ClientProgramId
				join ViewProgramPanel on ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId
		 		join ViewSessionPanel on ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId
				join ViewMeetingSession on ViewMeetingSession.MeetingSessionId = ViewSessionPanel.MeetingSessionId
				join ViewClientMeeting on ViewClientMeeting.ClientMeetingId = ViewMeetingSession.ClientMeetingId
				join MeetingType on MeetingType.MeetingTypeId = ViewClientMeeting.MeetingTypeId
join	

	FiscalYearParams ON  ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
	MeetingTypeParams ON MeetingTypeParams.MeetingTypeID = MeetingType.MeetingTypeId join
	MeetingParams ON ViewMeetingSession.ClientMeetingId = MeetingParams.ClientMeetingId 
	   
ORDER BY			  ViewMeetingSession.SessionDescription 


 end

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportPanelBadgesMeeting] TO [NetSqlAzMan_Users]
    AS [dbo];                   
               

