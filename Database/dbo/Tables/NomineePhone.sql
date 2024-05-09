CREATE TABLE [dbo].[NomineePhone](
	[NomineePhoneId] [int] IDENTITY(1,1) NOT NULL,
	[NomineeId] [int] NULL,
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
 CONSTRAINT [PK_NomineePhone] PRIMARY KEY CLUSTERED 
(
	[NomineePhoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[NomineePhone] ADD  DEFAULT ((6)) FOR [PhoneTypeId]
GO

ALTER TABLE [dbo].[NomineePhone] ADD  CONSTRAINT [DF_NomineePhone_IsPrimary]  DEFAULT ((1)) FOR [PrimaryFlag]
GO

ALTER TABLE [dbo].[NomineePhone] ADD  DEFAULT ((0)) FOR [DeletedFlag]
GO

ALTER TABLE [dbo].[NomineePhone]  ADD  CONSTRAINT [FK_NomineePhone_Nominee] FOREIGN KEY([NomineeId])
REFERENCES [dbo].[Nominee] ([NomineeId])
GO

ALTER TABLE [dbo].[NomineePhone] CHECK CONSTRAINT [FK_NomineePhone_Nominee]
GO

ALTER TABLE [dbo].[NomineePhone]  ADD  CONSTRAINT [FK_NomineePhone_PhoneType] FOREIGN KEY([PhoneTypeId])
REFERENCES [dbo].[PhoneType] ([PhoneTypeId])
GO

ALTER TABLE [dbo].[NomineePhone] CHECK CONSTRAINT [FK_NomineePhone_PhoneType]
GO
