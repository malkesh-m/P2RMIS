--Update existing records for new structure
UPDATE u SET PersonID = Person_ID
FROM [User] u 
	LEFT OUTER JOIN [$(P2RMIS)].dbo.PPL_People p ON u.PersonID = p.Person_ID
--Insert records from existing database
INSERT INTO [dbo].[User]
           ([UserLogin]
           ,[account_status_id]
           ,[account_status_date]
           ,[IsActivated]
           ,[PersonID]
           ,[CreatedDate]
           ,[ModifiedDate])
SELECT su.UserID, 6, dbo.GetP2rmisDateTime(), 0, p.Person_ID, ISNULL(p.LAST_UPDATE_DATE, dbo.GetP2rmisDateTime()), ISNULL(p.LAST_UPDATE_DATE, dbo.GetP2rmisDateTime())
FROM [$(P2RMIS)].dbo.PPL_People p INNER JOIN
[$(P2RMIS)].dbo.SYS_Users su ON p.Person_ID = su.Person_ID LEFT OUTER JOIN 
[User] u ON p.Person_ID = u.PersonID
WHERE u.PersonID IS NULL;
--Update the records we just inserted with CreatedBy and ModifiedBy information
UPDATE u SET CreatedBy = u2.UserId, ModifiedBy = u2.UserId
FROM [User] u
	INNER JOIN [$(P2RMIS)].dbo.PPL_People p ON u.PersonID = p.Person_ID
	INNER JOIN [$(P2RMIS)].dbo.SYS_Users su ON p.LAST_UPDATED_BY = su.UserID
	INNER JOIN [User] u2 ON su.Person_ID = u2.PersonID