CREATE TABLE [dbo].[ApplicationReviewStatus]
(
	[ApplicationReviewStatusId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[PanelApplicationId] INT NULL,
    [ReviewStatusId] INT NOT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationReviewStatus_ReviewStatus] FOREIGN KEY ([ReviewStatusId]) REFERENCES [ReviewStatus]([ReviewStatusId]), 
    CONSTRAINT [FK_ApplicationReviewStatus_PanelApplication] FOREIGN KEY ([PanelApplicationId]) REFERENCES [PanelApplication]([PanelApplicationId]), 
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_ApplicationReviewStatus_PanelApplicationId_ReviewStatusId] 
	ON [dbo].[ApplicationReviewStatus] ([PanelApplicationId], [ReviewStatusId])
	WHERE [DeletedFlag] = 0;
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationReviewStatus] TO [web-p2rmis]
    AS [dbo];
GO


CREATE NONCLUSTERED INDEX [IX_ApplicationReviewStatus_ReviewStatusId_DeletedFlag] ON [dbo].[ApplicationReviewStatus]
(
	[ReviewStatusId] ASC,
	[DeletedFlag] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
