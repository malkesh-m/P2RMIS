MERGE INTO [ActionLogReason] AS Target
USING (VALUES
  (1,'Application Transfer')
, (2,'Reviewer Transfer')
) AS Source ([ActionLogReasonId],[Reason])
ON (Target.[ActionLogReasonId] = Source.[ActionLogReasonId])
WHEN MATCHED AND (
	NULLIF(Source.[Reason], Target.[Reason]) IS NOT NULL OR NULLIF(Target.[Reason], Source.[Reason]) IS NOT NULL) THEN
 UPDATE SET
  [Reason] = Source.[Reason]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ActionLogReasonId],[Reason])
 VALUES(Source.[ActionLogReasonId],Source.[Reason])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ActionLogReason]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ActionLogReason] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
