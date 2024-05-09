CREATE TABLE [dbo].[NominatingOrganization](
	[OrganizationId] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationName] [nvarchar](200) NULL,
 CONSTRAINT [PK_NominatingOrganization] PRIMARY KEY CLUSTERED 
(
	[OrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE INDEX [IX_NominatingOrganization_OrganizationName] ON [dbo].[NominatingOrganization] ([OrganizationName])
