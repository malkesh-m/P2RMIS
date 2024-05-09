SET IDENTITY_INSERT [ClientScoringScaleLegendItem] ON

MERGE INTO [ClientScoringScaleLegendItem] AS Target
USING (VALUES
  (1,2,'Highest merit','1','3',1)
 ,(2,2,'Average merit','4','6',2)
 ,(3,2,'Lowest merit','7','9',3)
 ,(8,1,'Highest endorsement to fund proposed work','1','3',1)
 ,(9,1,'Modest endorsement to fund proposed work','4','6',2)
 ,(10,1,'Lowest endorsement to fund proposed work','7','9',3)
 ,(11,3,'Outstanding','1.0','1.5',1)
 ,(12,3,'Excellent','1.6','2.0',2)
 ,(13,3,'Good','2.1','2.5',3)
 ,(14,3,'Fair','2.6','3.5',4)
 ,(15,3,'Deficient','3.6','5.0',5)
 ,(16,4,'Outstanding','10','9',1)
 ,(17,4,'Excellent','8','7',2)
 ,(18,4,'Good','6','5',3)
 ,(19,4,'Fair','4','3',4)
 ,(20,4,'Deficient','2','1',5)
 ,(21,5,'Outstanding','1.0','1.5',1)
 ,(22,5,'Excellent','1.6','2.0',2)
 ,(23,5,'Good','2.1','2.5',3)
 ,(24,5,'Fair','2.6','3.5',4)
 ,(25,5,'Deficient','3.6','5.0',5)
 ,(26,6,'Outstanding','10','9',1)
 ,(27,6,'Excellent','8','7',2)
 ,(28,6,'Good','6','5',3)
 ,(29,6,'Fair','4','3',4)
 ,(30,6,'Deficient','2','1',5)
) AS Source ([ClientScoringScaleLegendItemId],[ClientScoringScaleLegendId],[LegendItemLabel],[HighValueLabel],[LowValueLabel],[SortOrder])
ON (Target.[ClientScoringScaleLegendItemId] = Source.[ClientScoringScaleLegendItemId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientScoringScaleLegendId], Target.[ClientScoringScaleLegendId]) IS NOT NULL OR NULLIF(Target.[ClientScoringScaleLegendId], Source.[ClientScoringScaleLegendId]) IS NOT NULL OR 
	NULLIF(Source.[LegendItemLabel], Target.[LegendItemLabel]) IS NOT NULL OR NULLIF(Target.[LegendItemLabel], Source.[LegendItemLabel]) IS NOT NULL OR 
	NULLIF(Source.[HighValueLabel], Target.[HighValueLabel]) IS NOT NULL OR NULLIF(Target.[HighValueLabel], Source.[HighValueLabel]) IS NOT NULL OR 
	NULLIF(Source.[LowValueLabel], Target.[LowValueLabel]) IS NOT NULL OR NULLIF(Target.[LowValueLabel], Source.[LowValueLabel]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [ClientScoringScaleLegendId] = Source.[ClientScoringScaleLegendId], 
  [LegendItemLabel] = Source.[LegendItemLabel], 
  [HighValueLabel] = Source.[HighValueLabel], 
  [LowValueLabel] = Source.[LowValueLabel], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientScoringScaleLegendItemId],[ClientScoringScaleLegendId],[LegendItemLabel],[HighValueLabel],[LowValueLabel],[SortOrder])
 VALUES(Source.[ClientScoringScaleLegendItemId],Source.[ClientScoringScaleLegendId],Source.[LegendItemLabel],Source.[HighValueLabel],Source.[LowValueLabel],Source.[SortOrder])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientScoringScaleLegendItem]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientScoringScaleLegendItem] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientScoringScaleLegendItem] OFF
GO