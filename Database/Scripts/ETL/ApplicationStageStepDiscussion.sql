--Import discussion data only if it exists
INSERT INTO [dbo].[ApplicationStageStepDiscussion]
           ([ApplicationStageStepId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ApplicationStageStep.ApplicationStageStepId, ApplicationStageStep.ModifiedBy, ApplicationStageStep.ModifiedDate
FROM ViewApplicationStageStep ApplicationStageStep INNER JOIN
	ViewApplicationStage ApplicationStage ON ApplicationStageStep.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	ViewPanelStageStep PanelStageStep ON ApplicationStageStep.PanelStageStepId = PanelStageStep.PanelStageStepId INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
	ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId
WHERE PanelStageStep.StepTypeId = 7 AND Application.LogNumber IN (Select DISTINCT LOG_NO FROM [$(P2RMIS)].dbo.PRG_Panel_Proposal_Review_Discussion)
	AND NOT EXISTS (Select 'X' FROM ApplicationStageStepDiscussion WHERE DeletedFlag = 0 AND ApplicationStageStepId = ApplicationStageStep.ApplicationStageStepId)
	
