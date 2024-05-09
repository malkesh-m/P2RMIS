CREATE PROCEDURE [dbo].[uspActivateLegacyProfile]
	@UserId int,
	@SystemRoleId int,
	@ClientId int
AS
	BEGIN
		--Insert user name for user
		UPDATE [User]
		SET UserLogin = SYS_Users.UserID
		FROM [User] INNER JOIN
		[$(P2RMIS)].dbo.SYS_Users SYS_Users ON [User].PersonID = SYS_Users.Person_ID 
		WHERE [User].UserId = @UserId AND UserLogin IS NULL
		--Add profile type based on whether user is an employee
		IF (NOT EXISTS(SELECT * FROM UserProfile INNER JOIN UserInfo ON UserProfile.UserInfoId = UserInfo.UserInfoID WHERE UserInfo.UserID = @UserId))
		BEGIN
			INSERT INTO UserProfile ([UserInfoId]
			   ,[ProfileTypeId]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
			Select UserInfo.UserInfoID, CASE PPL_People.Employee WHEN 1 THEN 3 ELSE 4 END, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime()
			FROM UserInfo INNER JOIN
			[User] ON UserInfo.UserID = [User].UserID INNER JOIN
			[$(P2RMIS)].dbo.PPL_People PPL_People ON [User].PersonID = PPL_People.Person_ID
			WHERE UserInfo.UserID = @UserId
		END
		--Add clients for user
		IF (NOT EXISTS(SELECT * FROM UserClient WHERE UserClient.UserID = @UserId))
		BEGIN
			INSERT INTO UserClient ([UserID]
           ,[ClientID]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[CreatedBy]
           ,[CreatedDate])
		   VALUES (@UserId, @ClientId, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime())
		END
		--Add user account status for user
		IF (NOT EXISTS(SELECT * FROM UserAccountStatus WHERE UserAccountStatus.UserID = @UserId))
		BEGIN
			INSERT INTO UserAccountStatus ([UserId]
           ,[AccountStatusId]
           ,[AccountStatusReasonId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		   VALUES (@UserId, 13, 1, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime())
		END
		--Add user account status for user
		IF (NOT EXISTS(SELECT * FROM UserSystemRole WHERE UserSystemRole.UserID = @UserId))
		BEGIN
			INSERT INTO UserSystemRole ([UserID]
           ,[SystemRoleId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		   VALUES (@UserId, @SystemRoleId, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime())
		END
	END
