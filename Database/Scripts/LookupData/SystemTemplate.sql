SET IDENTITY_INSERT [SystemTemplate] ON

MERGE INTO [SystemTemplate] AS Target
USING (VALUES
  (1,'P2RMIS Client Access - New User',1,1,1)
 ,(2,'P2RMIS Client Access - New User (PW)',1,1,1)
 ,(3,'P2RMIS Client Access - Reset User Account',1,1,1)
 ,(4,'P2RMIS Client Access - Reset User Account (PW)',1,1,1)
 ,(5,'P2RMIS Application Transfer Request',1,1,1)
 ,(6,'P2RMIS Reviewer Transfer Request',1,1,1)
 ,(7,'P2RMIS Reviewer Assignment Notification',1,1,1)
 ,(8,'P2RMIS Discussion Board Comment Notification',1,1,1)
 ,(10,'P2RMIS Reviewer Release Request',1,1,1)
 ,(11,'P2RMIS Critique Reset',1,1,1)
 ,(12,'P2RMIS Ticket Request Confirmation',1,1,1)
 ,(1012,'P2RMIS Online Discussion started',1,1,1)
 ,(1013,'P2RMIS Client Access - User Password Change Notification',1,1,1)
) AS Source ([TemplateId],[Name],[TypeID],[VersionId],[CategoryID])
ON (Target.[TemplateId] = Source.[TemplateId])
WHEN MATCHED AND (
	NULLIF(Source.[Name], Target.[Name]) IS NOT NULL OR NULLIF(Target.[Name], Source.[Name]) IS NOT NULL OR 
	NULLIF(Source.[TypeID], Target.[TypeID]) IS NOT NULL OR NULLIF(Target.[TypeID], Source.[TypeID]) IS NOT NULL OR 
	NULLIF(Source.[VersionId], Target.[VersionId]) IS NOT NULL OR NULLIF(Target.[VersionId], Source.[VersionId]) IS NOT NULL OR 
	NULLIF(Source.[CategoryID], Target.[CategoryID]) IS NOT NULL OR NULLIF(Target.[CategoryID], Source.[CategoryID]) IS NOT NULL) THEN
 UPDATE SET
  [Name] = Source.[Name], 
  [TypeID] = Source.[TypeID], 
  [VersionId] = Source.[VersionId], 
  [CategoryID] = Source.[CategoryID]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([TemplateId],[Name],[TypeID],[VersionId],[CategoryID])
 VALUES(Source.[TemplateId],Source.[Name],Source.[TypeID],Source.[VersionId],Source.[CategoryID])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [SystemTemplate]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[SystemTemplate] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [SystemTemplate] OFF
GO