SET IDENTITY_INSERT [UserSystemRole] ON
 
MERGE INTO [UserSystemRole] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  (10,10,10,NULL,10,'2012-08-27T00:00:00',NULL,NULL)
 ,(13,4,4,NULL,10,'2012-08-27T00:00:00',NULL,NULL)
 ,(17,8,8,NULL,10,'2012-08-27T00:00:00',NULL,NULL)

) AS Source ([UserSystemRoleID],[UserID],[RoleLkpID],[rowguid],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
ON (Target.[UserSystemRoleID] = Source.[UserSystemRoleID])
WHEN MATCHED AND (Target.[UserID] <> Source.[UserID] OR Target.[RoleLkpID] <> Source.[RoleLkpID] OR Target.[rowguid] <> Source.[rowguid] OR Target.[CreatedBy] <> Source.[CreatedBy] OR Target.[CreatedDate] <> Source.[CreatedDate] OR Target.[ModifiedBy] <> Source.[ModifiedBy] OR Target.[ModifiedDate] <> Source.[ModifiedDate]) THEN
 UPDATE SET
 [UserID] = Source.[UserID], 
[RoleLkpID] = Source.[RoleLkpID], 
[rowguid] = Source.[rowguid], 
[CreatedBy] = Source.[CreatedBy], 
[CreatedDate] = Source.[CreatedDate], 
[ModifiedBy] = Source.[ModifiedBy], 
[ModifiedDate] = Source.[ModifiedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([UserSystemRoleID],[UserID],[RoleLkpID],[rowguid],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
 VALUES(Source.[UserSystemRoleID],Source.[UserID],Source.[RoleLkpID],Source.[rowguid],Source.[CreatedBy],Source.[CreatedDate],Source.[ModifiedBy],Source.[ModifiedDate])
;
 

DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [UserSystemRole]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[UserSystemRole] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END

 
SET IDENTITY_INSERT [UserSystemRole] OFF

SET NOCOUNT OFF
GO