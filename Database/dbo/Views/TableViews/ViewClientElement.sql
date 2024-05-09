CREATE VIEW [dbo].ViewClientElement AS
SELECT [ClientElementId]
      ,[ClientId]
      ,[ElementTypeId]
      ,[ElementIdentifier]
      ,[ElementAbbreviation]
      ,[ElementDescription]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ClientElement]
WHERE [DeletedFlag] = 0

