--Update existing panel application review status
UPDATE ars
SET PanelApplicationId = panapp.PanelApplicationId
FROM ApplicationReviewStatus ars INNER JOIN
		[PanelApplication] panapp ON ars.ApplicationId = panapp.ApplicationId
WHERE ars.PanelApplicationId IS NULL


--Insert only for those that don't already have a review status
INSERT INTO [ApplicationReviewStatus]
           ([ApplicationId]
           ,[PanelApplicationId]
           ,[ReviewStatusId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT app.ApplicationId, panapp.PanelApplicationId, CASE opanapp.Triaged WHEN 1 THEN 1 ELSE 2 END, panapp.ModifiedBy, panapp.ModifiedDate
FROM [$(P2RMIS)].dbo.[PRG_Panel_Proposals] opanapp INNER JOIN
[ViewApplication] app ON opanapp.Log_No = app.LogNumber INNER JOIN
[ViewSessionPanel] sp ON opanapp.Panel_ID = sp.LegacyPanelId INNER JOIN
[ViewPanelApplication] panapp ON app.ApplicationId = panapp.ApplicationId AND sp.SessionPanelId = panapp.SessionPanelId
WHERE app.ApplicationId NOT IN (Select ApplicationId FROM ViewApplicationReviewStatus ars INNER JOIN ReviewStatus rs ON ars.ReviewStatusId = rs.ReviewStatusId WHERE ReviewStatusTypeId = 1)
--Add a record for qualifying range if doesn't exist
UNION ALL
SELECT app.ApplicationId, panapp.PanelApplicationId, 4, panapp.ModifiedBy, panapp.ModifiedDate
FROM [$(P2RMIS)].dbo.[PRG_Panel_Proposals] opanapp INNER JOIN
[ViewApplication] app ON opanapp.Log_No = app.LogNumber INNER JOIN
[ViewSessionPanel] sp ON opanapp.Panel_ID = sp.LegacyPanelId INNER JOIN
[ViewPanelApplication] panapp ON app.ApplicationId = panapp.ApplicationId AND sp.SessionPanelId = panapp.SessionPanelId
WHERE opanapp.Fundable_Range = 1 AND app.ApplicationId NOT IN (Select ApplicationId FROM ViewApplicationReviewStatus ars WHERE ReviewStatusId = 4)
--Add a record for command draft if doesn't exist
UNION ALL
SELECT app.ApplicationId, panapp.PanelApplicationId, 3, panapp.ModifiedBy, panapp.ModifiedDate
FROM [$(P2RMIS)].dbo.[SS_Tracking] sst INNER JOIN
[ViewApplication] app ON sst.Log_No = app.LogNumber INNER JOIN
[ViewPanelApplication] panapp ON app.ApplicationId = panapp.ApplicationId
WHERE sst.Client_Review = 1 AND app.ApplicationId NOT IN (Select ApplicationId FROM ViewApplicationReviewStatus ars WHERE ReviewStatusId = 3)

