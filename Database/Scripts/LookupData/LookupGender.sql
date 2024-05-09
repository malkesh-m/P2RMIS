SET IDENTITY_INSERT [LookupGender] ON
 
MERGE INTO [LookupGender] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  (1,'Female')
 ,(2,'Male')

) AS Source ([GenderID],[Gender])
ON (Target.[GenderID] = Source.[GenderID])
WHEN MATCHED AND (Target.[Gender] <> Source.[Gender]) THEN
 UPDATE SET
 [Gender] = Source.[Gender]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([GenderID],[Gender])
 VALUES(Source.[GenderID],Source.[Gender])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
 
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [LookupGender]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[LookupGender] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
 
SET IDENTITY_INSERT [LookupGender] OFF
GO