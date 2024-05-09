--EXEC [dbo].[uspReportPanelBadgesMeeting] '2013','1','347'
--EXEC [dbo].[uspReportPanelBadgesMeeting] '2013','1','167'



create PROCEDURE [dbo].[uspReportPanelBadges]
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



	SELECT DISTINCT       clientprogram.ProgramAbbreviation AS Program, 
					  viewmeetingsession.SessionAbbreviation AS Session, 
					  ViewSessionPanel.PanelAbbreviation AS Panel, 
                      viewuserinfo.BadgeName AS [Badge Name], 
					  viewuserinfo.Institution,
					  ClientParticipantType.ParticipantTypeName AS Position, 
					--  ViewUserProfile.ProfileTypeId, 
                   --   dbo.MeetingType.MeetingTypeName, 
					  viewuserinfo.LastName + ', ' + viewuserinfo.FirstName AS [Name],
					  ViewClientMeeting.MeetingDescription,
					  ViewClientMeeting.ClientMeetingId,
					  ViewMeetingSession.MeetingSessionId,ViewMeetingSession.SessionDescription 


FROM			clientprogram 
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

                      INNER JOIN dbo.ViewPanelUserAssignment ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId 
					  INNER JOIN dbo.ClientParticipantType ON viewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId 
														   AND dbo.ClientProgram.ClientId = dbo.ClientParticipantType.ClientId 
					  INNER JOIN dbo.ViewUserInfo ON ViewUserInfo.UserID = ViewPanelUserAssignment.UserId 
				--	  INNER JOIN dbo.ViewUserProfile ON ViewUserProfile.UserInfoId = ViewUserInfo.UserInfoID 
				--	  INNER JOIN dbo.ProfileType ON profiletype.ProfileTypeId = ViewUserProfile.ProfileTypeId 
				--	  INNER JOIN dbo.ViewMeetingSession ON dbo.ViewSessionPanel.MeetingSessionId = dbo.ViewMeetingSession.MeetingSessionId
				--	  INNER JOIN dbo.ViewClientMeeting ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId
					 -- INNER JOIN dbo.ViewClientMeeting ON dbo.ViewMeetingSession.ClientMeetingId = dbo.ViewClientMeeting.ClientMeetingId
					  --JOIN dbo.MeetingType ON dbo.ViewClientMeeting.MeetingTypeId = dbo.MeetingType.MeetingTypeId
 WHERE (ViewPanelUserAssignment.ParticipationMethodId = 1)   
ORDER BY [Name]

end

GO

GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportPanelBadges] TO [NetSqlAzMan_Users]
    AS [dbo];                   
                    



































