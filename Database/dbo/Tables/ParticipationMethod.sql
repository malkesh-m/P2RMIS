CREATE TABLE [dbo].[ParticipationMethod]
(
	[ParticipationMethodId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ParticipationMethodLabel] VARCHAR(20) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the method (in-person, etc) in which the user will participate on the panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ParticipationMethod',
    @level2type = N'COLUMN',
    @level2name = N'ParticipationMethodId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Display label for the method of participation',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ParticipationMethod',
    @level2type = N'COLUMN',
    @level2name = N'ParticipationMethodLabel'