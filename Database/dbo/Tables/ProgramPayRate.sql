CREATE TABLE [dbo].[ProgramPayRate]
(
	[ProgramPayRateId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProgramYearId] INT NOT NULL, 
    [ClientParticipantTypeId] INT NOT NULL, 
	[ParticipantMethodId] INT NOT NULL DEFAULT 1,
	[RestrictedAssignedFlag] BIT NOT NULL DEFAULT 0,
	[MeetingTypeId] INT NOT NULL DEFAULT 1,	
	[EmploymentCategoryId] INT NULL,
    [HonorariumAccepted] VARCHAR(50) NOT NULL, 
    [ConsultantFeeText] VARCHAR(200) NOT NULL, 
    [ConsultantFee] MONEY NOT NULL DEFAULT 0, 
    [PeriodStartDate] datetime2(0) NOT NULL, 
    [PeriodEndDate] datetime2(0) NOT NULL, 
	[ManagerList] VARCHAR(500) NULL,
	[DescriptionOfWork] VARCHAR(8000) NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ProgramPayRate_ProgramYear] FOREIGN KEY ([ProgramYearId]) REFERENCES [ProgramYear]([ProgramYearId]), 
    CONSTRAINT [FK_ProgramPayRate_EmploymentCategory] FOREIGN KEY ([EmploymentCategoryId]) REFERENCES [EmploymentCategory]([EmploymentCategoryId]), 
    CONSTRAINT [FK_ProgramPayRate_ClientParticipantType] FOREIGN KEY ([ClientParticipantTypeId]) REFERENCES [ClientParticipantType]([ClientParticipantTypeId]), 
    CONSTRAINT [FK_ProgramPayRate_MeetingType] FOREIGN KEY ([MeetingTypeId]) REFERENCES [MeetingType]([MeetingTypeId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a participant pay rate specified at the program level',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPayRate',
    @level2type = N'COLUMN',
    @level2name = N'ProgramPayRateId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a yearly program offering',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPayRate',
    @level2type = N'COLUMN',
    @level2name = N'ProgramYearId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a participant type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPayRate',
    @level2type = N'COLUMN',
    @level2name = N'ClientParticipantTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the honorarium is accepted by the reviewer',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPayRate',
    @level2type = N'COLUMN',
    @level2name = N'HonorariumAccepted'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Text specification for a consultant fee',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPayRate',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantFeeText'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Monetary amount of the consultant fee',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPayRate',
    @level2type = N'COLUMN',
    @level2name = N'ConsultantFee'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date period of performance is expected to start',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPayRate',
    @level2type = N'COLUMN',
    @level2name = N'PeriodStartDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date period of performance is expected to end',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPayRate',
    @level2type = N'COLUMN',
    @level2name = N'PeriodEndDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Delimited list of manager''s to display on the contract',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPayRate',
    @level2type = N'COLUMN',
    @level2name = N'ManagerList'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description of work specified by the contract',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPayRate',
    @level2type = N'COLUMN',
    @level2name = N'DescriptionOfWork'
GO

CREATE INDEX [IX_ProgramPayRate_ProgramYearId_ClientParticipantTypeId] ON [dbo].[ProgramPayRate] ([ProgramYearId], [ClientParticipantTypeId])
GO
GRANT SELECT, INSERT, UPDATE
    ON OBJECT::[dbo].[ProgramPayRate] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Method in which reviewer is participating on panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPayRate',
    @level2type = N'COLUMN',
    @level2name = N'ParticipantMethodId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the reviewer is restricted to only seeing their assignments',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPayRate',
    @level2type = N'COLUMN',
    @level2name = N'RestrictedAssignedFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a meeting type the pay rate applies to',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPayRate',
    @level2type = N'COLUMN',
    @level2name = N'MeetingTypeId'