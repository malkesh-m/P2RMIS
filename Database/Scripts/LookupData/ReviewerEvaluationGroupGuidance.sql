SET IDENTITY_INSERT [ReviewerEvaluationGroupGuidance] ON

MERGE INTO [ReviewerEvaluationGroupGuidance] AS Target
USING (VALUES
  (1,1,1,'Reviewer failed in multiple ways. Should not be invited back.')
 ,(2,1,2,'Reviewer has potential but required substantial coaching and/or reminders to complete their tasks acceptably. Reviewer should only be invited back if there is a likelihood of substantial improvement.')
 ,(3,1,3,'Reviewer performed acceptably in all respects but required coaching and/or reminders.')
 ,(4,1,4,'Reviewer performed well in all respects and required only limited coaching and/or reminders.')
 ,(5,1,5,'Reviewer performed in exemplary fashion in all respects. Did not require reminders or any appreciable coaching. Would definitely like to have this reviewer return to future panels.')
 ,(6,2,1,'Chair failed in multiple ways and should not be invited back in any capacity.')
 ,(7,2,2,'Chair has the knowledge base to be invited as a reviewer but not as a chair.')
 ,(8,2,3,'Chair performed all tasks acceptably.')
 ,(9,2,4,'Chair performed to a high standard.')
 ,(10,2,5,'Chair was outstanding.')
 ,(11,3,1,'Reviewer failed in multiple ways. Should not be invited back.')
 ,(12,3,2,'Reviewer has potential but required substantial coaching and/or reminders to complete their tasks acceptably. Reviewer should only be invited back if there is a likelihood of substantial improvement.')
 ,(13,3,3,'Reviewer performed acceptably in all respects but required coaching and/or reminders.')
 ,(14,3,4,'Reviewer performed well in all respects and required only limited coaching and/or reminders.')
 ,(15,3,5,'Reviewer performed in exemplary fashion in all respects. Did not require reminders or any appreciable coaching. Would definitely like to have this reviewer return to future panels.')
 ,(16,4,1,'Chair failed in multiple ways and should not be invited back in any capacity.')
 ,(17,4,2,'Chair has the knowledge base to be invited as a reviewer but not as a chair.')
 ,(18,4,3,'Chair performed all tasks acceptably.')
 ,(19,4,4,'Chair performed to a high standard.')
 ,(20,4,5,'Chair was outstanding.')
) AS Source ([ReviewerEvaluationGroupGuidanceId],[ClientReviewerEvaluationGroupId],[Rating],[RatingDescription])
ON (Target.[ReviewerEvaluationGroupGuidanceId] = Source.[ReviewerEvaluationGroupGuidanceId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientReviewerEvaluationGroupId], Target.[ClientReviewerEvaluationGroupId]) IS NOT NULL OR NULLIF(Target.[ClientReviewerEvaluationGroupId], Source.[ClientReviewerEvaluationGroupId]) IS NOT NULL OR 
	NULLIF(Source.[Rating], Target.[Rating]) IS NOT NULL OR NULLIF(Target.[Rating], Source.[Rating]) IS NOT NULL OR 
	NULLIF(Source.[RatingDescription], Target.[RatingDescription]) IS NOT NULL OR NULLIF(Target.[RatingDescription], Source.[RatingDescription]) IS NOT NULL) THEN
 UPDATE SET
  [ClientReviewerEvaluationGroupId] = Source.[ClientReviewerEvaluationGroupId], 
  [Rating] = Source.[Rating], 
  [RatingDescription] = Source.[RatingDescription]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ReviewerEvaluationGroupGuidanceId],[ClientReviewerEvaluationGroupId],[Rating],[RatingDescription])
 VALUES(Source.[ReviewerEvaluationGroupGuidanceId],Source.[ClientReviewerEvaluationGroupId],Source.[Rating],Source.[RatingDescription])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ReviewerEvaluationGroupGuidance]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ReviewerEvaluationGroupGuidance] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ReviewerEvaluationGroupGuidance] OFF
GO