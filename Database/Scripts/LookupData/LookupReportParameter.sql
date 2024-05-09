MERGE INTO [LookupReportParameter] AS Target
USING (VALUES
  (0,'','')
 ,(1,'Panel','int')
 ,(2,'Cycle','int')
 ,(3,'Meeting','string')
 ,(4,'Session','string')
 ,(5,'Program','int')
 ,(6,'Year','int')
 ,(7,'MeetingType', 'int')
 
) AS Source ([ParameterId],[ParamName],[DataType])
ON (Target.[ParameterId] = Source.[ParameterId])
WHEN MATCHED AND (
	NULLIF(Source.[ParamName], Target.[ParamName]) IS NOT NULL OR NULLIF(Target.[ParamName], Source.[ParamName]) IS NOT NULL OR 
	NULLIF(Source.[DataType], Target.[DataType]) IS NOT NULL OR NULLIF(Target.[DataType], Source.[DataType]) IS NOT NULL) THEN
 UPDATE SET
  [ParamName] = Source.[ParamName], 
  [DataType] = Source.[DataType]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ParameterId],[ParamName],[DataType])
 VALUES(Source.[ParameterId],Source.[ParamName],Source.[DataType])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [LookupReportParameter]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[LookupReportParameter] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO