CREATE VIEW [dbo].ViewClientAwardType AS
SELECT [ClientAwardTypeId]
      ,[ClientId]
      ,[LegacyAwardTypeId]
      ,[AwardAbbreviation]
      ,[AwardDescription]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ClientAwardType]
WHERE [DeletedFlag] = 0

