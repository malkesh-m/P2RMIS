CREATE TABLE [dbo].[ClientTransferType]
(
	[ClientTransferTypeId] INT NOT NULL PRIMARY KEY, 
    [ClientId] INT NOT NULL, 
    [Label] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(500) NOT NULL, 
    [ApiPath] VARCHAR(500) NOT NULL, 
    [ExternalUrl] VARCHAR(200) NULL, 
	[ExternalUrlParameters] VARCHAR(500) NULL,
    [Credentials] VARCHAR(100) NULL, 
    [AuthenticationType] VARCHAR(20) NULL, 
    [ReturnFormat] VARCHAR(20) NULL, 
    CONSTRAINT [FK_ClientTransferType_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]) ,
	--[FileLocation] VARCHAR(100) NULL --Could be used for local files in the future
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Internal ApiPath to relay data to',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientTransferType',
    @level2type = N'COLUMN',
    @level2name = N'ApiPath'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'URL to download file to be imported',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientTransferType',
    @level2type = N'COLUMN',
    @level2name = N'ExternalUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Parameters to supply to external URL',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientTransferType',
    @level2type = N'COLUMN',
    @level2name = N'ExternalUrlParameters'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Encoded credentials to authenticate with external endpoint',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientTransferType',
    @level2type = N'COLUMN',
    @level2name = 'Credentials'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Type of authentication to use (e.g. Basic)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientTransferType',
    @level2type = N'COLUMN',
    @level2name = N'AuthenticationType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Content type of payload returned (e.g. XML) from external endpoint',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientTransferType',
    @level2type = N'COLUMN',
    @level2name = 'ReturnFormat'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description of the transfer type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientTransferType',
    @level2type = N'COLUMN',
    @level2name = N'Description'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Label for the transfer type to display in interface',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientTransferType',
    @level2type = N'COLUMN',
    @level2name = N'Label'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client/cusomter/tenant',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientTransferType',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a transfer type available to a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientTransferType',
    @level2type = N'COLUMN',
    @level2name = N'ClientTransferTypeId'