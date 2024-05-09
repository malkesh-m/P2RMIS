
SET IDENTITY_INSERT [AcademicRank] ON

MERGE INTO [AcademicRank] AS Target
USING (VALUES
  (1,'Full Professor','Full Prof.',0)
 ,(2,'Assistant Professor','Asst. Prof.',1)
 ,(3,'Associate Professor','Asso. Prof.',2)
 ,(4,'Other Academic Rank','Other',3)
 ,(5,'None','None',4)
) AS Source ([AcademicRankId],[Rank],[RankAbbreviation],[SortOrder])
ON (Target.[AcademicRankId] = Source.[AcademicRankId])
WHEN MATCHED AND (
	NULLIF(Source.[Rank], Target.[Rank]) IS NOT NULL OR NULLIF(Target.[Rank], Source.[Rank]) IS NOT NULL OR 
	NULLIF(Target.[RankAbbreviation], Source.[RankAbbreviation]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [Rank] = Source.[Rank],
  [RankAbbreviation] = Source.[RankAbbreviation], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([AcademicRankId],[Rank],[RankAbbreviation],[SortOrder])
 VALUES(Source.[AcademicRankId],Source.[Rank],Source.[RankAbbreviation],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [AcademicRank]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[AcademicRank] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [AcademicRank] OFF
GO