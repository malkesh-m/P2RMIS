CREATE VIEW [dbo].ViewApplicationWorkflowStep AS
SELECT [ApplicationWorkflowStepId]
      ,[ApplicationWorkflowId]
      ,[StepTypeId]
      ,[StepName]
      ,[Active]
      ,[StepOrder]
      ,[Resolution]
      ,[ResolutionDate]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationWorkflowStep]
WHERE [DeletedFlag] = 0

