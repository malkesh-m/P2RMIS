CREATE TABLE [dbo].[TriggerErrorLog]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ErrorMessage] VARCHAR(800) NULL, 
    [ErrorLine] VARCHAR(800) NULL, 
    [ErrorProcedure] VARCHAR(800) NULL, 
    [Timestamp] DATETIME NULL
)
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[TriggerErrorLog] TO [web-p2rmis]
    AS [dbo];
