SET IDENTITY_INSERT [SystemRole] ON

MERGE INTO [SystemRole] AS Target
USING (VALUES
  (4,'Client','system','Client',8,1)
 ,(8,'Staff','system','Staff',6,12)
 ,(10,'Webmaster','system','Webmaster',1,14)
 ,(11,'SRO','system','SRO',7,11)
 ,(12,'Reviewer','system','Reviewer',9,8)
 ,(20,'Editor','system','Editor',7,5)
 ,(22,'RTA','system','RTA',6,9)
 ,(23,'Editing Manager','system','EditingManager',5,4)
 ,(24,'SRM','system','SRM',3,10)
 ,(25,'PM','system','PM',3,7)
 ,(26,'CRA','system','CRA',4,3)
 ,(27,'Helpdesk','system','Helpdesk',3,6)
 ,(28,'Task Lead','system','TaskLead',4,13)
 ,(29,'CPRIT Client','system','CpritClient',8,2)
 ,(30,'CPRIT Chair','system','CpritChair',9,15)
 ,(31,'Meeting Planner','system','MeetingPlanner',4,16)
 ,(32,'Security Administrator','system','SecurityAdmin',2,17)
) AS Source ([SystemRoleId],[SystemRoleName],[SystemRoleContext],[SystemRoleCode],[SystemPriorityOrder],[SortOrder])
ON (Target.[SystemRoleId] = Source.[SystemRoleId])
WHEN MATCHED AND (
	NULLIF(Source.[SystemRoleName], Target.[SystemRoleName]) IS NOT NULL OR NULLIF(Target.[SystemRoleName], Source.[SystemRoleName]) IS NOT NULL OR 
	NULLIF(Source.[SystemRoleContext], Target.[SystemRoleContext]) IS NOT NULL OR NULLIF(Target.[SystemRoleContext], Source.[SystemRoleContext]) IS NOT NULL OR 
	NULLIF(Source.[SystemRoleCode], Target.[SystemRoleCode]) IS NOT NULL OR NULLIF(Target.[SystemRoleCode], Source.[SystemRoleCode]) IS NOT NULL OR 
	NULLIF(Source.[SystemPriorityOrder], Target.[SystemPriorityOrder]) IS NOT NULL OR NULLIF(Target.[SystemPriorityOrder], Source.[SystemPriorityOrder]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [SystemRoleName] = Source.[SystemRoleName], 
  [SystemRoleContext] = Source.[SystemRoleContext], 
  [SystemRoleCode] = Source.[SystemRoleCode], 
  [SystemPriorityOrder] = Source.[SystemPriorityOrder], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([SystemRoleId],[SystemRoleName],[SystemRoleContext],[SystemRoleCode],[SystemPriorityOrder],[SortOrder])
 VALUES(Source.[SystemRoleId],Source.[SystemRoleName],Source.[SystemRoleContext],Source.[SystemRoleCode],Source.[SystemPriorityOrder],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [SystemRole]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[SystemRole] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [SystemRole] OFF
GO