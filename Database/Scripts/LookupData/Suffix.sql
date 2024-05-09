SET IDENTITY_INSERT [Suffix] ON

MERGE INTO [Suffix] AS Target
USING (VALUES
  (1,'III')
 ,(2,'IV')
 ,(3,'Jr.')
 ,(4,'Sr.')
) AS Source ([SuffixId],[SuffixName])
ON (Target.[SuffixId] = Source.[SuffixId])
WHEN MATCHED AND (
	NULLIF(Source.[SuffixName], Target.[SuffixName]) IS NOT NULL OR NULLIF(Target.[SuffixName], Source.[SuffixName]) IS NOT NULL) THEN
 UPDATE SET
  [SuffixName] = Source.[SuffixName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([SuffixId],[SuffixName])
 VALUES(Source.[SuffixId],Source.[SuffixName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [Suffix]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[Suffix] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [Suffix] OFF
GO