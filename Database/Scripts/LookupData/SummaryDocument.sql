SET IDENTITY_INSERT [SummaryDocument] ON

MERGE INTO [SummaryDocument] AS Target
USING (VALUES
  (2,'SSCdmrpRegular','CDMRP - Standard Discussed','Standard template with an average score table.')
 ,(3,'SSCdmrpTriage','CDMRP - Standard Not Discussed','Standard template for not discussed/triaged applications with an individual score table.')
 ,(4,'SSCprit','CPRIT Research - Standard','Standard template for CPRIT Research.')
 ,(7,'SSCpritPRV','CPRIT Prevention - Standard','Standard template for CPRIT Prevention.')
 ,(8,'SSCpritREC','CPRITRecruitment - Standard','Standard template for CPRIT Recruitment.')
 ,(10,'SSCpritDueDiligence','CPRIT Due Diligence','Standatrd template for CPRIT Due Diligence')
) AS Source ([SummaryDocumentId],[DocumentFile],[DocumentName],[DocumentDescription])
ON (Target.[SummaryDocumentId] = Source.[SummaryDocumentId])
WHEN MATCHED AND (
	NULLIF(Source.[DocumentFile], Target.[DocumentFile]) IS NOT NULL OR NULLIF(Target.[DocumentFile], Source.[DocumentFile]) IS NOT NULL OR 
	NULLIF(Source.[DocumentName], Target.[DocumentName]) IS NOT NULL OR NULLIF(Target.[DocumentName], Source.[DocumentName]) IS NOT NULL OR 
	NULLIF(Source.[DocumentDescription], Target.[DocumentDescription]) IS NOT NULL OR NULLIF(Target.[DocumentDescription], Source.[DocumentDescription]) IS NOT NULL) THEN
 UPDATE SET
  [DocumentFile] = Source.[DocumentFile], 
  [DocumentName] = Source.[DocumentName], 
  [DocumentDescription] = Source.[DocumentDescription]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([SummaryDocumentId],[DocumentFile],[DocumentName],[DocumentDescription])
 VALUES(Source.[SummaryDocumentId],Source.[DocumentFile],Source.[DocumentName],Source.[DocumentDescription])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [SummaryDocument]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[SummaryDocument] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [SummaryDocument] OFF
GO