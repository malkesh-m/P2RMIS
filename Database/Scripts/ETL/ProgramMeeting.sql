INSERT INTO [dbo].[ProgramMeeting]
           ([ProgramYearId]
           ,[ClientMeetingId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT DISTINCT ProgramYear.ProgramYearId, ClientMeeting.ClientMeetingId, ClientMeeting.CreatedBy, ClientMeeting.CreatedDate, ClientMeeting.ModifiedBy, ClientMeeting.ModifiedDate
FROM ViewClientMeeting ClientMeeting INNER JOIN
ViewMeetingSession MeetingSession ON ClientMeeting.ClientMeetingId = MeetingSession.ClientMeetingId INNER JOIN
ViewSessionPanel SessionPanel ON MeetingSession.MeetingSessionId = SessionPanel.MeetingSessionId INNER JOIN
ViewProgramPanel ProgramPanel ON SessionPanel.SessionPanelId = ProgramPanel.SessionPanelId INNER JOIN
ViewProgramYear ProgramYear ON ProgramPanel.ProgramYearId = ProgramYear.ProgramYearId
WHERE NOT EXISTS (SELECT 'X' FROM ProgramMeeting WHERE ProgramMeeting.ClientMeetingId = ClientMeeting.ClientMeetingId AND ProgramMeeting.ProgramYearId = ProgramYear.ProgramYearId)