CREATE TABLE [dbo].[MeetingRegistrationHotel]
(
	[MeetingRegistrationHotelId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MeetingRegistrationId] INT NOT NULL,
	[HotelNotRequiredFlag] BIT NOT NULL DEFAULT 1,
	[HotelId] INT NULL,
    [HotelCheckInDate] DATETIME2(0) NULL, 
    [HotelCheckOutDate] DATETIME2(0) NULL, 
    [HotelDoubleOccupancy] BIT NOT NULL DEFAULT 0, 
	[HotelAndFoodRequestComments] VARCHAR(500) NULL,
	[ExtraNightsRequestComments] VARCHAR(255) NULL,
	[LegacyHousingId] INT NULL,
	[CancellationFlag] BIT NOT NULL DEFAULT 0,
	[CancellationDate] DATETIME2(0) NULL,
	[ParticipantModifiedDate] DATETIME2(0) NULL,
	[IsDataComplete] bit not null default 0,
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_MeetingRegistrationHotel_MeetingRegistration] FOREIGN KEY ([MeetingRegistrationId]) REFERENCES [MeetingRegistration]([MeetingRegistrationId]), 
    CONSTRAINT [FK_MeetingRegistrationHotel_Hotel] FOREIGN KEY ([HotelId]) REFERENCES [Hotel]([HotelId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a meeting registration housing record',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationHotel',
    @level2type = N'COLUMN',
    @level2name = N'MeetingRegistrationHotelId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a meeting registration',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationHotel',
    @level2type = N'COLUMN',
    @level2name = N'MeetingRegistrationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether hotel accommadations are required for the user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationHotel',
    @level2type = N'COLUMN',
    @level2name = N'HotelNotRequiredFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Hotel check in date',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationHotel',
    @level2type = N'COLUMN',
    @level2name = N'HotelCheckInDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Hotel check out date',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationHotel',
    @level2type = N'COLUMN',
    @level2name = N'HotelCheckOutDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether a room that supports double occupancy is requested',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationHotel',
    @level2type = N'COLUMN',
    @level2name = N'HotelDoubleOccupancy'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Comments regarding hotel and food requests',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationHotel',
    @level2type = N'COLUMN',
    @level2name = N'HotelAndFoodRequestComments'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Comments regarding hotel requests for extra nights',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationHotel',
    @level2type = N'COLUMN',
    @level2name = N'ExtraNightsRequestComments'
GO

CREATE INDEX [IX_MeetingRegistrationHotel_MeetingRegistration] ON [dbo].[MeetingRegistrationHotel] ([MeetingRegistrationId],[DeletedFlag])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a legacy housing record',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationHotel',
    @level2type = N'COLUMN',
    @level2name = N'LegacyHousingId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the hotel in which the user is booked to stay',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationHotel',
    @level2type = N'COLUMN',
    @level2name = N'HotelId'
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[MeetingRegistrationHotel] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the hotel booking has been cancelled',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationHotel',
    @level2type = N'COLUMN',
    @level2name = N'CancellationFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date the hotel booking was cancelled',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationHotel',
    @level2type = N'COLUMN',
    @level2name = N'CancellationDate'