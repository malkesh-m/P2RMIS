MERGE INTO [RegistrationDocumentType] AS Target
USING (VALUES
  (1,'Acknowledge/NDA')
 ,(2,'Bias/COI')
 ,(3,'Contractual Agreement')
) AS Source ([RegistrationDocumentTypeId],[RegistrationDocumentName])
ON (Target.[RegistrationDocumentTypeId] = Source.[RegistrationDocumentTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[RegistrationDocumentName], Target.[RegistrationDocumentName]) IS NOT NULL OR NULLIF(Target.[RegistrationDocumentName], Source.[RegistrationDocumentName]) IS NOT NULL) THEN
 UPDATE SET
  [RegistrationDocumentName] = Source.[RegistrationDocumentName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([RegistrationDocumentTypeId],[RegistrationDocumentName])
 VALUES(Source.[RegistrationDocumentTypeId],Source.[RegistrationDocumentName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [RegistrationDocumentType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[RegistrationDocumentType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO