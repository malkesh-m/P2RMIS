SET IDENTITY_INSERT [UserAddress] ON
 
MERGE INTO [UserAddress] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  (2,10,2,'SRA','Leader',NULL,'123 SRA St.',NULL,NULL,'Frederick',23,NULL,'20832',1,NULL,NULL,NULL,NULL,NULL,10,'2012-10-27T00:00:00',10,'2012-10-27T00:00:00')
 ,(13,4,2,'SRA',NULL,NULL,'123 SRA St.',NULL,NULL,'Frederick',23,NULL,'20832',1,NULL,NULL,NULL,NULL,NULL,10,'2012-10-27T00:00:00',10,'2012-10-27T00:00:00')
 ,(17,8,2,'SRA',NULL,NULL,'123 SRA St.',NULL,NULL,'Frederick',23,NULL,'20832',1,NULL,NULL,NULL,NULL,NULL,10,'2012-10-27T00:00:00',10,'2012-10-27T00:00:00')

) AS Source ([AddressID],[UserInfoID],[AddressTypeLkpID],[Institution],[Department],[Position],[Address1],[Address2],[Address3],[City],[USStateLkpID],[StateOther],[Zip],[CountryLkpID],[W9StageID],[W9StatusDate],[W9RequestDate],[W9ReceiveDate],[rowguid],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
ON (Target.[AddressID] = Source.[AddressID])
WHEN MATCHED AND (Target.[UserInfoID] <> Source.[UserInfoID] OR Target.[AddressTypeLkpID] <> Source.[AddressTypeLkpID] OR Target.[Institution] <> Source.[Institution] OR Target.[Department] <> Source.[Department] OR Target.[Position] <> Source.[Position] OR Target.[Address1] <> Source.[Address1] OR Target.[Address2] <> Source.[Address2] OR Target.[Address3] <> Source.[Address3] OR Target.[City] <> Source.[City] OR Target.[USStateLkpID] <> Source.[USStateLkpID] OR Target.[StateOther] <> Source.[StateOther] OR Target.[Zip] <> Source.[Zip] OR Target.[CountryLkpID] <> Source.[CountryLkpID] OR Target.[W9StageID] <> Source.[W9StageID] OR Target.[W9StatusDate] <> Source.[W9StatusDate] OR Target.[W9RequestDate] <> Source.[W9RequestDate] OR Target.[W9ReceiveDate] <> Source.[W9ReceiveDate] OR Target.[rowguid] <> Source.[rowguid] OR Target.[CreatedBy] <> Source.[CreatedBy] OR Target.[CreatedDate] <> Source.[CreatedDate] OR Target.[ModifiedBy] <> Source.[ModifiedBy] OR Target.[ModifiedDate] <> Source.[ModifiedDate]) THEN
 UPDATE SET
 [UserInfoID] = Source.[UserInfoID], 
[AddressTypeLkpID] = Source.[AddressTypeLkpID], 
[Institution] = Source.[Institution], 
[Department] = Source.[Department], 
[Position] = Source.[Position], 
[Address1] = Source.[Address1], 
[Address2] = Source.[Address2], 
[Address3] = Source.[Address3], 
[City] = Source.[City], 
[USStateLkpID] = Source.[USStateLkpID], 
[StateOther] = Source.[StateOther], 
[Zip] = Source.[Zip], 
[CountryLkpID] = Source.[CountryLkpID], 
[W9StageID] = Source.[W9StageID], 
[W9StatusDate] = Source.[W9StatusDate], 
[W9RequestDate] = Source.[W9RequestDate], 
[W9ReceiveDate] = Source.[W9ReceiveDate], 
[rowguid] = Source.[rowguid], 
[CreatedBy] = Source.[CreatedBy], 
[CreatedDate] = Source.[CreatedDate], 
[ModifiedBy] = Source.[ModifiedBy], 
[ModifiedDate] = Source.[ModifiedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([AddressID],[UserInfoID],[AddressTypeLkpID],[Institution],[Department],[Position],[Address1],[Address2],[Address3],[City],[USStateLkpID],[StateOther],[Zip],[CountryLkpID],[W9StageID],[W9StatusDate],[W9RequestDate],[W9ReceiveDate],[rowguid],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
 VALUES(Source.[AddressID],Source.[UserInfoID],Source.[AddressTypeLkpID],Source.[Institution],Source.[Department],Source.[Position],Source.[Address1],Source.[Address2],Source.[Address3],Source.[City],Source.[USStateLkpID],Source.[StateOther],Source.[Zip],Source.[CountryLkpID],Source.[W9StageID],Source.[W9StatusDate],Source.[W9RequestDate],Source.[W9ReceiveDate],Source.[rowguid],Source.[CreatedBy],Source.[CreatedDate],Source.[ModifiedBy],Source.[ModifiedDate])
;
 

DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [UserAddress]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[UserAddress] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END

 
SET IDENTITY_INSERT [UserAddress] OFF
GO
