INSERT INTO [dbo].[ApplicationStageCalculatedScore]
           ([ApplicationStageId]
           ,[MechanismTemplateElementId]
           ,[AverageScore]
           ,[StandardDeviation]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ViewApplicationStage.ApplicationStageId, ScorePosition.MechanismTemplateElementId, CASE WHEN ScorePosition.OverallFlag = 1 THEN PP.Global_Score
				WHEN ScorePosition.Position = 1 THEN PP.Eval1
				WHEN ScorePosition.Position = 2 THEN PP.Eval2
				WHEN ScorePosition.Position = 3 THEN PP.Eval3
				WHEN ScorePosition.Position = 4 THEN PP.Eval4
				WHEN ScorePosition.Position = 5 THEN PP.Eval5
				WHEN ScorePosition.Position = 6 THEN PP.Eval6
				WHEN ScorePosition.Position = 7 THEN PP.Eval7
				WHEN ScorePosition.Position = 8 THEN PP.Eval8
				WHEN ScorePosition.Position = 9 THEN PP.Eval9
				WHEN ScorePosition.Position = 10 THEN PP.Eval10 END, 
				CASE WHEN ScorePosition.OverallFlag = 1 THEN PP.Std_Dev END,
				VUN.UserId, PP.LAST_UPDATE_DATE
FROM ViewPanelApplication 
INNER JOIN ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId
INNER JOIN ViewSessionPanel ON ViewPanelApplication.SessionPanelId = ViewPanelApplication.SessionPanelId
INNER JOIN ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId AND ViewApplicationStage.ReviewStageId = 2
INNER JOIN ViewMechanismTemplate ON ViewApplication.ProgramMechanismId = ViewMechanismTemplate.ProgramMechanismId AND ViewMechanismTemplate.ReviewStageId = 2
INNER JOIN (SELECT DENSE_RANK() OVER (PARTITION BY MechanismTemplateElement.MechanismTemplateId ORDER BY MechanismTemplateElement.OverallFlag, MechanismTemplateElement.SortOrder) AS Position, MechanismTemplateElement.MechanismTemplateElementId, MechanismTemplateElement.LegacyEcmId, MechanismTemplateElement.OverallFlag, MechanismTemplateElement.MechanismTemplateId
		FROM ViewMechanismTemplateElement MechanismTemplateElement
		WHERE MechanismTemplateElement.ScoreFlag = 1) ScorePosition ON ViewMechanismTemplate.MechanismTemplateId = ScorePosition.MechanismTemplateId
INNER JOIN [$(P2RMIS)].dbo.PRG_Panel_Proposals PP ON ViewApplication.LogNumber = PP.LOG_NO AND ViewSessionPanel.LegacyPanelId = PP.Panel_ID
LEFT OUTER JOIN ViewLegacyUserNameToUserId VUN ON PP.LAST_UPDATED_BY = VUN.UserName
WHERE PP.Global_Score IS NOT NULL OR PP.Eval1 IS NOT NULL;

