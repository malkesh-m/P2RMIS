-- Lookup Nominee affectect for Consumer Management Module
SET IDENTITY_INSERT NomineeAffected ON
MERGE INTO [NomineeAffected] AS Target
USING (VALUES
  (1,'Self',1)
 ,(2,'Self & Family',2)
 ,(3,'Family',3)
 ,(4,'Caregiver',4)
 ,(5,'Practitioner',5)) AS Source ([NomineeAffectedId],[NomineeAffected],[SortOrder])
ON (Target.[NomineeAffectedId] = Source.[NomineeAffectedId])
WHEN MATCHED AND (
	NULLIF(Source.[NomineeAffected], Target.[NomineeAffected]) IS NOT NULL OR NULLIF(Target.[NomineeAffected], Source.[NomineeAffected]) IS NOT NULL) THEN
 UPDATE SET
  [NomineeAffected] = Source.[NomineeAffected]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([NomineeAffectedId],[NomineeAffected],[SortOrder])
 VALUES(Source.[NomineeAffectedId],Source.[NomineeAffected],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [NomineeAffected]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[NomineeAffected] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
SET IDENTITY_INSERT NomineeAffected OFF
GO
