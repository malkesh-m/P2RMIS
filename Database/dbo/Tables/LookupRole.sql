CREATE TABLE [dbo].[LookupRole] (
    [RoleID]        INT           IDENTITY (1, 1) NOT NULL,
    [RoleName]      NVARCHAR (50) NULL,
    [RoleContext]   NVARCHAR (50) NULL,
    [RoleCode]      NVARCHAR (10) NULL,
    [PriorityOrder] INT           NULL,
    CONSTRAINT [PK_LookupRole] PRIMARY KEY CLUSTERED ([RoleID] ASC)
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user role',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'LookupRole',
    @level2type = N'COLUMN',
    @level2name = N'RoleID'