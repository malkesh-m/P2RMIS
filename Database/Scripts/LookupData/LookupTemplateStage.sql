SET IDENTITY_INSERT [LookupTemplateStage] ON
 
MERGE INTO [LookupTemplateStage] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  (1,'Draft')
 ,(2,'Published')
 ,(3,'Superseded')

) AS Source ([TemplateStageID],[TemplateStageName])
ON (Target.[TemplateStageID] = Source.[TemplateStageID])
WHEN MATCHED AND (Target.[TemplateStageName] <> Source.[TemplateStageName]) THEN
 UPDATE SET
 [TemplateStageName] = Source.[TemplateStageName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([TemplateStageID],[TemplateStageName])
 VALUES(Source.[TemplateStageID],Source.[TemplateStageName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
 
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [LookupTemplateStage]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[LookupTemplateStage] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
 
SET IDENTITY_INSERT [LookupTemplateStage] OFF
GO