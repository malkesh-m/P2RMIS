INSERT INTO [dbo].[ApplicationStage]
           ([PanelApplicationId]
           ,[ReviewStageId]
           ,[StageOrder]
           ,[ActiveFlag]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT PanelApplication.PanelApplicationId, PanelStage.ReviewStageId, PanelStage.StageOrder, 
	CASE WHEN ApplicationReviewStatus.ReviewStatusId = 1 AND PanelStage.ReviewStageId = 2 THEN 0 ELSE 1 END,
	PanelStage.ModifiedBy, PanelStage.ModifiedDate
FROM	ViewPanelApplication PanelApplication INNER JOIN 
	ViewPanelStage PanelStage ON PanelApplication.SessionPanelId = PanelStage.SessionPanelId INNER JOIN
	ViewApplicationReviewStatus ApplicationReviewStatus ON PanelApplication.PanelApplicationId = ApplicationReviewStatus.PanelApplicationId INNER JOIN
	ReviewStatus ON ApplicationReviewStatus.ReviewStatusId = ReviewStatus.ReviewStatusId
WHERE ReviewStatus.ReviewStatusTypeId = 1 AND PanelStage.ReviewStageId IN (1,2)
AND NOT EXISTS (Select 'X' FROM ViewApplicationStage WHERE PanelApplicationId = PanelApplication.PanelApplicationId AND ReviewStageId = PanelStage.ReviewStageId)
