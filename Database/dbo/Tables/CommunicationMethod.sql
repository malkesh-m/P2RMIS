CREATE TABLE [dbo].[CommunicationMethod]
(
	[CommunicationMethodId] INT NOT NULL PRIMARY KEY, 
    [MethodName] VARCHAR(20) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a method of communication',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationMethod',
    @level2type = N'COLUMN',
    @level2name = N'CommunicationMethodId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name for the method of communication',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationMethod',
    @level2type = N'COLUMN',
    @level2name = N'MethodName'