SET IDENTITY_INSERT [LookupDegree] ON
 
MERGE INTO [LookupDegree] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  (1,'')
 ,(2,'A.A.')
 ,(3,'A.B.')
 ,(4,'B.A.')
 ,(5,'B.S.')
 ,(6,'B.S.N.')
 ,(7,'B.Sc.')
 ,(8,'D.D.S.')
 ,(9,'D.O.')
 ,(10,'D.P.H.')
 ,(11,'D.S.W.')
 ,(12,'D.Sc.')
 ,(13,'D.V.M.')
 ,(14,'Dr.P.H.')
 ,(15,'Ed.D.')
 ,(16,'J.D.')
 ,(17,'M.A.')
 ,(18,'M.B.A.')
 ,(19,'M.F.A.')
 ,(20,'M.D.')
 ,(21,'M.I.S.')
 ,(22,'M.Ed.')
 ,(23,'M.P.H.')
 ,(24,'M.Phil.')
 ,(25,'M.S.')
 ,(26,'M.S.N.')
 ,(27,'M.S.W.')
 ,(28,'M.Sc.')
 ,(29,'Ph.D.')
 ,(30,'Pharm.D.')
 ,(31,'R.N.')
 ,(32,'Sc.D.')

) AS Source ([DegreeID],[DegreeName])
ON (Target.[DegreeID] = Source.[DegreeID])
WHEN MATCHED AND (Target.[DegreeName] <> Source.[DegreeName]) THEN
 UPDATE SET
 [DegreeName] = Source.[DegreeName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([DegreeID],[DegreeName])
 VALUES(Source.[DegreeID],Source.[DegreeName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
 
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [LookupDegree]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[LookupDegree] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
 
SET IDENTITY_INSERT [LookupDegree] OFF
GO