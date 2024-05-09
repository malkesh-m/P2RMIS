CREATE VIEW [dbo].ViewMechanismTemplateElementScoring AS
SELECT [MechanismTemplateScoringId]
      ,[MechanismTemplateElementId]
      ,[ClientScoringId]
      ,[StepTypeId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[MechanismTemplateElementScoring]
WHERE [DeletedFlag] = 0

