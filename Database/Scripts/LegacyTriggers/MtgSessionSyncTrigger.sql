CREATE TRIGGER [MtgSessionSyncTrigger]
ON [$(P2RMIS)].dbo.MTG_Session
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE (for updates only, we also need to update dates in panel table)
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted) AND (Select Count(*) FROM inserted) <= 1
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[MeetingSession]
	SET ClientMeetingId = ClientMeeting.ClientMeetingId, LegacySessionId = inserted.Session_ID, SessionAbbreviation = inserted.Session_ID, SessionDescription = inserted.Session_Desc, StartDate = inserted.Start_Date, 
	EndDate = inserted.End_Date, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM deleted CROSS JOIN
		inserted INNER JOIN
		[$(DatabaseName)].[dbo].[ViewClientMeeting] ClientMeeting ON inserted.Meeting_ID = ClientMeeting.LegacyMeetingId INNER JOIN
		[$(DatabaseName)].[dbo].[MeetingSession] MeetingSession ON deleted.Session_ID = MeetingSession.LegacySessionId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE MeetingSession.DeletedFlag = 0
	UPDATE [$(DatabaseName)].[dbo].[SessionPanel]
	SET StartDate = inserted.Start_Date, EndDate = inserted.End_Date, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM deleted CROSS JOIN
		inserted INNER JOIN
		[$(DatabaseName)].[dbo].[MeetingSession] MeetingSession ON deleted.Session_ID = MeetingSession.LegacySessionId INNER JOIN
		[$(DatabaseName)].[dbo].[SessionPanel] SessionPanel ON MeetingSession.MeetingSessionId = SessionPanel.MeetingSessionId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE SessionPanel.DeletedFlag = 0
	UPDATE [$(DatabaseName)].[dbo].[PanelStageStep]
	SET StartDate = inserted.Start_Date, EndDate = inserted.End_Date, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM deleted CROSS JOIN
		inserted INNER JOIN
		[$(DatabaseName)].[dbo].[MeetingSession] MeetingSession ON inserted.Session_ID = MeetingSession.LegacySessionId INNER JOIN
		[$(DatabaseName)].[dbo].[SessionPanel] SessionPanel ON MeetingSession.MeetingSessionId = SessionPanel.MeetingSessionId INNER JOIN
		[$(DatabaseName)].[dbo].[PanelStage] PanelStage ON SessionPanel.SessionPanelId = PanelStage.SessionPanelId INNER JOIN
		[$(DatabaseName)].[dbo].[PanelStageStep] PanelStageStep ON PanelStage.PanelStageId = PanelStageStep.PanelStageId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE PanelStage.ReviewStageId = 2 AND PanelStageStep.DeletedFlag = 0
	END
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	INSERT INTO [$(DatabaseName)].[dbo].[MeetingSession]
           ([LegacySessionId]
           ,[ClientMeetingId]
           ,[SessionAbbreviation]
           ,[SessionDescription]
           ,[StartDate]
           ,[EndDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT inserted.Session_ID, ClientMeeting.ClientMeetingId, inserted.Session_ID, inserted.Session_Desc,
	inserted.Start_Date, inserted.End_Date, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[ViewClientMeeting] ClientMeeting ON inserted.Meeting_ID = ClientMeeting.LegacyMeetingId	LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	--DELETE
	ELSE
	UPDATE [$(DatabaseName)].[dbo].[MeetingSession] 
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
			[$(DatabaseName)].dbo.MeetingSession MeetingSession ON deleted.Session_ID = MeetingSession.LegacySessionId
	WHERE MeetingSession.DeletedFlag = 0
END