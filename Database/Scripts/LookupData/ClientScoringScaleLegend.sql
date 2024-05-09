SET IDENTITY_INSERT [ClientScoringScaleLegend] ON

MERGE INTO [ClientScoringScaleLegend] AS Target
USING (VALUES
  (1,9,'Overall')
 ,(2,9,'Criterion')
 ,(3,19,'Overall')
 ,(4,19,'Criterion')
 ,(5,23,'Overall')
 ,(6,23,'Criterion')
) AS Source ([ClientScoringScaleLegendId],[ClientId],[LegendName])
ON (Target.[ClientScoringScaleLegendId] = Source.[ClientScoringScaleLegendId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientId], Target.[ClientId]) IS NOT NULL OR NULLIF(Target.[ClientId], Source.[ClientId]) IS NOT NULL OR 
	NULLIF(Source.[LegendName], Target.[LegendName]) IS NOT NULL OR NULLIF(Target.[LegendName], Source.[LegendName]) IS NOT NULL) THEN
 UPDATE SET
  [ClientId] = Source.[ClientId], 
  [LegendName] = Source.[LegendName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientScoringScaleLegendId],[ClientId],[LegendName])
 VALUES(Source.[ClientScoringScaleLegendId],Source.[ClientId],Source.[LegendName])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientScoringScaleLegend]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientScoringScaleLegend] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientScoringScaleLegend] OFF
GO