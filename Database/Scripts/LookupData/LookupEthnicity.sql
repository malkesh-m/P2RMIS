SET IDENTITY_INSERT [LookupEthnicity] ON
 
MERGE INTO [LookupEthnicity] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  (1,'')
 ,(2,'Alaskan Native')
 ,(3,'American Indian')
 ,(4,'Asian')
 ,(5,'Black or African American')
 ,(6,'Hispanic or Latino')
 ,(7,'Native Hawaiian or Pacific Islander')
 ,(8,'White')
 ,(9,'Other')

) AS Source ([EthnicityID],[Ethnicity])
ON (Target.[EthnicityID] = Source.[EthnicityID])
WHEN MATCHED AND (Target.[Ethnicity] <> Source.[Ethnicity]) THEN
 UPDATE SET
 [Ethnicity] = Source.[Ethnicity]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([EthnicityID],[Ethnicity])
 VALUES(Source.[EthnicityID],Source.[Ethnicity])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
 
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [LookupEthnicity]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[LookupEthnicity] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
 
SET IDENTITY_INSERT [LookupEthnicity] OFF
GO