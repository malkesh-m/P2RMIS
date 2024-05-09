SET IDENTITY_INSERT [StepTypeOperation] ON

MERGE INTO [StepTypeOperation] AS Target
USING (VALUES
  (1,1,42)
 ,(2,2,43)
 ,(3,10,44)
 ,(4,9,44)
 ,(5,3,21)
) AS Source ([StepTypeOperationId],[StepTypeId],[SystemOperationId])
ON (Target.[StepTypeOperationId] = Source.[StepTypeOperationId])
WHEN MATCHED AND (
	NULLIF(Source.[StepTypeId], Target.[StepTypeId]) IS NOT NULL OR NULLIF(Target.[StepTypeId], Source.[StepTypeId]) IS NOT NULL OR 
	NULLIF(Source.[SystemOperationId], Target.[SystemOperationId]) IS NOT NULL OR NULLIF(Target.[SystemOperationId], Source.[SystemOperationId]) IS NOT NULL) THEN
 UPDATE SET
  [StepTypeId] = Source.[StepTypeId], 
 [SystemOperationId] = Source.[SystemOperationId]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([StepTypeOperationId],[StepTypeId],[SystemOperationId])
 VALUES(Source.[StepTypeOperationId],Source.[StepTypeId],Source.[SystemOperationId])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [StepTypeOperation]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[StepTypeOperation] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [StepTypeOperation] OFF
GO