CREATE TABLE [dbo].[UserAddress] (
    [AddressID]        INT              IDENTITY (1, 1) NOT NULL,
    [UserInfoID]       INT              NOT NULL,
	[LegacyAddressID]  INT				NULL,
	[AddressTypeId]    INT				NULL ,
	[PrimaryFlag] BIT NOT NULL DEFAULT 1,
    [Address1]         NVARCHAR (100)   NULL,
    [Address2]         NVARCHAR (100)   NULL,
    [Address3]         NVARCHAR (100)   NULL,
	[Address4]		   VARCHAR(100) NULL,
    [City]             NVARCHAR (100)   NULL,
	[StateId]		   INT				NULL,
    [StateOther]       NVARCHAR (100)   NULL,
    [Zip]              NVARCHAR (20)    NULL,
	[CountryId]		   INT				NULL,
    [CreatedBy]        INT              NULL,
    [CreatedDate]      DATETIME         NULL,
    [ModifiedBy]       INT              NULL,
    [ModifiedDate]     DATETIME         NULL,
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [PK_UserAddress] PRIMARY KEY CLUSTERED ([AddressID] ASC),
    CONSTRAINT [FK_UserAddress_UserInfo] FOREIGN KEY ([UserInfoID]) REFERENCES [dbo].[UserInfo] ([UserInfoID]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserAddress_State] FOREIGN KEY ([StateId]) REFERENCES [State]([StateId]), 
    CONSTRAINT [FK_UserAddress_Country] FOREIGN KEY ([CountryId]) REFERENCES [Country]([CountryId]), 
    CONSTRAINT [FK_UserAddress_AddressTypeId] FOREIGN KEY ([AddressTypeId]) REFERENCES [AddressType]([AddressTypeId])
);


GO

GO

GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Address information of a user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s mailing address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = N'COLUMN',
    @level2name = N'AddressID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s perosnal informaiton',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Deprecated: Not used',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = N'COLUMN',
    @level2name = N'StateOther'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the address is the user''s primary mailing address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = N'COLUMN',
    @level2name = N'PrimaryFlag'
GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'First line of user mailing address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = N'COLUMN',
    @level2name = N'Address1'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Second line of user mailing address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = N'COLUMN',
    @level2name = N'Address2'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Third line of user mailing address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = N'COLUMN',
    @level2name = N'Address3'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Fourth line of user mailing address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = N'COLUMN',
    @level2name = N'Address4'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'City of user mailing address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = N'COLUMN',
    @level2name = N'City'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Zip code of user mailing address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = N'COLUMN',
    @level2name = N'Zip'
GO

CREATE INDEX [IX_UserAddress_UserInfoID_PrimaryFlag] ON [dbo].[UserAddress] ([UserInfoID], [PrimaryFlag])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for state/province of user mailing address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = N'COLUMN',
    @level2name = N'StateId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for country of user mailing address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = N'COLUMN',
    @level2name = N'CountryId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the type of mailing address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = N'COLUMN',
    @level2name = N'AddressTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier for a person address for mapping purposes',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAddress',
    @level2type = N'COLUMN',
    @level2name = N'LegacyAddressID'

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[UserAddress] TO [web-p2rmis]
    AS [dbo];