MERGE INTO [ReviewStatusType] AS Target
USING (VALUES
  (1,'Review')
 ,(2,'Summary')
) AS Source ([ReviewStatusTypeId],[StatusTypeName])
ON (Target.[ReviewStatusTypeId] = Source.[ReviewStatusTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[StatusTypeName], Target.[StatusTypeName]) IS NOT NULL OR NULLIF(Target.[StatusTypeName], Source.[StatusTypeName]) IS NOT NULL) THEN
 UPDATE SET
  [StatusTypeName] = Source.[StatusTypeName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ReviewStatusTypeId],[StatusTypeName])
 VALUES(Source.[ReviewStatusTypeId],Source.[StatusTypeName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ReviewStatusType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ReviewStatusType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO