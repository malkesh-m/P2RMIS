CREATE VIEW [dbo].ViewApplicationText AS
SELECT [ApplicationTextId]
      ,[ApplicationId]
      ,[ClientApplicationTextTypeId]
      ,[BodyText]
      ,[AbstractFlag]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationText]
WHERE [DeletedFlag] = 0

