SET IDENTITY_INSERT [ClientApplicationInfoType] ON

MERGE INTO [ClientApplicationInfoType] AS Target
USING (VALUES
  (1,19,'Grant ID')
 ,(2,23,'Grant ID')
 ,(3,9,'CARS Open Date')
 ,(4,9,'CARS Closed Date')
 ,(5,9,'Submitted Date')
  ,(6,9,'Application Type')
) AS Source ([ClientApplicationInfoTypeId],[ClientId],[InfoTypeDescription])
ON (Target.[ClientApplicationInfoTypeId] = Source.[ClientApplicationInfoTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientId], Target.[ClientId]) IS NOT NULL OR NULLIF(Target.[ClientId], Source.[ClientId]) IS NOT NULL OR 
	NULLIF(Source.[InfoTypeDescription], Target.[InfoTypeDescription]) IS NOT NULL OR NULLIF(Target.[InfoTypeDescription], Source.[InfoTypeDescription]) IS NOT NULL) THEN
 UPDATE SET
  [ClientId] = Source.[ClientId], 
  [InfoTypeDescription] = Source.[InfoTypeDescription]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientApplicationInfoTypeId],[ClientId],[InfoTypeDescription])
 VALUES(Source.[ClientApplicationInfoTypeId],Source.[ClientId],Source.[InfoTypeDescription])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientApplicationInfoType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientApplicationInfoType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientApplicationInfoType] OFF
GO