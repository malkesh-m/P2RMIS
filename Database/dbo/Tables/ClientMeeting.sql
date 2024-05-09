CREATE TABLE [dbo].[ClientMeeting]
(
	[ClientMeetingId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LegacyMeetingId] VARCHAR(10) NULL, 
	[ClientId] INT NOT NULL,
    [MeetingAbbreviation] VARCHAR(20) NOT NULL, 
    [MeetingDescription] VARCHAR(100) NOT NULL, 
    [StartDate] datetime2(0) NOT NULL, 
    [EndDate] datetime2(0) NOT NULL, 
    [MeetingLocation] VARCHAR(20) NOT NULL, 
	[HotelId] INT NULL,
    [MeetingTypeId] INT NOT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ClientMeeting_MeetingType] FOREIGN KEY ([MeetingTypeId]) REFERENCES [MeetingType]([MeetingTypeId]), 
    CONSTRAINT [FK_ClientMeeting_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]), 
    CONSTRAINT [CK_ClientMeeting_EndDateGreaterStartDate] CHECK (EndDate >= StartDate), 
    CONSTRAINT [FK_ClientMeeting_Hotel] FOREIGN KEY ([HotelId]) REFERENCES [Hotel]([HotelId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a review meeting',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientMeeting',
    @level2type = N'COLUMN',
    @level2name = N'ClientMeetingId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy meeting identifier for mapping purposes ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientMeeting',
    @level2type = N'COLUMN',
    @level2name = N'LegacyMeetingId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Shortened name for a meeting',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientMeeting',
    @level2type = N'COLUMN',
    @level2name = N'MeetingAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Full name for a review meeting',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientMeeting',
    @level2type = N'COLUMN',
    @level2name = N'MeetingDescription'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date meeting begins',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientMeeting',
    @level2type = N'COLUMN',
    @level2name = N'StartDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date meeting ends',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientMeeting',
    @level2type = N'COLUMN',
    @level2name = N'EndDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Location where the meeting takes place',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientMeeting',
    @level2type = N'COLUMN',
    @level2name = N'MeetingLocation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the type a meeting is',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientMeeting',
    @level2type = N'COLUMN',
    @level2name = N'MeetingTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientMeeting',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO

CREATE INDEX [IX_ClientMeeting_StartDate_EndDate] ON [dbo].[ClientMeeting] ([StartDate], [EndDate])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Review meetings offered by a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientMeeting',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ClientMeeting] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for hotel in which lodging will be provided',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientMeeting',
    @level2type = N'COLUMN',
    @level2name = N'HotelId'
GO

CREATE NONCLUSTERED INDEX [IX_ClientMeeting_DeletedFlag_ClientMeetingId] ON [dbo].[ClientMeeting]
(
	[DeletedFlag] ASC,
	[ClientMeetingId] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
