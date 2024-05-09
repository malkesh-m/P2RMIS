INSERT INTO [dbo].[ApplicationTemplate]
           ([ApplicationId]
           ,[ApplicationStageId]
           ,[MechanismTemplateId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT [Application].ApplicationId, ApplicationStage.ApplicationStageId, MechanismTemplate.MechanismTemplateId, MechanismTemplate.ModifiedBy, MechanismTemplate.ModifiedDate
FROM [ViewApplication] [Application] INNER JOIN
	ViewPanelApplication PanelApplication ON Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
	ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
	ViewMechanismTemplate MechanismTemplate ON Application.ProgramMechanismId = MechanismTemplate.ProgramMechanismId AND ApplicationStage.ReviewStageId = MechanismTemplate.ReviewStageId
WHERE ApplicationStage.ReviewStageId IN (1,2) AND NOT EXISTS
(Select 'X' FROM ViewApplicationTemplate WHERE ViewApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId AND ViewApplicationTemplate.MechanismTemplateId = MechanismTemplate.MechanismTemplateId) 