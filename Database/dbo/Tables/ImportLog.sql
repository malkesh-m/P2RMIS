CREATE TABLE [dbo].[ImportLog]
(
	[ImportLogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientTransferTypeId] INT NOT NULL, 
    [SuccessFlag] BIT NULL, 
    [Url] VARCHAR(2000) NULL, 
    [Message] VARCHAR(2000) NULL, 
    [Content] VARCHAR(MAX) NULL, 
    [Timestamp] DATETIME2 NULL, 
    CONSTRAINT [FK_ImportLog_ClientTransferType] FOREIGN KEY ([ClientTransferTypeId]) REFERENCES [ClientTransferType]([ClientTransferTypeId])
)
