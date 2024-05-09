CREATE TABLE [dbo].[SessionUserAssignment]
(
	[SessionUserAssignmentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL,
	[MeetingSessionId] INT NOT NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_SessionUserAssignment_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_SessionUserAssignment_MeetingSessionId] FOREIGN KEY ([MeetingSessionId]) REFERENCES [MeetingSession]([MeetingSessionId]), 
)

GO

CREATE INDEX [IX_SessionUserAssignment_MeetingSessionId] ON [dbo].[SessionUserAssignment] ([MeetingSessionId],[UserId])
