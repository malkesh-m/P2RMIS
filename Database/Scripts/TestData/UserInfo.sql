SET IDENTITY_INSERT [UserInfo] ON
 
MERGE INTO [UserInfo] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  (4,4,1,'Craig',NULL,'Henson',1,'Craig Henson','Craig Henson',23,2,NULL,NULL,NULL,NULL)
 ,(8,8,1,'Maureen',NULL,'Mussler',1,'Maureen Mussler','Maureen Mussler',23,2,NULL,NULL,NULL,NULL)
 ,(10,10,1,'Pushpa',NULL,'Unnithan',1,'Pushpa Unnithan','Pushpa Unnithan',23,1,NULL,NULL,NULL,NULL)

) AS Source ([UserInfoID],[UserID],[PrefixLkpID],[FirstName],[MiddleName],[LastName],[SuffixLkpID],[BadgeName],[FullName],[DegreeLkpID],[GenderLkpID],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
ON (Target.[UserInfoID] = Source.[UserInfoID])
WHEN MATCHED AND (Target.[UserID] <> Source.[UserID] OR Target.[PrefixLkpID] <> Source.[PrefixLkpID] OR Target.[FirstName] <> Source.[FirstName] OR Target.[MiddleName] <> Source.[MiddleName] OR Target.[LastName] <> Source.[LastName] OR Target.[SuffixLkpID] <> Source.[SuffixLkpID] OR Target.[BadgeName] <> Source.[BadgeName] OR Target.[FullName] <> Source.[FullName] OR Target.[DegreeLkpID] <> Source.[DegreeLkpID] OR Target.[GenderLkpID] <> Source.[GenderLkpID] OR Target.[CreatedBy] <> Source.[CreatedBy] OR Target.[CreatedDate] <> Source.[CreatedDate] OR Target.[ModifiedBy] <> Source.[ModifiedBy] OR Target.[ModifiedDate] <> Source.[ModifiedDate]) THEN
 UPDATE SET
 [UserID] = Source.[UserID], 
[PrefixLkpID] = Source.[PrefixLkpID], 
[FirstName] = Source.[FirstName], 
[MiddleName] = Source.[MiddleName], 
[LastName] = Source.[LastName], 
[SuffixLkpID] = Source.[SuffixLkpID], 
[BadgeName] = Source.[BadgeName], 
[FullName] = Source.[FullName], 
[DegreeLkpID] = Source.[DegreeLkpID], 
[GenderLkpID] = Source.[GenderLkpID], 
[CreatedBy] = Source.[CreatedBy], 
[CreatedDate] = Source.[CreatedDate], 
[ModifiedBy] = Source.[ModifiedBy], 
[ModifiedDate] = Source.[ModifiedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([UserInfoID],[UserID],[PrefixLkpID],[FirstName],[MiddleName],[LastName],[SuffixLkpID],[BadgeName],[FullName],[DegreeLkpID],[GenderLkpID],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
 VALUES(Source.[UserInfoID],Source.[UserID],Source.[PrefixLkpID],Source.[FirstName],Source.[MiddleName],Source.[LastName],Source.[SuffixLkpID],Source.[BadgeName],Source.[FullName],Source.[DegreeLkpID],Source.[GenderLkpID],Source.[CreatedBy],Source.[CreatedDate],Source.[ModifiedBy],Source.[ModifiedDate])
;
 

DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [UserInfo]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[UserInfo] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END

 
SET IDENTITY_INSERT [UserInfo] OFF
GO