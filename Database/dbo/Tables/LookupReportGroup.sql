CREATE TABLE [dbo].[LookupReportGroup]
(
	[ReportGroupId] INT NOT NULL PRIMARY KEY, 
    [ReportGroupName] VARCHAR(50) NOT NULL, 
    [ReportDescription] VARCHAR(4000) NULL,
	[SortOrder] INT NOT NULL
	
)
