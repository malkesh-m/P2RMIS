SET IDENTITY_INSERT [ParticipationMethod] ON

MERGE INTO [ParticipationMethod] AS Target
USING (VALUES
  (1,'InPerson')
 ,(2,'Remote')
) AS Source ([ParticipationMethodId],[ParticipationMethodLabel])
ON (Target.[ParticipationMethodId] = Source.[ParticipationMethodId])
WHEN MATCHED AND (
	NULLIF(Source.[ParticipationMethodLabel], Target.[ParticipationMethodLabel]) IS NOT NULL OR NULLIF(Target.[ParticipationMethodLabel], Source.[ParticipationMethodLabel]) IS NOT NULL) THEN
 UPDATE SET
  [ParticipationMethodLabel] = Source.[ParticipationMethodLabel]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ParticipationMethodId],[ParticipationMethodLabel])
 VALUES(Source.[ParticipationMethodId],Source.[ParticipationMethodLabel])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ParticipationMethod]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ParticipationMethod] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ParticipationMethod] OFF
GO