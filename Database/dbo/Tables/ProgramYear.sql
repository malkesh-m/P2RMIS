
CREATE TABLE [dbo].[ProgramYear]
(
	[ProgramYearId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[LegacyProgramId] INT NULL DEFAULT NEXT VALUE FOR seq_ProgramYear_LegacyProgramId,
    [ClientProgramId] INT NOT NULL, 
    [Year] VARCHAR(8) NOT NULL, 
	[DateClosed] datetime2(0) NULL,
    [CreatedDate] datetime2(0) NULL, 
    [CreatedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ProgramYear_ClientProgram] FOREIGN KEY ([ClientProgramId]) REFERENCES [ClientProgram]([ClientProgramId]), 
    CONSTRAINT [UN_ProgramYear_ClientProgramId_Year] UNIQUE ([ClientProgramId], [Year], [DeletedDate])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an instance of a program for a fiscal year',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramYear',
    @level2type = N'COLUMN',
    @level2name = N'ProgramYearId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramYear',
    @level2type = N'COLUMN',
    @level2name = N'ClientProgramId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Year for the program',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramYear',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO

GO

GO

GO

CREATE INDEX [IX_ProgramYear_ClientProgramId_Year] ON [dbo].[ProgramYear] ([ClientProgramId], [Year])

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date the program instance becomes no longer available',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramYear',
    @level2type = N'COLUMN',
    @level2name = N'DateClosed'
GO

CREATE INDEX [IX_ProgramYear_DateClosed_Year_ClientProgramId] ON [dbo].[ProgramYear] ([DateClosed], [Year], [ClientProgramId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Yearly offering of a research program',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramYear',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program from legacy database',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramYear',
    @level2type = N'COLUMN',
    @level2name = N'LegacyProgramId'
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ProgramYear] TO [web-p2rmis]
    AS [dbo];
