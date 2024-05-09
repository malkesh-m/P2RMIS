SET IDENTITY_INSERT [EmailAddressType] ON

MERGE INTO [EmailAddressType] AS Target
USING (VALUES
  (1,'Business',0)
 ,(2,'Personal',1)
 ,(3,'Alternate',2)
) AS Source ([EmailAddressTypeId],[EmailAddressType],[SortOrder])
ON (Target.[EmailAddressTypeId] = Source.[EmailAddressTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[EmailAddressType], Target.[EmailAddressType]) IS NOT NULL OR NULLIF(Target.[EmailAddressType], Source.[EmailAddressType]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [EmailAddressType] = Source.[EmailAddressType], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([EmailAddressTypeId],[EmailAddressType],[SortOrder])
 VALUES(Source.[EmailAddressTypeId],Source.[EmailAddressType],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [EmailAddressType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[EmailAddressType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [EmailAddressType] OFF
GO