﻿

MERGE INTO [PolicyType] AS Target
USING (VALUES
  (1,'Access','Access',1)
 ,(2,'Network','Network',2) 
) AS Source ([PolicyTypeId],[Name],[Description],[SortOrder])
ON (Target.[PolicyTypeId] = Source.[PolicyTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[Name], Target.[Name]) IS NOT NULL OR NULLIF(Target.[Name], Source.[Name]) IS NOT NULL OR 
	NULLIF(Source.[Description], Target.[Description]) IS NOT NULL OR NULLIF(Target.[Description], Source.[Description]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [Name] = Source.[Name], 
  [Description] = source.[Description],
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([PolicyTypeId],[Name],[Description],[SortOrder])
 VALUES(Source.[PolicyTypeId],Source.[Name],Source.[Description],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [PolicyType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[PolicyType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO