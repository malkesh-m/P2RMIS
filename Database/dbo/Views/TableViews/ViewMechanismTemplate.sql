CREATE VIEW [dbo].ViewMechanismTemplate AS
SELECT [MechanismTemplateId]
      ,[ProgramMechanismId]
      ,[ReviewStatusId]
      ,[ReviewStageId]
      ,[SummaryDocumentId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[MechanismTemplate]
WHERE [DeletedFlag] = 0

