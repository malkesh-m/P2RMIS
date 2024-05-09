CREATE TABLE [dbo].[ReportPermission]
(
	[ReportPermId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ReportId] INT NOT NULL, 
    [OperationName] VARCHAR(50) NOT NULL, 
    CONSTRAINT [UC_ReportPermission_Unique] UNIQUE (ReportId, OperationName), 
    CONSTRAINT [FK_ReportPermission_Report] FOREIGN KEY ([ReportId]) REFERENCES [Report]([ReportId]) ON UPDATE CASCADE ON DELETE CASCADE
)

GO

CREATE INDEX [IX_ReportPermission_OperationName] ON [dbo].[ReportPermission] ([OperationName])
