﻿SET IDENTITY_INSERT [Gender] ON

MERGE INTO [Gender] AS Target
USING (VALUES
  (1,'Female')
 ,(2,'Male')
) AS Source ([GenderId],[Gender])
ON (Target.[GenderId] = Source.[GenderId])
WHEN MATCHED AND (
	NULLIF(Source.[Gender], Target.[Gender]) IS NOT NULL OR NULLIF(Target.[Gender], Source.[Gender]) IS NOT NULL) THEN
 UPDATE SET
  [Gender] = Source.[Gender]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([GenderId],[Gender])
 VALUES(Source.[GenderId],Source.[Gender])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [Gender]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[Gender] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [Gender] OFF
GO