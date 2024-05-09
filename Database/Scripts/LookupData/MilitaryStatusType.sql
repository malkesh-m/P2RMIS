SET IDENTITY_INSERT [MilitaryStatusType] ON

MERGE INTO [MilitaryStatusType] AS Target
USING (VALUES
  (1,'Active',0)
 ,(2,'Retired',1)
 ,(3,'Veteran',2)
) AS Source ([MilitaryStatusTypeId],[StatusType],[SortOrder])
ON (Target.[MilitaryStatusTypeId] = Source.[MilitaryStatusTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[StatusType], Target.[StatusType]) IS NOT NULL OR NULLIF(Target.[StatusType], Source.[StatusType]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [StatusType] = Source.[StatusType], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([MilitaryStatusTypeId],[StatusType],[SortOrder])
 VALUES(Source.[MilitaryStatusTypeId],Source.[StatusType],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [MilitaryStatusType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[MilitaryStatusType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [MilitaryStatusType] OFF
GO