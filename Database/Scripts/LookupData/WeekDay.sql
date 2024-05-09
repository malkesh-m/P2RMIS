MERGE INTO [WeekDay] AS Target
USING (VALUES
  (1,'Monday','M',1)
 ,(2,'Tuesday','Tu',2) 
 ,(3,'Wednesday','W',3) 
 ,(4,'Thursday','Th',4) 
 ,(5,'Friday','F',5) 
 ,(6,'Saturday','Sa',6) 
 ,(7,'Sunday','Su',7) 
) AS Source ([WeekDayId],[Name],[Abbreviation],[SortOrder])
ON (Target.[WeekDayId] = Source.[WeekDayId])
WHEN MATCHED AND (
	NULLIF(Source.[Name], Target.[Name]) IS NOT NULL OR NULLIF(Target.[Name], Source.[Name]) IS NOT NULL OR 
	NULLIF(Source.[Abbreviation], Target.[Abbreviation]) IS NOT NULL OR NULLIF(Target.[Abbreviation], Source.[Abbreviation]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [Name] = Source.[Name], 
  [Abbreviation] = source.[Abbreviation],
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([WeekDayId],[Name],[Abbreviation],[SortOrder])
 VALUES(Source.[WeekDayId],Source.[Name],Source.[Abbreviation],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [WeekDay]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[WeekDay] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
