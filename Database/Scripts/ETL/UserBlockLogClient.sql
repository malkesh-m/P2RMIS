INSERT INTO [dbo].[UserBlockLogClient]
           ([UserBlockLogId]
           ,[ClientId]
           ,[BlockFlag]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT [UserBlockLogId]
      ,19
	  ,1
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[UserBlockLog]