SET IDENTITY_INSERT [RegistrationDocumentItem] ON

MERGE INTO [RegistrationDocumentItem] AS Target
USING (VALUES
  (1,'Financial Disclosure')
 ,(2,'Financial Disclosure Details')
 ,(3,'Additional Disclosure')
 ,(4,'Additional Disclosure Details')
 ,(8,'Consultant Fee Accepted')
 ,(9,'Business Category')
 ,(10,'Contractual Agreement')
) AS Source ([RegistrationDocumentItemId],[ItemName])
ON (Target.[RegistrationDocumentItemId] = Source.[RegistrationDocumentItemId])
WHEN MATCHED AND (
	NULLIF(Source.[ItemName], Target.[ItemName]) IS NOT NULL OR NULLIF(Target.[ItemName], Source.[ItemName]) IS NOT NULL) THEN
 UPDATE SET
  [ItemName] = Source.[ItemName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([RegistrationDocumentItemId],[ItemName])
 VALUES(Source.[RegistrationDocumentItemId],Source.[ItemName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [RegistrationDocumentItem]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[RegistrationDocumentItem] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [RegistrationDocumentItem] OFF
GO