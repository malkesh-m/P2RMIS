CREATE TABLE [dbo].[ProfileTypeRole]
(
	[ProfileTypeRoleId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProfileTypeId] INT NOT NULL, 
    [SystemRoleId] INT NULL 
)
