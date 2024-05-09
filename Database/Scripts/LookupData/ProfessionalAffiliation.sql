
SET IDENTITY_INSERT [ProfessionalAffiliation] ON

MERGE INTO [ProfessionalAffiliation] AS Target
USING (VALUES
  (1,'Institution/Organization',0)
 ,(2,'Nominating Organization',1)
 ,(3,'Other',2)
) AS Source ([ProfessionalAffiliationId],[Type],[SortOrder])
ON (Target.[ProfessionalAffiliationId] = Source.[ProfessionalAffiliationId])
WHEN MATCHED AND (
	NULLIF(Source.[Type], Target.[Type]) IS NOT NULL OR NULLIF(Target.[Type], Source.[Type]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [Type] = Source.[Type], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ProfessionalAffiliationId],[Type],[SortOrder])
 VALUES(Source.[ProfessionalAffiliationId],Source.[Type],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ProfessionalAffiliation]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ProfessionalAffiliation] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ProfessionalAffiliation] OFF
GO