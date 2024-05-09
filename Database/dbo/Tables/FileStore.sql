CREATE TABLE [dbo].[FileStore] (
    [FileID]           INT              IDENTITY (1, 1) NOT NULL,
    [FileStoreID]      UNIQUEIDENTIFIER CONSTRAINT [DF__FileStore__FileS__2B0A656D] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [FileStoreContent] VARCHAR (MAX)    NULL,
    [FileStoreName]    NVARCHAR (MAX)   NULL,
    [FileContentType]  NVARCHAR (MAX)   NULL,
    [FileStoreSize]    BIGINT           NULL,
    [FileStoreExt]     NVARCHAR (255)   NULL,
    [FileAuthor]       NVARCHAR (100)   NULL,
    [FileComment]      NVARCHAR (255)   NULL,
    [CreatedBy]        INT              NULL,
    [CreatedDate]      DATETIME         NULL,
    [ModifiedBy]       INT              NULL,
    [ModifiedDate]     DATETIME         NULL,
    [VersionNumber]    INT              NOT NULL,
    [FileStoreMainId]  INT              NOT NULL,
    CONSTRAINT [PK_FileID] PRIMARY KEY CLUSTERED ([FileID] ASC),
    CONSTRAINT [FK_FileStore_FileStoreMain] FOREIGN KEY ([FileStoreMainId]) REFERENCES [dbo].[FileStoreMain] ([FileStoreMainId]),
    CONSTRAINT [UQ__FileStor__A82754AB5441852A] UNIQUE NONCLUSTERED ([FileStoreID] ASC)
);

