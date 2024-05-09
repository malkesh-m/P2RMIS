CREATE VIEW [dbo].[ViewRolePermissions]
	AS SELECT        SystemRole.SystemRoleName, SystemRole.SystemRoleId, SystemTask.TaskName, SystemTask.TaskDescription, SystemTask.SystemTaskId, SystemOperation.OperationName, SystemOperation.OperationDescription, 
                         SystemOperation.SystemOperationId
FROM            SystemRole INNER JOIN
                         RoleTask ON SystemRole.SystemRoleId = RoleTask.SystemRoleId INNER JOIN
                         SystemTask ON RoleTask.SystemTaskId = SystemTask.SystemTaskId INNER JOIN
                         TaskOperation ON SystemTask.SystemTaskId = TaskOperation.SystemTaskId INNER JOIN
                         SystemOperation ON TaskOperation.SystemOperationId = SystemOperation.SystemOperationId
