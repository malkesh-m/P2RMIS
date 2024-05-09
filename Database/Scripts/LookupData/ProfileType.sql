SET IDENTITY_INSERT [ProfileType] ON

MERGE INTO [ProfileType] AS Target
USING (VALUES
  (1,'Prospect',0)
 ,(2,'Reviewer',1)
 ,(3,'SRA Staff',2)
 ,(4,'Client',3)
 ,(5,'Misconduct',4)
) AS Source ([ProfileTypeId],[ProfileTypeName],[SortOrder])
ON (Target.[ProfileTypeId] = Source.[ProfileTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[ProfileTypeName], Target.[ProfileTypeName]) IS NOT NULL OR NULLIF(Target.[ProfileTypeName], Source.[ProfileTypeName]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [ProfileTypeName] = Source.[ProfileTypeName], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ProfileTypeId],[ProfileTypeName],[SortOrder])
 VALUES(Source.[ProfileTypeId],Source.[ProfileTypeName],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ProfileType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ProfileType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ProfileType] OFF
GO