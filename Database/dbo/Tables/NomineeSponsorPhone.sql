CREATE TABLE [dbo].[NomineeSponsorPhone](
	[NomineeSponsorPhoneId] [int] IDENTITY(1,1) NOT NULL,
	[NomineeSponsorId] [int] NULL,
	[PhoneTypeId] [int] NULL,
	[Phone] [nvarchar](50) NULL,
	[Extension] [nvarchar](50) NULL,
	[PrimaryFlag] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedFlag] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime2](0) NULL,
 CONSTRAINT [PK_NomineeSponsorPhone] PRIMARY KEY CLUSTERED 
(
	[NomineeSponsorPhoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[NomineeSponsorPhone] ADD  DEFAULT ((6)) FOR [PhoneTypeId]
GO

ALTER TABLE [dbo].[NomineeSponsorPhone] ADD  CONSTRAINT [DF_NomineeSponsorPhone_IsPrimary]  DEFAULT ((1)) FOR [PrimaryFlag]
GO

ALTER TABLE [dbo].[NomineeSponsorPhone] ADD  DEFAULT ((0)) FOR [DeletedFlag]
GO

ALTER TABLE [dbo].[NomineeSponsorPhone]  ADD  CONSTRAINT [FK_NomineeSponsorPhone_NomineeSponsor] FOREIGN KEY([NomineeSponsorId])
REFERENCES [dbo].[NomineeSponsor] ([NomineeSponsorId])
GO

ALTER TABLE [dbo].[NomineeSponsorPhone] CHECK CONSTRAINT [FK_NomineeSponsorPhone_NomineeSponsor]
GO

ALTER TABLE [dbo].[NomineeSponsorPhone]  ADD  CONSTRAINT [FK_NomineeSponsorPhone_PhoneType] FOREIGN KEY([PhoneTypeId])
REFERENCES [dbo].[PhoneType] ([PhoneTypeId])
GO

ALTER TABLE [dbo].[NomineeSponsorPhone] CHECK CONSTRAINT [FK_NomineeSponsorPhone_PhoneType]
GO
