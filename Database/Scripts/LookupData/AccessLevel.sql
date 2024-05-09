MERGE INTO [AccessLevel] AS Target
USING (VALUES
  (1,'Read/Write')
 ,(2,'Read')
 ,(3,'None')
) AS Source ([AccessLevelId],[AccessLevel])
ON (Target.[AccessLevelId] = Source.[AccessLevelId])
WHEN MATCHED AND (
	NULLIF(Source.[AccessLevel], Target.[AccessLevel]) IS NOT NULL OR NULLIF(Target.[AccessLevel], Source.[AccessLevel]) IS NOT NULL) THEN
 UPDATE SET
  [AccessLevel] = Source.[AccessLevel]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([AccessLevelId],[AccessLevel])
 VALUES(Source.[AccessLevelId],Source.[AccessLevel])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [AccessLevel]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[AccessLevel] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO