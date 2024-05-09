CREATE TABLE [dbo].[PersonPhoto]
(
	[PhotoId] INT NOT NULL PRIMARY KEY, 
    [PersonId] INT NOT NULL, 
    [Photo] VARBINARY(MAX) NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [ModifiedBy] datetime2(0) NULL
)

GO

CREATE UNIQUE INDEX [IX_UN_PersonPhoto_PersonId] ON [dbo].[PersonPhoto] ([PersonId])
