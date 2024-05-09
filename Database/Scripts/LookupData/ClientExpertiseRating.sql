SET IDENTITY_INSERT [ClientExpertiseRating] ON

MERGE INTO [ClientExpertiseRating] AS Target
USING (VALUES
  (1,19,'High','High','High Expertise',0,1)
 ,(2,19,'Med','Medium','Medium Expertise',0,2)
 ,(3,19,'Low','Low','Low Expertise',0,3)
 ,(4,19,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(5,19,'None','None','None',0,4)
 ,(6,1,'High','High','High Expertise',0,1)
 ,(7,1,'Med','Medium','Medium Expertise',0,2)
 ,(8,1,'Low','Low','Low Expertise',0,3)
 ,(9,1,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(10,1,'None','None','None',0,4)
 ,(11,2,'None','None','None',0,4)
 ,(12,2,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(13,2,'Low','Low','Low Expertise',0,3)
 ,(14,2,'Med','Medium','Medium Expertise',0,2)
 ,(15,2,'High','High','High Expertise',0,1)
 ,(16,3,'High','High','High Expertise',0,1)
 ,(17,3,'Med','Medium','Medium Expertise',0,2)
 ,(18,3,'Low','Low','Low Expertise',0,3)
 ,(19,3,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(20,3,'None','None','None',0,4)
 ,(21,4,'None','None','None',0,4)
 ,(22,4,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(23,4,'Low','Low','Low Expertise',0,3)
 ,(24,4,'Med','Medium','Medium Expertise',0,2)
 ,(25,4,'High','High','High Expertise',0,1)
 ,(26,5,'High','High','High Expertise',0,1)
 ,(27,5,'Med','Medium','Medium Expertise',0,2)
 ,(28,5,'Low','Low','Low Expertise',0,3)
 ,(29,5,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(30,5,'None','None','None',0,4)
 ,(31,6,'None','None','None',0,4)
 ,(32,6,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(33,6,'Low','Low','Low Expertise',0,3)
 ,(34,6,'Med','Medium','Medium Expertise',0,2)
 ,(35,6,'High','High','High Expertise',0,1)
 ,(36,7,'High','High','High Expertise',0,1)
 ,(37,7,'Med','Medium','Medium Expertise',0,2)
 ,(38,7,'Low','Low','Low Expertise',0,3)
 ,(39,7,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(40,7,'None','None','None',0,4)
 ,(41,8,'None','None','None',0,4)
 ,(42,8,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(43,8,'Low','Low','Low Expertise',0,3)
 ,(44,8,'Med','Medium','Medium Expertise',0,2)
 ,(45,8,'High','High','High Expertise',0,1)
 ,(46,9,'High','High','High Expertise',0,1)
 ,(47,9,'Med','Medium','Medium Expertise',0,2)
 ,(48,9,'Low','Low','Low Expertise',0,3)
 ,(49,9,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(50,9,'None','None','None',0,4)
 ,(51,10,'None','None','None',0,4)
 ,(52,10,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(53,10,'Low','Low','Low Expertise',0,3)
 ,(54,10,'Med','Medium','Medium Expertise',0,2)
 ,(55,10,'High','High','High Expertise',0,1)
 ,(56,11,'High','High','High Expertise',0,1)
 ,(57,11,'Med','Medium','Medium Expertise',0,2)
 ,(58,11,'Low','Low','Low Expertise',0,3)
 ,(59,11,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(60,11,'None','None','None',0,4)
 ,(61,12,'None','None','None',0,4)
 ,(62,12,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(63,12,'Low','Low','Low Expertise',0,3)
 ,(64,12,'Med','Medium','Medium Expertise',0,2)
 ,(65,12,'High','High','High Expertise',0,1)
 ,(66,13,'High','High','High Expertise',0,1)
 ,(67,13,'Med','Medium','Medium Expertise',0,2)
 ,(68,13,'Low','Low','Low Expertise',0,3)
 ,(69,13,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(70,13,'None','None','None',0,4)
 ,(71,14,'None','None','None',0,4)
 ,(72,14,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(73,14,'Low','Low','Low Expertise',0,3)
 ,(74,14,'Med','Medium','Medium Expertise',0,2)
 ,(75,14,'High','High','High Expertise',0,1)
 ,(76,15,'High','High','High Expertise',0,1)
 ,(77,15,'Med','Medium','Medium Expertise',0,2)
 ,(78,15,'Low','Low','Low Expertise',0,3)
 ,(79,15,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(80,15,'None','None','None',0,4)
 ,(81,16,'None','None','None',0,4)
 ,(82,16,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(83,16,'Low','Low','Low Expertise',0,3)
 ,(84,16,'Med','Medium','Medium Expertise',0,2)
 ,(85,16,'High','High','High Expertise',0,1)
 ,(86,17,'High','High','High Expertise',0,1)
 ,(87,17,'Med','Medium','Medium Expertise',0,2)
 ,(88,17,'Low','Low','Low Expertise',0,3)
 ,(89,17,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(90,17,'None','None','None',0,4)
 ,(91,18,'None','None','None',0,4)
 ,(92,18,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(93,18,'Low','Low','Low Expertise',0,3)
 ,(94,18,'Med','Medium','Medium Expertise',0,2)
 ,(95,18,'High','High','High Expertise',0,1)
 ,(96,20,'High','High','High Expertise',0,1)
 ,(97,20,'Med','Medium','Medium Expertise',0,2)
 ,(98,20,'Low','Low','Low Expertise',0,3)
 ,(99,20,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(100,20,'None','None','None',0,4)
 ,(101,21,'None','None','None',0,4)
 ,(102,21,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(103,21,'Low','Low','Low Expertise',0,3)
 ,(104,21,'Med','Medium','Medium Expertise',0,2)
 ,(105,21,'High','High','High Expertise',0,1)
 ,(106,22,'High','High','High Expertise',0,1)
 ,(107,22,'Med','Medium','Medium Expertise',0,2)
 ,(108,22,'Low','Low','Low Expertise',0,3)
 ,(109,22,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(110,22,'None','None','None',0,4)
 ,(111,23,'High','High','High Expertise',0,1)
 ,(112,23,'Med','Medium','Medium Expertise',0,2)
 ,(113,23,'Low','Low','Low Expertise',0,3)
 ,(114,23,'COI','Conflict of Interest','Conflict of Interest',1,5)
 ,(115,23,'None','None','None',0,4)
) AS Source ([ClientExpertiseRatingId],[ClientId],[RatingAbbreviation],[RatingName],[RatingDescription],[ConflictFlag],[SortOrder])
ON (Target.[ClientExpertiseRatingId] = Source.[ClientExpertiseRatingId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientId], Target.[ClientId]) IS NOT NULL OR NULLIF(Target.[ClientId], Source.[ClientId]) IS NOT NULL OR 
	NULLIF(Source.[RatingAbbreviation], Target.[RatingAbbreviation]) IS NOT NULL OR NULLIF(Target.[RatingAbbreviation], Source.[RatingAbbreviation]) IS NOT NULL OR 
	NULLIF(Source.[RatingName], Target.[RatingName]) IS NOT NULL OR NULLIF(Target.[RatingName], Source.[RatingName]) IS NOT NULL OR 
	NULLIF(Source.[RatingDescription], Target.[RatingDescription]) IS NOT NULL OR NULLIF(Target.[RatingDescription], Source.[RatingDescription]) IS NOT NULL OR 
	NULLIF(Source.[ConflictFlag], Target.[ConflictFlag]) IS NOT NULL OR NULLIF(Target.[ConflictFlag], Source.[ConflictFlag]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [ClientId] = Source.[ClientId], 
  [RatingAbbreviation] = Source.[RatingAbbreviation], 
  [RatingName] = Source.[RatingName], 
  [RatingDescription] = Source.[RatingDescription], 
  [ConflictFlag] = Source.[ConflictFlag], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientExpertiseRatingId],[ClientId],[RatingAbbreviation],[RatingName],[RatingDescription],[ConflictFlag],[SortOrder])
 VALUES(Source.[ClientExpertiseRatingId],Source.[ClientId],Source.[RatingAbbreviation],Source.[RatingName],Source.[RatingDescription],Source.[ConflictFlag],Source.[SortOrder])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientExpertiseRating]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientExpertiseRating] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientExpertiseRating] OFF
GO