CREATE TABLE [dbo].[ClientScoringScale]
(
	[ClientScoringId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[ClientId] INT NOT NULL,
	[ClientScoringScaleLegendId] INT NULL,
    [ScoreType] VARCHAR(10) NOT NULL, 
    [HighValue] DECIMAL(4, 1) NOT NULL, 
    [HighValueDescription] VARCHAR(4000) NULL, 
    [MiddleValue] DECIMAL(4, 1) NULL, 
    [MiddleValueDescription] VARCHAR(4000) NULL, 
    [LowValue] DECIMAL(4, 1) NOT NULL, 
    [LowValueDescription] VARCHAR(4000) NULL, 
    [CreatedBy] INT NOT NULL, 
    [CreatedDate] datetime2(0) NOT NULL, 
    [ModifiedBy] INT NOT NULL, 
    [ModifiedDate] datetime2(0) NOT NULL, 
    CONSTRAINT [FK_ClientScoringScale_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]), 
    CONSTRAINT [FK_ClientScoringScale_ClientScoringScaleLegend] FOREIGN KEY ([ClientScoringScaleLegendId]) REFERENCES [ClientScoringScaleLegend]([ClientScoringScaleLegendId])
)

GO


CREATE INDEX [IX_ClientScoringScale_ClientId_ClientScoringId] ON [dbo].[ClientScoringScale] ([ClientId],[ClientScoringId])

GO
GRANT SELECT
    ON OBJECT::[dbo].[ClientScoringScale] TO [web-p2rmis]
    AS [dbo];