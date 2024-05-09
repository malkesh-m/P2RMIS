CREATE TABLE [dbo].[ClientProgram]
(
	[ClientProgramId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[LegacyProgramId] VARCHAR(10) NULL,
	[ClientId] INT NOT NULL,
    [ProgramAbbreviation] VARCHAR(20) NOT NULL, 
    [ProgramDescription] VARCHAR(75) NOT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ClientProgram_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]), 
    CONSTRAINT [UN_ClientProgram_ClientId_ProgramAbbreviation] UNIQUE ([ClientId], [ProgramAbbreviation])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a research program',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientProgram',
    @level2type = N'COLUMN',
    @level2name = N'ClientProgramId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the program in legacy database',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientProgram',
    @level2type = N'COLUMN',
    @level2name = N'LegacyProgramId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Client identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientProgram',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Abbreviation for a research program',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientProgram',
    @level2type = N'COLUMN',
    @level2name = N'ProgramAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description for a research program',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientProgram',
    @level2type = N'COLUMN',
    @level2name = N'ProgramDescription'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Review programs administered for a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientProgram',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ClientProgram] TO [web-p2rmis]
    AS [dbo];
