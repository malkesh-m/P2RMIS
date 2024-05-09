CREATE TABLE [dbo].[MeetingRegistrationTravel]
(
	[MeetingRegistrationTravelId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MeetingRegistrationId] INT NOT NULL, 
    [TravelModeId] INT NULL,
	[TravelRequestComments] VARCHAR(500),
	[LegacyTravelId] INT NULL,
	[CancellationFlag] BIT NOT NULL DEFAULT 0,
	[CancellationDate] DATETIME2(0) NULL,
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
	[ReservationCode] VARCHAR(200) NULL,
	[Fare] Decimal(18, 2) NULL,
	[AgentFee] Decimal(18, 2) NULL,
	[AgentFee2] Decimal(18, 2) NULL,
	[ChangeFee] Decimal(18, 2) NULL,
	[Ground] Bit NOT NULL,
	[GsaRate] Decimal(18, 2) NULL,
	[NoGsa] Bit NOT NULL,
	[ClientApprovedAmount] Decimal(18, 2) NULL,
	[NteAmount] Decimal(18, 2) NULL,
	[IsDataComplete] bit not null default 0
    CONSTRAINT [FK_MeetingRegistrationTravel_MeetingRegistration] FOREIGN KEY ([MeetingRegistrationId]) REFERENCES [MeetingRegistration]([MeetingRegistrationId]), 
    CONSTRAINT [FK_MeetingRegistrationTravel_TravelMode] FOREIGN KEY ([TravelModeId]) REFERENCES [TravelMode]([TravelModeId]), 
)

GO

CREATE INDEX [IX_MeetingRegistrationTravel_MeetingRegistrationId] ON [dbo].[MeetingRegistrationTravel] ([MeetingRegistrationId],[DeletedFlag])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a meeting registration travel record',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'MeetingRegistrationTravelId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s meeting registration',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'MeetingRegistrationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Mode in travel occurs',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'TravelModeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Comments regarding the users travel request',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'TravelRequestComments'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a legacy travel record',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'LegacyTravelId'
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[MeetingRegistrationTravel] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Not to exceed amount for a travel request',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'NteAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the travel booking has been cancelled',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'CancellationFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date the hotel booking was cancelled',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'CancellationDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The reservation code',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'ReservationCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The fare used for travel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'Fare'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The fee required by the agent',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'AgentFee'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The fee required by the agent #2',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'AgentFee2'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The change fee required',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'ChangeFee'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Determines whether user is traveling by ground',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'Ground'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The Gsa Rate used',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'GsaRate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Determines whether gsa is used',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'NoGsa'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The client approved amount',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravel',
    @level2type = N'COLUMN',
    @level2name = N'ClientApprovedAmount'