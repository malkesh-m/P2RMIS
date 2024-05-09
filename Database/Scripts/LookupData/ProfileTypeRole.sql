-- Profile type to system role correlation lookup table
SET IDENTITY_INSERT [ProfileTypeRole] ON

MERGE INTO [ProfileTypeRole] AS Target
USING (VALUES
  (1,1,12)
 ,(2,2,12)
 ,(4,3,10)
 ,(5,3,11)
 ,(6,3,20)
 ,(7,3,22)
 ,(8,3,23)
 ,(9,3,24)
 ,(10,4,4)
 ,(11,5,NULL)
 ,(12,3,25)
 ,(13,3,26)
 ,(14,3,27)
 ,(15,3,28)
 ,(16,4,29)
 ,(17,1,30)
 ,(18,2,30)
 ,(19,3,31)
 ,(20,4,32)
) AS Source ([ProfileTypeRoleId],[ProfileTypeId],[SystemRoleId])
ON (Target.[ProfileTypeRoleId] = Source.[ProfileTypeRoleId])
WHEN MATCHED AND (
	NULLIF(Source.[ProfileTypeId], Target.[ProfileTypeId]) IS NOT NULL OR NULLIF(Target.[ProfileTypeId], Source.[ProfileTypeId]) IS NOT NULL OR 
	NULLIF(Source.[SystemRoleId], Target.[SystemRoleId]) IS NOT NULL OR NULLIF(Target.[SystemRoleId], Source.[SystemRoleId]) IS NOT NULL) THEN
 UPDATE SET
  [ProfileTypeId] = Source.[ProfileTypeId], 
  [SystemRoleId] = Source.[SystemRoleId]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ProfileTypeRoleId],[ProfileTypeId],[SystemRoleId])
 VALUES(Source.[ProfileTypeRoleId],Source.[ProfileTypeId],Source.[SystemRoleId])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ProfileTypeRole]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ProfileTypeRole] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ProfileTypeRole] OFF
GO