CREATE VIEW [dbo].ViewApplicationWorkflowStepElementContent AS
SELECT [ApplicationWorkflowStepElementContentId]
      ,[ApplicationWorkflowStepElementId]
      ,[Score]
      ,[ContentText]
      ,[ContentTextNoMarkup]
      ,[Abstain]
	  ,[CritiqueRevised]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationWorkflowStepElementContent]
WHERE [DeletedFlag] = 0

