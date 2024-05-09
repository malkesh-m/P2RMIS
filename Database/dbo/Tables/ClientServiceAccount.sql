CREATE TABLE [dbo].[ClientServiceAccount]
(
	[ClientServiceAccountId] INT NOT NULL PRIMARY KEY, 
	[ClientId] INT NOT NULL,
    [Username] VARCHAR(100) NOT NULL, 
    [Password] VARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_ClientServiceAccount_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a service account associated',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientServiceAccount',
    @level2type = N'COLUMN',
    @level2name = N'ClientServiceAccountId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Client/customer/tenant identiifer',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientServiceAccount',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Username for service account',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientServiceAccount',
    @level2type = N'COLUMN',
    @level2name = N'Username'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Password',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientServiceAccount',
    @level2type = N'COLUMN',
    @level2name = N'Password'