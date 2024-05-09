CREATE TABLE [dbo].[SystemTemplate] (
    [TemplateId] INT              IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (100)   NULL,
    [TypeID]     INT              NULL,
    [VersionId]  INT              NOT NULL,
    [CategoryID] INT              NULL,
    CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED ([TemplateId] ASC),
    CONSTRAINT [FK_SystemTemplate_LookupTemplateCategory] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[LookupTemplateCategory] ([CategoryID]),
    CONSTRAINT [FK_SystemTemplate_LookupTemplateType] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[LookupTemplateType] ([TypeID])
);

