SET IDENTITY_INSERT [ScoringTemplate] ON

MERGE INTO [ScoringTemplate] AS [Target]
USING (VALUES
  (1,19,N'Onsite/Teleconference')
 ,(2,19,N'Online')
 ,(3,9,N'Research/Product-Dev/Prevention')
 ,(4,23,N'MRMC')
 ,(5,20,N'SOCOM')
 ,(6,23,N'Online')
) AS [Source] ([ScoringTemplateId],[ClientId],[TemplateName])
ON ([Target].[ScoringTemplateId] = [Source].[ScoringTemplateId])
WHEN MATCHED AND (
	NULLIF([Source].[ClientId], [Target].[ClientId]) IS NOT NULL OR NULLIF([Target].[ClientId], [Source].[ClientId]) IS NOT NULL OR 
	NULLIF([Source].[TemplateName], [Target].[TemplateName]) IS NOT NULL OR NULLIF([Target].[TemplateName], [Source].[TemplateName]) IS NOT NULL) THEN
 UPDATE SET
  [Target].[ClientId] = [Source].[ClientId], 
  [Target].[TemplateName] = [Source].[TemplateName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ScoringTemplateId],[ClientId],[TemplateName])
 VALUES([Source].[ScoringTemplateId],[Source].[ClientId],[Source].[TemplateName])
;

DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ScoringTemplate]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ScoringTemplate] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO



SET IDENTITY_INSERT [ScoringTemplate] OFF
GO