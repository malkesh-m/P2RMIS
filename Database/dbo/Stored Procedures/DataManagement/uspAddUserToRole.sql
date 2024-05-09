CREATE PROCEDURE [dbo].[uspAddUserToRole]
	@UserName varchar(50),
	@RoleName varchar(50)
AS
BEGIN
	DECLARE @RoleId int,
	@UserId int
	SELECT @RoleId = SystemRoleId FROM SystemRole WHERE SystemRoleName = @RoleName;
	SELECT @UserId = UserId FROM [User] WHERE UserLogin = @UserName;
	IF (@RoleId IS NOT NULL AND @UserId IS NOT NULL)
	BEGIN
		IF (SELECT COUNT(*) FROM UserSystemRole WHERE SystemRoleId = @RoleId AND UserId = @UserId) > 0
			RETURN 'UserSystemRole assignement already exists'
		ELSE
			INSERT INTO UserSystemRole ([UserID]
           ,[SystemRoleId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
			VALUES (@UserId, @RoleId, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime());
	END
	ELSE
		RETURN 'User or Role name was not found'
END
