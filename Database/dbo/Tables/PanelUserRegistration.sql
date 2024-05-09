CREATE TABLE [dbo].[PanelUserRegistration]
(
	[PanelUserRegistrationId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ClientRegistrationId] INT NOT NULL,
    [PanelUserAssignmentId] INT NOT NULL, 
    [RegistrationStartDate] datetime2(0) NULL, 
    [RegistrationCompletedDate] datetime2(0) NULL, 
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL,
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL,
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PanelUserRegistration_PanelUserAssignment] FOREIGN KEY ([PanelUserAssignmentId]) REFERENCES [PanelUserAssignment]([PanelUserAssignmentId]), 
    CONSTRAINT [FK_PanelUserRegistration_ClientRegistration] FOREIGN KEY ([ClientRegistrationId]) REFERENCES [ClientRegistration]([ClientRegistrationId]), 
)

GO

CREATE INDEX [IX_PanelUserRegistration_PanelUserAssignmentId_ClientRegistrationId] ON [dbo].[PanelUserRegistration] ([PanelUserAssignmentId],[ClientRegistrationId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel user''s registration instance',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistration',
    @level2type = N'COLUMN',
    @level2name = N'PanelUserRegistrationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client registration',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistration',
    @level2type = N'COLUMN',
    @level2name = N'ClientRegistrationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel user assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistration',
    @level2type = N'COLUMN',
    @level2name = N'PanelUserAssignmentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date the panel registration was started by the user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistration',
    @level2type = N'COLUMN',
    @level2name = N'RegistrationStartDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date the panel registration was completed by the user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistration',
    @level2type = N'COLUMN',
    @level2name = N'RegistrationCompletedDate'
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[PanelUserRegistration] TO [web-p2rmis]
    AS [dbo];
