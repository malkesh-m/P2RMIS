SET IDENTITY_INSERT [ClientReviewerEvaluationGroup] ON

MERGE INTO [ClientReviewerEvaluationGroup] AS Target
USING (VALUES
  (1,19,'Reviewers')
 ,(2,19,'Chair')
 ,(3,23,'Reviewers')
 ,(4,23,'Chair')
) AS Source ([ClientReviewerEvaluationGroupId],[ClientId],[ReviewerEvaluationGroupName])
ON (Target.[ClientReviewerEvaluationGroupId] = Source.[ClientReviewerEvaluationGroupId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientId], Target.[ClientId]) IS NOT NULL OR NULLIF(Target.[ClientId], Source.[ClientId]) IS NOT NULL OR 
	NULLIF(Source.[ReviewerEvaluationGroupName], Target.[ReviewerEvaluationGroupName]) IS NOT NULL OR NULLIF(Target.[ReviewerEvaluationGroupName], Source.[ReviewerEvaluationGroupName]) IS NOT NULL) THEN
 UPDATE SET
  [ClientId] = Source.[ClientId], 
  [ReviewerEvaluationGroupName] = Source.[ReviewerEvaluationGroupName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientReviewerEvaluationGroupId],[ClientId],[ReviewerEvaluationGroupName])
 VALUES(Source.[ClientReviewerEvaluationGroupId],Source.[ClientId],Source.[ReviewerEvaluationGroupName])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientReviewerEvaluationGroup]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientReviewerEvaluationGroup] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientReviewerEvaluationGroup] OFF
GO