CREATE TABLE [dbo].[PanelUserAssignment]
(
	[PanelUserAssignmentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SessionPanelId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [ClientParticipantTypeId] INT NOT NULL, 
    [ClientRoleId] INT NULL, 
	[LegacyParticipantId] INT NULL,
	[NotificationDateSent] datetime2(0) NULL,
	[RestrictedAssignedFlag] BIT NOT NULL DEFAULT 0,
	[ParticipationMethodId] INT NOT NULL ,
	[ClientApprovalFlag] BIT NULL,
	[ClientApprovalBy] INT NULL,
	[ClientApprovalDate] DATETIME2(0) NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PanelUserAssignment_SessionPanel] FOREIGN KEY ([SessionPanelId]) REFERENCES [SessionPanel]([SessionPanelId]), 
    CONSTRAINT [FK_PanelUserAssignment_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_PanelUserAssignment_ClientParticipantType] FOREIGN KEY ([ClientParticipantTypeId]) REFERENCES [ClientParticipantType]([ClientParticipantTypeId]), 
    CONSTRAINT [FK_PanelUserAssignment_ClientRole] FOREIGN KEY ([ClientRoleId]) REFERENCES [ClientRole]([ClientRoleId]), 
    CONSTRAINT [FK_PanelUserAssignment_ParticipationMethod] FOREIGN KEY ([ParticipationMethodId]) REFERENCES [ParticipationMethod]([ParticipationMethodId]), 
    CONSTRAINT [FK_PanelUserAssignment_User_ClientApproval] FOREIGN KEY ([ClientApprovalBy]) REFERENCES [User]([UserId]), 
    --TODO: After some data cleansing we can re-enable this unique constraint
	--CONSTRAINT [UN_PanelUserAssignment_SessionPanelId_UserId_ClientParticipantTypeId] UNIQUE ([SessionPanelId], [UserId], [ClientParticipantTypeId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user assignment to a review panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserAssignment',
    @level2type = N'COLUMN',
    @level2name = N'PanelUserAssignmentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a review panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserAssignment',
    @level2type = N'COLUMN',
    @level2name = N'SessionPanelId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserAssignment',
    @level2type = N'COLUMN',
    @level2name = N'UserId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User assignments to a review panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserAssignment',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Type of participation the user will be considered to serve the panel as',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserAssignment',
    @level2type = N'COLUMN',
    @level2name = N'ClientParticipantTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The role the user will be considered on the panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserAssignment',
    @level2type = N'COLUMN',
    @level2name = N'ClientRoleId'
GO

CREATE INDEX [IX_PanelUserAssignment_SessionPanelId_UserId] ON [dbo].[PanelUserAssignment] ([SessionPanelId], [UserId])

GO

CREATE INDEX [IX_PanelUserAssignment_UserId] ON [dbo].[PanelUserAssignment] ([UserId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier for a user participation for mapping',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserAssignment',
    @level2type = N'COLUMN',
    @level2name = N'LegacyParticipantId'

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[PanelUserAssignment] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date a notification was sent to user notifying about their panel assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserAssignment',
    @level2type = N'COLUMN',
    @level2name = N'NotificationDateSent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the user is limited to seeing only their assigned applications',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserAssignment',
    @level2type = N'COLUMN',
    @level2name = 'RestrictedAssignedFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The method (in-person, etc) in which the user will participate on the panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserAssignment',
    @level2type = N'COLUMN',
    @level2name = 'ParticipationMethodId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the assignment was approved or disapproved by client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserAssignment',
    @level2type = N'COLUMN',
    @level2name = N'ClientApprovalFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The user who marked the client approval',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserAssignment',
    @level2type = N'COLUMN',
    @level2name = N'ClientApprovalBy'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date the client approval was flagged',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserAssignment',
    @level2type = N'COLUMN',
    @level2name = N'ClientApprovalDate'
GO

CREATE NONCLUSTERED INDEX [IX_PanelUserAssignment_PanelUserAssignmentId_SessionPanelId_UserId] ON [dbo].[PanelUserAssignment]
(
	[PanelUserAssignmentId] ASC,
	[SessionPanelId] ASC,
	[UserId] ASC,
	[ClientParticipantTypeId] ASC,
	[ParticipationMethodId] ASC,
	[ClientRoleId] ASC
)
INCLUDE ( 	[RestrictedAssignedFlag]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [IX_PanelUserAssignment_SessionPanelId_PanelUserAssignmentId_ClientRoleId] ON [dbo].[PanelUserAssignment]
(
	[SessionPanelId] ASC,
	[PanelUserAssignmentId] ASC,
	[ClientRoleId] ASC,
	[ClientParticipantTypeId] ASC,
	[UserId] ASC,
	[ParticipationMethodId] ASC
)
INCLUDE ( 	[RestrictedAssignedFlag]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
