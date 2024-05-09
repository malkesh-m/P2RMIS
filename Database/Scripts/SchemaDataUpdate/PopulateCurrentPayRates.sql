INSERT INTO [dbo].[PanelUserRegistrationDocumentContract]
           ([PanelUserRegistrationDocumentId]
           ,[ContractStatusId]
           ,[FeeAmount]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ViewPanelUserRegistrationDocument.PanelUserRegistrationDocumentId, 1, ISNULL(ViewSessionPayRate.ConsultantFee, ViewProgramPayRate.ConsultantFee), 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime()
FROM ViewPanelUserRegistrationDocument
INNER JOIN ClientRegistrationDocument ON ViewPanelUserRegistrationDocument.ClientRegistrationDocumentId = ClientRegistrationDocument.ClientRegistrationDocumentId
INNER JOIN ViewPanelUserRegistration ON ViewPanelUserRegistrationDocument.PanelUserRegistrationId = ViewPanelUserRegistration.PanelUserRegistrationId
INNER JOIN ViewPanelUserAssignment ON ViewPanelUserRegistration.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId
INNER JOIN ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId
INNER JOIN ViewSessionPanel ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId
INNER JOIN ViewProgramPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId
INNER JOIN ViewProgramYear ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId
INNER JOIN ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId
INNER JOIN ViewClientMeeting ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId
INNER JOIN ViewPanelUserRegistrationDocument Acknowledge ON ViewPanelUserRegistration.PanelUserRegistrationId = Acknowledge.PanelUserRegistrationId
INNER JOIN ViewPanelUserRegistrationDocumentItem Acknowledgement ON Acknowledge.PanelUserRegistrationDocumentId = Acknowledgement.PanelUserRegistrationDocumentId AND Acknowledgement.RegistrationDocumentItemId = 8
-- get ConsultantFee from SesssionPayRate
left join ViewProgramSessionPayRate ViewSessionPayRate on ViewSessionPayRate.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId
     and ViewSessionPayRate.MeetingSessionId = ViewMeetingSession.MeetingSessionId
     and  ViewPanelUserAssignment.ParticipationMethodId = ViewSessionPayRate.ParticipantMethodId
     and ViewPanelUserAssignment.RestrictedAssignedFlag = ViewSessionPayRate.RestrictedAssignedFlag
     and ViewSessionPayRate.HonorariumAccepted = Acknowledgement.Value
     and ViewSessionPayRate.ProgramYearId = ViewProgramYear.ProgramYearId
-- get ConsultantFee from ProgramnPayRate
left join ViewProgramSessionPayRate ViewProgramPayRate on ViewProgramPayRate.ProgramYearId =  ViewProgramYear.ProgramYearId
     and ViewProgramPayRate.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId
     and ViewPanelUserAssignment.ParticipationMethodId = ViewProgramPayRate.ParticipantMethodId
     and ViewPanelUserAssignment.RestrictedAssignedFlag = ViewProgramPayRate.RestrictedAssignedFlag
     and ViewProgramPayRate.MeetingTypeId = ViewClientMeeting.MeetingTypeId
     and ViewProgramPayRate.HonorariumAccepted = Acknowledgement.Value
     and ViewProgramPayRate.MeetingSessionId IS NULL
WHERE ViewProgramYear.[Year] > 2018 AND ClientRegistrationDocument.RegistrationDocumentTypeId = 3 AND ViewPanelUserRegistrationDocument.DateSigned IS NOT NULL AND ViewPanelUserRegistrationDocument.SignedOfflineFlag = 0 AND
NOT EXISTS (SELECT 'X' FROM PanelUserRegistrationDocumentContract WHERE PanelUserRegistrationDocumentId = ViewPanelUserRegistrationDocument.PanelUserRegistrationDocumentId AND DeletedFlag = 0);