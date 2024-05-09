CREATE TABLE [dbo].[ReviewStatusType]
(
	[ReviewStatusTypeId] INT NOT NULL PRIMARY KEY, 
    [StatusTypeName] VARCHAR(40) NOT NULL
)
GO
GRANT SELECT
    ON OBJECT::[dbo].[ReviewStatusType] TO [web-p2rmis]
    AS [dbo];