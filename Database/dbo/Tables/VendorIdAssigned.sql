CREATE TABLE [dbo].[VendorIdAssigned]
(
	[VendorIdAssignedId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VendorId] NVARCHAR(10) NULL, 
    [AssignedFlag] BIT NOT NULL DEFAULT 0,
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
)

GO

CREATE UNIQUE INDEX [UIX_VendorIdAssigned_VendorId] ON [dbo].[VendorIdAssigned] ([VendorId]) WHERE (AssignedFlag = 0 AND DeletedFlag = 0)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Surrogate key for vendor ID assignment record',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'VendorIdAssigned',
    @level2type = N'COLUMN',
    @level2name = N'VendorIdAssignedId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'VendorId available for assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'VendorIdAssigned',
    @level2type = N'COLUMN',
    @level2name = N'VendorId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the vendor ID has been assigned',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'VendorIdAssigned',
    @level2type = N'COLUMN',
    @level2name = N'AssignedFlag'
	GO
	GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[VendorIdAssigned] TO [web-p2rmis]
    AS [dbo];