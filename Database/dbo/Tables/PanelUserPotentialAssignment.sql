CREATE TABLE [dbo].[PanelUserPotentialAssignment]
(
	[PanelUserPotentialAssignmentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SessionPanelId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [ClientParticipantTypeId] INT NULL, 
    [ClientRoleId] INT NULL, 
	[RestrictedAssignedFlag] BIT NOT NULL DEFAULT 0,
	[ParticipationMethodId] INT NULL ,
	[RecruitedFlag] BIT NOT NULL DEFAULT 0,
	[RecruitedDate] DATETIME2(0) NULL,
	[ClientApprovalFlag] BIT NULL,
	[ClientApprovalBy] INT NULL,
	[ClientApprovalDate] DATETIME2(0) NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] SMALLDATETIME NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] SMALLDATETIME NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] SMALLDATETIME NULL, 
    CONSTRAINT [FK_PanelUserPotentialAssignment_SessionPanel] FOREIGN KEY ([SessionPanelId]) REFERENCES [SessionPanel]([SessionPanelId]), 
    CONSTRAINT [FK_PanelUserPotentialAssignment_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_PanelUserPotentialAssignment_ClientParticipantType] FOREIGN KEY ([ClientParticipantTypeId]) REFERENCES [ClientParticipantType]([ClientParticipantTypeId]), 
    CONSTRAINT [FK_PanelUserPotentialAssignment_ClientRole] FOREIGN KEY ([ClientRoleId]) REFERENCES [ClientRole]([ClientRoleId]), 
    CONSTRAINT [FK_PanelUserPotentialAssignment_ParticipationMethod] FOREIGN KEY ([ParticipationMethodId]) REFERENCES [ParticipationMethod]([ParticipationMethodId]), 
    CONSTRAINT [FK_PanelUserPotentialAssignment_ClientApprovalBy] FOREIGN KEY ([ClientApprovalBy]) REFERENCES [User]([UserId]), 
)
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user assignment to a review panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserPotentialAssignment',
    @level2type = N'COLUMN',
    @level2name = 'PanelUserPotentialAssignmentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a review panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserPotentialAssignment',
    @level2type = N'COLUMN',
    @level2name = N'SessionPanelId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserPotentialAssignment',
    @level2type = N'COLUMN',
    @level2name = N'UserId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User assignments to a review panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserPotentialAssignment',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Type of participation the user will be considered to serve the panel as',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserPotentialAssignment',
    @level2type = N'COLUMN',
    @level2name = N'ClientParticipantTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The role the user will be considered on the panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserPotentialAssignment',
    @level2type = N'COLUMN',
    @level2name = N'ClientRoleId'
GO

CREATE INDEX [IX_PanelUserPotentialAssignment_SessionPanelId_RecruitedFlag_DeletedFlag] ON [dbo].[PanelUserPotentialAssignment] ([SessionPanelId], [RecruitedFlag], [DeletedFlag])
GO

CREATE INDEX [IX_PanelUserPotentialAssignment_UserId_RecruitedFlag_DeletedFlag] ON [dbo].[PanelUserPotentialAssignment] ([UserId], [RecruitedFlag], [DeletedFlag])
GO


EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the user is limited to seeing only their assigned applications',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserPotentialAssignment',
    @level2type = N'COLUMN',
    @level2name = 'RestrictedAssignedFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The method (in-person, etc) in which the user will participate on the panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserPotentialAssignment',
    @level2type = N'COLUMN',
    @level2name = N'ParticipationMethodId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the user has been recruited as a full participant to the panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserPotentialAssignment',
    @level2type = N'COLUMN',
    @level2name = N'RecruitedFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date the user was recruited as a full participant on the panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserPotentialAssignment',
    @level2type = N'COLUMN',
    @level2name = N'RecruitedDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the assignment was approved or disapproved by client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserPotentialAssignment',
    @level2type = N'COLUMN',
    @level2name = N'ClientApprovalFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The user who marked the client approval',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserPotentialAssignment',
    @level2type = N'COLUMN',
    @level2name = N'ClientApprovalBy'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date the client approval was flagged',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserPotentialAssignment',
    @level2type = N'COLUMN',
    @level2name = N'ClientApprovalDate'