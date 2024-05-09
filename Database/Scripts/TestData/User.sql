SET IDENTITY_INSERT [User] ON

MERGE INTO [User] AS Target
USING (VALUES
 (4,'ClientStaff','63BFE8F313960617AAB70B4F7186A21EC8866365',NULL,'xMLdy3GbmFcHB8Ci1QiDnGch/FEnXPB1ZUzRgUIltfg=',2,3,4,'a94a8fe5ccb19ba61c4c0873d391e987982fbbd3','a94a8fe5ccb19ba61c4c0873d391e987982fbbd3','a94a8fe5ccb19ba61c4c0873d391e987982fbbd3',NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'2012-07-25T15:03:15.423',NULL,'2012-08-25T15:04:24.047',5,'2012-07-25T00:00:00',1,'2012-07-25T15:03:15.423',0,'2012-07-25T15:03:15.423',NULL,NULL,'2012-07-25T15:03:15.423',NULL,'ae8bea75-a5e5-4093-9015-33cffc80725c','2012-07-25T15:03:15.423',NULL)
 ,(8,'SRAStaff','1ED7235721A5CC92457006CE8E29E9989B6A22FB',NULL,'6R2jDwJVZQ2EIW47UvcFCzNuFVIRU7+wA2oIkeKxu40=',9,10,11,'a94a8fe5ccb19ba61c4c0873d391e987982fbbd3','a94a8fe5ccb19ba61c4c0873d391e987982fbbd3','a94a8fe5ccb19ba61c4c0873d391e987982fbbd3',NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'2012-07-25T15:04:40.500',NULL,'2012-08-25T15:04:24.047',3,'2012-07-25T00:00:00',1,'2012-07-25T15:04:40.500',0,'2012-07-25T15:04:40.500',NULL,NULL,'2012-07-25T15:04:40.500',NULL,'469b23c4-7e9e-44c6-a7f8-9a536374a4ba','2012-07-25T15:04:40.500',NULL)
 ,(10,'Webmaster','C0DB4E8CCAC997483675A08266CE82B665F0CC64',NULL,'+F1Hs6IZ/Ls0UDMNLhzhpwTCv+sxETe3BA1Ssz3KbX0=',3,5,10,'a94a8fe5ccb19ba61c4c0873d391e987982fbbd3','a94a8fe5ccb19ba61c4c0873d391e987982fbbd3','a94a8fe5ccb19ba61c4c0873d391e987982fbbd3',0,NULL,NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,10,'2012-09-20T00:47:19.343',10,'2012-12-18T15:02:03.123',3,'2012-07-25T00:00:00',1,'2012-09-20T00:47:19.343',0,'2012-09-20T00:47:19.343',NULL,NULL,'2012-09-20T00:47:19.343',NULL,'f4dedd10-b3de-4976-a4ed-54419166a8bd','2012-09-20T00:47:19.343',NULL)
) AS Source ([UserID],[UserLogin],[Password],[PasswordDate],[PasswordSalt],[Q1ID],[Q2ID],[Q3ID],[Answer1],[Answer2],[Answer3],[International],[ResumeFileID],[ResumeDate],[Verified],[VerifiedDate],[Comments],[Deleted],[DeleteDate],[DeleteBy],[DeleteComment],[rowguid],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate],[account_status_id],[account_status_date],[IsActivated],[LastLoginDate],[IsLockedOut],[LastLockedOutDate],[LastLockedOutReason],[NewPasswordKey],[NewPasswordRequested],[NewEmail],[NewEmailKey],[NewEmailRequested],[OtherFields])
ON (Target.[UserID] = Source.[UserID])
WHEN MATCHED AND (
	NULLIF(Source.[UserLogin], Target.[UserLogin]) IS NOT NULL OR NULLIF(Target.[UserLogin], Source.[UserLogin]) IS NOT NULL OR 
	NULLIF(Source.[Password], Target.[Password]) IS NOT NULL OR NULLIF(Target.[Password], Source.[Password]) IS NOT NULL OR 
	NULLIF(Source.[PasswordDate], Target.[PasswordDate]) IS NOT NULL OR NULLIF(Target.[PasswordDate], Source.[PasswordDate]) IS NOT NULL OR 
	NULLIF(Source.[PasswordSalt], Target.[PasswordSalt]) IS NOT NULL OR NULLIF(Target.[PasswordSalt], Source.[PasswordSalt]) IS NOT NULL OR 
	NULLIF(Source.[Q1ID], Target.[Q1ID]) IS NOT NULL OR NULLIF(Target.[Q1ID], Source.[Q1ID]) IS NOT NULL OR 
	NULLIF(Source.[Q2ID], Target.[Q2ID]) IS NOT NULL OR NULLIF(Target.[Q2ID], Source.[Q2ID]) IS NOT NULL OR 
	NULLIF(Source.[Q3ID], Target.[Q3ID]) IS NOT NULL OR NULLIF(Target.[Q3ID], Source.[Q3ID]) IS NOT NULL OR 
	NULLIF(Source.[Answer1], Target.[Answer1]) IS NOT NULL OR NULLIF(Target.[Answer1], Source.[Answer1]) IS NOT NULL OR 
	NULLIF(Source.[Answer2], Target.[Answer2]) IS NOT NULL OR NULLIF(Target.[Answer2], Source.[Answer2]) IS NOT NULL OR 
	NULLIF(Source.[Answer3], Target.[Answer3]) IS NOT NULL OR NULLIF(Target.[Answer3], Source.[Answer3]) IS NOT NULL OR 
	NULLIF(Source.[International], Target.[International]) IS NOT NULL OR NULLIF(Target.[International], Source.[International]) IS NOT NULL OR 
	NULLIF(Source.[ResumeFileID], Target.[ResumeFileID]) IS NOT NULL OR NULLIF(Target.[ResumeFileID], Source.[ResumeFileID]) IS NOT NULL OR 
	NULLIF(Source.[ResumeDate], Target.[ResumeDate]) IS NOT NULL OR NULLIF(Target.[ResumeDate], Source.[ResumeDate]) IS NOT NULL OR 
	NULLIF(Source.[Verified], Target.[Verified]) IS NOT NULL OR NULLIF(Target.[Verified], Source.[Verified]) IS NOT NULL OR 
	NULLIF(Source.[VerifiedDate], Target.[VerifiedDate]) IS NOT NULL OR NULLIF(Target.[VerifiedDate], Source.[VerifiedDate]) IS NOT NULL OR 
	NULLIF(CAST(Source.[Comments] AS VARCHAR(MAX)), CAST(Target.[Comments] AS VARCHAR(MAX))) IS NOT NULL OR NULLIF(CAST(Target.[Comments] AS VARCHAR(MAX)), CAST(Source.[Comments] AS VARCHAR(MAX))) IS NOT NULL OR 
	NULLIF(Source.[Deleted], Target.[Deleted]) IS NOT NULL OR NULLIF(Target.[Deleted], Source.[Deleted]) IS NOT NULL OR 
	NULLIF(Source.[DeleteDate], Target.[DeleteDate]) IS NOT NULL OR NULLIF(Target.[DeleteDate], Source.[DeleteDate]) IS NOT NULL OR 
	NULLIF(Source.[DeleteBy], Target.[DeleteBy]) IS NOT NULL OR NULLIF(Target.[DeleteBy], Source.[DeleteBy]) IS NOT NULL OR 
	NULLIF(Source.[DeleteComment], Target.[DeleteComment]) IS NOT NULL OR NULLIF(Target.[DeleteComment], Source.[DeleteComment]) IS NOT NULL OR 
	NULLIF(Source.[rowguid], Target.[rowguid]) IS NOT NULL OR NULLIF(Target.[rowguid], Source.[rowguid]) IS NOT NULL OR 
	NULLIF(Source.[CreatedBy], Target.[CreatedBy]) IS NOT NULL OR NULLIF(Target.[CreatedBy], Source.[CreatedBy]) IS NOT NULL OR 
	NULLIF(Source.[CreatedDate], Target.[CreatedDate]) IS NOT NULL OR NULLIF(Target.[CreatedDate], Source.[CreatedDate]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedBy], Target.[ModifiedBy]) IS NOT NULL OR NULLIF(Target.[ModifiedBy], Source.[ModifiedBy]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedDate], Target.[ModifiedDate]) IS NOT NULL OR NULLIF(Target.[ModifiedDate], Source.[ModifiedDate]) IS NOT NULL OR 
	NULLIF(Source.[account_status_id], Target.[account_status_id]) IS NOT NULL OR NULLIF(Target.[account_status_id], Source.[account_status_id]) IS NOT NULL OR 
	NULLIF(Source.[account_status_date], Target.[account_status_date]) IS NOT NULL OR NULLIF(Target.[account_status_date], Source.[account_status_date]) IS NOT NULL OR 
	NULLIF(Source.[IsActivated], Target.[IsActivated]) IS NOT NULL OR NULLIF(Target.[IsActivated], Source.[IsActivated]) IS NOT NULL OR 
	NULLIF(Source.[LastLoginDate], Target.[LastLoginDate]) IS NOT NULL OR NULLIF(Target.[LastLoginDate], Source.[LastLoginDate]) IS NOT NULL OR 
	NULLIF(Source.[IsLockedOut], Target.[IsLockedOut]) IS NOT NULL OR NULLIF(Target.[IsLockedOut], Source.[IsLockedOut]) IS NOT NULL OR 
	NULLIF(Source.[LastLockedOutDate], Target.[LastLockedOutDate]) IS NOT NULL OR NULLIF(Target.[LastLockedOutDate], Source.[LastLockedOutDate]) IS NOT NULL OR 
	NULLIF(Source.[LastLockedOutReason], Target.[LastLockedOutReason]) IS NOT NULL OR NULLIF(Target.[LastLockedOutReason], Source.[LastLockedOutReason]) IS NOT NULL OR 
	NULLIF(Source.[NewPasswordKey], Target.[NewPasswordKey]) IS NOT NULL OR NULLIF(Target.[NewPasswordKey], Source.[NewPasswordKey]) IS NOT NULL OR 
	NULLIF(Source.[NewPasswordRequested], Target.[NewPasswordRequested]) IS NOT NULL OR NULLIF(Target.[NewPasswordRequested], Source.[NewPasswordRequested]) IS NOT NULL OR 
	NULLIF(Source.[NewEmail], Target.[NewEmail]) IS NOT NULL OR NULLIF(Target.[NewEmail], Source.[NewEmail]) IS NOT NULL OR 
	NULLIF(Source.[NewEmailKey], Target.[NewEmailKey]) IS NOT NULL OR NULLIF(Target.[NewEmailKey], Source.[NewEmailKey]) IS NOT NULL OR 
	NULLIF(Source.[NewEmailRequested], Target.[NewEmailRequested]) IS NOT NULL OR NULLIF(Target.[NewEmailRequested], Source.[NewEmailRequested]) IS NOT NULL OR 
	NULLIF(Source.[OtherFields], Target.[OtherFields]) IS NOT NULL OR NULLIF(Target.[OtherFields], Source.[OtherFields]) IS NOT NULL) THEN
 UPDATE SET
[UserLogin] = Source.[UserLogin], 
[Password] = Source.[Password], 
[PasswordDate] = Source.[PasswordDate], 
[PasswordSalt] = Source.[PasswordSalt], 
[Q1ID] = Source.[Q1ID], 
[Q2ID] = Source.[Q2ID], 
[Q3ID] = Source.[Q3ID], 
[Answer1] = Source.[Answer1], 
[Answer2] = Source.[Answer2], 
[Answer3] = Source.[Answer3], 
[International] = Source.[International], 
[ResumeFileID] = Source.[ResumeFileID], 
[ResumeDate] = Source.[ResumeDate], 
[Verified] = Source.[Verified], 
[VerifiedDate] = Source.[VerifiedDate], 
[Comments] = Source.[Comments], 
[Deleted] = Source.[Deleted], 
[DeleteDate] = Source.[DeleteDate], 
[DeleteBy] = Source.[DeleteBy], 
[DeleteComment] = Source.[DeleteComment], 
[rowguid] = Source.[rowguid], 
[CreatedBy] = Source.[CreatedBy], 
[CreatedDate] = Source.[CreatedDate], 
[ModifiedBy] = Source.[ModifiedBy], 
[ModifiedDate] = Source.[ModifiedDate], 
[account_status_id] = Source.[account_status_id], 
[account_status_date] = Source.[account_status_date], 
[IsActivated] = Source.[IsActivated], 
[LastLoginDate] = Source.[LastLoginDate], 
[IsLockedOut] = Source.[IsLockedOut], 
[LastLockedOutDate] = Source.[LastLockedOutDate], 
[LastLockedOutReason] = Source.[LastLockedOutReason], 
[NewPasswordKey] = Source.[NewPasswordKey], 
[NewPasswordRequested] = Source.[NewPasswordRequested], 
[NewEmail] = Source.[NewEmail], 
[NewEmailKey] = Source.[NewEmailKey], 
[NewEmailRequested] = Source.[NewEmailRequested], 
[OtherFields] = Source.[OtherFields]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([UserID],[UserLogin],[Password],[PasswordDate],[PasswordSalt],[Q1ID],[Q2ID],[Q3ID],[Answer1],[Answer2],[Answer3],[International],[ResumeFileID],[ResumeDate],[Verified],[VerifiedDate],[Comments],[Deleted],[DeleteDate],[DeleteBy],[DeleteComment],[rowguid],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate],[account_status_id],[account_status_date],[IsActivated],[LastLoginDate],[IsLockedOut],[LastLockedOutDate],[LastLockedOutReason],[NewPasswordKey],[NewPasswordRequested],[NewEmail],[NewEmailKey],[NewEmailRequested],[OtherFields])
 VALUES(Source.[UserID],Source.[UserLogin],Source.[Password],Source.[PasswordDate],Source.[PasswordSalt],Source.[Q1ID],Source.[Q2ID],Source.[Q3ID],Source.[Answer1],Source.[Answer2],Source.[Answer3],Source.[International],Source.[ResumeFileID],Source.[ResumeDate],Source.[Verified],Source.[VerifiedDate],Source.[Comments],Source.[Deleted],Source.[DeleteDate],Source.[DeleteBy],Source.[DeleteComment],Source.[rowguid],Source.[CreatedBy],Source.[CreatedDate],Source.[ModifiedBy],Source.[ModifiedDate],Source.[account_status_id],Source.[account_status_date],Source.[IsActivated],Source.[LastLoginDate],Source.[IsLockedOut],Source.[LastLockedOutDate],Source.[LastLockedOutReason],Source.[NewPasswordKey],Source.[NewPasswordRequested],Source.[NewEmail],Source.[NewEmailKey],Source.[NewEmailRequested],Source.[OtherFields])
;


DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [User]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[User] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END


SET IDENTITY_INSERT [User] OFF
GO