SET IDENTITY_INSERT [MechanismTemplate] ON

MERGE INTO [MechanismTemplate] AS Target
USING (VALUES
  (2,NULL,1741,2,10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(3,NULL,1741,1,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(4,NULL,1743,2,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(5,NULL,1743,1,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(6,NULL,1749,2,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(7,NULL,1749,1,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(8,NULL,1751,2,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(9,NULL,1751,1,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
) AS Source ([MechanismTemplateId],[ProgramMechanismId],[MechanismId],[ReviewStatusId],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
ON (Target.[MechanismTemplateId] = Source.[MechanismTemplateId])
WHEN MATCHED AND (
	NULLIF(Source.[ProgramMechanismId], Target.[ProgramMechanismId]) IS NOT NULL OR NULLIF(Target.[ProgramMechanismId], Source.[ProgramMechanismId]) IS NOT NULL OR 
	NULLIF(Source.[MechanismId], Target.[MechanismId]) IS NOT NULL OR NULLIF(Target.[MechanismId], Source.[MechanismId]) IS NOT NULL OR 
	NULLIF(Source.[ReviewStatusId], Target.[ReviewStatusId]) IS NOT NULL OR NULLIF(Target.[ReviewStatusId], Source.[ReviewStatusId]) IS NOT NULL OR 
	NULLIF(Source.[CreatedBy], Target.[CreatedBy]) IS NOT NULL OR NULLIF(Target.[CreatedBy], Source.[CreatedBy]) IS NOT NULL OR 
	NULLIF(Source.[CreatedDate], Target.[CreatedDate]) IS NOT NULL OR NULLIF(Target.[CreatedDate], Source.[CreatedDate]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedBy], Target.[ModifiedBy]) IS NOT NULL OR NULLIF(Target.[ModifiedBy], Source.[ModifiedBy]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedDate], Target.[ModifiedDate]) IS NOT NULL OR NULLIF(Target.[ModifiedDate], Source.[ModifiedDate]) IS NOT NULL) THEN
 UPDATE SET
  [ProgramMechanismId] = Source.[ProgramMechanismId], 
  [MechanismId] = Source.[MechanismId], 
  [ReviewStatusId] = Source.[ReviewStatusId], 
  [CreatedBy] = Source.[CreatedBy], 
  [CreatedDate] = Source.[CreatedDate], 
  [ModifiedBy] = Source.[ModifiedBy], 
  [ModifiedDate] = Source.[ModifiedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([MechanismTemplateId],[ProgramMechanismId],[MechanismId],[ReviewStatusId],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
 VALUES(Source.[MechanismTemplateId],Source.[ProgramMechanismId],Source.[MechanismId],Source.[ReviewStatusId],Source.[CreatedBy],Source.[CreatedDate],Source.[ModifiedBy],Source.[ModifiedDate])
;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [MechanismTemplate]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[MechanismTemplate] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [MechanismTemplate] OFF
GO