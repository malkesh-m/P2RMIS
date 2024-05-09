-- Load data from p2rmis.dbo.SS_Topic_Area to p2rmisdb.dbo.ApplicationTopicCodes

MERGE ApplicationTopicCodes AS SOURCE 
USING (select p2.ApplicationId, p1.[Topic_Area], log_no,	0 as DeletedFlag
	   from [$(P2RMIS)].[dbo].[SS_Topic_Area] p1
	   left join	[dbo].[Application] p2 on p1.Log_No = p2.LogNumber where p2.ApplicationId is not null) AS TARGET 
ON (TARGET.ApplicationId = SOURCE.ApplicationId) 
WHEN NOT MATCHED BY TARGET 
THEN INSERT (	[ApplicationId], [TopicCode], [DeletedFlag]) 
	 values	(TARGET.ApplicationId, TARGET.[Topic_Area], TARGET.DeletedFlag);