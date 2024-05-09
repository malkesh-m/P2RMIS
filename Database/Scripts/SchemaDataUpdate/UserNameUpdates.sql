--May need run manually with top(5000)
UPDATE [User]
SET UserLogin = SYS_Users.UserID
FROM [User] INNER JOIN
[$(P2RMIS)].dbo.SYS_Users SYS_Users ON [User].PersonId = SYS_Users.Person_ID
WHERE [User].account_status_id > 9 AND UserLogin <> SYS_Users.UserID