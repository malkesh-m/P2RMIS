CREATE TRIGGER [MtgPhaseMemberSyncTrigger]
ON [$(P2RMIS)].dbo.MTG_Phase_Member
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
		--In legacy system only phase 1 steps are modifiable in the system
		UPDATE [$(DatabaseName)].[dbo].PanelStageStep 
		SET StartDate = inserted.Phase_Start, EndDate = inserted.Phase_End, StepOrder = inserted.Phase_Order,
		ReOpenDate = inserted.Phase_ReOpen, ReCloseDate = inserted.Phase_ReClose, ModifiedBy = VUN.UserId,
		ModifiedDate = inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		 [$(DatabaseName)].[dbo].ViewMeetingSession MeetingSession ON inserted.Session_ID = MeetingSession.LegacySessionId INNER JOIN
		 [$(DatabaseName)].[dbo].ViewSessionPanel SessionPanel ON MeetingSession.MeetingSessionId = SessionPanel.MeetingSessionId INNER JOIN
		 [$(DatabaseName)].[dbo].ViewPanelStage PanelStage ON PanelStage.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
		 [$(DatabaseName)].[dbo].PanelStageStep PanelStageStep ON PanelStage.PanelStageId = PanelStageStep.PanelStageId AND
		 PanelStageStep.StepTypeId = CASE inserted.Phase_Id WHEN 1 THEN 5 WHEN 2 THEN 6 WHEN 3 THEN 7 END  LEFT OUTER JOIN
		 [$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
		WHERE PanelStage.ReviewStageId = 1 AND PanelStageStep.DeletedFlag = 0;
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted)
	BEGIN
		INSERT INTO [$(DatabaseName)].[dbo].[PanelStageStep]
			   ([PanelStageId]
			   ,[StepTypeId]
			   ,[StepName]
			   ,[StepOrder]
			   ,[StartDate]
			   ,[EndDate]
			   ,[ReOpenDate]
			   ,[ReCloseDate]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
		SELECT PanelStage.PanelStageId, CASE inserted.Phase_Id WHEN 1 THEN 5 WHEN 2 THEN 6 WHEN 3 THEN 7 END, PRG_Phase.Phase_Description,
		inserted.Phase_Order, inserted.Phase_Start, inserted.Phase_End, inserted.Phase_ReOpen, inserted.Phase_ReClose, VUN.UserId,
		inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		 [$(DatabaseName)].[dbo].ViewMeetingSession MeetingSession ON inserted.Session_ID = MeetingSession.LegacySessionId INNER JOIN
		 [$(DatabaseName)].[dbo].ViewSessionPanel SessionPanel ON MeetingSession.MeetingSessionId = SessionPanel.MeetingSessionId INNER JOIN
		 [$(DatabaseName)].[dbo].ViewPanelStage PanelStage ON PanelStage.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
		 [$(P2RMIS)].[dbo].PRG_Phase PRG_Phase ON inserted.Phase_Id = PRG_Phase.Phase_Id LEFT OUTER JOIN
		 [$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
		WHERE PanelStage.ReviewStageId = 1;

		WITH PanelStepsCte
		AS (
			SELECT SUM(CASE WHEN MTG_Phase_Member.Phase_ID =1 THEN 1 ELSE 0 END) AS PrelimCount, SUM(CASE WHEN MTG_Phase_Member.Phase_ID =2 THEN 1 ELSE 0 END) AS RevisedCount,
				SUM(CASE WHEN MTG_Phase_Member.Phase_ID =3 THEN 1 ELSE 0 END) AS FinalCount, inserted.Session_ID AS SessionId
			FROM inserted INNER JOIN
				[$(P2RMIS)].dbo.MTG_Phase_Member MTG_Phase_Member ON inserted.Session_ID = MTG_Phase_Member.Session_ID
			GROUP BY inserted.Session_ID
		)
		UPDATE [$(DatabaseName)].[dbo].[PanelStage] 
		SET WorkflowId = (Select WorkflowId FROM [$(DatabaseName)].[dbo].Workflow Workflow WHERE Workflow.ClientId = ClientMeeting.ClientId AND Workflow.ReviewStageId = 1 AND 
		Workflow.WorkflowName = CASE WHEN PanelStepsCte.FinalCount = 1 AND PanelStepsCte.RevisedCount = 1 THEN 'Online Discussion' WHEN PanelStepsCte.FinalCount = 1 THEN 'Online Discussion (Legacy)'
		WHEN PanelStepsCte.RevisedCount = 1 THEN 'Pre-Meeting' ELSE 'Pre-Meeting (Legacy)' END)
		FROM inserted INNER JOIN
		PanelStepsCte ON inserted.Session_ID = PanelStepsCte.SessionId INNER JOIN
		[$(DatabaseName)].[dbo].[ViewMeetingSession] MeetingSession ON inserted.Session_Id = MeetingSession.LegacySessionId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientMeeting] ClientMeeting ON MeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId INNER JOIN
		[$(DatabaseName)].[dbo].[ViewSessionPanel] SessionPanel ON MeetingSession.MeetingSessionId = SessionPanel.MeetingSessionId INNER JOIN
		 [$(DatabaseName)].[dbo].PanelStage PanelStage ON PanelStage.SessionPanelId = SessionPanel.SessionPanelId
		 WHERE PanelStage.ReviewStageId = 1 AND PanelStage.DeletedFlag = 0;
	--DELETE
	END
	ELSE
	BEGIN
		UPDATE [$(DatabaseName)].[dbo].[PanelStageStep] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].dbo.ViewMeetingSession MeetingSession ON deleted.Session_ID = MeetingSession.LegacySessionId INNER JOIN
			[$(DatabaseName)].dbo.ViewSessionPanel SessionPanel ON MeetingSession.MeetingSessionId = SessionPanel.MeetingSessionId INNER JOIN
			[$(DatabaseName)].dbo.ViewPanelStage PanelStage ON SessionPanel.SessionPanelId = PanelStage.SessionPanelId INNER JOIN
			[$(DatabaseName)].dbo.PanelStageStep PanelStageStep ON PanelStage.PanelStageId = PanelStageStep.PanelStageId AND 
			PanelStageStep.StepTypeId = CASE deleted.Phase_Id WHEN 1 THEN 5 WHEN 2 THEN 6 WHEN 3 THEN 7 END
		WHERE PanelStage.ReviewStageId = 1 AND PanelStageStep.DeletedFlag = 0;
		WITH PanelStepsCte
		AS (
			SELECT SUM(CASE WHEN MTG_Phase_Member.Phase_ID =1 THEN 1 ELSE 0 END) AS PrelimCount, SUM(CASE WHEN MTG_Phase_Member.Phase_ID =2 THEN 1 ELSE 0 END) AS RevisedCount,
				SUM(CASE WHEN MTG_Phase_Member.Phase_ID =3 THEN 1 ELSE 0 END) AS FinalCount, inserted.Session_ID AS SessionId
			FROM inserted INNER JOIN
				[$(P2RMIS)].dbo.MTG_Phase_Member MTG_Phase_Member ON inserted.Session_ID = MTG_Phase_Member.Session_ID
			GROUP BY inserted.Session_ID
		)
		UPDATE [$(DatabaseName)].[dbo].[PanelStage] 
		SET WorkflowId = (Select WorkflowId FROM [$(DatabaseName)].[dbo].Workflow Workflow WHERE Workflow.ClientId = ClientMeeting.ClientId AND Workflow.ReviewStageId = 1 AND 
		Workflow.WorkflowName = CASE WHEN PanelStepsCte.FinalCount = 1 AND PanelStepsCte.RevisedCount = 1 THEN 'Online Dsicussion' WHEN PanelStepsCte.FinalCount = 1 THEN 'Online Discussion (Legacy)'
		WHEN PanelStepsCte.RevisedCount = 1 THEN 'Pre-Meeting' ELSE 'Pre-Meeting (Legacy)' END)
		FROM deleted INNER JOIN
		PanelStepsCte ON deleted.Session_ID = PanelStepsCte.SessionId INNER JOIN
		[$(DatabaseName)].[dbo].[ViewMeetingSession] MeetingSession ON deleted.Session_Id = MeetingSession.LegacySessionId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientMeeting] ClientMeeting ON MeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId INNER JOIN
		[$(DatabaseName)].[dbo].[ViewSessionPanel] SessionPanel ON MeetingSession.MeetingSessionId = SessionPanel.MeetingSessionId INNER JOIN
		 [$(DatabaseName)].[dbo].PanelStage PanelStage ON PanelStage.SessionPanelId = SessionPanel.SessionPanelId
		 WHERE PanelStage.ReviewStageId = 1 AND PanelStage.DeletedFlag = 0;
	END
END
