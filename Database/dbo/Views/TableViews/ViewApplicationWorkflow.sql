CREATE VIEW [dbo].ViewApplicationWorkflow AS
SELECT [ApplicationWorkflowId]
      ,[WorkflowId]
      ,[ApplicationStageId]
      ,[ApplicationTemplateId]
      ,[PanelUserAssignmentId]
      ,[ApplicationWorkflowName]
      ,[DateAssigned]
      ,[DateClosed]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationWorkflow]
WHERE [DeletedFlag] = 0

