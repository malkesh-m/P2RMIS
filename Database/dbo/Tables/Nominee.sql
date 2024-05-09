CREATE TABLE [dbo].[Nominee](
	[NomineeId] [int] IDENTITY(1,1) NOT NULL,
	[PrefixId] [int] NULL,
	[LastName] [nvarchar](100) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[GenderId] [int] NULL,
	[EthnicityId] [int] NULL,
	[UserId] [int] NULL,
	[Email] [nvarchar](100) NULL,
	[DOB] [date] NULL,
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
 CONSTRAINT [PK_Nominee] PRIMARY KEY CLUSTERED 
(
	[NomineeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Nominee] ADD  DEFAULT ((0)) FOR [DeletedFlag]
GO

ALTER TABLE [dbo].[Nominee]  ADD  CONSTRAINT [FK_Nominee_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO

ALTER TABLE [dbo].[Nominee] CHECK CONSTRAINT [FK_Nominee_Country]
GO

ALTER TABLE [dbo].[Nominee]  ADD  CONSTRAINT [FK_Nominee_Ethnicity] FOREIGN KEY([EthnicityId])
REFERENCES [dbo].[Ethnicity] ([EthnicityID])
GO

ALTER TABLE [dbo].[Nominee] CHECK CONSTRAINT [FK_Nominee_Ethnicity]
GO

ALTER TABLE [dbo].[Nominee]  ADD  CONSTRAINT [FK_Nominee_Gender] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Gender] ([GenderID])
GO

ALTER TABLE [dbo].[Nominee] CHECK CONSTRAINT [FK_Nominee_Gender]
GO

ALTER TABLE [dbo].[Nominee]  ADD  CONSTRAINT [FK_Nominee_Prefix] FOREIGN KEY([PrefixId])
REFERENCES [dbo].[Prefix] ([PrefixId])
GO

ALTER TABLE [dbo].[Nominee] CHECK CONSTRAINT [FK_Nominee_Prefix]
GO

ALTER TABLE [dbo].[Nominee]  ADD  CONSTRAINT [FK_Nominee_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
GO

ALTER TABLE [dbo].[Nominee] CHECK CONSTRAINT [FK_Nominee_State]
GO

ALTER TABLE [dbo].[Nominee]  ADD  CONSTRAINT [FK_Nominee_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[Nominee] CHECK CONSTRAINT [FK_Nominee_User]
GO
