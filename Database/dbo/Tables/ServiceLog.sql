CREATE TABLE [dbo].[ServiceLog]
(
	[ServiceLogId] INT NOT NULL PRIMARY KEY IDENTITY,
	[ClientServiceAccountId] INT NULL, 
	[UserId] INT NULL,
    [RequestEndpoint] VARCHAR(50) NOT NULL, 
    [RequestHttpMethod] VARCHAR(20) NOT NULL, 
    [RequestParameters] VARCHAR(500) NULL, 
	[ResponseCode] VARCHAR(10) NULL,
    [ResponseMessage] VARCHAR(MAX) NULL, 
    [ResponseFileResult] VARBINARY(MAX) NULL, 
	[ResponseFileFormat] VARCHAR(50) NULL,
    [RequestDate] DATETIME2(3) NOT NULL DEFAULT dbo.GetP2rmisDateTime() 

)


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier of log entry',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ServiceLog',
    @level2type = N'COLUMN',
    @level2name = N'ServiceLogId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Service account initiating the service',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ServiceLog',
    @level2type = N'COLUMN',
    @level2name = N'ClientServiceAccountId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User account initiating the service',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ServiceLog',
    @level2type = N'COLUMN',
    @level2name = N'UserId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Endpoint of service called',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ServiceLog',
    @level2type = N'COLUMN',
    @level2name = 'RequestEndpoint'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'HttpMethod of call (e.g. Get or post)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ServiceLog',
    @level2type = N'COLUMN',
    @level2name = 'RequestHttpMethod'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Parameters supplied with request',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ServiceLog',
    @level2type = N'COLUMN',
    @level2name = 'RequestParameters'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Http response from P2RMIS',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ServiceLog',
    @level2type = N'COLUMN',
    @level2name = N'ResponseCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Message logged from P2RMIS',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ServiceLog',
    @level2type = N'COLUMN',
    @level2name = N'ResponseMessage'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'File contents returned from P2RMIS',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ServiceLog',
    @level2type = N'COLUMN',
    @level2name = N'ResponseFileResult'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'File format of return (e.g. XML, JSON)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ServiceLog',
    @level2type = N'COLUMN',
    @level2name = N'ResponseFileFormat'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date request was made',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ServiceLog',
    @level2type = N'COLUMN',
    @level2name = 'RequestDate'