CREATE VIEW [dbo].ViewCommunicationLogRecipient AS
SELECT [CommunicationLogRecipientId]
      ,[CommunicationLogId]
      ,[CommunicationLogRecipientTypeId]
      ,[PanelUserAssignmentId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[CommunicationLogRecipient]
WHERE [DeletedFlag] = 0

