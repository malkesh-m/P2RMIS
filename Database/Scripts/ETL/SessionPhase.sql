--Add pre-meeting phase information
INSERT INTO [dbo].[SessionPhase]
           ([MeetingSessionId]
           ,[StepTypeId]
           ,[SortOrder]
           ,[StartDate]
           ,[EndDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT MeetingSession.MeetingSessionId, StepType.StepTypeId, MTG_Phase_Member.Phase_Order, MTG_Phase_Member.Phase_Start, MTG_Phase_Member.Phase_End, VUN.UserId, MTG_Phase_Member.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.MTG_Phase_Member MTG_Phase_Member INNER JOIN
	ViewMeetingSession MeetingSession ON MTG_Phase_Member.Session_ID = MeetingSession.LegacySessionId INNER JOIN
	StepType ON MTG_Phase_Member.Phase_ID = CASE StepType.StepTypeId WHEN 5 THEN 1 WHEN 6 THEN 2 WHEN 7 THEN 3 ELSE NULL END LEFT OUTER JOIN
	ViewLegacyUserNameToUserId VUN ON MTG_Phase_Member.LAST_UPDATED_BY = VUN.UserName
WHERE NOT EXISTS (Select 'X' FROM SessionPhase WHERE MeetingSession.MeetingSessionId = SessionPhase.MeetingSessionId AND SessionPhase.DeletedFlag = 0);
--Add meeting phase information
INSERT INTO [dbo].[SessionPhase]
           ([MeetingSessionId]
           ,[StepTypeId]
           ,[SortOrder]
           ,[StartDate]
           ,[EndDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT MeetingSession.MeetingSessionId, 8, 4, MeetingSession.StartDate, MeetingSession.EndDate, MeetingSession.ModifiedBy, MeetingSession.ModifiedDate
FROM MeetingSession 
	INNER JOIN ClientMeeting ON MeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId
WHERE MeetingSession.DeletedFlag = 0 AND ClientMeeting.MeetingTypeId <> 3 AND
NOT EXISTS (Select 'X' FROM SessionPhase WHERE MeetingSession.MeetingSessionId = SessionPhase.MeetingSessionId AND SessionPhase.DeletedFlag = 0 AND SessionPhase.StepTypeId = 8);