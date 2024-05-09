CREATE TABLE [dbo].[LookupTemplateStage] (
    [TemplateStageID]   INT           IDENTITY (1, 1) NOT NULL,
    [TemplateStageName] NVARCHAR (50) NULL,
    CONSTRAINT [PK_LookupTemplateStatus] PRIMARY KEY CLUSTERED ([TemplateStageID] ASC)
);

