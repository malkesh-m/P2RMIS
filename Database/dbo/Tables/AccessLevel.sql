CREATE TABLE [dbo].[AccessLevel]
(
	[AccessLevelId] INT NOT NULL PRIMARY KEY, 
    [AccessLevel] VARCHAR(20) NOT NULL
)
GO
GRANT SELECT
    ON OBJECT::[dbo].[AccessLevel] TO [web-p2rmis]
    AS [dbo];