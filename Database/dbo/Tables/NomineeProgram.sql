CREATE TABLE [dbo].[NomineeProgram](
	[NomineeProgramid] [int] IDENTITY(1,1) NOT NULL,
	[NomineeId] [int] NOT NULL,
	[LegacyNomineeId] [int] NULL,
	[NomineeSponsorId] [int] NULL,
	[ProgramYearId] [int] NULL,
	[NomineeTypeId] [int] NULL,
	[Score] [int] NULL,
	[NomineeAffectedId] [int] NULL,
	[PrimaryFlag] [bit] NOT NULL,
	[DiseaseSite] [nvarchar](20) NULL,
	[Comments] [nvarchar](255) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime2](0) NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime2](0) NULL,
	[DeletedFlag] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime2](0) NULL,
 CONSTRAINT [PK_NomineeProgram] PRIMARY KEY CLUSTERED 
(
	[NomineeProgramid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[NomineeProgram] ADD  DEFAULT ((0)) FOR [DeletedFlag]
GO

ALTER TABLE [dbo].[NomineeProgram]  ADD CONSTRAINT [FK_NomineeProgram_Nominee] FOREIGN KEY([NomineeId])
REFERENCES [dbo].[Nominee] ([NomineeId])
GO

ALTER TABLE [dbo].[NomineeProgram] CHECK CONSTRAINT [FK_NomineeProgram_Nominee]
GO

ALTER TABLE [dbo].[NomineeProgram]  ADD CONSTRAINT [FK_NomineeProgram_Nominee_Affected] FOREIGN KEY([NomineeAffectedId])
REFERENCES [dbo].[NomineeAffected] ([NomineeAffectedId])
GO

ALTER TABLE [dbo].[NomineeProgram] CHECK CONSTRAINT [FK_NomineeProgram_Nominee_Affected]
GO

ALTER TABLE [dbo].[NomineeProgram] ADD CONSTRAINT [FK_NomineeProgram_NomineeSponsor] FOREIGN KEY([NomineeSponsorId])
REFERENCES [dbo].[NomineeSponsor] ([NomineeSponsorId])
GO

ALTER TABLE [dbo].[NomineeProgram] CHECK CONSTRAINT [FK_NomineeProgram_NomineeSponsor]
GO

ALTER TABLE [dbo].[NomineeProgram] ADD CONSTRAINT [FK_NomineeProgram_NomineeType] FOREIGN KEY([NomineeTypeId])
REFERENCES [dbo].[NomineeType] ([NomineeTypeId])
GO

ALTER TABLE [dbo].[NomineeProgram] CHECK CONSTRAINT [FK_NomineeProgram_NomineeType]
GO

ALTER TABLE [dbo].[NomineeProgram]  ADD CONSTRAINT [FK_NomineeProgram_ProgramYear] FOREIGN KEY([ProgramYearId])
REFERENCES [dbo].[ProgramYear] ([ProgramYearId])
GO

ALTER TABLE [dbo].[NomineeProgram] CHECK CONSTRAINT [FK_NomineeProgram_ProgramYear]
GO

