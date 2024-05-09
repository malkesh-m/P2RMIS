MERGE INTO [SummaryStatementMode] AS Target
USING (VALUES
  (1,'Web')
 ,(2,'Document')
) AS Source ([SummaryStatementModeId],[ModeLabel])
ON (Target.[SummaryStatementModeId] = Source.[SummaryStatementModeId])
WHEN MATCHED AND (
	NULLIF(Source.[ModeLabel], Target.[ModeLabel]) IS NOT NULL OR NULLIF(Target.[ModeLabel], Source.[ModeLabel]) IS NOT NULL) THEN
 UPDATE SET
  [ModeLabel] = Source.[ModeLabel]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([SummaryStatementModeId],[ModeLabel])
 VALUES(Source.[SummaryStatementModeId],Source.[ModeLabel])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [SummaryStatementMode]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[SummaryStatementMode] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO