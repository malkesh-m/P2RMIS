CREATE TABLE [dbo].[ClientElement]
(
	[ClientElementId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ClientId] INT NOT NULL, 
	[ElementTypeId] INT NOT NULL,
    [ElementIdentifier] VARCHAR(15) NOT NULL, 
    [ElementAbbreviation] VARCHAR(25) NOT NULL,
	[ElementDescription] VARCHAR(250) NOT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ClientElement_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]), 
    CONSTRAINT [FK_ClientElement_ElementType] FOREIGN KEY ([ElementTypeId]) REFERENCES [ElementType]([ElementTypeId])

)

GO

CREATE INDEX [IX_ClientElement_ClientId] ON [dbo].[ClientElement] ([ClientId],[ElementAbbreviation])

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ClientElement] TO [web-p2rmis]
    AS [dbo];