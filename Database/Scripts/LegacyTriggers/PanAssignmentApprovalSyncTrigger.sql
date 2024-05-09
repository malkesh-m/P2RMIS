CREATE TRIGGER [PanAssignmentApprovalSyncTrigger]
ON [$(P2RMIS)].dbo.PAN_Assignment_Approval
FOR INSERT
AS
BEGIN
UPDATE [$(DatabaseName)].dbo.ApplicationStage
SET AssignmentVisibilityFlag = 1
FROM inserted paa INNER JOIN
[$(DatabaseName)].dbo.SessionPanel SessionPanel ON paa.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN 
[$(DatabaseName)].dbo.PanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId INNER JOIN
[$(DatabaseName)].dbo.ApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId
WHERE paa.Released_Date IS NOT NULL
END