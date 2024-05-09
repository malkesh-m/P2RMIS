UPDATE [User]
SET UserLogin = SYS_Users.UserID
FROM ViewUser [User] INNER JOIN
	ViewUserAccountStatus UserAccountStatus ON [User].UserID = UserAccountStatus.UserId INNER JOIN
	[$(P2RMIS)].dbo.SYS_Users SYS_Users ON [User].PersonID = SYS_Users.Person_ID
WHERE UserAccountStatus.AccountStatusId = 3 AND UserAccountStatus.AccountStatusReasonId = 9 AND ISNULL(UserLogin, '') <> ISNULL(SYS_Users.UserID, '');