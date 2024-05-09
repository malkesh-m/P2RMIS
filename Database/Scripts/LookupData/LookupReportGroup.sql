MERGE INTO [LookupReportGroup] AS Target
USING (VALUES
  (1,'Applications','',1)
 ,(2,'Meeting Logistics','',3)
 ,(3,'Review Management','',4)
 ,(4,'Scoring','',5)
 ,(5,'Summary Statements','',6)
 ,(6,'Deliverables','',2)
 ,(7,'Consumer Management','',7)
 ,(8,'Programs','',8)
 ,(9,'SRO/RTA','',9)
) AS Source ([ReportGroupId],[ReportGroupName],[ReportDescription],[SortOrder])
ON (Target.[ReportGroupId] = Source.[ReportGroupId])
WHEN MATCHED AND (
	NULLIF(Source.[ReportGroupName], Target.[ReportGroupName]) IS NOT NULL OR NULLIF(Target.[ReportGroupName], Source.[ReportGroupName]) IS NOT NULL OR 
	NULLIF(Source.[ReportDescription], Target.[ReportDescription]) IS NOT NULL OR NULLIF(Target.[ReportDescription], Source.[ReportDescription]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [ReportGroupName] = Source.[ReportGroupName], 
  [ReportDescription] = Source.[ReportDescription], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ReportGroupId],[ReportGroupName],[ReportDescription],[SortOrder])
 VALUES(Source.[ReportGroupId],Source.[ReportGroupName],Source.[ReportDescription],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [lookupreportGroup]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[lookupreportGroup] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO