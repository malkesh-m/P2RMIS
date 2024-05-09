INSERT INTO [ApplicationBudget]
           ([ApplicationId]
           ,[DirectCosts]
           ,[IndirectCosts]
           ,[TotalFunding]
           ,[Comments]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT app.ApplicationId, bud.Requested_Direct, bud.Requested_Indirect, bud.Req_Total_Funding, CASE WHEN bud.Budget_Comments = 'No changes recommended' THEN NULL ELSE bud.Budget_Comments END, vun.UserID, bud.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PRO_Budget bud INNER JOIN
	[ViewApplication] app ON bud.LOG_NO = app.LogNumber LEFT OUTER JOIN
	ViewLegacyUserNameToUserId vun ON bud.LAST_UPDATED_BY = vun.UserName
WHERE bud.LOG_NO IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewApplicationBudget WHERE ApplicationId = app.ApplicationId)