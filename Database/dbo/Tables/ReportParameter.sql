CREATE TABLE [dbo].[ReportParameter]
(
	[ReportParamId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ReportId] INT NOT NULL, 
    [ParameterId] INT NOT NULL, 
	[Required] BIT NOT NULL DEFAULT 1,
	[MultiSelect] BIT NOT NULL DEFAULT 1
    CONSTRAINT [UN_ReportParameter_ReportIdParameterId] UNIQUE (ReportId, ParameterId), 
    CONSTRAINT [FK_ReportParameter_Report] FOREIGN KEY ([ReportId]) REFERENCES [Report]([ReportId]) ON UPDATE CASCADE ON DELETE CASCADE, 
    CONSTRAINT [FK_ReportParameter_LookupReportParameter] FOREIGN KEY ([ParameterId]) REFERENCES [LookupReportParameter]([ParameterId])
)
