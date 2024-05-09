SET IDENTITY_INSERT [MechanismTemplateElement] ON

MERGE INTO [MechanismTemplateElement] AS Target
USING (VALUES
  (3,2,1,NULL,1,NULL,1,0,1,10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(4,2,2,'The Principal Investigators (PIs) of this application plans to [the hypothesis to be tested or the objective to be reached]. The project''s specific aims are [list of the specific aims with numbering].',2,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(5,2,5,NULL,3,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(9,2,7,NULL,4,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(10,2,8,NULL,5,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(11,2,9,NULL,6,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(12,2,11,NULL,7,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(13,2,15,NULL,8,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(14,2,16,NULL,9,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(15,2,17,NULL,10,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(16,2,18,NULL,11,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(17,2,20,NULL,12,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(18,2,21,NULL,13,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(19,2,22,NULL,14,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(20,2,23,NULL,15,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(21,2,25,NULL,16,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(22,3,1,NULL,1,NULL,1,0,1,10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(24,3,5,NULL,3,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(26,3,8,NULL,5,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(28,3,11,NULL,7,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(30,3,16,NULL,9,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(32,3,18,NULL,11,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(34,3,21,NULL,13,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(36,3,23,NULL,15,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(38,4,1,NULL,1,NULL,1,0,1,10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(39,4,2,'The Principal Investigators (PIs) of this application plans to [the hypothesis to be tested or the objective to be reached]. The project''s specific aims are [list of the specific aims with numbering].',2,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(40,4,5,NULL,3,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(41,4,7,NULL,4,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(42,4,8,NULL,5,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(43,4,9,NULL,6,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(44,4,11,NULL,7,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(45,4,15,NULL,8,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(46,4,16,NULL,9,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(47,4,17,NULL,10,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(48,4,18,NULL,11,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(49,4,20,NULL,12,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(50,4,21,NULL,13,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(51,4,22,NULL,14,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(52,4,23,NULL,15,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(53,4,25,NULL,16,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(54,5,1,NULL,1,NULL,1,0,1,10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(56,5,5,NULL,3,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(58,5,8,NULL,5,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(60,5,11,NULL,7,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(62,5,16,NULL,9,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(64,5,18,NULL,11,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(66,5,21,NULL,13,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(68,5,23,NULL,15,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(70,6,1,NULL,1,NULL,1,0,1,10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(71,6,2,'The Principal Investigators (PIs) of this application plans to [the hypothesis to be tested or the objective to be reached]. The project''s specific aims are [list of the specific aims with numbering].',2,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(72,6,5,NULL,3,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(73,6,7,NULL,4,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(74,6,8,NULL,5,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(75,6,9,NULL,6,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(76,6,11,NULL,7,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(77,6,15,NULL,8,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(78,6,16,NULL,9,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(79,6,17,NULL,10,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(80,6,18,NULL,11,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(81,6,20,NULL,12,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(82,6,21,NULL,13,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(83,6,22,NULL,14,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(84,6,23,NULL,15,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(85,6,25,NULL,16,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(86,7,1,NULL,1,NULL,1,0,1,10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(88,7,5,NULL,3,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(90,7,8,NULL,5,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(92,7,11,NULL,7,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(94,7,16,NULL,9,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(96,7,18,NULL,11,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(98,7,21,NULL,13,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(100,7,23,NULL,15,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(102,8,1,NULL,1,NULL,1,0,1,10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(103,8,2,'The Principal Investigators (PIs) of this application plans to [the hypothesis to be tested or the objective to be reached]. The project''s specific aims are [list of the specific aims with numbering].',2,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(104,8,5,NULL,3,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(105,8,7,NULL,4,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(106,8,8,NULL,5,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(107,8,9,NULL,6,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(108,8,11,NULL,7,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(109,8,15,NULL,8,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(110,8,16,NULL,9,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(111,8,17,NULL,10,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(112,8,18,NULL,11,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(113,8,20,NULL,12,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(114,8,21,NULL,13,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(115,8,22,NULL,14,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(116,8,23,NULL,15,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(117,8,25,NULL,16,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(118,9,1,NULL,1,NULL,1,0,1,10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(120,9,5,NULL,3,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(122,9,8,NULL,5,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(124,9,11,NULL,7,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(126,9,16,NULL,9,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(128,9,18,NULL,11,NULL,1,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(130,9,21,NULL,13,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(132,9,23,NULL,15,NULL,0,1,0,10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
) AS Source ([MechanismTemplateElementId],[MechanismTemplateId],[ClientElementId],[InstructionText],[SortOrder],[RecommendedWordCount],[ScoreFlag],[TextFlag],[OverallFlag],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
ON (Target.[MechanismTemplateElementId] = Source.[MechanismTemplateElementId])
WHEN MATCHED AND (
	NULLIF(Source.[MechanismTemplateId], Target.[MechanismTemplateId]) IS NOT NULL OR NULLIF(Target.[MechanismTemplateId], Source.[MechanismTemplateId]) IS NOT NULL OR 
	NULLIF(Source.[ClientElementId], Target.[ClientElementId]) IS NOT NULL OR NULLIF(Target.[ClientElementId], Source.[ClientElementId]) IS NOT NULL OR 
	NULLIF(Source.[InstructionText], Target.[InstructionText]) IS NOT NULL OR NULLIF(Target.[InstructionText], Source.[InstructionText]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL OR 
	NULLIF(Source.[RecommendedWordCount], Target.[RecommendedWordCount]) IS NOT NULL OR NULLIF(Target.[RecommendedWordCount], Source.[RecommendedWordCount]) IS NOT NULL OR 
	NULLIF(Source.[ScoreFlag], Target.[ScoreFlag]) IS NOT NULL OR NULLIF(Target.[ScoreFlag], Source.[ScoreFlag]) IS NOT NULL OR 
	NULLIF(Source.[TextFlag], Target.[TextFlag]) IS NOT NULL OR NULLIF(Target.[TextFlag], Source.[TextFlag]) IS NOT NULL OR 
	NULLIF(Source.[OverallFlag], Target.[OverallFlag]) IS NOT NULL OR NULLIF(Target.[OverallFlag], Source.[OverallFlag]) IS NOT NULL OR 
	NULLIF(Source.[CreatedBy], Target.[CreatedBy]) IS NOT NULL OR NULLIF(Target.[CreatedBy], Source.[CreatedBy]) IS NOT NULL OR 
	NULLIF(Source.[CreatedDate], Target.[CreatedDate]) IS NOT NULL OR NULLIF(Target.[CreatedDate], Source.[CreatedDate]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedBy], Target.[ModifiedBy]) IS NOT NULL OR NULLIF(Target.[ModifiedBy], Source.[ModifiedBy]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedDate], Target.[ModifiedDate]) IS NOT NULL OR NULLIF(Target.[ModifiedDate], Source.[ModifiedDate]) IS NOT NULL) THEN
 UPDATE SET
  [MechanismTemplateId] = Source.[MechanismTemplateId], 
  [ClientElementId] = Source.[ClientElementId], 
  [InstructionText] = Source.[InstructionText], 
  [SortOrder] = Source.[SortOrder], 
  [RecommendedWordCount] = Source.[RecommendedWordCount], 
  [ScoreFlag] = Source.[ScoreFlag], 
  [TextFlag] = Source.[TextFlag], 
  [OverallFlag] = Source.[OverallFlag], 
  [CreatedBy] = Source.[CreatedBy], 
  [CreatedDate] = Source.[CreatedDate], 
  [ModifiedBy] = Source.[ModifiedBy], 
  [ModifiedDate] = Source.[ModifiedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([MechanismTemplateElementId],[MechanismTemplateId],[ClientElementId],[InstructionText],[SortOrder],[RecommendedWordCount],[ScoreFlag],[TextFlag],[OverallFlag],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
 VALUES(Source.[MechanismTemplateElementId],Source.[MechanismTemplateId],Source.[ClientElementId],Source.[InstructionText],Source.[SortOrder],Source.[RecommendedWordCount],Source.[ScoreFlag],Source.[TextFlag],Source.[OverallFlag],Source.[CreatedBy],Source.[CreatedDate],Source.[ModifiedBy],Source.[ModifiedDate])
;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [MechanismTemplateElement]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[MechanismTemplateElement] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [MechanismTemplateElement] OFF
GO