CREATE TABLE [dbo].[NomineeAffected](
	[NomineeAffectedId] [int] IDENTITY(1,1) NOT NULL,
	[NomineeAffected] [nvarchar](16) NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [PK_Nominee_Affected] PRIMARY KEY CLUSTERED 
(
	[NomineeAffectedId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
