CREATE PROCEDURE [dbo].[uspReportMeetingPlanningSummary]
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

	select distinct 
		ViewApplication.LogNumber, 
				clientprogram.ProgramAbbreviation as [Program],
				clientprogram.ClientProgramId,
				ViewProgramYear.Year as [FY],
				ViewSessionPanel.PanelAbbreviation as [Panel],
				null as [Room],
				convert(varchar,ViewMeetingSession.StartDate,23) + '  -  ' + convert(varchar,ViewMeetingSession.EndDate,23) as [Session Dates],
				null as [Estimated End Time]
		, MeetingType.MeetingTypeID
		, ViewMeetingSession.MeetingSessionId
		, ViewMeetingSession.SessionDescription
		, ViewClientMeeting.ClientMeetingId
		, ViewClientMeeting.MeetingDescription
		, MeetingType.MeetingTypeName
		, ViewApplicationReviewStatus.ReviewStatusId
		, ClientParticipantType.ParticipantTypeAbbreviation
		, ClientParticipantType.ReviewerFlag
		, ViewPanelUserAssignment.RestrictedAssignedFlag
		, ViewPanelUserAssignment.ParticipationMethodId
		, ClientParticipantType.ConsumerFlag
		, ViewSessionPanel.SessionPanelId panelID

	from 
		clientprogram 
		join ViewProgramYear on ViewProgramYear.ClientProgramId = clientprogram.ClientProgramId
		join ViewProgramPanel on ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId
		join ViewSessionPanel on ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId
			INNER JOIN ViewPanelUserAssignment ON ViewSessionPanel.SessionPanelId = ViewPanelUserAssignment.SessionPanelId
			join ViewPanelApplicationReviewerAssignment on ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId
			join ViewPanelApplication on ViewPanelApplication.PanelApplicationId = ViewPanelApplicationReviewerAssignment.PanelApplicationId
			join ViewApplication on ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId
				join ViewMeetingSession on ViewMeetingSession.MeetingSessionId = ViewSessionPanel.MeetingSessionId
				join ViewClientMeeting on ViewClientMeeting.ClientMeetingId = ViewMeetingSession.ClientMeetingId
				join MeetingType on MeetingType.MeetingTypeId = ViewClientMeeting.MeetingTypeId
				INNER JOIN ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId
				join ViewApplicationReviewStatus on ViewApplicationReviewStatus.PanelApplicationId = ViewPanelApplication.PanelApplicationId
				
	join	

	FiscalYearParams ON  ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
	MeetingTypeParams ON MeetingTypeParams.MeetingTypeID = MeetingType.MeetingTypeId join
	MeetingParams ON ViewMeetingSession.ClientMeetingId = MeetingParams.ClientMeetingId 

	where ClientParticipantType.ParticipantScope = 'Panel'

	order by ViewApplication.LogNumber

end


GO


GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportMeetingPlanningSummary] TO [NetSqlAzMan_Users]
    AS [dbo];