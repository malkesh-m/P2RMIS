SET IDENTITY_INSERT [UserPhone] ON
 
MERGE INTO [UserPhone] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  (3,10,'240-389-1333',NULL,1,0,NULL,10,'2012-08-27T00:00:00',10,'2012-08-27T00:00:00')
 ,(4,10,'301-555-5555',NULL,0,0,NULL,10,'2012-08-27T00:00:00',10,'2012-08-27T00:00:00')
 ,(15,4,'301-555-5555',NULL,1,0,NULL,10,'2012-08-27T00:00:00',10,'2012-08-27T00:00:00')
 ,(16,4,NULL,NULL,0,0,NULL,10,'2012-08-27T00:00:00',10,'2012-08-27T00:00:00')
 ,(21,8,'301-555-5555',NULL,1,0,NULL,10,'2012-08-27T00:00:00',10,'2012-08-27T00:00:00')
 ,(22,8,NULL,NULL,0,0,NULL,10,'2012-08-27T00:00:00',10,'2012-08-27T00:00:00')

) AS Source ([PhoneID],[UserInfoID],[Phone],[Extension],[PrimaryFlag],[International],[rowguid],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
ON (Target.[PhoneID] = Source.[PhoneID])
WHEN MATCHED AND (Target.[UserInfoID] <> Source.[UserInfoID] OR Target.[Phone] <> Source.[Phone] OR Target.[Extension] <> Source.[Extension] OR Target.[PrimaryFlag] <> Source.[PrimaryFlag] OR Target.[International] <> Source.[International] OR Target.[rowguid] <> Source.[rowguid] OR Target.[CreatedBy] <> Source.[CreatedBy] OR Target.[CreatedDate] <> Source.[CreatedDate] OR Target.[ModifiedBy] <> Source.[ModifiedBy] OR Target.[ModifiedDate] <> Source.[ModifiedDate]) THEN
 UPDATE SET
 [UserInfoID] = Source.[UserInfoID], 
[Phone] = Source.[Phone], 
[Extension] = Source.[Extension], 
[PrimaryFlag] = Source.[PrimaryFlag], 
[International] = Source.[International], 
[rowguid] = Source.[rowguid], 
[CreatedBy] = Source.[CreatedBy], 
[CreatedDate] = Source.[CreatedDate], 
[ModifiedBy] = Source.[ModifiedBy], 
[ModifiedDate] = Source.[ModifiedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([PhoneID],[UserInfoID],[Phone],[Extension],[PrimaryFlag],[International],[rowguid],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
 VALUES(Source.[PhoneID],Source.[UserInfoID],Source.[Phone],Source.[Extension],Source.[PrimaryFlag],Source.[International],Source.[rowguid],Source.[CreatedBy],Source.[CreatedDate],Source.[ModifiedBy],Source.[ModifiedDate])
;
 

DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [UserPhone]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[UserPhone] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END

 
SET IDENTITY_INSERT [UserPhone] OFF
GO
