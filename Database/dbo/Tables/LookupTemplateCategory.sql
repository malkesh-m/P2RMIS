CREATE TABLE [dbo].[LookupTemplateCategory] (
    [CategoryID]   INT           IDENTITY (1, 1) NOT NULL,
    [CategoryName] NVARCHAR (50) NULL,
    CONSTRAINT [PK_LookupTemplateCategory] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);

