SET IDENTITY_INSERT [ResearchCategoryType] ON

MERGE INTO [ResearchCategoryType] AS Target
USING (VALUES
  (1,'CDMRP Research Classification')
 ,(2,'CSO Research Classification')
 ,(3,'Research Area')
) AS Source ([ResearchCategoryTypeId],[ResearchCategoryName])
ON (Target.[ResearchCategoryTypeId] = Source.[ResearchCategoryTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[ResearchCategoryName], Target.[ResearchCategoryName]) IS NOT NULL OR NULLIF(Target.[ResearchCategoryName], Source.[ResearchCategoryName]) IS NOT NULL) THEN
 UPDATE SET
  [ResearchCategoryName] = Source.[ResearchCategoryName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ResearchCategoryTypeId],[ResearchCategoryName])
 VALUES(Source.[ResearchCategoryTypeId],Source.[ResearchCategoryName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ResearchCategoryType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ResearchCategoryType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ResearchCategoryType] OFF
GO