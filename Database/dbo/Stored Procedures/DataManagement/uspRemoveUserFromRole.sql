CREATE PROCEDURE [dbo].[uspRemoveUserFromRole]
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
		DELETE FROM UserSystemRole
		WHERE SystemRoleId = @RoleId AND UserId = @UserId
	END
	ELSE
	BEGIN
		RETURN 'Role or User name was not found'
	END
END
