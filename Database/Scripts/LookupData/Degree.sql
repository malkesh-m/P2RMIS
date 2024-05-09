SET IDENTITY_INSERT [Degree] ON

MERGE INTO [Degree] AS Target
USING (VALUES
  (1,'A.A.')
 ,(2,'A.B.')
 ,(3,'B.A.')
 ,(4,'B.S.')
 ,(5,'B.S.N.')
 ,(6,'B.Sc.')
 ,(7,'D.D.S.')
 ,(8,'D.O.')
 ,(9,'D.P.H.')
 ,(10,'D.S.W.')
 ,(11,'D.Sc.')
 ,(12,'D.V.M.')
 ,(13,'Dr.P.H.')
 ,(14,'Ed.D.')
 ,(15,'J.D.')
 ,(16,'M.A.')
 ,(17,'M.B.A.')
 ,(18,'M.F.A.')
 ,(19,'M.D.')
 ,(20,'M.I.S.')
 ,(21,'M.Ed.')
 ,(22,'M.P.H.')
 ,(23,'M.Phil.')
 ,(24,'M.S.')
 ,(25,'M.S.N.')
 ,(26,'M.S.W.')
 ,(27,'M.Sc.')
 ,(28,'Ph.D.')
 ,(29,'Pharm.D.')
 ,(30,'R.N.')
 ,(31,'Sc.D.')
 ,(32,'O.D.')
 ,(33,'P.T.')
 ,(34,'Other')
 ,(36,'M.D./Ph.D.')
 ,(37,'D.Phil.')
 ,(38,'D.M.D.')
) AS Source ([DegreeId],[DegreeName])
ON (Target.[DegreeId] = Source.[DegreeId])
WHEN MATCHED AND (
	NULLIF(Source.[DegreeName], Target.[DegreeName]) IS NOT NULL OR NULLIF(Target.[DegreeName], Source.[DegreeName]) IS NOT NULL) THEN
 UPDATE SET
  [DegreeName] = Source.[DegreeName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([DegreeId],[DegreeName])
 VALUES(Source.[DegreeId],Source.[DegreeName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [Degree]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[Degree] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [Degree] OFF
GO