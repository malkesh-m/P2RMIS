SET IDENTITY_INSERT [LookupTemplateCategory] ON
 
MERGE INTO [LookupTemplateCategory] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  (1,'Email Template')
 ,(2,'Critique Template')
 ,(3,'Compliance Template')
 ,(4,'SS Template')

) AS Source ([CategoryID],[CategoryName])
ON (Target.[CategoryID] = Source.[CategoryID])
WHEN MATCHED AND (Target.[CategoryName] <> Source.[CategoryName]) THEN
 UPDATE SET
 [CategoryName] = Source.[CategoryName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([CategoryID],[CategoryName])
 VALUES(Source.[CategoryID],Source.[CategoryName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
 
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [LookupTemplateCategory]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[LookupTemplateCategory] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
 
SET IDENTITY_INSERT [LookupTemplateCategory] OFF
GO