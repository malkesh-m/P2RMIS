MERGE INTO [ClientTransferType] AS Target
USING (VALUES
  (1,19,'Transfer Single Mechanism','Transfers application data from source system for a single mechanism','TransferSingleMechanism','https://egs.cdmrp.army.mil/EGS/ws/egsWebservice.do?operation=getProposalInfo','&program={0}&fy={1}&awardType={2}&receiptCycle={3}','d2ViaGVhZHM6dXNlYXBhY2hl','basic','xml')
) AS Source ([ClientTransferTypeId],[ClientId],[Label],[Description],[ApiPath],[ExternalUrl],[ExternalUrlParameters],[Credentials],[AuthenticationType],[ReturnFormat])
ON (Target.[ClientTransferTypeId] = Source.[ClientTransferTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientId], Target.[ClientId]) IS NOT NULL OR NULLIF(Target.[ClientId], Source.[ClientId]) IS NOT NULL OR 
	NULLIF(Source.[Label], Target.[Label]) IS NOT NULL OR NULLIF(Target.[Label], Source.[Label]) IS NOT NULL OR 
	NULLIF(Source.[Description], Target.[Description]) IS NOT NULL OR NULLIF(Target.[Description], Source.[Description]) IS NOT NULL OR 
	NULLIF(Source.[ApiPath], Target.[ApiPath]) IS NOT NULL OR NULLIF(Target.[ApiPath], Source.[ApiPath]) IS NOT NULL OR 
	NULLIF(Source.[ExternalUrl], Target.[ExternalUrl]) IS NOT NULL OR NULLIF(Target.[ExternalUrl], Source.[ExternalUrl]) IS NOT NULL OR 
	NULLIF(Source.[ExternalUrlParameters], Target.[ExternalUrlParameters]) IS NOT NULL OR NULLIF(Target.[ExternalUrlParameters], Source.[ExternalUrlParameters]) IS NOT NULL OR 
	NULLIF(Source.[Credentials], Target.[Credentials]) IS NOT NULL OR NULLIF(Target.[Credentials], Source.[Credentials]) IS NOT NULL OR 
	NULLIF(Source.[AuthenticationType], Target.[AuthenticationType]) IS NOT NULL OR NULLIF(Target.[AuthenticationType], Source.[AuthenticationType]) IS NOT NULL OR 
	NULLIF(Source.[ReturnFormat], Target.[ReturnFormat]) IS NOT NULL OR NULLIF(Target.[ReturnFormat], Source.[ReturnFormat]) IS NOT NULL) THEN
 UPDATE SET
  [ClientId] = Source.[ClientId], 
 [Label] = Source.[Label], 
 [Description] = Source.[Description], 
 [ApiPath] = Source.[ApiPath], 
 [ExternalUrl] = Source.[ExternalUrl], 
 [ExternalUrlParameters] = Source.[ExternalUrlParameters], 
 [Credentials] = Source.[Credentials], 
 [AuthenticationType] = Source.[AuthenticationType], 
 [ReturnFormat] = Source.[ReturnFormat]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientTransferTypeId],[ClientId],[Label],[Description],[ApiPath],[ExternalUrl],[ExternalUrlParameters],[Credentials],[AuthenticationType],[ReturnFormat])
 VALUES(Source.[ClientTransferTypeId],Source.[ClientId],Source.[Label],Source.[Description],Source.[ApiPath],Source.[ExternalUrl],Source.[ExternalUrlParameters],Source.[Credentials],Source.[AuthenticationType],Source.[ReturnFormat])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientTransferType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientTransferType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO