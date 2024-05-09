SET IDENTITY_INSERT [ScoringTemplatePhase] ON

MERGE INTO [ScoringTemplatePhase] AS [Target]
USING (VALUES
  (1,1,5,1,3,2)
 ,(2,1,6,2,3,2)
 ,(3,1,7,3,3,2)
 ,(4,1,8,4,1,2)
 ,(5,2,5,1,1,2)
 ,(6,2,6,2,1,2)
 ,(7,2,7,3,1,2)
 ,(8,3,5,1,146,147)
 ,(9,3,6,2,146,147)
 ,(10,3,7,3,146,147)
 ,(11,3,8,4,146,147)
 ,(12,4,5,1,164,164)
 ,(13,4,6,2,164,164)
 ,(14,4,7,3,164,164)
 ,(16,5,5,1,6,7)
 ,(17,5,6,2,6,7)
 ,(18,5,7,3,6,7)
 ,(19,6,5,1,164,165)
 ,(20,6,6,2,164,165)
 ,(21,6,7,3,164,165)
) AS [Source] ([ScoringTemplatePhaseId],[ScoringTemplateId],[StepTypeId],[StepOrder],[OverallClientScoringId],[CriteriaClientScoringId])
ON ([Target].[ScoringTemplatePhaseId] = [Source].[ScoringTemplatePhaseId])
WHEN MATCHED AND (
	NULLIF([Source].[ScoringTemplateId], [Target].[ScoringTemplateId]) IS NOT NULL OR NULLIF([Target].[ScoringTemplateId], [Source].[ScoringTemplateId]) IS NOT NULL OR 
	NULLIF([Source].[StepTypeId], [Target].[StepTypeId]) IS NOT NULL OR NULLIF([Target].[StepTypeId], [Source].[StepTypeId]) IS NOT NULL OR 
	NULLIF([Source].[StepOrder], [Target].[StepOrder]) IS NOT NULL OR NULLIF([Target].[StepOrder], [Source].[StepOrder]) IS NOT NULL OR 
	NULLIF([Source].[OverallClientScoringId], [Target].[OverallClientScoringId]) IS NOT NULL OR NULLIF([Target].[OverallClientScoringId], [Source].[OverallClientScoringId]) IS NOT NULL OR 
	NULLIF([Source].[CriteriaClientScoringId], [Target].[CriteriaClientScoringId]) IS NOT NULL OR NULLIF([Target].[CriteriaClientScoringId], [Source].[CriteriaClientScoringId]) IS NOT NULL) THEN
 UPDATE SET
  [Target].[ScoringTemplateId] = [Source].[ScoringTemplateId], 
  [Target].[StepTypeId] = [Source].[StepTypeId], 
  [Target].[StepOrder] = [Source].[StepOrder], 
  [Target].[OverallClientScoringId] = [Source].[OverallClientScoringId], 
  [Target].[CriteriaClientScoringId] = [Source].[CriteriaClientScoringId]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ScoringTemplatePhaseId],[ScoringTemplateId],[StepTypeId],[StepOrder],[OverallClientScoringId],[CriteriaClientScoringId])
 VALUES([Source].[ScoringTemplatePhaseId],[Source].[ScoringTemplateId],[Source].[StepTypeId],[Source].[StepOrder],[Source].[OverallClientScoringId],[Source].[CriteriaClientScoringId]);

DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ScoringTemplatePhase]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ScoringTemplatePhase] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO



SET IDENTITY_INSERT [ScoringTemplatePhase] OFF
GO