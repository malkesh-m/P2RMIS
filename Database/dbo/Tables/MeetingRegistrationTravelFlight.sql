CREATE TABLE [dbo].[MeetingRegistrationTravelFlight](
	[MeetingRegistrationTravelFlightId] [int] IDENTITY(1,1) NOT NULL,
	[MeetingRegistrationTravelId] [int] NOT NULL,
	[CarrierName] [varchar](100) NULL,
	[FlightNumber] [varchar](10) NULL,
	[DepartureCity] [varchar](100) NULL,
	[DepartureDate] [datetime2](0) NULL,
	[ArrivalCity] [varchar](100) NULL,
	[ArrivalDate] [datetime2](0) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime2](0) NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime2](0) NULL,
	[DeletedFlag] [bit] NOT NULL DEFAULT 0,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime2](0) NULL,
 CONSTRAINT [PK_MeetingRegistrationTravelFlight] PRIMARY KEY CLUSTERED 
(
	[MeetingRegistrationTravelFlightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], 
    CONSTRAINT [FK_MeetingRegistrationTravelFlight_ToTable] FOREIGN KEY ([MeetingRegistrationTravelId]) REFERENCES [MeetingRegistrationTravel]([MeetingRegistrationTravelId]),
	CONSTRAINT [FK_MeetingRegistrationTravelFlight_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [User]([UserId])
) ON [PRIMARY]

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Flight information identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravelFlight',
    @level2type = N'COLUMN',
    @level2name = N'MeetingRegistrationTravelFlightId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Travel identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravelFlight',
    @level2type = N'COLUMN',
    @level2name = N'MeetingRegistrationTravelId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Carrier name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravelFlight',
    @level2type = N'COLUMN',
    @level2name = N'CarrierName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Flight number',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravelFlight',
    @level2type = N'COLUMN',
    @level2name = N'FlightNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Departure city',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravelFlight',
    @level2type = N'COLUMN',
    @level2name = N'DepartureCity'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Departure date & time',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravelFlight',
    @level2type = N'COLUMN',
    @level2name = N'DepartureDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Arrival city',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravelFlight',
    @level2type = N'COLUMN',
    @level2name = N'ArrivalCity'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Arrival date & time',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationTravelFlight',
    @level2type = N'COLUMN',
    @level2name = N'ArrivalDate'
GO

CREATE INDEX [IX_MeetingRegistrationTravelFlight_MeetingRegistrationTravelId] ON [dbo].[MeetingRegistrationTravelFlight] ([MeetingRegistrationTravelId]) WHERE DeletedFlag = 0
