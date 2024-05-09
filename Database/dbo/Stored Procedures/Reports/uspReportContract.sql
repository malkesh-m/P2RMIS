
-- =============================================
-- Author:		Pushpa Unnithan
-- Create date: 10/13/2015
-- Description:	Storeprocedure to Create Client Contract
-- =============================================
CREATE PROCEDURE [dbo].[uspReportContract] 
	-- Add the parameters for the stored procedure here
	@PanelUserAssignmentId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT     [dbo].[ViewPanelUserAssignment].[PanelUserAssignmentId], [dbo].[ClientProgram].[ProgramDescription], [dbo].[ViewProgramYear].[Year], [dbo].[ViewSessionPanel].[PanelAbbreviation], [dbo].[ViewSessionPanel].[PanelName], 
                      [dbo].[ClientParticipantType].[ParticipantTypeAbbreviation], [dbo].[ClientParticipantType].[ParticipantTypeName], [dbo].[ViewPanelUserRegistrationDocumentItem].[Value], 
                      CASE WHEN [dbo].[ViewSessionPanel].[MeetingSessionId] IS NOT NULL THEN [dbo].[ViewSessionPayRate].[PeriodStartDate] ELSE [dbo].[ViewProgramPayRate].[PeriodStartDate] END AS PeriodStartDate, 
                      CASE WHEN [dbo].[ViewSessionPanel].[MeetingSessionId] IS NOT NULL THEN [dbo].[ViewSessionPayRate].[PeriodEndDate] ELSE [dbo].[ViewProgramPayRate].[PeriodEndDate] END AS PeriodEndDate, 
                      CASE WHEN [dbo].[ViewUserInfo].[SuffixText] IS NULL 
                      THEN [dbo].[ViewUserInfo].[FirstName] + ' ' + [dbo].[ViewUserInfo].[LastName] ELSE [dbo].[ViewUserInfo].[FirstName] + ' ' + [dbo].[ViewUserInfo].[LastName] + [dbo].[ViewUserInfo].[SuffixText] END AS Name, 
                      [dbo].[ViewUserInfo].[FirstName], [dbo].[ViewUserInfo].[LastName], CASE WHEN [dbo].[ViewSessionPanel].[MeetingSessionId] IS NOT NULL 
                      THEN [dbo].[ViewSessionPayRate].[ManagerList] ELSE [dbo].[ViewProgramPayRate].[ManagerList] END AS ManagerList, CASE WHEN [dbo].[ViewSessionPanel].[MeetingSessionId] IS NOT NULL 
                      THEN [dbo].[ViewSessionPayRate].[DescriptionOfWork] ELSE [dbo].[ViewProgramPayRate].[DescriptionOfWork] END AS DescriptionOfWork, 
                      CASE WHEN [dbo].[ViewSessionPanel].[MeetingSessionId] IS NOT NULL 
                      THEN [dbo].[ViewSessionPayRate].[ConsultantFeeText] ELSE [dbo].[ViewProgramPayRate].[ConsultantFeeText] END AS ConsultantFeeText, [dbo].[ViewUserInfo].[SuffixText] AS [SuffixName], [dbo].[ViewUserInfo].[Institution], 
                      [dbo].[Client].[ClientDesc], [dbo].[ViewPanelUserRegistrationDocument].[PanelUserRegistrationDocumentId], CASE WHEN [dbo].[ViewUserAddress].[Address1] IS NULL OR
                      [dbo].[ViewUserAddress].[Address1] = '' THEN '' ELSE [dbo].[ViewUserAddress].[Address1] END + CASE WHEN [dbo].[ViewUserAddress].[Address2] IS NULL OR
                      [dbo].[ViewUserAddress].[Address2] = '' THEN '' ELSE [dbo].[ViewUserAddress].[Address2] END + CASE WHEN [dbo].[ViewUserAddress].[Address3] IS NULL OR
                      [dbo].[ViewUserAddress].[Address3] = '' THEN '' ELSE [dbo].[ViewUserAddress].[Address3] END + CASE WHEN [dbo].[ViewUserAddress].[Address4] IS NULL OR
                      [dbo].[ViewUserAddress].[Address4] = '' THEN '' ELSE [dbo].[ViewUserAddress].[Address4] END + CASE WHEN [dbo].[ViewUserAddress].[City] IS NULL OR
                      [dbo].[ViewUserAddress].[City] = '' THEN '' ELSE + CHAR(13) + CHAR(10) + [dbo].[ViewUserAddress].[City] END + CASE WHEN [State].StateAbbreviation IS NULL OR
                      [dbo].[State].[StateAbbreviation] = '' THEN '' ELSE ', ' + [dbo].[State].[StateAbbreviation] END + CASE WHEN [dbo].[ViewUserAddress].[Zip] IS NULL OR
                      [dbo].[ViewUserAddress].[Zip] = '' THEN '' ELSE ' ' + [dbo].[ViewUserAddress].[Zip] END AS 'Address'
FROM        [dbo].ViewPanelUserAssignment INNER JOIN
				ViewSessionPanel ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
				ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId INNER JOIN
				ClientMeeting ON ViewMeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId INNER JOIN
				ViewProgramPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId INNER JOIN
				ViewProgramYear ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
				ClientProgram ON ViewProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
				ViewUserInfo ON ViewPanelUserAssignment.UserId = ViewUserInfo.UserID INNER JOIN
				Client ON ClientProgram.ClientId = Client.ClientID INNER JOIN
				ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId LEFT OUTER JOIN
				ViewUserAddress ON ViewUserInfo.UserInfoID = ViewUserAddress.UserInfoID AND ViewUserAddress.PrimaryFlag = 1 LEFT OUTER JOIN
				[State] ON ViewUserAddress.StateId = [State].StateId INNER JOIN
				[ViewPanelUserRegistration] ON ViewPanelUserAssignment.PanelUserAssignmentId = ViewPanelUserRegistration.PanelUserAssignmentId LEFT OUTER JOIN
				[ViewPanelUserRegistrationDocument] ON ViewPanelUserRegistration.PanelUserRegistrationId = ViewPanelUserRegistrationDocument.PanelUserRegistrationId LEFT OUTER JOIN
				[ViewPanelUserRegistrationDocumentItem] ON ViewPanelUserRegistrationDocument.PanelUserRegistrationDocumentId = ViewPanelUserRegistrationDocumentItem.PanelUserRegistrationDocumentId AND ViewPanelUserRegistrationDocumentItem.RegistrationDocumentItemId = 8 LEFT OUTER JOIN
				[ViewProgramPayRate] ON ClientParticipantType.ClientParticipantTypeId = ViewProgramPayRate.ClientParticipantTypeId AND
				ViewPanelUserAssignment.ParticipationMethodId = ViewProgramPayRate.ParticipantMethodId AND
				ViewPanelUserAssignment.RestrictedAssignedFlag = ViewProgramPayRate.RestrictedAssignedFlag AND
				ViewPanelUserRegistrationDocumentItem.[Value] = ViewProgramPayRate.HonorariumAccepted AND 
				ClientMeeting.MeetingTypeId = ViewProgramPayRate.MeetingTypeId AND
				ViewProgramYear.ProgramYearId = ViewProgramPayRate.ProgramYearId LEFT OUTER JOIN
				[ViewSessionPayRate] ON ClientParticipantType.ClientParticipantTypeId = ViewSessionPayRate.ClientParticipantTypeId AND
				ViewPanelUserAssignment.ParticipationMethodId = ViewSessionPayRate.ParticipantMethodId AND
				ViewPanelUserAssignment.RestrictedAssignedFlag = ViewSessionPayRate.RestrictedAssignedFlag AND
				ViewPanelUserRegistrationDocumentItem.[Value] = ViewSessionPayRate.HonorariumAccepted AND
				ViewSessionPanel.MeetingSessionId = ViewSessionPayRate.MeetingSessionId
          WHERE     ([dbo].[ViewPanelUserAssignment].[PanelUserAssignmentId] = @PanelUserAssignmentId)
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportContract] TO [NetSqlAzMan_Users]
    AS [dbo];
