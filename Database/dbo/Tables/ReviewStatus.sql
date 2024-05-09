CREATE TABLE [dbo].[ReviewStatus]
(
	[ReviewStatusId] INT NOT NULL PRIMARY KEY, 
	[ReviewStatusTypeId] INT NOT NULL,
    [ReviewStatusLabel] VARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_ReviewStatus_ReviewStatusType] FOREIGN KEY ([ReviewStatusTypeId]) REFERENCES [ReviewStatusType]([ReviewStatusTypeId])
)

GO
GRANT SELECT
    ON OBJECT::[dbo].[ReviewStatus] TO [web-p2rmis]
    AS [dbo];
