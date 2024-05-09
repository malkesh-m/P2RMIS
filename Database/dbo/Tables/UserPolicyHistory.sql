CREATE TABLE [dbo].[UserPolicyHistory]
(
	[UserPolicyHistoryId] INT NOT NULL PRIMARY KEY,
	[UserId] INT NOT NULL,
	[PolicyId] INT NOT NULL,
    [Active] bit NOT NULL Default 1, 
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
)
