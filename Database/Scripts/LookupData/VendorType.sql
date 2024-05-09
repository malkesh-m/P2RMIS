MERGE INTO [VendorType] AS Target
USING (VALUES
  (1,'Individual')
 ,(2,'Institution')
) AS Source ([VendorTypeId],[VendorTypeName])
ON (Target.[VendorTypeId] = Source.[VendorTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[VendorTypeName], Target.[VendorTypeName]) IS NOT NULL OR NULLIF(Target.[VendorTypeName], Source.[VendorTypeName]) IS NOT NULL) THEN
 UPDATE SET
  [VendorTypeName] = Source.[VendorTypeName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([VendorTypeId],[VendorTypeName])
 VALUES(Source.[VendorTypeId],Source.[VendorTypeName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [VendorType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[VendorType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO