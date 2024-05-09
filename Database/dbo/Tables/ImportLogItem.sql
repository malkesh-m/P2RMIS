CREATE TABLE [dbo].[ImportLogItem]
(
	[ImportLogItemId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ImportLogId] INT NOT NULL, 
    [Key] VARCHAR(50) NULL, 
    [SuccessFlag] BIT NOT NULL DEFAULT 0, 
    [Message] VARCHAR(2000) NULL, 
    [Content] VARCHAR(MAX) NULL, 
    CONSTRAINT [FK_ImportLogItem_ImportLog] FOREIGN KEY ([ImportLogId]) REFERENCES [ImportLog]([ImportLogId])
)

GO

CREATE INDEX [IX_ImportLogItem_ImportLogId] ON [dbo].[ImportLogItem] ([ImportLogId])
