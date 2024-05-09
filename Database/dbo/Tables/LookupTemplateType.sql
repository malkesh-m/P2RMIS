CREATE TABLE [dbo].[LookupTemplateType] (
    [TypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [TypeName] NVARCHAR (50) NULL,
    CONSTRAINT [PK_LookupTemplateType] PRIMARY KEY CLUSTERED ([TypeID] ASC)
);

