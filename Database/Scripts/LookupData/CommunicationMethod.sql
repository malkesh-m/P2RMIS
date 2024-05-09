MERGE INTO [CommunicationMethod] AS Target
USING (VALUES
  (1,'Phone')
 ,(2,'Text-Cell')
 ,(3,'Fax')
 ,(4,'Email')
) AS Source ([CommunicationMethodId],[MethodName])
ON (Target.[CommunicationMethodId] = Source.[CommunicationMethodId])
WHEN MATCHED AND (
	NULLIF(Source.[MethodName], Target.[MethodName]) IS NOT NULL OR NULLIF(Target.[MethodName], Source.[MethodName]) IS NOT NULL) THEN
 UPDATE SET
  [MethodName] = Source.[MethodName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([CommunicationMethodId],[MethodName])
 VALUES(Source.[CommunicationMethodId],Source.[MethodName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [CommunicationMethod]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[CommunicationMethod] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO