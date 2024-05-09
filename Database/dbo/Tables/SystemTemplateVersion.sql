CREATE TABLE [dbo].[SystemTemplateVersion] (
    [VersionId]       INT              IDENTITY (1, 1) NOT NULL,
    [TemplateId]      INT              NOT NULL,
    [Subject]         NVARCHAR (100)   NULL,
    [Description]     NVARCHAR (100)   NULL,
    [VersionNumber]   INT              NOT NULL,
    [Body]            NVARCHAR (MAX)   NULL,
    [TemplateStageID] INT              NULL,
    [SentBy]          INT              NULL,
    [SentDate]        DATETIME         NULL,
    [CreatedBy]       INT              NULL,
    [CreatedDate]     DATETIME         NULL,
    [ModifiedBy]      INT              NULL,
    [ModifiedDate]    DATETIME         NULL,
    CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED ([VersionId] ASC),
    CONSTRAINT [FK_SystemTemplateVersion_LookupTemplateStage] FOREIGN KEY ([TemplateStageID]) REFERENCES [dbo].[LookupTemplateStage] ([TemplateStageID]),
    CONSTRAINT [FK_SystemTemplateVersion_SystemTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[SystemTemplate] ([TemplateId])
);

