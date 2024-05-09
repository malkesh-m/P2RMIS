MERGE INTO [AccountStatusReason] AS Target
USING (VALUES
  (1,'Awaiting Credentials',13)
 ,(2,'Temp PWD',3)
 ,(3,'Locked',13)
 ,(4,'PWD Expired',13)
 ,(5,'Inactivity',13)
 ,(6,'Ineligible',13)
 ,(7,'Account Closed',13)
 ,(8,'Perm Credentials',3)
 ,(9,'Legacy Credentials',3)
) AS Source ([AccountStatusReasonId],[AccountStatusReasonName],[AccountStatusId])
ON (Target.[AccountStatusReasonId] = Source.[AccountStatusReasonId])
WHEN MATCHED AND (
	NULLIF(Source.[AccountStatusReasonName], Target.[AccountStatusReasonName]) IS NOT NULL OR NULLIF(Target.[AccountStatusReasonName], Source.[AccountStatusReasonName]) IS NOT NULL OR 
	NULLIF(Source.[AccountStatusId], Target.[AccountStatusId]) IS NOT NULL OR NULLIF(Target.[AccountStatusId], Source.[AccountStatusId]) IS NOT NULL) THEN
 UPDATE SET
  [AccountStatusReasonName] = Source.[AccountStatusReasonName], 
  [AccountStatusId] = Source.[AccountStatusId]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([AccountStatusReasonId],[AccountStatusReasonName],[AccountStatusId])
 VALUES(Source.[AccountStatusReasonId],Source.[AccountStatusReasonName],Source.[AccountStatusId])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [AccountStatusReason]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[AccountStatusReason] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO