CREATE PROCEDURE [dbo].[uspReportReimbursement]
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@PanelList varchar(4000)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	WITH ProgramParams(ClientProgramId)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
	FiscalYearParams(FY)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
	PanelParams(PanelId)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))

	SELECT				ClientProgram.ProgramAbbreviation,
						ViewProgramYear.Year, 
						ViewSessionPanel.PanelAbbreviation,
						ViewUserInfo.FirstName, 
						ViewUserInfo.LastName, 
						ViewUserVendor.VendorName,
	                    ViewUserInfo.SuffixText, 
						ViewUserInfo.Institution, 
						ViewUserInfo.Department, 
						ViewUserAddress.Address1, 
						ViewUserAddress.Address2, 
			            ViewUserAddress.Address3, 
						ViewUserAddress.Address4, 
						ViewUserAddress.City, 
						ViewUserAddress.StateId, 
						ViewUserAddress.Zip, 
						Client.ClientAbrv, 
					    ViewMeetingSession.SessionAbbreviation, 
						ViewClientMeeting.MeetingAbbreviation, 
						ViewClientMeeting.MeetingDescription, 
						ViewMeetingSession.SessionDescription, 
			            ViewSessionPanel.PanelName, 
						State.StateAbbreviation, 
						ProfileType.ProfileTypeName,
						ViewUserInfo.UserInfoID
	FROM				ViewUserInfo
	LEFT OUTER JOIN		ViewUserAddress ON ViewUserInfo.UserInfoID = ViewUserAddress.UserInfoID 
										AND ViewUserAddress.AddressTypeId = 4
	LEFT OUTER JOIN     ViewUserVendor ON ViewUserInfo.UserInfoID = ViewUserVendor.UserInfoId 
									   AND ViewUserVendor.ActiveFlag = 1
	INNER JOIN          ViewUserProfile ON ViewUserInfo.UserInfoID = ViewUserProfile.UserInfoId
	INNER JOIN          ProfileType ON ViewUserProfile.ProfileTypeId = ProfileType.ProfileTypeId 
	INNER JOIN          ViewPanelUserAssignment ON ViewUserInfo.UserId = ViewPanelUserAssignment.UserID
	INNER JOIN			ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId
	INNER JOIN          ViewSessionPanel ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId
	INNER JOIN          ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId
	INNER JOIN          ViewClientMeeting ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId
	INNER JOIN          ViewProgramPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId 
	INNER JOIN          ViewProgramYear ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId
	INNER JOIN          ClientProgram ON ViewProgramYear.ClientProgramId = ClientProgram.ClientProgramId
	INNER JOIN          Client ON ClientProgram.ClientId = Client.ClientID
	LEFT OUTER JOIN     State ON ViewUserAddress.StateId = State.StateId
	INNER JOIN          ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId 
	INNER JOIN          FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY 
	INNER JOIN		    PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId
	WHERE ClientParticipantType.ReviewerFlag = 1

END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportReimbursement] TO [NetSqlAzMan_Users]
    AS [dbo];