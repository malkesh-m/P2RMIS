SET IDENTITY_INSERT [WorkflowMechanism] ON

MERGE INTO [WorkflowMechanism] AS Target
USING (VALUES
  (1,2,1741,3,10,'2014-11-17T00:00:00',10,'2014-11-17T00:00:00')
 ,(2,2,1741,4,10,'2014-11-17T00:00:00',10,'2014-11-17T00:00:00')
 ,(3,2,1743,3,10,'2014-11-17T00:00:00',10,'2014-11-17T00:00:00')
 ,(4,2,1749,3,10,'2014-11-17T00:00:00',10,'2014-11-17T00:00:00')
 ,(6,4,1749,4,10,'2014-11-17T00:00:00',10,'2014-11-17T00:00:00')
) AS Source ([WorkflowMechanismId],[WorkflowId],[MechanismId],[ReviewStatusId],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
ON (Target.[WorkflowMechanismId] = Source.[WorkflowMechanismId])
WHEN MATCHED AND (
	NULLIF(Source.[WorkflowId], Target.[WorkflowId]) IS NOT NULL OR NULLIF(Target.[WorkflowId], Source.[WorkflowId]) IS NOT NULL OR 
	NULLIF(Source.[MechanismId], Target.[MechanismId]) IS NOT NULL OR NULLIF(Target.[MechanismId], Source.[MechanismId]) IS NOT NULL OR 
	NULLIF(Source.[ReviewStatusId], Target.[ReviewStatusId]) IS NOT NULL OR NULLIF(Target.[ReviewStatusId], Source.[ReviewStatusId]) IS NOT NULL OR 
	NULLIF(Source.[CreatedBy], Target.[CreatedBy]) IS NOT NULL OR NULLIF(Target.[CreatedBy], Source.[CreatedBy]) IS NOT NULL OR 
	NULLIF(Source.[CreatedDate], Target.[CreatedDate]) IS NOT NULL OR NULLIF(Target.[CreatedDate], Source.[CreatedDate]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedBy], Target.[ModifiedBy]) IS NOT NULL OR NULLIF(Target.[ModifiedBy], Source.[ModifiedBy]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedDate], Target.[ModifiedDate]) IS NOT NULL OR NULLIF(Target.[ModifiedDate], Source.[ModifiedDate]) IS NOT NULL) THEN
 UPDATE SET
  [WorkflowId] = Source.[WorkflowId], 
  [MechanismId] = Source.[MechanismId], 
  [ReviewStatusId] = Source.[ReviewStatusId], 
  [CreatedBy] = Source.[CreatedBy], 
  [CreatedDate] = Source.[CreatedDate], 
  [ModifiedBy] = Source.[ModifiedBy], 
  [ModifiedDate] = Source.[ModifiedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([WorkflowMechanismId],[WorkflowId],[MechanismId],[ReviewStatusId],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
 VALUES(Source.[WorkflowMechanismId],Source.[WorkflowId],Source.[MechanismId],Source.[ReviewStatusId],Source.[CreatedBy],Source.[CreatedDate],Source.[ModifiedBy],Source.[ModifiedDate])
;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [WorkflowMechanism]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[WorkflowMechanism] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [WorkflowMechanism] OFF
GO