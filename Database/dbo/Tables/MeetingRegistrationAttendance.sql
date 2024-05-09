CREATE TABLE [dbo].[MeetingRegistrationAttendance]
(
	[MeetingRegistrationAttendanceId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MeetingRegistrationId] INT NOT NULL, 
    [AttendanceStartDate] DATETIME2(0) NULL, 
    [AttendanceEndDate] DATETIME2(0) NULL, 
    [MealRequestComments] VARCHAR(255) NULL,
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_MeetingRegistrationAttendance_MeetingRegistrationId] FOREIGN KEY ([MeetingRegistrationId]) REFERENCES [MeetingRegistration]([MeetingRegistrationId]), 
)

GO

CREATE INDEX [IX_MeetingRegistrationAttendance_MeetingRegistrationId] ON [dbo].[MeetingRegistrationAttendance] ([MeetingRegistrationId],[DeletedFlag])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a meeting attendance record',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationAttendance',
    @level2type = N'COLUMN',
    @level2name = N'MeetingRegistrationAttendanceId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a meeting registration',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationAttendance',
    @level2type = N'COLUMN',
    @level2name = N'MeetingRegistrationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date meeting attendance is expected to start',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationAttendance',
    @level2type = N'COLUMN',
    @level2name = N'AttendanceStartDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date meeting attendance is expected to end',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationAttendance',
    @level2type = N'COLUMN',
    @level2name = N'AttendanceEndDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Special requests related to food and beverage',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationAttendance',
    @level2type = N'COLUMN',
    @level2name = N'MealRequestComments'
GO
GRANT SELECT, INSERT, UPDATE
    ON OBJECT::[dbo].[MeetingRegistrationAttendance] TO [web-p2rmis]
    AS [dbo];
GO