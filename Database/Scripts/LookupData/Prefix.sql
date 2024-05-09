SET IDENTITY_INSERT [Prefix] ON

MERGE INTO [Prefix] AS Target
USING (VALUES
  (1,'Dr.',0)
 ,(2,'Miss',4)
 ,(3,'Mr.',1)
 ,(4,'Mrs.',2)
 ,(5,'Ms.',3)
 ,(6,'N/A',5)
) AS Source ([PrefixId],[PrefixName],[SortOrder])
ON (Target.[PrefixId] = Source.[PrefixId])
WHEN MATCHED AND (
	NULLIF(Source.[PrefixName], Target.[PrefixName]) IS NOT NULL OR NULLIF(Target.[PrefixName], Source.[PrefixName]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [PrefixName] = Source.[PrefixName], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([PrefixId],[PrefixName],[SortOrder])
 VALUES(Source.[PrefixId],Source.[PrefixName],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [Prefix]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[Prefix] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [Prefix] OFF
GO
