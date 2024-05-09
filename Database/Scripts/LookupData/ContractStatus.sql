MERGE INTO [ContractStatus] AS [Target]
USING (VALUES
  (1,NULL,N'Original',0,0)
 ,(2,N'Add Addendum',N'Addendum Added',1,1)
 ,(3,N'Bypass',N'Bypassed',4,1)
 ,(4,N'Regenerate',N'Regenerated',2,1)
 ,(5,N'Replace',N'Replaced',3,1)
) AS [Source] ([ContractStatusId],[ActionLabel],[StatusLabel],[SortOrder],[UserActionFlag])
ON ([Target].[ContractStatusId] = [Source].[ContractStatusId])
WHEN MATCHED AND (
	NULLIF([Source].[ActionLabel], [Target].[ActionLabel]) IS NOT NULL OR NULLIF([Target].[ActionLabel], [Source].[ActionLabel]) IS NOT NULL OR 
	NULLIF([Source].[StatusLabel], [Target].[StatusLabel]) IS NOT NULL OR NULLIF([Target].[StatusLabel], [Source].[StatusLabel]) IS NOT NULL OR 
	NULLIF([Source].[SortOrder], [Target].[SortOrder]) IS NOT NULL OR NULLIF([Target].[SortOrder], [Source].[SortOrder]) IS NOT NULL OR 
	NULLIF([Source].[UserActionFlag], [Target].[UserActionFlag]) IS NOT NULL OR NULLIF([Target].[UserActionFlag], [Source].[UserActionFlag]) IS NOT NULL) THEN
 UPDATE SET
  [Target].[ActionLabel] = [Source].[ActionLabel], 
  [Target].[StatusLabel] = [Source].[StatusLabel], 
  [Target].[SortOrder] = [Source].[SortOrder], 
  [Target].[UserActionFlag] = [Source].[UserActionFlag]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ContractStatusId],[ActionLabel],[StatusLabel],[SortOrder],[UserActionFlag])
 VALUES([Source].[ContractStatusId],[Source].[ActionLabel],[Source].[StatusLabel],[Source].[SortOrder],[Source].[UserActionFlag])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ContractStatus]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ContractStatus] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO