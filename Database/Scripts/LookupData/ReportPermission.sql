SET IDENTITY_INSERT [ReportPermission] ON

MERGE INTO [ReportPermission] AS Target
USING (VALUES
  (1,1,'View Reports')
 ,(2,2,'View Reports')
 ,(3,3,'View Reports')
 ,(4,4,'View Reports')
 ,(5,5,'View Reports')
 ,(6,6,'View Scientific Admin Reports')
 ,(7,9,'View Reports')
 ,(9,10,'View Reports')
 ,(10,13,'View Reports')
 ,(11,14,'View Reports')
 ,(12,15,'View Reports')
 ,(13,16,'View Reports')
 ,(15,18,'View Reports')
 ,(16,19,'View Reports')
 ,(23,22,'View Reports')
 ,(24,23,'View Scientific Admin Reports')
 ,(26,24,'View Reports')
 ,(28,28,'View Scientific Admin Reports')
 ,(29,30,'View Reports')
 ,(30,31,'View Reports')
 ,(31,32,'View Reports')
 ,(32,33,'View Reports')
 ,(33,34,'View Reports')
 ,(34,35,'View Reports')
 ,(35,36,'View Reports')
 ,(36,37,'View Reports')
 ,(37,38,'View Reports')
 ,(38,39,'View Reports')
 ,(39,40,'View Reports')
 ,(40,41,'View Reports')
 ,(41,23,'View Reports')
 ,(42,6,'View Reports')
 ,(43,28,'View Reports')
 ,(44,13,'View Client Reports')
 ,(45,2,'View Client Reports')
 ,(46,14,'View Client Reports')
 ,(47,1,'View Client Reports')
 ,(48,23,'View Client Reports')
 ,(49,35,'View Client Reports')
 ,(50,16,'View Client Reports')
 ,(51,15,'View Client Reports')
 ,(52,24,'View Client Reports')
 ,(53,31,'View Client Reports')
 ,(54,30,'View Client Reports')
 ,(55,18,'View Client Reports')
 ,(57,42,'View Reports')
 ,(59,42,'View Scientific Admin Reports')
 ,(60,42,'View Client Reports')
 ,(61,13,'View Scientific Admin Reports')
 ,(65,45,'View Reports')
 ,(69,50, 'View Reports')
 ,(73,55, 'View Reports')
 ,(77,60,'View Reports')
 ,(78,1,'View Scientific Admin Reports')
 ,(81,65,'View Reports')
 ,(82,66,'View Reports')
 ,(83,67,'View Reports')
 ,(84,68,'View Reports')
 ,(85,68,'View Scientific Admin Reports')
 ,(86,69,'View Reports')
 ,(87,69,'View Scientific Admin Reports')
 ,(88,70,'View Reports')
 ,(89,71,'View Reports')
 ,(90,50,'View Scientific Admin Reports')
 ,(91,72,'View Reports')
 ,(92,73, 'View Reports')
 ,(93,74,'View Reports')
 ,(95,90,'View Reports')
 ,(98,91,'View Reports')
 ,(99,91,'View Client Reports')
 ,(100,91,'View Scientific Admin Reports')
 ,(101,75,'View CR Reports')
 ,(104,92,'View Reports') 
 ,(105,77,'View CR Reports') 
 ,(106,76,'View CR Reports') 
 ,(107,78,'View Reports')
 ,(108,79,'View Reports')
 ,(109,93,'View Scientific Admin Reports')
 ,(110,93,'View Reports') 
 ,(111,94,'View Reports')
 ,(112,94,'View Scientific Admin Reports')
 ,(113,95,'View Reports')
 ,(114,96,'View Scientific Admin Reports')
 ,(115,96,'View Reports') 
) AS Source ([ReportPermId],[ReportId],[OperationName])
ON (Target.[ReportPermId] = Source.[ReportPermId])
WHEN MATCHED AND (
	NULLIF(Source.[ReportId], Target.[ReportId]) IS NOT NULL OR NULLIF(Target.[ReportId], Source.[ReportId]) IS NOT NULL OR 
	NULLIF(Source.[OperationName], Target.[OperationName]) IS NOT NULL OR NULLIF(Target.[OperationName], Source.[OperationName]) IS NOT NULL) THEN
 UPDATE SET
  [ReportId] = Source.[ReportId], 
  [OperationName] = Source.[OperationName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ReportPermId],[ReportId],[OperationName])
 VALUES(Source.[ReportPermId],Source.[ReportId],Source.[OperationName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ReportPermission]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ReportPermission] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ReportPermission] OFF
GO