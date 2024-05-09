WITH cte
AS (Select PanelApplicationReviewerExpertiseId, ROW_NUMBER() OVER (Partition By  PanelApplicationId, PanelUserAssignmentId, DeletedDate Order By DeletedFlag asc, PanelApplicationReviewerExpertiseId asc) AS dupCount
From PanelApplicationReviewerExpertise)
UPDATE PanelApplicationReviewerExpertise SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = DATEADD(mi, cte.dupCount, dbo.GetP2rmisDateTime())
FROM PanelApplicationReviewerExpertise INNER JOIN
cte ON PanelApplicationReviewerExpertise.PanelApplicationReviewerExpertiseId = cte.PanelApplicationReviewerExpertiseId
WHERE cte.dupCount > 1