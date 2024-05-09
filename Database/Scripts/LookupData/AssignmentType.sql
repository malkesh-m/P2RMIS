MERGE INTO [AssignmentType] AS Target
USING (VALUES
  (1,'Writer',NULL)
 ,(2,'Editor',NULL)
 ,(3,'Scientific Review Manager',NULL)
 ,(4,'Client',NULL)
 ,(5,'Scientist Reviewer',1)
 ,(6,'Consumer Reviewer',5)
 ,(7,'Reader',11)
 ,(8,'COI',9)
 ,(9,'Specialist Reviewer',NULL)
) AS Source ([AssignmentTypeId],[AssignmentLabel],[LegacyAssignmentId])
ON (Target.[AssignmentTypeId] = Source.[AssignmentTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[AssignmentLabel], Target.[AssignmentLabel]) IS NOT NULL OR NULLIF(Target.[AssignmentLabel], Source.[AssignmentLabel]) IS NOT NULL OR 
	NULLIF(Source.[LegacyAssignmentId], Target.[LegacyAssignmentId]) IS NOT NULL OR NULLIF(Target.[LegacyAssignmentId], Source.[LegacyAssignmentId]) IS NOT NULL) THEN
 UPDATE SET
  [AssignmentLabel] = Source.[AssignmentLabel], 
  [LegacyAssignmentId] = Source.[LegacyAssignmentId]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([AssignmentTypeId],[AssignmentLabel],[LegacyAssignmentId])
 VALUES(Source.[AssignmentTypeId],Source.[AssignmentLabel],Source.[LegacyAssignmentId])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [AssignmentType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[AssignmentType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO