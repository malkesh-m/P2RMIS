SET IDENTITY_INSERT [UserEmail] ON
 
MERGE INTO [UserEmail] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  (3,10,'craig_henson@sra.com',1,NULL,NULL,NULL,NULL,NULL)
 ,(4,10,'prsm-test9@srahosting.com',0,NULL,NULL,NULL,NULL,NULL)
 ,(14,4,'prsm-test2@srahosting.com',1,NULL,NULL,NULL,NULL,NULL)
 ,(15,4,'prsm-test2@srahosting.com',0,NULL,NULL,NULL,NULL,NULL)
 ,(20,8,'prsm-test6@srahosting.com',1,NULL,NULL,NULL,NULL,NULL)
 ,(21,8,'prsm-test6@srahosting.com',0,NULL,NULL,NULL,NULL,NULL)

) AS Source ([EmailID],[UserInfoID],[Email],[PrimaryFlag],[rowguid],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
ON (Target.[EmailID] = Source.[EmailID])
WHEN MATCHED AND (Target.[UserInfoID] <> Source.[UserInfoID] OR Target.[Email] <> Source.[Email] OR Target.[PrimaryFlag] <> Source.[PrimaryFlag] OR Target.[rowguid] <> Source.[rowguid] OR Target.[CreatedBy] <> Source.[CreatedBy] OR Target.[CreatedDate] <> Source.[CreatedDate] OR Target.[ModifiedBy] <> Source.[ModifiedBy] OR Target.[ModifiedDate] <> Source.[ModifiedDate]) THEN
 UPDATE SET
 [UserInfoID] = Source.[UserInfoID], 
[Email] = Source.[Email], 
[PrimaryFlag] = Source.[PrimaryFlag], 
[rowguid] = Source.[rowguid], 
[CreatedBy] = Source.[CreatedBy], 
[CreatedDate] = Source.[CreatedDate], 
[ModifiedBy] = Source.[ModifiedBy], 
[ModifiedDate] = Source.[ModifiedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([EmailID],[UserInfoID],[Email],[PrimaryFlag],[rowguid],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
 VALUES(Source.[EmailID],Source.[UserInfoID],Source.[Email],Source.[PrimaryFlag],Source.[rowguid],Source.[CreatedBy],Source.[CreatedDate],Source.[ModifiedBy],Source.[ModifiedDate])
;
 

DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [UserEmail]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[UserEmail] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END

 
SET IDENTITY_INSERT [UserEmail] OFF
GO