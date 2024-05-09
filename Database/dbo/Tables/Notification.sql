CREATE TABLE [dbo].[Notification](
	[NotificationId] [int] NOT NULL IDENTITY,
	[Label] [varchar](20) NOT NULL,
	[Message] [varchar](1000) NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[DeletedFlag] [bit] NOT NULL DEFAULT 0,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Notification identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Notification',
    @level2type = N'COLUMN',
    @level2name = N'NotificationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Label or type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Notification',
    @level2type = N'COLUMN',
    @level2name = N'Label'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Notification message if any',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Notification',
    @level2type = N'COLUMN',
    @level2name = N'Message'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Notification display period''s start date',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Notification',
    @level2type = N'COLUMN',
    @level2name = N'StartDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Notification display period of end date',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Notification',
    @level2type = N'COLUMN',
    @level2name = N'EndDate'