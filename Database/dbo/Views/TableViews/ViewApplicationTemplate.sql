CREATE VIEW [dbo].ViewApplicationTemplate AS
SELECT [ApplicationTemplateId]
      ,[ApplicationId]
      ,[ApplicationStageId]
      ,[MechanismTemplateId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationTemplate]
WHERE [DeletedFlag] = 0

