SET IDENTITY_INSERT [ClientRegistration] ON

MERGE INTO [ClientRegistration] AS Target
USING (VALUES
  (1,9,1)
 ,(2,19,1)
 ,(3,23,1)
 ,(4,20,1)
) AS Source ([ClientRegistrationId],[ClientId],[ActiveFlag])
ON (Target.[ClientRegistrationId] = Source.[ClientRegistrationId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientId], Target.[ClientId]) IS NOT NULL OR NULLIF(Target.[ClientId], Source.[ClientId]) IS NOT NULL OR 
	NULLIF(Source.[ActiveFlag], Target.[ActiveFlag]) IS NOT NULL OR NULLIF(Target.[ActiveFlag], Source.[ActiveFlag]) IS NOT NULL) THEN
 UPDATE SET
  [ClientId] = Source.[ClientId], 
  [ActiveFlag] = Source.[ActiveFlag]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientRegistrationId],[ClientId],[ActiveFlag])
 VALUES(Source.[ClientRegistrationId],Source.[ClientId],Source.[ActiveFlag])
;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientRegistration]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientRegistration] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientRegistration] OFF
GO