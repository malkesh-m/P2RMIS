CREATE VIEW [dbo].ViewUserClient AS
SELECT [UserClientID]
      ,[UserID]
      ,[ClientID]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserClient]
WHERE [DeletedFlag] = 0

