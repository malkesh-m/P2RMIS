SET IDENTITY_INSERT [SummaryReviewerDescription] ON

MERGE INTO [SummaryReviewerDescription] AS Target
USING (VALUES
  (1,NULL,NULL,NULL,1,1,'Scientist Reviewer A')
 ,(2,NULL,NULL,NULL,2,3,'Consumer Reviewer')
 ,(3,NULL,NULL,NULL,3,2,'Scientist Reviewer B')
 ,(4,NULL,NULL,NULL,4,4,'Scientist Reviewer C')
 ,(5,NULL,NULL,NULL,5,5,'Scientist Reviewer D')
 ,(6,NULL,NULL,NULL,6,6,'Scientist Reviewer E')
) AS Source ([SummaryReviewerDescriptionId],[ProgramMechanismId],[ClientParticipantTypeId],[ClientRoleId],[AssignmentOrder],[CustomOrder],[DisplayName])
ON (Target.[SummaryReviewerDescriptionId] = Source.[SummaryReviewerDescriptionId])
WHEN MATCHED AND (
	NULLIF(Source.[ProgramMechanismId], Target.[ProgramMechanismId]) IS NOT NULL OR NULLIF(Target.[ProgramMechanismId], Source.[ProgramMechanismId]) IS NOT NULL OR 
	NULLIF(Source.[ClientParticipantTypeId], Target.[ClientParticipantTypeId]) IS NOT NULL OR NULLIF(Target.[ClientParticipantTypeId], Source.[ClientParticipantTypeId]) IS NOT NULL OR 
	NULLIF(Source.[ClientRoleId], Target.[ClientRoleId]) IS NOT NULL OR NULLIF(Target.[ClientRoleId], Source.[ClientRoleId]) IS NOT NULL OR 
	NULLIF(Source.[AssignmentOrder], Target.[AssignmentOrder]) IS NOT NULL OR NULLIF(Target.[AssignmentOrder], Source.[AssignmentOrder]) IS NOT NULL OR 
	NULLIF(Source.[CustomOrder], Target.[CustomOrder]) IS NOT NULL OR NULLIF(Target.[CustomOrder], Source.[CustomOrder]) IS NOT NULL OR 
	NULLIF(Source.[DisplayName], Target.[DisplayName]) IS NOT NULL OR NULLIF(Target.[DisplayName], Source.[DisplayName]) IS NOT NULL) THEN
 UPDATE SET
  [ProgramMechanismId] = Source.[ProgramMechanismId], 
  [ClientParticipantTypeId] = Source.[ClientParticipantTypeId], 
  [ClientRoleId] = Source.[ClientRoleId], 
  [AssignmentOrder] = Source.[AssignmentOrder], 
  [CustomOrder] = Source.[CustomOrder], 
  [DisplayName] = Source.[DisplayName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([SummaryReviewerDescriptionId],[ProgramMechanismId],[ClientParticipantTypeId],[ClientRoleId],[AssignmentOrder],[CustomOrder],[DisplayName])
 VALUES(Source.[SummaryReviewerDescriptionId],Source.[ProgramMechanismId],Source.[ClientParticipantTypeId],Source.[ClientRoleId],Source.[AssignmentOrder],Source.[CustomOrder],Source.[DisplayName])
;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [SummaryReviewerDescription]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[SummaryReviewerDescription] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [SummaryReviewerDescription] OFF
GO