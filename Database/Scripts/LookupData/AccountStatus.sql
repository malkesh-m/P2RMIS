SET IDENTITY_INSERT [AccountStatus] ON

MERGE INTO [AccountStatus] AS Target
USING (VALUES
  (3,'Active')
 ,(13,'Inactive')
) AS Source ([AccountStatusId],[AccountStatusName])
ON (Target.[AccountStatusId] = Source.[AccountStatusId])
WHEN MATCHED AND (
	NULLIF(Source.[AccountStatusName], Target.[AccountStatusName]) IS NOT NULL OR NULLIF(Target.[AccountStatusName], Source.[AccountStatusName]) IS NOT NULL) THEN
 UPDATE SET
  [AccountStatusName] = Source.[AccountStatusName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([AccountStatusId],[AccountStatusName])
 VALUES(Source.[AccountStatusId],Source.[AccountStatusName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [AccountStatus]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[AccountStatus] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [AccountStatus] OFF
GO