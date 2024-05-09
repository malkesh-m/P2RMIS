CREATE PROCEDURE [dbo].[uspReportThankYouLetter]
	-- Add the parameters for the stored procedure here
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@PanelList varchar(4000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
WITH 
	ProgramParams(ClientProgramID) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))
SELECT DISTINCT ClientProgram.ProgramAbbreviation, ClientParticipantType.ParticipantTypeAbbreviation, ClientParticipantType.ParticipantTypeName, Prefix.PrefixName,
	UserInfo.FirstName, UserInfo.LastName, UserInfo.SuffixText, UserAddress.Address1, UserAddress.Address2, UserAddress.Address3, UserAddress.Address4,
	UserAddress.City, UserAddress.StateAbbreviation, UserAddress.Zip, SessionPanel.PanelAbbreviation, UserEmail.Email, ClientMeeting.ClientMeetingId
FROM ClientProgram INNER JOIN
	ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId INNER JOIN
	ViewProgramPanel ProgramPanel ON ProgramYear.ProgramYearId = ProgramPanel.ProgramYearId INNER JOIN
	ViewSessionPanel SessionPanel ON ProgramPanel.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
	ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
    FiscalYearParams ON ProgramYear.Year = FiscalYearParams.FY INNER JOIN
    PanelParams ON PanelParams.PanelId = 0 OR SessionPanel.SessionPanelId = PanelParams.PanelId INNER JOIN
	ViewPanelUserAssignment PanelUserAssignment ON SessionPanel.SessionPanelId = PanelUserAssignment.SessionPanelId INNER JOIN
	ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId INNER JOIN
	ViewUser [User] ON PanelUserAssignment.UserId = [User].UserID INNER JOIN
	ViewMeetingSession MeetingSession ON SessionPanel.MeetingSessionId = MeetingSession.MeetingSessionId INNER JOIN
	ViewClientMeeting ClientMeeting ON MeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId INNER JOIN
	ViewUserInfo UserInfo On [User].UserID = UserInfo.UserID LEFT OUTER JOIN
	Prefix ON UserInfo.PrefixId = Prefix.PrefixId LEFT OUTER JOIN
	ViewUserEmail UserEmail ON UserInfo.UserInfoID = UserEmail.UserInfoID 
	and UserEmail.PrimaryFlag = 1 LEFT OUTER JOIN
	(Select UserInfoId, Address1, Address2, Address3, Address4, City, StateAbbreviation, Zip
	FROM ViewUserAddress INNER JOIN
	[State] ON ViewUserAddress.StateId = [State].StateId
	WHERE PrimaryFlag = 1) UserAddress ON UserInfo.UserInfoID = UserAddress.UserInfoID
WHERE ClientParticipantType.ReviewerFlag = 1 OR ClientParticipantType.LegacyPartTypeId = 'SRA'
END
 GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportThankYouLetter] TO [NetSqlAzMan_Users]
    AS [dbo];