MERGE INTO [ElementType] AS Target
USING (VALUES
  (1,'Review Criteria')
 ,(2,'Application Summary')
 ,(3,'Criteria Summary')
 ,(4,'Unassigned Comments')
) AS Source ([ElementTypeId],[ElementTypeName])
ON (Target.[ElementTypeId] = Source.[ElementTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[ElementTypeName], Target.[ElementTypeName]) IS NOT NULL OR NULLIF(Target.[ElementTypeName], Source.[ElementTypeName]) IS NOT NULL) THEN
 UPDATE SET
  [ElementTypeName] = Source.[ElementTypeName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ElementTypeId],[ElementTypeName])
 VALUES(Source.[ElementTypeId],Source.[ElementTypeName])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ElementType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ElementType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO