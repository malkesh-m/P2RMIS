SET IDENTITY_INSERT [LookupTemplateType] ON
 
MERGE INTO [LookupTemplateType] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  (1,'System Generated')
 ,(2,'User Generated')

) AS Source ([TypeID],[TypeName])
ON (Target.[TypeID] = Source.[TypeID])
WHEN MATCHED AND (Target.[TypeName] <> Source.[TypeName]) THEN
 UPDATE SET
 [TypeName] = Source.[TypeName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([TypeID],[TypeName])
 VALUES(Source.[TypeID],Source.[TypeName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
 
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [LookupTemplateType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[LookupTemplateType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
 
SET IDENTITY_INSERT [LookupTemplateType] OFF
GO