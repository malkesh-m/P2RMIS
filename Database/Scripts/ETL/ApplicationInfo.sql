--Will probably need more info imported eventually, starting with CDMRP Grant_ID for now
INSERT INTO [ApplicationInfo]
           ([ApplicationId]
           ,[ClientApplicationInfoTypeId]
           ,[InfoText]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT app.ApplicationId, 1, prop.Grant_ID, vun.UserId, prop.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PRO_Proposal prop INNER JOIN
[Application] app ON prop.LOG_NO = app.LogNumber INNER JOIN
[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId LEFT OUTER JOIN
[ViewLegacyUserNameToUserId] vun ON prop.LAST_UPDATED_BY = vun.UserName
WHERE Grant_ID IS NOT NULL AND cat.ClientId IN (19, 23) AND NOT EXISTS (Select 'X' FROM ViewApplicationInfo WHERE ApplicationId = app.ApplicationId)