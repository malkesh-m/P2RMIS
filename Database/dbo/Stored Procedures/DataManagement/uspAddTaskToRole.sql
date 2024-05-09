CREATE PROCEDURE [dbo].[uspAddTaskToRole]
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
		IF (SELECT COUNT(*) FROM RoleTask WHERE SystemRoleId = @RoleId AND SystemTaskId = @TaskId) > 0
			RETURN 'RoleTask assignement already exists'
		ELSE
			INSERT INTO RoleTask (SystemRoleId, SystemTaskId)
			VALUES (@RoleId, @TaskId);
	END
	ELSE
		RETURN 'Role or Task name was not found'
END