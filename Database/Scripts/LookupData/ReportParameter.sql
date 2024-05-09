SET IDENTITY_INSERT [ReportParameter] ON

MERGE INTO [ReportParameter] AS Target
USING (VALUES
  (1,1,5,1,1)
 ,(2,1,6,1,1)
 ,(3,1,1,0,1)

 ,(4,2,5,1,1)
 ,(5,2,6,1,1)
 ,(6,2,1,0,1)

 ,(7,3,5,1,1)
 ,(8,3,6,1,1)
 ,(9,3,1,0,1)

 ,(10,4,5,1,1)
 ,(11,4,6,1,1)
 ,(12,4,1,0,1)

 ,(13,5,5,1,1)
 ,(14,5,6,1,1)
 ,(15,5,1,0,1)

 ,(16,6,5,1,1)
 ,(17,6,6,1,1)
 ,(18,6,1,0,1)

 ,(19,9,5,1,1)
 ,(20,9,6,1,1)
 ,(21,9,1,0,1)

 ,(22,10,5,1,1)
 ,(23,10,6,1,1)
 ,(24,10,1,0,1)
 
 ,(25,13,5,1,1)
 ,(26,13,6,1,1)
 ,(27,13,2,0,1)

 ,(28,14,5,1,1)
 ,(29,14,6,1,1)
 ,(30,14,1,0,1)

 ,(31,15,5,1,1)
 ,(32,15,6,1,1)
 ,(33,15,2,0,1)

 ,(34,16,5,1,1)
 ,(35,16,6,1,1)
 ,(36,16,2,0,1)

 ,(37,18,5,1,1)
 ,(38,18,6,1,1)
 ,(39,18,2,0,1)

 ,(40,19,5,1,1)
 ,(41,19,6,1,1)
 ,(42,19,1,0,1)

 ,(43,22,5,1,1)
 ,(44,22,6,1,1)
 ,(45,22,1,0,1)

 ,(46,23,5,1,1)
 ,(47,23,6,1,1)
 ,(48,23,1,0,1)

 ,(49,24,5,1,1)
 ,(50,24,6,1,1)
 ,(51,24,1,0,1)

 ,(52,28,5,1,1)
 ,(53,28,6,1,1)
 ,(54,28,1,0,1)

 ,(55,30,5,1,1)
 ,(56,30,6,1,1)
 ,(57,30,1,0,1)

 ,(58,31,5,1,1)
 ,(59,31,6,1,1)
 ,(60,31,1,0,1)

 ,(61,32,5,1,1)
 ,(62,32,6,1,1)
 ,(63,32,1,0,1)

 ,(64,33,5,1,1)
 ,(65,33,6,1,1)
 ,(66,33,2,0,1)

 ,(67,34,5,1,1)
 ,(68,34,6,1,1)
 ,(69,34,2,0,1)

 ,(70,35,5,1,1)
 ,(71,35,6,1,1)
 ,(72,35,1,0,1)

 ,(73,36,5,1,1)
 ,(74,36,6,1,1)
 ,(75,36,2,0,1)

 ,(76,37,5,1,1)
 ,(77,37,6,1,1)
 ,(78,37,2,0,1)

 ,(79,38,5,1,1)
 ,(80,38,6,1,1)
 ,(81,38,1,0,1)

 ,(82,39,5,1,1)
 ,(83,39,6,1,1)
 ,(84,39,1,0,1)

 ,(85,40,5,1,1)
 ,(86,40,6,1,1)
 ,(87,40,1,0,1)

 ,(88,41,5,1,1)
 ,(89,41,6,1,1)
 ,(90,41,1,0,1)

 ,(91,42,5,1,1)
 ,(92,42,6,1,1)
 ,(93,42,1,0,1)

 ,(94,45,5,1,1)
 ,(95,45,6,1,1)
 ,(96,45,1,0,1)

 ,(97,50,5,1,1)
 ,(98,50,6,1,1)
 ,(99,50,1,0,1)

 ,(100,55,5,1,1)
 ,(101,55,6,1,1)
 ,(102,55,2,0,1)

 ,(103,60,5,1,1)
 ,(104,60,6,1,1)
 ,(105,60,1,0,1)

 ,(106,65,7,1,1)
 ,(107,65,6,1,1)
 ,(108,65,3,1,1)

 ,(109,66,5,1,1)
 ,(110,66,6,1,1)
 ,(111,66,1,0,1)

 ,(112,68,5,1,1)
 ,(113,68,6,1,1)
 ,(114,68,1,0,1)

 ,(115,69,5,1,1)
 ,(116,69,6,1,1)
 ,(117,69,1,0,1)

 ,(118,70,5,1,1)
 ,(119,70,6,1,1)
 ,(120,70,2,0,1)

 ,(121,71,5,1,1)
 ,(122,71,6,1,1)
 ,(123,71,1,0,1)

 ,(124,72,7,1,1)
 ,(125,72,6,1,1)
 ,(126,72,3,1,1)

 ,(127,73,5,1,1)
 ,(128,73,6,1,1)
 ,(129,73,2,0,1)

 ,(130,90,5,1,1)
 ,(131,90,6,1,1)
 ,(132,90,2,0,1)

 ,(133,91,5,1,0)
 ,(134,91,6,1,0)
 ,(135,91,1,1,0)

 ,(136,92,5,1,0)
 ,(137,92,6,1,0)
 ,(138,92,2,1,0)
 
 ,(139,76,5,1,0)
 ,(140,76,6,1,0)

 ,(141,75,5,1,0)
 ,(142,75,6,1,1)

 ,(144,77,5,1,1)
 ,(145,77,6,1,1)

 ,(146,78,5,1,1)
 ,(147,78,6,1,1)
 ,(148,78,1,0,1)

 ,(149,79,5,1,0)
 ,(150,79,6,1,0)
 ,(151,79,1,0,1)

 ,(152,74,7,1,1)
 ,(153,74,6,1,1)
 ,(154,74,3,1,1)

 ,(155,93,5,1,0)
 ,(156,93,6,1,0)
 ,(157,93,1,0,1)

 ,(158,94,5,1,0)
 ,(159,94,6,1,0)
 ,(160,94,1,0,1)

 ,(161,95,5,1,1)
 ,(162,95,6,1,1)
 ,(163,95,2,0,1)

 ,(164,96,5,1,0)
 ,(165,96,6,1,0)
 ,(166,96,1,1,1)

) AS Source ([ReportParamId],[ReportId],[ParameterId],[Required],[MultiSelect])
ON (Target.[ReportParamId] = Source.[ReportParamId])
WHEN MATCHED AND (
	NULLIF(Source.[ReportId], Target.[ReportId]) IS NOT NULL OR NULLIF(Target.[ReportId], Source.[ReportId]) IS NOT NULL OR 
	NULLIF(Source.[ParameterId], Target.[ParameterId]) IS NOT NULL OR NULLIF(Target.[ParameterId], Source.[ParameterId]) IS NOT NULL  OR 
	NULLIF(Source.[Required], Target.[Required]) IS NOT NULL OR NULLIF(Target.[Required], Source.[Required]) IS NOT NULL OR 
	NULLIF(Source.[MultiSelect], Target.[MultiSelect]) IS NOT NULL OR NULLIF(Target.[MultiSelect], Source.[MultiSelect]) IS NOT NULL) THEN
 UPDATE SET
  [ReportId] = Source.[ReportId], 
  [ParameterId] = Source.[ParameterId], 
  [Required] = Source.[Required], 
  [MultiSelect] = Source.[MultiSelect]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ReportParamId],[ReportId],[ParameterId],[Required],[MultiSelect])
 VALUES(Source.[ReportParamId],Source.[ReportId],Source.[ParameterId],Source.[Required],Source.[MultiSelect])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ReportParameter]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ReportParameter] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ReportParameter] OFF
GO