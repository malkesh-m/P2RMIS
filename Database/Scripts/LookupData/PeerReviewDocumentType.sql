MERGE INTO [PeerReviewDocumentType] AS Target
USING (VALUES
  (1,'Training',1)
 ,(2,'Meeting Information',2)
 ,(3,'Email Templates',3)
) AS Source ([PeerReviewDocumentTypeId],[DocumentType],[SortOrder])
ON (Target.[PeerReviewDocumentTypeId] = Source.[PeerReviewDocumentTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[DocumentType], Target.[DocumentType]) IS NOT NULL OR NULLIF(Target.[DocumentType], Source.[DocumentType]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [DocumentType] = Source.[DocumentType], 
 [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([PeerReviewDocumentTypeId],[DocumentType],[SortOrder])
 VALUES(Source.[PeerReviewDocumentTypeId],Source.[DocumentType],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [PeerReviewDocumentType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[PeerReviewDocumentType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO