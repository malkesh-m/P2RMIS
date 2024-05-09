SET IDENTITY_INSERT [ClientScoringScaleAdjectival] ON

MERGE INTO [ClientScoringScaleAdjectival] AS Target
USING (VALUES
  (1,3,'Outstanding',1,10,'2014-08-22T00:00:00',10,'2014-08-22T00:00:00')
 ,(2,3,'Excellent',2,10,'2014-08-22T00:00:00',10,'2014-08-22T00:00:00')
 ,(3,3,'Good',3,10,'2014-08-22T00:00:00',10,'2014-08-22T00:00:00')
 ,(4,3,'Fair',4,10,'2014-08-22T00:00:00',10,'2014-08-22T00:00:00')
 ,(5,3,'Deficient',5,10,'2014-08-22T00:00:00',10,'2014-08-22T00:00:00')
 ,(6,8,'High Enthusiasm',1,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(7,8,'Medium Enthusiasm',2,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(8,8,'Low Enthusiasm',3,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(9,9,'Outstanding',1,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(10,9,'Very Good',2,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(11,9,'Good',3,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(12,9,'Fair',4,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(13,10,'Outstanding',1,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(14,10,'Excellent',2,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(15,10,'Good',3,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(16,10,'Fair',4,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(17,10,'Deficient',5,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(18,11,'High',1,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(19,11,'Medium',2,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(20,11,'Low',3,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(21,12,'Acceptable',1,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(22,12,'Acceptable with Revision',2,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(23,12,'Unacceptable',3,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(24,17,'Outstanding',1,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(25,17,'Excellent',2,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(26,17,'Good',3,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(27,17,'Fair',4,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(28,17,'Deficient',5,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(29,18,'Fair',1,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(30,18,'Good',2,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(31,18,'Very Good',3,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(32,18,'Outstanding',4,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(33,19,'Outstanding',1,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(34,19,'Excellent',2,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(35,19,'Good',3,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(36,19,'Fair',4,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(37,19,'Deficient',5,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(38,20,'Outstanding',1,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(39,20,'Excellent',2,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(40,20,'Good',3,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(41,20,'Fair',4,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(42,20,'Deficient',5,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(43,142,'Poor',1,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(44,142,'Moderate',2,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(45,142,'Good',3,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(46,142,'Excellent',4,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(47,142,'Outstanding',5,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(48,149,'Outstanding',1,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(49,149,'Excellent',2,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(50,149,'Good',3,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(51,149,'Fair',4,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(52,149,'Deficient',5,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(53,152,'Not Acceptable (5)',5,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(54,152,'Acceptable (11)',11,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(55,152,'Good (14)',14,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(56,152,'Excellent (17)',17,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(57,160,'Exceptional',1,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(58,160,'Good',2,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(59,160,'Fair',3,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(60,160,'Deficient',4,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
 ,(61,160,'Reject',5,10,'2015-02-04T00:00:00',10,'2015-02-04T00:00:00')
) AS Source ([ClientScoringScaleAdjectivalId],[ClientScoringId],[ScoreLabel],[NumericEquivalent],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
ON (Target.[ClientScoringScaleAdjectivalId] = Source.[ClientScoringScaleAdjectivalId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientScoringId], Target.[ClientScoringId]) IS NOT NULL OR NULLIF(Target.[ClientScoringId], Source.[ClientScoringId]) IS NOT NULL OR 
	NULLIF(Source.[ScoreLabel], Target.[ScoreLabel]) IS NOT NULL OR NULLIF(Target.[ScoreLabel], Source.[ScoreLabel]) IS NOT NULL OR 
	NULLIF(Source.[NumericEquivalent], Target.[NumericEquivalent]) IS NOT NULL OR NULLIF(Target.[NumericEquivalent], Source.[NumericEquivalent]) IS NOT NULL OR 
	NULLIF(Source.[CreatedBy], Target.[CreatedBy]) IS NOT NULL OR NULLIF(Target.[CreatedBy], Source.[CreatedBy]) IS NOT NULL OR 
	NULLIF(Source.[CreatedDate], Target.[CreatedDate]) IS NOT NULL OR NULLIF(Target.[CreatedDate], Source.[CreatedDate]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedBy], Target.[ModifiedBy]) IS NOT NULL OR NULLIF(Target.[ModifiedBy], Source.[ModifiedBy]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedDate], Target.[ModifiedDate]) IS NOT NULL OR NULLIF(Target.[ModifiedDate], Source.[ModifiedDate]) IS NOT NULL) THEN
 UPDATE SET
  [ClientScoringId] = Source.[ClientScoringId], 
  [ScoreLabel] = Source.[ScoreLabel], 
  [NumericEquivalent] = Source.[NumericEquivalent], 
  [CreatedBy] = Source.[CreatedBy], 
  [CreatedDate] = Source.[CreatedDate], 
  [ModifiedBy] = Source.[ModifiedBy], 
  [ModifiedDate] = Source.[ModifiedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientScoringScaleAdjectivalId],[ClientScoringId],[ScoreLabel],[NumericEquivalent],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
 VALUES(Source.[ClientScoringScaleAdjectivalId],Source.[ClientScoringId],Source.[ScoreLabel],Source.[NumericEquivalent],Source.[CreatedBy],Source.[CreatedDate],Source.[ModifiedBy],Source.[ModifiedDate])
;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientScoringScaleAdjectival]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientScoringScaleAdjectival] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientScoringScaleAdjectival] OFF
GO