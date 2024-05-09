-- copy value from column RoleLkpID to SystemRoleId
UPDATE usr 
SET usr.SystemRoleId = rl.RoleLkpID
FROM [UserSystemRole] usr
INNER JOIN [UserSystemRole] rl
ON usr.UserSystemRoleID = rl.UserSystemRoleID AND rl.RoleLkpID > 0
