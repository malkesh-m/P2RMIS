CREATE VIEW [dbo].ViewCommunicationLogAttachment AS
SELECT [CommunicationLogAttachmentId]
      ,[CommunicationLogId]
      ,[AttachmentFileName]
      ,[AttachmentLocation]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[CommunicationLogAttachment]
WHERE [DeletedFlag] = 0

