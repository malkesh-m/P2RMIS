MERGE INTO [ComplianceStatus] AS Target
USING (VALUES
  (1,'Admin Review')
 ,(2,'Compliance Review')
 ,(3,'Compliant')
 ,(4,'Compliant Modified')
 ,(5,'Files Out of Order')
 ,(6,'No Selection')
 ,(7,'Non-Compliant')
 ,(8,'Scientific Review')
) AS Source ([ComplianceStatusId],[ComplianceStatusLabel])
ON (Target.[ComplianceStatusId] = Source.[ComplianceStatusId])
WHEN MATCHED AND (
	NULLIF(Source.[ComplianceStatusLabel], Target.[ComplianceStatusLabel]) IS NOT NULL OR NULLIF(Target.[ComplianceStatusLabel], Source.[ComplianceStatusLabel]) IS NOT NULL) THEN
 UPDATE SET
  [ComplianceStatusLabel] = Source.[ComplianceStatusLabel]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ComplianceStatusId],[ComplianceStatusLabel])
 VALUES(Source.[ComplianceStatusId],Source.[ComplianceStatusLabel])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ComplianceStatus]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ComplianceStatus] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO