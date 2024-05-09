CREATE VIEW [dbo].ViewWorkflowStep AS
SELECT [WorkflowStepId]
      ,[WorkflowId]
      ,[StepTypeId]
      ,[StepName]
      ,[StepOrder]
      ,[ActiveDefault]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[WorkflowStep]
WHERE [DeletedFlag] = 0

