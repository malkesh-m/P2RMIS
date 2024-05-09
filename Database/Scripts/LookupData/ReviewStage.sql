MERGE INTO [ReviewStage] AS Target
USING (VALUES
  (1,'Asynchronous')
 ,(2,'Synchronous')
 ,(3,'Summary')
) AS Source ([ReviewStageId],[ReviewStageName])
ON (Target.[ReviewStageId] = Source.[ReviewStageId])
WHEN MATCHED AND (
	NULLIF(Source.[ReviewStageName], Target.[ReviewStageName]) IS NOT NULL OR NULLIF(Target.[ReviewStageName], Source.[ReviewStageName]) IS NOT NULL) THEN
 UPDATE SET
  [ReviewStageName] = Source.[ReviewStageName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ReviewStageId],[ReviewStageName])
 VALUES(Source.[ReviewStageId],Source.[ReviewStageName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ReviewStage]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ReviewStage] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO