CREATE TABLE [dbo].[UserPolicy]
(
	[UserPolicyId] INT NOT NULL PRIMARY KEY IDENTITY, 
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
    CONSTRAINT [FK_UserPolicy_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_UserPolicy_Policy] FOREIGN KEY ([PolicyId]) REFERENCES [Policy]([PolicyId])
)
