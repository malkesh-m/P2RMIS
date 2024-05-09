CREATE TRIGGER [PanMasterSyncTrigger]
ON [$(P2RMIS)].dbo.PAN_Master
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
		UPDATE [$(DatabaseName)].[dbo].[SessionPanel]
		SET MeetingSessionId = MeetingSession.MeetingSessionId, PanelAbbreviation = inserted.Panel_Abrv, PanelName = inserted.Panel_Name,
		StartDate = MeetingSession.StartDate, EndDate = MeetingSession.EndDate, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[SessionPanel] SessionPanel ON inserted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
		[$(DatabaseName)].dbo.ViewMeetingSession MeetingSession ON inserted.Session_ID = MeetingSession.LegacySessionId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
		WHERE SessionPanel.DeletedFlag = 0
	END
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	BEGIN
		INSERT INTO [$(DatabaseName)].[dbo].[SessionPanel]
			   ([LegacyPanelId]
			   ,[MeetingSessionId]
			   ,[PanelAbbreviation]
			   ,[PanelName]
			   ,[StartDate]
			   ,[EndDate]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
		SELECT inserted.Panel_ID, MeetingSession.MeetingSessionId, inserted.Panel_Abrv, inserted.Panel_Name, MeetingSession.StartDate, 
		MeetingSession.EndDate, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(DatabaseName)].dbo.ViewMeetingSession MeetingSession ON inserted.Session_ID = MeetingSession.LegacySessionId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
		INSERT INTO [$(DatabaseName)].[dbo].[ProgramPanel]
			   ([ProgramYearId]
			   ,[SessionPanelId]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
		SELECT ProgramYear.ProgramYearId, SessionPanel.SessionPanelId, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[ViewSessionPanel] SessionPanel ON inserted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
		[$(DatabaseName)].dbo.ViewMeetingSession MeetingSession ON SessionPanel.MeetingSessionId = MeetingSession.MeetingSessionId INNER JOIN
		[$(DatabaseName)].dbo.ClientProgram ClientProgram ON inserted.Program = ClientProgram.LegacyProgramId INNER JOIN
		[$(DatabaseName)].dbo.ViewProgramYear ProgramYear ON inserted.FY = ProgramYear.Year AND ClientProgram.ClientProgramId = ProgramYear.ClientProgramId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName; 

		--Default to inserting a record for each stage and Meeting PanelStageStep
		WITH PanelStepsCte
		AS (
			SELECT SUM(CASE WHEN MTG_Phase_Member.Phase_ID =1 THEN 1 ELSE 0 END) AS PrelimCount, SUM(CASE WHEN MTG_Phase_Member.Phase_ID =2 THEN 1 ELSE 0 END) AS RevisedCount,
				SUM(CASE WHEN MTG_Phase_Member.Phase_ID =3 THEN 1 ELSE 0 END) AS FinalCount, inserted.Session_ID AS SessionId
			FROM inserted INNER JOIN
				[$(P2RMIS)].dbo.MTG_Phase_Member MTG_Phase_Member ON inserted.Session_ID = MTG_Phase_Member.Session_ID
			GROUP BY inserted.Session_ID
		)
		INSERT INTO [$(DatabaseName)].[dbo].[PanelStage]
			   ([SessionPanelId]
			   ,[ReviewStageId]
			   ,[StageOrder]
			   ,[WorkflowId]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
		SELECT SessionPanel.SessionPanelId, 1, 1, (Select WorkflowId FROM [$(DatabaseName)].[dbo].Workflow Workflow WHERE Workflow.ClientId = ClientMeeting.ClientId AND Workflow.ReviewStageId = 1 AND 
		Workflow.WorkflowName = CASE WHEN PanelStepsCte.FinalCount = 1 AND PanelStepsCte.RevisedCount = 1 THEN 'Online Discussion' WHEN PanelStepsCte.FinalCount = 1 THEN 'Online Discussion (Legacy)'
		WHEN PanelStepsCte.RevisedCount = 1 THEN 'Pre-Meeting' ELSE 'Pre-Meeting (Legacy)' END),
		SessionPanel.CreatedBy, SessionPanel.CreatedDate, SessionPanel.ModifiedBy, SessionPanel.ModifiedDate
		FROM inserted INNER JOIN
		PanelStepsCte ON inserted.Session_ID = PanelStepsCte.SessionId INNER JOIN
		[$(DatabaseName)].[dbo].[ViewSessionPanel] SessionPanel ON inserted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
		[$(DatabaseName)].[dbo].[ViewMeetingSession] MeetingSession ON SessionPanel.MeetingSessionId = MeetingSession.MeetingSessionId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientMeeting] ClientMeeting ON MeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId 
		UNION ALL
		SELECT SessionPanel.SessionPanelId, 2, 2, (Select WorkflowId FROM [$(DatabaseName)].[dbo].Workflow Workflow WHERE Workflow.ClientId = ClientMeeting.ClientId AND Workflow.ReviewStageId = 2), SessionPanel.CreatedBy, SessionPanel.CreatedDate, SessionPanel.ModifiedBy, SessionPanel.ModifiedDate
		FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[ViewSessionPanel] SessionPanel ON inserted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
		[$(DatabaseName)].[dbo].[ViewMeetingSession] MeetingSession ON SessionPanel.MeetingSessionId = MeetingSession.MeetingSessionId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientMeeting] ClientMeeting ON MeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId;
		
		INSERT INTO [$(DatabaseName)].[dbo].[PanelStageStep]
			   ([PanelStageId]
			   ,[StepTypeId]
			   ,[StepName]
			   ,[StepOrder]
			   ,[StartDate]
			   ,[EndDate]
			   ,[ReopenDate]
			   ,[ReCloseDate]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
		SELECT PanelStage.PanelStageId, WorkflowStep.StepTypeId, WorkflowStep.StepName, WorkflowStep.StepOrder, MPM.Phase_Start, MPM.Phase_End, MPM.Phase_ReOpen, MPM.Phase_ReClose,
		VUN.UserId, dbo.GetP2rmisDateTime(), VUN.UserId, dbo.GetP2rmisDateTime()
		FROM inserted INNER JOIN
		[$(P2RMIS)].dbo.MTG_Phase_Member MPM ON inserted.Session_ID = MPM.Session_ID INNER JOIN
		[$(P2RMIS)].dbo.PRG_Phase PRG_Phase ON MPM.Phase_ID = PRG_Phase.Phase_ID INNER JOIN
		[$(DatabaseName)].[dbo].[ViewSessionPanel] SessionPanel ON inserted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
		[$(DatabaseName)].[dbo].[ViewPanelStage] PanelStage ON SessionPanel.SessionPanelId = PanelStage.SessionPanelId INNER JOIN
		[$(DatabaseName)].[dbo].[Workflow] Workflow ON PanelStage.WorkflowId = Workflow.WorkflowId INNER JOIN
		[$(DatabaseName)].[dbo].[WorkflowStep] WorkflowStep ON Workflow.WorkflowId = WorkflowStep.WorkflowId AND WorkflowStep.StepTypeId = CASE PRG_Phase.Phase_Id WHEN 1 THEN 5 WHEN 2 THEN 6 WHEN 3 THEN 7 END LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON MPM.LAST_UPDATED_BY = VUN.UserName 
		WHERE PanelStage.ReviewStageId = 1


		INSERT INTO [$(DatabaseName)].[dbo].[PanelStageStep]
			   ([PanelStageId]
			   ,[StepTypeId]
			   ,[StepName]
			   ,[StepOrder]
			   ,[StartDate]
			   ,[EndDate]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
		SELECT PanelStage.PanelStageId, 8, 'Meeting', 1, SessionPanel.StartDate, SessionPanel.EndDate, SessionPanel.CreatedBy, SessionPanel.CreatedDate, SessionPanel.ModifiedBy, SessionPanel.ModifiedDate
		FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[ViewSessionPanel] SessionPanel ON inserted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
		[$(DatabaseName)].[dbo].[ViewPanelStage] PanelStage ON SessionPanel.SessionPanelId = PanelStage.SessionPanelId
		WHERE PanelStage.ReviewStageId = 2

	END
	--DELETE
	ELSE
	BEGIN
		UPDATE [$(DatabaseName)].[dbo].[SessionPanel] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].dbo.SessionPanel SessionPanel ON deleted.Panel_ID = SessionPanel.LegacyPanelId
		WHERE SessionPanel.DeletedFlag = 0

		UPDATE [$(DatabaseName)].[dbo].[ProgramPanel] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].dbo.SessionPanel SessionPanel ON deleted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
			[$(DatabaseName)].dbo.ProgramPanel ProgramPanel ON SessionPanel.SessionPanelId = ProgramPanel.SessionPanelId
		WHERE ProgramPanel.DeletedFlag = 0

		UPDATE [$(DatabaseName)].[dbo].[PanelStage] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].dbo.SessionPanel SessionPanel ON deleted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
			[$(DatabaseName)].dbo.PanelStage PanelStage ON SessionPanel.SessionPanelId = PanelStage.SessionPanelId
		WHERE PanelStage.DeletedFlag = 0

		UPDATE [$(DatabaseName)].[dbo].[PanelStageStep] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].dbo.SessionPanel SessionPanel ON deleted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
			[$(DatabaseName)].dbo.PanelStage PanelStage ON SessionPanel.SessionPanelId = PanelStage.SessionPanelId INNER JOIN
			[$(DatabaseName)].dbo.PanelStageStep PanelStageStep ON PanelStage.PanelStageId = PanelStageStep.PanelStageId
		WHERE PanelStageStep.DeletedFlag = 0
	END
END
