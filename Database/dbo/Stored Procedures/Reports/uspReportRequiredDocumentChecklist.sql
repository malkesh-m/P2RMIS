CREATE PROCEDURE [dbo].[uspReportRequiredDocumentChecklist]
	-- Add the parameters for the stored procedure here
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@PanelList varchar(4000)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;
	WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))
SELECT ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ProgramYear.[Year], SessionPanel.PanelAbbreviation,
	SessionPanel.StartDate, SessionPanel.EndDate, Rta.PanelRtas, Sro.PanelSros, UserInfo.FirstName, UserInfo.LastName,
	ClientParticipantType.ParticipantTypeAbbreviation, ParticipationMethod.ParticipationMethodLabel, PanelUserAssignment.RestrictedAssignedFlag,
	ClientRole.RoleName, PanelUserAssignment.CreatedDate AS DateAssignedToPanel, [User].VerifiedDate, [UserInfo].ModifiedDate As PersonalInfoLastUpdateDate,
	 Acknowledgement.DateSigned AS AcknowledgementSignedDate, BiasCoi.DateSigned AS BiasCoiSignedDate, [Contract].DateSigned AS ContractSignedDate,
	 Acknowledgement.PaymentCategory, CASE WHEN [User].W9Verified = 1 THEN 'Accurate' WHEN [User].W9Verified = 0 THEN 'Inaccurate' ELSE '' END AS W9Verified,
	 [User].W9VerifiedDate, [UserResume].ModifiedDate AS CvUploadDate, TravelMode.TravelModeAbbreviation, MeetingRegistrationTravel.NteAmount, 
	 MeetingRegistrationHotel.HotelCheckInDate, MeetingRegistrationHotel.HotelCheckOutDate, MeetingRegistrationTravel.TravelRequestComments, UserAddress.ModifiedDate AS W9UploadDate
FROM  ClientProgram INNER JOIN
               ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId INNER JOIN
               ViewProgramPanel ProgramPanel ON ProgramYear.ProgramYearId = ProgramPanel.ProgramYearId INNER JOIN
               ViewSessionPanel SessionPanel ON ProgramPanel.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
               ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
               FiscalYearParams ON ProgramYear.Year = FiscalYearParams.FY INNER JOIN
               PanelParams ON PanelParams.PanelId = 0 OR SessionPanel.SessionPanelId = PanelParams.PanelId INNER JOIN
               ViewPanelUserAssignment PanelUserAssignment ON SessionPanel.SessionPanelId = PanelUserAssignment.SessionPanelId INNER JOIN
               ViewUser [User]  ON PanelUserAssignment.UserId = [User].UserID INNER JOIN
               ViewUserInfo UserInfo ON PanelUserAssignment.UserId = UserInfo.UserID INNER JOIN
               ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId LEFT OUTER JOIN
               ClientRole ON PanelUserAssignment.ClientRoleId = ClientRole.ClientRoleId LEFT OUTER JOIN
               ViewPanelUserRegistration PanelUserRegistration ON PanelUserAssignment.PanelUserAssignmentId = PanelUserRegistration.PanelUserAssignmentId OUTER APPLY
               dbo.udfPanelRtasDelimited(SessionPanel.SessionPanelId) Rta OUTER APPLY
               dbo.udfPanelSrosDelimited(SessionPanel.SessionPanelId) Sro LEFT OUTER JOIN
               (Select ViewPanelUserRegistrationDocument.PanelUserRegistrationId, ViewPanelUserRegistrationDocument.DateSigned, ViewPanelUserRegistrationDocumentItem.Value AS PaymentCategory
               FROM ViewPanelUserRegistrationDocument INNER JOIN
               ClientRegistrationDocument ON ViewPanelUserRegistrationDocument.ClientRegistrationDocumentId = ClientRegistrationDocument.ClientRegistrationDocumentId INNER JOIN
               ViewPanelUserRegistrationDocumentItem ON ViewPanelUserRegistrationDocument.PanelUserRegistrationDocumentId = ViewPanelUserRegistrationDocumentItem.PanelUserRegistrationDocumentId
               WHERE ViewPanelUserRegistrationDocumentItem.RegistrationDocumentItemId = 8 AND ClientRegistrationDocument.DocumentAbbreviation = 'AckNDA') 
               Acknowledgement ON PanelUserRegistration.PanelUserRegistrationId = Acknowledgement.PanelUserRegistrationId LEFT OUTER JOIN
               (Select ViewPanelUserRegistrationDocument.PanelUserRegistrationId, ViewPanelUserRegistrationDocument.DateSigned
               FROM ViewPanelUserRegistrationDocument INNER JOIN
               ClientRegistrationDocument ON ViewPanelUserRegistrationDocument.ClientRegistrationDocumentId = ClientRegistrationDocument.ClientRegistrationDocumentId 
               WHERE ClientRegistrationDocument.DocumentAbbreviation = 'BiasCOI') 
               BiasCoi ON PanelUserRegistration.PanelUserRegistrationId = BiasCoi.PanelUserRegistrationId LEFT OUTER JOIN
               (Select ViewPanelUserRegistrationDocument.PanelUserRegistrationId, ViewPanelUserRegistrationDocument.DateSigned
               FROM ViewPanelUserRegistrationDocument INNER JOIN
               ClientRegistrationDocument ON ViewPanelUserRegistrationDocument.ClientRegistrationDocumentId = ClientRegistrationDocument.ClientRegistrationDocumentId 
               WHERE ClientRegistrationDocument.DocumentAbbreviation = 'Contract') 
               [Contract] ON PanelUserRegistration.PanelUserRegistrationId = [Contract].PanelUserRegistrationId LEFT OUTER JOIN
               ViewUserResume [UserResume] ON UserInfo.UserInfoID = UserResume.UserInfoId LEFT OUTER JOIN
               ViewMeetingRegistration [MeetingRegistration] ON PanelUserAssignment.PanelUserAssignmentId = MeetingRegistration.PanelUserAssignmentId LEFT OUTER JOIN
               ViewMeetingRegistrationTravel [MeetingRegistrationTravel] ON MeetingRegistration.MeetingRegistrationId = MeetingRegistrationTravel.MeetingRegistrationId AND MeetingRegistrationTravel.CancellationFlag = 0 LEFT OUTER JOIN
               ViewMeetingRegistrationHotel [MeetingRegistrationHotel] ON MeetingRegistration.MeetingRegistrationId = MeetingRegistrationHotel.MeetingRegistrationId AND MeetingRegistrationHotel.CancellationFlag = 0 LEFT OUTER JOIN
               ParticipationMethod ON PanelUserAssignment.ParticipationMethodId = ParticipationMethod.ParticipationMethodId LEFT OUTER JOIN
               TravelMode ON MeetingRegistrationTravel.TravelModeId = TravelMode.TravelModeId LEFT OUTER JOIN
			   ViewUserAddress UserAddress ON UserInfo.UserInfoID = UserAddress.UserInfoID AND UserAddress.AddressTypeId = 4
 WHERE ClientParticipantType.ReviewerFlag =  1
 END
 GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportRequiredDocumentChecklist] TO [NetSqlAzMan_Users]
    AS [dbo];