CREATE TABLE [dbo].[NomineeSponsor](
	[NomineeSponsorId] [int] IDENTITY(1,1) NOT NULL,
	[LegacyNomineeId] [int] NULL,
	[Organization] [nvarchar](125) NULL,
	[OrganizationId] [int] NULL,
	[LastName] [nvarchar](100) NULL,
	[FirstName] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Title] [nvarchar](100) NULL,
	[Address1] [nvarchar](100) NULL,
	[Address2] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[StateId] [int] NULL,
	[ZipCode] [nvarchar](20) NULL,
	[CountryId] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime2](0) NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime2](0) NULL,
	[DeletedFlag] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime2](0) NULL,
 CONSTRAINT [PK_NomineeSponsor] PRIMARY KEY CLUSTERED 
(
	[NomineeSponsorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY], 
    CONSTRAINT [FK_NomineeSponsor_NominatingOrganization] FOREIGN KEY ([OrganizationId]) REFERENCES [NominatingOrganization]([OrganizationId])
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[NomineeSponsor] ADD  DEFAULT ((0)) FOR [DeletedFlag]
GO

ALTER TABLE [dbo].[NomineeSponsor]  ADD  CONSTRAINT [FK_NomineeSponsor_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO

ALTER TABLE [dbo].[NomineeSponsor] CHECK CONSTRAINT [FK_NomineeSponsor_Country]
GO

ALTER TABLE [dbo].[NomineeSponsor]  ADD  CONSTRAINT [FK_NomineeSponsor_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
GO

ALTER TABLE [dbo].[NomineeSponsor] CHECK CONSTRAINT [FK_NomineeSponsor_State]
GO