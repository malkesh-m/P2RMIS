MERGE INTO [AddressType] AS Target
USING (VALUES
  (1,'')
 ,(2,'Organization')
 ,(3,'Personal')
 ,(4,'W-9')
) AS Source ([AddressTypeId],[AddressTypeName])
ON (Target.[AddressTypeId] = Source.[AddressTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[AddressTypeName], Target.[AddressTypeName]) IS NOT NULL OR NULLIF(Target.[AddressTypeName], Source.[AddressTypeName]) IS NOT NULL) THEN
 UPDATE SET
  [AddressTypeName] = Source.[AddressTypeName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([AddressTypeId],[AddressTypeName])
 VALUES(Source.[AddressTypeId],Source.[AddressTypeName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [AddressType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[AddressType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO