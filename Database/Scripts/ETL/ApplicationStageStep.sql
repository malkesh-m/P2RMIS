INSERT INTO [dbo].[ApplicationStageStep]
           ([ApplicationStageId]
           ,[PanelStageStepId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ApplicationStage.ApplicationStageId, PanelStageStep.PanelStageStepId, ApplicationStage.ModifiedBy, ApplicationStage.ModifiedDate
FROM ViewApplicationStage ApplicationStage INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
	ViewSessionPanel SessionPanel ON PanelApplication.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
	ViewPanelStage PanelStage ON SessionPanel.SessionPanelId = PanelStage.SessionPanelId AND ApplicationStage.ReviewStageId = PanelStage.ReviewStageId INNER JOIN
	ViewPanelStageStep PanelStageStep ON PanelStage.PanelStageId = PanelStageStep.PanelStageId
WHERE NOT EXISTS (Select 'X' FROM ApplicationStageStep WHERE DeletedFlag = 0 AND ApplicationStageId = ApplicationStage.ApplicationStageId AND PanelStageStepId = PanelStageStep.PanelStageStepId)
