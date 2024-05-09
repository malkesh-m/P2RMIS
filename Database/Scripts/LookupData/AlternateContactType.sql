SET IDENTITY_INSERT [AlternateContactType] ON

MERGE INTO [AlternateContactType] AS Target
USING (VALUES
  (1,'Assistant',0)
 ,(2,'Spouse',1)
 ,(3,'Other',2)
 ,(4,'Emergency',3)
) AS Source ([AlternateContactTypeId],[AlternateContactType],[SortOrder])
ON (Target.[AlternateContactTypeId] = Source.[AlternateContactTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[AlternateContactType], Target.[AlternateContactType]) IS NOT NULL OR NULLIF(Target.[AlternateContactType], Source.[AlternateContactType]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [AlternateContactType] = Source.[AlternateContactType], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([AlternateContactTypeId],[AlternateContactType],[SortOrder])
 VALUES(Source.[AlternateContactTypeId],Source.[AlternateContactType],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [AlternateContactType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[AlternateContactType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [AlternateContactType] OFF
GO