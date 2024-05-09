SET IDENTITY_INSERT [UserInfoChangeLog] ON

MERGE INTO [UserInfoChangeLog] AS Target
USING (VALUES
  (1,10,9,'CA','MD',116087,0,NULL,NULL,10,'2015-12-16T23:17:00',10,'2015-12-16T00:00:00',0,NULL,NULL)
 ,(2,10,12,'Lorem','Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',10,0,NULL,NULL,10,'2015-12-16T08:15:00',10,'2015-12-16T00:00:00',0,NULL,NULL)
 ,(3,4,2,'Richard','Rich',4,1,'2015-12-16T04:00:00',10,8,'2015-12-16T00:00:00',8,'2015-12-16T00:00:00',0,NULL,NULL)
 ,(4,8,4,'ch@mail.mil','ch@amedd.mail.mil',20,0,NULL,NULL,4,'2015-12-16T05:31:00',4,'2015-12-16T00:00:00',0,NULL,NULL)
 ,(5,8,10,'Missing','Present',200,0,NULL,NULL,4,'2015-12-17T06:30:00',4,'2015-12-16T00:00:00',1,4,'2015-12-16T00:00:00')
 ,(6,8,13,'ID','NH',116086,0,NULL,NULL,4,'2015-12-16T00:00:00',4,'2015-12-16T00:00:00',0,NULL,NULL)
 ,(7,8,3,'Jr.','Ph.D., M.D.',8,1,'2015-12-16T04:00:00',10,4,'2015-12-16T00:00:03',4,'2015-12-16T00:00:00',0,NULL,NULL)
) AS Source ([UserInfoChangeLogId],[UserInfoId],[UserInfoChangeTypeId],[OldValue],[NewValue],[Identifier],[ReviewedFlag],[ReviewedDate],[ReviewedBy],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate],[DeletedFlag],[DeletedBy],[DeletedDate])
ON (Target.[UserInfoChangeLogId] = Source.[UserInfoChangeLogId])
WHEN MATCHED AND (
	NULLIF(Source.[UserInfoId], Target.[UserInfoId]) IS NOT NULL OR NULLIF(Target.[UserInfoId], Source.[UserInfoId]) IS NOT NULL OR 
	NULLIF(Source.[UserInfoChangeTypeId], Target.[UserInfoChangeTypeId]) IS NOT NULL OR NULLIF(Target.[UserInfoChangeTypeId], Source.[UserInfoChangeTypeId]) IS NOT NULL OR 
	NULLIF(Source.[OldValue], Target.[OldValue]) IS NOT NULL OR NULLIF(Target.[OldValue], Source.[OldValue]) IS NOT NULL OR 
	NULLIF(Source.[NewValue], Target.[NewValue]) IS NOT NULL OR NULLIF(Target.[NewValue], Source.[NewValue]) IS NOT NULL OR 
	NULLIF(Source.[Identifier], Target.[Identifier]) IS NOT NULL OR NULLIF(Target.[Identifier], Source.[Identifier]) IS NOT NULL OR 
	NULLIF(Source.[ReviewedFlag], Target.[ReviewedFlag]) IS NOT NULL OR NULLIF(Target.[ReviewedFlag], Source.[ReviewedFlag]) IS NOT NULL OR 
	NULLIF(Source.[ReviewedDate], Target.[ReviewedDate]) IS NOT NULL OR NULLIF(Target.[ReviewedDate], Source.[ReviewedDate]) IS NOT NULL OR 
	NULLIF(Source.[ReviewedBy], Target.[ReviewedBy]) IS NOT NULL OR NULLIF(Target.[ReviewedBy], Source.[ReviewedBy]) IS NOT NULL OR 
	NULLIF(Source.[CreatedBy], Target.[CreatedBy]) IS NOT NULL OR NULLIF(Target.[CreatedBy], Source.[CreatedBy]) IS NOT NULL OR 
	NULLIF(Source.[CreatedDate], Target.[CreatedDate]) IS NOT NULL OR NULLIF(Target.[CreatedDate], Source.[CreatedDate]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedBy], Target.[ModifiedBy]) IS NOT NULL OR NULLIF(Target.[ModifiedBy], Source.[ModifiedBy]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedDate], Target.[ModifiedDate]) IS NOT NULL OR NULLIF(Target.[ModifiedDate], Source.[ModifiedDate]) IS NOT NULL OR 
	NULLIF(Source.[DeletedFlag], Target.[DeletedFlag]) IS NOT NULL OR NULLIF(Target.[DeletedFlag], Source.[DeletedFlag]) IS NOT NULL OR 
	NULLIF(Source.[DeletedBy], Target.[DeletedBy]) IS NOT NULL OR NULLIF(Target.[DeletedBy], Source.[DeletedBy]) IS NOT NULL OR 
	NULLIF(Source.[DeletedDate], Target.[DeletedDate]) IS NOT NULL OR NULLIF(Target.[DeletedDate], Source.[DeletedDate]) IS NOT NULL) THEN
 UPDATE SET
  [UserInfoId] = Source.[UserInfoId], 
  [UserInfoChangeTypeId] = Source.[UserInfoChangeTypeId], 
  [OldValue] = Source.[OldValue], 
  [NewValue] = Source.[NewValue], 
  [Identifier] = Source.[Identifier], 
  [ReviewedFlag] = Source.[ReviewedFlag], 
  [ReviewedDate] = Source.[ReviewedDate], 
  [ReviewedBy] = Source.[ReviewedBy], 
  [CreatedBy] = Source.[CreatedBy], 
  [CreatedDate] = Source.[CreatedDate], 
  [ModifiedBy] = Source.[ModifiedBy], 
  [ModifiedDate] = Source.[ModifiedDate], 
  [DeletedFlag] = Source.[DeletedFlag], 
  [DeletedBy] = Source.[DeletedBy], 
  [DeletedDate] = Source.[DeletedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([UserInfoChangeLogId],[UserInfoId],[UserInfoChangeTypeId],[OldValue],[NewValue],[Identifier],[ReviewedFlag],[ReviewedDate],[ReviewedBy],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate],[DeletedFlag],[DeletedBy],[DeletedDate])
 VALUES(Source.[UserInfoChangeLogId],Source.[UserInfoId],Source.[UserInfoChangeTypeId],Source.[OldValue],Source.[NewValue],Source.[Identifier],Source.[ReviewedFlag],Source.[ReviewedDate],Source.[ReviewedBy],Source.[CreatedBy],Source.[CreatedDate],Source.[ModifiedBy],Source.[ModifiedDate],Source.[DeletedFlag],Source.[DeletedBy],Source.[DeletedDate])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [UserInfoChangeLog]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[UserInfoChangeLog] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [UserInfoChangeLog] OFF
GO