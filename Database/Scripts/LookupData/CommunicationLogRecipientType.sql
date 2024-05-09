SET NOCOUNT ON

SET IDENTITY_INSERT [CommunicationLogRecipientType] ON

MERGE INTO [CommunicationLogRecipientType] AS Target
USING (VALUES
  (1,'To')
 ,(2,'CC')
) AS Source ([CommunicationLogRecipientTypeId],[RecipientType])
ON (Target.[CommunicationLogRecipientTypeId] = Source.[CommunicationLogRecipientTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[RecipientType], Target.[RecipientType]) IS NOT NULL OR NULLIF(Target.[RecipientType], Source.[RecipientType]) IS NOT NULL) THEN
 UPDATE SET
  [RecipientType] = Source.[RecipientType]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([CommunicationLogRecipientTypeId],[RecipientType])
 VALUES(Source.[CommunicationLogRecipientTypeId],Source.[RecipientType])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [CommunicationLogRecipientType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[CommunicationLogRecipientType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [CommunicationLogRecipientType] OFF
GO
SET NOCOUNT OFF
GO