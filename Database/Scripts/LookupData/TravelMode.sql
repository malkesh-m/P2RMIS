MERGE INTO [TravelMode] AS Target
USING (VALUES
  (1,'Air',1,'Air')
 ,(2,'Other',5,'Other')
 ,(3,'POV',3,'POV')
 ,(4,'Rental Car',4,'Rental Car')
 ,(5,'Train',2,'Train')
) AS Source ([TravelModeId],[TravelModeAbbreviation],[SortOrder],[LegacyTravelModeAbbreviation])
ON (Target.[TravelModeId] = Source.[TravelModeId])
WHEN MATCHED AND (
	NULLIF(Source.[TravelModeAbbreviation], Target.[TravelModeAbbreviation]) IS NOT NULL OR NULLIF(Target.[TravelModeAbbreviation], Source.[TravelModeAbbreviation]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL OR 
	NULLIF(Source.[LegacyTravelModeAbbreviation], Target.[LegacyTravelModeAbbreviation]) IS NOT NULL OR NULLIF(Target.[LegacyTravelModeAbbreviation], Source.[LegacyTravelModeAbbreviation]) IS NOT NULL) THEN
 UPDATE SET
  [TravelModeAbbreviation] = Source.[TravelModeAbbreviation], 
  [SortOrder] = Source.[SortOrder], 
  [LegacyTravelModeAbbreviation] = Source.[LegacyTravelModeAbbreviation]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([TravelModeId],[TravelModeAbbreviation],[SortOrder],[LegacyTravelModeAbbreviation])
 VALUES(Source.[TravelModeId],Source.[TravelModeAbbreviation],Source.[SortOrder],Source.[LegacyTravelModeAbbreviation])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [TravelMode]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[TravelMode] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO