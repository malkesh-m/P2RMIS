CREATE TABLE [dbo].[Client] (
    [ClientID]   INT            IDENTITY (1, 1) NOT NULL,
    [ClientAbrv] NVARCHAR (10)  NOT NULL,
    [ClientDesc] NVARCHAR (100) NOT NULL,
	[SummaryStatementModeId] INT NOT NULL DEFAULT 2,
    CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED ([ClientID] ASC), 
    CONSTRAINT [FK_Client_SummaryStatementMode] FOREIGN KEY ([SummaryStatementModeId]) REFERENCES [SummaryStatementMode]([SummaryStatementModeId])
);

GO
GRANT SELECT
    ON OBJECT::[dbo].[Client] TO [web-p2rmis]
    AS [dbo];