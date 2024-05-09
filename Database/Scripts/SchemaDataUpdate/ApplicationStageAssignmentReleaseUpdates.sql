UPDATE [ApplicationStage] SET AssignmentReleaseDate = CASE WHEN AA.Released_Date IS NOT NULL AND AssignmentReleaseDate IS NULL THEN AA.Released_Date ELSE AssignmentReleaseDate END, 
ModifiedDate = CASE WHEN AA.Released_Date IS NOT NULL AND AssignmentReleaseDate IS NULL THEN AA.Released_Date ELSE ISNULL(AssignmentReleaseDate, ApplicationStage.ModifiedDate) END
FROM [ApplicationStage]
INNER JOIN [ViewPanelApplication] ON ApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
INNER JOIN [ViewSessionPanel] ON ViewPanelApplication.SessionPanelId = ViewSessionPanel.SessionPanelId
INNER JOIN [$(P2RMIS)].dbo.PAN_Assignment_Approval AA ON ViewSessionPanel.LegacyPanelId = AA.Panel_ID
WHERE ApplicationStage.DeletedFlag = 0 AND ApplicationStage.ReviewStageId = 1