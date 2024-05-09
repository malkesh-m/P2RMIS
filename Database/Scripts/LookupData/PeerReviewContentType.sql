MERGE INTO [PeerReviewContentType] AS Target
USING (VALUES
  (1,'File/Image',1,1,'file')
 ,(2,'Link to the Document',2,0,'web')
 ,(3,'Video',3,0,'embed')
) AS Source ([PeerReviewContentTypeId],[ContentType],[SortOrder],[DefaultFlag],[AccessMethod])
ON (Target.[PeerReviewContentTypeId] = Source.[PeerReviewContentTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[ContentType], Target.[ContentType]) IS NOT NULL OR NULLIF(Target.[ContentType], Source.[ContentType]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL OR 
	NULLIF(Source.[DefaultFlag], Target.[DefaultFlag]) IS NOT NULL OR NULLIF(Target.[DefaultFlag], Source.[DefaultFlag]) IS NOT NULL OR 
	NULLIF(Source.[AccessMethod], Target.[AccessMethod]) IS NOT NULL OR NULLIF(Target.[AccessMethod], Source.[AccessMethod]) IS NOT NULL) THEN
 UPDATE SET
  [ContentType] = Source.[ContentType], 
 [SortOrder] = Source.[SortOrder], 
 [DefaultFlag] = Source.[DefaultFlag], 
 [AccessMethod] = Source.[AccessMethod]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([PeerReviewContentTypeId],[ContentType],[SortOrder],[DefaultFlag],[AccessMethod])
 VALUES(Source.[PeerReviewContentTypeId],Source.[ContentType],Source.[SortOrder],Source.[DefaultFlag],Source.[AccessMethod])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [PeerReviewContentType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[PeerReviewContentType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
