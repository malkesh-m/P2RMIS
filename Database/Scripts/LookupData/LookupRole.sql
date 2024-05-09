SET IDENTITY_INSERT [LookupRole] ON
 
MERGE INTO [LookupRole] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
 (4,'Client','system','Client',5)
 ,(8,'Staff','system','Staff',3)
 ,(10,'Webmaster','system','Webmaster',1)
 ,(11,'SRO','system','SRO',4)
 ,(12,'Reviewer','system','Reviewer',6)


) AS Source ([RoleID],[RoleName],[RoleContext],[RoleCode],[PriorityOrder])
ON (Target.[RoleID] = Source.[RoleID])
WHEN MATCHED AND (Target.[RoleName] <> Source.[RoleName] OR Target.[RoleContext] <> Source.[RoleContext] OR Target.[RoleCode] <> Source.[RoleCode] OR Target.[PriorityOrder] <> Source.[PriorityOrder]) THEN
 UPDATE SET
 [RoleName] = Source.[RoleName], 
[RoleContext] = Source.[RoleContext], 
[RoleCode] = Source.[RoleCode], 
[PriorityOrder] = Source.[PriorityOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([RoleID],[RoleName],[RoleContext],[RoleCode],[PriorityOrder])
 VALUES(Source.[RoleID],Source.[RoleName],Source.[RoleContext],Source.[RoleCode],Source.[PriorityOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
 
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [LookupRole]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[LookupRole] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
 
SET IDENTITY_INSERT [LookupRole] OFF
GO