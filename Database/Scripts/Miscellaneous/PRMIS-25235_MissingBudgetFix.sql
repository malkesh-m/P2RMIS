/* This query inserts an empty budget for an application 
	This was run for the following SessionPanelIds
	4650, 4652, 4651, 4653, 4654, 4665, 4656, 4658, 4655, 4659, 4661, 4663, 4685, 4657, 4684, 4660, 4664, 4662, 4847, 4666
*/

INSERT INTO [dbo].[ApplicationBudget]
           ([ApplicationId]
           ,[DirectCosts]
           ,[IndirectCosts]
           ,[TotalFunding]
           ,[Comments]
           ,[CommentModifiedBy]
           ,[CommentModifiedDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ViewApplication.ApplicationId,
NULL,
NULL,
NULL,
NULL,
NULL,
NULL,
10,
dbo.GetP2rmisDateTime(),
10,
dbo.GetP2rmisDateTime()
FROM ViewPanelApplication INNER JOIN
ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId
WHERE ViewPanelApplication.SessionPanelId = @SessionId AND NOT EXISTS (SELECT 'X' FROM ViewApplicationBudget WHERE ApplicationId = ViewApplication.ApplicationId);