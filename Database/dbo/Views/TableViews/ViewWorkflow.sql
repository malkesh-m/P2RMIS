CREATE VIEW [dbo].ViewWorkflow AS
SELECT [WorkflowId]
      ,[ClientId]
      ,[ReviewStageId]
      ,[WorkflowName]
      ,[WorkflowDescription]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[Workflow]
WHERE [DeletedFlag] = 0

