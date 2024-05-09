MERGE INTO [ReviewStatus] AS Target
USING (VALUES
  (1,1,'Not Discussed')
 ,(2,1,'Ready to Score')
 ,(3,2,'Command Draft')
 ,(4,2,'Qualifying')
 ,(5,1,'Disapproved')
 ,(6,1,'Scored')
 ,(7,1,'Scoring')
 ,(8,1,'Active')
) AS Source ([ReviewStatusId],[ReviewStatusTypeId],[ReviewStatusLabel])
ON (Target.[ReviewStatusId] = Source.[ReviewStatusId])
WHEN MATCHED AND (
	NULLIF(Source.[ReviewStatusTypeId], Target.[ReviewStatusTypeId]) IS NOT NULL OR NULLIF(Target.[ReviewStatusTypeId], Source.[ReviewStatusTypeId]) IS NOT NULL OR 
	NULLIF(Source.[ReviewStatusLabel], Target.[ReviewStatusLabel]) IS NOT NULL OR NULLIF(Target.[ReviewStatusLabel], Source.[ReviewStatusLabel]) IS NOT NULL) THEN
 UPDATE SET
  [ReviewStatusTypeId] = Source.[ReviewStatusTypeId], 
  [ReviewStatusLabel] = Source.[ReviewStatusLabel]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ReviewStatusId],[ReviewStatusTypeId],[ReviewStatusLabel])
 VALUES(Source.[ReviewStatusId],Source.[ReviewStatusTypeId],Source.[ReviewStatusLabel])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ReviewStatus]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ReviewStatus] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO