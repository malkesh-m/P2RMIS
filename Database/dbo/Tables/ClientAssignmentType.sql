CREATE TABLE [dbo].[ClientAssignmentType]
(
	[ClientAssignmentTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NOT NULL, 
    [AssignmentTypeId] INT NOT NULL, 
    [AssignmentAbbreviation] VARCHAR(8) NOT NULL, 
    [AssignmentLabel] VARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_ClientAssignmentType_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]), 
    CONSTRAINT [FK_ClientAssignmentType_AssignmentTypeId] FOREIGN KEY ([AssignmentTypeId]) REFERENCES [AssignmentType]([AssignmentTypeId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientAssignmentType',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client instance of an assignment type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientAssignmentType',
    @level2type = N'COLUMN',
    @level2name = N'ClientAssignmentTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a system assignment type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientAssignmentType',
    @level2type = N'COLUMN',
    @level2name = N'AssignmentTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Abbreviation for the assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientAssignmentType',
    @level2type = N'COLUMN',
    @level2name = N'AssignmentAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Label/name for the assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientAssignmentType',
    @level2type = N'COLUMN',
    @level2name = N'AssignmentLabel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Assignment types available to a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientAssignmentType',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT
    ON OBJECT::[dbo].[ClientAssignmentType] TO [web-p2rmis]
    AS [dbo];