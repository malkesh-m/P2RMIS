INSERT INTO .[dbo].[MechanismTemplate]
           ([ProgramMechanismId]
           ,[MechanismId]
           ,[ReviewStageId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ProgramMechanism.ProgramMechanismId, ProgramMechanism.LegacyAtmId, PanelStage.ReviewStageId, ProgramMechanism.ModifiedBy, ProgramMechanism.ModifiedDate
FROM   ViewPanelStage	PanelStage INNER JOIN
	ViewSessionPanel SessionPanel ON PanelStage.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
	ViewPanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId INNER JOIN
	ViewApplication [Application] ON PanelApplication.ApplicationId = [Application].ApplicationId INNER JOIN
	ViewProgramMechanism ProgramMechanism ON [Application].ProgramMechanismId = ProgramMechanism.ProgramMechanismId
WHERE NOT EXISTS (Select 'X' FROM ViewMechanismTemplate WHERE ProgramMechanismId = ProgramMechanism.ProgramMechanismId AND ReviewStageId = PanelStage.ReviewStageId)
GROUP BY ProgramMechanism.ProgramMechanismId, ProgramMechanism.LegacyAtmId, PanelStage.ReviewStageId, ProgramMechanism.ModifiedBy, ProgramMechanism.ModifiedDate
HAVING PanelStage.ReviewStageId IN (1,2)
ORDER BY ProgramMechanismId, ReviewStageId