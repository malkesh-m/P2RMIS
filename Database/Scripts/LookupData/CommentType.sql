SET IDENTITY_INSERT [CommentType] ON
GO
MERGE INTO [CommentType] AS Target
USING (VALUES
(1, 'Discussion Note')
, (2, 'Admin Note')
, (3, 'General Note')
, (4, 'Summary Note') 
, (5, 'Reviewer Note') 

) AS Source ([CommentTypeID],[CommentTypeName])
ON (Target.[CommentTypeID] = Source.[CommentTypeID])
WHEN MATCHED AND (Target.[CommentTypeName] <> Source.[CommentTypeName]) THEN
 UPDATE SET
 [CommentTypeName] = Source.[CommentTypeName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([CommentTypeID],[CommentTypeName])
 VALUES(Source.[CommentTypeID],Source.[CommentTypeName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
 
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [CommentType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[CommentType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
 
SET IDENTITY_INSERT [CommentType] OFF
GO