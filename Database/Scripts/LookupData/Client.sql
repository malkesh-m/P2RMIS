SET IDENTITY_INSERT [Client] ON

MERGE INTO [Client] AS Target
USING (VALUES
  (1,'BNBI','Batelle National Biodefense Institute, LLC',1)
 ,(2,'CDC','Centers for Disease Control and Prevention',1)
 ,(3,'CDC NCHM','CDC- National Center for Health Marketing',1)
 ,(4,'CDC NIOSH','Centers for Disease Control and Prevention - NIOSH',1)
 ,(5,'CDC STEPS','Centers for Disease Control and Prevention - STEPS',1)
 ,(6,'CDCF','Centers for Disease Control Foundation',1)
 ,(7,'CNCS','The Corporation for National and Community Service',1)
 ,(8,'Constella','SRA International Inc.',2)
 ,(9,'CPRIT','Cancer Prevention and Research Institute of Texas',2)
 ,(10,'FNIH','Foundation for the National Institutes of Health (FNIH)',1)
 ,(11,'KOMEN','Susan G. Komen for the Cure Grants Program',1)
 ,(12,'NIC','National Institutes of Health',1)
 ,(13,'NIDDK','National Inst of Diabetes, Digestive& Kidney Dis ',1)
 ,(14,'OMH','Office of Minority Health',1)
 ,(15,'Other','Other',1)
 ,(16,'ST','State of Texas',1)
 ,(17,'SUNY','State University of New York',1)
 ,(18,'US DOE GTP','U.S. DOE Geothermal Technologies Program',1)
 ,(19,'USAMRMC','USAMRMC/CDMRP',2)
 ,(20,'USSOCOM','US Special Operations Command',2)
 ,(21,'USUHS','Uniform Service University Health Sciences',1)
 ,(22,'WRIISC','VA/War-Related Illness & Injury Study Center',1)
 ,(23,'MRMC','Medical Research and Materiel Command',2)
) AS Source ([ClientID],[ClientAbrv],[ClientDesc],[SummaryStatementModeId])
ON (Target.[ClientID] = Source.[ClientID])
WHEN MATCHED AND (
	NULLIF(Source.[ClientAbrv], Target.[ClientAbrv]) IS NOT NULL OR NULLIF(Target.[ClientAbrv], Source.[ClientAbrv]) IS NOT NULL OR 
	NULLIF(Source.[ClientDesc], Target.[ClientDesc]) IS NOT NULL OR NULLIF(Target.[ClientDesc], Source.[ClientDesc]) IS NOT NULL OR 
	NULLIF(Source.[SummaryStatementModeId], Target.[SummaryStatementModeId]) IS NOT NULL OR NULLIF(Target.[SummaryStatementModeId], Source.[SummaryStatementModeId]) IS NOT NULL) THEN
 UPDATE SET
  [ClientAbrv] = Source.[ClientAbrv], 
  [ClientDesc] = Source.[ClientDesc], 
  [SummaryStatementModeId] = Source.[SummaryStatementModeId]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientID],[ClientAbrv],[ClientDesc],[SummaryStatementModeId])
 VALUES(Source.[ClientID],Source.[ClientAbrv],Source.[ClientDesc],Source.[SummaryStatementModeId])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [Client]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[Client] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [Client] OFF
GO