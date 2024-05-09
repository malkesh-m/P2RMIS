CREATE TABLE [dbo].[Report](
	[ReportId] [int] IDENTITY(1,1) NOT NULL,
	[ReportName] [nvarchar](100) NOT NULL,
	[ReportFileName] [nvarchar](50) NOT NULL, 
	[ReportDescription] [nvarchar](4000) NULL,
    [Active] BIT NOT NULL DEFAULT 1, 
	[ReportGroupId] [int] NOT NULL,
	[ReportParameterGroupId] [int] NULL,
	[ReportParameterGroupDesc] [varchar](25) NULL,
    CONSTRAINT [PK_Report] PRIMARY KEY (ReportId), 
    CONSTRAINT [FK_Report_ReportGroup] FOREIGN KEY ([ReportGroupId]) REFERENCES [LookupReportGroup]([ReportGroupId])
) 
