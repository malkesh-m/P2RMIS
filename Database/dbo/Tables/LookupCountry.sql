CREATE TABLE [dbo].[LookupCountry] (
    [CountryID]   INT            IDENTITY (1, 1) NOT NULL,
    [CountryCode] VARCHAR(5)   CONSTRAINT [DF__Countries__Count__7814D14C] DEFAULT ('') NULL,
    [CountryName] VARCHAR(100) CONSTRAINT [DF__Countries__Count__7908F585] DEFAULT ('') NULL,
    CONSTRAINT [PK__Countrie__3214EC07762C88DA] PRIMARY KEY CLUSTERED ([CountryID] ASC)
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Refactored: Table renamed to Country',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'LookupCountry',
    @level2type = NULL,
    @level2name = NULL