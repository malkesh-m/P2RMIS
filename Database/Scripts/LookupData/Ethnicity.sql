SET IDENTITY_INSERT [Ethnicity] ON

MERGE INTO [Ethnicity] AS Target
USING (VALUES
  (1,'None')
 ,(2,'Alaskan Native')
 ,(3,'American Indian')
 ,(4,'Asian')
 ,(5,'Black or African American')
 ,(6,'Hispanic or Latino')
 ,(7,'Native Hawaiian or Pacific Islander')
 ,(8,'White')
 ,(9,'Other')
) AS Source ([EthnicityId],[Ethnicity])
ON (Target.[EthnicityId] = Source.[EthnicityId])
WHEN MATCHED AND (
	NULLIF(Source.[Ethnicity], Target.[Ethnicity]) IS NOT NULL OR NULLIF(Target.[Ethnicity], Source.[Ethnicity]) IS NOT NULL) THEN
 UPDATE SET
  [Ethnicity] = Source.[Ethnicity]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([EthnicityId],[Ethnicity])
 VALUES(Source.[EthnicityId],Source.[Ethnicity])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [Ethnicity]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[Ethnicity] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [Ethnicity] OFF
GO