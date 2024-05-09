CREATE TABLE [dbo].[MeetingRegistrationComment](
	[MeetingRegistrationCommentId] [int] IDENTITY(1,1) NOT NULL,
	[MeetingRegistrationId] [int] NOT NULL,
	[InternalComments] [varchar](1000) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime2](0) NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime2](0) NULL,
	[DeletedFlag] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime2](0) NULL,
 CONSTRAINT [PK_MeetingRegistrationComment] PRIMARY KEY CLUSTERED 
(
	[MeetingRegistrationCommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], 
    CONSTRAINT [FK_MeetingRegistrationComment_MeetingRegistration] FOREIGN KEY ([MeetingRegistrationId]) REFERENCES [MeetingRegistration]([MeetingRegistrationId])
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MeetingRegistrationComment] ADD  CONSTRAINT [DF_MeetingRegistrationComment_DeletedFlag]  DEFAULT ((0)) FOR [DeletedFlag]
GO 

CREATE INDEX [IX_MeetingRegistrationComment_MeetingRegistrationId] ON [dbo].[MeetingRegistrationComment] ([MeetingRegistrationId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for meeting registration comments',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationComment',
    @level2type = N'COLUMN',
    @level2name = N'MeetingRegistrationCommentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for meeting registration',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationComment',
    @level2type = N'COLUMN',
    @level2name = N'MeetingRegistrationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Internal comments',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistrationComment',
    @level2type = N'COLUMN',
    @level2name = N'InternalComments'