CREATE VIEW [dbo].ViewApplicationWorkflowStepWorkLog AS
SELECT [ApplicationWorkflowStepWorkLogId]
      ,[ApplicationWorkflowStepId]
      ,[UserId]
	  ,[CheckInUserId]
      ,[CheckOutDate]
      ,[CheckInDate]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationWorkflowStepWorkLog]
WHERE [DeletedFlag] = 0

