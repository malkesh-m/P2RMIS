MERGE INTO [StepType] AS Target
USING (VALUES
  (1,'Writing',3,0,1)
 ,(2,'Editing',3,0,2)
 ,(3,'Review',3,0,3)
 ,(4,'Standard',3,0,6)
 ,(5,'Preliminary',1,0,1)
 ,(6,'Revised',1,0,2)
 ,(7,'Online Discussion',1,0,3)
 ,(8,'Final Scoring',2,0,1)
 ,(9,'Review Support',3,0,5)
 ,(10,'Assurance',3,0,4)
) AS Source ([StepTypeId],[StepTypeName],[ReviewStageId],[CleanMarkupFlag],[SortOrder])
ON (Target.[StepTypeId] = Source.[StepTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[StepTypeName], Target.[StepTypeName]) IS NOT NULL OR NULLIF(Target.[StepTypeName], Source.[StepTypeName]) IS NOT NULL OR 
	NULLIF(Source.[ReviewStageId], Target.[ReviewStageId]) IS NOT NULL OR NULLIF(Target.[ReviewStageId], Source.[ReviewStageId]) IS NOT NULL OR 
	NULLIF(Source.[CleanMarkupFlag], Target.[CleanMarkupFlag]) IS NOT NULL OR NULLIF(Target.[CleanMarkupFlag], Source.[CleanMarkupFlag]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [StepTypeName] = Source.[StepTypeName], 
  [ReviewStageId] = Source.[ReviewStageId], 
  [CleanMarkupFlag] = Source.[CleanMarkupFlag], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([StepTypeId],[StepTypeName],[ReviewStageId],[CleanMarkupFlag],[SortOrder])
 VALUES(Source.[StepTypeId],Source.[StepTypeName],Source.[ReviewStageId],Source.[CleanMarkupFlag],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [StepType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[StepType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO