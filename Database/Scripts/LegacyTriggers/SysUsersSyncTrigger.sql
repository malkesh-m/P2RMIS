CREATE TRIGGER [SysUsersSyncTrigger]
ON [$(P2RMIS)].dbo.SYS_Users
FOR INSERT
AS
BEGIN
	SET NOCOUNT ON
		--INSERT
		--In legacy P2RMIS Person comes first. In new P2RMIS User comes first.  Therefore we must update User at this point.
		UPDATE u
		SET UserLogin = inserted.UserID
		FROM [$(DatabaseName)].dbo.[User] u INNER JOIN
			inserted ON u.PersonId = inserted.Person_ID INNER JOIN
			[$(DatabaseName)].dbo.ViewUserAccountStatus ViewUserAccountStatus ON u.UserId = ViewUserAccountStatus.UserId
		WHERE ViewUserAccountStatus.AccountStatusId = 3 AND ViewUserAccountStatus.AccountStatusReasonId = 9
END