MERGE INTO [TransferReason] AS Target
USING (VALUES
  (1,'Application’s content does not fit the review panel','Application',1)
 ,(2,'Low or no reviewer expertise coverage on the review panel','Application',2)
 ,(3,'A reviewer has a COI with the application','Application',3)
) AS Source ([TransferReasonId],[Reason],[TransferType],[SortOrder])
ON (Target.[TransferReasonId] = Source.[TransferReasonId])
WHEN MATCHED AND (
	NULLIF(Source.[Reason], Target.[Reason]) IS NOT NULL OR NULLIF(Target.[Reason], Source.[Reason]) IS NOT NULL OR 
	NULLIF(Source.[TransferType], Target.[TransferType]) IS NOT NULL OR NULLIF(Target.[TransferType], Source.[TransferType]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [Reason] = Source.[Reason], 
  [TransferType] = Source.[TransferType], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([TransferReasonId],[Reason],[TransferType],[SortOrder])
 VALUES(Source.[TransferReasonId],Source.[Reason],Source.[TransferType],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [TransferReason]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[TransferReason] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO