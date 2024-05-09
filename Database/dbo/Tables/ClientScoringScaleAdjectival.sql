CREATE TABLE [dbo].[ClientScoringScaleAdjectival]
(
	[ClientScoringScaleAdjectivalId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY NONCLUSTERED,
	[ClientScoringId] INT NOT NULL,
	[ScoreLabel] VARCHAR(500) NOT NULL,
	[NumericEquivalent] INT NOT NULL,
    [CreatedBy] INT NOT NULL, 
    [CreatedDate] datetime2(0) NOT NULL, 
    [ModifiedBy] INT NOT NULL, 
    [ModifiedDate] datetime2(0) NOT NULL, 
    CONSTRAINT [FK_ClientScoringScaleAdjectival_ClientScoringScale] FOREIGN KEY ([ClientScoringId]) REFERENCES [ClientScoringScale]([ClientScoringId]), 
    CONSTRAINT [UN_ClientScoringScaleAdjectival_ClientScoringId_NumericEquivalent] UNIQUE ([ClientScoringId],[NumericEquivalent])
)

GO

CREATE CLUSTERED INDEX [IX_ClientScoringScaleAdjectival_ClientScoringId_NumericEquivalent] ON [dbo].[ClientScoringScaleAdjectival] ([ClientScoringId],[NumericEquivalent])

GO
GRANT SELECT
    ON OBJECT::[dbo].[ClientScoringScaleAdjectival] TO [web-p2rmis]
    AS [dbo];