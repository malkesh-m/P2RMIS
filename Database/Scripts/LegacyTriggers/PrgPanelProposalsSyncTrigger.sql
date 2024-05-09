CREATE TRIGGER [PrgPanelProposalsSyncTrigger]
ON [$(P2RMIS)].dbo.PRG_Panel_Proposals
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[PanelApplication]
	SET ReviewOrder = inserted.Order_Of_Review, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
		[$(DatabaseName)].dbo.ViewSessionPanel SessionPanel ON inserted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
		[$(DatabaseName)].dbo.ViewApplication Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].dbo.PanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId AND 
		Application.ApplicationId = PanelApplication.ApplicationId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE PanelApplication.DeletedFlag = 0
	--Was triaged updated?
	IF EXISTS (Select * FROM inserted INNER JOIN deleted ON inserted.LOG_NO = deleted.Log_NO AND inserted.Panel_ID = deleted.Panel_ID WHERE inserted.Triaged <> deleted.Triaged)
		UPDATE [$(DatabaseName)].[dbo].[ApplicationReviewStatus]
		SET ReviewStatusId = CASE inserted.Triaged WHEN 0 THEN 2 WHEN 1 THEN 1 END, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(DatabaseName)].dbo.ViewSessionPanel SessionPanel ON inserted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
		[$(DatabaseName)].dbo.ViewApplication Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId AND 
		Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationReviewStatus ApplicationReviewStatus ON PanelApplication.PanelApplicationId = ApplicationReviewStatus.PanelApplicationId	INNER JOIN
		[$(DatabaseName)].dbo.ReviewStatus ReviewStatus ON ApplicationReviewStatus.ReviewStatusId = ReviewStatus.ReviewStatusId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
		WHERE ReviewStatus.ReviewStatusTypeId = 1 AND ApplicationReviewStatus.DeletedFlag = 0
	END
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[PanelApplication]
           ([SessionPanelId]
           ,[ApplicationId]
           ,[ReviewOrder]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT SessionPanel.SessionPanelId, Application.ApplicationId, inserted.Order_Of_Review, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM	inserted INNER JOIN
		[$(DatabaseName)].dbo.ViewSessionPanel SessionPanel ON inserted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
		[$(DatabaseName)].dbo.ViewApplication Application ON inserted.Log_No = Application.LogNumber LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE [Application].DeletedFlag = 0
	INSERT INTO [$(DatabaseName)].[dbo].[ApplicationStage]
           ([PanelApplicationId]
           ,[ReviewStageId]
           ,[StageOrder]
           ,[ActiveFlag]
           ,[AssignmentVisibilityFlag]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT PanelApplication.PanelApplicationId, 1, 1, 1, CASE WHEN PAN_Assignment_Approval.Released_Date IS NOT NULL THEN 1 ELSE 0 END, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM  inserted INNER JOIN
		[$(DatabaseName)].dbo.ViewSessionPanel SessionPanel ON inserted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
		[$(DatabaseName)].dbo.ViewApplication Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId AND
			Application.ApplicationId = PanelApplication.ApplicationId LEFT OUTER JOIN
		[$(P2RMIS)].dbo.PAN_Assignment_Approval PAN_Assignment_Approval ON SessionPanel.LegacyPanelId = PAN_Assignment_Approval.Panel_ID LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE [Application].DeletedFlag = 0
	UNION ALL
	SELECT PanelApplication.PanelApplicationId, 2, 2, 0, CASE WHEN PAN_Assignment_Approval.Released_Date IS NOT NULL THEN 1 ELSE 0 END, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM  inserted INNER JOIN
		[$(DatabaseName)].dbo.ViewSessionPanel SessionPanel ON inserted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
		[$(DatabaseName)].dbo.ViewApplication Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId AND
			Application.ApplicationId = PanelApplication.ApplicationId LEFT OUTER JOIN
		[$(P2RMIS)].dbo.PAN_Assignment_Approval PAN_Assignment_Approval ON SessionPanel.LegacyPanelId = PAN_Assignment_Approval.Panel_ID LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE [Application].DeletedFlag = 0
	INSERT INTO [$(DatabaseName)].[dbo].[ApplicationReviewStatus]
	([PanelApplicationId]
           ,[ReviewStatusId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT PanelApplication.PanelApplicationId, CASE inserted.Triaged WHEN 0 THEN 2 ELSE 1 END, VUN.UserId, inserted.LAST_UPDATE_DATE,
	VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
		[$(DatabaseName)].dbo.ViewSessionPanel SessionPanel ON inserted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
		[$(DatabaseName)].dbo.ViewApplication Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId AND
			Application.ApplicationId = PanelApplication.ApplicationId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE [Application].DeletedFlag = 0
	INSERT INTO [$(DatabaseName)].[dbo].[ApplicationStageStep]
	([ApplicationStageId]
           ,[PanelStageStepId]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT ApplicationStage.ApplicationStageId, PanelStageStep.PanelStageStepId, ApplicationStage.ModifiedBy, ApplicationStage.ModifiedDate
	FROM inserted INNER JOIN
		[$(DatabaseName)].dbo.ViewSessionPanel SessionPanel ON inserted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
		[$(DatabaseName)].dbo.ViewApplication Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId AND
			Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
		[$(DatabaseName)].dbo.ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelStage PanelStage ON SessionPanel.SessionPanelId = PanelStage.SessionPanelId AND ApplicationStage.ReviewStageId = PanelStage.ReviewStageId INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelStageStep PanelStageStep ON PanelStage.PanelStageId = PanelStageStep.PanelStageId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE [ApplicationStage].DeletedFlag = 0
	END
	--DELETE
	ELSE
	BEGIN
		UPDATE [$(DatabaseName)].[dbo].[ApplicationStageStep] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.ViewSessionPanel SessionPanel ON deleted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
			[$(DatabaseName)].dbo.ViewApplication Application ON deleted.Log_No = Application.LogNumber INNER JOIN
			[$(DatabaseName)].dbo.ViewPanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId AND
			Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
			[$(DatabaseName)].dbo.ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
			[$(DatabaseName)].dbo.ApplicationStageStep ApplicationStageStep ON ApplicationStage.ApplicationStageId = ApplicationStageStep.ApplicationStageId
		WHERE ApplicationStageStep.DeletedFlag = 0
		UPDATE [$(DatabaseName)].[dbo].[ApplicationReviewStatus] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].dbo.ViewSessionPanel SessionPanel ON deleted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
			[$(DatabaseName)].dbo.ViewApplication Application ON deleted.Log_No = Application.LogNumber INNER JOIN
			[$(DatabaseName)].dbo.ViewPanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId AND
			Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
			[$(DatabaseName)].dbo.ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
			[$(DatabaseName)].dbo.ApplicationReviewStatus ApplicationReviewStatus ON PanelApplication.PanelApplicationId = ApplicationReviewStatus.PanelApplicationId
		WHERE ApplicationReviewStatus.DeletedFlag = 0
		UPDATE [$(DatabaseName)].[dbo].[ApplicationStage] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].dbo.ViewSessionPanel SessionPanel ON deleted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
			[$(DatabaseName)].dbo.ViewApplication Application ON deleted.Log_No = Application.LogNumber INNER JOIN
			[$(DatabaseName)].dbo.ViewPanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId AND
			Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
			[$(DatabaseName)].dbo.ApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId
		WHERE ApplicationStage.DeletedFlag = 0
		UPDATE [$(DatabaseName)].[dbo].[PanelApplication] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].dbo.ViewSessionPanel SessionPanel ON deleted.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
			[$(DatabaseName)].dbo.ViewApplication Application ON deleted.Log_No = Application.LogNumber INNER JOIN
			[$(DatabaseName)].dbo.PanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId AND
			Application.ApplicationId = PanelApplication.ApplicationId
		WHERE PanelApplication.DeletedFlag = 0

		
		
		
	END
END
