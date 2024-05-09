CREATE TABLE [dbo].[ApplicationTopicCodes] (
	[ApplicationTopicCodeId] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationId] [int] NOT NULL,
	[TopicCode] [nvarchar](2000) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime2](0) NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime2](0) NULL,
	[DeletedFlag] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime2](0) NULL,
PRIMARY KEY CLUSTERED 
(
	[ApplicationTopicCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApplicationTopicCodes]  ADD  CONSTRAINT [FK_ApplicationTopicCodes_Application] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[Application] ([ApplicationId])
GO

ALTER TABLE [dbo].[ApplicationTopicCodes] CHECK CONSTRAINT [FK_ApplicationTopicCodes_Application]
GO
ALTER TABLE [dbo].[ApplicationTopicCodes] ADD  DEFAULT ((0)) FOR [DeletedFlag]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identifier for an application''s Topic Codes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplicationTopicCodes', @level2type=N'COLUMN',@level2name=N'ApplicationTopicCodeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identifier for an application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplicationTopicCodes', @level2type=N'COLUMN',@level2name=N'ApplicationId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Topic Codes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplicationTopicCodes', @level2type=N'COLUMN',@level2name=N'TopicCode'
GO

CREATE INDEX [IX_ApplicationTopicCodes_ApplicationId] ON [dbo].[ApplicationTopicCodes] ([ApplicationId],[DeletedFlag]) 
