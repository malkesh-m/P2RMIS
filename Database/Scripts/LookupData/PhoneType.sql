SET IDENTITY_INSERT [PhoneType] ON

MERGE INTO [PhoneType] AS Target
USING (VALUES
  (2,'Work Fax',4)
 ,(3,'Desk',2)
 ,(4,'Cell/NoText',1)
 ,(5,'Home Fax',5)
 ,(6,'Home',3)
 ,(9,'Cell/Text',0)
) AS Source ([PhoneTypeId],[PhoneType],[SortOrder])
ON (Target.[PhoneTypeId] = Source.[PhoneTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[PhoneType], Target.[PhoneType]) IS NOT NULL OR NULLIF(Target.[PhoneType], Source.[PhoneType]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [PhoneType] = Source.[PhoneType], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([PhoneTypeId],[PhoneType],[SortOrder])
 VALUES(Source.[PhoneTypeId],Source.[PhoneType],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [PhoneType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[PhoneType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [PhoneType] OFF
GO
