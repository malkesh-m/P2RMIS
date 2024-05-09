CREATE VIEW [dbo].ViewApplicationWorkflowStepAssignment AS
SELECT [ApplicationWorkflowStepAssignmentId]
      ,[ApplicationWorkflowStepId]
      ,[UserId]
      ,[AssignmentId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationWorkflowStepAssignment]
WHERE [DeletedFlag] = 0

