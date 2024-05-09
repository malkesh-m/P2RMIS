CREATE TABLE [dbo].[FileStoreMain] (
    [FileStoreMainId] INT              IDENTITY (1, 1) NOT NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_FileStoreMain_rowguid] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [ActiveVersion]   INT              NOT NULL,
    CONSTRAINT [PK_FileStoreMain] PRIMARY KEY CLUSTERED ([FileStoreMainId] ASC)
);

