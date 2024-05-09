CREATE VIEW [dbo].ViewCommunicationLog AS
SELECT [CommunicationLogId]
      ,[SessionPanelId]
      ,[Subject]
      ,[Message]
      ,[BCC]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[CommunicationLog]
WHERE [DeletedFlag] = 0

