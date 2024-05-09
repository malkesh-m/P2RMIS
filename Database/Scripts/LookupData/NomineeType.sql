-- Lookup Nominee type for Consumer Management Module
SET IDENTITY_INSERT [NomineeType] ON
MERGE INTO [NomineeType] AS Target
USING (VALUES
  (1,'Eligible Nominee',1)
 ,(2,'Selected Novice',2)
 ,(3,'Ineligible Nominee',3)) AS Source ([NomineeTypeId],[NomineeType],[SortOrder])
ON (Target.[NomineeTypeId] = Source.[NomineeTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[NomineeType], Target.[NomineeType]) IS NOT NULL OR NULLIF(Target.[NomineeType], Source.[NomineeType]) IS NOT NULL) THEN
 UPDATE SET
  [NomineeType] = Source.[NomineeType]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([NomineeTypeId],[NomineeType],[SortOrder])
 VALUES(Source.[NomineeTypeId],Source.[NomineeType],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [NomineeType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[NomineeType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
SET IDENTITY_INSERT NomineeType OFF
GO
