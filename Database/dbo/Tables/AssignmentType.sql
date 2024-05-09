CREATE TABLE [dbo].[AssignmentType]
(
	[AssignmentTypeId] INT NOT NULL PRIMARY KEY, 
    [AssignmentLabel] VARCHAR(50) NOT NULL, 
    [LegacyAssignmentId] INT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the type of assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AssignmentType',
    @level2type = N'COLUMN',
    @level2name = N'AssignmentTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'How the assignment should be labeled',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AssignmentType',
    @level2type = N'COLUMN',
    @level2name = N'AssignmentLabel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The legacy id for an assignment type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AssignmentType',
    @level2type = N'COLUMN',
    @level2name = N'LegacyAssignmentId'

GO
GRANT SELECT
    ON OBJECT::[dbo].[AssignmentType] TO [web-p2rmis]
    AS [dbo];