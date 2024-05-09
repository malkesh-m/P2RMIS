UPDATE [ApplicationStage]
SET AssignmentVisibilityFlag = 1
FROM [$(P2RMIS)].dbo.PAN_Assignment_Approval paa INNER JOIN
SessionPanel ON paa.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN 
PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId INNER JOIN
ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId
WHERE paa.Released_Date IS NOT NULL