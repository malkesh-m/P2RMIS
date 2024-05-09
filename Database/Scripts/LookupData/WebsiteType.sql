SET IDENTITY_INSERT [WebsiteType] ON

MERGE INTO [WebsiteType] AS Target
USING (VALUES
  (1,'Primary',0)
 ,(2,'Secondary',1)
) AS Source ([WebsiteTypeId],[WebsiteType],[SortOrder])
ON (Target.[WebsiteTypeId] = Source.[WebsiteTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[WebsiteType], Target.[WebsiteType]) IS NOT NULL OR NULLIF(Target.[WebsiteType], Source.[WebsiteType]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [WebsiteType] = Source.[WebsiteType], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([WebsiteTypeId],[WebsiteType],[SortOrder])
 VALUES(Source.[WebsiteTypeId],Source.[WebsiteType],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [WebsiteType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[WebsiteType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [WebsiteType] OFF
GO