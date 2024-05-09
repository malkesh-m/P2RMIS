CREATE PROCEDURE [dbo].[uspRemoveTaskFromRole]
	@RoleName varchar(50),
	@TaskName varchar(50)
AS
BEGIN
	DECLARE @RoleId int,
	@TaskId int
	SELECT @RoleId = SystemRoleId FROM SystemRole WHERE SystemRoleName = @RoleName;
	SELECT @TaskId = SystemTaskId FROM SystemTask WHERE TaskName = @TaskName;
	IF (@RoleId IS NOT NULL AND @TaskId IS NOT NULL)
	BEGIN
		DELETE FROM RoleTask
		WHERE SystemRoleId = @RoleId AND SystemTaskId = @TaskId
	END
	ELSE
	BEGIN
		RETURN 'Role or Task name was not found'
	END
END

