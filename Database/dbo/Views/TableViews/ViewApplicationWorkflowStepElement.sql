CREATE VIEW [dbo].ViewApplicationWorkflowStepElement AS
SELECT [ApplicationWorkflowStepElementId]
      ,[ApplicationWorkflowStepId]
      ,[ApplicationTemplateElementId]
      ,[AccessLevelId]
      ,[ClientScoringId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationWorkflowStepElement]
WHERE [DeletedFlag] = 0

