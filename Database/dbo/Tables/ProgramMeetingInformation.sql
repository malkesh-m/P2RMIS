CREATE TABLE [dbo].[ProgramMeetingInformation]
(
	[ProgramMeetingInformationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProgramYearId] INT NOT NULL,
	[DocumentName] VARCHAR(200) NOT NULL, 
    [DocumentDescription] VARCHAR(1000) NULL, 
    [FileLocation] VARCHAR(500) NOT NULL, 
    [ExternalAddressFlag] BIT NOT NULL DEFAULT 0,
	[ActiveFlag] BIT NOT NULL DEFAULT 1,
	[LegacyMiId] INT NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ProgramMeetingInformation_ProgramYear] FOREIGN KEY ([ProgramYearId]) REFERENCES [ProgramYear]([ProgramYearId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a meeting information document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMeetingInformation',
    @level2type = N'COLUMN',
    @level2name = N'ProgramMeetingInformationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program year',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMeetingInformation',
    @level2type = N'COLUMN',
    @level2name = N'ProgramYearId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Display name of the meeting document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMeetingInformation',
    @level2type = N'COLUMN',
    @level2name = N'DocumentName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description of the meeting document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMeetingInformation',
    @level2type = N'COLUMN',
    @level2name = N'DocumentDescription'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Url of the meeting document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMeetingInformation',
    @level2type = N'COLUMN',
    @level2name = N'FileLocation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the document is stored externally or locally',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMeetingInformation',
    @level2type = N'COLUMN',
    @level2name = N'ExternalAddressFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the document should actively display to users',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMeetingInformation',
    @level2type = N'COLUMN',
    @level2name = N'ActiveFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier for a meeting information document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMeetingInformation',
    @level2type = N'COLUMN',
    @level2name = N'LegacyMiId'
GO

CREATE INDEX [IX_ProgramMeetingInformation_ProgramYearId] ON [dbo].[ProgramMeetingInformation] ([ProgramYearId],[DeletedFlag])
