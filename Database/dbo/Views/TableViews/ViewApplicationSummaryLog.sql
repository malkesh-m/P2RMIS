CREATE VIEW [dbo].ViewApplicationSummaryLog AS
SELECT [ApplicationSummaryLogId]
      ,[ApplicationWorkflowStepId]
      ,[UserId]
      ,[CompletedFlag]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationSummaryLog]
WHERE [DeletedFlag] = 0

