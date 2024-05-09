SET IDENTITY_INSERT [ClientDefaultWorkflow] ON

MERGE INTO [ClientDefaultWorkflow] AS Target
USING (VALUES
  (1,19,2,NULL)
 ,(2,19,3,3)
 ,(3,19,2,4)
 ,(4,23,2,NULL)
 ,(5,23,3,3)
 ,(6,23,2,4)
 ,(7,9,75,NULL)
 ,(8,9,75,3)
 ,(9,9,75,4)
) AS Source ([ClientDefaultWorkflowId],[ClientId],[WorkflowId],[ReviewStatusId])
ON (Target.[ClientDefaultWorkflowId] = Source.[ClientDefaultWorkflowId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientId], Target.[ClientId]) IS NOT NULL OR NULLIF(Target.[ClientId], Source.[ClientId]) IS NOT NULL OR 
	NULLIF(Source.[WorkflowId], Target.[WorkflowId]) IS NOT NULL OR NULLIF(Target.[WorkflowId], Source.[WorkflowId]) IS NOT NULL OR 
	NULLIF(Source.[ReviewStatusId], Target.[ReviewStatusId]) IS NOT NULL OR NULLIF(Target.[ReviewStatusId], Source.[ReviewStatusId]) IS NOT NULL) THEN
 UPDATE SET
  [ClientId] = Source.[ClientId], 
  [WorkflowId] = Source.[WorkflowId], 
  [ReviewStatusId] = Source.[ReviewStatusId]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientDefaultWorkflowId],[ClientId],[WorkflowId],[ReviewStatusId])
 VALUES(Source.[ClientDefaultWorkflowId],Source.[ClientId],Source.[WorkflowId],Source.[ReviewStatusId])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientDefaultWorkflow]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientDefaultWorkflow] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientDefaultWorkflow] OFF
GO