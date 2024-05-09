CREATE TABLE [dbo].[ProgramMechanismImportLog]
(
	[ProgramMechanismImportLogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProgramMechanismId] INT NOT NULL, 
    [ImportLogId] INT NOT NULL, 
    [CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ProgramMechanismImportLog_ProgramMechanism] FOREIGN KEY ([ProgramMechanismId]) REFERENCES [ProgramMechanism]([ProgramMechanismId]), 
    CONSTRAINT [FK_ProgramMechanismImportLog_ImportLog] FOREIGN KEY ([ImportLogId]) REFERENCES [ImportLog]([ImportLogId]), 
)

GO

CREATE INDEX [IX_ProgramMechanismImportLog_ProgramMechanismId_ImportLogId] ON [dbo].[ProgramMechanismImportLog] ([ProgramMechanismId],[ImportLogId])
